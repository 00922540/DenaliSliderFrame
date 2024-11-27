    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;

namespace BoardDriver
{

    public class CBBBoard_P1:CBoardBase
    {
        #region Equipment
        public CEquipment BB_STATUS = new CEquipment(0x99, 32);
        public CEquipment BB_MASK = new CEquipment(0x98, 32);
        public CEquipment BB_PWR_CTRL = new CEquipment(0x96, 32);
        public CEquipment BB_ABUS_CTRL = new CEquipment(0x95, 32);
        public CEquipment BB_AUX_CTRL = new CEquipment(0x00, 32);
        public CEquipment BB_Mod_part_switch_control = new CEquipment(0x00, 33);
        public CEquipment BB_Demod_part_switch_control = new CEquipment(0x94, 33);
        public CEquipment BB_CHIP_ID_L = new CEquipment(0x00, 32);
        public CEquipment BB_CHIP_ID_H = new CEquipment(0x00, 32);
        public CEquipment BB_FLASH_READ = new CEquipment(0x00, 1);
        public CEquipment BB_FLASH_WRITE = new CEquipment(0x00, 1);
        public CEquipment BB_FLASH_COMMAND = new CEquipment(0x00, 32);
        public CEquipment BB_FLASH_STATUS = new CEquipment(0x00, 32);
        public CEquipment BB_COUNTER = new CEquipment(0x00, 32);
        public CEquipment BB_Request = new CEquipment(0x00, 32);
        public CEquipment BB_Response = new CEquipment(0x00, 32);
        public CEquipment BB_Accumlator = new CEquipment(0x00, 32);
        #endregion
        #region Abus define
        public Dictionary<string, CAbus> AbusDic = new Dictionary<string, CAbus>()
        {
            {"ABUS_Temp",new CAbus("00001",-1.00)},
            {"Temp_demodulator",new CAbus("00011",-1.00)},
            {"P5V_amp_demod",new CAbus("00101",0.50)},
            {"N3V3_base_net",new CAbus("00111",0.50)},
            {"Q-_OFFSET demod_ABUS",new CAbus("01001",1.00)},
            {"Q_CM_demod_ABUS",new CAbus("01011",1.00)},
            {"Q+_OFFSET_demod_ABUS",new CAbus("01101",1.00)},
            {"ABUS_P6V8_SW",new CAbus("01111",0.33)},
            {"ABUS_REF_demod",new CAbus("10001",1.00)},
            {"P5V_base_net",new CAbus("10011",0.50)},
            {"P5V_demod",new CAbus("10101",0.50)},
            {"I-_OFFSET_demod_ABUS",new CAbus("10111",1.00)},
            {"I_CM_demod_ABUS",new CAbus("11001",1.00)},
            {"I+_OFFSET_demod_ABUS",new CAbus("11011",1.00)},
            {"ABUS_from channel",new CAbus("11101",1.00)},
            {"GND",new CAbus("11111",1.00)},

        };
        #endregion
        public double readAComValue()
        {
            string mAbusName = "ACOM";
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
            aComValue = (avgLong - 32768) * 156.25e-6 / AbusDic[mAbusName].Coefficient;
            return aComValue;
        }
        public double readAbusValue(string mAbusName, out double adcValue)
        {
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
            double abusValue;
            double aComValue;
            if (mAbusName == "ACOM")
            {
                aComValue = 0;
            }
            else
            {
                aComValue = readAComValue();
            }
            abusValue = (((avgLong - 32768) * 156.25e-6) - aComValue) / AbusDic[mAbusName].Coefficient;
            switch (mAbusName)
            {
                case "ABUS_TEMP":
                    abusValue = abusValue / 0.01;
                    break;
            }
            adcValue = avgLong;
            return abusValue;
        }
        public long readAbusAdc(string mAbusName)
        {            
            long longData = 0;
            Driver.BBBoard_P1.BB_ABUS_CTRL.WriteBits(0, AbusDic[mAbusName].Size, AbusDic[mAbusName].Addr);
            longData = Driver.Debug_P1.selectAndReadAbus(0);
            //Driver.SetBoard(Driver.BBBoard_P1.ID);
           
            return longData;
        }
        public void Power(bool Value)
        {
            if(Value)
            {
                Driver.BBBoard_P1.BB_PWR_CTRL.WriteBits(1,1,1);
            }
            else
            {

            }
            
        }

        public CBBBoard_P1()
        {
            ID = 15;
            FlashSPIAddr = 0x00;
        }
    }
}
