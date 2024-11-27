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
    public class CDebugBoard:CBoardBase
    {
        #region Denali
        public CEquipment Debug_SOFT_RST = new CEquipment(0x04, 32);
        public CEquipment Debug_STATUS = new CEquipment(0x08, 32);
        public CEquipment Debug_FPGA_VERSION1 = new CEquipment(0x0C, 32);
        public CEquipment Debug_PWR_CTRL = new CEquipment(0x10, 32);
        public CEquipment Debug_ABUS_CTRL = new CEquipment(0x14, 32);
        public CEquipment Debug_AUX_CTRL = new CEquipment(0x18, 32);
        public CEquipment Debug_DEB_CTRL = new CEquipment(0x1C, 32);
        public CEquipment Debug_GPIO_CTRL1 = new CEquipment(0x20, 32);
        public CEquipment Debug_GPIO_CTRL2 = new CEquipment(0x24, 32);
        public CEquipment Debug_GPIO_CTRL3 = new CEquipment(0x28, 32);
        public CEquipment Debug_GPIO_CTRL4 = new CEquipment(0x2C, 32);
        public CEquipment Debug_FPGA_VERSION2 = new CEquipment(0x40, 32);
        public CEquipment Debug_MASK = new CEquipment(0x03, 32);
        public CEquipment FLASH_READ = new CEquipment(0x4C, 32);
        public CEquipment FLASH_WRITE = new CEquipment(0x50, 32);
        public CEquipment FLASH_COMMAND = new CEquipment(0x54, 32);
        public CEquipment FLASH_STATUS = new CEquipment(0x58, 32);
        public CEquipment Debug_ABUS_ADC = new CEquipment(0x80, 32); //ADS8681 ADC on debug
        #endregion
        #region ABUS Definetion
        public List<CAbus> AbusList = new List<CAbus>(){ 
            //U16
            //new CAbus("MUL_ABUS", "00000000", 1) ,   //0
            //new CAbus("TX_ABUS", "00000001", 1) ,    //1
            //new CAbus("subRx_Temp", "00000011", 1) , //3
            //new CAbus("RX_ABUS", "00000101", 1) ,    //5
            //new CAbus("PA_ABUS", "00000110", 1) ,    //6
            new CAbus("P12V_DET", "00001001", 0.176) ,
            new CAbus("P6.2V_DET", "00001010", 0.176) ,
            new CAbus("P10V_DET", "00001011", 0.176),          
            new CAbus("P1.0V_DET", "00001101", 1),
            new CAbus("P2.5V_DET", "00001110", 1),
            new CAbus("U16_DCOM", "00001111", 1),
            //u63
            new CAbus("P3.3V_SRC_CUR", "00001100", 1),
            new CAbus("P12V_CUR", "00011100", 1),
            new CAbus("P10V_CUR", "00101100", 1),
            new CAbus("P15.4V_CUR", "00111100", 1),
            new CAbus("P4V_CUR", "01011100", 1),
            new CAbus("P6.2V_CUR", "10101100", 1),
            new CAbus("USB_PWR_DET", "10111100", 0.5),
            new CAbus("P3.3V_POWER_CUR", "11001100", 1),
            new CAbus("TEMP_REF_DET", "11011100", 1),
            new CAbus("TEMP_DEBUG_DET", "11101100", 1),
            new CAbus("U63_DCOM", "11111100", 1),
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
            Driver.Write(Driver.Debug_P1.Debug_ABUS_CTRL.EquipmentName, AbusList.Find(t => t.Name == mAbusName).Addr);
            Thread.Sleep(20);
            longData = selectAndReadAbus(0);
            return longData;
        }
        #endregion
        public CDebugBoard()
        {
            ID = 31;
        }
        public void Power(bool value)
        {
            if(value)
                Debug_PWR_CTRL.Write("0x0001ffff");
            else
                Debug_PWR_CTRL.Write("0x0");
        }
        public void InitBoard()
        {
            //Debug_CTRL.Write(Convert.ToInt32("000004", 16));
            //Debug_CTRL.Write(Convert.ToInt32("000A01", 16));
            //Debug_CTRL.Write(Convert.ToInt32("048012", 16));
        }
        public void startExtAdc()
        {
            //Debug_ABUS_ADC.Write(Convert.ToUInt32("D0140003", 16));
        }
        public long readExtAdc()
        {
            long data = Debug_ABUS_ADC.Read();
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
    }
}
