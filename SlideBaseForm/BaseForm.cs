using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Xml;
using System.Runtime.InteropServices;
using MyControl;
//using MyEncapsulation;
using BoardDriver;
namespace SlideBase
{
    public partial class BaseForm : Form
    {
        public dynamic Dut;
        
        public dynamic rTB_Operation = null;
        public dynamic FrameRichTextBox = null;
        public object oHash;
        public string sRegisterFileName = "register.xml";
        public string sCurrentFileName = "current.xml";
        public string sDefaultFileName = "default.xml";
        public bool _BoardTest = true;

        public bool _IVITest = false;
        private string lastValue = "";
        [CategoryAttribute("Slider属性"), DefaultValue(true)]
        public bool BoardTest
        {
            set { _BoardTest = value; }
            get { return _BoardTest; }
        }
        //TestName与Xml文件名相同
        private string _BoardName = "xxx";
        [CategoryAttribute("Slider属性"), DefaultValue("xxx")]
        public string BoardName
        {
            set { _BoardName = value; }
            get { return _BoardName; }
        }
        private bool _RecoverToHw = false;
        [CategoryAttribute("Slider属性"), DefaultValue(false)]
        public bool RecoverToHw
        {
            set { _RecoverToHw = value; }
            get { return _RecoverToHw; }
        }
        private bool _LoadXml = true;
        [CategoryAttribute("Slider属性"), DefaultValue(true)]
        public bool LoadXml
        {
            set { _LoadXml = value; }
            get { return _LoadXml; }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BaseForm());
        }
        public BaseForm()
        {

            InitializeComponent();

        }
        public void SaveSliderLocation()
        {
            //Save positions of each controls
            Hashtable ht = new Hashtable();//使用哈希表存放位置以及控件在容器中的位置比例
            ht.Add("TxTabwidth", (double)this.ClientSize.Width);
            ht.Add("TxTabheight", (double)this.ClientSize.Height);
            foreach (Control ctrl in this.Controls)
            {
                //ht.Add(ctrl.Name + "X", ctrl.Location.X / (double)this.Width);//存储控件在容器中的相对比例
                //ht.Add(ctrl.Name + "Y", ctrl.Location.Y / (double)this.Height);//存储控件在容器中的相对比例
                //ht.Add(ctrl.Name + "Height", ctrl.Height);
                //ht.Add(ctrl.Name + "Width", ctrl.Width);
                ht.Add(ctrl.Name + "Left", (double)ctrl.Left);
                ht.Add(ctrl.Name + "Top", (double)ctrl.Top);
                ctrl.Tag = ctrl.Size;
            }
            oHash = ht;
        }
        private void MoveSliderLocation()
        {

            if (DesignMode) { return; }
            Hashtable scale = (Hashtable)oHash;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Left = (int)(this.ClientSize.Width * 1.0d / (double)scale["TxTabwidth"] * (double)scale[ctrl.Name + "Left"]);
                ctrl.Top = (int)(this.ClientSize.Height * 1.0d / (double)scale["TxTabheight"] * (double)scale[ctrl.Name + "Top"]);
                ctrl.Width = (int)(this.ClientSize.Width * 1.0d / (double)scale["TxTabwidth"] * ((Size)ctrl.Tag).Width);
                ctrl.Height = (int)(this.ClientSize.Height * 1.0d / (double)scale["TxTabheight"] * ((Size)ctrl.Tag).Height);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SaveSliderLocation();
            this.Resize += new System.EventHandler(this.win_SizeChanged);
            BindControlsEvent();

            if (_BoardTest)
            {
                
                sRegisterFileName = this.BoardName + ".xml";
                sCurrentFileName = this.BoardName + "_current.xml";
                sDefaultFileName = this.BoardName + "_default.xml";
                if (!_IVITest)
                {
                    BoardPowerOn();
                }
                if (_LoadXml)
                {
                    if (_IVITest)
                    {
                        BOOARDINFO.LoadRegInfoFromConfigFile(this.BoardName, System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sRegisterFileName);
                    }
                    else
                    {

                        if (!this.BoardName.Contains("Debug") && sender != null)
                        {
                            BOOARDINFO.LoadRegInfoFromConfigFile(this.BoardName, System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sRegisterFileName);
                        }
                    }


                }
            }
            else
            {
                if (_LoadXml)
                {
                    sRegisterFileName = this.BoardName + ".xml";
                    BOOARDINFO.LoadRegInfoFromConfigFile(this.BoardName, System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sRegisterFileName);

                }
            }
            //加载各个控件信息
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                if (control is UserControlBase)
                {
                    (control as UserControlBase).Init(this.BoardName, _BoardTest);
                }
            }
            //恢复上次的状态
            if (_LoadXml)
            {
                if (_IVITest)
                {
                }
                else
                {
                    RecoverControlStatus(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sCurrentFileName, _RecoverToHw);
                }
            }
            else
            {
                if (_LoadXml)
                {
                }
            }

        }
        public void win_SizeChanged(object sender, EventArgs e)
        {
            MoveSliderLocation();
        }
        //public void RecoverModuleControlStatus(string xmlFullName, string BoardName,bool WriteToHw)
        //{
        //    if (_BoardTest)
        //    {
        //        Dictionary<string, Dictionary<string, string>> ModuleRegVaules = new Dictionary<string, Dictionary<string, string>>();
        //        BOOARDINFO.GetModuleRegValuesByXML(xmlFullName, ref ModuleRegVaules);
        //        if (_IVITest)
        //        {
        //            foreach(string key in ModuleRegVaules[BoardName].Keys)
        //            {
        //                foreach (System.Windows.Forms.Control control in this.Controls)
        //                {
        //                    if (control is UserControlBase)
        //                    {
        //                        (control as UserControlBase).RefreshControl(key, ModuleRegVaules[BoardName][key]);
        //                    }

        //                }
        //                if (WriteToHw)
        //                {
        //                    Dut.Write( BOOARDINFO.Registers[this.BoardName][key].Group,  BOOARDINFO.Registers[this.BoardName][key].IVIName, Convert.ToInt64(ModuleRegVaules[BoardName][key], 2));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            foreach (string key in ModuleRegVaules[BoardName].Keys)
        //            {
        //                foreach (System.Windows.Forms.Control control in this.Controls)
        //                {
        //                    if (control is UserControlBase)
        //                    {
        //                        (control as UserControlBase).RefreshControl(key, ModuleRegVaules[BoardName][key]);
        //                    }

        //                }
        //                if (WriteToHw)
        //                {
        //                    Dut.Write(key, Convert.ToInt64(ModuleRegVaules[BoardName][key], 2));
        //                }
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 在IVI和FPGA模式下，根据XML文件恢复各个寄存器状态以及UI
        /// FPGA模式下有恢复DAC功能
        /// 
        /// </summary>
        /// <param name="xmlFullName"></param>
        /// <param name="WriteToHw"></param>
        public void RecoverControlStatus(string xmlFullName, bool WriteToHw)
        {
            if (_BoardTest)
            {
                List<string> RegName = new List<string>();
                List<string> RegValue = new List<string>();
                List<string> DACName = new List<string>();
                List<string> DACValue = new List<string>();
                BOOARDINFO.GetRegAndDACValuesByXML(xmlFullName, ref RegName, ref RegValue, ref DACName, ref DACValue);
                if (_IVITest)
                {
                    for (int i = 0; i < RegName.Count; i++)
                    {
                        foreach (System.Windows.Forms.Control control in this.Controls)
                        {
                            if (control is UserControlBase)
                            {
                                (control as UserControlBase).RefreshControl(RegName[i], RegValue[RegName.IndexOf(RegName[i])]);
                            }
                        }
                        if (WriteToHw)
                        {
                            Dut.Write( BOOARDINFO.Registers[this.BoardName][RegName[i]].Group,  BOOARDINFO.Registers[this.BoardName][RegName[i]].IVIName, Convert.ToInt64(RegValue[RegName.IndexOf(RegName[i])], 2));
                        }
                    }
                }
                else
                {
                    #region 恢复DAC状态和值
                    foreach (System.Windows.Forms.Control control in this.Controls)
                    {
                        if (control is UserControlBase)
                        {
                            if (control is DAC)
                            {
                                for (int i = 0; i < DACName.Count; i++)
                                {
                                    if ((control as DAC).SaveValue && (control as DAC).RegisterName == DACName[i])
                                    {
                                        (control as DAC).ControlDevice(decimal.Parse(DACValue[DACName.IndexOf(DACName[i])]));
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region 恢复普通寄存器的值以及对应开关的状态
                    for (int i = 0; i < RegName.Count; i++)
                    {
                        foreach (System.Windows.Forms.Control control in this.Controls)
                        {
                            if (control is UserControlBase && (control as UserControlBase).RegisterName== RegName[i])
                            {
                                (control as UserControlBase).RefreshControl(RegName[i], RegValue[RegName.IndexOf(RegName[i])]);
                            }
                        }
                        if (WriteToHw && !Dut.Status)
                        {
                            Dut.Write(RegName[i], Convert.ToInt64(RegValue[RegName.IndexOf(RegName[i])], 2));
                        }
                    }
                    #endregion
                }
                if (xmlFullName != System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sCurrentFileName)
                {
                    string DefaultOpFile = xmlFullName;
                    string DestOpFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sCurrentFileName;
                    File.Copy(DefaultOpFile, DestOpFile, true);
                }
            }
        }
        public void BaseSetRegByBits(string RegisterName,string BitNames,string BitsValue)
        {
            //Register RG = new Register();
            //sRegisterFileName = this.BoardName + ".xml";
            //RG.Bind(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sRegisterFileName, RegisterName);
            //RG.SetBitValue(BitNames, BitsValue);
            //if (_BoardTest)
            //{
            //    Dut.Write(RegisterName, Convert.ToInt64(RG.Value, 2));

            //}
        }
        public void BindControlsEvent()
        {

            foreach (System.Windows.Forms.Control ctl in this.Controls)
            {
                if (ctl is UserControlBase)
                {

                    ctl.Click += new System.EventHandler(this.Control_Click);
                    (ctl as UserControlBase).ControlChange += new MyControl.Switch.MyEventHandler(this.OnControlChang);
                }
            }

        }

        //public void SaveRegValue(string RegName, int regSize)
        //{
        //    long lData = Dut.Read(RegName);
        //    string sValue = Convert.ToString((Int32)lData, 2).PadLeft(regSize, '0');
        //    XmlEditor XmlOperation = new XmlEditor();
        //    if (!XmlOperation.LoadFromXmlFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sCurrentFileName)) //BlockSet是XML文件名称
        //    {
        //        XmlOperation.CreateXmlFile();
        //    }
        //    if (XmlOperation.GetNode("/root/register[@Name='" + RegName + "']") == null)
        //    {
        //        XmlOperation.CurNode = XmlOperation.CreateNode(XmlOperation.GetRootNode(), "register", sValue);
        //        XmlOperation.CreateAttribute(XmlOperation.CurNode, "Name", RegName);
        //    }
        //    else
        //    {
        //        XmlOperation.SetNodeText("/root/register[@Name='" + RegName + "']", sValue);
        //    }
        //    try
        //    {
        //        XmlOperation.SaveXml(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sCurrentFileName);
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show( "BaseForm:\n SaveLog->" + e.ToString());
        //    }
        //}


        public void Control_Click(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case "MyControl.Switch":
                    (sender as Switch).ControlDevice();
                    break;
                case "MyControl.SliderSwitch":
                    (sender as SliderSwitch).ControlDevice();
                    break;
                case "MyControl.AMP":
                    (sender as AMP).ControlDevice();
                    break;
                case "MyControl.SliderPowerSwitch":
                    if(_BoardTest && !_IVITest )
                    {
                        (sender as SliderPowerSwitch).ControlDevice();
                    }
                    else
                    {
                        MessageBox.Show("BaseForm->RefreshOneOfSliderByROM\n" + "IVI或System Slider不支持该刷新");
                    }   
                    break;
            }
            (sender as Control).Invalidate();
            ActionAfterClick(sender);
        }
        public virtual void ActionAfterClick(dynamic obj)
        {

        }
        public void OnControlChang(object sender, UserControlBase.MyEventArges e)
        {
            UserControlBase which = null;
            which = sender as UserControlBase;       
            if (e.Value == null )
            {
                ErrMsg(this.GetType().Name+ "->set value is null!");
                return;
            }

            ActionBeforeWriteToHw(which);

            #region 显示发送给硬件的信息
            //每四位分段
            string str = e.Value;  //寄存器的值二进制表示
            for (int i = 0; i < e.Value.Length / 4 - 1; i++)
            {
                str = str.Insert((i + 1) * 4 + i, ".");
            }
            //若有信息提示控件则将信息显示其中，否则显示在窗口标题中
            if (FrameRichTextBox != null)
            {
                FrameRichTextBox.Text = which.RegisterName + ":" + str;
                FrameRichTextBox.Refresh();
                if (lastValue != "")
                {
                    if (lastValue.Length == str.Length)
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (lastValue[i] != str[i])
                            {
                                FrameRichTextBox.Select(which.RegisterName.Length + i + 1, 1);
                                FrameRichTextBox.SelectionColor = Color.Red;
                            }
                        }
                    }
                }
                lastValue = str;
            }
            else
            {
                this.Text = which.RegisterName + ":" + str;
            }
            #endregion 显示发送给硬件的信息

            //根据regvalue刷新UI
            which.ResponseControl(e.Value); 

            WriteHardware(which.RegisterName, e.Value);

            ActionAfterWriteToHw(which);

            if(sender is SliderPowerSwitch)
            {
                RefreshOneOfSliderByROM((sender as SliderPowerSwitch).RegisterName);
            }


            #region 关联开关联动
            if (e.RelatedSwitch != "")
            {
            if(this.Controls[which.Name] is  null)
            {

            }
            else
            {
                foreach (System.Windows.Forms.Control control in this.Controls)
                {

                    if (control is UserControlBase)
                    {
                        if ((control as UserControlBase).SwitchName == e.RelatedSwitch && e.RelatedSwitch != "")
                        {
                            if (control is SliderPowerSwitch)
                            {

                                Thread.Sleep((control as SliderPowerSwitch).RelatedDelayTime);
                            }
                            (control as UserControlBase).ControlDevice(which.CurrentWay);
                            (control as UserControlBase).Invalidate();
                        }

                    }
                }
            }

        }
        #endregion
        }
        /// <summary>
        /// Slider写硬件的方法
        /// </summary>
        /// <param name="EquipmentName"></param>
        /// <param name="str"></param>
        public virtual void WriteHardware(string RegName, string str)
        {
            if (!Dut.Status)
            {
                return;
            }
            if (str.Contains("|"))
            {
                string[] sCmds = str.Split('|');
                for (int i = 0; i < sCmds.Length; i++)
                {
                    if (_BoardTest)
                    {
                        MessageBox.Show("单板测试并不支持该方式！");
                    }
                    else
                    {
                        Dut.WriteLine(sCmds[i]);
                    }
                }
            }
            else
            {
                if (_BoardTest)
                {
                    if (_IVITest)
                    {
                        if ( BOOARDINFO.Registers[this.BoardName].ContainsKey(RegName))
                            Dut.Write( BOOARDINFO.Registers[this.BoardName][RegName].Group,  BOOARDINFO.Registers[this.BoardName][RegName].IVIName, Convert.ToInt64(str, 2));
                        else
                            MessageBox.Show("没有找到Register相关IVI信息","SliderBaseFrom");
                    }
                    else
                    {
                        Dut.Write(RegName, Convert.ToInt64(str, 2));
                    }
                    
                }
                else
                {
                    Dut.WriteLatch(string.Format("Source ExternalRf:{0}-{1}",  BOOARDINFO.Registers[this.BoardName][RegName].Group,  BOOARDINFO.Registers[this.BoardName][RegName].IVIName), Convert.ToUInt64(str, 2));
                    //Dut.WriteLatch( BOOARDINFO.Registers[this.BoardName][EquipmentName].Group,  BOOARDINFO.Registers[this.BoardName][EquipmentName].IVIName, Convert.ToInt64(str, 2));
                }
            }
        }
        /// <summary>
        /// 从硬件读取配置表中的寄存器值并刷新界面
        /// </summary>
        /// <param name="AllBoard"></param>
        public void RefreshSliderByHW(bool AllBoard)
        {
            long data = 0;
            string sData;
            List<string> RegNames = new List<string>();
            if (_BoardTest)
            {
                if (_IVITest) //IVI 方式
                {
                    RegNames = BOOARDINFO.Registers[this.BoardName].Keys.ToList();
                    foreach (string regName in RegNames)
                    {
                        data = Dut.Read(BOOARDINFO.Registers[this.BoardName][regName].Group, BOOARDINFO.Registers[this.BoardName][regName].IVIName);
                        sData = Convert.ToString((UInt32)data, 2).PadLeft(BOOARDINFO.Registers[this.BoardName][regName].Size, '0');
                        BOOARDINFO.UpdateCurFile(sCurrentFileName,regName, data,  BOOARDINFO.Registers[this.BoardName][regName].Size);
                        WriteLog("Read GroupName=" +  BOOARDINFO.Registers[this.BoardName][regName].Group + ":IVIName=" +  BOOARDINFO.Registers[this.BoardName][regName].IVIName + ":Value=" + Convert.ToString((UInt32)data) + ":" + sData, false);
                        sData = Convert.ToString((UInt32)data, 2).PadLeft( BOOARDINFO.Registers[this.BoardName][regName].Size, '0');

                        foreach (System.Windows.Forms.Control control in this.Controls)
                        {
                            if (control is UserControlBase)
                            {
                               
                                bool  rs= (control as UserControlBase).RefreshControl(regName, sData);
                            }
                        }
                    }    
                }
                else//FPGA 方式
                {
                    RegNames = BOOARDINFO.Registers[this.BoardName].Keys.ToList();
                    foreach (string regName in RegNames)
                    {
                        data = Dut.Read(regName);
                        sData = Convert.ToString(data, 2).PadLeft(Dut.GetEquipment(regName).Size, '0');
                        foreach (System.Windows.Forms.Control control in this.Controls)
                        {
                            if (control is UserControlBase)
                            {
                                (control as UserControlBase).RefreshControl(regName, sData);
                            }
                        }
                    }
                }
            }
            else//SCPI 方式
            {
                RegNames = BOOARDINFO.Registers[this.BoardName].Keys.ToList();
                foreach (string regName in RegNames)
                {
                    UInt64 data1 = Dut.ReadLatch(string.Format("Source ExternalRf:{0}-{1}",  BOOARDINFO.Registers[this.BoardName][regName].Group,  BOOARDINFO.Registers[this.BoardName][regName].IVIName));
                    sData = Convert.ToString((UInt32)data1, 2).PadLeft( BOOARDINFO.Registers[this.BoardName][regName].Size, '0');
                    //UpdateCurFile(regName, data,  BOOARDINFO.Registers[this.BoardName][regName].Size);
                    WriteLog("Read GroupName=" +  BOOARDINFO.Registers[this.BoardName][regName].Group + ":IVIName=" +  BOOARDINFO.Registers[this.BoardName][regName].IVIName + ":Value=" + Convert.ToString((UInt32)data) + ":" + sData, false);
                    sData = Convert.ToString((UInt32)data1, 2).PadLeft( BOOARDINFO.Registers[this.BoardName][regName].Size, '0');
                    foreach (System.Windows.Forms.Control control in this.Controls)
                    {
                        if (control is UserControlBase)
                        {
                            (control as UserControlBase).RefreshControl(regName, sData);
                        }
                    }
                }  
            }
        }
        public void RefreshOneOfSliderByHW(string regName)
        {
            long data=0;
            string sData;
            if (_BoardTest)
            {
                if (_IVITest)
                {

                    if ( BOOARDINFO.Registers[this.BoardName].ContainsKey(regName))
                    {
                        data = Dut.Read( BOOARDINFO.Registers[this.BoardName][regName].Group,  BOOARDINFO.Registers[this.BoardName][regName].IVIName);
                        sData = Convert.ToString((UInt32)data, 2).PadLeft( BOOARDINFO.Registers[this.BoardName][regName].Size, '0');
                        BOOARDINFO.UpdateCurFile(sCurrentFileName,regName, data,  BOOARDINFO.Registers[this.BoardName][regName].Size);
                        WriteLog("Read GroupName=" +  BOOARDINFO.Registers[this.BoardName][regName].Group + ":IVIName=" +  BOOARDINFO.Registers[this.BoardName][regName].IVIName + ":Value=" + Convert.ToString((UInt32)data) + ":" + sData, false);
                        
                    }
                    else
                    {
                        MessageBox.Show("没有找到" + regName + "相关IVI信息", "SliderBaseFrom");
                        return;
                    }
                    sData = Convert.ToString((UInt32)data, 2).PadLeft( BOOARDINFO.Registers[this.BoardName][regName].Size, '0');
                }
                else
                {
                    data = Dut.Read(regName);
                    sData = Convert.ToString(data, 2).PadLeft(Dut.GetEquipment(regName).Size, '0');
                }

                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    if (control is UserControlBase)
                    {
                        (control as UserControlBase).RefreshControl(regName, sData);

                    }

                }
            }
            else// System Level 
            {
                if ( BOOARDINFO.Registers[this.BoardName].ContainsKey(regName))
                {
                    data = Dut.ReadLatch(string.Format("Source ExternalRf:{0}-{1}",  BOOARDINFO.Registers[this.BoardName][regName].Group,  BOOARDINFO.Registers[this.BoardName][regName].IVIName));
                    sData = Convert.ToString((UInt32)data, 2).PadLeft( BOOARDINFO.Registers[this.BoardName][regName].Size, '0');
                    BOOARDINFO.UpdateCurFile(sCurrentFileName,regName, data,  BOOARDINFO.Registers[this.BoardName][regName].Size);
                    WriteLog("Read " + string.Format("Source ExternalRf:{0}-{1}",  BOOARDINFO.Registers[this.BoardName][regName].Group,  BOOARDINFO.Registers[this.BoardName][regName].IVIName) + " : Value= " + Convert.ToString((UInt32)data) + ":" + sData, true);

                }
                else
                {
                    MessageBox.Show("没有找到" + regName + "相关IVI信息", "SliderBaseFrom");
                    return;
                }
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    if (control is UserControlBase)
                    {
                        (control as UserControlBase).RefreshControl(regName, sData);

                    }

                }
            }

        }

        public void RefreshOneOfSliderByROM(string regName)
        {
            if (_BoardTest)
            {
                if (_IVITest)
                {
                    MessageBox.Show("BaseForm->RefreshOneOfSliderByROM\n" + "IVI不支持该刷新");    
                }
                else
                {
                    if (BOOARDINFO.Registers[this.BoardName].ContainsKey(regName))
                    {
                        foreach (System.Windows.Forms.Control control in this.Controls)
                        {
                            if (control is UserControlBase)
                            {
                                (control as UserControlBase).RefreshControl(regName, BOOARDINFO.CurRegValue[regName]);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("BaseForm->RefreshOneOfSliderByROM\n" + "界面属性所需寄存器在ROM未找到！");
                        return;
                    }
                }


            }
            else// System Level 
            {
                MessageBox.Show("BaseForm->RefreshOneOfSliderByROM\n" + "System不支持该刷新");
            }

        }

        private void saveStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DefaultOpFile;
            string DestOpFile;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xml files   (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            DefaultOpFile = sCurrentFileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((DestOpFile = saveFileDialog1.FileName) != null)
                {
                    File.Copy(DefaultOpFile, DestOpFile, true);
                }
            }
        }
        private void loadStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SourceOpFile;
            string DestOpFile;
            //rxDriver.init();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml files   (*.xml)|*.xml";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if ((SourceOpFile = dialog.FileName) != null)
                {
                    RecoverControlStatus(SourceOpFile,true);
                    //dut.writeData();
                    //FrequencyTuning.Value = 0;
                    DestOpFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sCurrentFileName;
                    File.Copy(SourceOpFile, DestOpFile, true);
                }
            }  
        }
        private void saveAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DefaultOpFile = sCurrentFileName;
            string DestOpFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sDefaultFileName;
            File.Copy(DefaultOpFile, DestOpFile, true);
        }
        public void BaseAddPairSwitch(string CallName, MyControl.Switch S1, MyControl.Switch S2)
        {
            if (CallName == S1.Name)
                S2.ControlDevice(S1.CurrentWay);
            else if (CallName == S2.Name)
                S1.ControlDevice(S2.CurrentWay);
        }
        public void BaseAddPairSwitch(string CallName, MyControl.SliderSwitch S1, MyControl.SliderSwitch S2)
        {
            if (CallName == S1.Name)
                S2.ControlDevice(S1.CurrentWay);
            else if (CallName == S2.Name)
                S1.ControlDevice(S2.CurrentWay);
        }
        Dictionary<string, string> SwitchPathValue = new Dictionary<string, string>();
        public void BaseAddSwitchValue(string switchName, string pathValue)
        {
            if (!SwitchPathValue.Keys.Contains(switchName))
            {
                SwitchPathValue.Add(switchName, pathValue);
            }
        }
        private void WriteLog(string log, bool Enable)
        {
            if (rTB_Operation!=null)
            {
                rTB_Operation.AppendText(log+"\n");
            }
            
            if (Enable)
            {
                FileStream fi;
                StreamWriter sw;
                string logFileName = "RegisterInputLog.txt";
                try
                {
                    fi = new FileStream(Application.StartupPath.ToString() + "\\" + logFileName, FileMode.Append);
                    sw = new StreamWriter(fi);
                    sw.WriteLine(log);
                    sw.Close();
                    sw = null;
                    fi.Close();
                    fi = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }

        }
        public virtual void BoardPowerOn()
        {

        }
        public virtual void BoardPowerOff()
        {

        }
        public virtual void ActionBeforeWriteToHw(dynamic sliderController)
        {

        }
        public virtual void ActionAfterWriteToHw(dynamic sliderController)
        {

        }
        public void WarningMsg(string info)
        {
            MessageBox.Show(info, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void ErrMsg(string info)
        {
            MessageBox.Show(info, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void InfomationMsg(string info)
        {
            MessageBox.Show(info, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
