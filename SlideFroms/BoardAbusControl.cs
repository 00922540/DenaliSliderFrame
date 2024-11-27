using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlideBase;
namespace SlideFroms
{
    public partial class BoardAbusControl : BaseForm
    {
        BoardDriver.FTDIBoardDriver Dut;
        private static BoardAbusControl instance;
        private static readonly object obj = new object();
        dynamic Board;
        public BoardAbusControl(BoardDriver.FTDIBoardDriver Dut, dynamic Board)
        {
            InitializeComponent();
            this.Dut = Dut;
            this.Board = Board;
            initControl();
            Dut.Debug_P1.startExtAdc();
            Dut.SetBoard(Board.ID);
            this.Text = this.Text + ":" + Board.BoardName;
        }
        public static BoardAbusControl GetInstance(BoardDriver.FTDIBoardDriver Dut, dynamic Board)
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new BoardAbusControl(Dut, Board);
                    }
                }
            }
            return instance;
        }
        private void initControl()
        {
            cbAbusNames.DataSource = Board.AbusList;
            cbAbusNames.DisplayMember = "Name";
            cbAbusNames.ValueMember = "Addr";
        }
        private void Read_Click(object sender, EventArgs e)
        {
            string abusName = cbAbusNames.Text;
            double abusAdc = 0;
            double value = Board.readAbusValue(abusName, Board.readAComValue(), out abusAdc);
            richTextBox1.Text = Math.Round(value, 3).ToString() + "\n" + abusAdc.ToString();
        }

        private void RefreshAll_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            double acomvalue = Board.readAComValue();
            foreach (dynamic Abus in Board.AbusList)
            {
                double abusAdc = 0;
                double value = Board.readAbusValue(Abus.Name, acomvalue, out abusAdc);
                richTextBox1.AppendText(Abus.Name + " : " + Math.Round(value,3).ToString() + '\n');
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        private void BoardAbusControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }


    }
}
