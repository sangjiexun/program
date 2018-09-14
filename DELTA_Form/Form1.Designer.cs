namespace DELTA_Form
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "通道1",
            "1"}, -1);
            this.Btn_Activate = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.btn_XML_Create = new System.Windows.Forms.Button();
            this.txt_connect_state = new System.Windows.Forms.TextBox();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.List_cns = new System.Windows.Forms.ListBox();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lv_Addresses = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Table_Main = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.List_Channels = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Table_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Activate
            // 
            this.Btn_Activate.Location = new System.Drawing.Point(581, 3);
            this.Btn_Activate.Name = "Btn_Activate";
            this.Btn_Activate.Size = new System.Drawing.Size(75, 23);
            this.Btn_Activate.TabIndex = 0;
            this.Btn_Activate.Text = "啟動";
            this.Btn_Activate.UseVisualStyleBackColor = true;
            this.Btn_Activate.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.Table_Main.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Stop);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Activate);
            this.flowLayoutPanel1.Controls.Add(this.btn_XML_Create);
            this.flowLayoutPanel1.Controls.Add(this.txt_connect_state);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 433);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(740, 28);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.Location = new System.Drawing.Point(662, 3);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.Btn_Stop.TabIndex = 2;
            this.Btn_Stop.Text = "停止";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // btn_XML_Create
            // 
            this.btn_XML_Create.Location = new System.Drawing.Point(500, 3);
            this.btn_XML_Create.Name = "btn_XML_Create";
            this.btn_XML_Create.Size = new System.Drawing.Size(75, 23);
            this.btn_XML_Create.TabIndex = 3;
            this.btn_XML_Create.Text = "創建XML";
            this.btn_XML_Create.UseVisualStyleBackColor = true;
            this.btn_XML_Create.Click += new System.EventHandler(this.btn_XML_Create_Click);
            // 
            // txt_connect_state
            // 
            this.txt_connect_state.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_connect_state.Location = new System.Drawing.Point(9, 3);
            this.txt_connect_state.Name = "txt_connect_state";
            this.txt_connect_state.ReadOnly = true;
            this.txt_connect_state.Size = new System.Drawing.Size(485, 22);
            this.txt_connect_state.TabIndex = 6;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "軸名稱";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "埠代號";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "類別";
            this.columnHeader7.Width = 74;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "代碼";
            this.columnHeader5.Width = 68;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "軸數";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "啟用";
            // 
            // List_cns
            // 
            this.List_cns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List_cns.FormattingEnabled = true;
            this.List_cns.ItemHeight = 12;
            this.List_cns.Location = new System.Drawing.Point(0, 0);
            this.List_cns.Name = "List_cns";
            this.List_cns.Size = new System.Drawing.Size(192, 295);
            this.List_cns.TabIndex = 2;
            this.List_cns.SelectedIndexChanged += new System.EventHandler(this.List_cns_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Mac Address";
            this.columnHeader2.Width = 173;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP Address";
            this.columnHeader1.Width = 193;
            // 
            // Lv_Addresses
            // 
            this.Lv_Addresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.Lv_Addresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lv_Addresses.Location = new System.Drawing.Point(3, 18);
            this.Lv_Addresses.Name = "Lv_Addresses";
            this.Lv_Addresses.Size = new System.Drawing.Size(734, 102);
            this.Lv_Addresses.TabIndex = 0;
            this.Lv_Addresses.UseCompatibleStateImageBehavior = false;
            this.Lv_Addresses.View = System.Windows.Forms.View.Details;
            this.Lv_Addresses.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.Lv_Addresses_ItemSelectionChanged);
            this.Lv_Addresses.SelectedIndexChanged += new System.EventHandler(this.Lv_Addresses_SelectedIndexChanged);
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Mask";
            this.columnHeader10.Width = 159;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Gateway";
            this.columnHeader11.Width = 156;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "DHCP";
            this.columnHeader12.Width = 49;
            // 
            // groupBox1
            // 
            this.Table_Main.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.Lv_Addresses);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP 位址";
            // 
            // Table_Main
            // 
            this.Table_Main.ColumnCount = 2;
            this.Table_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.Table_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 471F));
            this.Table_Main.Controls.Add(this.groupBox1, 0, 0);
            this.Table_Main.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.Table_Main.Controls.Add(this.splitContainer1, 0, 1);
            this.Table_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table_Main.Location = new System.Drawing.Point(0, 0);
            this.Table_Main.Name = "Table_Main";
            this.Table_Main.RowCount = 3;
            this.Table_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.Table_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.Table_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.Table_Main.Size = new System.Drawing.Size(746, 464);
            this.Table_Main.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.Table_Main.SetColumnSpan(this.splitContainer1, 2);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 132);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.List_cns);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.List_Channels);
            this.splitContainer1.Size = new System.Drawing.Size(740, 295);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 6;
            // 
            // List_Channels
            // 
            this.List_Channels.CheckBoxes = true;
            this.List_Channels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader6,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.List_Channels.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewItem1.StateImageIndex = 0;
            this.List_Channels.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.List_Channels.Location = new System.Drawing.Point(0, 0);
            this.List_Channels.Name = "List_Channels";
            this.List_Channels.Size = new System.Drawing.Size(544, 295);
            this.List_Channels.TabIndex = 4;
            this.List_Channels.UseCompatibleStateImageBehavior = false;
            this.List_Channels.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "通道";
            this.columnHeader3.Width = 71;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 464);
            this.Controls.Add(this.Table_Main);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.Table_Main.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Button Btn_Activate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListBox List_cns;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView Lv_Addresses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel Table_Main;
        private System.Windows.Forms.ListView List_Channels;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.TextBox txt_connect_state;
        private System.Windows.Forms.Button btn_XML_Create;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

