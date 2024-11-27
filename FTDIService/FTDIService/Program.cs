using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace FTDIService
{
    class Program
    {
        static void Main(string[] args)
        {
            initFTDIServer();

        }
        public static void initFTDIServer()
        {
            FTDIServer.startServer();
            Console.WriteLine("ServerStarted");
            //if (File.ReadAllText("C:\\TEMP\\FTDI_Client.txt") != "-99")
            //{
            //    while(true);
            //}
            while (true) ;
        }

        public void closeFTDIServer()
        {
            try
            {
                FTDIServer.closeServer();
            }
            catch (Exception e)
            {
                Console.WriteLine("FTDI close error. Press Any Key to Continue......");
                Console.ReadLine();
            }
        }
    }
}
