namespace SlideFroms
{
    partial class DebugBoard
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAbusNames = new System.Windows.Forms.ComboBox();
            this.Read = new System.Windows.Forms.Button();
            this.RefreshAll = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bt_Flash_Erase = new System.Windows.Forms.Button();
            this.rTB_Flash_Data = new System.Windows.Forms.RichTextBox();
            this.bt_Flash_Write = new System.Windows.Forms.Button();
            this.bt_Flash_Read = new System.Windows.Forms.Button();
            this.tB_Flash_Addr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cB_Registers = new System.Windows.Forms.ComboBox();
            this.cB_Boards = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bt_Read = new System.Windows.Forms.Button();
            this.bt_Write = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cB_RxMode = new System.Windows.Forms.CheckBox();
            this.bt_Init100M = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.cB_M_VeRead = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tB_M_SPICommand = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tB_M_Addr = new System.Windows.Forms.TextBox();
            this.tB_M_DataSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tB_M_Value = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tB_M_BoardNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tBox_Header = new System.Windows.Forms.TextBox();
            this.bt_ReadHeader = new System.Windows.Forms.Button();
            this.bt_WriteHeader = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbAbusNames);
            this.groupBox2.Controls.Add(this.Read);
            this.groupBox2.Controls.Add(this.RefreshAll);
            this.groupBox2.Controls.Add(this.Clear);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Location = new System.Drawing.Point(441, 214);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(361, 432);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Abus";
            // 
            // cbAbusNames
            // 
            this.cbAbusNames.FormattingEnabled = true;
            this.cbAbusNames.Location = new System.Drawing.Point(4, 22);
            this.cbAbusNames.Margin = new System.Windows.Forms.Padding(2);
            this.cbAbusNames.Name = "cbAbusNames";
            this.cbAbusNames.Size = new System.Drawing.Size(132, 21);
            this.cbAbusNames.TabIndex = 24;
            // 
            // Read
            // 
            this.Read.Location = new System.Drawing.Point(140, 22);
            this.Read.Margin = new System.Windows.Forms.Padding(2);
            this.Read.Name = "Read";
            this.Read.Size = new System.Drawing.Size(45, 20);
            this.Read.TabIndex = 23;
            this.Read.Text = "Read";
            this.Read.UseVisualStyleBackColor = true;
            this.Read.Click += new System.EventHandler(this.Read_Click);
            // 
            // RefreshAll
            // 
            this.RefreshAll.Location = new System.Drawing.Point(190, 22);
            this.RefreshAll.Margin = new System.Windows.Forms.Padding(2);
            this.RefreshAll.Name = "RefreshAll";
            this.RefreshAll.Size = new System.Drawing.Size(56, 20);
            this.RefreshAll.TabIndex = 22;
            this.RefreshAll.Text = "RefreshAll";
            this.RefreshAll.UseVisualStyleBackColor = true;
            this.RefreshAll.Click += new System.EventHandler(this.RefreshAll_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(250, 20);
            this.Clear.Margin = new System.Windows.Forms.Padding(2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(56, 20);
            this.Clear.TabIndex = 21;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(4, 48);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(303, 380);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.bt_WriteHeader);
            this.groupBox4.Controls.Add(this.bt_ReadHeader);
            this.groupBox4.Controls.Add(this.tBox_Header);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.richTextBox2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.bt_Flash_Erase);
            this.groupBox4.Controls.Add(this.rTB_Flash_Data);
            this.groupBox4.Controls.Add(this.bt_Flash_Write);
            this.groupBox4.Controls.Add(this.bt_Flash_Read);
            this.groupBox4.Controls.Add(this.tB_Flash_Addr);
            this.groupBox4.Location = new System.Drawing.Point(11, 214);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(418, 385);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Flash";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(122, 155);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(57, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "4";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(14, 178);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(344, 109);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(184, 155);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 6;
            this.button1.Text = "Read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 155);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(91, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "800";
            // 
            // bt_Flash_Erase
            // 
            this.bt_Flash_Erase.Location = new System.Drawing.Point(302, 24);
            this.bt_Flash_Erase.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Flash_Erase.Name = "bt_Flash_Erase";
            this.bt_Flash_Erase.Size = new System.Drawing.Size(56, 19);
            this.bt_Flash_Erase.TabIndex = 4;
            this.bt_Flash_Erase.Text = "Earse";
            this.bt_Flash_Erase.UseVisualStyleBackColor = true;
            this.bt_Flash_Erase.Click += new System.EventHandler(this.bt_Flash_Erase_Click);
            // 
            // rTB_Flash_Data
            // 
            this.rTB_Flash_Data.Location = new System.Drawing.Point(16, 46);
            this.rTB_Flash_Data.Margin = new System.Windows.Forms.Padding(2);
            this.rTB_Flash_Data.Name = "rTB_Flash_Data";
            this.rTB_Flash_Data.Size = new System.Drawing.Size(343, 97);
            this.rTB_Flash_Data.TabIndex = 3;
            this.rTB_Flash_Data.Text = "";
            // 
            // bt_Flash_Write
            // 
            this.bt_Flash_Write.Location = new System.Drawing.Point(184, 23);
            this.bt_Flash_Write.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Flash_Write.Name = "bt_Flash_Write";
            this.bt_Flash_Write.Size = new System.Drawing.Size(56, 19);
            this.bt_Flash_Write.TabIndex = 2;
            this.bt_Flash_Write.Text = "Write";
            this.bt_Flash_Write.UseVisualStyleBackColor = true;
            this.bt_Flash_Write.Click += new System.EventHandler(this.bt_Flash_Write_Click);
            // 
            // bt_Flash_Read
            // 
            this.bt_Flash_Read.Location = new System.Drawing.Point(122, 23);
            this.bt_Flash_Read.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Flash_Read.Name = "bt_Flash_Read";
            this.bt_Flash_Read.Size = new System.Drawing.Size(56, 19);
            this.bt_Flash_Read.TabIndex = 1;
            this.bt_Flash_Read.Text = "Read";
            this.bt_Flash_Read.UseVisualStyleBackColor = true;
            this.bt_Flash_Read.Click += new System.EventHandler(this.bt_Flash_Read_Click);
            // 
            // tB_Flash_Addr
            // 
            this.tB_Flash_Addr.Location = new System.Drawing.Point(16, 24);
            this.tB_Flash_Addr.Margin = new System.Windows.Forms.Padding(2);
            this.tB_Flash_Addr.Name = "tB_Flash_Addr";
            this.tB_Flash_Addr.Size = new System.Drawing.Size(91, 20);
            this.tB_Flash_Addr.TabIndex = 0;
            this.tB_Flash_Addr.Text = "800";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Register:";
            // 
            // cB_Registers
            // 
            this.cB_Registers.FormattingEnabled = true;
            this.cB_Registers.Location = new System.Drawing.Point(63, 84);
            this.cB_Registers.Margin = new System.Windows.Forms.Padding(2);
            this.cB_Registers.Name = "cB_Registers";
            this.cB_Registers.Size = new System.Drawing.Size(129, 21);
            this.cB_Registers.TabIndex = 2;
            this.cB_Registers.SelectedIndexChanged += new System.EventHandler(this.cB_Registers_SelectedIndexChanged);
            // 
            // cB_Boards
            // 
            this.cB_Boards.FormattingEnabled = true;
            this.cB_Boards.Location = new System.Drawing.Point(63, 41);
            this.cB_Boards.Margin = new System.Windows.Forms.Padding(2);
            this.cB_Boards.Name = "cB_Boards";
            this.cB_Boards.Size = new System.Drawing.Size(129, 21);
            this.cB_Boards.TabIndex = 9;
            this.cB_Boards.SelectedIndexChanged += new System.EventHandler(this.cB_Boards_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Boards:";
            // 
            // bt_Read
            // 
            this.bt_Read.Location = new System.Drawing.Point(214, 59);
            this.bt_Read.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Read.Name = "bt_Read";
            this.bt_Read.Size = new System.Drawing.Size(56, 19);
            this.bt_Read.TabIndex = 6;
            this.bt_Read.Text = "Read";
            this.bt_Read.UseVisualStyleBackColor = true;
            this.bt_Read.Click += new System.EventHandler(this.bt_Read_Click);
            // 
            // bt_Write
            // 
            this.bt_Write.Location = new System.Drawing.Point(214, 86);
            this.bt_Write.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Write.Name = "bt_Write";
            this.bt_Write.Size = new System.Drawing.Size(56, 19);
            this.bt_Write.TabIndex = 7;
            this.bt_Write.Text = "Write";
            this.bt_Write.UseVisualStyleBackColor = true;
            this.bt_Write.Click += new System.EventHandler(this.bt_Write_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cB_RxMode);
            this.groupBox1.Controls.Add(this.bt_Init100M);
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.cB_M_VeRead);
            this.groupBox1.Controls.Add(this.bt_Write);
            this.groupBox1.Controls.Add(this.bt_Read);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cB_Registers);
            this.groupBox1.Controls.Add(this.tB_M_SPICommand);
            this.groupBox1.Controls.Add(this.cB_Boards);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tB_M_Addr);
            this.groupBox1.Controls.Add(this.tB_M_DataSize);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tB_M_Value);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.tB_M_BoardNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1039, 200);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debug";
            // 
            // cB_RxMode
            // 
            this.cB_RxMode.AutoSize = true;
            this.cB_RxMode.Location = new System.Drawing.Point(63, 65);
            this.cB_RxMode.Name = "cB_RxMode";
            this.cB_RxMode.Size = new System.Drawing.Size(107, 17);
            this.cB_RxMode.TabIndex = 40;
            this.cB_RxMode.Text = "Rx Module Mode";
            this.cB_RxMode.UseVisualStyleBackColor = true;
            this.cB_RxMode.Visible = false;
            this.cB_RxMode.CheckedChanged += new System.EventHandler(this.cB_RxMode_CheckedChanged);
            // 
            // bt_Init100M
            // 
            this.bt_Init100M.Location = new System.Drawing.Point(640, 18);
            this.bt_Init100M.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Init100M.Name = "bt_Init100M";
            this.bt_Init100M.Size = new System.Drawing.Size(93, 33);
            this.bt_Init100M.TabIndex = 39;
            this.bt_Init100M.Text = "Init_100M_PLL";
            this.bt_Init100M.UseVisualStyleBackColor = true;
            this.bt_Init100M.Click += new System.EventHandler(this.bt_Init100M_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 134);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(1028, 54);
            this.dataGridView2.TabIndex = 38;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView2_KeyPress);
            // 
            // cB_M_VeRead
            // 
            this.cB_M_VeRead.AutoSize = true;
            this.cB_M_VeRead.Checked = true;
            this.cB_M_VeRead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_M_VeRead.Location = new System.Drawing.Point(483, 88);
            this.cB_M_VeRead.Margin = new System.Windows.Forms.Padding(2);
            this.cB_M_VeRead.Name = "cB_M_VeRead";
            this.cB_M_VeRead.Size = new System.Drawing.Size(62, 17);
            this.cB_M_VeRead.TabIndex = 36;
            this.cB_M_VeRead.Text = "上升延";
            this.cB_M_VeRead.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(373, 42);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 35;
            this.label14.Text = "B";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(297, 42);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "SPI Command:";
            // 
            // tB_M_SPICommand
            // 
            this.tB_M_SPICommand.Enabled = false;
            this.tB_M_SPICommand.Location = new System.Drawing.Point(387, 41);
            this.tB_M_SPICommand.Margin = new System.Windows.Forms.Padding(2);
            this.tB_M_SPICommand.Name = "tB_M_SPICommand";
            this.tB_M_SPICommand.Size = new System.Drawing.Size(92, 20);
            this.tB_M_SPICommand.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(316, 20);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Board NO:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(320, 89);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "DataSize:";
            // 
            // tB_M_Addr
            // 
            this.tB_M_Addr.Location = new System.Drawing.Point(387, 63);
            this.tB_M_Addr.Margin = new System.Windows.Forms.Padding(2);
            this.tB_M_Addr.Name = "tB_M_Addr";
            this.tB_M_Addr.Size = new System.Drawing.Size(92, 20);
            this.tB_M_Addr.TabIndex = 17;
            // 
            // tB_M_DataSize
            // 
            this.tB_M_DataSize.Location = new System.Drawing.Point(387, 86);
            this.tB_M_DataSize.Margin = new System.Windows.Forms.Padding(2);
            this.tB_M_DataSize.Name = "tB_M_DataSize";
            this.tB_M_DataSize.Size = new System.Drawing.Size(92, 20);
            this.tB_M_DataSize.TabIndex = 31;
            this.tB_M_DataSize.Text = "32";
            this.tB_M_DataSize.TextChanged += new System.EventHandler(this.tB_M_DataSize_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(370, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "0x";
            // 
            // tB_M_Value
            // 
            this.tB_M_Value.Location = new System.Drawing.Point(63, 110);
            this.tB_M_Value.Margin = new System.Windows.Forms.Padding(2);
            this.tB_M_Value.Name = "tB_M_Value";
            this.tB_M_Value.Size = new System.Drawing.Size(416, 20);
            this.tB_M_Value.TabIndex = 19;
            this.tB_M_Value.TextChanged += new System.EventHandler(this.tB_M_Value_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 110);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "0x";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(549, 63);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 19);
            this.button4.TabIndex = 21;
            this.button4.Text = "Read";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(549, 84);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 19);
            this.button3.TabIndex = 22;
            this.button3.Text = "Write";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tB_M_BoardNo
            // 
            this.tB_M_BoardNo.Location = new System.Drawing.Point(387, 18);
            this.tB_M_BoardNo.Margin = new System.Windows.Forms.Padding(2);
            this.tB_M_BoardNo.Name = "tB_M_BoardNo";
            this.tB_M_BoardNo.Size = new System.Drawing.Size(92, 20);
            this.tB_M_BoardNo.TabIndex = 26;
            this.tB_M_BoardNo.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Addr:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 110);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Value:";
            // 
            // tBox_Header
            // 
            this.tBox_Header.Location = new System.Drawing.Point(14, 317);
            this.tBox_Header.Name = "tBox_Header";
            this.tBox_Header.Size = new System.Drawing.Size(345, 20);
            this.tBox_Header.TabIndex = 9;
            // 
            // bt_ReadHeader
            // 
            this.bt_ReadHeader.Location = new System.Drawing.Point(79, 343);
            this.bt_ReadHeader.Name = "bt_ReadHeader";
            this.bt_ReadHeader.Size = new System.Drawing.Size(89, 23);
            this.bt_ReadHeader.TabIndex = 10;
            this.bt_ReadHeader.Text = "Read Header";
            this.bt_ReadHeader.UseVisualStyleBackColor = true;
            this.bt_ReadHeader.Click += new System.EventHandler(this.bt_ReadHeader_Click);
            // 
            // bt_WriteHeader
            // 
            this.bt_WriteHeader.Location = new System.Drawing.Point(184, 343);
            this.bt_WriteHeader.Name = "bt_WriteHeader";
            this.bt_WriteHeader.Size = new System.Drawing.Size(88, 23);
            this.bt_WriteHeader.TabIndex = 11;
            this.bt_WriteHeader.Text = "Write Header";
            this.bt_WriteHeader.UseVisualStyleBackColor = true;
            this.bt_WriteHeader.Click += new System.EventHandler(this.bt_WriteHeader_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(283, 343);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Write_Read";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DebugBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1102, 684);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DebugBoard";
            this.BoardName = "Debug";
            this.Text = "DebugBoard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DebugBoard_FormClosed);
            this.Load += new System.EventHandler(this.SlideForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cB_Registers;
        private System.Windows.Forms.Button bt_Read;
        private System.Windows.Forms.Button bt_Write;
        private System.Windows.Forms.ComboBox cB_Boards;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tB_M_Addr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tB_M_Value;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tB_M_BoardNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tB_M_DataSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tB_M_SPICommand;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cB_M_VeRead;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox rTB_Flash_Data;
        private System.Windows.Forms.Button bt_Flash_Write;
        private System.Windows.Forms.Button bt_Flash_Read;
        private System.Windows.Forms.TextBox tB_Flash_Addr;
        private System.Windows.Forms.Button bt_Flash_Erase;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbAbusNames;
        private System.Windows.Forms.Button Read;
        private System.Windows.Forms.Button RefreshAll;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bt_Init100M;
        private System.Windows.Forms.CheckBox cB_RxMode;
        private System.Windows.Forms.Button bt_WriteHeader;
        private System.Windows.Forms.Button bt_ReadHeader;
        private System.Windows.Forms.TextBox tBox_Header;
        private System.Windows.Forms.Button button2;
    }
}

