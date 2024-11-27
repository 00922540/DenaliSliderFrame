using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VXT.DebugDriver;

namespace Keysight.NPIVXT.LO
{
    public partial class PowerControl : Form
    {
        LoDriver loDriver;
        public PowerControl(LoDriver loDriver)
        {
            InitializeComponent();
            this.loDriver = loDriver;
            initControl();            
        }
        private void PowerControlCall(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name;
            int bitSet = 0;
            if ("On" == button.Text)
            {
                bitSet = 0;
                button.Text = "Off";
                button.BackColor = Color.Red;
            }
            else
            {
                bitSet = 1;
                button.Text = "On";
                button.BackColor = Color.Green;
            }
            int bitSetPosition = Convert.ToInt32(buttonName.Substring(6,1),16);
            string data = loDriver.ReadReg(RegDef.LOPowerCtrl);
            data = DebugDriver.setBits(data, bitSet, 1, bitSetPosition, 32);
            loDriver.WriteReg(RegDef.LOPowerCtrl, data);
        }
        public void initControl()
        {
            string data = loDriver.ReadReg(RegDef.LOPowerCtrl);
            int dataInt = Convert.ToInt32(data, 16);
            for(int i=0;i<13;i++)
            {
                int mask = 1<<i;
                int powerStatus = dataInt & mask;
                Control[] controls = this.Controls.Find("button" + Convert.ToString(i + 1, 16), true);
                if(powerStatus>0)
                {                    
                    if(controls.Length>0)
                    {
                        Button button = (Button)controls[0];
                        button.Text = "On";
                        button.BackColor = Color.Green;
                    }
                }
                else
                {                    
                    if (controls.Length > 0)
                    {
                        Button button = (Button)controls[0];
                        button.Text = "Off";
                        button.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
