namespace SlideFroms
{
    partial class BoardAbusControl
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
            this.Read = new System.Windows.Forms.Button();
            this.RefreshAll = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cbAbusNames = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Read
            // 
            this.Read.Location = new System.Drawing.Point(194, 9);
            this.Read.Name = "Read";
            this.Read.Size = new System.Drawing.Size(75, 25);
            this.Read.TabIndex = 17;
            this.Read.Text = "Read";
            this.Read.UseVisualStyleBackColor = true;
            this.Read.Click += new System.EventHandler(this.Read_Click);
            // 
            // RefreshAll
            // 
            this.RefreshAll.Location = new System.Drawing.Point(275, 9);
            this.RefreshAll.Name = "RefreshAll";
            this.RefreshAll.Size = new System.Drawing.Size(94, 25);
            this.RefreshAll.TabIndex = 15;
            this.RefreshAll.Text = "RefreshAll";
            this.RefreshAll.UseVisualStyleBackColor = true;
            this.RefreshAll.Click += new System.EventHandler(this.RefreshAll_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(375, 9);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(56, 25);
            this.Clear.TabIndex = 13;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 40);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(419, 288);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // cbAbusNames
            // 
            this.cbAbusNames.FormattingEnabled = true;
            this.cbAbusNames.Location = new System.Drawing.Point(13, 10);
            this.cbAbusNames.Name = "cbAbusNames";
            this.cbAbusNames.Size = new System.Drawing.Size(175, 24);
            this.cbAbusNames.TabIndex = 19;
            // 
            // BoardAbusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 333);
            this.Controls.Add(this.cbAbusNames);
            this.Controls.Add(this.Read);
            this.Controls.Add(this.RefreshAll);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.richTextBox1);
            this.Name = "BoardAbusControl";
            this.Text = "BoardAbus";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BoardAbusControl_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Read;
        private System.Windows.Forms.Button RefreshAll;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox cbAbusNames;
    }
}