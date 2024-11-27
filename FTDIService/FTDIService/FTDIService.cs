using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Windows.Forms;

namespace FTDIService
{
    [ServiceContract(Namespace = "Http://Keysight.NPI.ServiceModel.FTDI")]
    public interface IFTDIService
    {
        [OperationContract]
        void Init_FDTIDevice(byte Clock_select, int SPI);
        [OperationContract]
        void Send_data(int bit_length, byte[] buffer, int VE, int MSB, int CS, int Add_clock);
        [OperationContract]
        byte[] Read_data(int Write_buffer_bit_length, byte[] Write_buffer, int Read_bit_length, int VE, int MSB, int CS, int Read_VE, int Add_clock);
        [OperationContract]
        void FDTI_close();
    }
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class FTDIService:IFTDIService
    {

        FTDIDriver ftdi;
        public FTDIService()
        {
            ftdi = new FTDIDriver();
            int rs = ftdi.Init_FDTIDevice(20, 0);//clock_select = 4, spi = 0;
            if (rs == 0)
            {
                File.WriteAllText("C:\\TEMP\\FTDI_Status.txt", "0");
            }
            else
            {
                //File.WriteAllText("C:\\TEMP\\FTDI_Client.txt", "1");
                File.WriteAllText("C:\\TEMP\\FTDI_Status.txt", "1");
            }
            File.WriteAllText("C:\\TEMP\\FTDI_Client.txt", "0"); 

        }        
        public void Init_FDTIDevice(byte Clock_select, int SPI)
        {
            ftdi.Init_FDTIDevice(Clock_select,SPI);

        }
        public void Send_data(int bit_length, byte[] buffer, int VE, int MSB, int CS, int Add_clock)
        {
            //int status = ftdi.Send_data(bit_length, buffer, VE, MSB, CS, Add_clock);
            //if(status==0)
            //{
            //    ftdi = new FTDIDriver();
            //    ftdi.Init_FDTIDevice(2, 0);//clock_select = 4, spi = 0;
            //}
            ftdi.Send_data(bit_length, buffer, VE, MSB, CS, Add_clock);
        }
        public byte[] Read_data(int Write_buffer_bit_length, byte[] Write_buffer, int Read_bit_length, int VE, int MSB, int CS, int Read_VE, int Add_clock)
        {
            //byte[] r = ftdi.Read_data(Write_buffer_bit_length, Write_buffer, Read_bit_length, VE, MSB, CS, Read_VE, Add_clock);
            //if(r == null)
            //{
            //    ftdi = new FTDIDriver();
            //    ftdi.Init_FDTIDevice(2, 0);//clock_select = 4, spi = 0;
            //}
            //return r;
            return ftdi.Read_data(Write_buffer_bit_length, Write_buffer, Read_bit_length, VE, MSB, CS, Read_VE, Add_clock);
        }
        public void FDTI_close()
        {
            ftdi.FDTI_close();
        }
    }
    public class FTDIServer
    {
        public static ServiceHost host;
        //public static ManualResetEvent mEvent = new ManualResetEvent(false);
        public static bool isHostServer = false;//whether the current app host the server
        //public static void startServer(int port)
        //{
        //    Thread ServerThread = new Thread(new ParameterizedThreadStart(FTDIServer.startServer));
        //    ServerThread.Start(8081);
        //}
        public static void startServer()
        {
            //Uri baseAddress = new Uri("http://localhost:"+port+"/FTDI");
            Uri baseAddress = new Uri("net.pipe://localhost/FTDIService");   
           
            try
            {
                host = new ServiceHost(typeof(FTDIService), baseAddress);                
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                //smb.HttpGetEnabled = true;
                //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                smb.HttpGetEnabled = false;                
                host.Description.Behaviors.Add(smb);
                NetNamedPipeBinding binding = new NetNamedPipeBinding();
                binding.ReceiveTimeout = TimeSpan.MaxValue;
                host.AddServiceEndpoint(typeof(IFTDIService), binding, "");
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexNamedPipeBinding(), "mex");//used for metadata downloading
                host.Open();
                isHostServer = true;
                Console.WriteLine("------Service is running at net.pipe://localhost/FTDIService");
            }
            catch (AddressAlreadyInUseException e)
            {
                isHostServer = false;
                Console.WriteLine(e.ToString());
                Console.WriteLine("------Service is already running in another Program");
            }
            //mEvent.Set();
        }
        public static void closeServer()
        {
            CommunicationState state = (CommunicationState)host.State;//check if this application start the server
            if(state == CommunicationState.Opened) host.Close();
            //mEvent.Reset();
        }        
    }    
}
