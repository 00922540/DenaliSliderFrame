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
    public partial class BoardHeader : BaseForm
    {
        BoardDriver.FTDIBoardDriver Dut;
        private static BoardHeader instance;
        private static readonly object obj = new object();
        dynamic Board;
        public BoardHeader(BoardDriver.FTDIBoardDriver Dut, dynamic Board)
        {
            InitializeComponent();
            this.Dut = Dut;
            this.Board = Board;
            Dut.SetBoard(Board.ID);
            this.Text = this.Text + ":" + Board.BoardName;
        }
        public static BoardHeader GetInstance(BoardDriver.FTDIBoardDriver Dut, dynamic Board)
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new BoardHeader(Dut, Board);
                    }
                }
            }
            return instance;
        }

        private void BoardHeader_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        private void bt_Write_Click(object sender, EventArgs e)
        {
            string data = textBox1.Text + "," + "{BoardRevision}" + "," + textBox3.Text + "," + "{SerialNumber}" + "," + textBox5.Text + "," + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text+"," + textBox9.Text;
            string[] text = new string[] { textBox2.Text,textBox4.Text,  data };

           // Dut.SetHeader(Board.ID, text);
        }

        private void bt_Read_Click(object sender, EventArgs e)
        {
            //string[] info = Dut.GetHeader(Board.ID).Split(',');
            //if (info.Count()<9)
            //{
            //    return;
            //}
            //textBox1.Text = info[0];
            //textBox2.Text = info[1];
            //textBox3.Text = info[2];
            //textBox4.Text = info[3];
            //textBox5.Text = info[4];
            //textBox6.Text = info[5];
            //textBox7.Text = info[6];
            //textBox8.Text = info[7];
            //textBox9.Text = info[8];

        }


    }
}
