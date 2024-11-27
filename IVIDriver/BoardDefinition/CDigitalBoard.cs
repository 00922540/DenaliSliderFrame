using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Keysight.ModularInstruments.KtM9347.Peripherals;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Threading;
using System.IO;
using Keysight.ModularInstruments;
namespace BoardDriver
{
    public class CDigitalBoard : CBoardBase
    {
        public const int READ_WRITE_FLAG_BIT_LENGTH = 1;
        public const int LO_WYVERN_WRITE_DATA_SIZE = 16;
        public const int LO_WYVERN_READ_DATA_SIZE = 32;
        public const int LO_WYVERN_REGADDR_LENGTH = 13;
        public WyvernRegisterSet Wyvern1;
        public WyvernRegisterSet Wyvern2;
        public WyvernDevice Wyvern1Device;
        public WyvernDevice Wyvern2Device;
        #region Equipment
        public CEquipment WyvernFilter = new CEquipment(0x21504, 32);
        public CEquipment Pll3GAtt = new CEquipment(0x21508, 32);
        public CEquipment Pll3GSel = new CEquipment(0x2150C, 32);
        public WyvernCalBase.MeasureMarkerValueDelegation MeasureMarkerValueMethod;
        public WyvernCalBase.PrintTestResultDelegation PrintTestResult;
        private Keysight.ModularInstruments.CurrentSourceAdjustCal wyverncal;
        private Keysight.ModularInstruments.SwitchDriverOffsetCal wyvernswitchcal;
        #endregion

        #region ABUS Function
        #endregion
        public CDigitalBoard()
        {


        }
        public void SetPower(bool on)
        {
            if(on)
            {
                Driver.Write("CarrierBoard", "PowerControl", 4095);
            }
            else
            {
                Driver.Write("CarrierBoard", "PowerControl", 0);
            }
        }

        #region Wyvern Function
        private bool bPowerOn = true;
        public double dDefaultClockRate = 19.2e9;
        public void SetWyvernPower(int Wyvern, bool On)
        {
            if (1 == Wyvern || 2 == Wyvern)
            {
                if (On)
                {
                    Driver.Resource.Register.WriteField("WyvernCtrl", "PowerControl", "Wyvern_" + Wyvern + "_EN", 1);
                }
                else
                {
                    Driver.Resource.Register.WriteField("WyvernCtrl", "PowerControl", "Wyvern_" + Wyvern + "_EN", 0);
                }
            }
        }
        public void refreshWyvern(int WyvernNum)
        {
            if (1 == WyvernNum)
            {
                for (int i = 0; i < Wyvern1.Registers.Length; i++)
                {
                    if (!Wyvern1.Registers[i].NameBase.Contains("GNcoFreq"))
                        Wyvern1.Registers[i].Read();                                      
                }
            }
            else
            {
                for (int i = 0; i < Wyvern2.Registers.Length; i++)
                {
                    if (!Wyvern1.Registers[i].NameBase.Contains("GNcoFreq"))
                    Wyvern2.Registers[i].Read();
                }
            }

        }
        public void initWyvern(int WyvernNum)
        {
            if (WyvernNum == 1)
                Wyvern1Device.Initialize(bPowerOn, dDefaultClockRate);
            else
                Wyvern2Device.Initialize(bPowerOn, dDefaultClockRate);
            if (bPowerOn)
                bPowerOn = false;
        }
        public void ResetWyvern(int Wyvern)
        {
            if (1 == Wyvern)
            {
                long dataInt = Driver.Read("WyvernCtrl", "Wyvern1Control");
                dataInt = dataInt | 0x2000;
                Driver.Write("WyvernCtrl", "Wyvern1Control", dataInt);
                System.Threading.Thread.Sleep(100);

                dataInt = dataInt & 0xFFFFDFFF;
                Driver.Write("WyvernCtrl", "Wyvern1Control", dataInt);
                System.Threading.Thread.Sleep(100);
            }
            else
            {
                long dataInt = Driver.Read("WyvernCtrl", "Wyvern2Control");
                dataInt = dataInt | 0x2000;
                Driver.Write("WyvernCtrl", "Wyvern2Control", dataInt);
                System.Threading.Thread.Sleep(100);

                dataInt = dataInt & 0xFFFFDFFF;
                Driver.Write("WyvernCtrl", "Wyvern2Control", dataInt);
                System.Threading.Thread.Sleep(100);
            }

        }
        public void setWyvernFreq(int wyvernNum,double frequency)
        {
            if (1 == wyvernNum)
            {
                Driver.SetValue("CarrierBoard;SourceWyvernFrequency", frequency.ToString());
                
            }
            else
            {
                Driver.SetValue("CarrierBoard;ReceiverWyvernFrequency", frequency.ToString());
               
            }

        }
        public void setWyvernScale(int wyvernNum,double scale)
        {
            if (1 == wyvernNum)
            {
                Driver.SetValue("CarrierBoard;SourceWyvernScale", scale.ToString());
            }
            else
            {
                Driver.SetValue("CarrierBoard;ReceiverWyvernScale", scale.ToString());
            }
        }
        public void SetWyvernVterm(int iWyven, double value)
        {
            if (1 == iWyven)
            {
                Driver.SetValue("CarrierBoard;Ltc2602SourceDac", value.ToString());
            }
            else
            {
                Driver.SetValue("CarrierBoard;Ltc2602ReceiverDac", value.ToString());
            }

        }
        public double GetWyvernFreq(int wyvernNum)
        {
            double rs;
            if (1 == wyvernNum)
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;SourceWyvernFrequency"));
            }
            else
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;ReceiverWyvernFrequency"));
            }
            return rs;
        }
        public double GetWyvernScale(int wyvernNum)
        {
            double rs;
            if (1 == wyvernNum)
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;SourceWyvernScale"));
            }
            else
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;ReceiverWyvernScale"));
            }
             return rs;
        }
        public double GetWyvernVterm(int wyvernNum)
        {
            double rs;
            if (1 == wyvernNum)
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;Ltc2602SourceDac"));
            }
            else
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;Ltc2602ReceiverDac"));
            }
            return rs;
        }
        public void RestartWyvern(int WyvernNum)
        {
            if (1 == WyvernNum)
            {
                //SetWyvernPower(1, false);
                //System.Threading.Thread.Sleep(200);
                SetWyvernPower(1, true);
                System.Threading.Thread.Sleep(800);
                SetWyvernVterm(1, 32768);
                ResetWyvern(1);
                refreshWyvern(1);
                initWyvern(1);
            }
            else
            {
                //SetWyvernPower(2, false);
                //System.Threading.Thread.Sleep(200);
                SetWyvernPower(2, true);
                System.Threading.Thread.Sleep(800);
                SetWyvernVterm(2, 32768);
                ResetWyvern(2);
                refreshWyvern(2);
                initWyvern(2);
            }
        }
        public void RestartWyvernByIVI(int WyvernNum)
        {
            if (1 == WyvernNum)
            {
                Driver.SetValue("CarrierBoard;ReInitSourceWyvern", "");
                Thread.Sleep(1000);
            }
            else
            {
                Driver.SetValue("CarrierBoard;ReInitReceiverWyvern", "");
                Thread.Sleep(1000);
            }
        }
        //Wyvern switch Cal
        string CalDataSaveDirectory;
        public void InitWyvernSwitchCal(int iWyvern,  string sCalDataSavePath)
        {
            CalDataSaveDirectory = sCalDataSavePath;
            if (iWyvern == 1)
            {
                wyvernswitchcal = new Keysight.ModularInstruments.SwitchDriverOffsetCal("Tx", sCalDataSavePath, Wyvern1, MeasureMarkerValueMethod, PrintTestResult);
            }
            else
            {
                wyvernswitchcal = new Keysight.ModularInstruments.SwitchDriverOffsetCal( "Rx", sCalDataSavePath, Wyvern2, MeasureMarkerValueMethod, PrintTestResult);
            }

        }
        public void StartWyvernSwitchCal()
        {
            wyvernswitchcal.Execute();
        }
        public void SaveWyvernSwitchCalData()
        {
            wyvernswitchcal.WriteCalValueToLogFile();
            Driver.LoadFile("CarrierBoardDisk", wyvernswitchcal.CalFileDir + "\\" + wyvernswitchcal.CalFileName);
        }
        public string LoadWyvernSwitchCalData(int iWyvern, string sCalDataSavePath)
        {
            string pathDir = System.IO.Path.GetDirectoryName(sCalDataSavePath);
            if (iWyvern == 1)
            {
                wyvernswitchcal = new Keysight.ModularInstruments.SwitchDriverOffsetCal("Tx", sCalDataSavePath, Wyvern1, MeasureMarkerValueMethod, PrintTestResult);
            }
            else
            {
                wyvernswitchcal = new Keysight.ModularInstruments.SwitchDriverOffsetCal("Rx", sCalDataSavePath, Wyvern2, MeasureMarkerValueMethod, PrintTestResult);
            }
            wyvernswitchcal.LoadCalDataToModule();
            return "Write Wyvern Switch cal data successful!";
        }
        //Wyvern Cur Cal
        public void InitWyvernCal(int iWyvern,  string sCalDataSavePath)
        {

            if (iWyvern == 1)
            {
                wyverncal = new Keysight.ModularInstruments.CurrentSourceAdjustCal("Tx", sCalDataSavePath, Wyvern1, MeasureMarkerValueMethod, PrintTestResult);
            }
            else
            {
                wyverncal = new Keysight.ModularInstruments.CurrentSourceAdjustCal("Rx", sCalDataSavePath, Wyvern2, MeasureMarkerValueMethod, PrintTestResult);
            }

        }
        public void StartWyvernCal()
        {
            wyverncal.Execute();
        }
        public void SaveWyvernCalData(int vtermValue)
        {
            wyverncal.WriteCalValueToLogFile(vtermValue);
            Driver.LoadFile("CarrierBoardDisk",wyverncal.CalFileDir + "\\" + wyverncal.CalFileName);
        }
        public string LoadWyvernCurCalData(int iWyvern,  string sCalDataSavePath)
        {
            string pathDir = System.IO.Path.GetDirectoryName(sCalDataSavePath);
            int iVtermValue;
            if (iWyvern == 1)
            {
                wyverncal = new Keysight.ModularInstruments.CurrentSourceAdjustCal("Tx", sCalDataSavePath, Wyvern1, MeasureMarkerValueMethod, PrintTestResult);
            }
            else
            {
                wyverncal = new Keysight.ModularInstruments.CurrentSourceAdjustCal("Rx", sCalDataSavePath, Wyvern2, MeasureMarkerValueMethod, PrintTestResult);
            }
            iVtermValue = wyverncal.GetVtermValueFormCalData();
            if (iVtermValue == 0)
                return "Get vterm data error!";
            SetWyvernVterm(iWyvern, iVtermValue);
            wyverncal.LoadCalDataToModule();
            return "Write Wyvern cal data successful!";
        }
  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexCode"></param>
        /// <param name="bitsSet"></param>
        /// <param name="bitsSetLength"></param>
        /// <param name="bitSetPosition">start from number 1 not 0,from higt to low</param>
        /// <param name="totalBitLength"></param>
        /// <returns></returns>
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
        public string GetWyvernHeaderString(Int32 Addr, Int32 Cmmand, int WriteFlag)
        {
            long iHeader = 0;
            iHeader = iHeader << LO_WYVERN_REGADDR_LENGTH;
            iHeader += Addr;
            //Wyvern parity bit default 0
            iHeader = iHeader << 2;
            iHeader += Cmmand;
            iHeader = iHeader << READ_WRITE_FLAG_BIT_LENGTH;
            iHeader += WriteFlag;
            return Convert.ToString(iHeader, 2);
        }


        #endregion 
    }
}
