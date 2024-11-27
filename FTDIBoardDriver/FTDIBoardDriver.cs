using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SliderDriver;
using FTDIBoardDriver.MyService;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Keysight.ModularInstruments.KtM9347.Peripherals;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Reflection;
using System.Threading;
using System.IO;
namespace BoardDriver
{
    /// <summary>
    /// ClassName must be created like xxxBoardDriver
    /// </summary>
    public class FTDIBoardDriver:IBoard
    {
        [DllImport("kernel32.dll")]
        #region CONST Define
        public extern static int WinExec(string exeName, int operType);
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
        public int SPIAddrSize = 10;
        #region Veriable Define
        public WyvernRegisterSet Wyvern1;
        public WyvernRegisterSet Wyvern2;
        public WyvernDevice Wyvern1Device;
        public WyvernDevice Wyvern2Device;
        private bool bPowerOn = true;
        public double dDefaultClockRate = 19.2e9;
        public static bool bConnect;
        #endregion
        
        #region Board Define
        public CDebugBoard Debug_P1 = new CDebugBoard();
        public CAdapterBoard AdapterBoard = new CAdapterBoard();
        public CBBBoard_P1 BBBoard_P1 = new CBBBoard_P1();


        #endregion
        public FTDIBoardDriver()
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
            SetDriverToBoard();//通过反射的方法给实例化的各个单板加载Driver和板名
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
        public CBoardBase GetBoardByReg(string EquipmentName)
        {
            foreach (FieldInfo BoardField in this.GetType().GetFields())
            {
                if (IsBoard(BoardField.GetValue(this)))
                {
                    object Board = BoardField.GetValue(this);
                    if (Board.GetType().GetField(EquipmentName) != null)
                    {
                        return (CBoardBase)Board;
                    }
                }

            }
            return null;
        }
        public CEquipment GetRegisterByName(string EquipmentName)
        {
            foreach (FieldInfo BoardField in this.GetType().GetFields())
            {
                if (IsBoard(BoardField.GetValue(this)))
                {
                    object Board = BoardField.GetValue(this);
                    if (Board.GetType().GetField(EquipmentName) != null)
                    {
                        return (CEquipment)Board.GetType().GetField(EquipmentName).GetValue(BoardField.GetValue(this));
                    }
                }

            }
            return null;
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
                    SetDriverToEquipment(Board);//通过反射的方法给实例化的各个单板的设备加载Driver，板名，设备名和板ID
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
                        obj.GetType().GetField("BoardID").SetValue(obj, board.ID);
                    }
                }
                else
                {
                    continue;
                }

            }
        }
        /// <summary>
        /// 通过查询Object中是否还有类型为的公共域从而判断是否为单板对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True/False</returns>
        public bool IsBoard(object obj)//
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
            int iRegAddr = GetEquipment(Group, regName) != null ? GetEquipment(regName).Addr : 0;
            if (iRegAddr == 0)
            {
                MessageBox.Show("FTDIBoardDriver->Read->Not Find this Register:" + regName);
                return 0;
            }
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = VE;
            int RegControlBitLength = SPIAddrSize+1 + SPIContol.Length;
            int RegDataBitLength = GetDataSize(regName, ReadWrite, SPIContol.Length);
            long RegControl = GetControlData(iRegAddr, ReadWrite, SPIContol);
            long data = Read_Data(RegDataBitLength, RegControl, RegControlBitLength, addedClock, ReadVe);
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
            int iRegAddr = GetEquipment(Group, regName) != null ? GetEquipment(regName).Addr : 0;
            if (iRegAddr == 0)
            {
                MessageBox.Show("FTDIBoardDriver->Write->Not Find this Register:" + regName);
                return;
            }
            int addedClock = ADDED_CLOCK;
            int ReadWrite = 1;
            int RegControlBitLength = SPIAddrSize+1 + SPIContol.Length;
            //int RegDataBitLength = GetEquipment(regName).Size - SPIContol.Length;
            int RegDataBitLength = GetDataSize(regName, ReadWrite, SPIContol.Length);
            //SetBoard(GetBoard(Group).ID);
            long RegControlBit = GetControlData(iRegAddr, ReadWrite, SPIContol);
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
            int iRegAddr = GetEquipment(regName) != null ? GetEquipment(regName).Addr : 0;
            if (iRegAddr == 0)
            {
                MessageBox.Show("FTDIBoardDriver->Read->Not Find this Register:" + regName);
                return 0;
            }
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = GetReadVe(regName);
            int RegControlBitLength = SPIAddrSize+1 + SPIContol.Length;
            int RegDataBitLength = GetDataSize(regName, ReadWrite, SPIContol.Length);
            long RegControlBit = GetControlData(iRegAddr, ReadWrite, SPIContol);

            long data = Read_Data(RegDataBitLength, RegControlBit, RegControlBitLength, addedClock, ReadVe);
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
            int iRegAddr = GetEquipment(regName)!=null?GetEquipment(regName).Addr:0;
            if (iRegAddr == 0)
            {
                MessageBox.Show("FTDIBoardDriver->Write->Not Find this Register:" + regName);
                return;
            }
            int addedClock = ADDED_CLOCK;
            int ReadWrite = 1;
            int RegControlBitLength = SPIAddrSize+1 + SPIContol.Length;
            //int RegDataBitLength = GetEquipment(regName).Size - SPIContol.Length;
            //SetBoard(GetBoardByReg(regName).ID);
            //RegDataBitLength = GetDataSize(regName, ReadWrite, RegDataBitLength);
            //if (regName == Mixer.Mixer_Demodulator.EquipmentName)
            //{
            //    addedClock = 6;
            //}
            int RegDataBitLength = GetDataSize(regName, ReadWrite, SPIContol.Length);
            long RegControlBit = GetControlData(iRegAddr, ReadWrite, SPIContol);
            Write_Data(data, RegDataBitLength, RegControlBit, RegControlBitLength, addedClock);
        }
        public long Read(int BoardId, string sSPIControl, int Addr, int iDataSize, int iReadVe)
        {
            string SPIContol = sSPIControl;
            int iRegAddr = Addr;
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = iReadVe;
            int RegControlBitLength = SPIAddrSize+1 + SPIContol.Length;
            //int RegDataBitLength = iRegSize - SPIContol.Length;
            //SetBoard(BoardId);
            int RegDataBitLength = iDataSize;
            long RegControlBit = GetControlData(iRegAddr, ReadWrite, SPIContol);
            long data = Read_Data(RegDataBitLength, RegControlBit, RegControlBitLength, addedClock, ReadVe);
            return data;
        }
        public void Write(int BoardId, string sSPIControl, int Addr, int iDataSize, int iReadVe, long data)
        {
            string SPIContol = sSPIControl;
            int iRegAddr = Addr;
            int ReadWrite = 1;
            int addedClock = ADDED_CLOCK;
            int ReadVe = iReadVe;
            int RegControlBitLength = SPIAddrSize+1 + SPIContol.Length;
            //int RegDataBitLength = iRegSize - SPIContol.Length;
            //SetBoard(BoardId);
            int RegDataBitLength = iDataSize;
            long RegControlBit = GetControlData(iRegAddr, ReadWrite, SPIContol);
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
        public int GetDataSize(string RegName,int ReadWriteFlag,int controlBitLength)
        {
            int RegDataBitLength = GetEquipment(RegName).Size - controlBitLength; ;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BoardID"></param>
        public void SetBoard(int BoardID)
        {
            
            if (!CheckBoardOnLine(BoardID))
            {
                MessageBox.Show( GetBoardNameByID(BoardID) + " 单板不在线！","Error");
            }

        }
        public bool CheckBoardOnLine(int BoardID)
        {
            int addedClock = ADDED_CLOCK;
            long RegControlBit = GetControlData(0x02*4, 0, "");
            long data = Read_Data(32, RegControlBit, 11, addedClock, 1);
            long OnlineBoardID = getBits(data, 5, 4, 32);
            if (OnlineBoardID != BoardID && BoardID != 31)
            {
                return false;
            }
            else if(BoardID==31 &&  !Status)
            {
                return false;
            }
            return true;

        }
        public string GetBoardNameByID(int BoardID)
        {
            string rs = "";
            switch(BoardID)
            {
                case 15:
                    rs = "Mul";
                    break;
                case 23:
                    rs = "Tx";
                    break;
                case 27:
                    rs = "Pa";
                    break;
                case 29:
                    rs = "SubRx";
                    break;
                case 30:
                    rs = "Rx";
                    break;
                default:
                    rs = "Debug";
                    break;
            }
            return rs;
        }
        public long GetControlData(int RegAddr, int ReadWriteFlag, string sSPIControl)
        {
            long iHeader = 0;
            iHeader = iHeader << FPGA_ADDR_BIT_LENGTH;
            iHeader += RegAddr;
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
                return -1;
            }
            
        }
        public bool Connect(string Resource, bool idquery, bool reset, string options)
        {
            Process[] processes = Process.GetProcessesByName("FTDIService");
            if (processes.Length == 0)
            {
                int code = WinExec("FTDIService.exe",0);
                if (code == 2)
                    MessageBox.Show("FTDIService.exe is not found please contact engineer");

            }
            try
            {
                Status = FTDIClient.connect();
                return Status;

            }
            catch
            {
                Status = false;
                return Status;
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
                Status = FTDIClient.connect();
                return Status;
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
                MessageBox.Show("FTDI Server disconnect failed!");
            }
        }
        public void WriteDAC(string DACName,string Command,string Addr, string Value)
        {
            string sValue = Command + Addr + Value;
            int val = Convert.ToInt32(sValue,2);
            Write(DACName, Convert.ToInt32(sValue,2));
        }

    #endregion
        
        #region Wyvern Function

        public void initWyvern(int WyvernNum, bool cmos)
        {
            if(WyvernNum == 1)
                Wyvern1Device.Initialize(bPowerOn, dDefaultClockRate);
            else
                Wyvern2Device.Initialize(bPowerOn, dDefaultClockRate);
            if (bPowerOn)
                bPowerOn = false;
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
        //public void PowerWyvern(int WyvernNum, bool isOn)
        //{
        //    long data = Read(this.LO.Power_Ctr.EquipmentName);
        //    int bitSetPosition = 0x0C;
        //    if (1 == WyvernNum)
        //    {
        //        bitSetPosition = 0x0C;
        //    }
        //    else
        //    {
        //        bitSetPosition = 0x0D;
        //    }
        //    if (isOn)
        //    {
        //        data = setBits(data, 1, 1, bitSetPosition, 32);
        //    }
        //    else
        //    {
        //        data = setBits(data, 0, 1, bitSetPosition, 32);
        //    }
        //    Write(this.LO.Power_Ctr.EquipmentName, data);
        //}
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
        public void WriteRegisterField()
        {
            
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
        
        //#region Abus  //wait for DebugBoard
        //public void startExtAdc()
        //{
   
        //    Write(Debug_P1.Debug_ABUS_ADC.EquipmentName, Convert.ToUInt32("D0140003", 16));// 32005000003);
        //}
        //public long readExtAdc()
        //{
        //    long data = Read(Debug_P1.Debug_ABUS_ADC.EquipmentName);
        //    return data >> 16;
        //}
        ////read boards Abus
        //public long selectAndReadAbus(UInt32 boardCode)
        //{
        //    //SetBoard(Debug.ID);
        //    UInt32 dataInt;
        //    dataInt = (UInt32)Read(Debug_P1.Debug_ABUS_CTRL.EquipmentName);
        //    Thread.Sleep(20);
        //    //打开Debug板上对于单板Abus通道
        //    long data = setBits(dataInt, boardCode, 4, 4, 32);
        //    Write(Debug_P1.Debug_ABUS_CTRL.EquipmentName, data);
        //    Thread.Sleep(2);
        //    return readExtAdc();    
        //}
        ////read debug board Abus
        //public long selectAndReadAbus()
        //{
        //    return readExtAdc();
        //}
        //#endregion
        
        //#region Flash Function
        //    #region common Function
        //    public bool WriteFalsh(int BoardId, string offsetAddr, string data)
        //    {
        //        bool rs = false;
        //        int offset = Convert.ToInt32(offsetAddr, 16);
        //        string address;
        //        switch(BoardId)
        //        {
        //            case 29:
        //                address = Convert.ToString(offset, 2);
        //                WriteFlashString_E(BoardId, address, data);
        //                break;
        //            case 15:
        //            case 27:
        //                address = Convert.ToString(0x100000 + offset, 16);
        //                WriteFlashString_A(BoardId, address, data);
        //                break;
        //            case 23:
        //            case 30:
        //            case 31:
        //                address = Convert.ToString(0x300000 + offset, 16);
        //                WriteFlashString_X(BoardId, address, data);
        //                break;
                    
        //        }
        //        return rs;
        //    }
        //    public bool WriteFalsh_P2(int BoardId, string offsetAddr, string data)
        //    {
        //        bool rs = false;
        //        int offset = Convert.ToInt32(offsetAddr, 16);
        //        string address;
        //        switch (BoardId)
        //        {
        //            case 29:
        //                address = Convert.ToString(offset, 2);
        //                WriteFlashString_E(BoardId, address, data);
        //                break;
        //            case 15:
        //            case 27:
        //            case 23:
        //            case 30:
        //            case 31:
        //                address = Convert.ToString(0x300000 + offset, 16);
        //                WriteFlashString_X(BoardId, address, data);
        //                break;

        //        }
        //        return rs;
        //    }
        //    public string ReadFlashString(int BoardId, string offsetAddr)
        //    {
        //        string rs = "";
        //        int offset = Convert.ToInt32(offsetAddr, 16);
        //        string address;
        //        switch (BoardId)
        //        {
        //            case 15:
        //            case 27:
        //                address = Convert.ToString(0x100000 + offset, 16);
        //                rs = ReadFlashString_A(BoardId, address);
        //                break;
        //            case 29:
        //                address = Convert.ToString(offset, 2);
        //                rs=ReadFlashString_E(BoardId, address);
        //                break;
        //            case 23:
        //            case 30:
        //            case 31:
        //                address = Convert.ToString(0x300000 + offset, 16);
        //                rs = ReadFlashString_X(BoardId, address);
        //                break;

        //        }
        //        return rs;
        //    }
        //    public string ReadFlashString_P2(int BoardId, string offsetAddr)
        //    {
        //        string rs = "";
        //        int offset = Convert.ToInt32(offsetAddr, 16);
        //        string address;
        //        switch (BoardId)
        //        {
        //            case 29:
        //                address = Convert.ToString(offset, 2);
        //                rs = ReadFlashString_E(BoardId, address);
        //                break;
        //            case 15:
        //            case 27:
        //            case 23:
        //            case 30:
        //            case 31: //debug
        //                address = Convert.ToString(0x300000 + offset, 16);
        //                rs = ReadFlashString_X(BoardId, address);
        //                break;

        //        }
        //        return rs;
        //    }
        //    public void clearFlash(int BoardId, string offsetAddr)
        //    {
        //        int offset = Convert.ToInt32(offsetAddr, 16);
        //        string address;
        //        switch (BoardId)
        //        {
        //            case 15:
        //            case 27:
        //                address = Convert.ToString(0x100000 + offset, 16);
        //                erasePage_A(BoardId);
        //                break;
        //            case 29:
        //                address = Convert.ToString(offset, 2);
        //                ClearFalsh_E(BoardId, offsetAddr);
        //                break;
        //            case 23:
        //            case 30:
        //            case 31:
        //                address = Convert.ToString(0x300000 + offset, 16);
        //                ClearFalsh_X(BoardId, address);
        //                break;

        //        }
        //    }
        //    public void clearFlash_P2(int BoardId, string offsetAddr)
        //    {
        //        int offset = Convert.ToInt32(offsetAddr, 16);
        //        string address;
        //        switch (BoardId)
        //        {
        //            case 29:
        //                address = Convert.ToString(offset, 2);
        //                //ClearFalsh_E(BoardId, offsetAddr);
        //                ClearAllEEPROM_E(BoardId);
        //                break;
        //            case 15:
        //            case 27:
        //            case 23:
        //            case 30:
        //            case 31:
        //                address = Convert.ToString(0x300000 + offset, 16);
        //                ClearFalsh_X(BoardId, address);
        //                break;
        //        }
        //    }
        //    public string GetHeader(int BoardId)
        //    {
        //        string sHeader = "";
        //        string[] HeadFromFW = new string[3];
        //        switch (BoardId)
        //        {
        //            case 15:
        //            case 27:
        //                HeadFromFW[0] = this.ReadFlashString_A(BoardId, "100800");
        //                HeadFromFW[1] = this.ReadFlashString_A(BoardId, "100820");
        //                HeadFromFW[2] = this.ReadFlashString_A(BoardId, "101000");
        //                sHeader = HeadFromFW[2];
        //                sHeader = sHeader.Replace("{BoardRevision}", HeadFromFW[0]);
        //                sHeader = sHeader.Replace("{SerialNumber}", HeadFromFW[1]);
        //                break;
        //            case 29:
        //                HeadFromFW[0] = this.ReadFlashString_E(BoardId, "0");
        //                HeadFromFW[1] = this.ReadFlashString_E(BoardId, "1000");
        //                HeadFromFW[2] = this.ReadFlashString_E(BoardId, "10000");
        //                sHeader = HeadFromFW[2];
        //                sHeader = sHeader.Replace("{BoardRevision}", HeadFromFW[0]);
        //                sHeader = sHeader.Replace("{SerialNumber}", HeadFromFW[1]);
        //                break;
        //            case 23:
        //            case 30:
        //            case 31:
        //                HeadFromFW[0] = this.ReadFlashString_X(BoardId, "300800");
        //                HeadFromFW[1] = this.ReadFlashString_X(BoardId, "300820");
        //                HeadFromFW[2] = this.ReadFlashString_X(BoardId, "301000");
        //                sHeader = HeadFromFW[2];
        //                sHeader = sHeader.Replace("{BoardRevision}", HeadFromFW[0]);
        //                sHeader = sHeader.Replace("{SerialNumber}", HeadFromFW[1]);
        //                break;
        //        }
        //        return sHeader;
        //    }
        //    public string GetHeader_P2(int BoardId)
        //    {
        //        string sHeader = "";
        //        string[] HeadFromFW = new string[3];
        //        switch (BoardId)
        //        {
        //            case 29:
        //                HeadFromFW[0] = this.ReadFlashString_E(BoardId, "0");
        //                HeadFromFW[1] = this.ReadFlashString_E(BoardId, "10000");
        //                HeadFromFW[2] = this.ReadFlashString_E(BoardId, "100000");
        //                sHeader = HeadFromFW[2];
        //                sHeader = sHeader.Replace("{BR}", HeadFromFW[0]);
        //                sHeader = sHeader.Replace("{SN}", HeadFromFW[1]);
        //                break;
        //            case 23:
        //            case 30:
        //            case 31:
        //            case 15:
        //            case 27:
        //                HeadFromFW[0] = this.ReadFlashString_X(BoardId, "310000");
        //                HeadFromFW[1] = this.ReadFlashString_X(BoardId, "310020");
        //                HeadFromFW[2] = this.ReadFlashString_X(BoardId, "320000");
        //                sHeader = HeadFromFW[2];
        //                sHeader = sHeader.Replace("{BR}", HeadFromFW[0]);
        //                sHeader = sHeader.Replace("{SN}", HeadFromFW[1]);
        //                break;
        //        }
        //        return sHeader;
        //    }
        //    public bool SetHeader(int BoardId, string[] data)
        //    {
        //        bool a = false, b = false, c = false;
        //        switch (BoardId)
        //        {
        //            case 15: //Mul
        //            case 27: //Pa
        //                this.erasePage_A(BoardId, "800");
        //                this.erasePage_A(BoardId, "1000");
        //                a = this.WriteFlashString_A(BoardId, "100800", data[0]);
        //                b = this.WriteFlashString_A(BoardId, "100820", data[1]);
        //                c = this.WriteFlashString_A(BoardId, "101000", data[2]);
        //                break;
        //            case 29: //SubRx
        //                ClearFalsh_E(BoardId, "0");
        //                this.WriteFlashString_E(BoardId, "0", data[0]);
        //                this.WriteFlashString_E(BoardId, "20", data[1]);
        //                this.WriteFlashString_E(BoardId, "40", data[2]);
        //                break;
        //            case 23:
        //            case 30:
        //            case 31:
        //                this.ClearFalsh_X(BoardId, "300800");
        //                this.ClearFalsh_X(BoardId, "300820");
        //                this.ClearFalsh_X(BoardId, "301000");
        //                this.WriteFlashString_X(BoardId, "300800", data[0]);
        //                this.WriteFlashString_X(BoardId, "300820", data[1]);
        //                this.WriteFlashString_X(BoardId, "301000", data[2]);
        //                break;
        //        }
        //        if (a && b && c)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    public bool SetHeader_P2(int BoardId, string[] data)
        //    {
        //        bool a = false, b = false, c = false;
            
        //        switch (BoardId)
        //        {
        //            case 29: //SubRx
        //                //ClearFalsh_E(BoardId, "0");
        //                ClearAllEEPROM_E(BoardId);
        //                this.WriteFlashString_E(BoardId, "0", data[0]);
        //                this.WriteFlashString_E(BoardId, "10000", data[1]);
        //                this.WriteFlashString_E(BoardId, "100000", data[2]);
        //                break;
        //            case 23:
        //            case 30:
        //            case 31:
        //            case 15: //Mul
        //            case 27: //Pa
        //                this.ClearFalsh_X(BoardId, "310000");
        //                this.ClearFalsh_X(BoardId, "310020");
        //                this.ClearFalsh_X(BoardId, "320000");
        //                this.WriteFlashString_X(BoardId, "310000", data[0]);
        //                this.WriteFlashString_X(BoardId, "310020", data[1]);
        //                this.WriteFlashString_X(BoardId, "320000", data[2]);
        //                break;
        //        }
        //        return true;
        //    }
        //    public bool SetHeaderByString(int BoardId, string data)
        //    {
        //        string[] aHeader = data.Split(',');
        //        if (aHeader.Length != 9)
        //        {
        //            return false;
        //        }
        //        string[] HeaderToFW = new string[] { aHeader[1], aHeader[3], aHeader[0] + ",{BoardRevision}," + aHeader[2] + ",{SerialNumber}," + aHeader[4] + "," + aHeader[5] + "," + aHeader[6] + "," + aHeader[7] + "," + aHeader[8] };
        //        return SetHeader_P2(BoardId, HeaderToFW);
        //    }

        //    private UInt32 BitReverse(UInt32 value, int bitLen)
        //    {
        //        UInt32 left = (UInt32)1 << (bitLen - 1);
        //        UInt32 right = 1;
        //        UInt32 result = 0;

        //        for (int i = (bitLen - 1); i >= 1; i -= 2)
        //        {
        //            result |= (value & left) >> i;
        //            result |= (value & right) << i;
        //            left >>= 1;
        //            right <<= 1;
        //        }
        //        return result;
        //    }
        //    int GetFlashSPIAddr(int BoardId)
        //    {
        //        int SPI_Addr = 0;
        //        switch (BoardId)
        //        {
        //            case 15://mul
        //                SPI_Addr = 0x51;
        //                break;
        //            case 27://pa
        //                SPI_Addr = 0xC0;
        //                break;
        //            case 23://tx
        //                SPI_Addr = 0x52;
        //                break;
        //            case 29:
        //                SPI_Addr = 0x80;
        //                break;
        //            case 30://rx
        //                SPI_Addr = 0x81;
        //                break;
        //            case 31: //debug
        //                SPI_Addr = 0x23;
        //                break;
        //        }
        //        return SPI_Addr*4;
        //    }
        //    bool ParamCheck(int SPI_Addr, string command, string address)
        //    {
        //        if (SPI_Addr == 0x200) //EEPROM
        //        {
        //            int addr = Convert.ToInt32(address, 2);
        //            if (addr > 128)
        //            {
        //                MessageBox.Show("The address can't be operation!");
        //                return false;
        //            }
        //            else if (address.Length > 8)
        //            {
        //                MessageBox.Show("Address length error!");
        //                return false;
        //            }
        //            else if (command.Length != 3 || command == null)
        //            {
        //                MessageBox.Show("Command length error!");
        //                return false;
        //            }
        //            return true;
        //        }
        //        else
        //        {
        //            if (command != "05" && command != "06")
        //            {
        //                int addr = Convert.ToInt32(address, 16);
        //                if (addr < 0x300000)
        //                {
        //                    MessageBox.Show("The address can't be operation!");
        //                    return false;
        //                }
        //                else if (address.Length > 6)
        //                {
        //                    MessageBox.Show("Address length error!");
        //                    return false;
        //                }
        //                else if (command.Length != 2 || command == null)
        //                {
        //                    MessageBox.Show("Command length error!");
        //                    return false;
        //                }
        //            }
        //            return true;
        //        }

        //        return true;
        //    }
        //    #endregion common Function
        //    //====================New flash function for Altera FPGA===================
        //    #region flash function for Altera FPGA
        //    private struct SFalshRegister
        //    {
        //       public string writeRegister;
        //       public string readRegister ;
        //       public string comRegister ;
        //       public string statusRegister;
        //    }
        //    public void erasePage_A(int BoardId)
        //    {
        //        SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
        //        Write(flashRegisters.comRegister, Convert.ToInt64("04" + "000001", 16));
        //        System.Threading.Thread.Sleep(500);
        //        int iter = 0;
        //        int dataInt = 0;
        //        do
        //        {
        //            iter++;
        //            Write(flashRegisters.comRegister, Convert.ToInt32("05000000", 16));
        //            dataInt = (int)Read(flashRegisters.statusRegister);
        //            dataInt = dataInt & (0x10000000);
        //            if (iter == 10)
        //            {
        //                MessageBox.Show("Erase Fail");
        //                return;
        //            }
        //        }
        //        while (dataInt != 0x10000000);

        //    }
        //    public void erasePage_A(int BoardId, string offset)
        //    {
        //        string addrStr = Convert.ToString(0x100000 + Convert.ToInt32(offset, 16) / 4, 16);
        //        SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
        //        Write(flashRegisters.comRegister, Convert.ToInt64("03" + addrStr, 16));
        //        System.Threading.Thread.Sleep(500);
        //        int iter = 0;
        //        int dataInt = 0;
        //        do
        //        {
        //            iter++;
        //            Write(flashRegisters.comRegister, Convert.ToInt32("05000000", 16));
        //            dataInt = (int)Read(flashRegisters.statusRegister);
        //            dataInt = dataInt & (0x10000000);
        //            if (iter == 10)
        //            {
        //                MessageBox.Show("Page Erase Fail");
        //                return;
        //            }
        //        }
        //        while (dataInt != 0x10000000);

        //    }
        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    /// <param name="BoardId"></param>
        //    /// <param name="address"></param>
        //    /// <returns>return one ASCString</returns>
        //    //public string ReadFlash_X(int BoardId, string address)
        //    //{
        //    //    SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
        //    //    int iter = 0;
        //    //    int dataInt;
        //    //    iter = 0;
        //    //    //judge read header fail or successful
        //    //    string header;
        //    //    do
        //    //    {
        //    //        Write(flashRegisters.comRegister, Convert.ToInt64("01" + address, 16));
        //    //        System.Threading.Thread.Sleep(10);
        //    //        header = Read(flashRegisters.readRegister).ToString("X").PadLeft(8, '0'); ;
        //    //        Write(flashRegisters.comRegister, Convert.ToInt64("05000000", 16));
        //    //        dataInt = (int)Read(flashRegisters.statusRegister);
        //    //        dataInt = dataInt & 0x4000000;
        //    //        if (iter++ > 10)
        //    //        {
        //    //            //MessageBox.Show("Read Header Fail");
        //    //            return "";
        //    //        }
        //    //    }
        //    //    while (dataInt != 0x4000000);
        //    //    return header;
        //    //}

        //    public bool WriteFlashString_A(int BoardId, string address, string stringData)
        //    {
        //        if (stringData == null)
        //            stringData = "";
        //        SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
        //        int iter = 0;
        //        int dataInt = 0;
        //        do
        //        {
        //            iter++;
        //            Write(flashRegisters.comRegister, Convert.ToInt32("05000000", 16));
        //            dataInt = (int)Read(flashRegisters.statusRegister);
        //            dataInt = dataInt & (0x10000000);
        //            if (iter == 10)
        //            {
        //                MessageBox.Show("Erase first!");
        //                return false;
        //            }
        //        }
        //        while (dataInt != 0x10000000);
        //        dataInt = 0;
        //        int addressData = Convert.ToInt32(address, 16);
        //        string outputString;
        //        stringData = stringData + '\0';
        //        int iTakeUpAddrSize = Convert.ToInt32(Math.Ceiling(stringData.Length / 4.0));
        //        stringData = stringData.PadRight(iTakeUpAddrSize * 4, '\0');
        //        //Length = Convert.ToInt32(Math.Ceiling(data.Length / 4.0));
        //        for (int AddrNo = 0; AddrNo < iTakeUpAddrSize; AddrNo++)
        //        {
        //            //if (section == Math.Ceiling(data.Length / 4.0 )- 1)
        //            //    outputString = data.Substring(section * 4, data.Length - section * 4);
        //            //else
        //            outputString = stringData.Substring(AddrNo * 4, 4);
        //            iter = 0;
        //            do
        //            {
        //                iter++;
        //                char[] values = outputString.ToCharArray();
        //                string hexOutput, encodeData = "";
        //                foreach (char letter in values)
        //                {
        //                    UInt32 value = Convert.ToUInt32(letter);
        //                    value = BitReverse(value, 8);
        //                    hexOutput = Convert.ToString(value, 16).PadLeft(2, '0');
        //                    encodeData += hexOutput;
        //                }
        //                Write(flashRegisters.writeRegister, Convert.ToInt64(encodeData, 16));
        //                Write(flashRegisters.comRegister, Convert.ToInt64("02" + address, 16));
        //                Write(flashRegisters.comRegister, Convert.ToInt64("05000000", 16));
        //                dataInt = (int)Read(flashRegisters.statusRegister);
        //                dataInt = dataInt & 0x8000000;
        //                if (iter++ > 10)
        //                    return false;
        //            } while (dataInt != 0x8000000);

        //            addressData += 4;
        //            address = addressData.ToString("x8").TrimStart(new char[] { '0' });
        //        }
        //        return true;

        //    }

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    /// <param name="BoardId"></param>
        //    /// <param name="address"></param>
        //    /// <param name="ReadBitLength"></param>
        //    /// <returns>return ASCString</returns>
        //    public string ReadFlashString_A(int BoardId, string address = "100000")
        //    {
        //        string tmpInf = null;
        //        string tmp = "";
        //        UInt32 value;
        //        string stringValue;
        //        char charValue;
        //        string mHeader = null;
        //        string Headers = "";
        //        BigInteger addressData = BigInteger.Parse(address, System.Globalization.NumberStyles.AllowHexSpecifier);
        //        while (true)
        //        {
        //            mHeader = null;
        //            tmpInf = flashRegRead_A(BoardId, address, 8);
        //            if (tmpInf.Contains("FF") || tmpInf == "")
        //                break;
        //            for (int i = 0; i < tmpInf.Length; i += 2)
        //            {
        //                tmp = tmpInf.Substring(i, 2);
        //                value = Convert.ToUInt32(tmp, 16);
        //                value = BitReverse(value, 8);
        //                stringValue = Char.ConvertFromUtf32((int)value);
        //                charValue = (char)value;
        //                mHeader = String.Concat(mHeader, charValue);
        //            }
        //            char[] arrstr = mHeader.ToArray();
        //            mHeader = new string(arrstr);
        //            Headers = String.Concat(Headers, mHeader);
        //            addressData += 4;
        //            address = addressData.ToString("x8").TrimStart(new char[] { '0' });
        //        }
        //        return Headers.Replace("\0","");
        //    }


        //    private SFalshRegister GetFlashRegisters(int BoardId)
        //    {
        //        SFalshRegister rs = new SFalshRegister();
        //        #region Define veriable base on diff Boards
        //        //if (BoardId == this.MulBoard.ID)
        //        //{
        //        //    rs.writeRegister = this.MulBoard.Mul_FLASH_WRITE.EquipmentName;
        //        //    rs.comRegister = this.MulBoard.Mul_FLASH_COMMAND.EquipmentName;
        //        //    rs.statusRegister = this.MulBoard.Mul_FLASH_STATUS.EquipmentName;
        //        //    rs.readRegister = this.MulBoard.Mul_FLASH_READ.EquipmentName;
        //        //}
        //        //else if (BoardId == this.PaBoard.ID)
        //        //{
        //        //    rs.writeRegister = this.PaBoard.Pa_FLASH_WRITE.EquipmentName;
        //        //    rs.comRegister = this.PaBoard.Pa_FLASH_COMMAND.EquipmentName;
        //        //    rs.statusRegister = this.PaBoard.Pa_FLASH_STATUS.EquipmentName;
        //        //    rs.readRegister = this.PaBoard.Pa_FLASH_READ.EquipmentName;
        //        //}
        //        #endregion
        //        return rs;
        //    }

        //    public string flashRegRead_A(int BoardId, string address, int ReadBitLength)
        //    {
        //        SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
        //        int iter = 0;
        //        int dataInt;
        //        iter = 0;
        //        //judge read header fail or successful
        //        string rs = "", data;
        //        for (int i = 0; i < ReadBitLength / 8; i++)
        //        {
        //            do
        //            {
        //                Write(flashRegisters.comRegister, Convert.ToInt64("01" + address, 16));
        //                System.Threading.Thread.Sleep(10);
        //                data = Read(flashRegisters.readRegister).ToString("X").PadLeft(8, '0'); ;
        //                Write(flashRegisters.comRegister, Convert.ToInt64("05000000", 16));
        //                dataInt = (int)Read(flashRegisters.statusRegister);
        //                dataInt = dataInt & 0x4000000;
        //                if (iter++ > 10)
        //                {
        //                    //MessageBox.Show("Read Header Fail");
        //                    return "";
        //                }
        //            }
        //            while (dataInt != 0x4000000);
        //            //data = ReadFlash_X(BoardId, address + i * 4);
        //            rs = rs + data;

        //        }
        //        return rs;
        //    }

        //    public bool flashRegWrite_A(int BoardId, string address, string ASCStringData)
        //    {
        //        SFalshRegister flashRegisters = GetFlashRegisters(BoardId);
        //        int iter = 0;
        //        int dataInt = 0;
        //        do
        //        {
        //            iter++;
        //            Write(flashRegisters.comRegister, Convert.ToInt32("05000000", 16));
        //            dataInt = (int)Read(flashRegisters.statusRegister);
        //            dataInt = dataInt & (0x10000000);
        //            if (iter == 10)
        //            {
        //                MessageBox.Show("Erase first!");
        //                return false;
        //            }
        //        }
        //        while (dataInt != 0x10000000);
        //        do
        //        {
        //            iter++;
        //            int iRegAddr = GetEquipment(flashRegisters.comRegister).Addr;
        //            int ReadWrite = 1;
        //            int RegControlBitLength = 9;
        //            int RegDataBitLength = ASCStringData.Length * 4;
        //            long RegControlBit = GetControlData(iRegAddr, ReadWrite, "");
        //            long data = long.Parse(ASCStringData);
        //            Write_Data(data, RegDataBitLength, RegControlBit, RegControlBitLength, ADDED_CLOCK);
        //            Write(flashRegisters.comRegister, Convert.ToInt64("02" + address, 16));
        //            Write(flashRegisters.comRegister, Convert.ToInt64("05000000", 16));
        //            dataInt = (int)Read(flashRegisters.statusRegister);
        //            dataInt = dataInt & 0x8000000;
        //            if (iter++ > 10)
        //                return false;
        //        } while (dataInt != 0x8000000);
        //        return true;
        //    }
        //    #endregion 
        //    //====================New flash function for Xilinx FPGA===================
        //    #region flash function for Xilinx FPGA
        //    public void ClearFalsh_X(int BoardId, string address)
        //    {
        //        string ret = flashRegRead_X(BoardId, "05", "", 8);//READ STATUS REG1
        //        ret = flashRegRead_X(BoardId, "05", "", 8);
        //        flashRegWrite_X(BoardId, "06", "", ""); //WREN
        //        ret = flashRegRead_X(BoardId, "05", "", 8);
        //        while (ret != "02")
        //        {
        //            ret = flashRegRead_X(BoardId, "05", "", 8);
        //        }
        //        flashRegWrite_X(BoardId, "D8", address, "");//SE 

        //        ret = flashRegRead_X(BoardId, "05", "", 8);
        //        while (ret != "00")
        //        {
        //            ret = flashRegRead_X(BoardId, "05", "", 8);
        //        }
        //    }

        //    public void WriteFlashString_X(int BoardId, string address, string stringData)
        //    {
        //        int iAddress = Convert.ToInt32(address, 16);
        //        string outputString;
        //        //stringData = stringData + '\0';
        //        int iTakeUpAddrSize = Convert.ToInt32(Math.Ceiling(stringData.Length / 4.0));
        //        stringData = stringData.PadRight(iTakeUpAddrSize * 4, '\0');
        //        for (int AddrNo = 0; AddrNo < iTakeUpAddrSize; AddrNo++)
        //        {
        //            outputString = stringData.Substring(AddrNo * 4, 4);
        //            char[] values = outputString.ToCharArray();
        //            string hexOutput, ASCDataString = "";
        //            //foreach (char letter in values)
        //            //{
        //            //    UInt32 value = Convert.ToUInt32(letter);
        //            //    value = BitReverse(value, 8);
        //            //    hexOutput = Convert.ToString(value, 16).PadLeft(2, '0');
        //            //    ASCDataString += hexOutput;
        //            //}
        //            foreach (char letter in values)
        //            {
        //                UInt32 value = Convert.ToUInt32(letter);
        //                hexOutput = Convert.ToString(value, 16).PadLeft(2, '0');
        //                ASCDataString += hexOutput;
        //            }
        //            WriteFlash_X(BoardId, address, ASCDataString);
        //            iAddress += 4;
        //            address = iAddress.ToString("x8").TrimStart(new char[] { '0' });
        //        }
        //    }
        //    public void WriteFlash_X(int BoardId, string address, string ASCStringData)
        //    {
        //        string ret = flashRegRead_X(BoardId, "05", "", 8);
        //        flashRegWrite_X(BoardId, "06", "", "");
        //        ret = flashRegRead_X(BoardId, "05", "", 8);
        //        //while (ret != "02")
        //        //{
        //        //    ret = flashRegRead_X(BoardId, "05", "", 8);
        //        //}
        //        flashRegWrite_X(BoardId, "02", address, ASCStringData); //PP
        //        ret = flashRegRead_X(BoardId, "05", "", 8);
        //        //while (ret != "02")
        //        //{
        //        //    ret = flashRegRead_X(BoardId, "05", "", 8);
        //        //}
        //    }
        //    void flashRegWrite_X(int BoardId, string command, string address, string ASCStringData) //Address: data or address + data
        //    {
        //        int SPI_Addr = GetFlashSPIAddr(BoardId);
        //        if (!ParamCheck(SPI_Addr, command, address))
        //        {
        //            return;
        //        }
        //        SPI_Addr = SPI_Addr << 1;
        //        BigInteger RegControlBit = BigInteger.Parse(string.Format("0{0}", SPI_Addr.ToString("X")) + command + address + ASCStringData, System.Globalization.NumberStyles.AllowHexSpecifier);
        //        int dataBitLength = SPIAddrSize + 1 + command.Length * 4 + address.Length * 4 + ASCStringData.Length * 4;//9 = SPI addr(8 bits) + Read/Write(1 bit)
        //        int rem = dataBitLength % 8;
        //        if (rem != 0)
        //        {
        //            RegControlBit = RegControlBit << (8 - rem);
        //        }
        //        byte[] byteBuffer = RegControlBit.ToByteArray();
        //        byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];

        //        for (int i = 0; i < InputDataBuffer.Length; i++)
        //        {
        //            InputDataBuffer[InputDataBuffer.Length - i - 1] = byteBuffer[i];
        //        }
        //        byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, 0, 1, 1, 0, 0, 0);
        //        //FTDIClient.Send_data(dataBitLength, InputDataBuffer, 1, 1, 0, 0);
        //        //string RegData = "";
        //        //if (OutputDataBuffer != null)
        //        //{
        //        //    for (int i = 0; i < OutputDataBuffer.Length; i++)
        //        //    {
        //        //        RegData += OutputDataBuffer[i].ToString("X2");
        //        //    }
        //        //}
        //    }

        //    public string ReadFlashString_X(int BoardId, string address)
        //    {
        //        string data,datan;
        //        UInt32 tmp;
        //        char charValue;
        //        string value = null, Headers="";
        //        BigInteger iAddr = BigInteger.Parse(string.Format("0{0}", address), System.Globalization.NumberStyles.AllowHexSpecifier);
        //        while (true)
        //        {
        //            value = null;
        //            data = flashRegRead_X(BoardId, "03",address,32);
        //            if (data.Contains("FF") || data == "")
        //                break;

        //            //tmp = Convert.ToUInt32(data, 16);
        //            //tmp = BitReverse(tmp, 8);
        //            //charValue = (char)tmp;
        //            //value = String.Concat(value, charValue);
        //            //iAddr += 4;
        //            //address = iAddr.ToString("x8").TrimStart(new char[] { '0' });

        //            for (int i = 0; i < data.Length; i += 2)
        //            {
        //                datan = data.Substring(i, 2);
        //                tmp = Convert.ToUInt32(datan, 16);
        //                //tmp = BitReverse(tmp, 8);
        //                charValue = (char)(tmp);
        //                value = String.Concat(value, charValue);
        //            }
        //            char[] arrstr = value.ToArray();
        //            value = new string(arrstr);
        //            Headers = String.Concat(Headers, value);
        //            iAddr += 4;
        //            address = iAddr.ToString("x8").TrimStart(new char[] { '0' });


        //        }
        //        return Headers.Replace("\0", "");
        //    }
        //    string flashRegRead_X(int BoardId, string command, string address,int ReadBitLength) //value, read bit number
        //    {
        //        int SPI_Addr = GetFlashSPIAddr(BoardId);
        //        if (!ParamCheck(SPI_Addr, command, address))
        //        {
        //            return null;
        //        }
        //        SPI_Addr = SPI_Addr << 1;
        //        //long RegControlBit = Convert.ToInt64(SPI_Addr.ToString("X") + command + address,16);
        //        BigInteger RegControlBit = BigInteger.Parse(string.Format("0{0}", SPI_Addr.ToString("X")) + command + address, System.Globalization.NumberStyles.AllowHexSpecifier);
        //        int dataBitLength = SPIAddrSize + 1 + command.Length * 4 + address.Length * 4;
        //        int rem = dataBitLength % 8;
        //        if (rem != 0)
        //        {
        //            RegControlBit = RegControlBit << (8 - rem);
        //        }
        //        byte[] byteBuffer = RegControlBit.ToByteArray();
        //        byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];
        //        for (int i = 0; i < InputDataBuffer.Length; i++)
        //        {
        //            InputDataBuffer[InputDataBuffer.Length - i - 1] = byteBuffer[i];
        //        }
        //        byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, ReadBitLength, 1, 1, 0, 0, 0);
        //        string RegData = "";
        //        if (OutputDataBuffer != null)
        //        {
        //            for (int i = 0; i < OutputDataBuffer.Length; i++)
        //            {
        //                RegData += OutputDataBuffer[i].ToString("X2");
        //            }
        //        }

        //        return RegData;
        //    }
        //    #endregion
        //    //====================New flash function for EEPROM===========================
        //    #region flash function for EEPROM
        //    public void ClearFalsh_E(int BoardId, string address)
        //    {
        //        flashRegWrite_E(BoardId, "100", "110000", ""); //WREN
        //        flashRegWrite_E(BoardId, "111", Convert.ToString(Convert.ToInt32(address, 16), 2), "");
        //    }
        //    public void ClearAllEEPROM_E(int BoardId)
        //    {
        //        for(int addr=0;addr<64;addr++)
        //        {
        //            flashRegWrite_E(BoardId, "100", "110000", ""); //WREN
        //            flashRegWrite_E(BoardId, "111", Convert.ToString(addr, 2), "");
        //        }
                
        //    }
        //public void WriteFlashString_E(int BoardId, string address, string stringData)
        //    {
        //        int iAddress = Convert.ToInt32(address, 2);
        //        string outputString;
        //        //stringData = stringData + '\0';
        //        int iTakeUpAddrSize = Convert.ToInt32(Math.Ceiling(stringData.Length / 2.0));
        //        stringData = stringData.PadRight(iTakeUpAddrSize * 2, '\0');
        //        for (int AddrNo = 0; AddrNo < iTakeUpAddrSize; AddrNo++)
        //        {
        //            outputString = stringData.Substring(AddrNo * 2, 2);
        //            char[] values = outputString.ToCharArray();
        //            string hexOutput, ASCDataString = "";
        //            foreach (char letter in values)
        //            {
        //                UInt32 value = Convert.ToUInt32(letter);
        //                //value = BitReverse(value, 8);
        //                hexOutput = Convert.ToString(value, 16).PadLeft(2, '0');
        //                //ASCDataString += hexOutput;
        //                ASCDataString = hexOutput + ASCDataString;
        //            }
        //            WriteFlash_E(BoardId, address, ASCDataString);
        //            iAddress += 1;
        //            address = Convert.ToString(iAddress, 2);
        //        }
        //    }
        //    public void WriteFlash_E(int BoardId, string address, string ASCStringData)
        //    {
        //        flashRegWrite_E(BoardId, "100", "110000", "");
        //        flashRegWrite_E(BoardId, "101", address, ASCStringData);
        //    }
        //    void flashRegWrite_E(int BoardId, string command, string address, string ASCStringData) //Address: data or address + data
        //    {
        //        long RegControlBit;
        //        int dataBitLength;
        //        int SPI_Addr = GetFlashSPIAddr(BoardId);
        //        if (!ParamCheck(SPI_Addr, command, address))
        //        {
        //            return;
        //        }
        //        long head;
        //        head = (SPI_Addr << 1) + 1; //W/R fla
        //        head = (head << 3) + Convert.ToInt64(command, 2);// Opcode
        //        head = (head << 6) + Convert.ToInt64(address, 2);// Adress
        //        if (ASCStringData == "")
        //        {
        //            RegControlBit = head;
        //        dataBitLength = SPIAddrSize + 1 + 1 + 2 + 6;
        //        }
        //        else
        //        {
        //            RegControlBit = (head << 16) + Convert.ToInt64(ASCStringData, 16);// data
        //            dataBitLength = SPIAddrSize + 1 + 1 + 2 + 6 + 16;
        //        }

        //        RegControlBit = RegControlBit << (8 - dataBitLength % 8); // left move 3 bit for byte[] convert
        //        byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];
        //        for (int i = 0; i < InputDataBuffer.Length; i++)
        //        {
        //            InputDataBuffer[InputDataBuffer.Length - i - 1] = (byte)(RegControlBit >> (i * 8));
        //        }
        //        //FTDIClient.Send_data(dataBitLength, InputDataBuffer, 1, 1, 0, 0);
        //        byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, 0, 1, 1, 0, 0, 0);
        //    }

        //    public string ReadFlashString_E(int BoardId, string address)
        //    {
        //        string data, data1, data2, data3;
        //        UInt32 tmp;
        //        char charValue;
        //        string value = "";
        //        int iAddr = Convert.ToInt32(address,2);

        //        while (true)
        //        {
        //            data = flashRegRead_E(BoardId, "110", address);
        //            data1 = data.Substring(2, 2);
        //            data2 = data.Substring(0, 2);
        //            //data3 = data.Substring(4, 2);
        //            if (data.Contains("FF") || data == "")
        //                break;
        //            tmp = Convert.ToUInt32(data1, 16);
        //            //tmp = BitReverse(tmp, 8);
        //            charValue = (char)tmp;
        //            value = String.Concat(value, charValue);

        //            tmp = Convert.ToUInt32(data2, 16);
        //            //tmp = BitReverse(tmp, 8);
        //            charValue = (char)tmp;
        //            value = String.Concat(value, charValue);
        //            iAddr += 1;
        //            address = Convert.ToString(iAddr, 2);
        //            //iAddr += 4;
        //            //address = iAddr.ToString("x8").TrimStart(new char[] { '0' });
        //    }
        //        return value.Replace("\0", "");
        //    }

        //    string flashRegRead_E(int BoardId, string command, string address) //value, read bit number
        //    {
        //        int SPI_Addr = GetFlashSPIAddr(BoardId);
        //        if (!ParamCheck(SPI_Addr, command, address))
        //        {
        //            return null;
        //        }
        //        long RegControlBit = (SPI_Addr << 1) + 1; //W/R fla
        //        RegControlBit = (RegControlBit << 3) + Convert.ToInt64(command, 2);// Opcode
        //        RegControlBit = (RegControlBit << 6) + Convert.ToInt64(address, 2);// Adress
        //        int dataBitLength = SPIAddrSize + 1 + 3 + 6;
        //        RegControlBit = RegControlBit << (8 - dataBitLength % 8);
        //        byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];
        //        for (int i = 0; i < InputDataBuffer.Length; i++)
        //        {
        //            InputDataBuffer[InputDataBuffer.Length - i - 1] = (byte)(RegControlBit >> (i * 8));
        //        }
        //        byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, 16, 1, 1, 0, 0, 0);
        //        string RegData = "";
        //        if (OutputDataBuffer != null)
        //        {
        //            for (int i = 0; i < OutputDataBuffer.Length; i++)
        //            {
        //                RegData += OutputDataBuffer[i].ToString("X2");
        //        }
        //        }
        //        return RegData;
        //    }
        //    #endregion
        //#endregion
    }
    public class FTDIClient
    {    
        private static FTDIServiceClient client;
        private static byte mClock;
        private static int mSPI;
        private static int FTDIClientNumber=0;
        public static bool connect()
        {
            int FTDI_Status;
            do
            {
                client = new FTDIServiceClient();
                //FTDIClientNumber++;
                System.Threading.Thread.Sleep(200);
            }
            while (!tryConnect(client));//wait until the server is online  
            FTDIClientNumber = Convert.ToInt16(File.ReadAllText("C:\\TEMP\\FTDI_Client.txt"));
            File.WriteAllText("C:\\TEMP\\FTDI_Client.txt", (++FTDIClientNumber).ToString());
            FTDI_Status = Convert.ToInt16(File.ReadAllText("C:\\TEMP\\FTDI_Status.txt"));
            if (FTDI_Status==1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
    
        }
        public static bool  tryConnect(FTDIServiceClient clientA)
        {
            bool Status = false;
            try
            {
                clientA.Open();
                Status = true;
            }
            catch (Exception e)
            {
                Status = false;
            }
            return Status;
        }
        public static void disconnect()
        {            
            
            //if (File.ReadAllText("C:\\TEMP\\FTDI_Client.txt") != "-99")
            //{
                client.Close();
                FTDIClientNumber = Convert.ToInt16(File.ReadAllText("C:\\TEMP\\FTDI_Client.txt"));
                File.WriteAllText("C:\\TEMP\\FTDI_Client.txt", (--FTDIClientNumber).ToString()); 
            //}
            
            if(FTDIClientNumber<=0)
            {
                Process[] processes = Process.GetProcessesByName("FTDIService");
                if (processes.Length != 0)
                {
                    processes[0].Kill();
                }
            }

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
