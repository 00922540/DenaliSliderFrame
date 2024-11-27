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
    public partial class TxBoard : BaseForm
    {
        //BoardDriver.VXT3BoardDriver Dut;
        public int LoFreqPath;
        public TxBoard()
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

        public override void ActionAfterClick(dynamic obj)
        {


        }

        private void bt_Abus_Click(object sender, EventArgs e)
        {
            BoardAbusControl w = BoardAbusControl.GetInstance(Dut, Dut.TxBoard);
            w.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BoardHeader w = BoardHeader.GetInstance(Dut, Dut.TxBoard);
            w.Show();
        }



        private void bt_Sync_Click(object sender, EventArgs e)
        {

            //RefreshControlStatusFromHW(Dut.TxBoardP10.Tx_ATT_CTRL1.EquipmentName);
            //RefreshControlStatusFromHW(Dut.TxBoardP10.Tx_RF_CTRL1.EquipmentName);
            //RefreshControlStatusFromHW(Dut.TxBoardP10.Tx_RF_CTRL2.EquipmentName);
            RefreshSliderByHW(false);
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            Dut.GetRegisterByName("BB_PWR_CTRL").WriteBits(1, 1, 1);
        }

        private void dac1_Load(object sender, EventArgs e)
        {

        }
    }
}
