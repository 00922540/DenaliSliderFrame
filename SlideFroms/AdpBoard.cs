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
using System.Threading;
namespace SlideFroms
{
    public partial class AdpBoard : BaseForm
    {
        public AdpBoard()
        {
            InitializeComponent();
        }

        public override void BoardPowerOn()
        {
            base.BoardPowerOn();

        }
        public override void BoardPowerOff()
        {
            base.BoardPowerOff();

        }
        private void TxBoard_Load(object sender, EventArgs e)
        {
            SaveSliderLocation();

        }

        private void bt_Abus_Click(object sender, EventArgs e)
        {
            BoardAbusControl w = BoardAbusControl.GetInstance(Dut,Dut.Tx);
            w.Show();
        }
        private void VCM_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown VCM = (NumericUpDown)sender;
            Dut.AdapterBoard_P10.SetVCM(VCM.Name, (double)VCM.Value);

        }
        private void Gain_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown mGain = (NumericUpDown)sender;
            Dut.AdapterBoard_P10.SetGain(mGain.Name, (double)mGain.Value);
        }

        #region Flash Function

        #endregion

        private void Power_Click(object sender, EventArgs e)
        {
            if ("All Power Off" == Power.Text)
            {
                Dut.AdapterBoard_P10.PowerADC(true);
                Power.Text = "All Power On";
                Power.BackColor = Color.Green;

            }
            else if ("All Power On" == Power.Text)
            {
                Dut.AdapterBoard_P10.PowerADC(false);
                Power.Text = "All Power Off";
                Power.BackColor = Color.Red;
            }
        }
    }
}
