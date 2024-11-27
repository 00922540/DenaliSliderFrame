namespace SlideFroms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MsgBox = new System.Windows.Forms.RichTextBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.splitContainer_Top = new System.Windows.Forms.SplitContainer();
            this.label_Mode = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.rTB_Operation = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Top)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer_Top.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer_Top);
            this.splitContainer1.Panel1.Controls.Add(this.MsgBox);
            this.splitContainer1.Panel1MinSize = 8;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TabControl);
            this.splitContainer1.Panel2MinSize = 15;
            this.splitContainer1.Size = new System.Drawing.Size(859, 376);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // MsgBox
            // 
            this.MsgBox.BackColor = System.Drawing.SystemColors.Control;
            this.MsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.MsgBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgBox.Location = new System.Drawing.Point(0, 0);
            this.MsgBox.Margin = new System.Windows.Forms.Padding(0);
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(599, 25);
            this.MsgBox.TabIndex = 0;
            this.MsgBox.Text = "";
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Margin = new System.Windows.Forms.Padding(2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(859, 350);
            this.TabControl.TabIndex = 1;
            this.TabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl_DrawItem);
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // splitContainer_Top
            // 
            this.splitContainer_Top.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitContainer_Top.Location = new System.Drawing.Point(624, 0);
            this.splitContainer_Top.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer_Top.Name = "splitContainer_Top";
            // 
            // splitContainer_Top.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label_Mode);
            this.splitContainer_Top.Size = new System.Drawing.Size(235, 25);
            this.splitContainer_Top.SplitterDistance = 120;
            this.splitContainer_Top.TabIndex = 1;
            // 
            // label_Mode
            // 
            this.label_Mode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Mode.Location = new System.Drawing.Point(0, 0);
            this.label_Mode.Name = "label_Mode";
            this.label_Mode.Size = new System.Drawing.Size(120, 25);
            this.label_Mode.TabIndex = 0;
            this.label_Mode.Text = "label1";
            this.label_Mode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(851, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Operation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rTB_Operation);
            this.splitContainer2.Size = new System.Drawing.Size(851, 324);
            this.splitContainer2.SplitterDistance = 283;
            this.splitContainer2.TabIndex = 0;
            // 
            // rTB_Operation
            // 
            this.rTB_Operation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTB_Operation.Location = new System.Drawing.Point(0, 0);
            this.rTB_Operation.Name = "rTB_Operation";
            this.rTB_Operation.Size = new System.Drawing.Size(564, 324);
            this.rTB_Operation.TabIndex = 0;
            this.rTB_Operation.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 376);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Top)).EndInit();
            this.splitContainer_Top.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.RichTextBox MsgBox;
        private System.Windows.Forms.SplitContainer splitContainer_Top;
        private System.Windows.Forms.Label label_Mode;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox rTB_Operation;

    }
}