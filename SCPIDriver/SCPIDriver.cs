using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Agilent.TMFramework.InstrumentIO;
using Agilent.TMFramework.InstrumentIO.Configuration;
using SliderDriver;
using System.Windows.Forms;
using System.Reflection;
namespace ModuleDriver
{
    public class SCPIDriver:IModule
    {
        public List<string> LatchNames = new List<string>();
        public bool Status;
        public DirectIO IO=null;
        #region Board Instance
        public CCarrierBoard CarrierBoard = new CCarrierBoard();
        public CTxBoard TxBoard = new CTxBoard();
        #endregion 
        public SCPIDriver()
        {
            AddDriverToBoard();
        }
        public bool Connect(string Addr, bool Locked, int TimeOut)
        {
            try
            {
                IO = new DirectIO(Addr, Locked, TimeOut);
                IO.WriteLine("*IDN?");
                IO.Read();
                //IO.WriteLine("*RST");
                IO.Timeout = TimeOut;
            }
            catch
            {
                Status = false;
                return false;
            }
            Status = true;
            return true;
            
        }
        public UInt64 ReadLatch(string mlatch)
        {
            if (IO != null)
            {
                try
                {
                    UInt64 mvalue = 0;
                    string sCommand = string.Format("SERV:LATC:SEL '{0}'", mlatch);
                    IO.WriteLine(sCommand);
                    IO.WriteLine("SERV:LATC:VAL?");
                    string readvalue = IO.Read();
                    return mvalue = Convert.ToUInt64(readvalue);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(mlatch + ":\n\n" + ex.ToString());
                }

            }
            return 0;
        }
        public double GetValue(string cmd)
        {
            double rs=0;
            if (IO != null)
            {
                try
                {
                    IO.WriteLine(string.Format("SERV:ABUS:FEED \"GetValue:EXTERNALRF;{0}\"", cmd));
                    IO.WriteLine(" SERV:ABUS:READ?");
                    string readvalue = IO.Read();
                    rs = Convert.ToDouble(readvalue);
                    return rs;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(cmd + ":\n\n" + ex.ToString());

                }
            }
            return rs;
                
        }
        public void SetValue(string cmd, string value)
        {
            if (IO != null)
            {
                try
                {
                    IO.WriteLine(string.Format("SERV:ABUS:FEED \"SetValue:EXTERNALRF;{0},{1}\"", cmd,value));
                    IO.WriteLine(" SERV:ABUS:READ?");
                    //string readvalue = IO.Read();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(cmd + ":\n\n" + ex.ToString());

                }
            }

        }
        public double GetWyvernVterm(int wyvernNum)
        {
            string cmd;
            if (IO != null)
            {
                try
                {
                    double rs;
                    if (1 == wyvernNum)
                    {
                        cmd = "GetValue:EXTERNALRF;CarrierBoard;Ltc2602SourceDac";
                    }
                    else
                    {
                        cmd = "GetValue:EXTERNALRF;CarrierBoard;Ltc2602ReceiverDac";
                    }
                    IO.WriteLine(string.Format("SERV:ABUS:FEED '{0}'", cmd));
                    IO.WriteLine(" SERV:ABUS:READ?");
                    string readvalue = IO.Read();
                    return  Convert.ToDouble(readvalue);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("wyvern"+wyvernNum + ":\n\n" + ex.ToString());
                }

            }
            return 0;
        }
        public void WriteLatch(string GroupName,string IVIName, UInt64 mvalue)
        {
            string mlatch = string.Format("Source ExternalRf:{0}-{1}", GroupName, IVIName);
            if (IO != null)
            {
                IO.WriteLine("INIT:CONT OFF");
                IO.WriteLine("*OPC?");
                Read();
                string sCommand = string.Format("SERV:LATC:SEL '{0}'", mlatch);
                IO.WriteLine(sCommand);
                IO.WriteLine("SERV:LATC:LOCK OFF");
                sCommand = string.Format("SERV:LATC:VAL {0}", mvalue);
                IO.WriteLine(sCommand);
                if (!LatchNames.Contains(mlatch))
                {
                    LatchNames.Add(mlatch);
                }
                IO.WriteLine("SERV:LATC:LOCK ON");
                //IO.WriteLine("INIT:CONT ON");
                IO.WriteLine("*OPC?");
                Read();
            }

        }
        public void WriteLatch(string mlatch, UInt64 mvalue)
        {
            if(IO!=null)
            {
                IO.WriteLine("INIT:CONT OFF");
                IO.WriteLine("*OPC?");
                Read();
                string sCommand = string.Format("SERV:LATC:SEL '{0}'", mlatch);
                IO.WriteLine(sCommand);
                IO.WriteLine("SERV:LATC:LOCK OFF");
                sCommand = string.Format("SERV:LATC:VAL {0}", mvalue);
                IO.WriteLine(sCommand);
                if (!LatchNames.Contains(mlatch))
                {
                    LatchNames.Add(mlatch);
                }             
                IO.WriteLine("SERV:LATC:LOCK ON");
                //IO.WriteLine("INIT:CONT ON");
                IO.WriteLine("*OPC?");
                Read();
            }

        }
        public void UnlockAllLatch()
        {
            foreach(string latch in LatchNames)
            {
                string sCommand = string.Format("SERV:LATC:SEL '{0}'", latch);
                IO.WriteLine(sCommand);
                IO.WriteLine("SERV:LATC:LOCK OFF");
            }
            LatchNames.Clear();
        }
        public void WriteLine(string cmd)
        {
            IO.WriteLine(cmd);
        }
        public string Read()
        {
            return IO.Read();
        }
        public long setBits(long hexCode, long bitsSet, int bitsSetLength, int bitSetPosition, int totalBitLength)
        {
            int tempCodeA = (int)Math.Pow(2, totalBitLength - bitSetPosition) - 1;
            tempCodeA = tempCodeA << bitSetPosition;
            int tempCodeB = (int)Math.Pow(2, bitSetPosition - bitsSetLength) - 1;
            int zeroCode = tempCodeA | tempCodeB;
            hexCode = hexCode & zeroCode;
            bitsSet = bitsSet << (bitSetPosition - bitsSetLength);
            hexCode = hexCode | bitsSet;
            return hexCode;
        }
        public long getBits(long hexCode, int bitsSetLength, int bitSetPosition, int totalBitLength)
        {
            string sData = Convert.ToString(hexCode, 2).PadLeft(totalBitLength, '0');
            return Convert.ToInt32(sData.Substring(sData.Length - bitSetPosition - 1, bitsSetLength), 2);
        }
        public void Close()
        {
            ;
        }
        public void AddDriverToBoard()
        {
            foreach (FieldInfo DriverField in this.GetType().GetFields())
            {

                if (DriverField.Name.Contains("Board"))
                {
                    if (IsBoard(DriverField.GetValue(this)))
                    {
                        dynamic Board = DriverField.GetValue(this);
                        Board.GetType().GetField("Driver").SetValue(Board, this);
                        Board.GetType().GetField("BoardName").SetValue(Board, DriverField.Name);
                        //SetDriverToEquipment(Board);
                    }
                }
            }
        }

        public bool IsBoard(object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.Name == "IsBoardDriverMark")
                    return true;
                else
                    continue;
                //if (field.GetValue(obj) != null)
                //{
                //    object obj2 = field.GetValue(obj);
                //    if (obj2.GetType().Name == "CEquipment")
                //    {
                //        return true;
                //    }
                //}
                //else
                //{
                //    continue;
                //}

            }
            return false;
        }
    }
}
