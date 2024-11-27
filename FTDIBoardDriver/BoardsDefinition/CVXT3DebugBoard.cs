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
    /// <summary>
    /// addr use 10 bits
    /// </summary>
    public class CVXT3DebugBoard:CBoardBase
    {
        #region VXT3 Equipment Definetion
        public CEquipment Debug_PWR_CTRL = new CEquipment(0x04, 32, 8);
        public CEquipment Debug_ABUS_CTRL = new CEquipment(0x05, 32, 8);
        public CEquipment Debug_CTRL = new CEquipment(0x06, 32, 8);
        public CEquipment Debug_SLUG_CTRL = new CEquipment(0x07, 32, 8);
        public CEquipment Debug_FPGA_VERSION = new CEquipment(0x10, 32, 8);
        public CEquipment Debug_CHIP_ID_L = new CEquipment(0x11, 32, 8);
        public CEquipment Debug_CHIP_ID_H = new CEquipment(0x12, 32, 8);
        public CEquipment Debug_FLASH_READ = new CEquipment(0x13, 32, 8);
        public CEquipment Debug_FLASH_WRITE = new CEquipment(0x14, 32, 8);
        public CEquipment Debug_FLASH_COMMAND = new CEquipment(0x15, 32, 8);
        public CEquipment Debug_FLASH_STATUS = new CEquipment(0x16, 32, 8);
        public CEquipment Debug_COUNTER = new CEquipment(0x17, 32, 8);
        public CEquipment Debug_REQUEST = new CEquipment(0x18, 32, 8);
        public CEquipment Debug_RESPONSE = new CEquipment(0x19, 32, 8);
        public CEquipment Debug_ACCUMLATOR = new CEquipment(0x1a, 32, 8);
        public CEquipment Debug_INT_MASK = new CEquipment(0x1b, 32, 8);
        public CEquipment Debug_Abus_ADC = new CEquipment(0x22, 32, 8);
        public CEquipment Debug_ADF4002PLL = new CEquipment(0x20, 32, 8);
        public CEquipment Debug_AD5660Ref10MHz = new CEquipment(0x21, 32, 8);
        #endregion

        #region ABUS Definetion
        public List<CAbus> AbusList = new List<CAbus>(){
            new CAbus("P3.3V_DET", "00000111", 0.5) ,
            new CAbus("P12V_DET", "00001001", 0.176) ,
            new CAbus("P6.2V_DET", "00001010", 0.176) ,
            new CAbus("P14V_DET", "00001011", 0.176) ,
            new CAbus("P1.2V_DET", "00001101", 1) ,
            new CAbus("P2.5V_DET", "00001110", 1) ,
            new CAbus("ACOM", "00001111", 1) ,

           new CAbus("P3.3V_SRC_CUR", "00001100", 1),
           new CAbus("P12V_CUR", "00011100", 1),
           new CAbus("P14V_CUR", "00101100", 1),
           new CAbus("P15V_CUR", "00111100", 1),
           new CAbus("P6.2V_CUR", "01001100", 1),
           new CAbus("P3.3V_LO_CUR", "10101100", 1),
           new CAbus("USB_PWR_DET", "10111100", 0.5),
           new CAbus("P3.3V_CUR", "11001100", 1),
           new CAbus("TEMP_REF_DET", "11011100", 1),
           new CAbus("TEMP_DEBUG_DET", "11101100", 1),

        };
        #endregion
        #region ABUS Function
        public double readAComValue(string mAbusName)
        {

                //string mAbusName = "U16_DCOM";
                long AdcSum = 0;
                double avgLong = 0;
                long[] gndAdcs = new long[5];
                long[] Adcs = new long[5];
                for (int i = 0; i < 5; i++)
                {
                    Adcs[i] = readAbusAdc(mAbusName);
                    AdcSum += Adcs[i];
                }
                avgLong = (AdcSum - (Adcs.Max() + Adcs.Min())) / 3.0;
                double aComValue;

                CAbus abus = AbusList.Find(t => t.Name == mAbusName);
                aComValue = (avgLong - 32768) * 156.25e-6 / abus.Coefficient;
                return aComValue;

        }
        public double readAbusValue(string mAbusName,  out double adcValue)
        {
            long AdcSum = 0;
            double avgLong = 0;
            long[] gndAdcs = new long[5];
            long[] Adcs = new long[5];
            double aComValue;
            string U16DOM = "P12V_DET,P6.2V_DET,P10V_DET,P1.0V_DET,P2.5V_DET";
            for (int i = 0; i < 5; i++)
            {
                Adcs[i] = readAbusAdc(mAbusName);
                AdcSum += Adcs[i];
            }
            avgLong = (AdcSum - (Adcs.Max() + Adcs.Min())) / 3.0;
            double abusValue;

            if (mAbusName == "U16_DCOM" || mAbusName == "U63_DCOM")
            {
                aComValue=0;
            }
            else
            {
                if (U16DOM.Contains(mAbusName))
                {
                   aComValue = readAComValue("U16_DCOM");
                }
                else
                {
                   aComValue = readAComValue("U63_DCOM");
                }
                
            }
            CAbus abus = AbusList.Find(t => t.Name == mAbusName);
            abusValue = (avgLong - 32768) * 156.25e-6 / abus.Coefficient;

            
            switch (mAbusName)
            {
                case "TEMP_REF_DET":
                    abusValue = abusValue / 0.01;
                    break;
                case "TEMP_DEBUG_DET":
                    abusValue = (abusValue - 0.5) / 0.01;
                    break;
                case "P3.3V_SRC_CUR":
                case "P10V_CUR":
                case "P12V_CUR":
                case "P15.4V_CUR":
                case "P4V_CUR":
                case "P6.2V_CUR":
                case "P3.3V_LO_CUR":
                    abusValue = abusValue / 0.5;
                    break;

            }
            adcValue = avgLong;
            return abusValue;
        }
        public long readAbusAdc(string mAbusName)
        {
            long longData = 0;
            Debug_ABUS_CTRL.Write(AbusList.Find(t => t.Name == mAbusName).Addr);
            Thread.Sleep(20);
            longData = selectAndReadAbus(0);
            return longData;
        }
        #endregion
        public CVXT3DebugBoard()
        {
            ID = 9;
        }
        public void Power(bool value)
        {
            if(value)
                Debug_CTRL.Write("0x0001ffff");
            else
                Debug_CTRL.Write("0x0");
        }
        public void InitBoard()
        {
            Debug_CTRL.Write(Convert.ToInt32("000004", 16));
            Debug_CTRL.Write(Convert.ToInt32("000A01", 16));
            Debug_CTRL.Write(Convert.ToInt32("048012", 16));
        }
        public void startExtAdc()
        {
            Debug_ABUS_CTRL.Write(Convert.ToUInt32("D0140003", 16));
        }
        public long readExtAdc()
        {
            long data = Debug_ABUS_CTRL.Read();
            return data >> 16;
        }
        //read boards Abus
        public long selectAndReadAbus(UInt32 boardCode)
        {
            //SetBoard(Debug.ID);
            UInt32 dataInt;
            dataInt = (UInt32)Debug_ABUS_CTRL.Read();
            Thread.Sleep(20);
            //打开Debug板上对于单板Abus通道
            Debug_ABUS_CTRL.WriteBits(4, 4, boardCode);
            Thread.Sleep(2);
            return readExtAdc();
        }
        public long selectAndReadAbus()
        {
            return readExtAdc();
        }
    }
}
