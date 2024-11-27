using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Numerics;
using System.Windows.Forms;
namespace BoardDriver
{
    public class CBoardBase
    {
        public int ID;
        public FTDIBoardDriver Driver;
        public string BoardName = "";
        public int SPIAddrSize = 10;
        public int FlashSPIAddr;
        public virtual long BoardFpgaVersion()
        {
            return 0;
        }

        bool ParamCheck(int SPI_Addr, string command, string address)
        {
            if (SPI_Addr == 0x200) //EEPROM
            {
                int addr = Convert.ToInt32(address, 2);
                if (addr > 128)
                {
                    MessageBox.Show("The address can't be operation!");
                    return false;
                }
                else if (address.Length > 8)
                {
                    MessageBox.Show("Address length error!");
                    return false;
                }
                else if (command.Length != 3 || command == null)
                {
                    MessageBox.Show("Command length error!");
                    return false;
                }
                return true;
            }
            else
            {
                if (command != "05" && command != "06")
                {
                    int addr = Convert.ToInt32(address, 16);
                    if (addr < 0x300000)
                    {
                        MessageBox.Show("The address can't be operation!");
                        return false;
                    }
                    else if (address.Length > 6)
                    {
                        MessageBox.Show("Address length error!");
                        return false;
                    }
                    else if (command.Length != 2 || command == null)
                    {
                        MessageBox.Show("Command length error!");
                        return false;
                    }
                }
                return true;
            }

            return true;
        }

        // FPGA FLsah Operator
        public bool WriteFalsh(string offsetAddr, string data)
        {
            bool rs = false;
            int offset = Convert.ToInt32(offsetAddr, 16);
            string address;
            switch (ID)
            {
                case 29:
                    address = Convert.ToString(offset, 2);
                    WriteFlashString_E(address, data);
                    break;
                case 15:
                case 27:
                case 23:
                case 30:
                case 31:
                    address = Convert.ToString(0x300000 + offset, 16);
                    WriteFlashString_X(address, data);
                    break;

            }
            return rs;
        }
        public string ReadFlash(string offsetAddr)
        {
            string rs = "";
            int offset = Convert.ToInt32(offsetAddr, 16);
            string address;
            switch (ID)
            {
                case 29:
                    address = Convert.ToString(offset, 2);
                    rs = ReadFlashString_E(address);
                    break;
                case 15:
                case 27:
                case 23:
                case 30:
                case 31: //debug
                    address = Convert.ToString(0x300000 + offset, 16);
                    rs = ReadFlashString_X(address);
                    break;

            }
            return rs;
        }
        public void ClearFlash(string offsetAddr)
        {
            int offset = Convert.ToInt32(offsetAddr, 16);
            string address;
            switch (ID)
            {
                case 29:
                    address = Convert.ToString(offset, 2);
                    ClearAll_E();
                    break;
                case 15:
                case 27:
                case 23:
                case 30:
                case 31:
                    address = Convert.ToString(0x300000 + offset, 16);
                    ClearFalsh_X(address);
                    break;
            }
        }

        #region Xlinx FPGA FLsah Operator
        public void WriteFlashString_X(string address, string stringData)
        {
            int iAddress = Convert.ToInt32(address, 16);
            string outputString;
            //stringData = stringData + '\0';
            int iTakeUpAddrSize = Convert.ToInt32(Math.Ceiling(stringData.Length / 4.0));
            stringData = stringData.PadRight(iTakeUpAddrSize * 4, '\0');
            for (int AddrNo = 0; AddrNo < iTakeUpAddrSize; AddrNo++)
            {
                outputString = stringData.Substring(AddrNo * 4, 4);
                char[] values = outputString.ToCharArray();
                string hexOutput, ASCDataString = "";
                foreach (char letter in values)
                {
                    UInt32 value = Convert.ToUInt32(letter);
                    hexOutput = Convert.ToString(value, 16).PadLeft(2, '0');
                    ASCDataString += hexOutput;
                }
                WriteFlash_X(address, ASCDataString);
                iAddress += 4;
                address = iAddress.ToString("x8").TrimStart(new char[] { '0' });
            }
        }
        public void WriteFlash_X(string address, string ASCStringData)
        {
            string ret = flashRegRead_X("05", "", 8);
            flashRegWrite_X("06", "", "");
            ret = flashRegRead_X("05", "", 8);
            //while (ret != "02")
            //{
            //    ret = flashRegRead_X("05", "", 8);
            //}
            flashRegWrite_X("02", address, ASCStringData); //PP
            ret = flashRegRead_X("05", "", 8);
            //while (ret != "02")
            //{
            //    ret = flashRegRead_X("05", "", 8);
            //}
        }
        void flashRegWrite_X(string command, string address, string ASCStringData) //Address: data or address + data
        {
            if (!ParamCheck(FlashSPIAddr, command, address))
            {
                return;
            }
            FlashSPIAddr = FlashSPIAddr << 1;
            BigInteger RegControlBit = BigInteger.Parse(string.Format("0{0}", FlashSPIAddr.ToString("X")) + command + address + ASCStringData, System.Globalization.NumberStyles.AllowHexSpecifier);
            int dataBitLength = SPIAddrSize + 1 + command.Length * 4 + address.Length * 4 + ASCStringData.Length * 4;//9 = SPI addr(8 bits) + Read/Write(1 bit)
            int rem = dataBitLength % 8;
            if (rem != 0)
            {
                RegControlBit = RegControlBit << (8 - rem);
            }
            byte[] byteBuffer = RegControlBit.ToByteArray();
            byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];

            for (int i = 0; i < InputDataBuffer.Length; i++)
            {
                InputDataBuffer[InputDataBuffer.Length - i - 1] = byteBuffer[i];
            }
            byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, 0, 1, 1, 0, 0, 0);

        }


        public string ReadFlashString_X(string address)
        {
            string data, datan;
            UInt32 tmp;
            char charValue;
            string value = null, Headers = "";
            BigInteger iAddr = BigInteger.Parse(string.Format("0{0}", address), System.Globalization.NumberStyles.AllowHexSpecifier);
            while (true)
            {
                value = null;
                data = flashRegRead_X("03", address, 32);
                if (data.Contains("FF") || data == "")
                    break;

                for (int i = 0; i < data.Length; i += 2)
                {
                    datan = data.Substring(i, 2);
                    tmp = Convert.ToUInt32(datan, 16);
                    charValue = (char)(tmp);
                    value = String.Concat(value, charValue);
                }
                char[] arrstr = value.ToArray();
                value = new string(arrstr);
                Headers = String.Concat(Headers, value);
                iAddr += 4;
                address = iAddr.ToString("x8").TrimStart(new char[] { '0' });
            }
            return Headers.Replace("\0", "");
        }
        string flashRegRead_X(string command, string address, int ReadBitLength) //value, read bit number
        {
            //int FlashSPIAddr = GetFlashSPIAddr(ID);
            //if (!ParamCheck(FlashSPIAddr, command, address))
            //{
            //    return null;
            //}
            //FlashSPIAddr = FlashSPIAddr << 1;
            ////long RegControlBit = Convert.ToInt64(FlashSPIAddr.ToString("X") + command + address,16);
            //BigInteger RegControlBit = BigInteger.Parse(string.Format("0{0}", FlashSPIAddr.ToString("X")) + command + address, System.Globalization.NumberStyles.AllowHexSpecifier);
            //int dataBitLength = SPIAddrSize + 1 + command.Length * 4 + address.Length * 4;
            //int rem = dataBitLength % 8;
            //if (rem != 0)
            //{
            //    RegControlBit = RegControlBit << (8 - rem);
            //}
            //byte[] byteBuffer = RegControlBit.ToByteArray();
            //byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];
            //for (int i = 0; i < InputDataBuffer.Length; i++)
            //{
            //    InputDataBuffer[InputDataBuffer.Length - i - 1] = byteBuffer[i];
            //}
            //byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, ReadBitLength, 1, 1, 0, 0, 0);
            //string RegData = "";
            //if (OutputDataBuffer != null)
            //{
            //    for (int i = 0; i < OutputDataBuffer.Length; i++)
            //    {
            //        RegData += OutputDataBuffer[i].ToString("X2");
            //    }
            //}

            //return RegData;
            return "";
        }


        public void ClearFalsh_X(string address)
        {
            string ret = flashRegRead_X("05", "", 8);//READ STATUS REG1
            ret = flashRegRead_X("05", "", 8);
            flashRegWrite_X("06", "", ""); //WREN
            ret = flashRegRead_X("05", "", 8);
            while (ret != "02")
            {
                ret = flashRegRead_X("05", "", 8);
            }
            flashRegWrite_X("D8", address, "");//SE 

            ret = flashRegRead_X("05", "", 8);
            while (ret != "00")
            {
                ret = flashRegRead_X("05", "", 8);
            }
        }
        #endregion

        #region EEPROM Operator
        public void WriteFlashString_E(string address, string stringData)
        {
            int iAddress = Convert.ToInt32(address, 2);
            string outputString;
            //stringData = stringData + '\0';
            int iTakeUpAddrSize = Convert.ToInt32(Math.Ceiling(stringData.Length / 2.0));
            stringData = stringData.PadRight(iTakeUpAddrSize * 2, '\0');
            for (int AddrNo = 0; AddrNo < iTakeUpAddrSize; AddrNo++)
            {
                outputString = stringData.Substring(AddrNo * 2, 2);
                char[] values = outputString.ToCharArray();
                string hexOutput, ASCDataString = "";
                foreach (char letter in values)
                {
                    UInt32 value = Convert.ToUInt32(letter);
                    //value = BitReverse(value, 8);
                    hexOutput = Convert.ToString(value, 16).PadLeft(2, '0');
                    //ASCDataString += hexOutput;
                    ASCDataString = hexOutput + ASCDataString;
                }
                WriteFlash_E(address, ASCDataString);
                iAddress += 1;
                address = Convert.ToString(iAddress, 2);
            }
        }
        public void WriteFlash_E(string address, string ASCStringData)
        {
            flashRegWrite_E("100", "110000", "");
            flashRegWrite_E("101", address, ASCStringData);
        }
        void flashRegWrite_E(string command, string address, string ASCStringData) //Address: data or address + data
        {
            //long RegControlBit;
            //int dataBitLength;
            //int FlashSPIAddr = GetFlashSPIAddr(ID);
            //if (!ParamCheck(FlashSPIAddr, command, address))
            //{
            //    return;
            //}
            //long head;
            //head = (FlashSPIAddr << 1) + 1; //W/R fla
            //head = (head << 3) + Convert.ToInt64(command, 2);// Opcode
            //head = (head << 6) + Convert.ToInt64(address, 2);// Adress
            //if (ASCStringData == "")
            //{
            //    RegControlBit = head;
            //    dataBitLength = SPIAddrSize + 1 + 1 + 2 + 6;
            //}
            //else
            //{
            //    RegControlBit = (head << 16) + Convert.ToInt64(ASCStringData, 16);// data
            //    dataBitLength = SPIAddrSize + 1 + 1 + 2 + 6 + 16;
            //}

            //RegControlBit = RegControlBit << (8 - dataBitLength % 8); // left move 3 bit for byte[] convert
            //byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];
            //for (int i = 0; i < InputDataBuffer.Length; i++)
            //{
            //    InputDataBuffer[InputDataBuffer.Length - i - 1] = (byte)(RegControlBit >> (i * 8));
            //}
            ////FTDIClient.Send_data(dataBitLength, InputDataBuffer, 1, 1, 0, 0);
            //byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, 0, 1, 1, 0, 0, 0);
        }


        public string ReadFlashString_E(string address)
        {
            string data, data1, data2, data3;
            UInt32 tmp;
            char charValue;
            string value = "";
            int iAddr = Convert.ToInt32(address, 2);

            while (true)
            {
                data = flashRegRead_E("110", address);
                data1 = data.Substring(2, 2);
                data2 = data.Substring(0, 2);
                //data3 = data.Substring(4, 2);
                if (data.Contains("FF") || data == "")
                    break;
                tmp = Convert.ToUInt32(data1, 16);
                //tmp = BitReverse(tmp, 8);
                charValue = (char)tmp;
                value = String.Concat(value, charValue);

                tmp = Convert.ToUInt32(data2, 16);
                //tmp = BitReverse(tmp, 8);
                charValue = (char)tmp;
                value = String.Concat(value, charValue);
                iAddr += 1;
                address = Convert.ToString(iAddr, 2);
                //iAddr += 4;
                //address = iAddr.ToString("x8").TrimStart(new char[] { '0' });
            }
            return value.Replace("\0", "");
        }
        string flashRegRead_E(string command, string address) //value, read bit number
        {
            //int FlashSPIAddr = GetFlashSPIAddr(ID);
            //if (!ParamCheck(FlashSPIAddr, command, address))
            //{
            //    return null;
            //}
            //long RegControlBit = (FlashSPIAddr << 1) + 1; //W/R fla
            //RegControlBit = (RegControlBit << 3) + Convert.ToInt64(command, 2);// Opcode
            //RegControlBit = (RegControlBit << 6) + Convert.ToInt64(address, 2);// Adress
            //int dataBitLength = SPIAddrSize + 1 + 3 + 6;
            //RegControlBit = RegControlBit << (8 - dataBitLength % 8);
            //byte[] InputDataBuffer = new byte[(int)Math.Ceiling(dataBitLength / 8.0)];
            //for (int i = 0; i < InputDataBuffer.Length; i++)
            //{
            //    InputDataBuffer[InputDataBuffer.Length - i - 1] = (byte)(RegControlBit >> (i * 8));
            //}
            //byte[] OutputDataBuffer = FTDIClient.Read_data(dataBitLength, InputDataBuffer, 16, 1, 1, 0, 0, 0);
            //string RegData = "";
            //if (OutputDataBuffer != null)
            //{
            //    for (int i = 0; i < OutputDataBuffer.Length; i++)
            //    {
            //        RegData += OutputDataBuffer[i].ToString("X2");
            //    }
            //}
            //return RegData;
            return "";
        }

        public void ClearAll_E()
        {
            for (int addr = 0; addr < 64; addr++)
            {
                flashRegWrite_E("100", "110000", ""); //WREN
                flashRegWrite_E("111", Convert.ToString(addr, 2), "");
            }

        }
        #endregion

        #region DAC
        /// <summary>
        /// LTC2666 8通道 16/12 bit数模转换器
        /// 其中一部分通道用于 可变增益放大器 LMH6401 V0CM 控制
        /// 其中一部分通道用于 全差分放大器   LMH5401 V0CM 控制
        /// 其中一部分通道用于 解调器   LTC5594 VCM 控制
        /// </summary>
        /// <param name="Voltage"></param>
        /// <returns></returns>
        public string DAC_LTC2666_Cal(double Voltage)
        {
            string Hex;
            long DAC;
            int N = 16;
            double Gain = 4;
            double Vref = 2.5;
            DAC = (long)(((Voltage + Vref * Gain) * (Math.Pow(2, N))) / (2 * Vref * Gain));
            Hex = Convert.ToString(DAC, 16);
            return Hex;
        }
        #endregion
    }
}
