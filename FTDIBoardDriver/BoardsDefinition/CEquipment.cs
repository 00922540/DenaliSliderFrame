using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BoardDriver
{
    public class CEquipment
    {
        public int SPIAddrSize = 10;
        public const int FPGA_ADDR_BIT_LENGTH = 8;
        public const int READ_WRITE_FLAG_BIT_LENGTH = 1;
        public const int LO_WYVERN_REGADDR_LENGTH = 13;
        public const int ADDED_CLOCK = 0;
        public const int MSB = 1;
        public const int CS = 0;
        public const int VE = 1;
        public const int LO_WYVERN_WRITE_DATA_SIZE = 16;
        public const int LO_WYVERN_READ_DATA_SIZE = 32;

        public FTDIBoardDriver Driver;
        public string BoardName = "";
        public string EquipmentName = "";
        public int BoardID = 0;
        public int Addr;
        public int Size;
        public CEquipment(int iAddr, int iSize)
        {
            Addr = iAddr*4;
            Size = iSize;
        }
        public CEquipment(int iAddr, int iSize,int SPIAddrLength)
        {
            this.SPIAddrSize = SPIAddrLength;
            if (SPIAddrLength == 10)
                Addr = iAddr * 4;
            else
                Addr = iAddr;
            Size = iSize;
        }
        public void ReSetBits(long Value)
        {
            long Data = this.Driver.Read(EquipmentName, BoardName);
            Data = Data | Value;
            this.Driver.Write(EquipmentName, BoardName, Value);
        }
        public void Write(long data)
        {
            string SPIContol = "";
            //this.Driver.Write(EquipmentName, BoardName, Value);
            int addedClock = ADDED_CLOCK;
            int ReadWrite = 1;
            int RegControlBitLength = SPIAddrSize + 1 + SPIContol.Length;
            int RegDataBitLength = GetDataSize(EquipmentName, ReadWrite, SPIContol.Length);
            long RegControlBit = GetControlData(Addr, ReadWrite, SPIContol);
            Driver.Write_Data(data, RegDataBitLength, RegControlBit, RegControlBitLength, addedClock);
        }
        public void Write(string sData)
        {
            long data = 0;
            try
            {
                data = Convert.ToInt32(sData, 16);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "CEquipmeng Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
            }
            string SPIContol = "";
            //this.Driver.Write(EquipmentName, BoardName, Value);
            int addedClock = ADDED_CLOCK;
            int ReadWrite = 1;
            int RegControlBitLength = SPIAddrSize + 1 + SPIContol.Length;
            int RegDataBitLength = GetDataSize(EquipmentName, ReadWrite, SPIContol.Length);
            long RegControlBit = GetControlData(Addr, ReadWrite, SPIContol);
            Driver.Write_Data(data, RegDataBitLength, RegControlBit, RegControlBitLength, addedClock);
        }
        public long Read()
        {
            string SPIContol = "";
            int ReadWrite = 0;
            int addedClock = ADDED_CLOCK;
            int ReadVe = VE;
            int RegControlBitLength = SPIAddrSize + 1 + SPIContol.Length;
            int RegDataBitLength = GetDataSize(EquipmentName, ReadWrite, SPIContol.Length);
            long RegControl = GetControlData(Addr, ReadWrite, SPIContol);
            long data = Driver.Read_Data(RegDataBitLength, RegControl, RegControlBitLength, addedClock, ReadVe);
            return data;
        }

        public void WriteBits(int bitSetPosition, int bitsSetLength, long bitsSetValue)
        {
            long value = Read();
            value = SetBitsValue(value, Size, bitSetPosition, bitsSetLength, bitsSetValue);
            Write(value);
        }
        public long ReadBits(int bitSetPosition, int bitsSetLength)
        {
            long data = Read();
            return GetBitsValue(data, Size, bitSetPosition, bitsSetLength);
        }


        public void WriteDAC(string Command, string Addr, string Value)
        {
            string sValue = Command + Addr + Value;
            int val = Convert.ToInt32(sValue, 2);
            Write( Convert.ToInt32(sValue, 2));
        }
        public int GetDataSize(string RegName, int ReadWriteFlag, int controlBitLength)
        {
            int RegDataBitLength = Size - controlBitLength; ;
            switch (RegName)
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
        private long SetBitsValue(long OriginalValue, int totalBitLength, int bitSetPosition, int bitsSetLength, long bitsSetValue)
        {
            int tempCodeA = (int)Math.Pow(2, totalBitLength - bitSetPosition) - 1;
            tempCodeA = tempCodeA << bitSetPosition;
            int tempCodeB = (int)Math.Pow(2, bitSetPosition - bitsSetLength) - 1;
            int zeroCode = tempCodeA | tempCodeB;
            OriginalValue = OriginalValue & zeroCode;
            bitsSetValue = bitsSetValue << (bitSetPosition - bitsSetLength);
            OriginalValue = OriginalValue | bitsSetValue;
            return OriginalValue;
        }
        private long GetBitsValue(long OriginalValue, int totalBitLength, int bitSetPosition, int bitsSetLength)
        {
            string sData = Convert.ToString(OriginalValue, 2).PadLeft(totalBitLength, '0');
            return Convert.ToInt32(sData.Substring(sData.Length - bitSetPosition - 1, bitsSetLength), 2);
        }
    }
}
