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
using SlideBase;
namespace SlideFroms
{
    
    public partial class MainForm : Form
    {
        dynamic Dut;
        //BoardDriver.VXT3BoardDriver Dut;
        bool bBoardTest;
        bool bIVITest;
        List<int> ilOnLineTab = new List<int>();
        List<int> iFunTab = new List<int>();
        List<string> UpLoadBoards;
        public dynamic frm_MI;
        public MainForm()
        {

            InitializeComponent();
        }
        public MainForm(Object oDut,bool bBoardTest,bool bIVITest,string[] sBoards)
        {
            //this.WindowState = FormWindowState.Maximized;
            Dut = oDut;
            this.bBoardTest = bBoardTest;
            this.bIVITest = bIVITest;
            UpLoadBoards = sBoards.ToList();
            InitializeComponent();
            if (bBoardTest & !bIVITest)
            {
                if (bIVITest)
                {
                    label_Mode.Text = "Module Mode";
                }
                else
                {
                    label_Mode.Text = "Board Mode";
                }     
            }
            else
            {
                label_Mode.Text = "System Mode";
            }
    
        }
        public MainForm(Object oDut, string TestLevel, string[] sBoards)
        {
            //this.WindowState = FormWindowState.Maximized;
            Dut = oDut;
            UpLoadBoards = sBoards.ToList();
            InitializeComponent();
            switch(TestLevel)
            {
                case "Board":
                    label_Mode.Text = "Board Mode";
                    this.bBoardTest = true;
                    this.bIVITest = false;
                    break;
                case "Module":
                    label_Mode.Text = "Module Mode";
                    this.bBoardTest = true;
                    this.bIVITest = true;
                    break;
                case "System":
                    label_Mode.Text = "System Mode";
                    this.bBoardTest = false;
                    this.bIVITest = false;
                    break;
            }
        }
        public MainForm(dynamic Mother)
        {
            InitializeComponent();
            Dut = Mother.dut;
            UpLoadBoards = ((string[])Mother.sBoards).ToList();
            this.Text = Mother.Text;
            switch ((string)Mother.sTestLevel)
            {
                case "Board":
                    label_Mode.Text = "Board Mode";
                    this.bBoardTest = true;
                    this.bIVITest = false;
                    break;
                case "Module":
                    label_Mode.Text = "Module Mode";
                    this.bBoardTest = true;
                    this.bIVITest = true;
                    break;
                case "System":
                    label_Mode.Text = "System Mode";
                    this.bBoardTest = false;
                    this.bIVITest = false;
                    break;
            }
            //this.bBoardTest = Mother.bBoardTest;
            //this.bIVITest = Mother.bIVITest;
            //UpLoadBoards = ((string[])Mother.sBoards).ToList();
            //this.Text = Mother.Text;
            //if (bBoardTest & !bIVITest)
            //{
            //    if (bIVITest)
            //    {
            //        label_Mode.Text = "Module Mode";
            //    }
            //    else
            //    {
            //        label_Mode.Text = "Board Mode";
            //    }
            //}
            //else
            //{
            //    label_Mode.Text = "System Mode";
            //}
    
        }
        private void Add_TabPage(string str, BaseForm myForm)
        {
            if (tabControlCheckHave(this.TabControl, str))
            {
                return;
            }
            else
            {
                TabControl.TabPages.Add(str);
                TabControl.SelectTab(TabControl.TabPages.Count - 1);
                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.Dock = DockStyle.Fill;
                myForm.TopLevel = false;
                myForm.Show();
                myForm.Parent = TabControl.SelectedTab;
                TabControl.SelectedTab.Name = myForm.BoardName;

            }
        }
        private bool tabControlCheckHave(System.Windows.Forms.TabControl tab, String tabName)
        {
            for (int i = 0; i < tab.TabCount; i++)
            {
                if (tab.TabPages[i].Text == tabName)
                {
                    tab.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }
        public Form getForm(string AssemblyName,Object oDut)
        {
            Type testType = Type.GetType(AssemblyName);
            Form testInstance = (Form)Activator.CreateInstance(testType);
            testType.GetField("Dut").SetValue(testInstance, oDut);
            testType.GetField("_BoardTest").SetValue(testInstance, bBoardTest);
            testType.GetField("_IVITest").SetValue(testInstance, bIVITest);
            testType.GetField("FrameRichTextBox").SetValue(testInstance, this.MsgBox);//MessageBox
            testType.GetField("rTB_Operation").SetValue(testInstance, this.rTB_Operation);
            return testInstance;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Dut.Status)
            {
                this.Text = this.Text + " - connected";
            }
            else
            {
                this.Text = this.Text + " - disconnect";
            }
            //根据每个BoardSlider的类名反射加载Slider
            foreach(string sBoard in UpLoadBoards)
            {
                BaseForm SliderFrm = getForm("SlideFroms." + sBoard, Dut);
                Add_TabPage(sBoard, SliderFrm);
                //if (Dut.CheckBoardOnLine(Dut.GetBoard(TabControl.SelectedTab.Name).ID))
                //{
                //    iOnLineTab = TabControl.SelectedIndex;
                //}
            }
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Width = 1024;
            //this.Height = 700;
            this.WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 20;
            //设置当前BoardSlider
            if (bBoardTest & !bIVITest)
            {
                for (int i = 0; i < TabControl.TabPages.Count; i++)
                {
                    if (TabControl.TabPages[i].Name != "tabPage1")
                    {
                        //if (Dut.CheckBoardOnLine(Dut.GetBoard(TabControl.TabPages[i].Name).ID))
                        //{
                            ilOnLineTab.Add(i);
                        //}
                    }
                    else
                    {
                        iFunTab.Add(i);
                    }

                }
            }
            else
            {
                if (Dut.Status)
                {
                    for (int i = 0; i < TabControl.TabPages.Count; i++)
                    {
                        if (TabControl.TabPages[i].Name != "tabPage1")
                        {
                            ilOnLineTab.Add(i);
                        }
                        else
                        {
                            iFunTab.Add(i);
                        }
                        
                    }
                }
            }

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bBoardTest & !bIVITest)
            {
                //关闭Slider时，分别执行每个BoardSlider的BoardPowerOff进行掉电。
                for (int i = 0; i < TabControl.TabCount; i++)
                {
                    if (TabControl.TabPages[i].Name != "tabPage1")
                    {
                        (TabControl.TabPages[i].Controls[0] as BaseForm).BoardPowerOff();
                    }

                }
                //Debug板掉电
                //Dut.Write(Dut.Debug.Debug_Rx_PWR_CTRL.EquipmentName, 0x0);
            }

            Dut.Close();
            System.Environment.Exit(0);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bBoardTest&!bIVITest)
            {
                //点击不同BoardSlider时进行底层选片操作
                if (TabControl.SelectedTab.Name != "")
                {
                    //Dut.SetBoard(Dut.GetBoard(TabControl.SelectedTab.Name).ID);
                }
            }
        }

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            #region 重绘标签头=======================
            SolidBrush back;
            SolidBrush white;
            if (ilOnLineTab.Contains(e.Index))//当前Tab page页的样式
            {
                //背景颜色
                back = new SolidBrush(Color.GreenYellow);
                //字体颜色
                white = new SolidBrush(Color.Black);
            }
            else if (iFunTab.Contains(e.Index))
            {
                //背景颜色
                back = new SolidBrush(Color.DarkGray);
                //字体颜色
                white = new SolidBrush(Color.Black);
            }
            else//其余Tab page页的样式
            {
                //背景颜色
                back = new SolidBrush(Color.Red);
                //字体颜色
                white = new SolidBrush(Color.Black);
            }
            StringFormat StringF = new StringFormat();
            //设置文字对齐方式
            StringF.Alignment = StringAlignment.Center;
            StringF.LineAlignment = StringAlignment.Center;
            //绑定选项卡
            Rectangle rec = TabControl.GetTabRect(e.Index);
            //设置选项卡背景
            e.Graphics.FillRectangle(back, rec);
            //设置选项卡字体及颜色
            e.Graphics.DrawString(TabControl.TabPages[e.Index].Text, new Font("Microsoft Sans Serif", 8, FontStyle.Regular), white, rec, StringF);
            #endregion
        }



    }
}
