using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
namespace BoardDriver
{
    public class CAdapterBoard : CBoardBase
    {
        public int SPI = 4;
        #region Equipment Definetion
        public CEquipment AdapterLoopBackPath_DAC_CS = new CEquipment(0x27, 24, 8);
        public CEquipment AdapterDACOutPutPath_CS_Q_LMH6401 = new CEquipment(0x26, 24, 8);
        public CEquipment AdapterADCInputPath_CS_Q_LMH6401 = new CEquipment(0x24, 24, 8);
        public CEquipment AdapterloopBackPath_CS_I_LMH6401 = new CEquipment(0x6, 8, 8);
        public CEquipment AdapterloopBackPath_CS_Q_LMH6401 = new CEquipment(0x7, 8, 8);
        public CEquipment AdapterDACOutPutPath_CS_I_LMH6401 = new CEquipment(0x25, 24, 8);

        public CEquipment AdapterDAC_CS = new CEquipment(0x28, 24, 8);
        public CEquipment AdapterADCInputPath_CS_I_LMH6401 = new CEquipment(0x23, 24, 8);
        public CEquipment Adapter_Control = new CEquipment(0x0C, 32, 8);
        public CEquipment Adapter74LVC595 = new CEquipment(0x08, 32, 8);
        public CEquipment Debug_Control = new CEquipment(0x01, 32, 8);
        public CEquipment REC_Control = new CEquipment(0x0C, 32, 8);
        public CEquipment SLUG_CTRL = new CEquipment(0x07, 32, 8);
        #endregion


        #region ABUS Definetion
        public List<CAbus> AbusList = new List<CAbus>(){
            new CAbus("P3.5VA", "00001", 0.176) ,
            new CAbus("N3.3VA", "00011", 0.25) ,
            new CAbus("P5.4VA", "00101", 0.3125) ,
            new CAbus("N6.0VA", "00111", 0.25) ,
            new CAbus("P3.3VD", "01001", 1) ,
            new CAbus("P2.5VD", "01011", 1) ,
            new CAbus("P8.5VA", "01101", 1) ,
            new CAbus("N3.0VA", "01111", 1) ,
            new CAbus("P3.3VA", "10001", 1) ,
            new CAbus("P7.5VA", "10011",0.17603) ,
            new CAbus("N2.5VA", "10101", 0.33333) ,
            new CAbus("P6.0VA", "10111", 1) ,
            new CAbus("Temperature 2", "11001", 1) ,
            new CAbus("Temperature 1", "11011", 1)
        };
        #endregion
        #region Register Function
        private void InitializeDAC()
        {

        }
        public void WriteAdapterDac(int dac, int dacChannel, string dacValue, bool one)
        {
            string addr = "0010";
            int DacAddressLength = 4;
            int DacRegControl;
            string command;
            if (one)
            {
                DacRegControl = 0x06;//"0110"CommandCode write to all dac
                dacValue = "0003";
            }
            else
            {
                DacRegControl = 0x03;//"0011"CommandCode
            }
            DacRegControl = DacRegControl << DacAddressLength;//
            DacRegControl += dacChannel;
            command = Convert.ToString(DacRegControl, 2);
            if (dac == 1)
            {
                AdapterLoopBackPath_DAC_CS.WriteDAC(command, addr, dacValue);
            }
            else
            {
                AdapterDAC_CS.WriteDAC(command, addr, dacValue);
            }
        }

        public void SetVCM(string DACName, double Vot)
        {
            string dacValue = DAC_LTC2666_Cal(Vot);
            int dacChannel = Convert.ToInt32(DACName.Substring(5));
            int dacRegNum = Convert.ToInt32(DACName.Substring(1, 1));
            WriteAdapterDac(dacRegNum, dacChannel, dacValue, true);
            Thread.Sleep(30);
            WriteAdapterDac(dacRegNum, dacChannel, dacValue, false);
        }
        //public void WriteGain(string regName, string GainValue, int add)
        //{
        //    int GainRegControl;
        //    int GainAddressLength = 4;
        //    int GainRegControl1 = 0x02;
        //    GainRegControl = add << GainAddressLength;
        //    GainRegControl += GainRegControl1;

        //    Driver.Write(regName, GainValue, GainRegControl);
        //}
        public void SetGain(string GainName, double Value)
        {
            double GainValue1 = Value;
            double GainValue = 26 - GainValue1;
            long value = Convert.ToInt32(GainValue);
            int add = Convert.ToInt32(GainName.Substring(4));
            switch (add.ToString())
            {
                case "0":
                    AdapterDACOutPutPath_CS_Q_LMH6401.Write(value);
                    break;
                case "1":
                    AdapterADCInputPath_CS_Q_LMH6401.Write(value);
                    break;
                case "2":
                    AdapterloopBackPath_CS_I_LMH6401.Write(value);
                    break;
                case "3":
                    AdapterloopBackPath_CS_Q_LMH6401.Write(value);
                    break;
                case "5":
                    AdapterDACOutPutPath_CS_I_LMH6401.Write(value);
                    break;
                case "6":
                    AdapterADCInputPath_CS_I_LMH6401.Write(value);
                    break;

                default:
                    break;
            }
        }
        public long BaseConvert(string data, int originBase, int targetBase)
        {
            if (data != "")
            {
                long mData = Convert.ToInt64(data, originBase);
                return mData;
            }
            else
                return -999;
        }
        public void PowerADC(bool on)
        {
            long value1 = on ? 1 : 0;
            int bitSetPosition = 0x5;//if index from 0,should be 4.
            REC_Control.WriteBits(bitSetPosition, 1, value1);
        }
        public void PowerAdapter()
        {
            int bitSetPosition = 0xA;
            Debug_Control.WriteBits(bitSetPosition, 1, 1);
        }
        public string getBoardVersion(string getstring)
        {
            return "P0";
        }

        #endregion
    }
}
