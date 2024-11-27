using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
namespace AutomaticUpdates
{
    class Program
    {

        
        static void Main(string[] args)
        {
            if(args.Length>1)
            {
                
                RefreshTestCase(args[0], args.Length > 3 ? args[3] : "", args.Length > 4 ? args[4] : "");
                try
                {

                    Process myprocess = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo(Application.StartupPath+"\\"+args[1]);
                    myprocess.StartInfo = startInfo;
                    myprocess.StartInfo.UseShellExecute = false;
                    myprocess.Start();
                    System.Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("启动应用程序时出错！原因：" + ex.Message);
                }
            }
            else
            {
                return;
            }

        }

        public static void RefreshTestCase(string sServerDllPath, string exculsiveFile, string includeFile)
        {
            string str;
            string sLocalDllPath = Application.StartupPath+"\\";
            String[] serverFiles;
            string fileName;
            string selfFileName = Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\\") + 1, Application.ExecutablePath.Length - Application.ExecutablePath.LastIndexOf("\\") - 1);
            serverFiles = Directory.GetFiles(sServerDllPath, "*.*", SearchOption.TopDirectoryOnly);
            string[] sExculsiveFiles = exculsiveFile.Split(';');
            Console.WriteLine("Prepare copy files: " + includeFile);
            try
            {
                if (includeFile=="")
                {
                    for (int l = 0; l < serverFiles.Length; l++)
                    {
                        fileName = serverFiles[l].Substring(serverFiles[l].LastIndexOf("\\") + 1, serverFiles[l].Length - serverFiles[l].LastIndexOf("\\") - 1);
                        if (!sExculsiveFiles.Contains(fileName) && fileName != selfFileName)
                        {
                            File.Copy(serverFiles[l], sLocalDllPath + fileName, true);
                            Console.WriteLine("Copy " + fileName + " finished.");
                        }
                    }
                }
                else
                {
                    
                    if (includeFile.Contains("*."))
                    {
                        str = includeFile.Split(';').First(t => t.Contains("*"));
                        
                        serverFiles = Directory.GetFiles(sServerDllPath, str, SearchOption.TopDirectoryOnly);
                        includeFile.Replace(str, "");
                        foreach (string s in serverFiles)
                        {
                            includeFile = includeFile + ";" + s.Substring(s.LastIndexOf("\\") + 1, s.Length - s.LastIndexOf("\\") - 1);
                        }
                    }
                    serverFiles = Directory.GetFiles(sServerDllPath, "*.*", SearchOption.TopDirectoryOnly);
                    string[] sIncludeFiles = includeFile.Split(';');
                    //Console.WriteLine("serverFiles " + string.Join(";",serverFiles.ToList()));
                    //Console.WriteLine("includeFile " + includeFile);
                    //Console.Read();
                    for (int l = 0; l < serverFiles.Length; l++)
                    {
                        fileName = serverFiles[l].Substring(serverFiles[l].LastIndexOf("\\") + 1, serverFiles[l].Length - serverFiles[l].LastIndexOf("\\") - 1);
                        if (sIncludeFiles.Contains(fileName) && fileName != selfFileName)
                        {
                            File.Copy(serverFiles[l], sLocalDllPath + fileName, true);
                            Console.WriteLine("Copy " + fileName + " finished.");
                        }
                    }
                }

            }
            catch(Exception e)
            {
                MessageBox.Show("AutoUpdate:"+e.ToString());
            }

        }


    }
}
