using System;
using System.Collections.Generic;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Diagnostics;
using Keysight.ModularInstruments.Core.Register;
using System.Windows.Forms;
namespace Keysight.ModularInstruments
{
    public class SwitchDriverOffsetCal: WyvernCalBase
    {
        //Calibration Variables
        int debugFinalMux = 0x0; //when writing to cal registers make sure to force midscale
        string SwitchRegister = "SwDrvOsAdj";
        string Module = "M9347A";
        string ClkConfig = "ClkaCfg";
        string SlugSerialNumber = "";

        //I have to rename everything again :(
        string[] WyvernMuxRegisters = { "FinalMuxInCfg", "CClkGate", "DdrInterpCfg", "FinalMuxCfg", "DacCfg1", "DemCfg", "ClkaCfg" };
        string[] DebugRegisters = { "FinalMuxInCfg", "CClkGate", "DdrInterpCfg", "FinalMuxCfg", "DacCfg1", "DemCfg", "ClkaCfg", "Status0" };
        int[] CalValues;


        Dictionary<String, int> adjRegVals = new Dictionary<string, int>();
        Dictionary<Tuple<char, char>, int[]> midState = new Dictionary<Tuple<char, char>, int[]>();  //creates a dictionary of type <[char, char],int[]>
        Dictionary<string, uint> saveRegs = new Dictionary<string, uint>();
        Dictionary<string, double> bestErr = new Dictionary<string, double>();
        Dictionary<string, double> bestCode = new Dictionary<string, double>();


        //Calibration  
        double DutClock_MHz = 19200; //19.2GHz
        double ClockPower_dBm = 3;
        double FreqTestSignal;
        int mAverages = 5; //10;
        double SaSpan_MHz = .010;
        double SaRefLevel_dBm = -5;
        double ClockPowerLoss = 0;

        bool opt_sa = true;
        bool Is102HardWare = true;
        int saMeasDiv = 8; //trying to calibrate at 2.4 GHz instead of 1.2 to hit the balun sweet spot.
        int minLsb = 5;
        bool opt_existing = false;
        int niter = 4;      //number of iterations
        public string CalFileName,CalFileDir;

        public SwitchDriverOffsetCal(//string slugSerialNumber,
                                      string wyvernName,
                                      string calDataSavePath,
                                      WyvernRegisterSet regiserset,
                                      MeasureMarkerValueDelegation measureDelegation, PrintTestResultDelegation printTestResultDelegation)
            : base( wyvernName, calDataSavePath, regiserset, measureDelegation, printTestResultDelegation)
        {

        }


        public override void Execute()
        {
            //empty all arrays and dictionaries to avoid issues when running second wyvern
            adjRegVals.Clear();
            midState.Clear();
            saveRegs.Clear();
            bestErr.Clear();
            bestCode.Clear();

            string name = "";

            mPrintTestResultMethod(((uint)ReadWyvernRegister("CClkGate")).ToString("X8"));

            #region Save cal register states
            foreach (string dac in new string[] { "A.", "B." })
            {
                for (int i = 0; i < 16; i++)
                {
                    name = "Msb." + dac + i;		//so the name is: Msb.A.0-15, 
                    adjRegVals.Add(name, readAdj(name));    //then it calls this function which reads the current DAC A/B adjustment value
                                                            //and then it stores it into adjRegValues dictionary
                }
                for (int i = 0; i < 10; i++)
                {
                    foreach (string ps in new string[] { "P", "S" })
                    {
                        name = ps + "Lsb." + dac + i;	//name = PLsbA.0-9 and SLsb == primary significant bit and secondary significant bit
                        adjRegVals.Add(name, readAdj(name));
                    }
                }
            }
            #endregion

            //save current configuration
            LogWyvernRegisters(WyvernMuxRegisters);

            //make sure that wyvern is set up in DDR mode and in NRZ mode
            SetDDRMode();
            SetAClktoStaticNRZ();

            //create mid state for cal
            midState = createMidState();
            setForceMux(true, "Msb.A.0", 0);

            Dictionary<Tuple<string, int>, double> dutyErr = new Dictionary<Tuple<string, int>, double>();

            int adjMin = -63;
            int adjMax = 63;
            double adjGain = double.NaN;

            //Toggle all DAC bits and measure offsets
            foreach (char dac in new char[] { 'A', 'B' })
            {
                //choose which mux to force
                int MuxA = dac == 'A' ? 1 : 0;      //if DAC is A we are forcing mux A, else B
                WriteWyvernRegisterField( "ClkaCfg", "AClkStatic", MuxA);
                WriteWyvernRegisterField( "ClkaCfg", "AClkStatic", MuxA);

                foreach (char psm in new char[] { 'M', 'P', 'S' }) //M,P,S in perl code
                {
                    for (int i = midState[new Tuple<char, char>('A', psm)].Length - 1; i >= 0; --i)
                    {
                        if (psm == 'M' || i >= minLsb)
                        {
                            name = (psm == 'M' ? "Msb" : psm + "Lsb") + "." + dac + "." + i;
                            if (adjGain.Equals(double.NaN))
                            {
                                writeAdj(name, adjMin);
                                measOffset(name, adjMin, dutyErr);
                                writeAdj(name, adjMax);
                                measOffset(name, adjMax, dutyErr);
                                adjGain = (dutyErr[new Tuple<string, int>(name, adjMax)] - dutyErr[new Tuple<string, int>(name, adjMin)]) / (adjMax - adjMin);
                                mPrintTestResultMethod("Adjust gain = " + adjGain);
                            }
                            mPrintTestResultMethod("Calibrating " + name);
                            int code = opt_existing ? adjRegVals[name] : 0;
                            writeAdj(name, code);
                            measOffset(name, code, dutyErr);

                            mPrintTestResultMethod("Starting iterations");
                            int iter = 0;
                            while (iter < niter)        //cycle through iterations
                            {
                                int newCode = (int)Math.Round(code - dutyErr[new Tuple<string, int>(name, code)] / adjGain, 0);
                                mPrintTestResultMethod("Calculated best code = " + newCode);
                                if (newCode > adjMax) newCode = adjMax;
                                if (newCode < adjMin) newCode = adjMin;
                                if (newCode == code)
                                    break;
                                string DebugFileName = name + "iter" + iter.ToString();
                                ++iter;
                                code = newCode;
                                writeAdj(name, code);
                                measOffset(name, code, dutyErr);
                            }
                            mPrintTestResultMethod(String.Format("Best Error for {0} is {1} ", name, code));
                        }
                    }
                }
            }        
            foreach (string regname in saveRegs.Keys)
            {
                WriteWyvernRegister(regname, (int)saveRegs[regname]);

            }
        }


        public void LoadCalDataToModule()
        {
            string pathDir = System.IO.Path.GetDirectoryName(mSavePath + "\\");
            string logfile = System.IO.Path.Combine(pathDir, String.Format("WyvernSwDrvOsAdjs_{0}.txt",  whichWyvern));
            if (System.IO.File.Exists(logfile))
            {
                LoadCalDataToModule(logfile);
            }
            else
            {
                MessageBox.Show("Don't find " + logfile, "ÌבÊ¾");
            }
            

        }
        public void LoadCalDataToModule(string calFile)
        {
            System.IO.StreamReader file;
            string sRegName, sReagValue;
            file = new System.IO.StreamReader(calFile, false);
            string sData = file.ReadLine();
            //sData = file.ReadLine();
            while (sData != null)
            {
                sRegName = sData.Split(',')[0];
                sReagValue = sData.Split(',')[1];
                //WriteWyvernRegisterField(sRegName, "SwDrvOsAdj", Convert.ToInt32(sReagValue, 16));
                WriteWyvernRegister(sRegName,Convert.ToInt32(sReagValue, 16));
                sData = file.ReadLine();
            }
        }

        //public void SaveCalDataToModule()
        //{

        //    int[] SwitchDriverCalValues = new int[39];

        //    //to guarantee register name and mapping read back from module 
        //    foreach (string regname in bestCode.Keys)
        //    {
        //        writeAdj(regname, (int)Math.Round(bestCode[regname]));
        //    }

        //    for (int x = 0; x < 39; x++)
        //    {
        //        string CurrentSwitchReg = "SwDrvOsAdj" + x;
        //        double RegRead;
        //        RegRead = ReadWyvernRegister( CurrentSwitchReg);
        //        SwitchDriverCalValues[x] = (int)RegRead;
           
        //}

        private void LogWyvernRegisters( string[] RegisterNames )
        {
            foreach (string regName in RegisterNames)
            {
                //DUT.Refresh();
                //ReadWyvernRegister( regName);
                //DUT.Refresh();
                saveRegs.Add(regName, (uint)ReadWyvernRegister( regName));
                //ReadWyvernRegister( regName);
                uint regVal = (uint)ReadWyvernRegister( regName);
                mPrintTestResultMethod(String.Format("{0} : {1}", regName, regVal.ToString("X8")));
            }

        }

        public void WriteCalValueToLogFile()
        {
            // writing to network folder until we integrate this test into module level testing.
            CalFileDir = System.IO.Path.GetDirectoryName(mSavePath + "\\");
            CalFileName = String.Format("WyvernSwDrvOsAdjs_{0}.txt",  whichWyvern);
            string logfile = System.IO.Path.Combine(CalFileDir, CalFileName);
            System.IO.StreamWriter file;
            using (file = new System.IO.StreamWriter(logfile, false))
            {
                foreach (string regname in bestCode.Keys)
                {
                    writeAdj(regname, (int)Math.Round(bestCode[regname]));
                }

                for (int x = 0; x < 39; x++)
                {
                    string CurrentSwitchReg = "SwDrvOsAdj" + x;
                    double RegRead;
                    //DUT.Refresh();
                    //ReadWyvernRegister( CurrentSwitchReg);
                    //DUT.Refresh();
                    RegRead = ReadWyvernRegister( CurrentSwitchReg);
                    file.WriteLine(CurrentSwitchReg + "," + "{0:X}", (int)RegRead);

                }
            }
        }

        public int readAdj(string name)
        {
            string[] parse = name.Split('.');				// ==> [Msb][A][0-15] --> what does the name splitting map to 
            string type = parse[0];							// type = Msb
            string ab = parse[1];                           // ab = A
            int i = int.Parse(parse[2]);                    // i = (int)0-15

            string regName = "SwDrvOsAdj";                  // use register SwDrvOsAdj and then reads a specific register named
            string adjAorB = "Adj" + ab;
            string adjEnBP = "AdjEn" + ab + "p";            //status of backgate connection for P-side DAC switch driver, 
            string adjEnBN = "AdjEn" + ab + "n";            //status of backgate connection for N-side DAC switch driver, 0 == shorted to VDDA, 1 == backgate comes from adj

            //maps code from Msb.AB.0-XX to Wyvern registers
            if (type.StartsWith("SL")) regName += (i + 3);
            else if (type.StartsWith("M")) regName += (i + 13);
            else regName += (38 - i); 

            //read register here! but in the gondor way (oooh)
            //driver is incorrectly reading back registers from wyvern 
            //TODO: workaround = refresh, read, read?
            //DUT.Refresh();
            //ReadWyvernRegisterField( regName, adjAorB);
            //DUT.Refresh();
            int code = (int)ReadWyvernRegisterField( regName, adjAorB);
            int enp = (int)ReadWyvernRegisterField( regName, adjEnBP);
            int enn = (int)ReadWyvernRegisterField( regName, adjEnBN);



            //finds sign of code (+/-)
            int val = 0;
            if (enp == 1 && enn == 0) val = code;
            else if (enp == 0 && enn == 1) val = -code;

            return val;
        }

        public Dictionary<Tuple<char, char>, int[]> createMidState(string name = "Msb.A.0")
        {
            string[] parse = name.Split('.');
            char psm = parse[0].ToCharArray()[0];
            char ab = parse[1].ToCharArray()[0];
            int i = int.Parse(parse[2]);
            Dictionary<Tuple<char, char>, int[]> state = new Dictionary<Tuple<char, char>, int[]>();

            state.Add(new Tuple<char, char>('A', 'S'), new int[10]);
            state.Add(new Tuple<char, char>('B', 'S'), new int[10]);
            state.Add(new Tuple<char, char>('A', 'M'), new int[16]);
            state.Add(new Tuple<char, char>('B', 'M'), new int[16]);
            state.Add(new Tuple<char, char>('A', 'P'), new int[10]);
            state.Add(new Tuple<char, char>('B', 'P'), new int[10]);



            foreach (char dac in new char[] { 'A', 'B' })
            {
                for (int n = 0; n < 10; n++)
                {

                    int val;
                    if (psm == 'P' || psm == 'S')
                    {
                        if (i == 8) val = (n == 7) ? 1 : 0;
                        else val = (n == 8) ? 1 : 0;

                        state[new Tuple<char, char>(dac, psm)][n] = val;
                        state[new Tuple<char, char>(dac, psm == 'P' ? 'S' : 'P')][n] = val == 1 ? 0 : 1;
                    }
                    else
                    {
                        state[new Tuple<char, char>(dac, 'P')][n] = 0;
                        state[new Tuple<char, char>(dac, 'S')][n] = 1;
                    }
                }

                for (int n = 0; n < 8; n++)
                {
                    if (psm == 'M')
                    {
                        state[new Tuple<char, char>(dac, 'M')][(i + n) % 16] = 0;
                        state[new Tuple<char, char>(dac, 'M')][(i + n + 8) % 16] = 1;
                    }
                    else
                    {
                        state[new Tuple<char, char>(dac, 'M')][n] = 1;
                        state[new Tuple<char, char>(dac, 'M')][n + 8] = 0;
                    }

                }
            }
            return state;


        }

        void setForceMux(bool force, string name, int opt_val)
        {
            string[] parse = name.Split('.');
            char opt_psm = parse[0].ToCharArray()[0];
            char opt_dac = parse[1].ToCharArray()[0];
            int opt_n = int.Parse(parse[2]);
            //DUT.Refresh();
            WriteWyvernRegister( "FinalMuxInCfg", debugFinalMux);
            //# write FinalMuxIn registers
            foreach (char dac in new char[] { 'A', 'B' })
            {
                int iStart = dac == 'A' ? 0 : 32;
                int iStop = iStart + 31;
                foreach (char psm in new char[] { 'P', 'S', 'M' })
                {
                    int valHi = 0;
                    int valLo = 0;
                    int nb = midState[new Tuple<char, char>(dac, psm)].Length;
                    for (int n = 0; n < nb; n++)
                    {
                        if (dac == opt_dac && psm == opt_psm && n == opt_n)
                        {
                            valHi += 1 << n;
                        }
                        else
                        {
                            valHi += midState[new Tuple<char, char>(dac, psm)][n] << n;
                            valLo += midState[new Tuple<char, char>(dac, psm)][n] << n;
                        }
                    }

                    for (int i = iStart; i <= iStop; i++)
                    {
                        string regName = "FinalMuxIn" + psm + i;
                        string fieldName = psm == 'M' ? "" : psm + "Lsb";
                        int progVal;
                        if (opt_val == 1)
                        {
                            //for DVM, static 1
                            //for SA, 00111111 pattern
                            progVal = opt_sa && i % saMeasDiv < 2 ? valLo : valHi;
                        }
                        else if (opt_val == 0)
                        {
                            //for DVM, static 0
                            //for SA, 1100000 pattern
                            progVal = opt_sa && i % saMeasDiv < 2 ? valHi : valLo;
                        }
                        else
                        {
                            //0101 pattern
                            progVal = i % 2 < 1 ? valLo : valHi;
                        }
                        WriteWyvernRegister( regName, progVal);
                    }
                }
            }

            WriteWyvernRegister( "FinalMuxInCfg", 0x1); //set final mux source from finalmux registers
        }

        private void writeAdj(string name, int val)
        {
            string[] parse = name.Split('.');
            string type = parse[0];
            string ab = parse[1];
            int i = int.Parse(parse[2]);
            string regName = "SwDrvOsAdj";
            if (type.StartsWith("SL")) regName += (i + 3);
            else if (type.StartsWith("M")) regName += (i + 13);
            else regName += (38 - i);
            string codeField = "Adj" + ab;
            string enpField = "AdjEn" + ab + "p";
            string ennField = "AdjEn" + ab + "n";

            //DUT.Refresh();
            WriteWyvernRegisterField( regName, codeField, Math.Abs(val));
            WriteWyvernRegisterField( regName, enpField, (val > 0 ? 1 : 0)); //reg[enpField] = val > 0 ? 1 : 0;
            WriteWyvernRegisterField( regName, ennField, (val < 0 ? 1 : 0)); //reg[ennField] = val < 0 ? 1 : 0;

        }

        private void measOffset(string name, int adj, Dictionary<Tuple<string, int>, double> dutyErr)
        {
            string[] parse = name.Split('.');
            string type = parse[0];
            string ab = parse[1];
            int i = int.Parse(parse[2]);

            midState = createMidState(name);
            setForceMux(false, name, 0);
            double vlo = measVolt(1);
            vlo = opt_sa ? -vlo : vlo;

            setForceMux(false, name, 1);
            double vhi = measVolt(1);
            double vtog = 0;
            if (!opt_sa)
            {
                setForceMux(false, name, 2);
                vtog = measVolt(1);
            }

            double vos = vtog - .5 * (vhi + vlo);
            Tuple<string, int> tup = new Tuple<string, int>(name, adj);
            double val = 100 * vos / (vhi - vlo);
            if (dutyErr.ContainsKey(tup))
                dutyErr[tup] = val;
            else
            {
                dutyErr.Add(tup, val);
            }
            if (!bestErr.ContainsKey(name)) bestErr.Add(name, double.MaxValue);
            if (Math.Abs(dutyErr[tup]) < Math.Abs(bestErr[name]))
            {
                bestErr[name] = dutyErr[tup];
                bestCode[name] = adj;
            }

            //DUT.Refresh();

            //adding this to make sure its in the right state before we measure
            WriteWyvernRegister( "FinalMuxInCfg", debugFinalMux);

            mPrintTestResultMethod(String.Format("Measure Offset for {0}: Code:{1} Vlo:{2:0.000000} Vhi:{3:0.000000} Vos:{4:0.000000} Duty Error: {5:0.000000}", name, adj, vlo, vhi, vos, val));
        }

       


        private void SetAClktoStaticNRZ()
        {
            //DDR mode is on by default in M9347A driver, but it still needs to be placed into DAC-A NRZ mode ACLK static 1 
            //write following registers
            //ClkaCfg AClkStatic 1      
            //ClkaCfg AClkMux1 1        
            //ClkaCfg AClkMux2 0       
            //ClkaCfg FbEnAClkDrive 0   
            //ClkaCfg FbEnAClkMux2 0    

            //DUT.Refresh();

            //AnalogClock configuration
            WriteWyvernRegisterField( "ClkaCfg", "AClkStatic", 0x1);
            WriteWyvernRegisterField( "ClkaCfg", "AClkMux1", 0x1);
            WriteWyvernRegisterField( "ClkaCfg", "AClkMux2", 0x0);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnAClkDrive", 0x0);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnAClkMux2", 0x0);
           
            WriteWyvernRegisterField( "ClkaCfg", "LClkMux", 0x0);
            WriteWyvernRegisterField( "ClkaCfg", "TestClkRxEn", 0x0);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnTestClkDelay1", 0x0);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnTestClkDelay2", 0x0);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnIClk2Drive", 0x0);
            WriteWyvernRegisterField( "ClkaCfg", "IClk2Off", 0x1);

            WriteWyvernRegisterField( "ClkaCfg", "FbEnGlobalDelay", 0x1);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnIClkDrive", 0x1);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnLClkDelay", 0x1);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnLClkDrive", 0x1);
            WriteWyvernRegisterField( "ClkaCfg", "FbEnLs", 0x1);

            WriteWyvernRegister( "CClkGate", 13309);
            WriteWyvernRegister( "DdrInterpCfg", 0x1);
            WriteWyvernRegister( "FinalMuxCfg", 33);
            WriteWyvernRegister( "DacCfg1", 15891);
            WriteWyvernRegister( "DemCfg", 3); 
            WriteWyvernRegister( "OutMarkCfg0", 16390);
            WriteWyvernRegister( "OutMarkCfg1", 16390);
            WriteWyvernRegister( "OutMarkCfg2", 16390);
            WriteWyvernRegister( "OutMarkCfg3", 16390);

        }

    }
}
