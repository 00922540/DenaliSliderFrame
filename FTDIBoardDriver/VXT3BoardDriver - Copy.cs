using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SliderDriver;
using VXT3BoardDriver.MyService;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Keysight.ModularInstruments.KtM9347.Peripherals;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Reflection;
using System.Threading;
namespace BoardDriver
{
    public class VXT3BoardDriver:IBoard
    {
        [DllImport("kernel32.dll")]
        #region CONST Define
        public extern static int WinExec(string exeName, int operType);
        public const int BOARD_EQUIPMENT_BIT_LENGTH = 3;
        public const int FPGA_SPI_ADDR_BIT_LENGTH = 8;
        public const int FPGA_ADDR_BIT_LENGTH = 8;
        public const int READ_WRITE_FLAG_BIT_LENGTH = 1;
        public const int LO_WYVERN_REGADDR_LENGTH = 13;
        public const int ADDED_CLOCK = 0;
        public const int MSB = 1;
        public const int CS = 0;
        public const int VE = 1;
        public const int LO_WYVERN_WRITE_DATA_SIZE = 16;
        public const int LO_WYVERN_READ_DATA_SIZE = 32;
        #endregion
        #region Veriable Define
        public WyvernRegisterSet Wyvern1;
        public WyvernRegisterSet Wyvern2;
        public WyvernDevice Wyvern1Device;
        public WyvernDevice Wyvern2Device;
        private bool bPowerOn = true;
        public double dDefaultClockRate = 19.2e9;
        #endregion
        #region Board Define
        public CLoBoard LO = new CLoBoard();
        public CDebugBoard Debug = new CDebugBoard();
        public CMulBoard Mul = new CMulBoard();
        public CRxBoard Rx = new CRxBoard();
        public CPaBoard Pa = new CPaBoard();
        public CTxBoard Tx = new CTxBoard();
        public CBbBoard Bb = new CBbBoard();
        #endregion
        public VXT3BoardDriver()
        {
            Wyvern1 = new WyvernRegisterSet("Wyvern1", "", null);
            foreach (Keysight.ModularInstruments.Core.Register.Register Register in Wyvern1.Registers)
            {
                Register.GetType().GetField("driver").SetValue(Register, this);
            }
            Wyvern2 = new WyvernRegisterSet("Wyvern2", "", null);
            foreach (Keysight.ModularInstruments.Core.Register.Register Register in Wyvern2.Registers)
            {
                Register.GetType().GetField("driver").SetValue(Register, this);
            }
            Wyvern1Device = new WyvernDevice("Wyvern1", Wyvern1, "CLK1_IN_DET");
            Wyvern2Device = new WyvernDevice("Wyvern2", Wyvern2, "CLK2_IN_DET");
            SetDriverToBoard();
        }
        #region Funtion For Board Definition
        public CEquipment GetEquipment(string BoardName,string EquipmentName)
        {
            if(this.GetType().GetField(BoardName)!=null)
            {
                object Board = this.GetType().GetField(BoardName).GetValue(this);

                if (Board.GetType().GetField(EquipmentName) != null)
                {
                    return (CEquipment) Board.GetType().GetField(EquipmentName).GetValue(Board);

                }
                else
                {
                    return null;
                }                
            }
            return null;
        }
        public CEquipment GetEquipment(string EquipmentName)
        {
            foreach(FieldInfo BoardField in this.GetType().GetFields())
            {
                if (IsBoard(BoardField.GetValue(this)))
                {
                    object Board = BoardField.GetValue(this);
                    if (Board.GetType().GetField(EquipmentName) != null)
                    {
                        return (CEquipment)Board.GetType().GetField(EquipmentName).GetValue(Board);
                    } 
                }
       
            }
            return null;
        }
        public List<string> GetAllBoardNames()
        {
            List<string> rs = new List<string>();
            foreach (FieldInfo DriverField in this.GetType().GetFields())
            {
                if (IsBoard(DriverField.GetValue(this)))
                {

                    rs.Add(DriverField.Name);

                }
            }
            return rs;
        }
        public List<string> GetAllEquipmentNames(string BoardName)
        {
            List<string> rs = new List<string>();
            if (this.GetType().GetField(BoardName) != null)
            {
                object Board = this.GetType().GetField(BoardName).GetValue(this);
                foreach(FieldInfo EquipmentField in Board.GetType().GetFields())
                { 
                    object Equipment =  EquipmentField.GetValue(Board);
                    if(Equipment.GetType()==typeof(CEquipment))
                    {
                        rs.Add(EquipmentField.Name);
                    }           
                }
            }


            return rs;
        }
        public List<string> GetAllEquipmentNames()
        {
            List<string> rs = new List<string>();

            foreach (FieldInfo BoardField in this.GetType().GetFields()) 
            {
                object Board = BoardField.GetValue(this);
                foreach (FieldInfo EquipmentField in Board.GetType().GetFields())
                {
                    object Equipment = EquipmentField.GetValue(Board);
                    if (Equipment.GetType() == typeof(CEquipment))
                    {
                        rs.Add(EquipmentField.Name);
                    }
                }
            }


            return rs;
        }
        public CBoardBase GetBoard(string BoardName)
        {
            if(this.GetType().GetField(BoardName)!=null)
            {
                return (CBoardBase)this.GetType().GetField(BoardName).GetValue(this);
            }
            else
            {
                return null;
            }
        }
        public void SetDriverToBoard()
        {
            foreach (FieldInfo DriverField in this.GetType().GetFields())
            {
                if (IsBoard(DriverField.GetValue(this)))
                {
                    dynamic Board = DriverField.GetValue(this);
                    Board.GetType().GetField("Driver").SetValue(Board, this);
                    Board.GetType().GetField("BoardName").SetValue(Board, DriverField.Name);
                    //Board.GetType().GetField("BoardName").SetValue(Board, DriverField.Name);
                    //Board.GetType().GetMethod("SetDriverToEquipment").Invoke(Board, null);
                    //Board.SetDriverToEquipment();
                    SetDriverToEquipment(Board);
                }
            }
        }
        public virtual void SetDriverToEquipment(dynamic board)
        {
            foreach (FieldInfo BoardField in board.GetType().GetFields())
            {
                if (BoardField.GetValue(board) != null)
                {
                    object obj = BoardField.GetValue(board);
                    if (obj.GetType() == typeof(CEquipment))
                    {
                        obj.GetType().GetField("Driver").SetValue(obj, board.Driver);
                        obj.GetType().GetField("BoardName").SetValue(obj, board.BoardName);
                        obj.GetType().GetField("EquipmentName").SetValue(obj, BoardField.Name);
                        obj.GetType().GetField("BoardSPI").SetValue(obj, board.SPI);
                    }
                }
                else
                {
                    continue;
                }

            }
        }
        public bool IsBoard(object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach(FieldInfo field in fields)
            {
                if(field.GetValue(obj)!=null)
                {
                    object obj2 = field.GetValue(obj);
                    if (obj2.GetType().Name == "CEquipment")
                    {
                        return true;
                    }
                }
                else
                {
                    continue;
                }
    
            }
            return false;
        }
    #endregion
        #region Driver Basic Funtion (Write,Read,connect)
        public bool Status = true;
        public long Read(string regName,string Group)
        {
            string SPIContol = "";
            if (regName.Contains("@"))
            {
                SPIContol = regName.Split('@')[1];
                regName = regName.Split('@')[0];
            }
            int iRegAddr = GetEquipment(Group,regName).Addr;
            int iBoardNo = GetEquipment(Group,regName).BoardSPI;
            int iEquipmentNo = GetEquipment(Group,regName).SPI;
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = VE;
            int RegControlBitLength = 15 + SPIContol.Length;
            int RegDataBitLength = GetEquipment(regName).Size - SPIContol.Length;
            long RegControl;
            long data;
            RegDataBitLength = GetDataSize(regName, ReadWrite, RegDataBitLength);
            RegControl = GetControlData(iBoardNo, iEquipmentNo, iRegAddr, ReadWrite,  SPIContol);
            data = Read_Data(RegDataBitLength, RegControl, RegControlBitLength, addedClock, ReadVe);
            return data;
        }
        public void Write(string regName,string Group, long data)
        {
            string SPIContol = "";
            if (regName.Contains("@"))
            {
                SPIContol = regName.Split('@')[1];
                regName = regName.Split('@')[0];
            }
            int iRegAddr = GetEquipment(Group,regName).Addr;
            int iBoardNo = GetEquipment(Group,regName).BoardSPI;
            int iEquipmentNo = GetEquipment(Group,regName).SPI;
            int addedClock = ADDED_CLOCK;
            int ReadWrite = 1;
            int RegControlBitLength = 15 + SPIContol.Length;
            int RegDataBitLength = GetEquipment(regName).Size - SPIContol.Length;
            long RegControlBit;
            RegControlBit = GetControlData(iBoardNo, iEquipmentNo, iRegAddr, ReadWrite, SPIContol);
            Write_Data(data, RegDataBitLength, RegControlBit, RegControlBitLength, addedClock);
        }
        public long Read(string regName)
        {
            string SPIContol = "";
            if (regName.Contains("@"))
            {
                SPIContol = regName.Split('@')[1];
                regName = regName.Split('@')[0];
            }
            int iRegAddr = GetEquipment(regName).Addr;
            int iBoardNo =
            int iEquipmentNo = GetEquipment(regName).SPI;
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = GetReadVe(regName);
            int RegControlBitLength = 15 + SPIContol.Length;
            int RegDataBitLength = GetEquipment(regName).Size - SPIContol.Length;
            long RegControlBit;
            long data;
            RegDataBitLength = GetDataSize(regName, ReadWrite,RegDataBitLength);
            RegControlBit = GetControlData(iBoardNo, iEquipmentNo, iRegAddr, ReadWrite, SPIContol);
            data = Read_Data(RegDataBitLength, RegControlBit, RegControlBitLength, addedClock, ReadVe);
            return data;
        }
        public void Write(string regName, long data)
        {
            string SPIContol = "";
            if (regName.Contains("@"))
            {
                SPIContol = regName.Split('@')[1];
                regName = regName.Split('@')[0];
            }
            int iRegAddr = GetEquipment(regName).Addr;
            int iBoardNo = GetEquipment(regName).BoardSPI;
            int iEquipmentNo = GetEquipment(regName).SPI;
            int addedClock = ADDED_CLOCK;
            int ReadWrite = 1;
            int RegControlBitLength = 15 + SPIContol.Length;
            int RegDataBitLength = GetEquipment(regName).Size - SPIContol.Length;
            long RegControlBit;
            RegDataBitLength = GetDataSize(regName, ReadWrite, RegDataBitLength);
            RegControlBit = GetControlData(iBoardNo, iEquipmentNo, iRegAddr, ReadWrite,  SPIContol);
            Write_Data(data, RegDataBitLength, RegControlBit, RegControlBitLength, addedClock);
        }
        public long Read(int Board, int SPI, string sSPIControl, int Addr, int iRegSize, int iDataSize, int iReadVe)
        {
            string SPIContol = sSPIControl;

            int iRegAddr = Addr;
            int iBoardNo = Board;
            int iEquipmentNo = SPI;
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = iReadVe;
            int RegControlBitLength = 15 + SPIContol.Length;
            int RegDataBitLength = iRegSize - SPIContol.Length;
            long RegControlBit;
            long data;
            RegDataBitLength = iDataSize;
            RegControlBit = GetControlData(iBoardNo, iEquipmentNo, iRegAddr, ReadWrite, SPIContol);
            data = Read_Data(RegDataBitLength, RegControlBit, RegControlBitLength, addedClock, ReadVe);
            return data;
        }
        public void Write(int Board, int SPI, string sSPIControl, int Addr, int iRegSize, int iDataSize, int iReadVe, long data)
        {
            string SPIContol = sSPIControl;
            int iRegAddr = Addr;
            int iBoardNo = Board;
            int iEquipmentNo = SPI;
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = iReadVe;
            int RegControlBitLength = 15 + SPIContol.Length;
            int RegDataBitLength = iRegSize - SPIContol.Length;
            long RegControlBit;
            RegDataBitLength = iDataSize;
            RegControlBit = GetControlData(iBoardNo, iEquipmentNo, iRegAddr, ReadWrite, SPIContol);
            Write_Data(data, RegDataBitLength, RegControlBit, RegControlBitLength, addedClock);
        }
        public int GetReadVe(string regName)
        {
            int rs =1;
            switch (regName)
            {
                case "Wyver1":
                case "Wyver2":
                    rs = 0;
                    break;
            }
            return rs;
        }
        public int GetDataSize(string RegName,int ReadWriteFlag,int RegDataBitLength)
        {
            switch(RegName)
            {
                case "Wyver1":
                case "Wyver2":
                    if (1 == ReadWriteFlag)
                    {
                        RegDataBitLength = 16;
                    }
                    break;
            }

            return RegDataBitLength;
        }
        //public long GetControlData(int BoardNo, int EquipmentNo, int RegAddr, int ReadWriteFlag, string sSPIControl)
        //{
        //    long iHeader = BoardNo;
        //    iHeader=iHeader << BOARD_EQUIPMENT_BIT_LENGTH;
        //    iHeader += EquipmentNo;
        //    if(EquipmentNo==0)
        //    {
        //        // FPGA
        //        iHeader = iHeader << FPGA_SPI_ADDR_BIT_LENGTH;
        //        iHeader = iHeader += RegAddr;
        //        iHeader = iHeader << READ_WRITE_FLAG_BIT_LENGTH;
        //        iHeader += ReadWriteFlag;
        //        //RegDataBitLength = 32;
        //    }
        //    else
        //    {
        //        iHeader = iHeader << FPGA_SPI_ADDR_BIT_LENGTH;
        //        iHeader = iHeader << READ_WRITE_FLAG_BIT_LENGTH;
        //        if (sSPIControl!="")
        //        {
        //            iHeader = iHeader << sSPIControl.Length;
        //            iHeader += Convert.ToInt32(sSPIControl, 2);
        //        }
        //        //wyvern Equipment
        //        //if (regName == "Wyver2" || regName == "Wyver1")
        //        //{
        //        //    if (0 == ReadWriteFlag)
        //        //        RegDataBitLength = 32;
        //        //    else
        //        //        RegDataBitLength = 16;
        //        //}
        //    }           

        //    return iHeader;

        //}
        public long GetControlData(int RegAddr, int ReadWriteFlag, string sSPIControl)
        {
            long iHeader = RegAddr;
            iHeader = iHeader << FPGA_ADDR_BIT_LENGTH;
            iHeader = iHeader << READ_WRITE_FLAG_BIT_LENGTH;
            iHeader += ReadWriteFlag;
            if (sSPIControl != "")
            {
                iHeader = iHeader << sSPIControl.Length;
                iHeader += Convert.ToInt32(sSPIControl, 2);
            }
            return iHeader;

        }
        public void Write_Data(long dataBuffer, int RegDataBitLength, long RegControlBit, int RegControlBitLength, int addedClock)
        {
            byte[] InputDataBuffer;
            //RegControlBit = RegControlBit << 9;
            //RegControlBit += 9;//for key spi debug
            int RegDataByteLength = (int)Math.Ceiling(RegDataBitLength / 8.0);
            int dataBitLength = RegControlBitLength + RegDataBitLength;
            int dataByteLength = (int)Math.Ceiling(dataBitLength / 8.0);

            RegControlBit = (RegControlBit << RegDataBitLength) + dataBuffer;
            RegControlBit = RegControlBit << (dataByteLength * 8 - dataBitLength);
            //MessageBox.Show(Convert.ToString(RegControlBit, 2) + ":" + (RegControlBitLength + RegDataBitLength));//show binary when debug
            InputDataBuffer = new byte[(int)Math.Ceiling((RegControlBitLength + RegDataBitLength) / 8.0)];
            for (int i = 0; i < InputDataBuffer.Length; i++)
            {
                InputDataBuffer[InputDataBuffer.Length - i - 1] = (byte)(RegControlBit >> (i * 8));
            }
            if (InputDataBuffer.Length > 1)
                FTDIClient.Send_data(dataBitLength, InputDataBuffer, VE, MSB, CS, addedClock);
            else
            {
                //MessageBox.Show("Input String Covert to Data_Buffer fail");
                return;
            }

 
        }
        public long Read_Data(int RegDataBitLength, long RegControl, int RegControlBitLength, int added_clock, int readVe = VE)
        {
            string RegData = "";
            int RegControlByteLength = (int)Math.Ceiling(RegControlBitLength / 8.0);
            RegControl = RegControl << (8 - RegControlBitLength % 8); // left move 3 bit for byte[] convert

            byte[] InputDataBuffer = new byte[RegControlByteLength];
            for (int i = 0; i < InputDataBuffer.Length; i++)
            {
                InputDataBuffer[InputDataBuffer.Length - i - 1] = (byte)(RegControl >> (i * 8));
            }
            byte[] Databuffer = FTDIClient.Read_data(RegControlBitLength, InputDataBuffer, RegDataBitLength, VE, MSB, CS, readVe, added_clock);
            //if (Databuffer != null)
            //{
            //    Array.Reverse(Databuffer);
            //    RegData = System.BitConverter.ToInt64(Databuffer, 0);
            //}
            if (Databuffer != null)
            {
                for (int i = 0; i < Databuffer.Length; i++)
                {
                    RegData += Databuffer[i].ToString("X2");
                }
            }
            if (RegData!="")
            {
                return Convert.ToInt64(RegData, 16);
            }
            else
            {
                return Int64.MaxValue;
            }
            
        }
        public bool Connect(string Resource, bool idquery, bool reset, string options)
        {
            Process[] processes = Process.GetProcessesByName("FTDIService");

            if (processes.Length == 0)
            {
                int code = WinExec("FTDIService.exe", 0);
                if (code == 2)
                    MessageBox.Show("FTDIService.exe is not found please contact engineer");
            }
            try
            {
                FTDIClient.connect();
                Status = true;
                return true;
            }
            catch
            {
                Status = false;
                return false;
            }


        }
        public bool Connect()
        {
            Process[] processes = Process.GetProcessesByName("FTDIService");

            if (processes.Length == 0)
            {
                int code = WinExec("FTDIService.exe", 0);
                if (code == 2)
                    MessageBox.Show("FTDIService.exe is not found please contact engineer");
            }
            try
            {
                FTDIClient.connect();
                Status = true;
                return true;
            }
            catch
            {
                Status = false;
                return false;
            }


        }
        public void Close()
        {
            try
            {
                FTDIClient.disconnect();
            }
            catch (Exception e)
            {
                MessageBox.Show("FTDI close fail");
            }
        }
        public void WriteDAC(string DACName,string Command,string Addr, string Value)
        {
            string sValue = Command + Addr + Value;
            Write(DACName, Convert.ToInt32(sValue,2));
        }
    #endregion
        #region Wyvern Function
        public void setCommType(int WyvernNum, bool cmos)
        {
            long dataInt = 0;
            if (1 == WyvernNum)
            {
                dataInt = Read(this.LO.Wyvern1_Ctr.EquipmentName);
                //if (dataInt == Int64.MaxValue) MessageBox.Show("FTDI error");

                if (cmos)
                {
                    dataInt = dataInt | 0x1000;// lvds disable
                    Write(this.LO.Wyvern1_Ctr.EquipmentName, dataInt);
                }
                else
                {
                    dataInt = dataInt & 0xFFFFEFFF;// lvds enable
                    Write(this.LO.Wyvern1_Ctr.EquipmentName, dataInt);
                }
            }
            else
            {
                dataInt = Read(this.LO.Wyvern2_Ctr.EquipmentName);
                //if (dataInt == 0) MessageBox.Show("FTDI error");

                if (cmos)
                {
                    dataInt = dataInt | 0x1000;// lvds disable
                    Write(this.LO.Wyvern2_Ctr.EquipmentName, dataInt);
                }
                else
                {
                    dataInt = dataInt & 0xFFFFEFFF;// lvds enable
                    Write(this.LO.Wyvern2_Ctr.EquipmentName, dataInt);
                }
            }
        }
        public void initWyvern(int WyvernNum, bool cmos)
        {
            if(WyvernNum == 1)
                Wyvern1Device.Initialize(bPowerOn, dDefaultClockRate);
            else
                Wyvern2Device.Initialize(bPowerOn, dDefaultClockRate);
            if (bPowerOn)
                bPowerOn = false;
        }
        public void resetWyvern(int WyvernNum)
        {
            if (1 == WyvernNum)
            {
                long data = Read(this.LO.Wyvern1_Ctr.EquipmentName);
                data = data | 0x2000;
                Write(this.LO.Wyvern1_Ctr.EquipmentName, data);                
                data = data & 0xFFFFDFFF;
                Write(this.LO.Wyvern1_Ctr.EquipmentName, data);
            }
            else
            {
                long data = Read(this.LO.Wyvern2_Ctr.EquipmentName);
                data = data | 0x2000;
                Write(this.LO.Wyvern2_Ctr.EquipmentName, data);
                data = data & 0xFFFFDFFF;
                Write(this.LO.Wyvern2_Ctr.EquipmentName, data);               
            }
        }
        public void setWyvernFreq(double frequency, int wyvernNum, double clockRate)
        {
            if (1 == wyvernNum)
            {
                Wyvern1Device.SetFrequencyRegValues(frequency, clockRate);
                Wyvern1Device.ApplyRegValuesToHw(true);
            }
            else
            {
                Wyvern2Device.SetFrequencyRegValues(frequency, clockRate);
                Wyvern2Device.ApplyRegValuesToHw(true);
            }
           
        }
        public void setWyvernScale(double scale, int wyvernNum)
        {
            if (1 == wyvernNum)
            {
                Wyvern1Device.SetScaleRegValues(scale);
                Wyvern1Device.ApplyRegValuesToHw(true);
            }
            else
            {
                Wyvern2Device.SetScaleRegValues(scale);
                Wyvern2Device.ApplyRegValuesToHw(true);
            }        
        }
        public void PowerWyvern(int WyvernNum, bool isOn)
        {
            long data = Read(this.LO.Power_Ctr.EquipmentName);
            int bitSetPosition = 0x0C;
            if (1 == WyvernNum)
            {
                bitSetPosition = 0x0C;
            }
            else
            {
                bitSetPosition = 0x0D;
            }
            if (isOn)
            {
                data = setBits(data, 1, 1, bitSetPosition, 32);
            }
            else
            {
                data = setBits(data, 0, 1, bitSetPosition, 32);
            }
            Write(this.LO.Power_Ctr.EquipmentName, data);
        }
        public void refreshWyvern(int WyvernNum)
        {
            if (1 == WyvernNum)
            {
                for (int i = 0; i < Wyvern1.Registers.Length; i++)
                {
                    Wyvern1.Registers[i].Read();
                }
            }
            else
            {
                for (int i = 0; i < Wyvern2.Registers.Length; i++)
                {
                    Wyvern2.Registers[i].Read();
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexCode"></param>
        /// <param name="bitsSet"></param>
        /// <param name="bitsSetLength"></param>
        /// <param name="bitSetPosition">start from number 1 not 0</param>
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
            return Convert.ToInt32(sData.Substring(sData.Length-bitSetPosition-1, bitsSetLength), 2);
        }
        public string GetWyvernHeaderString(Int32 Addr,Int32 Cmmand,int WriteFlag)
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
        #region Abus  //wait for DebugBoard
        public long selectAndReadAbus(UInt32 boardCode)
        {
            UInt32 dataInt;
            dataInt = (UInt32)Read(Debug.DebugControl.EquipmentName);
            Thread.Sleep(20);
            UInt32 mask = boardCode << 16;
            dataInt = dataInt & 0xFFF0FFFF;//clear corresponding bits
            dataInt = dataInt | mask;
            Write(Debug.DebugControl.EquipmentName, dataInt);
            Thread.Sleep(2);
            return Debug.readExtAdc();
        }
        #endregion
        #region Flash Function
        private struct SFalshRegister
        {
           public string writeRegister;
           public string readRegister ;
           public string comRegister ;
           public string statusRegister;
        }
        public void erasePage(int BoardId, int addr)
        {
            SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
            Write(flashRegisters.comRegister, 0x4000000 + addr);//erase page at addr
            System.Threading.Thread.Sleep(500);
        }
        public bool WriteFalsh(int BoardId, string address, string data)
        {
            SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
            int iter = 0;
            int dataInt = 0;     
            do
            {
                iter++;
                Write(flashRegisters.comRegister, Convert.ToInt32("05000000", 16));
                dataInt = (int)Read(flashRegisters.statusRegister);
                dataInt = dataInt & (0x10000000);
                if (iter == 10)
                {
                    MessageBox.Show("Erase Fail");
                    return false;
                }
            }
            while (dataInt != 0x10000000);

            int addressData = Convert.ToInt32(address, 16);

            string outputString;
            data = data + '\\';
            int Length = Convert.ToInt32(Math.Ceiling(data.Length / 4.0));
            data = data.PadRight(Length * 4, '\\');
            Length = Convert.ToInt32(Math.Ceiling(data.Length / 4.0));
            for (int len = 0; len < Length; len++)
            {
                if (len == Length - 1)
                    outputString = data.Substring(len * 4, data.Length - len * 4);
                else
                    outputString = data.Substring(len * 4, 4);

                dataInt = 0;
                iter = 0;
                do
                {
                    char[] values = outputString.ToCharArray();
                    string hexOutput, encodeData = "";
                    foreach (char letter in values)
                    {
                        UInt32 value = Convert.ToUInt32(letter);
                        value = BitReverse(value, 8);
                        hexOutput = Convert.ToString(value, 16).PadLeft(2, '0');
                        encodeData += hexOutput;
                    }
                    Write(flashRegisters.writeRegister, Convert.ToInt64(encodeData, 16));
                    Write(flashRegisters.comRegister, Convert.ToInt64("02" + address, 16));
                    Write(flashRegisters.comRegister, Convert.ToInt64("05000000", 16));
                    dataInt = (int)Read(flashRegisters.statusRegister);
                    dataInt = dataInt & 0x8000000;
                    if (iter++ > 10)
                        return false;
                } while (dataInt != 0x8000000);

                addressData += 4;
                address = addressData.ToString("x8").TrimStart(new char[] { '0' });
            }
            return true;

        }
        public string ReadFlash(int BoardId, string address)
        {
            SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
            int iter = 0;
            int dataInt;
            iter = 0;
            //judge read header fail or successful
            string header;
            do
            {
                Write(flashRegisters.comRegister, Convert.ToInt64("01" + address, 16));
                System.Threading.Thread.Sleep(10);
                header = Read(flashRegisters.readRegister).ToString("X").PadLeft(8, '0');;
                Write(flashRegisters.comRegister, Convert.ToInt64("05000000", 16));
                dataInt = (int)Read(flashRegisters.statusRegister);
                dataInt = dataInt & 0x4000000;
                if (iter++ > 10)
                {
                    //MessageBox.Show("Read Header Fail");
                    return "";
                }
            }
            while (dataInt != 0x4000000);
            return header;
        }
        public string ReadHeaderToString(int BoardId, string address = "100000")
        {
            string tmpInf = null;
            string tmp = "";
            UInt32 value;
            string stringValue;
            char charValue;
            string mHeader = null;
            string Headers = "";
            int errorCount = 0;
            BigInteger addressData = BigInteger.Parse(address, System.Globalization.NumberStyles.AllowHexSpecifier);
            while (errorCount++ < 25)//header is less than 100 Byte
            {
                mHeader = null;
                tmpInf = ReadFlash(BoardId, address);
                if (tmpInf.Contains("FF") || tmpInf == "")
                    break;
                for (int i = 0; i < tmpInf.Length; i += 2)
                {
                    tmp = tmpInf.Substring(i, 2);
                    value = Convert.ToUInt32(tmp, 16);
                    value = BitReverse(value, 8);
                    stringValue = Char.ConvertFromUtf32((int)value);
                    charValue = (char)value;
                    mHeader = String.Concat(mHeader, charValue);
                }
                char[] arrstr = mHeader.ToArray();
                mHeader = new string(arrstr);
                Headers = String.Concat(Headers, mHeader);
                addressData += 4;
                address = addressData.ToString("x8").TrimStart(new char[] { '0' });
                if (Headers.Contains("\\"))
                {
                    while (Headers.Contains("\\"))
                    {
                        Headers = Headers.Replace("\\", "");
                        break;
                    }
                    break;
                }
            }
            return Headers;
        }
        private UInt32 BitReverse(UInt32 value, int bitLen)
        {
            UInt32 left = (UInt32)1 << (bitLen - 1);
            UInt32 right = 1;
            UInt32 result = 0;

            for (int i = (bitLen - 1); i >= 1; i -= 2)
            {
                result |= (value & left) >> i;
                result |= (value & right) << i;
                left >>= 1;
                right <<= 1;
            }
            return result;
        }
        private SFalshRegister GetFlashRegisters(int BoardId)
        {
            SFalshRegister rs = new SFalshRegister();
            #region Define veriable base on diff Boards
            if (BoardId == this.LO.ID)
            {
                rs.writeRegister = this.LO.FLASH_WRITE.EquipmentName;
                rs.comRegister = this.LO.FLASH_COMMAND.EquipmentName;
                rs.statusRegister = this.LO.FLASH_STATUS.EquipmentName;
                rs.readRegister = this.LO.FLASH_READ.EquipmentName;
            }
            else if (BoardId == this.Mul.ID)
            {
                rs.writeRegister = this.Mul.Mul_FLASH_WRITE.EquipmentName;
                rs.comRegister = this.Mul.Mul_FLASH_COMMAND.EquipmentName;
                rs.statusRegister = this.Mul.Mul_FLASH_STATUS.EquipmentName;
                rs.readRegister = this.Mul.Mul_FLASH_READ.EquipmentName;
            }
            else if (BoardId == this.Pa.ID)
            {
                rs.writeRegister = this.Pa.Pa_FLASH_WITE.EquipmentName;
                rs.comRegister = this.Pa.Pa_FLASH_COMMAND.EquipmentName;
                rs.statusRegister = this.Pa.Pa_FLASH_STATUS.EquipmentName;
                rs.readRegister = this.Pa.Pa_FLASH_READ.EquipmentName;
            }
            else if (BoardId == this.Bb.ID)
            {
                rs.writeRegister = this.Bb.Bb_FLASH_WITE.EquipmentName;
                rs.comRegister = this.Bb.Bb_FLASH_COMMAND.EquipmentName;
                rs.statusRegister = this.Bb.Bb_FLASH_STATUS.EquipmentName;
                rs.readRegister = this.Bb.Bb_FLASH_READ.EquipmentName;
            }
            else if (BoardId == this.Rx.ID)
            {
                rs.writeRegister = this.Rx.Rx_FLASH_WITE.EquipmentName;
                rs.comRegister = this.Rx.Rx_FLASH_COMMAND.EquipmentName;
                rs.statusRegister = this.Rx.Rx_FLASH_STATUS.EquipmentName;
                rs.readRegister = this.Rx.Rx_FLASH_READ.EquipmentName;
            }
            else if (BoardId == this.Tx.ID)
            {
                rs.writeRegister = this.Tx.Tx_FLASH_WITE.EquipmentName;
                rs.comRegister = this.Tx.Tx_FLASH_COMMAND.EquipmentName;
                rs.statusRegister = this.Tx.Tx_FLASH_STATUS.EquipmentName;
                rs.readRegister = this.Tx.Tx_FLASH_READ.EquipmentName;
            }
            else if (BoardId == this.Debug.ID)
            {
                rs.writeRegister = this.Debug.DebugFlashWrite.EquipmentName;
                rs.comRegister = this.Debug.DebugFlashCom.EquipmentName;
                rs.statusRegister = this.Debug.DebugFlashStatus.EquipmentName;
                rs.readRegister = this.Debug.DebugFlashRead.EquipmentName;
            }
            #endregion
            return rs;
        }
        #endregion 
    }
    public class FTDIClient
    {    
        private static FTDIServiceClient client;
        private static byte mClock;
        private static int mSPI;
        public static void connect()
        {

            do
            {
                client = new FTDIServiceClient();
                System.Threading.Thread.Sleep(200);
            }
            while (tryConnect(client) != 1);//wait until the server is online        
        }
        public static int tryConnect(FTDIServiceClient clientA)
        {
            int success = 0;
            try
            {
                clientA.Open();
                success = 1;
            }
            catch (Exception e)
            {
            }
            return success;
        }
        public static void disconnect()
        {
            
            client.Close();
        }
        public static void Init_FDTIDevice(byte Clock_select, int SPI)
        {
            mClock = Clock_select;
            mSPI = SPI;
            client.Init_FDTIDevice(Clock_select, SPI);
        }
        public static void Send_data(int bit_length, byte[] buffer, int VE, int MSB, int CS, int Add_clock)
        {
            try
            {
                client.Send_data(bit_length, buffer, VE, MSB, CS, Add_clock);
            }
            catch (Exception e)
            {
                Console.WriteLine("FTDI fail.");
                connect();
                Init_FDTIDevice(mClock, mSPI);
            }
        }
        public static byte[] Read_data(int Write_buffer_bit_length, byte[] Write_buffer, int Read_bit_length, int VE, int MSB, int CS, int Read_VE, int Add_clock)
        {
            try
            {
                return client.Read_data(Write_buffer_bit_length, Write_buffer, Read_bit_length, VE, MSB, CS, Read_VE, Add_clock);
            }
            catch (Exception e)
            {
                Console.WriteLine("FTDI fail.");
                connect();
                Init_FDTIDevice(mClock, mSPI);
                return new byte[] { 0 };
            }
        }
        public static void FDTI_close()
        {
            try
            {
                client.FDTI_close();
            }
            catch (Exception e)
            {
                Console.WriteLine("FTDI fail.");
            }            
        }
        //public static string getServicInfo()
        //{
        //    string serviceInfo = "";
        //    try
        //    {
        //        serviceInfo = client.getServiceInfo();
        //    }
        //    catch(EndpointNotFoundException e)
        //    {
        //        Console.WriteLine("Server is not running");
        //        System.Windows.Forms.MessageBox.Show("?");
        //    }
        //    return serviceInfo;
        //}        
    }
}
