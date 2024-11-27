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
    public partial class BoardFlashControl : BaseForm
    {
        private static BoardFlashControl instance;
        private static readonly object obj = new object();
        //BoardDriver.VXT3BoardDriver Dut;
        int BoardId;
        public BoardFlashControl(BoardDriver.FTDIBoardDriver Dut, string boardName)
        {
            InitializeComponent();
            this.Dut = Dut;
            this.BoardId = this.Dut.GetBoard(boardName).ID;
            this.Text = boardName + " FalshControl";
        }
        public BoardFlashControl(BoardDriver.FTDIBoardDriver Dut)
        {
            InitializeComponent();
            this.Dut = Dut;
        }
        public BoardFlashControl()
        {
            InitializeComponent();

        }
        public static BoardFlashControl GetInstance(BoardDriver.FTDIBoardDriver Dut, string boardName)
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new BoardFlashControl(Dut, boardName);
                    }
                }
            }
            return instance;
        }
        private void EraseBtn_Click(object sender, EventArgs e)
        {
            Dut.erasePage(this.BoardId, 1);
        }

        private void ReadFlash_Click(object sender, EventArgs e)
        {
            string offsetStr = addrOffsetTB.Text;
            int offset = Convert.ToInt32(offsetStr, 16);
            string addrStr = Convert.ToString(0x100000 + offset, 16);
            richTextBox1.Text = Dut.ReadHeaderToString(this.BoardId, addrStr);
        }

        private void WriteFlash_Click(object sender, EventArgs e)
        {
            string offsetStr = addrOffsetTB.Text;
            int offset = Convert.ToInt32(offsetStr, 16);
            string addrStr = Convert.ToString(0x100000 + offset, 16);
            Dut.WriteFalsh(this.BoardId, addrStr, richTextBox1.Text.ToString()); 
        }

        private void BoardFlashControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }
    }
}
