using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using DataHelper;
using BoardDriver;
namespace SlideFroms
{
    public partial class Welcome : Form
    {
        //public BoardDriver.VXT3BoardDriver dut;
        //public SliderDriver.IBoard dut;
        public dynamic dut;
        public string[] sBoards;
        //public bool bBoardTest;
        //public bool bIVITest;
        public string sTestLevel; //Board,Module,System
        IniFileHelper ConfigFile;
        string DrvierDll = "";
        string DrvierCodeName = "";
        string IVIOptions = "";
        string UpdateFileListString;
        string UpdateDir = "";
        public Welcome()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        private void bt_Enter_Click(object sender, EventArgs e)
        {
            Form frm_MainForm;

            if (sTestLevel == "System")
            {
                ConfigFile.WriteIniString("ModuleDriver", "Addr", tB_addr.Text);
                DrvierDll = ConfigFile.GetIniString("ModuleDriver", "Dll", "");
                DrvierCodeName = ConfigFile.GetIniString("ModuleDriver", "NameSpace&ClassName", "");
                dut = (SliderDriver.IModule)getDut(DrvierDll, DrvierCodeName);
                dut.Connect(tB_addr.Text, false, 2000);
                if (cbB_StartSliders.Text == "ALL")
                {
                    //frm_MainForm = new MainForm(dut, bBoardTest,bIVITest, sBoards);
                    frm_MainForm = new MainForm(dut, sTestLevel, sBoards);
                    ConfigFile.WriteIniString(cbB_Version.Text, "StartBoards", "ALL");
                }
                else
                {
                    frm_MainForm = new MainForm(dut, sTestLevel, new string[] { cbB_StartSliders.Text });
                    ConfigFile.WriteIniString(cbB_Version.Text, "StartBoards", cbB_StartSliders.Text);
                }
            }
            else
            {
                if (sTestLevel=="Board")
                {
                    DrvierDll = ConfigFile.GetIniString("BoardDriver", "Dll", "");
                    DrvierCodeName = ConfigFile.GetIniString("BoardDriver", "NameSpace&ClassName", "");
                    dut = (SliderDriver.IBoard)getDut(DrvierDll, DrvierCodeName);
                    dut.Connect(" ", true, false, rTB_Options.Text);
                    if (cbB_StartSliders.Text == "ALL")
                    {
                        ConfigFile.WriteIniString(cbB_Version.Text, "StartBoards", "ALL");
                    }
                    else
                    {
                        sBoards = new string[] { cbB_StartSliders.Text };
                        ConfigFile.WriteIniString(cbB_Version.Text, "StartBoards", cbB_StartSliders.Text);
                    }
                }
                else
                {
                    ConfigFile.WriteIniString("IVIDriver", "Addr", tB_addr.Text);
                    ConfigFile.WriteIniString("IVIDriver", "Options", rTB_Options.Text);

                    DrvierDll = ConfigFile.GetIniString("IVIDriver", "Dll", "");
                    DrvierCodeName = ConfigFile.GetIniString("IVIDriver", "NameSpace&ClassName", "");
                    IVIOptions = ConfigFile.GetIniString("IVIDriver", "Options", "");
                    dut = (SliderDriver.IBoard)getDut(DrvierDll, DrvierCodeName);
                    dut.Connect(tB_addr.Text, true, false, IVIOptions);
                    if (cbB_StartSliders.Text == "ALL")
                    {
                        ConfigFile.WriteIniString(cbB_Version.Text, "StartBoards", "ALL");
                    }
                    else
                    {
                        ConfigFile.WriteIniString(cbB_Version.Text, "StartBoards", cbB_StartSliders.Text);
                        sBoards = new string[] { cbB_StartSliders.Text };
                    }
                }
            }
            frm_MainForm = new MainForm(this);
            frm_MainForm.Show();
            this.Hide();
            ConfigFile.WriteIniString(cbB_Version.Text, "StartBoards", cbB_StartSliders.Text);
        }
        public dynamic getDut(string DutDllPath, string DutClassName)
        {
            Type testType = Assembly.UnsafeLoadFrom(DutDllPath).GetType(DutClassName);
            Object testInstance = Activator.CreateInstance(testType);
            return testInstance;
        }
        private void UpdateBoardList()
        {
            string board;
            cbB_StartSliders.Items.Clear();
            ConfigFile.WriteIniString("Main", "VersionIndex", (cbB_Version.SelectedIndex + 1).ToString());

            switch (sTestLevel)
            {
                case "Board":
                    cbB_StartSliders.Items.Add("ALL");
                    sBoards = ConfigFile.GetIniString(cbB_Version.Text, "Boards", "").Split(',');                  
                    foreach (string sliderboard in sBoards)
                    {
                        cbB_StartSliders.Items.Add(sliderboard);
                    }
                    cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
                    break;
                case "Module":
                    cbB_StartSliders.Items.Add("ALL");
                    cbB_StartSliders.Items.Add(ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", ""));
                    sBoards = ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", "").Split(',');
                    foreach (string sliderboard in sBoards)
                    {
                        cbB_StartSliders.Items.Add(sliderboard);
                    }
                    cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
                    break;
                case "System":
                cbB_StartSliders.Items.Add("ALL");
                cbB_StartSliders.Items.Add(ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", ""));
                sBoards = ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", "").Split(',');        
                foreach (string sliderboard in sBoards)
                {
                    cbB_StartSliders.Items.Add(sliderboard);
                }
                cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
                    break;
            }
            //if (bBoardTest)
            //{
            //    if (bIVITest)
            //    {
            //        cbB_StartSliders.Items.Add("ALL");
            //        cbB_StartSliders.Items.Add(ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", ""));
            //        sBoards = ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", "").Split(',');
            //        foreach (string sliderboard in sBoards)
            //        {
            //            cbB_StartSliders.Items.Add(sliderboard);
            //        }
            //        cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
            //    }
            //    else
            //    {
            //        cbB_StartSliders.Items.Add("ALL");
            //        sBoards = ConfigFile.GetIniString(cbB_Version.Text, "Boards", "").Split(',');                  
            //        foreach (string sliderboard in sBoards)
            //        {
            //            cbB_StartSliders.Items.Add(sliderboard);
            //        }
            //        cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
            //    }
            //}
            //else
            //{
            //    cbB_StartSliders.Items.Add("ALL");
            //    cbB_StartSliders.Items.Add(ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", ""));
            //    sBoards = ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", "").Split(',');        
            //    foreach (string sliderboard in sBoards)
            //    {
            //        cbB_StartSliders.Items.Add(sliderboard);
            //    }
            //    cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
            //}
        }
        private void Welcome_Load(object sender, EventArgs e)
        {
            string fileName = Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\\") + 1, Application.ExecutablePath.Length - Application.ExecutablePath.LastIndexOf("\\") - 1);
            FileVersionInfo FviLocal = FileVersionInfo.GetVersionInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName);
            this.Text = "Slider(Version: " + FviLocal.FileVersion.ToString() + ")" ; 

            ConfigFile = new IniFileHelper(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config.ini");
            //UpdateFileList = ConfigFile.GetIniString("Main", "UpdateList", "").Split(';').ToList();
            UpdateFileListString = ConfigFile.GetIniString("Main", "UpdateList", "");
            UpdateDir = ConfigFile.GetIniString("Main", "UpdateDir", "");
            string[] sVersions = ConfigFile.GetIniString("Main", "Versions", "").Split(',');
            for (int i = 0; i < sVersions.Length; i++)
            {
                cbB_Version.Items.Add(sVersions[i]);
            }
            sTestLevel = ConfigFile.GetIniString("Main", "TestLevel ", "");

            cbB_Version.SelectedIndex = Convert.ToInt16(ConfigFile.GetIniString("Main", "VersionIndex", "1")) - 1;

            switch (sTestLevel)
            {
                case "Board":
                    tB_addr.Enabled = false;
                    rTB_Options.Enabled = false;
                    rB_Board.Checked = true;
                    break;
                case "Module":       
                    tB_addr.Enabled = true;
                    rTB_Options.Enabled = true;
                    tB_addr.Text = ConfigFile.GetIniString("IVIDriver", "Addr", "");
                    rTB_Options.Text = ConfigFile.GetIniString("IVIDriver", "Options", "");
                    rB_Module.Checked = true;
                    break;
                case "System":
                    rB_System.Checked = true;
                    tB_addr.Enabled = true;
                    rTB_Options.Enabled = false;
                    tB_addr.Text = ConfigFile.GetIniString("ModuleDriver", "Addr", "");
                    rB_System.Checked = true;
                    break;
            }

            checkUpdate();
        }
        private void cbB_Version_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBoardList();        
        }
        private void rB_Board_CheckedChanged(object sender, EventArgs e)
        {
            cbB_StartSliders.Items.Clear();
            tB_addr.Enabled = false;
            rTB_Options.Enabled = false;
            sTestLevel = "Board";
            ConfigFile.WriteIniString("Main", "TestLevel ", sTestLevel);
            cbB_StartSliders.Items.Add("ALL");
            sBoards = ConfigFile.GetIniString(cbB_Version.Text, "Boards", "").Split(',');
            
            foreach (string sliderboard in sBoards)
            {
                cbB_StartSliders.Items.Add(sliderboard);
            }
            cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
            //UpdateBoardList();
        }

        private void rB_Module_CheckedChanged(object sender, EventArgs e)
        {
            if (rB_Module.Checked)
            {
                cbB_StartSliders.Items.Clear();
                tB_addr.Enabled = true;
                rTB_Options.Enabled = true;
                tB_addr.Text = ConfigFile.GetIniString("IVIDriver", "Addr", "");
                rTB_Options.Text = ConfigFile.GetIniString("IVIDriver", "Options", "");
                sTestLevel = "Module";
                ConfigFile.WriteIniString("Main", "TestLevel ", sTestLevel);
                cbB_StartSliders.Items.Add("ALL");
                 sBoards = ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", "").Split(',');

                foreach (string sliderboard in sBoards)
                {
                    cbB_StartSliders.Items.Add(sliderboard);
                }
                cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
                tB_addr.Enabled = true;
                rTB_Options.Enabled = true;

            }

        }

        private void rB_System_CheckedChanged(object sender, EventArgs e)
        {
            if (rB_System.Checked)
            {
                cbB_StartSliders.Items.Clear();
                tB_addr.Enabled = true;
                rTB_Options.Enabled = false;
                tB_addr.Text = ConfigFile.GetIniString("ModuleDriver", "Addr", "");
                sTestLevel = "System";
                ConfigFile.WriteIniString("Main", "TestLevel ", sTestLevel);
                cbB_StartSliders.Items.Add("ALL");
                sBoards = ConfigFile.GetIniString(cbB_Version.Text, "IVIBoards", "").Split(',');

                foreach (string sliderboard in sBoards)
                {
                    cbB_StartSliders.Items.Add(sliderboard);
                }
                cbB_StartSliders.Text = ConfigFile.GetIniString(cbB_Version.Text, "StartBoards", "ALL");
                tB_addr.Enabled = true;
                rTB_Options.Enabled = false;

            }

        }


        private void bt_InitFDTI_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText("C:\\TEMP\\FTDI_Status.txt", "0");
            System.IO.File.WriteAllText("C:\\TEMP\\FTDI_Client.txt", "0"); 
        }

        private void button1_Click(object sender, EventArgs e)
        {

                if (!Directory.Exists(UpdateDir))
                    return;
                string ServerPath = UpdateDir + "\\";
                //Process[] processes = Process.GetProcessesByName("FTDIService");
                //if (processes.Length != 0)
                //{
                //    processes[0].Kill();
                //}
                ServerPath = ServerPath + " " + Process.GetCurrentProcess().ProcessName;
                string fileName = Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\\") + 1, Application.ExecutablePath.Length - Application.ExecutablePath.LastIndexOf("\\") - 1);
                ServerPath = ServerPath + " " + fileName;
                ServerPath = ServerPath + " " + "NA";
                ServerPath = ServerPath + " " + UpdateFileListString;
                try
                {
                    Process myprocess = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo(Application.StartupPath + "\\" + "AutoUpdate.exe", ServerPath);
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
        public void checkUpdate()
        {
            List<FileCheckInfo> CurFiles = GetAllFiles(System.AppDomain.CurrentDomain.BaseDirectory.Substring(0, System.AppDomain.CurrentDomain.BaseDirectory.Length));
            List<FileCheckInfo> PastFiles = GetAllFiles(UpdateDir);
            foreach (FileCheckInfo pastfile in PastFiles)
            {
                switch (CompareFiles(CurFiles, pastfile))
                {
                    case 2:
                        button1.Visible=true;
                        break;
                }
            }
        }
        private string[] fileType = { "dll", "exe" };
        public List<FileCheckInfo> GetAllFiles(string srcPath)
        {
            List<FileCheckInfo> AllFiles = new List<FileCheckInfo>();
            if (!Directory.Exists(srcPath))
                return AllFiles;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        ;
                    }
                    else
                    {
                        FileCheckInfo f = new FileCheckInfo();
                        if (fileType.Contains(i.Name.Substring(i.Name.LastIndexOf(".") + 1, i.Name.Length - i.Name.LastIndexOf(".") - 1)))
                        {
                            f.FileName = i.FullName.Substring(i.FullName.LastIndexOf("\\") + 1);
                            if (UpdateFileListString.Contains(f.FileName))
                            {
                                f.ParentName = dir.Name;
                                f.FileVersion = FileVersionInfo.GetVersionInfo(i.FullName).FileVersion;
                                f.FileModifyTime = i.LastWriteTime.ToString();
                                AllFiles.Add(f);
                            }

                        }

                    }
                }
                return AllFiles;

            }
            catch (Exception e)
            {
                throw;
            }
        }
        private int CompareFiles(List<FileCheckInfo> Files, FileCheckInfo File)
        {
            foreach (FileCheckInfo file in Files)
            {
                if (file.FileName == File.FileName)
                {
                    if (file.FileVersion == File.FileVersion)
                    {
                        return 1; //same file
                    }
                    else
                    {
                        return 2;//different
                    }
                }
            }
            return 0; //lost
        }
        public struct FileCheckInfo
        {
            public string FileName;
            public string ParentName;
            public string FileVersion;
            public string FileModifyTime;
        }

        private void cbB_StartSliders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }


}
