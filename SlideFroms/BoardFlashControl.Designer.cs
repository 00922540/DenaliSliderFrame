namespace SlideFroms
{
    partial class BoardFlashControl
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
            this.EraseBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.addrOffsetTB = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.WriteFlash = new System.Windows.Forms.Button();
            this.ReadFlash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EraseBtn
            // 
            this.EraseBtn.Location = new System.Drawing.Point(297, 27);
            this.EraseBtn.Name = "EraseBtn";
            this.EraseBtn.Size = new System.Drawing.Size(103, 25);
            this.EraseBtn.TabIndex = 11;
            this.EraseBtn.Text = "Erase";
            this.EraseBtn.UseVisualStyleBackColor = true;
            this.EraseBtn.Click += new System.EventHandler(this.EraseBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Offset: 0x";
            // 
            // addrOffsetTB
            // 
            this.addrOffsetTB.Location = new System.Drawing.Point(126, 30);
            this.addrOffsetTB.Name = "addrOffsetTB";
            this.addrOffsetTB.Size = new System.Drawing.Size(100, 22);
            this.addrOffsetTB.TabIndex = 9;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(47, 69);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(608, 355);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // WriteFlash
            // 
            this.WriteFlash.Location = new System.Drawing.Point(546, 27);
            this.WriteFlash.Name = "WriteFlash";
            this.WriteFlash.Size = new System.Drawing.Size(109, 25);
            this.WriteFlash.TabIndex = 7;
            this.WriteFlash.Text = "WriteFlash";
            this.WriteFlash.UseVisualStyleBackColor = true;
            this.WriteFlash.Click += new System.EventHandler(this.WriteFlash_Click);
            // 
            // ReadFlash
            // 
            this.ReadFlash.Location = new System.Drawing.Point(416, 27);
            this.ReadFlash.Name = "ReadFlash";
            this.ReadFlash.Size = new System.Drawing.Size(103, 25);
            this.ReadFlash.TabIndex = 6;
            this.ReadFlash.Text = "ReadFlash";
            this.ReadFlash.UseVisualStyleBackColor = true;
            this.ReadFlash.Click += new System.EventHandler(this.ReadFlash_Click);
            // 
            // LoBoardFlashControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 443);
            this.Controls.Add(this.EraseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addrOffsetTB);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.WriteFlash);
            this.Controls.Add(this.ReadFlash);
            this.Name = "LoBoardFlashControl";
            this.Text = "BoardFlashControl";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BoardFlashControl_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EraseBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addrOffsetTB;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button WriteFlash;
        private System.Windows.Forms.Button ReadFlash;
    }
}