namespace SlideFroms
{
    partial class TxBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TxBoard));
            this.mLinkage = new System.Windows.Forms.CheckBox();
            this.bt_Sync = new System.Windows.Forms.Button();
            this.sliderSwitch2 = new MyControl.SliderSwitch();
            this.sliderPowerSwitch3 = new MyControl.SliderPowerSwitch();
            this.sliderPowerSwitch2 = new MyControl.SliderPowerSwitch();
            this.sliderPowerSwitch1 = new MyControl.SliderPowerSwitch();
            this.switch1 = new MyControl.Switch();
            this.sliderSwitch1 = new MyControl.SliderSwitch();
            this.attenuator1 = new MyControl.Attenuator();
            this.amp1 = new MyControl.AMP();
            this.switch21 = new MyControl.Switch();
            this.dac1 = new MyControl.DAC();
            this.SuspendLayout();
            // 
            // mLinkage
            // 
            this.mLinkage.AutoSize = true;
            this.mLinkage.Checked = true;
            this.mLinkage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mLinkage.Location = new System.Drawing.Point(271, 30);
            this.mLinkage.Margin = new System.Windows.Forms.Padding(2);
            this.mLinkage.Name = "mLinkage";
            this.mLinkage.Size = new System.Drawing.Size(64, 17);
            this.mLinkage.TabIndex = 121;
            this.mLinkage.Text = "Linkage";
            this.mLinkage.UseVisualStyleBackColor = true;
            // 
            // bt_Sync
            // 
            this.bt_Sync.Location = new System.Drawing.Point(359, 24);
            this.bt_Sync.Name = "bt_Sync";
            this.bt_Sync.Size = new System.Drawing.Size(75, 23);
            this.bt_Sync.TabIndex = 267;
            this.bt_Sync.Text = "Sync";
            this.bt_Sync.UseVisualStyleBackColor = true;
            this.bt_Sync.Click += new System.EventHandler(this.bt_Sync_Click);
            // 
            // sliderSwitch2
            // 
            this.sliderSwitch2.BackColor = System.Drawing.Color.Transparent;
            this.sliderSwitch2.BoardName = "Default";
            this.sliderSwitch2.CurrentWay = 0;
            this.sliderSwitch2.DisableColor = System.Drawing.Color.Red;
            this.sliderSwitch2.DisableLable = "OFF";
            this.sliderSwitch2.EnableColor = System.Drawing.Color.Lime;
            this.sliderSwitch2.EnableLable = "ON";
            this.sliderSwitch2.Location = new System.Drawing.Point(680, 51);
            this.sliderSwitch2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sliderSwitch2.Name = "sliderSwitch2";
            this.sliderSwitch2.RegisterName = "Default";
            this.sliderSwitch2.Size = new System.Drawing.Size(142, 36);
            this.sliderSwitch2.Style = MyControl.SliderSwitch.SwitchStyle.Rectangle;
            this.sliderSwitch2.SwitchName = null;
            this.sliderSwitch2.TabIndex = 278;
            // 
            // sliderPowerSwitch3
            // 
            this.sliderPowerSwitch3.BackColor = System.Drawing.Color.Transparent;
            this.sliderPowerSwitch3.BindXML = true;
            this.sliderPowerSwitch3.BoardName = "Default";
            this.sliderPowerSwitch3.CurrentWay = 0;
            this.sliderPowerSwitch3.DisableColor = System.Drawing.Color.Red;
            this.sliderPowerSwitch3.DisableLable = "OFF";
            this.sliderPowerSwitch3.EnableColor = System.Drawing.Color.Lime;
            this.sliderPowerSwitch3.EnableLable = "ON";
            this.sliderPowerSwitch3.Location = new System.Drawing.Point(20, 81);
            this.sliderPowerSwitch3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sliderPowerSwitch3.Name = "sliderPowerSwitch3";
            this.sliderPowerSwitch3.PowerSwitchText = "P5V_EN";
            this.sliderPowerSwitch3.RegisterName = "Tx_PWR_CTRL";
            this.sliderPowerSwitch3.RelatedDelayTime = 0;
            this.sliderPowerSwitch3.RelatedSwitch = "";
            this.sliderPowerSwitch3.Size = new System.Drawing.Size(160, 26);
            this.sliderPowerSwitch3.SwitchName = "U130";
            this.sliderPowerSwitch3.TabIndex = 277;
            // 
            // sliderPowerSwitch2
            // 
            this.sliderPowerSwitch2.BackColor = System.Drawing.Color.Transparent;
            this.sliderPowerSwitch2.BindXML = true;
            this.sliderPowerSwitch2.BoardName = "Default";
            this.sliderPowerSwitch2.CurrentWay = 0;
            this.sliderPowerSwitch2.DisableColor = System.Drawing.Color.Red;
            this.sliderPowerSwitch2.DisableLable = "OFF";
            this.sliderPowerSwitch2.EnableColor = System.Drawing.Color.Lime;
            this.sliderPowerSwitch2.EnableLable = "ON";
            this.sliderPowerSwitch2.Location = new System.Drawing.Point(20, 51);
            this.sliderPowerSwitch2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sliderPowerSwitch2.Name = "sliderPowerSwitch2";
            this.sliderPowerSwitch2.PowerSwitchText = "HMC994_P_EN1";
            this.sliderPowerSwitch2.RegisterName = "Tx_PWR_CTRL";
            this.sliderPowerSwitch2.RelatedDelayTime = 0;
            this.sliderPowerSwitch2.RelatedSwitch = "";
            this.sliderPowerSwitch2.Size = new System.Drawing.Size(160, 26);
            this.sliderPowerSwitch2.SwitchName = "U129";
            this.sliderPowerSwitch2.TabIndex = 275;
            // 
            // sliderPowerSwitch1
            // 
            this.sliderPowerSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.sliderPowerSwitch1.BindXML = true;
            this.sliderPowerSwitch1.BoardName = "Default";
            this.sliderPowerSwitch1.CurrentWay = 0;
            this.sliderPowerSwitch1.DisableColor = System.Drawing.Color.Red;
            this.sliderPowerSwitch1.DisableLable = "OFF";
            this.sliderPowerSwitch1.EnableColor = System.Drawing.Color.Lime;
            this.sliderPowerSwitch1.EnableLable = "ON";
            this.sliderPowerSwitch1.Location = new System.Drawing.Point(20, 21);
            this.sliderPowerSwitch1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sliderPowerSwitch1.Name = "sliderPowerSwitch1";
            this.sliderPowerSwitch1.PowerSwitchText = "总电源";
            this.sliderPowerSwitch1.RegisterName = "Tx_PWR_CTRL";
            this.sliderPowerSwitch1.RelatedDelayTime = 2000;
            this.sliderPowerSwitch1.RelatedSwitch = "";
            this.sliderPowerSwitch1.Size = new System.Drawing.Size(160, 26);
            this.sliderPowerSwitch1.SwitchName = "POWER";
            this.sliderPowerSwitch1.TabIndex = 274;
            // 
            // switch1
            // 
            this.switch1.BackColor = System.Drawing.Color.Transparent;
            this.switch1.BindXML = true;
            this.switch1.BoardName = "Default";
            this.switch1.CurrentWay = 0;
            this.switch1.Direction = MyControl.Switch.SwitchDirection.Right;
            this.switch1.FrontMarginRatio = 0.28F;
            this.switch1.Location = new System.Drawing.Point(881, 233);
            this.switch1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.switch1.Name = "switch1";
            this.switch1.RegisterName = "Tx_RF_CTRL1";
            this.switch1.RelatedSwitch = "";
            this.switch1.SideMargin = 12F;
            this.switch1.Size = new System.Drawing.Size(56, 75);
            this.switch1.SwitchColor = System.Drawing.Color.Cyan;
            this.switch1.SwitchName = "U136";
            this.switch1.TabIndex = 273;
            this.switch1.Type = MyControl.Switch.SwitchType.LeftSide;
            this.switch1.Way = 4;
            // 
            // sliderSwitch1
            // 
            this.sliderSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.sliderSwitch1.BindXML = true;
            this.sliderSwitch1.BoardName = "Default";
            this.sliderSwitch1.CurrentWay = 0;
            this.sliderSwitch1.DisableColor = System.Drawing.Color.Red;
            this.sliderSwitch1.DisableLable = "OFF";
            this.sliderSwitch1.EnableColor = System.Drawing.Color.Lime;
            this.sliderSwitch1.EnableCurrentStatus = true;
            this.sliderSwitch1.EnableLable = "ON";
            this.sliderSwitch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sliderSwitch1.Location = new System.Drawing.Point(572, 81);
            this.sliderSwitch1.Margin = new System.Windows.Forms.Padding(2);
            this.sliderSwitch1.Name = "sliderSwitch1";
            this.sliderSwitch1.RegisterName = "Tx_RF_CTRL1";
            this.sliderSwitch1.Size = new System.Drawing.Size(53, 48);
            this.sliderSwitch1.SwitchName = "U137";
            this.sliderSwitch1.TabIndex = 272;
            // 
            // attenuator1
            // 
            this.attenuator1.ATTColor = System.Drawing.Color.LightSkyBlue;
            this.attenuator1.AttStep = 1F;
            this.attenuator1.BackColor = System.Drawing.Color.Transparent;
            this.attenuator1.BindXML = true;
            this.attenuator1.BoardName = "Default";
            this.attenuator1.CurrentWay = 0;
            this.attenuator1.DecimalPlaces = 0;
            this.attenuator1.EnableCurrentStatus = true;
            this.attenuator1.HorMargin = 10;
            this.attenuator1.Location = new System.Drawing.Point(1048, 476);
            this.attenuator1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.attenuator1.Name = "attenuator1";
            this.attenuator1.RegisterName = "Tx_ATT_CTRL1";
            this.attenuator1.Size = new System.Drawing.Size(70, 68);
            this.attenuator1.SwitchName = "AT9";
            this.attenuator1.TabIndex = 271;
            this.attenuator1.TextSize = 32;
            // 
            // amp1
            // 
            this.amp1.AMPColor0 = System.Drawing.Color.DarkGray;
            this.amp1.AMPColor1 = System.Drawing.Color.Lime;
            this.amp1.BackColor = System.Drawing.Color.Transparent;
            this.amp1.BindXML = true;
            this.amp1.BoardName = "Default";
            this.amp1.BorderWidth = 1;
            this.amp1.CurrentWay = 0;
            this.amp1.Direction = MyControl.AMP.AMPDirection.Right;
            this.amp1.EnableCurrentStatus = true;
            this.amp1.Location = new System.Drawing.Point(405, 190);
            this.amp1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.amp1.Name = "amp1";
            this.amp1.RegisterName = "Tx_RF_CTRL1";
            this.amp1.Size = new System.Drawing.Size(38, 32);
            this.amp1.SwitchName = "U138";
            this.amp1.TabIndex = 270;
            // 
            // switch21
            // 
            this.switch21.BackColor = System.Drawing.Color.Transparent;
            this.switch21.BindXML = true;
            this.switch21.BoardName = "TxBoard";
            this.switch21.CurrentWay = 0;
            this.switch21.Direction = MyControl.Switch.SwitchDirection.Left;
            this.switch21.EnableCurrentStatus = true;
            this.switch21.FrontMarginRatio = 0.34F;
            this.switch21.Location = new System.Drawing.Point(767, 258);
            this.switch21.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.switch21.Name = "switch21";
            this.switch21.RegisterName = "Tx_RF_CTRL1";
            this.switch21.RelatedSwitch = "U136";
            this.switch21.SideMargin = 10F;
            this.switch21.Size = new System.Drawing.Size(58, 53);
            this.switch21.SwitchColor = System.Drawing.Color.Cyan;
            this.switch21.SwitchName = "U139";
            this.switch21.TabIndex = 269;
            this.switch21.Type = MyControl.Switch.SwitchType.DualSide;
            this.switch21.Way = 4;
            // 
            // dac1
            // 
            this.dac1.AddLength = 4;
            this.dac1.Address = 5;
            this.dac1.BackColor = System.Drawing.Color.Transparent;
            this.dac1.BoardName = "Default";
            this.dac1.CMD = 12;
            this.dac1.CMDLength = 4;
            this.dac1.CurrentWay = 0;
            this.dac1.EnableCurrentStatus = true;
            this.dac1.InputWidth = 80;
            this.dac1.Location = new System.Drawing.Point(498, 283);
            this.dac1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dac1.MyBgColor = System.Drawing.Color.Yellow;
            this.dac1.MyFeFrColor = System.Drawing.Color.Black;
            this.dac1.Name = "dac1";
            this.dac1.RegisterName = "Mixer_DAC_U75";
            this.dac1.SaveValue = true;
            this.dac1.Size = new System.Drawing.Size(114, 40);
            this.dac1.SwitchName = "U99";
            this.dac1.TabIndex = 279;
            this.dac1.Unit = "dB";
            this.dac1.Value = 100D;
            this.dac1.ValueLength = 16;
            this.dac1.ValueStep = 5F;
            // 
            // TxBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BoardName = "TxBoardP10";
            this.ClientSize = new System.Drawing.Size(1521, 665);
            this.Controls.Add(this.dac1);
            this.Controls.Add(this.sliderSwitch2);
            this.Controls.Add(this.sliderPowerSwitch3);
            this.Controls.Add(this.sliderPowerSwitch2);
            this.Controls.Add(this.sliderPowerSwitch1);
            this.Controls.Add(this.switch1);
            this.Controls.Add(this.sliderSwitch1);
            this.Controls.Add(this.attenuator1);
            this.Controls.Add(this.amp1);
            this.Controls.Add(this.switch21);
            this.Controls.Add(this.bt_Sync);
            this.Controls.Add(this.mLinkage);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TxBoard";
            this.Text = "TxBoard";
            this.Load += new System.EventHandler(this.TxBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox mLinkage;
        private System.Windows.Forms.Button bt_Sync;
        private MyControl.Switch switch21;
        private MyControl.AMP amp1;
        private MyControl.Attenuator attenuator1;
        private MyControl.SliderSwitch sliderSwitch1;
        private MyControl.Switch switch1;
        private MyControl.SliderPowerSwitch sliderPowerSwitch1;
        private MyControl.SliderPowerSwitch sliderPowerSwitch2;
        private MyControl.SliderPowerSwitch sliderPowerSwitch3;
        private MyControl.SliderSwitch sliderSwitch2;
        private MyControl.DAC dac1;
    }
}