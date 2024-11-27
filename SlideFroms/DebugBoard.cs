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
using BoardDriver;
using System.Threading;
namespace SlideFroms
{
    public partial class DebugBoard :BaseForm
    {
        //BoardDriver.VXT3BoardDriver Dut;
        private static DebugBoard instance;
        private static readonly object obj = new object();
        public DebugBoard()
        {
            InitializeComponent();

        }
        public DebugBoard(BoardDriver.FTDIBoardDriver Dut)
        {
            InitializeComponent();

        }
        public static DebugBoard GetInstance(BoardDriver.FTDIBoardDriver Dut)
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new DebugBoard(Dut);
                    }
                }
            }
            return instance;
        }
        private void InitAbusList(dynamic board)
        {
            cbAbusNames.DataSource = board.AbusList;
            cbAbusNames.DisplayMember = "Name";
            cbAbusNames.ValueMember = "Addr";
        }
        private void SlideForm_Load(object sender, EventArgs e)
        {
            
            Dut.startExtAdc();
            cB_Boards.DataSource = Dut.GetAllBoardNames();
            InitAbusList(Dut.GetBoard(cB_Boards.Text));
            this.Resize += new System.EventHandler(this.win_SizeChanged);
            SaveSliderLocation();

            dataGridView2.ColumnCount = 32;
            for (int i = 0; i < 32; i++)
            {
                dataGridView2.Columns[i].Name = (31 - i).ToString();
                dataGridView2.Columns[i].HeaderText = (31 - i).ToString();
                dataGridView2.Columns[i].Width = 28;
            }

            bt_Init100M.Visible = false;
            cB_RxMode.Checked = true;
        }

        private void bt_Read_Click(object sender, EventArgs e)
        {
            long rs = Dut.Read(cB_Registers.Text);
            if(rs==Int64.MaxValue)
            {
                return;
            }

            tB_M_Value.Text = Convert.ToString(rs, 16);

            string temp = Convert.ToString(rs, 2);
            temp = temp.PadLeft(32, '0');
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add();
            //DataGridViewRow row = new DataGridViewRow();
            for (int i = 0; i < Convert.ToInt32(tB_M_DataSize.Text); i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = temp[i];
            }
        }

        private void bt_Write_Click(object sender, EventArgs e)
        {
            //string str = ""; ;
            //for (int i = 0; i < 32; i++)
            //{
            //    str = str + dataGridView1.Rows[i].Cells[1].Value;
            //}
            //Dut.Write(cB_Registers.Text, Convert.ToInt64(str, 2));

            //string str = ""; ;
            //for (int i = 0; i < Convert.ToInt32(tB_M_DataSize.Text); i++)
            //{
            //    str = str + dataGridView2.Rows[0].Cells[i].Value;
            //}
            //Dut.Write(cB_Registers.Text, Convert.ToInt64(str, 2));
            Dut.Write(cB_Registers.Text, Convert.ToInt64(tB_M_Value.Text, 16));//tB_M_Value.Text
        }

        private void cB_Boards_SelectedIndexChanged(object sender, EventArgs e)
        {
            cB_Registers.DataSource = Dut.GetAllEquipmentNames(cB_Boards.Text);
            tB_M_BoardNo.Text = Dut.GetBoard(cB_Boards.Text).ID.ToString();
            if (cB_Boards.Text.Contains("Rx"))
            {
                cB_RxMode.Visible = true;
            }
            else
            {
                cB_RxMode.Visible = false;
            }
            InitAbusList(Dut.GetBoard(cB_Boards.Text));
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 0 && e.KeyChar != 1 && !Char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
            //{
            //    e.Control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
            //}
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int iBoardNo = Convert.ToInt32(tB_M_BoardNo.Text);
            int iAddr = Convert.ToInt32(tB_M_Addr.Text,16);
            int iDateSize = Convert.ToInt32(tB_M_DataSize.Text);
            string sSPICommand = tB_M_SPICommand.Text;
            long rs = Dut.Read(iBoardNo, sSPICommand, iAddr, iDateSize, cB_M_VeRead.Checked ? 1 : 0);
            if (rs == Int64.MaxValue)
            {
                return;
            }
            tB_M_Value.Text = Convert.ToString(rs,16);

            string temp = Convert.ToString(rs, 2);
            temp = temp.PadLeft(32, '0');
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(); 
            for (int i = 0; i < Convert.ToInt32(tB_M_DataSize.Text); i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = temp[i];
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int iBoardNo = Convert.ToInt32(tB_M_BoardNo.Text);
            int iAddr = Convert.ToInt32(tB_M_Addr.Text,16);
            int iDateSize = Convert.ToInt32(tB_M_DataSize.Text);
            string sSPICommand = tB_M_SPICommand.Text;
            Dut.Write(iBoardNo, sSPICommand, iAddr, iDateSize, cB_M_VeRead.Checked?1:0,Convert.ToInt64(tB_M_Value.Text,16));//cB_M_VeRead.Checked?1:0
  

        }


        private void cB_Registers_SelectedIndexChanged(object sender, EventArgs e)
        {
            tB_M_Addr.Text = Dut.GetEquipment(cB_Boards.Text,cB_Registers.Text).Addr.ToString("X");
        }

        private void bt_Flash_Read_Click(object sender, EventArgs e)
        {
            string offsetStr = tB_Flash_Addr.Text;
            //rTB_Flash_Data.Text = Dut.ReadFlashString(Dut.GetBoard(cB_Boards.Text).ID, offsetStr);
            rTB_Flash_Data.Text =Dut.GetBoard(cB_Boards.Text).ReadFlash(offsetStr);

        }

        private void bt_Flash_Write_Click(object sender, EventArgs e)
        {
            string offsetStr = tB_Flash_Addr.Text;
            //Dut.WriteFalsh(Dut.GetBoard(cB_Boards.Text).ID, offsetStr, rTB_Flash_Data.Text.ToString());
            Dut.GetBoard(cB_Boards.Text).WriteFalsh(offsetStr, rTB_Flash_Data.Text.ToString());

        }

        private void bt_Flash_Erase_Click(object sender, EventArgs e)
        {
            string offsetStr = tB_Flash_Addr.Text;
            //Dut.clearFlash(Dut.GetBoard(cB_Boards.Text).ID, offsetStr);
            Dut.GetBoard(cB_Boards.Text).ClearFlash(offsetStr);
        }

        private void tB_Value_TextChanged(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            //int rs = Convert.ToInt32(tB_Value.Text == "" ? "0" : tB_Value.Text, 16);
            //string temp = Convert.ToString(rs, 2);
            //temp = temp.PadLeft(32, '0');
            //for (int i = 0; i < 32; i++)
            //{
            //    dataGridView1.Rows.Add(32 - i - 1, temp[i]);
            //}
            //tB_M_Value.Text=tB_Value.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BoardFlashControl w = BoardFlashControl.GetInstance(Dut, cB_Boards.Text);
            w.Show();
        }

        private void DebugBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance=null;
             
        }

        private void Read_Click(object sender, EventArgs e)
        {
            string abusName = cbAbusNames.Text;
            double abusAdc = 0;
            double value = Dut.GetBoard(cB_Boards.Text).readAbusValue(abusName, out abusAdc);
            richTextBox1.Text = Math.Round(value, 5).ToString() + "\n" + abusAdc.ToString();
        }

        private void RefreshAll_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            //double acomvalue = Dut.GetBoard(cB_Boards.Text).readAComValue();
            foreach (dynamic Abus in Dut.GetBoard(cB_Boards.Text).AbusList)
            {
                double abusAdc = 0;
                double value = Dut.GetBoard(cB_Boards.Text).readAbusValue(Abus.Name, out abusAdc);
                richTextBox1.AppendText(Abus.Name + ":" + Math.Round(value, 3).ToString() + '\n');
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void tB_M_Value_TextChanged(object sender, EventArgs e)
        {
            if (tB_M_Value.Focused)
            {
                dataGridView2.Rows.Clear();
                int rs = Convert.ToInt32(tB_M_Value.Text == "" ? "0" : tB_M_Value.Text, 16);
                string temp = Convert.ToString(rs, 2);
                temp = temp.PadLeft(32, '0');
                dataGridView2.Rows.Add();
                for (int i = 0; i < Convert.ToInt32(tB_M_DataSize.Text); i++)
                {
                    dataGridView2.Rows[0].Cells[i].Value = temp[i];
                }
            }

        }

        private void tB_M_DataSize_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.ColumnCount = Convert.ToInt32(tB_M_DataSize.Text);
            for (int i = 0; i < 32; i++)
            {
                dataGridView2.Columns[i].Name = (31 - i).ToString();
                dataGridView2.Columns[i].HeaderText = (31 - i).ToString();
                dataGridView2.Columns[i].Width = 28;
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Focused)
            {
                string str = ""; ;
                for (int i = 0; i < Convert.ToInt32(tB_M_DataSize.Text); i++)
                {
                    str = str + dataGridView2.Rows[0].Cells[i].Value;
                }
                tB_M_Value.Text = Convert.ToString(Convert.ToInt64(str, 2), 16);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str="";
            string offsetStr = textBox1.Text;
            int offset = Convert.ToInt32(offsetStr, 16);
            richTextBox2.Text = Dut.ReadFlash(Dut.GetBoard(cB_Boards.Text).ID, Convert.ToInt32(textBox2.Text)*8);
           
        }

        private void bt_Init100M_Click(object sender, EventArgs e)
        {
            Dut.Write(Dut.Debug.ADF4002PLL.EquipmentName, 0x00000004);
            Dut.Write(Dut.Debug.ADF4002PLL.EquipmentName, 0x00000A01);
            Dut.Write(Dut.Debug.ADF4002PLL.EquipmentName, 0x00048012);

        }

        private void cB_RxMode_CheckedChanged(object sender, EventArgs e)
        {
            if(cB_RxMode.Checked)
            {
                //if (cB_Boards.Text.Contains("RxBoard"))
                //{
                Dut.RxBoard.SetMode(1);
                //}
                
            }
            else
            {
                //if (cB_Boards.Text.Contains("RxBoard"))
                //{
                Dut.RxBoard.SetMode(0);
                //}
            }
        }

        private void bt_ReadHeader_Click(object sender, EventArgs e)
        {
             tBox_Header.Text = Dut.GetHeader_P2(Dut.GetBoard(cB_Boards.Text).ID);
        }

        private void bt_WriteHeader_Click(object sender, EventArgs e)
        {
            string sHeader =
            1 + ","
            + "{BoardRevision}" + ","
            + 2 + ","
            + "{Seri2alNumber}" + ","
            + 3 + ","
            + 4 + ","
            + 5 + ","
            + 6 + ","
            + 7;
            string[] HeaderToFW = new string[] { "001", "123456789", sHeader };
            Dut.SetHeader_P2(Dut.GetBoard(cB_Boards.Text).ID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sHeader =
            1 + ","
            + "{BoardRevision}" + ","
            + 2 + ","
            + "{Seri2alNumber}" + ","
            + 3 + ","
            + 4 + ","
            + 5 + ","
            + 6 + ","
            + 7;
            string[] HeaderToFW = new string[] { "001", "123456789", sHeader };
            Dut.SetHeader_P2(Dut.GetBoard(cB_Boards.Text).ID, HeaderToFW);
            Thread.Sleep(5000);
            string h = Dut.GetHeader_P2(Dut.GetBoard(cB_Boards.Text).ID);
            Thread.Sleep(5000);
            tBox_Header.Text = h;
        }
    }
}
