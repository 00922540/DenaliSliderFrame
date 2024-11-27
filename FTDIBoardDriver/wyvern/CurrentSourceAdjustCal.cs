using System;
using System.Collections.Generic;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Diagnostics;
using Keysight.ModularInstruments.Core.Register;

namespace Keysight.ModularInstruments
{
    // Current Source Adjust Calibration
    // Adjusts the tweak DAC of 72 current sources to get them as close to nominal weight as possible 
    // Current sources tweak DAC adjust range is +/- 3% of nominal.
    /*
     * Captain's Log
     * 
     * 10/3/2018
     * Major refactor. Update cal method to reccommended method and clean code for readability
     * Calibration has also been sped up from 30 minutes/module to 8 minutes/module
     * 
     * */
    public class CurrentSourceAdjustCal: WyvernCalBase
    {
        //now I also need to store the register group name blegh
        string[] WyvernMuxRegisters = { "FinalMuxInCfg", "CClkGate", "DdrInterpCfg", "FinalMuxCfg", "DacCfg1", "DemCfg", "ClkaCfg" };
        string DacCurAdjField = "DacCurAdj";

        double saMeasDiv = 8;   //trying to calibrate at 2.4 GHz instead of 1.2 to hit the balun sweet spot.
        // 8 = 2.4 GH, 16 = 1.2 GHz
        bool opt_sa = true;
        int TotalIterations = 3;//10;

        //debug ints
        int TotalRegisterWrites = 0;
        int TotalRegisterReads = 0;

        // cal ints
        int DacMaxValue = 1023;
        int DacMinValue = 0;

        private double mRmsMismatch = 0;

        Dictionary<string, long> adjRegValues = new Dictionary<string, long>();
        Dictionary<string, uint> saveRegs = new Dictionary<string, uint>();
        Dictionary<Tuple<char, char>, int[]> midState = new Dictionary<Tuple<char, char>, int[]>();  //creates a dictionary of type <[char, char],int[]>


        public CurrentSourceAdjustCal(string slugSerialNumber,
                                      string wyvernName,
                                      string calDataSavePath,
                                      WyvernRegisterSet regiserset,
                                      MeasureMarkerValueDelegation measureDelegation, PrintTestResultDelegation printTestResultDelegation)
            :base(slugSerialNumber,wyvernName,calDataSavePath,regiserset,measureDelegation, printTestResultDelegation)
        {

        }

        public double RmsMismatch
        {
            get { return mRmsMismatch; }
        }

        public override void Execute()
        {

            SetAttenuatorsForCal();

            #region Calibrate
            // 1. Measure initial weights at miscale adjust (512)
            int MidscaleValue = 512;
            foreach (string SwitchingElement in AllSwitchingElements)
                WriteAdj(SwitchingElement, MidscaleValue);

            var NewBitWeights = MeasureBitWeights();
            AnalyzeBitWeights(NewBitWeights, 0);

            // 2. Compute adjust gains in LSB's per code
            Dictionary<string, double> AdjGainsPerSwitch = new Dictionary<string, double>();
            foreach (string SwEl in AllSwitchingElements)
                AdjGainsPerSwitch[SwEl] = -127e-6 * NominalBitWeights[SwEl];    //-127PPM/Code

            // 3. iteratively adjust trim DAC's
            int Iteration = 0;

            //Debug.WriteLine(String.Format("{0,10} {1,10:0.000} {2,10:0.00 } {3, 10}", "Bit", "new error", "bit weight", "Ideal Bit Weight"));
            mPrintTestResultMethod(String.Format("{0,10} {1,10:0.000} {2,10:0.00 } {3, 10}", "Bit", "new error", "bit weight", "Ideal Bit Weight"));
            while (Iteration < TotalIterations)
            {
                Iteration++;

                // debug log
                //Debug.WriteLine(String.Format("Iteration {0} Start Time:{1}", Iteration, DateTime.Now));
                mPrintTestResultMethod(String.Format("Iteration {0} Start Time:{1}", Iteration, DateTime.Now));
                foreach (string SwitchingElement in AllSwitchingElements)
                {
                    double NewError = NewBitWeights[SwitchingElement] - NominalBitWeights[SwitchingElement];

                    //Debug.WriteLine(String.Format("{0,10} {1,10:0.000} {2,10:0.00 } {3,10}", SwitchingElement, NewError, NewBitWeights[SwitchingElement], NominalBitWeights[SwitchingElement]));
                    mPrintTestResultMethod(String.Format("{0,10} {1,10:0.000} {2,10:0.00 } {3,10}", SwitchingElement, NewError, NewBitWeights[SwitchingElement], NominalBitWeights[SwitchingElement]));
                    if (Math.Abs(NewError) > 0.25)
                    {
                        int OldDacCode = ReadAdj(SwitchingElement);
                        int NewDacCode = (int)Math.Round(OldDacCode - (NewError / AdjGainsPerSwitch[SwitchingElement]));
                        NewDacCode = BoundDatum(NewDacCode, DacMinValue, DacMaxValue);

                        WriteAdj(SwitchingElement, NewDacCode);

                        //Debug.WriteLine(String.Format("Adjusting {0} Old Code: {1} New Code: {2}", SwitchingElement, OldDacCode, NewDacCode));
                        mPrintTestResultMethod(String.Format("Adjusting {0} Old Code: {1} New Code: {2}", SwitchingElement, OldDacCode, NewDacCode));
                    }
                }

                NewBitWeights = MeasureBitWeights();
                mRmsMismatch = AnalyzeBitWeights(NewBitWeights, Iteration);

                // debug log 
                //Debug.WriteLine(String.Format("Iteration {0} End Time:{1}", Iteration, DateTime.Now));
                mPrintTestResultMethod(String.Format("Iteration {0} End Time:{1}", Iteration, DateTime.Now));

            }
            #endregion

            //WriteCalValueToLogFile();

            //SaveCalDataToModule();

        }
        

        private int BoundDatum(int Datum, int MinValue, int MaxValue)
        {
            Datum = Math.Min(Datum, MaxValue);
            Datum = Math.Max(Datum, MinValue);
            return Datum;
        }


        private void SetAttenuatorsForCal()
        {
            WriteWyvernRegister("Scale", 7360);

        }


        private void LogWyvernRegisters(string[] RegisterNames)
        {
            uint RegisterValue;

            foreach (string Register in RegisterNames)
            {
                RegisterValue = (uint)ReadWyvernRegister(Register);
                saveRegs[Register] = RegisterValue;
                //Debug.WriteLine(String.Format("{0} : {1}", Register, RegisterValue.ToString("X8")));
                mPrintTestResultMethod(String.Format("{0} : {1}", Register, RegisterValue.ToString("X8")));
            }
        }
        
        private long discardRedundantBits(long code)
        {
            return (code & ((1 << 5) - 1)) + ((code >> 6) << 5);
        }

        private long insertRedundantBit(long code)
        {
            return (code & ((1 << 5) - 1)) + ((code >> 5) << 6);
        }

        public void WriteCalValueToLogFile(int VtermValue)
        {
            // writing to network folder until we integrate this test into module level testing.
            string[] DacsAB = { "A", "B" };
            string[] PrimOrSec = { "P", "S" };
            string CurrentCsaRegister;
            int RegRead;
            var RegistersAndValues = new Dictionary<string, int>();

            string pathDir = System.IO.Path.GetDirectoryName(mSavePath);

            string logfile = System.IO.Path.Combine(pathDir, String.Format("WyvernCalData_{0}_{1}.txt", mSlugSerialNumber, whichWyvern));
            System.IO.StreamWriter file;
            using (file = new System.IO.StreamWriter(logfile, false))
            {
                file.WriteLine("WyvernVtermDAC,{0:X}", VtermValue);
                foreach (string dacsAB in DacsAB)
                {

                    for (int i = 0; i < 16; i++)
                    {
                        CurrentCsaRegister = "DacCurAdjMsb" + dacsAB + i.ToString();
                        RegRead = (int)ReadWyvernRegisterField(CurrentCsaRegister, "DacCurAdj");
                        file.WriteLine(CurrentCsaRegister + "," + "{0:X}", (int)RegRead);
                    }
                }

                foreach (string dacsAB in DacsAB)
                {
                    foreach (string ps in PrimOrSec)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            CurrentCsaRegister = "DacCurAdj" + ps + "Lsb" + dacsAB + j.ToString();
                            RegRead = (int)ReadWyvernRegisterField(CurrentCsaRegister, "DacCurAdj");
                            file.WriteLine(CurrentCsaRegister + "," + "{0:X}", (int)RegRead);
                        }
                    }

                }




            }
        }

        public int GetVtermValueFormCalData()
        {
            string pathDir = System.IO.Path.GetDirectoryName(mSavePath);
            string logfile = System.IO.Path.Combine(pathDir, String.Format("WyvernCalData_{0}_{1}.txt", mSlugSerialNumber, whichWyvern));
            System.IO.StreamReader file;
            file = new System.IO.StreamReader(logfile, false);
            string sData = file.ReadLine();
            if (sData.Split(',')[0] != "WyvernVtermDAC")
                return 0;
            return Convert.ToInt32(sData.Split(',')[1], 16);
        }
        public int GetVtermValueFormCalData(string calFile)
        {
            System.IO.StreamReader file;
            file = new System.IO.StreamReader(calFile, false);
            string sData = file.ReadLine();
            if (sData.Split(',')[0] != "WyvernVtermDAC")
                return 0;
            return Convert.ToInt32(sData.Split(',')[1], 16);
        }
        public void SaveCalDataToModule()
        {
            string pathDir = System.IO.Path.GetDirectoryName(mSavePath);
            string logfile = System.IO.Path.Combine(pathDir, String.Format("WyvernCalData_{0}_{1}.txt", mSlugSerialNumber, whichWyvern));
            System.IO.StreamReader file;
            string sRegName, sReagValue;
            file = new System.IO.StreamReader(logfile, false);
            string sData = file.ReadLine();
            sData = file.ReadLine();
            while (sData != null)
            {
                sRegName = sData.Split(',')[0];
                sReagValue = sData.Split(',')[1];
                WriteWyvernRegisterField(sRegName, "DacCurAdj", Convert.ToInt32(sReagValue, 16));
                sData = file.ReadLine();
            }
        }
        public void SaveCalDataToModule(string calFile)
        {
            System.IO.StreamReader file;
            string sRegName, sReagValue;
            file = new System.IO.StreamReader(calFile, false);
            string sData = file.ReadLine();
            sData = file.ReadLine();
            while (sData != null)
            {
                sRegName = sData.Split(',')[0];
                sReagValue = sData.Split(',')[1];
                WriteWyvernRegisterField(sRegName, "DacCurAdj", Convert.ToInt32(sReagValue, 16));
                sData = file.ReadLine();
            }
        }

        /// <summary>
        /// Measure bitweights of all 52 switching elements.
        /// Returns a data structure mapping switching element to weight measured in LSBs.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, double> MeasureBitWeights()
        {
            var MeasuredBitWeightLSBs = new Dictionary<string, double>();
            var MeasuredVolts = new Dictionary<string, double>();

            SetDDRMode();

            double Vfs = 0;

            //Debug.WriteLine("Measuring BitWeights....");
            mPrintTestResultMethod("Measuring BitWeights....");
            foreach (char AB in new char[] { 'A', 'B' })
            {
                Vfs = 0;
                foreach (string SwEl in SwitchingElements[AB])
                {
                    double VLow = 0;
                    double VHigh = 0;

                    SetPatternInForceMux(SwEl, 1);
                    VHigh = measVolt(1);
                    MeasuredVolts[SwEl] = (VHigh - VLow) * 4.1047;
                    Vfs = Vfs + MeasuredVolts[SwEl];

                    // debug log
                    Debug.WriteLine(String.Format("{0,20} Vhigh:{1,20} Measured Volts: {2,10} ", SwEl, Math.Round(VHigh, 7), MeasuredVolts[SwEl]));
                    mPrintTestResultMethod(String.Format("{0,20} Vhigh:{1,20} Measured Volts: {2,10} ", SwEl, Math.Round(VHigh, 7), MeasuredVolts[SwEl]));
                }

                double Vlsb = Vfs / 18430;

                foreach (string SwEl in SwitchingElements[AB])
                {
                    MeasuredBitWeightLSBs[SwEl] = MeasuredVolts[SwEl] / Vlsb;
                }
            }

            return MeasuredBitWeightLSBs;
        }

        /// <summary>
        /// Calculate and print RMS mismatch and total mistmatch
        /// </summary>
        /// <param name="Bw"></param>
        /// <param name="Iteration"></param>
        private double AnalyzeBitWeights(Dictionary<string, double> Bw, int Iter)
        {
            double RmsMismatch = 0;
            double TotalMismatch = 0;
            double Error = 0;

            string Iteration = Iter == -1 ? "Midscale Start" : Iter.ToString();

            // no dots and ganged 6-4, 3-0
            // calculate bit error per switching element, and total mismatch
            foreach (string SwitchingElement in NominalBitWeights.Keys)
            {
                Error = Bw[SwitchingElement] - NominalBitWeights[SwitchingElement];
                RmsMismatch = RmsMismatch + (Error * Error);
                TotalMismatch = TotalMismatch + Math.Abs(Error);
            }

            RmsMismatch = Math.Sqrt(RmsMismatch / 52);
            Debug.WriteLine(String.Format("{0,6} {1,10:0.000} {2,10:0.00 }",
                        Iteration, RmsMismatch, TotalMismatch));
            mPrintTestResultMethod(String.Format("{0,6} {1,10:0.000} {2,10:0.00 }",
                        Iteration, RmsMismatch, TotalMismatch));
            return RmsMismatch;
        }

        public void SetPatternInForceMux(string sw, int value)
        {
            Dictionary<string, int> Default_MPS = new Dictionary<string, int>();
            string Mps = CalSwitchElementMap[sw].MPS;
            string dacAorB = CalSwitchElementMap[sw].DAC;
            int SwitchIndex = CalSwitchElementMap[sw].N;
            int Default_M = 0;
            int Default_P = 0;
            int Default_S = 0;
            int ActiveLsb = 0;
            int OtherLsb = 0;

            // 2. Determine default register states from Force Mux Programming table
            if (Mps == "M")
            {
                Default_M = 0;
                Default_P = 0;
                Default_S = 0x3FF;

                for (int x = 0; x < 8; x = x + 1)
                {
                    Default_M = Default_M + (1 << (x + SwitchIndex + 8) % 16);
                }
            }
            else
            {
                Default_M = 0x00FF;
                if (SwitchIndex == 8)
                {
                    ActiveLsb = 0x080;
                    OtherLsb = 0x27E;   // discrepancy between force mux table and pseudo-code 0x27F
                }
                else
                {
                    ActiveLsb = 0x100;
                    OtherLsb = 0x2FF;
                }

                Default_P = Mps == "P" ? ActiveLsb : OtherLsb;
                Default_S = Mps == "S" ? ActiveLsb : OtherLsb;
            }

            // store in dictionary for easy readability
            Default_MPS["M"] = Default_M;
            Default_MPS["P"] = Default_P;
            Default_MPS["S"] = Default_S;

            // 3. Force midscale while programming FinalMuxIn registers to avoid overvoltage trips
            WriteWyvernRegister("FinalMuxInCfg", 0x0);

            foreach (string DacLoop in new string[] { "A", "B" })
            {
                int ValHi = 0;
                int ValLo = 0;
                int IStart = DacLoop == "A" ? 0 : 32;
                int IStop = IStart + 31;

                foreach (string MpsLoop in new string[] { "M", "P", "S" })
                {
                    // optimization for less register writing
                    //if (MpsLoop != Mps)
                    //    if ((Mps == "M" && SwitchIndex > 0) || (Mps != "M" && SwitchIndex > 3))
                    //        continue;
                    //if (dacAorB != DacLoop)
                    //    if (Mps != "M")
                    //        continue;

                    ValHi = 0;
                    ValLo = Default_MPS[MpsLoop];

                    if (dacAorB == DacLoop && Mps == MpsLoop)
                    {
                        // force specified bits high with bitwise OR
                        if (Mps != "M" && SwitchIndex == 6)
                            ValHi = Default_MPS[MpsLoop] | 0x0070;
                        else if (Mps != "M" & SwitchIndex == 3)
                            ValHi = Default_MPS[MpsLoop] | 0x000F;
                        else
                            ValHi = Default_MPS[MpsLoop] | (1 << SwitchIndex);
                    }
                    else
                    {
                        ValHi = Default_MPS[MpsLoop];
                    }

                    for (int i = IStart; i <= IStop; i++)
                    {
                        if (value == 0)
                            WriteFinalMux(MpsLoop, i, ValLo);
                        else if (value == 1)
                            WriteFinalMux(MpsLoop, i, ((i % saMeasDiv) < (saMeasDiv / 2) ? ValLo : ValHi));
                    }

                }
            }

            // 5. output form FinalMuxIn registers
            WriteWyvernRegisterField("FinalMuxInCfg", "Source", 0x1);

        }

        public void WriteFinalMux(string Mps, int N, int Value)
        {
            string RegName = "FinalMuxIn" + Mps + N;
            string fieldName = Mps == "M" ? "" : Mps + "Lsb";
            WriteWyvernRegister(RegName, Value);
            TotalRegisterWrites++;
        }

        public void WriteAdj(string SwitchingElement, int code)
        {
            int adjCode = (int)insertRedundantBit(code);
            string RegName = CalSwitchElementMap[SwitchingElement].RegisterName;

            WriteWyvernRegisterField(RegName, DacCurAdjField, adjCode);
            TotalRegisterWrites++;
        }

        public int ReadAdj(string SwitchingElement)
        {
            string RegName = CalSwitchElementMap[SwitchingElement].RegisterName;
            int code = (int)ReadWyvernRegisterField(RegName, DacCurAdjField);

            // debug stuff
            TotalRegisterReads++;

            return (int)discardRedundantBits(code);
        }

        #region Calbration register mapping
        private static SortedDictionary<string, double> NominalBitWeights = new SortedDictionary<string, double>
        {
            // A datapath
            {"MsbA15", 1024 },
            {"MsbA14", 1024 },
            {"MsbA13", 1024 },
            {"MsbA12", 1024 },
            {"MsbA11", 1024 },
            {"MsbA10", 1024 },
            {"MsbA9", 1024 },
            {"MsbA8", 1024 },
            {"MsbA7", 1024 },
            {"MsbA6", 1024 },
            {"MsbA5", 1024 },
            {"MsbA4", 1024 },
            {"MsbA3", 1024 },
            {"MsbA2", 1024 },
            {"MsbA1", 1024 },
            {"MsbA0", 1024 },
            {"PLsbA9", 512 },
            {"PLsbA8", 256 },
            {"PLsbA7", 128 },
            {"PLsbA6-4", 112 },
            {"PLsbA3-0", 15 },
            {"SLsbA9", 512 },
            {"SLsbA8", 256 },
            {"SLsbA7", 128 },
            {"SLsbA6-4", 112 },
            {"SLsbA3-0", 15 },
            // B Datapath
            {"MsbB15", 1024 },
            {"MsbB14", 1024 },
            {"MsbB13", 1024 },
            {"MsbB12", 1024 },
            {"MsbB11", 1024 },
            {"MsbB10", 1024 },
            {"MsbB9", 1024 },
            {"MsbB8", 1024 },
            {"MsbB7", 1024 },
            {"MsbB6", 1024 },
            {"MsbB5", 1024 },
            {"MsbB4", 1024 },
            {"MsbB3", 1024 },
            {"MsbB2", 1024 },
            {"MsbB1", 1024 },
            {"MsbB0", 1024 },
            {"PLsbB9", 512 },
            {"PLsbB8", 256 },
            {"PLsbB7", 128 },
            {"PLsbB6-4", 112 },
            {"PLsbB3-0", 15 },
            {"SLsbB9", 512 },
            {"SLsbB8", 256 },
            {"SLsbB7", 128 },
            {"SLsbB6-4", 112 },
            {"SLsbB3-0", 15 }
        };

        private static Dictionary<char, string[]> SwitchingElements = new Dictionary<char, string[]>
        {
            { 'A', new string[] {"MsbA15", "MsbA14", "MsbA13", "MsbA12", "MsbA11", "MsbA10", "MsbA9","MsbA8", "MsbA7", "MsbA6", "MsbA5", "MsbA4","MsbA3", "MsbA2", "MsbA1", "MsbA0",
                                 "PLsbA9", "PLsbA8", "PLsbA7", "PLsbA6-4", "PLsbA3-0",
                                 "SLsbA9", "SLsbA8", "SLsbA7", "SLsbA6-4", "SLsbA3-0",} },
            { 'B', new string[] {"MsbB15", "MsbB14", "MsbB13", "MsbB12", "MsbB11", "MsbB10", "MsbB9","MsbB8", "MsbB7", "MsbB6", "MsbB5", "MsbB4","MsbB3", "MsbB2", "MsbB1", "MsbB0",
                                 "PLsbB9", "PLsbB8", "PLsbB7", "PLsbB6-4", "PLsbB3-0",
                                 "SLsbB9", "SLsbB8", "SLsbB7", "SLsbB6-4", "SLsbB3-0",} }
        };

        public Dictionary<string, CalSwitchElement> CalSwitchElementMap = new Dictionary<string, CalSwitchElement>
        {
            {"MsbA15", new CalSwitchElement { RegisterName = "DacCurAdjMsbA15", DAC = "A", MPS = "M", N = 15} },
            {"MsbA14", new CalSwitchElement { RegisterName = "DacCurAdjMsbA14", DAC = "A", MPS = "M", N = 14} },
            {"MsbA13", new CalSwitchElement { RegisterName = "DacCurAdjMsbA13", DAC = "A", MPS = "M", N = 13} },
            {"MsbA12", new CalSwitchElement { RegisterName = "DacCurAdjMsbA12", DAC = "A", MPS = "M", N = 12} },
            {"MsbA11", new CalSwitchElement { RegisterName = "DacCurAdjMsbA11", DAC = "A", MPS = "M", N = 11} },
            {"MsbA10", new CalSwitchElement { RegisterName = "DacCurAdjMsbA10", DAC = "A", MPS = "M", N = 10} },
            {"MsbA9", new CalSwitchElement { RegisterName = "DacCurAdjMsbA9", DAC = "A", MPS = "M", N = 9} },
            {"MsbA8", new CalSwitchElement { RegisterName = "DacCurAdjMsbA8", DAC = "A", MPS = "M", N = 8} },
            {"MsbA7", new CalSwitchElement { RegisterName = "DacCurAdjMsbA7", DAC = "A", MPS = "M", N = 7} },
            {"MsbA6", new CalSwitchElement { RegisterName = "DacCurAdjMsbA6", DAC = "A", MPS = "M", N = 6} },
            {"MsbA5", new CalSwitchElement { RegisterName = "DacCurAdjMsbA5", DAC = "A", MPS = "M", N = 5} },
            {"MsbA4", new CalSwitchElement { RegisterName = "DacCurAdjMsbA4", DAC = "A", MPS = "M", N = 4} },
            {"MsbA3", new CalSwitchElement { RegisterName = "DacCurAdjMsbA3", DAC = "A", MPS = "M", N = 3} },
            {"MsbA2", new CalSwitchElement { RegisterName = "DacCurAdjMsbA2", DAC = "A", MPS = "M", N = 2} },
            {"MsbA1", new CalSwitchElement { RegisterName = "DacCurAdjMsbA1", DAC = "A", MPS = "M", N = 1} },
            {"MsbA0", new CalSwitchElement { RegisterName = "DacCurAdjMsbA0", DAC = "A", MPS = "M", N = 0} },
            {"PLsbA9", new CalSwitchElement { RegisterName = "DacCurAdjPLsbA4", DAC = "A", MPS = "P", N = 9} },
            {"PLsbA8", new CalSwitchElement { RegisterName = "DacCurAdjPLsbA3", DAC = "A", MPS = "P", N = 8} },
            {"PLsbA7", new CalSwitchElement { RegisterName = "DacCurAdjPLsbA2", DAC = "A", MPS = "P", N = 7} },
            {"PLsbA6-4", new CalSwitchElement { RegisterName = "DacCurAdjPLsbA1", DAC = "A", MPS = "P", N = 6} },
            {"PLsbA3-0", new CalSwitchElement { RegisterName = "DacCurAdjPLsbA0", DAC = "A", MPS = "P", N = 3} },
            {"SLsbA9", new CalSwitchElement { RegisterName = "DacCurAdjSLsbA4", DAC = "A", MPS = "S", N = 9} },
            {"SLsbA8", new CalSwitchElement { RegisterName = "DacCurAdjSLsbA3", DAC = "A", MPS = "S", N = 8} },
            {"SLsbA7", new CalSwitchElement { RegisterName = "DacCurAdjSLsbA2", DAC = "A", MPS = "S", N = 7} },
            {"SLsbA6-4", new CalSwitchElement { RegisterName = "DacCurAdjSLsbA1", DAC = "A", MPS = "S", N = 6} },
            {"SLsbA3-0", new CalSwitchElement { RegisterName = "DacCurAdjSLsbA0", DAC = "A", MPS = "S", N = 3} },
            // b DAC
            {"MsbB15", new CalSwitchElement { RegisterName = "DacCurAdjMsbB15", DAC = "B", MPS = "M", N = 15} },
            {"MsbB14", new CalSwitchElement { RegisterName = "DacCurAdjMsbB14", DAC = "B", MPS = "M", N = 14} },
            {"MsbB13", new CalSwitchElement { RegisterName = "DacCurAdjMsbB13", DAC = "B", MPS = "M", N = 13} },
            {"MsbB12", new CalSwitchElement { RegisterName = "DacCurAdjMsbB12", DAC = "B", MPS = "M", N = 12} },
            {"MsbB11", new CalSwitchElement { RegisterName = "DacCurAdjMsbB11", DAC = "B", MPS = "M", N = 11} },
            {"MsbB10", new CalSwitchElement { RegisterName = "DacCurAdjMsbB10", DAC = "B", MPS = "M", N = 10} },
            {"MsbB9", new CalSwitchElement { RegisterName = "DacCurAdjMsbB9", DAC = "B", MPS = "M", N = 9} },
            {"MsbB8", new CalSwitchElement { RegisterName = "DacCurAdjMsbB8", DAC = "B", MPS = "M", N = 8} },
            {"MsbB7", new CalSwitchElement { RegisterName = "DacCurAdjMsbB7", DAC = "B", MPS = "M", N = 7} },
            {"MsbB6", new CalSwitchElement { RegisterName = "DacCurAdjMsbB6", DAC = "B", MPS = "M", N = 6} },
            {"MsbB5", new CalSwitchElement { RegisterName = "DacCurAdjMsbB5", DAC = "B", MPS = "M", N = 5} },
            {"MsbB4", new CalSwitchElement { RegisterName = "DacCurAdjMsbB4", DAC = "B", MPS = "M", N = 4} },
            {"MsbB3", new CalSwitchElement { RegisterName = "DacCurAdjMsbB3", DAC = "B", MPS = "M", N = 3} },
            {"MsbB2", new CalSwitchElement { RegisterName = "DacCurAdjMsbB2", DAC = "B", MPS = "M", N = 2} },
            {"MsbB1", new CalSwitchElement { RegisterName = "DacCurAdjMsbB1", DAC = "B", MPS = "M", N = 1} },
            {"MsbB0", new CalSwitchElement { RegisterName = "DacCurAdjMsbB0", DAC = "B", MPS = "M", N = 0} },
            {"PLsbB9", new CalSwitchElement { RegisterName = "DacCurAdjPLsbB4", DAC = "B", MPS = "P", N = 9} },
            {"PLsbB8", new CalSwitchElement { RegisterName = "DacCurAdjPLsbB3", DAC = "B", MPS = "P", N = 8} },
            {"PLsbB7", new CalSwitchElement { RegisterName = "DacCurAdjPLsbB2", DAC = "B", MPS = "P", N = 7} },
            {"PLsbB6-4", new CalSwitchElement { RegisterName = "DacCurAdjPLsbB1", DAC = "B", MPS = "P", N = 6} },
            {"PLsbB3-0", new CalSwitchElement { RegisterName = "DacCurAdjPLsbB0", DAC = "B", MPS = "P", N = 3} },
            {"SLsbB9", new CalSwitchElement { RegisterName = "DacCurAdjSLsbB4", DAC = "B", MPS = "S", N = 9} },
            {"SLsbB8", new CalSwitchElement { RegisterName = "DacCurAdjSLsbB3", DAC = "B", MPS = "S", N = 8} },
            {"SLsbB7", new CalSwitchElement { RegisterName = "DacCurAdjSLsbB2", DAC = "B", MPS = "S", N = 7} },
            {"SLsbB6-4", new CalSwitchElement { RegisterName = "DacCurAdjSLsbB1", DAC = "B", MPS = "S", N = 6} },
            {"SLsbB3-0", new CalSwitchElement { RegisterName = "DacCurAdjSLsbB0", DAC = "B", MPS = "S", N = 3} },

        };

        static string[] AllSwitchingElements = new string[]
        {   "MsbA15",
            "MsbB15",
            "MsbA14",
            "MsbB14",
            "MsbA13",
            "MsbB13",
            "MsbA12",
            "MsbB12",
            "MsbA11",
            "MsbB11",
            "MsbA10",
            "MsbB10",
            "MsbA9",
            "MsbB9",
            "MsbA8",
            "MsbB8",
            "MsbA7",
            "MsbB7",
            "MsbA6",
            "MsbB6",
            "MsbA5",
            "MsbB5",
            "MsbA4",
            "MsbB4",
            "MsbA3",
            "MsbB3",
            "MsbA2",
            "MsbB2",
            "MsbA1",
            "MsbB1",
            "MsbA0",
            "MsbB0",
            "PLsbA9",
            "PLsbB9",
            "PLsbA8",
            "PLsbB8",
            "PLsbA7",
            "PLsbB7",
            "PLsbA6-4",
            "PLsbB6-4",
            "PLsbA3-0",
            "PLsbB3-0",
            "SLsbA9",
            "SLsbB9",
            "SLsbA8",
            "SLsbB8",
            "SLsbA7",
            "SLsbB7",
            "SLsbA6-4",
            "SLsbB6-4",
            "SLsbA3-0",
            "SLsbB3-0",
        };

        public class CalSwitchElement
        {
            public string RegisterName;
            public string DAC;
            public string MPS;
            public int N;
        }

        #endregion
    }

}