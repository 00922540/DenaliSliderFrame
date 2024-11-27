using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class BinaryAnalyzer : Form
    {
        private int DataSize;
        //TextBox[] myTextBox;
        List<TextBox> myTextBox;
        List<Label> myLabel = new List<Label>();
        public BinaryAnalyzer()
        {
            InitializeComponent();
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BinaryAnalyzer());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataSize = Convert.ToInt32(tB_Size.Text);
            string sBinaryData = Convert.ToString(Convert.ToInt64(tB_Send_Data.Text, 16), 2).PadLeft(DataSize, '0');

            for (int i = 0; i < 9; i++)
            {
                dataGridView_Header.Rows[0].Cells[i].Value = sBinaryData[i];
            }
            for (int i = 9; i < DataSize; i++)
            {
                dataGridView_Data.Rows[0].Cells[i].Value = sBinaryData[i];
            }
            tB_10_ADDR.Text = Convert.ToInt32(sBinaryData.Substring(0, 9),2).ToString();
            tB_10_Data.Text = Convert.ToInt32(sBinaryData.Substring(9),2).ToString();
        }


        private void BinaryAnalyzer_Load(object sender, EventArgs e)
        {
            dataGridView_Data.ColumnCount = 9;
            for (int i = 0; i < 9; i++)
            {
                dataGridView_Data.Columns[i].Name = (8 - i).ToString();
                dataGridView_Data.Columns[i].HeaderText = (8 - i).ToString();
                dataGridView_Data.Columns[i].Width = 28;
            }
            dataGridView_Header.ColumnCount = 32;
            for (int i = 0; i < 32; i++)
            {
                dataGridView_Header.Columns[i].Name = (31 - i).ToString();
                dataGridView_Header.Columns[i].HeaderText = (31 - i).ToString();
                dataGridView_Header.Columns[i].Width = 28;
            }

            myTextBox = new List<TextBox>(){
                textBox1,textBox2,textBox3,textBox4,textBox5,textBox6,textBox7,textBox8,
                textBox9,textBox10,textBox11,textBox12,textBox13,textBox14,textBox15,textBox16,
                textBox17,textBox18,textBox19,textBox20,textBox21,textBox22,textBox23,textBox24,
                textBox25,textBox26,textBox27,textBox28,textBox29,textBox30,textBox31,textBox32,
                textBox33,textBox34,textBox35,textBox36,textBox37,textBox38,textBox39,textBox40,
                textBox41,textBox42,textBox43,textBox44,textBox45,textBox46,textBox47,textBox48
            };
            for (int i = 0; i < 48;i++ )
            {
                Label lb = new  System.Windows.Forms.Label();


                lb.AutoSize = true;
                lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lb.Location = new System.Drawing.Point(myTextBox[i].Left, myTextBox[i].Top - myTextBox[i].Height+5);
                lb.Name = "lb" + i;
                lb.Size = new System.Drawing.Size(myTextBox[i].Width, myTextBox[i].Height);
                lb.TabIndex = 113;
                lb.AutoSize = false;
                lb.Margin = new System.Windows.Forms.Padding(0);
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Text = i.ToString();
                lb.Visible = true;
                this.groupBox2.Controls.Add(lb);
                myLabel.Add(lb);
            }


            for (int i = 0; i < myTextBox.Count(); i++)
            {
                myTextBox[i].Text = "0";
                myTextBox[i].Click += new System.EventHandler(this.Tb_Click);
                myTextBox[i].TextChanged += new System.EventHandler(this.Tb_TextChanged);
            }
        }

        private void Tb_Click(object sender, EventArgs e)
        {
            tB_Hex.Text="0x" ;
            if((sender as TextBox).Text =="0")
            {
                (sender as TextBox).Text ="1";
                (sender as TextBox).ForeColor = Color.Red;
            }
            else
            {
                (sender as TextBox).Text = "0";
                (sender as TextBox).ForeColor = Color.Black;
            }
            //for (int i = myTextBox.Count()-1; i >=3 ;i= i-4 )
            //{
            //    tB_Hex.Text = tB_Hex.Text +  Convert.ToInt32((myTextBox[i].Text + myTextBox[i - 1].Text + myTextBox[i - 2].Text + myTextBox[i - 3].Text), 2).ToString("x");
            //}
            //tB_Dec.Text = Convert.ToInt32(tB_Hex.Text.Substring(2, tB_Hex.Text.Length - 2), 16).ToString();
        }

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            tB_Hex.Text = "";
            string str = "";
            if ((sender as TextBox).Text == "1")
            {
                (sender as TextBox).ForeColor = Color.Red;
            }
            else
            {
                (sender as TextBox).ForeColor = Color.Black;
            }
            //for (int i = myTextBox.Count() - 1; i >= 3; i = i - 4)
            //{
            //    tB_Hex.Text = tB_Hex.Text + Convert.ToInt32((myTextBox[i].Text + myTextBox[i - 1].Text + myTextBox[i - 2].Text + myTextBox[i - 3].Text), 2).ToString("x");

            //}
            for (int i = 0; i < myTextBox.Count()/4; i ++)
            {
                tB_Hex.Text = Convert.ToInt32((myTextBox[i * 4 + 3].Text + myTextBox[i * 4 + 2].Text + myTextBox[i * 4 + 1].Text + myTextBox[i * 4].Text), 2).ToString("x") + tB_Hex.Text;

            }
            if (myTextBox.Count() % 4 !=0)
            {
                for (int i = 0; i < myTextBox.Count() % 4; i++)
                {
                    str = myTextBox[myTextBox.Count() / 4 * 4 + i].Text + str;
                }
                tB_Hex.Text = Convert.ToInt32(str, 2).ToString("x") + tB_Hex.Text;
            }

            tB_Dec.Text = Convert.ToInt64(tB_Hex.Text, 16).ToString();
            tB_Hex.Text = "0x" + tB_Hex.Text;
        }

        private void BinaryAnalyzer_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.V)
            {
                   IDataObject data = Clipboard.GetDataObject();
                   if (data.GetDataPresent(DataFormats.Text))
                   {
                       this.tB_Data.Text = data.GetData(DataFormats.Text).ToString();
                   }
            }
        }

        private void tB_Data_TextChanged(object sender, EventArgs e)
        {
            DataChange();
        }
        public void DataChange()
        {
            string[] sBinString = {"1","0"};
            string sPos;
            for(int i=0; i<tB_Data.Text.Length;i++)
            {
                sPos=tB_Data.Text.Substring(tB_Data.Text.Length-1-i,1);
                if(sBinString.Contains(sPos))
                {
                    myTextBox[i].Text = sPos;

                }
                else
                {
                    foreach (TextBox tb in myTextBox) { tb.Text = "0";  }
                    break;
                }

            }
        }
        public void ReCal()
        {
            string str="";
            tB_Hex.Text = "";

            for (int i = 0; i < myTextBox.Count() / 4; i++)
            {
                tB_Hex.Text = Convert.ToInt32((myTextBox[i * 4 + 3].Text + myTextBox[i * 4 + 2].Text + myTextBox[i * 4 + 1].Text + myTextBox[i * 4].Text), 2).ToString("x") + tB_Hex.Text;

            }
            if (myTextBox.Count() % 4 != 0)
            {
                for (int i = 0; i < myTextBox.Count() % 4; i++)
                {
                    str = myTextBox[myTextBox.Count() / 4 * 4 + i].Text + str;
                }
                tB_Hex.Text = Convert.ToInt32(str, 2).ToString("x") + tB_Hex.Text;
            }

            tB_Dec.Text = Convert.ToInt64(tB_Hex.Text, 16).ToString();
            tB_Hex.Text = "0x" + tB_Hex.Text;
        }
        private void cbB_Size_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbB_Size.Text) < myTextBox.Count())
            {
                for (int i = myTextBox.Count() - 1; i > Convert.ToInt16(cbB_Size.Text)-1; i--)
                {
                    myTextBox[i].Visible = false;
                    myTextBox.RemoveAt(i);
                    myLabel[i].Visible =false;
                }
            }
            else
            {
                for (int i = myTextBox.Count(); i < Convert.ToInt16(cbB_Size.Text); i++)
                {

                    myLabel[i].Visible = true;
                    myTextBox.Add(this.groupBox2.Controls["textBox" + (i+1)] as TextBox);
                    myTextBox[i].Visible = true;
                    
                }

            }
            ReCal();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(this.groupBox2.Controls["textBox33"].Name);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            int value;
            Dictionary<int, Dictionary<long, double>> TLL = new Dictionary<int, Dictionary<long, double>>();
            Dictionary<long, double> FreqTLL;
            for(int index=0;index<200;index++)
            {
                FreqTLL = new Dictionary<long, double>();
                for(int freq=0;freq<50000;freq++)
                {                  
                    FreqTLL.Add(freq, index%100);
                }
                TLL.Add(index, FreqTLL);
            }
            DateTime dt1 = DateTime.Now;

            for(long judge =0;judge<1000000;judge++)
            {
                if(TLL.ContainsKey((int)judge%200))
                {
                    if (TLL[(int)judge % 200].ContainsKey((int)judge % 50000))
                    {
                        if(TLL[(int)judge % 200][(int)judge % 50000]>1)
                        {
                            value = 0;
                        }
                    }
                }
            }
            DateTime dt2 = DateTime.Now;
            TimeSpan dtspan = dt2 - dt1;
            tB_Time.Text = dtspan.Milliseconds.ToString() + "毫秒";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int value;
            DateTime dt1 = DateTime.Now;
            for (long judge = 0; judge < 1000000; judge++)
            {
                if (1>judge)
                {
                    value = 0;
                }
            }
            DateTime dt2 = DateTime.Now;
            TimeSpan dtspan = dt2 - dt1;
            tB_Time2.Text = dtspan.Milliseconds.ToString() + "毫秒";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int offset = Convert.ToInt32("800", 16);
            string address = Convert.ToString(0x300000 + offset, 16);
            MessageBox.Show(address);
        }
    }
}
