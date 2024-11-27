namespace SlideFroms
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.cbB_Version = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_Enter = new System.Windows.Forms.Button();
            this.rB_Board = new System.Windows.Forms.RadioButton();
            this.rB_System = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tB_addr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbB_StartSliders = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rTB_Options = new System.Windows.Forms.RichTextBox();
            this.bt_InitFDTI = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rB_Module = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cbB_Version
            // 
            this.cbB_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbB_Version.FormattingEnabled = true;
            this.cbB_Version.Location = new System.Drawing.Point(102, 51);
            this.cbB_Version.Margin = new System.Windows.Forms.Padding(2);
            this.cbB_Version.Name = "cbB_Version";
            this.cbB_Version.Size = new System.Drawing.Size(108, 25);
            this.cbB_Version.TabIndex = 1;
            this.cbB_Version.SelectedIndexChanged += new System.EventHandler(this.cbB_Version_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Slider Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Version:";
            // 
            // bt_Enter
            // 
            this.bt_Enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Enter.Location = new System.Drawing.Point(331, 114);
            this.bt_Enter.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Enter.Name = "bt_Enter";
            this.bt_Enter.Size = new System.Drawing.Size(101, 62);
            this.bt_Enter.TabIndex = 4;
            this.bt_Enter.Text = "Enter";
            this.bt_Enter.UseVisualStyleBackColor = true;
            this.bt_Enter.Click += new System.EventHandler(this.bt_Enter_Click);
            // 
            // rB_Board
            // 
            this.rB_Board.AutoSize = true;
            this.rB_Board.BackColor = System.Drawing.Color.Transparent;
            this.rB_Board.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rB_Board.Location = new System.Drawing.Point(105, 20);
            this.rB_Board.Margin = new System.Windows.Forms.Padding(2);
            this.rB_Board.Name = "rB_Board";
            this.rB_Board.Size = new System.Drawing.Size(63, 21);
            this.rB_Board.TabIndex = 5;
            this.rB_Board.TabStop = true;
            this.rB_Board.Text = "FPGA";
            this.rB_Board.UseVisualStyleBackColor = false;
            this.rB_Board.CheckedChanged += new System.EventHandler(this.rB_Board_CheckedChanged);
            // 
            // rB_System
            // 
            this.rB_System.AutoSize = true;
            this.rB_System.BackColor = System.Drawing.Color.Transparent;
            this.rB_System.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rB_System.Location = new System.Drawing.Point(280, 20);
            this.rB_System.Margin = new System.Windows.Forms.Padding(2);
            this.rB_System.Name = "rB_System";
            this.rB_System.Size = new System.Drawing.Size(56, 21);
            this.rB_System.TabIndex = 6;
            this.rB_System.TabStop = true;
            this.rB_System.Text = "SCPI";
            this.rB_System.UseVisualStyleBackColor = false;
            this.rB_System.CheckedChanged += new System.EventHandler(this.rB_System_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Addr:";
            // 
            // tB_addr
            // 
            this.tB_addr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_addr.Location = new System.Drawing.Point(102, 82);
            this.tB_addr.Margin = new System.Windows.Forms.Padding(2);
            this.tB_addr.Name = "tB_addr";
            this.tB_addr.Size = new System.Drawing.Size(330, 23);
            this.tB_addr.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(214, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Start Sliders:";
            // 
            // cbB_StartSliders
            // 
            this.cbB_StartSliders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbB_StartSliders.FormattingEnabled = true;
            this.cbB_StartSliders.Location = new System.Drawing.Point(307, 51);
            this.cbB_StartSliders.Margin = new System.Windows.Forms.Padding(2);
            this.cbB_StartSliders.Name = "cbB_StartSliders";
            this.cbB_StartSliders.Size = new System.Drawing.Size(125, 24);
            this.cbB_StartSliders.TabIndex = 11;
            this.cbB_StartSliders.SelectedIndexChanged += new System.EventHandler(this.cbB_StartSliders_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(151, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "- Off Line";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(303, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "- On Line";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(254, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(39, 114);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "Options:";
            // 
            // rTB_Options
            // 
            this.rTB_Options.Location = new System.Drawing.Point(102, 114);
            this.rTB_Options.Name = "rTB_Options";
            this.rTB_Options.Size = new System.Drawing.Size(224, 62);
            this.rTB_Options.TabIndex = 18;
            this.rTB_Options.Text = "";
            // 
            // bt_InitFDTI
            // 
            this.bt_InitFDTI.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_InitFDTI.Location = new System.Drawing.Point(42, 180);
            this.bt_InitFDTI.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.bt_InitFDTI.Name = "bt_InitFDTI";
            this.bt_InitFDTI.Size = new System.Drawing.Size(103, 23);
            this.bt_InitFDTI.TabIndex = 19;
            this.bt_InitFDTI.Text = "Init FDTI Connection";
            this.bt_InitFDTI.UseVisualStyleBackColor = true;
            this.bt_InitFDTI.Click += new System.EventHandler(this.bt_InitFDTI_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(370, 180);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rB_Module
            // 
            this.rB_Module.AutoSize = true;
            this.rB_Module.BackColor = System.Drawing.Color.Transparent;
            this.rB_Module.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rB_Module.Location = new System.Drawing.Point(201, 20);
            this.rB_Module.Margin = new System.Windows.Forms.Padding(2);
            this.rB_Module.Name = "rB_Module";
            this.rB_Module.Size = new System.Drawing.Size(41, 21);
            this.rB_Module.TabIndex = 21;
            this.rB_Module.TabStop = true;
            this.rB_Module.Text = "IVI";
            this.rB_Module.UseVisualStyleBackColor = false;
            this.rB_Module.CheckedChanged += new System.EventHandler(this.rB_Module_CheckedChanged);
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(470, 220);
            this.Controls.Add(this.rB_Module);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bt_InitFDTI);
            this.Controls.Add(this.rTB_Options);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbB_StartSliders);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tB_addr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rB_System);
            this.Controls.Add(this.rB_Board);
            this.Controls.Add(this.bt_Enter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbB_Version);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Welcome";
            this.Text = "Denali Sliders";
            this.Load += new System.EventHandler(this.Welcome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbB_Version;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_Enter;
        private System.Windows.Forms.RadioButton rB_Board;
        private System.Windows.Forms.RadioButton rB_System;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tB_addr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbB_StartSliders;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rTB_Options;
        private System.Windows.Forms.Button bt_InitFDTI;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rB_Module;
    }
}