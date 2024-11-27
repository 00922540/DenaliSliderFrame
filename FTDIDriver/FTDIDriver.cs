using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTD2XX_NET;
using System.Windows.Forms;
using System.Threading;

namespace FTDIService
{
    public class FTDIDriver
    {
        private FTDI myFtdiDevice;
        //private static FTDIDriver ftdiDriver;
        public FTDIDriver()
        {
            myFtdiDevice = new FTDI();
        }
        //public static FTDIDriver getInstance()
        //{
        //    if (ftdiDriver == null)
        //    {
        //        ftdiDriver = new FTDIDriver();
        //    }
        //    return ftdiDriver;
        //}
        public static bool errorPompted = false;//当设备读写失败时只显示一次对话框
        private Byte[] byOutputBuffer = new Byte[34816]; //34k
        private Byte[] byInputBuffer = new Byte[32];
        private UInt32 dwNumBytesToSend = 0;
        private UInt32 dwNumBytesSent = 0;
        private UInt32 dwNumBytesToRead = 0;
        private UInt32 dwNumBytesRead = 0;
        #region ConstValue
        public const int Fail = 1;
        public const int OK = 0;
        #endregion

        public int Init_FDTIDevice(byte Clock_select, int SPI)
        {
            //Clock_select is the number of main clock devided by 
            //SPI is the SPI Port number we use to connect our device, generally we use the first port 0
            int pass = 1, fail = 0;
            UInt32 ftdiDeviceCount = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;




            // Create new instance of the FTDI device class
            //FTDI myFtdiDevice = new FTDI();
            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

            // Check status
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                //MessageBox.Show("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                // Console.WriteLine("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                // Console.WriteLine("");
            }
            else
            {
                // Wait for a key press
                MessageBox.Show("Failed to get number of devices (error " + ftStatus.ToString() + ")", "FTDI initialize failed");
                //Console.WriteLine("Failed to get number of devices (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return fail;
            }

            // If no devices available, return
            if (ftdiDeviceCount == 0)
            {
                // Wait for a key press
                //MessageBox.Show("Failed to get number of devices (error " + ftStatus.ToString() + ")");
                if (!errorPompted)
                {
                    MessageBox.Show("Number of FTDI devices:0", "FTDI initialize failed");
                    errorPompted = true;
                }
                //Console.WriteLine("Failed to get number of devices (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return fail;
            }

            // Allocate storage for device info list
            FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

            // Populate our device list
            ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                for (UInt32 i = 0; i < ftdiDeviceCount; i++)
                {

                    /*
                    Console.WriteLine("Device Index: " + i.ToString());
                    Console.WriteLine("Flags: " + String.Format("{0:x}", ftdiDeviceList[i].Flags));
                    Console.WriteLine("Type: " + ftdiDeviceList[i].Type.ToString());
                    Console.WriteLine("ID: " + String.Format("{0:x}", ftdiDeviceList[i].ID));
                    Console.WriteLine("Location ID: " + String.Format("{0:x}", ftdiDeviceList[i].LocId));
                    Console.WriteLine("Serial Number: " + ftdiDeviceList[i].SerialNumber.ToString());
                    Console.WriteLine("Description: " + ftdiDeviceList[i].Description.ToString());
                    Console.WriteLine("");
                    */
                }
            }


            // Open first device in our list by serial number SPI=0 or 1

            ftStatus = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[SPI].SerialNumber);

            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                if (!errorPompted)
                {
                    MessageBox.Show("Failed to open device (error " + ftStatus.ToString() + "), please restart program and try!");
                    errorPompted = true;
                }
                //Console.WriteLine("Failed to open device (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return fail;
            }

            #region set configration of FTDI
            //Add by Carlles
            //Reset USB Device
            ftStatus |= myFtdiDevice.ResetDevice();
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                MessageBox.Show("ResetDevice Fail");
                return fail;
            }
            //Purge USB receive buffer first by reading out all old data from device receie buffer
            ftStatus |= myFtdiDevice.GetRxBytesAvailable(ref dwNumBytesToRead);//Get the number of bytes in the device receive buffer
            if ((ftStatus == FTDI.FT_STATUS.FT_OK) && (dwNumBytesToRead > 0))
                myFtdiDevice.Read(byInputBuffer, dwNumBytesToRead, ref dwNumBytesRead);//Read out the data from device receive buffer

            ftStatus |= myFtdiDevice.InTransferSize(65536);//Set USB request transfer sizes to 64K
            ftStatus |= myFtdiDevice.SetCharacters(0, false, 0, false);//Disable event and error characters
            ftStatus |= myFtdiDevice.SetTimeouts(1000, 500000);//Set the read and write timeouts in miliseconds
            ftStatus |= myFtdiDevice.SetLatency(1);//Set the latency timer to 1mS(default is 16mS)
            ftStatus |= myFtdiDevice.SetFlowControl(0x0100, 0x00, 0x00);//Ture on flow control to synchronize IN requests
            ftStatus |= myFtdiDevice.SetBitMode(0x00, 0x00);//Reeset Controller
            ftStatus |= myFtdiDevice.SetBitMode(0xFB, 0x02);//Enable MPSSE Mode

            // Set to MPSSE mode
            //            ftStatus = myFtdiDevice.SetBitMode(0xff, 0x2);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                MessageBox.Show("Failed to set MPSSE mode (error " + ftStatus.ToString() + ")");
                return fail;
            }
            Thread.Sleep(50);//Wait for all USB stuff to complete and work

            //Configure the FTDI MPSSE
            //MPSSE Setup
            dwNumBytesToSend = 0;
            byOutputBuffer[dwNumBytesToSend++] = 0x8A;//Use 60MHz master clock(disable divide by 5)
            byOutputBuffer[dwNumBytesToSend++] = 0x97;//Turn off adptive clocking(may be need for ARM)
            byOutputBuffer[dwNumBytesToSend++] = 0x8D;//Disable three-phase clocking
            ftStatus = myFtdiDevice.Write(byOutputBuffer, dwNumBytesToSend, ref dwNumBytesSent);//Send off the HS-specific commands
            dwNumBytesToSend = 0;//Reset output buffer pointer
            byOutputBuffer[dwNumBytesToSend++] = 0x86;//Command to set clock divisor
            byOutputBuffer[dwNumBytesToSend++] = Clock_select;//Set 0xValueL of clock divisor
            byOutputBuffer[dwNumBytesToSend++] = 0x0;//Set 0xValueH of clock divisor
            ftStatus = myFtdiDevice.Write(byOutputBuffer, dwNumBytesToSend, ref dwNumBytesSent);//Send off the clock divisor commands
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                MessageBox.Show("Failed to Configure the FTDI MPSSE");
                return fail;
            }
            #endregion
            return pass;
        }
        public int FDTI_close()
        {
            int pass = 1, fail = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
            ftStatus = myFtdiDevice.Close();


            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                //MessageBox.Show("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                // Console.WriteLine("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                // Console.WriteLine("");
                return pass;
            }
            else
            {
                // Wait for a key press
                if (!errorPompted)
                {
                    MessageBox.Show("FDTI close fail");
                    errorPompted = true;
                }
                //Console.WriteLine("Failed to get number of devices (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return fail;
            }
        }

        public int FDTI_close(bool bCatchException)
        {
            int pass = 1, fail = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
            ftStatus = myFtdiDevice.Close();
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                //MessageBox.Show("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                // Console.WriteLine("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                // Console.WriteLine("");
                return pass;
            }
            else
            {
                // Wait for a key press
                //MessageBox.Show("FDTI close fail");
                //throw new Exception("FDTI close fail");
                //Console.WriteLine("Failed to get number of devices (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return fail;
            }
        }

        public int Send_data(int bit_length, byte[] buffer, int VE, int MSB, int CS, int Add_clock)
        {


            int pass = 1, fail = 0;
            int byte_number, bit_number;
            int j;
            int dwNumBytesTosend = 0;
            int head_length = 0;
            byte[] InputBuffer = new byte[buffer.Length + 30];
            byte_number = bit_length / 8;
            bit_number = bit_length % 8;
            j = byte_number;

            for (int i = 0; i < InputBuffer.Length; i++)
            {
                InputBuffer[i] = 0;
            }
            ///add clock if needed before CS enable
            ///
            if (Add_clock != 0)
            {
                InputBuffer[dwNumBytesTosend++] = 0x13;
                InputBuffer[dwNumBytesTosend++] = (byte)(Add_clock - 1);
                InputBuffer[dwNumBytesTosend++] = 0;
            }

            //Step1: valid cs
            // dwNumBytesTosend=0
            InputBuffer[dwNumBytesTosend++] = 0x80;

            // dwNumBytesTosend=1
            if (CS == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x01;
            else if (CS == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0;
            else if (CS == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x0F;
            else if (CS == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0E;
            else InputBuffer[dwNumBytesTosend++] = 0x0E;

            // dwNumBytesTosend=2
            InputBuffer[dwNumBytesTosend++] = 0x0B;

            //Step2: Enable CS
            //dwNumBytesTosend=3
            InputBuffer[dwNumBytesTosend++] = 0x80;

            // dwNumBytesTosend=4
            if (CS == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x0F;
            else if (CS == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0E;
            else if (CS == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x01;
            else if (CS == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x00;
            else InputBuffer[dwNumBytesTosend++] = 0x00;

            // dwNumBytesTosend=5
            InputBuffer[dwNumBytesTosend++] = 0x0B;

            //Step3: config MSB/LSB 
            // dwNumBytesTosend=6

            if (byte_number != 0)
            {
                if (MSB == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x10;
                else if (MSB == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x11;
                else if (MSB == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x18;
                else if (MSB == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x19;
                else InputBuffer[dwNumBytesTosend++] = 0x19;

                // dwNumBytesTosend=7 and 8, the send_data length
                InputBuffer[dwNumBytesTosend++] = (byte)(byte_number - 1);
                InputBuffer[dwNumBytesTosend++] = (byte)((byte_number - 1) >> 8);

                head_length = dwNumBytesTosend;
                //Step4: Send data
                // dwNumBytesTosend= 9 to 9+byte_number, put buffer value to inputbuffer
                while (j > 0)
                {
                    //InputBuffer[dwNumBytesTosend] = buffer[dwNumBytesTosend - 9]; // no clock add
                    InputBuffer[dwNumBytesTosend] = buffer[dwNumBytesTosend - head_length];
                    j--;
                    dwNumBytesTosend++;
                }
            }

            // dwNumBytesTosend 10+byte_number, may not exist, put the left bit_number into inputbuffer
            if (bit_number != 0)
            {
                if (MSB == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x12;
                else if (MSB == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x13;
                else if (MSB == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x1A;
                else if (MSB == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x1B;
                else InputBuffer[dwNumBytesTosend++] = 0x1B;


                InputBuffer[dwNumBytesTosend++] = (byte)(bit_number - 1);
                if (byte_number == 0)
                { // Send data < 8 bit, directly write data in bit, no byte write as up
                    //InputBuffer[dwNumBytesTosend] = buffer[dwNumBytesTosend - 8];// no clock add
                    InputBuffer[dwNumBytesTosend] = buffer[0];
                    dwNumBytesTosend++;
                }
                else
                { // Send data > 8 bit, need write the left data as bit
                    // InputBuffer[dwNumBytesTosend] = buffer[dwNumBytesTosend - 11];// no clock add
                    InputBuffer[dwNumBytesTosend] = buffer[dwNumBytesTosend - head_length - 2];
                    dwNumBytesTosend++;
                }
                //  dwNumBytesTosend++;
            }

            //Step5: disenable CS
            InputBuffer[dwNumBytesTosend++] = 0x80;

            if (CS == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x01;
            else if (CS == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0;
            else if (CS == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x0F;
            else if (CS == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0E;
            else InputBuffer[dwNumBytesTosend++] = 0x0E;

            InputBuffer[dwNumBytesTosend++] = 0x0B;
            InputBuffer[dwNumBytesTosend++] = 0x87;

            ///add clock if needed after CS disabled
            ///
            if (Add_clock != 0)
            {
                InputBuffer[dwNumBytesTosend++] = 0x13;
                InputBuffer[dwNumBytesTosend++] = (byte)(Add_clock - 1);
                InputBuffer[dwNumBytesTosend++] = 0;
            }
            ///add clock end


            //FTDI myFtdiDevice = new FTDI();
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
            UInt32 numBytesWritten = 0;
            numBytesWritten = 0;
            ftStatus = myFtdiDevice.Write(InputBuffer, dwNumBytesTosend, ref numBytesWritten);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                if (!errorPompted)
                {
                    MessageBox.Show("Failed to write data to device (error " + ftStatus.ToString() + ")");
                    errorPompted = true;
                }
                //Console.WriteLine("Failed to write data to device (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return fail;
            }
            return pass;
        }

        public byte[] Read_data(int Write_buffer_bit_length, byte[] Write_buffer, int Read_bit_length, int VE, int MSB, int CS, int Read_VE, int Add_clock)
        {
            //Only for ZHB test, 0xa0, 7 bits for Write_buffer
            //byte[] buffer = { 0x80, 0x00, 0x0B, 0x80, 0x0E, 0x0B, 0x13, 0x06, Write_buffer[0], 0x26, 0x07, 0x26, 0x07, 0x26, 0x07, 0x80, 0x00, 0x0B, 0x87 };
            //ZHB test end

            int byte_number, bit_number;
            int j;
            int dwNumBytesTosend = 0;
            int head_length = 0;
            byte[] InputBuffer = new byte[Write_buffer.Length + 30];
            byte_number = Write_buffer_bit_length / 8;
            bit_number = Write_buffer_bit_length % 8;
            j = byte_number;

            ///add clock if needed before CS enable
            ///
            if (Add_clock != 0)
            {
                InputBuffer[dwNumBytesTosend++] = 0x13;
                InputBuffer[dwNumBytesTosend++] = (byte)(Add_clock - 1);
                InputBuffer[dwNumBytesTosend++] = 0;
            }
            ///add clock end

            //Step1: valid cs
            // dwNumBytesTosend=0
            InputBuffer[dwNumBytesTosend++] = 0x80;

            // dwNumBytesTosend=1
            if (CS == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x01;
            else if (CS == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0;
            else if (CS == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x0F;
            else if (CS == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0E;
            else InputBuffer[dwNumBytesTosend++] = 0x0E;

            // dwNumBytesTosend=2
            InputBuffer[dwNumBytesTosend++] = 0x0B;

            //Step2: Enable CS
            //dwNumBytesTosend=3
            InputBuffer[dwNumBytesTosend++] = 0x80;

            // dwNumBytesTosend=4
            if (CS == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x0F;
            else if (CS == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0E;
            else if (CS == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x01;
            else if (CS == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x00;
            else InputBuffer[dwNumBytesTosend++] = 0x00;

            // dwNumBytesTosend=5
            InputBuffer[dwNumBytesTosend++] = 0x0B;

            //Step3: config MSB/LSB and clock in 
            // dwNumBytesTosend=6
            if (byte_number != 0)
            {
                if (MSB == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x10;
                else if (MSB == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x11;
                else if (MSB == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x18;
                else if (MSB == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x19;
                else InputBuffer[dwNumBytesTosend++] = 0x19;
                // dwNumBytesTosend=7 and 8, the send_data length
                InputBuffer[dwNumBytesTosend++] = (byte)(byte_number - 1);
                InputBuffer[dwNumBytesTosend++] = (byte)((byte_number - 1) >> 8);

                head_length = dwNumBytesTosend;
                //Step4: Send data
                // dwNumBytesTosend= 9 to 9+byte_number, put buffer value to inputbuffer
                while (j > 0)
                {
                    InputBuffer[dwNumBytesTosend] = Write_buffer[dwNumBytesTosend - head_length]; //
                    j--;
                    dwNumBytesTosend++;
                }
            }

            // dwNumBytesTosend 10+byte_number, may not exist, put the left bit_number into inputbuffer
            if (bit_number != 0)
            {
                if (MSB == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x12;
                else if (MSB == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x13;
                else if (MSB == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x1A;
                else if (MSB == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x1B;
                else InputBuffer[dwNumBytesTosend++] = 0x1B;

                InputBuffer[dwNumBytesTosend++] = (byte)(bit_number - 1);
                if (byte_number == 0)
                { // Send data < 8 bit, directly write data in bit, no byte write as up
                    InputBuffer[dwNumBytesTosend] = Write_buffer[0];
                    dwNumBytesTosend++;
                }
                else
                { // Send data > 8 bit, need write the left data as bit
                    InputBuffer[dwNumBytesTosend] = Write_buffer[dwNumBytesTosend - head_length - 2];
                    dwNumBytesTosend++;
                }

            }

            ///
            //Step5: 0x26=Clock Data Bits In on -ve Clock Edge MSB First, and 0x22= +ve Clock
            byte_number = Read_bit_length / 8;
            bit_number = Read_bit_length % 8;
            /* HB's code
            for (j = 0; j < byte_number; j++)
            {
                if (MSB == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x22;
                else if (MSB == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x26;
                else if (MSB == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x2A;
                else if (MSB == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x2E;
                InputBuffer[dwNumBytesTosend++] = 0x07;
                //Data length of 0x07 means 1 byte data to clock in
            }
            */

            if (byte_number != 0)
            {
                if (MSB == 1 && Read_VE == 1) InputBuffer[dwNumBytesTosend++] = 0x20;
                else if (MSB == 1 && Read_VE == 0) InputBuffer[dwNumBytesTosend++] = 0x24;
                else if (MSB == 0 && Read_VE == 1) InputBuffer[dwNumBytesTosend++] = 0x28;
                else if (MSB == 0 && Read_VE == 0) InputBuffer[dwNumBytesTosend++] = 0x2C;
                // the write_data byte length
                InputBuffer[dwNumBytesTosend++] = (byte)(byte_number - 1);
                InputBuffer[dwNumBytesTosend++] = (byte)((byte_number - 1) >> 8);
            }
            if (bit_number != 0)
            {
                if (MSB == 1 && Read_VE == 1) InputBuffer[dwNumBytesTosend++] = 0x22;
                else if (MSB == 1 && Read_VE == 0) InputBuffer[dwNumBytesTosend++] = 0x26;
                else if (MSB == 0 && Read_VE == 1) InputBuffer[dwNumBytesTosend++] = 0x2A;
                else if (MSB == 0 && Read_VE == 0) InputBuffer[dwNumBytesTosend++] = 0x2E;
                InputBuffer[dwNumBytesTosend++] = (byte)(bit_number - 1);
            }

            //Step6:disenable CS
            InputBuffer[dwNumBytesTosend++] = 0x80;

            if (CS == 1 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x01;
            else if (CS == 1 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0;
            else if (CS == 0 && VE == 0) InputBuffer[dwNumBytesTosend++] = 0x0F;
            else if (CS == 0 && VE == 1) InputBuffer[dwNumBytesTosend++] = 0x0E;
            else InputBuffer[dwNumBytesTosend++] = 0x0E;

            InputBuffer[dwNumBytesTosend++] = 0x0B;
            InputBuffer[dwNumBytesTosend++] = 0x87;

            ///Step7:add clock if needed after CS disabled
            if (Add_clock != 0)
            {
                InputBuffer[dwNumBytesTosend++] = 0x13;
                InputBuffer[dwNumBytesTosend++] = (byte)(Add_clock - 1);
                InputBuffer[dwNumBytesTosend++] = 0;
            }
            //add clock end

            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            // Check the amount of data available to read
            // In this case we know how much data we are expecting, 
            // so wait until we have all of the bytes we have sent.
            /*
            //myFtdiDevice.Purge(0x0008); 
            //clear buffer before set
            UInt32 numBytesAvailable = 0;
            ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
            byte[] readData1 = new byte[numBytesAvailable];
            UInt32 numBytesRead = 0;
            ftStatus = myFtdiDevice.Read(readData1, numBytesAvailable, ref numBytesRead);
            //Clear end
            */


            UInt32 numBytesWritten = 0;
            ftStatus = myFtdiDevice.Write(InputBuffer, dwNumBytesTosend, ref numBytesWritten);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                if (!errorPompted)
                {
                    MessageBox.Show("Failed to write data");
                    errorPompted = true;
                }
                //Console.WriteLine("Failed to get number of bytes available to read (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return null;
            }
            Thread.Sleep(50);


            UInt32 numBytesAvailable = 0;
            ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
            //cancel auto check buffer number because find "FA00" before readdata 
            /*
            if (bit_number != 0)
            numBytesAvailable = (uint) (byte_number+1);
            else
                numBytesAvailable = (uint)(byte_number);
             */
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                if (!errorPompted)
                {
                    MessageBox.Show("Failed to get number of bytes available to read (error " + ftStatus.ToString() + ")");
                    errorPompted = true;
                }
                //Console.WriteLine("Failed to get number of bytes available to read (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return null;
            }

            UInt32 numBytesRead = 0;
            // Now that we have the amount of data we want available, read it
            byte[] readData = new byte[numBytesAvailable];
            //UInt32 numBytesRead = 0;
            // Note that the Read method is overloaded, so can read string or byte array data
            ftStatus = myFtdiDevice.Read(readData, numBytesAvailable, ref numBytesRead);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                if (!errorPompted)
                {
                    MessageBox.Show("Failed to read data (error " + ftStatus.ToString() + ")");
                    errorPompted = true;
                }
                //Console.WriteLine("Failed to read data (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return null;
            }
            else
            {
                //MessageBox.Show("Read Sucessfully");

            }
            //Console.WriteLine(readData);
            return readData;
        }

        /// <summary>
        ///Send data
        ///MSB variable is not used at this moment. May add later if it should be used.
        ///Data is send MSB first by default
        ///All data are moved to allign on the right and first byte may have less than 8 bits
        ///The first byte is right alligned
        ///Maxinum per send 64K Bytes
        /// </summary>
        public int Write_Flash(Int64 Send_Length_bits, Byte[] Write_Buffer, int VE, int MSB, int CS, int Add_clock)
        {
            Int64 bits_Num = Send_Length_bits % 8;//the number of bit to send
            Int64 Bytes_Num = Send_Length_bits / 8;//the number of Bytes to send
            Int64 Send_Count = 0, NumToSend = 0;
            UInt16 Length = 0, SendOffset = 0;
            const Int32 Send_MAX_NUM = 32768;//Send maximum number in bytes 
            Byte Command_SetStateConfig_Enable_CS = 0;
            Byte Command_SetStateConfig_Disable_CS = 0;
            Byte Command_Bytes_Out = 0;
            Byte Command_bits_Out = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
            //Byte Send_Temp;
            //Clear Buffer

            Array.Clear(byOutputBuffer, 0, byOutputBuffer.Length);

            dwNumBytesToSend = 0;

            #region CommandRecognize
            if (VE == 1)
            {
                Command_Bytes_Out = 0x10;
                Command_bits_Out = 0x12;
            }
            else
            {
                Command_Bytes_Out = 0x11;
                Command_bits_Out = 0x13;
            }
            if (CS == 1)
            {
                if (VE == 1)
                {
                    Command_SetStateConfig_Enable_CS = 0x09;
                    Command_SetStateConfig_Disable_CS = 0x01;
                }
                else
                {
                    Command_SetStateConfig_Enable_CS = 0x08;
                    Command_SetStateConfig_Disable_CS = 0x00;
                }

            }
            else
            {
                if (VE == 1)
                {
                    Command_SetStateConfig_Enable_CS = 0x01;
                    Command_SetStateConfig_Disable_CS = 0x09;
                }
                else
                {
                    Command_SetStateConfig_Enable_CS = 0x00;
                    Command_SetStateConfig_Disable_CS = 0x08;
                }
            }
            #endregion

            //Add extra dummy clock before Enable CS
            if (Add_clock != 0)
            {
                byOutputBuffer[dwNumBytesToSend++] = Command_bits_Out;//Clock Data Bits Out on -ve clock edge MSB first
                byOutputBuffer[dwNumBytesToSend++] = (byte)(Add_clock - 1);
                byOutputBuffer[dwNumBytesToSend++] = 0;
            }

            //Enable CS
            byOutputBuffer[dwNumBytesToSend++] = 0x80;//Configure data bits low-byte of MPSSE port
            byOutputBuffer[dwNumBytesToSend++] = Command_SetStateConfig_Enable_CS;//Enalbe CS IO state config
            byOutputBuffer[dwNumBytesToSend++] = 0xFB;//Direction config above

            #region Send Data
            //Send Data
            //Send bits First
            //MSB first
            if (bits_Num > 0)
            {
                Length = (UInt16)(bits_Num - 1);
                Byte Temp_Byte = (Byte)Write_Buffer[0];
                byOutputBuffer[dwNumBytesToSend++] = Command_bits_Out;//MSB first, clock a number of bits out
                byOutputBuffer[dwNumBytesToSend++] = (byte)(Length & 0xFF);
                byOutputBuffer[dwNumBytesToSend++] = Temp_Byte;
                SendOffset = 1;
            }

            //Send Data
            //Send Bytes Last
            //if send number is larger than 32K
            while (Send_Count < Bytes_Num)
            {
                NumToSend = Bytes_Num - Send_Count;
                if (NumToSend > Send_MAX_NUM)
                {
                    Length = (UInt16)(Send_MAX_NUM - 1);
                    byOutputBuffer[dwNumBytesToSend++] = Command_Bytes_Out;//MSB first, clock a number of bytes out
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length & 0xFF);//Length L
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length >> 8);//Length H
                    for (uint count = 0; count < Bytes_Num; count++)
                    {
                        byOutputBuffer[dwNumBytesToSend++] = Write_Buffer[SendOffset + count + Send_Count];//a number of Bytes
                    }
                    Send_Count = Send_Count + Send_MAX_NUM;
                    //Send data to FTDI one time
                    ftStatus = myFtdiDevice.Write(byOutputBuffer, dwNumBytesToSend, ref dwNumBytesSent);
                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    {
                        if (!errorPompted)
                        {
                            MessageBox.Show("Failed to write data to device (error " + ftStatus.ToString() + ")");
                            errorPompted = true;
                        }
                        return Fail;
                    }
                    dwNumBytesToSend = 0;
                }
                else
                {
                    Length = (UInt16)(NumToSend - 1);
                    byOutputBuffer[dwNumBytesToSend++] = Command_Bytes_Out;//MSB first, clock a number of bytes out
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length & 0xFF);//Length L
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length >> 8);//Length H
                    for (uint count = 0; count < Bytes_Num; count++)
                    {
                        byOutputBuffer[dwNumBytesToSend++] = Write_Buffer[SendOffset + count + Send_Count];//a number of Bytes
                    }
                    Send_Count = Send_Count + NumToSend;
                }
            }
            #endregion

            //Disable CS
            byOutputBuffer[dwNumBytesToSend++] = 0x80;//Configure data bits low-byte of MPSSE port
            byOutputBuffer[dwNumBytesToSend++] = Command_SetStateConfig_Disable_CS;//Disable CS state config
            byOutputBuffer[dwNumBytesToSend++] = 0xFB;//Direction config above

            //Add extra dummy clock after disable CS
            if (Add_clock != 0)
            {
                byOutputBuffer[dwNumBytesToSend++] = Command_bits_Out;//Clock Data Bits Out on -ve clock edge MSB first
                byOutputBuffer[dwNumBytesToSend++] = (byte)(Add_clock - 1);
                byOutputBuffer[dwNumBytesToSend++] = 0;
            }

            ftStatus = myFtdiDevice.Write(byOutputBuffer, dwNumBytesToSend, ref dwNumBytesSent);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                if (!errorPompted)
                {
                    MessageBox.Show("Failed to write data to device (error " + ftStatus.ToString() + ")");
                    errorPompted = true;
                }
                return Fail;
            }
            return OK;
        }
        //Read data
        //MSB variable is not used at this moment. May add later if it should be used.
        //Data is send MSB first by default
        /// <summary>
        /// Read data
        /// MSB variable is not used at this moment. May add later if it should be used.
        /// Data is send MSB first by default
        /// </summary>
        public int Read_Flash(Int64 Send_Length_bits, Byte[] Write_Buffer, Int64 BitsToRead, ref Int64 BytesRead, ref Byte[] Read_Buffer, int VE, int MSB, int CS, int Read_VE, int Add_clock)
        {
            Int64 bits_Num_Send = Send_Length_bits % 8;//the number of bit to send
            Int64 Bytes_Num_Send = Send_Length_bits / 8;//the number of Bytes to send
            Int64 bits_Num_Read = BitsToRead % 8;//the number of bit to send
            Int64 Bytes_Num_Read = BitsToRead / 8;//the number of Bytes to send
            UInt16 Length = 0, SendOffset = 0;
            Int64 Read_Count = 0, NumToRead = 0, ReadOffset = 0;
            const Int32 READ_MAX_NUM = 32768;//Send maximum number in bytes 
            Byte Command_SetStateConfig_Enable_CS = 0;
            Byte Command_SetStateConfig_Disable_CS = 0;
            Byte Command_Bytes_Out = 0;
            Byte Command_bits_Out = 0;
            Byte Command_Bytes_In = 0;
            Byte Command_bits_In = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            //Byte Send_Temp;
            Array.Clear(byOutputBuffer, 0, byOutputBuffer.Length);

            dwNumBytesToSend = 0;

            #region CommandRecognize
            if (VE == 1)
            {
                Command_Bytes_Out = 0x10;//Output on the rising clock, no input
                Command_bits_Out = 0x12;

            }
            else
            {
                Command_Bytes_Out = 0x11;
                Command_bits_Out = 0x13;
            }
            if (CS == 1)
            {
                if (VE == 1)
                {
                    Command_SetStateConfig_Enable_CS = 0x09;
                    Command_SetStateConfig_Disable_CS = 0x01;
                }
                else
                {
                    Command_SetStateConfig_Enable_CS = 0x08;
                    Command_SetStateConfig_Disable_CS = 0x00;
                }

            }
            else
            {
                if (VE == 1)
                {
                    Command_SetStateConfig_Enable_CS = 0x01;
                    Command_SetStateConfig_Disable_CS = 0x09;
                }
                else
                {
                    Command_SetStateConfig_Enable_CS = 0x00;
                    Command_SetStateConfig_Disable_CS = 0x08;
                }
            }
            if (Read_VE == 1)
            {
                Command_Bytes_In = 0x20;//Clock Data Bytes In on the rising clock edge, no write
                Command_bits_In = 0x22;//Clock Data Bits In on the rising clock edge, no write
            }
            else
            {
                Command_Bytes_In = 0x24;//Clock Data Bytes In on the falling clock edge, no write
                Command_bits_In = 0x26;//Clock Data Bits In on the falling clock edge, no write
            }
            #endregion

            //Add extra dummy clock before Enable CS
            if (Add_clock != 0)
            {
                byOutputBuffer[dwNumBytesToSend++] = Command_bits_Out;//Clock Data Bits Out on -ve clock edge MSB first
                byOutputBuffer[dwNumBytesToSend++] = (byte)(Add_clock - 1);
                byOutputBuffer[dwNumBytesToSend++] = 0;
            }

            //Enable CS
            byOutputBuffer[dwNumBytesToSend++] = 0x80;//Configure data bits low-byte of MPSSE port
            byOutputBuffer[dwNumBytesToSend++] = Command_SetStateConfig_Enable_CS;//Enalbe CS IO state config
            byOutputBuffer[dwNumBytesToSend++] = 0xFB;//Direction config above

            #region Send Data
            //Send Data
            //Send bits First
            //MSB first
            if (bits_Num_Send > 0)
            {
                Length = (UInt16)(bits_Num_Send - 1);
                Byte Temp_Byte = (Byte)Write_Buffer[0];
                byOutputBuffer[dwNumBytesToSend++] = Command_bits_Out;//MSB first, clock a number of bits out
                byOutputBuffer[dwNumBytesToSend++] = (byte)(Length & 0xFF);
                byOutputBuffer[dwNumBytesToSend++] = Temp_Byte;
                SendOffset = 1;
            }

            //Send Data
            //Send Bytes Last
            Length = (UInt16)(Bytes_Num_Send - 1);
            byOutputBuffer[dwNumBytesToSend++] = Command_Bytes_Out;//MSB first, clock a number of bytes out
            byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length & 0xFF);//Length L
            byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length >> 8);//Length H

            for (uint count = 0; count < Bytes_Num_Send; count++)
            {
                byOutputBuffer[dwNumBytesToSend++] = Write_Buffer[SendOffset + count];//a number of Bytes
            }
            #endregion

            #region Dummy Bits
            //Send 3 Bits --- dummy bits used in Flash Read
            //MSB first
            Length = (UInt16)(3 - 1);
            byOutputBuffer[dwNumBytesToSend++] = Command_bits_Out;//MSB first, clock a number of bits out
            byOutputBuffer[dwNumBytesToSend++] = (byte)(Length & 0xFF);
            byOutputBuffer[dwNumBytesToSend++] = 0x0;
            #endregion
            #region Read Data
            //Read Data
            //Read Bytes First
            //Maxmum Read Count is 32768
            Read_Count = 0;
            BytesRead = 0;
            while (Read_Count < Bytes_Num_Read)
            {
                NumToRead = Bytes_Num_Read - Read_Count;
                if (NumToRead > READ_MAX_NUM)
                {
                    Length = (UInt16)(READ_MAX_NUM - 1);
                    byOutputBuffer[dwNumBytesToSend++] = Command_Bytes_In;//MSB first, clock a number of bytes In
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length & 0xFF);//Length L
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length >> 8);//Length H

                    ftStatus = FTDI.FT_STATUS.FT_OK;
                    ftStatus = myFtdiDevice.Write(byOutputBuffer, dwNumBytesToSend, ref dwNumBytesSent);
                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    {
                        if (!errorPompted)
                        {
                            MessageBox.Show("Failed to write data to device (error " + ftStatus.ToString() + ")");
                            errorPompted = true;
                        }
                        return Fail;
                    }
                    dwNumBytesToRead = 0;//Clear Value

                    Thread.Sleep(10);//Wait for data to be transmitted and status to returned by the device driver, see latency timer above

                    //Check the receive buffer
                    ftStatus = myFtdiDevice.GetRxBytesAvailable(ref dwNumBytesToRead);
                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    {
                        if (!errorPompted)
                        {
                            MessageBox.Show("Failed to GetRxBytesAvailable:  (error " + ftStatus.ToString() + ")");
                            errorPompted = true;
                        }
                        return Fail;
                    }
                    dwNumBytesRead = 0;//Clear Value
                    //Get the number of bytes in
                    ftStatus = myFtdiDevice.Read(Read_Buffer, dwNumBytesToRead, ref dwNumBytesRead);
                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    {
                        if (!errorPompted)
                        {
                            MessageBox.Show("Failed to Read Data, (error " + ftStatus.ToString() + ")");
                            errorPompted = true;
                        }
                        return Fail;
                    }
                    if (dwNumBytesRead < READ_MAX_NUM)
                    {
                        //MessageBox.Show("Fail to Read enough data, dwNumBytesRead < READ_MAX_NUM");
                        //return Fail;
                    }
                    Read_Count = Read_Count + READ_MAX_NUM;
                    BytesRead = BytesRead + dwNumBytesRead;
                }
                else
                {
                    Length = (UInt16)(NumToRead - 1);
                    byOutputBuffer[dwNumBytesToSend++] = Command_Bytes_In;//MSB first, clock a number of bytes In
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length & 0xFF);//Length L
                    byOutputBuffer[dwNumBytesToSend++] = (Byte)(Length >> 8);//Length H

                    /*
                    //Read Last Byte
                    //MSB first, First 5 bits
                    Length = (5 - 1);
                    byOutputBuffer[dwNumBytesToSend++] = Command_bits_In;//MSB first, clock a number of bits In
                    byOutputBuffer[dwNumBytesToSend++] = (byte)(Length & 0xFF);
                    */

                    //Disable CS
                    byOutputBuffer[dwNumBytesToSend++] = 0x80;//Configure data bits low-byte of MPSSE port
                    byOutputBuffer[dwNumBytesToSend++] = Command_SetStateConfig_Disable_CS;//Disable CS state config
                    byOutputBuffer[dwNumBytesToSend++] = 0xFB;//Direction config above

                    /*
                    //Read Last Byte
                    //MSB first, last 3 bits
                    Length =(3 - 1);
                    byOutputBuffer[dwNumBytesToSend++] = Command_bits_In;//MSB first, clock a number of bits In
                    byOutputBuffer[dwNumBytesToSend++] = (byte)(Length & 0xFF);
                    */

                    //Add extra dummy clock after Disable CS
                    if (Add_clock != 0)
                    {
                        byOutputBuffer[dwNumBytesToSend++] = Command_bits_Out;//Clock Data Bits Out on -ve clock edge MSB first
                        byOutputBuffer[dwNumBytesToSend++] = (byte)(Add_clock - 1);
                        byOutputBuffer[dwNumBytesToSend++] = 0;
                    }

                    ftStatus = FTDI.FT_STATUS.FT_OK;
                    ftStatus = myFtdiDevice.Write(byOutputBuffer, dwNumBytesToSend, ref dwNumBytesSent);
                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    {
                        if (!errorPompted)
                        {
                            MessageBox.Show("Failed to write data to device (error " + ftStatus.ToString() + ")");
                            errorPompted = true;
                        }
                        return Fail;
                    }

                    Thread.Sleep(10);//Wait for data to be transmitted and status to returned by the device driver, see latency timer above

                    dwNumBytesToRead = 0;//Clear Value
                    //Check the receive buffer
                    ftStatus = myFtdiDevice.GetRxBytesAvailable(ref dwNumBytesToRead);
                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    {
                        if (!errorPompted)
                        {
                            MessageBox.Show("Failed to GetRxBytesAvailable:  (error " + ftStatus.ToString() + ")");
                            errorPompted = true;
                        }
                        return Fail;
                    }
                    dwNumBytesRead = 0;//Clear Value
                    //Get the number of bytes in
                    ftStatus = myFtdiDevice.Read(Read_Buffer, dwNumBytesToRead, ref dwNumBytesRead);
                    //                    Byte[] Temp = new Byte[100];
                    //                    ftStatus = myFtdiDevice.Read(Temp, dwNumBytesToRead, ref dwNumBytesRead);

                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    {
                        if (!errorPompted)
                        {
                            MessageBox.Show("Failed to Read Data, (error " + ftStatus.ToString() + ")");
                            errorPompted = true;
                        }
                        return Fail;
                    }
                    if (dwNumBytesRead < NumToRead)
                    {
                        //MessageBox.Show("Fail to Read enough data, dwNumBytesRead < NumToRead");
                        //return Fail;
                    }
                    Read_Count = Read_Count + NumToRead;
                    BytesRead = BytesRead + dwNumBytesRead;

                }

            }
            #endregion

            return OK;
        }
    }
}

