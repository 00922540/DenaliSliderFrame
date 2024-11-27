namespace SlideBase
{
    partial class BaseForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveStatusToolStripMenuItem,
            this.saveAsDefaultToolStripMenuItem,
            this.loadStatusToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 76);
            // 
            // saveStatusToolStripMenuItem
            // 
            this.saveStatusToolStripMenuItem.Name = "saveStatusToolStripMenuItem";
            this.saveStatusToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.saveStatusToolStripMenuItem.Text = "Save Status";
            this.saveStatusToolStripMenuItem.Click += new System.EventHandler(this.saveStatusToolStripMenuItem_Click);
            // 
            // saveAsDefaultToolStripMenuItem
            // 
            this.saveAsDefaultToolStripMenuItem.Name = "saveAsDefaultToolStripMenuItem";
            this.saveAsDefaultToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.saveAsDefaultToolStripMenuItem.Text = "Save as default";
            this.saveAsDefaultToolStripMenuItem.Click += new System.EventHandler(this.saveAsDefaultToolStripMenuItem_Click);
            // 
            // loadStatusToolStripMenuItem
            // 
            this.loadStatusToolStripMenuItem.Name = "loadStatusToolStripMenuItem";
            this.loadStatusToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.loadStatusToolStripMenuItem.Text = "Load Status";
            this.loadStatusToolStripMenuItem.Click += new System.EventHandler(this.loadStatusToolStripMenuItem_Click);
            // 
            // BaseForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1720, 985);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsDefaultToolStripMenuItem;
        //private MyControl.Switch OC_IO_0;
        //private MyControl.DualSwitch ANT_TN1;
        //private MyControl.Switch M_BUS;
        //private MyControl.Switch TLL_IN_0;
        //private MyControl.Switch TLL_OUT_0;
        //private MyControl.Switch M_BUS2;
        //private MyControl.DualSwitch ANT_OUT2_2;
        //private MyControl.DualSwitch ANT_OUT1_2;
        //private MyControl.DualSwitch ANT_IN2_2;
        //private MyControl.DualSwitch ANT_IN1_2;
        //private MyControl.DualSwitch ANT_OUT2;
        //private MyControl.DualSwitch ANT_OUT1;
        //private MyControl.DualSwitch ANT_TN2;
        //private MyControl.Switch switch1;







    }
}

