namespace DELTA_Service_Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_info = new System.Windows.Forms.RichTextBox();
            this.btn_setting = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_new_CNC = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_Timeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_RemoteIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Lv_Addresses = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.check_new_tag = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_XML_Path = new System.Windows.Forms.TextBox();
            this.btn_xml_read = new System.Windows.Forms.Button();
            this.btn_export_csv = new System.Windows.Forms.Button();
            this.btn_origin = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_info);
            this.groupBox1.Location = new System.Drawing.Point(11, 330);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 98);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "訊息列";
            // 
            // txt_info
            // 
            this.txt_info.Location = new System.Drawing.Point(17, 17);
            this.txt_info.Name = "txt_info";
            this.txt_info.Size = new System.Drawing.Size(735, 81);
            this.txt_info.TabIndex = 1;
            this.txt_info.Text = "";
            this.txt_info.TextChanged += new System.EventHandler(this.txt_info_TextChanged_1);
            // 
            // btn_setting
            // 
            this.btn_setting.Location = new System.Drawing.Point(11, 289);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(770, 23);
            this.btn_setting.TabIndex = 6;
            this.btn_setting.Text = "設定";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_new_CNC);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.txt_Timeout);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txt_RemoteIP);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(11, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(766, 236);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "調整";
            // 
            // btn_new_CNC
            // 
            this.btn_new_CNC.Location = new System.Drawing.Point(19, 195);
            this.btn_new_CNC.Name = "btn_new_CNC";
            this.btn_new_CNC.Size = new System.Drawing.Size(607, 23);
            this.btn_new_CNC.TabIndex = 8;
            this.btn_new_CNC.Text = "新增機台";
            this.btn_new_CNC.UseVisualStyleBackColor = true;
            this.btn_new_CNC.Click += new System.EventHandler(this.btn_new_CNC_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "CNC連線資訊:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(650, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 82);
            this.button1.TabIndex = 6;
            this.button1.Text = "機台細項設定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txt_Timeout
            // 
            this.txt_Timeout.Location = new System.Drawing.Point(534, 153);
            this.txt_Timeout.Name = "txt_Timeout";
            this.txt_Timeout.Size = new System.Drawing.Size(92, 22);
            this.txt_Timeout.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(464, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "TimeOut:";
            // 
            // txt_RemoteIP
            // 
            this.txt_RemoteIP.Location = new System.Drawing.Point(174, 150);
            this.txt_RemoteIP.Name = "txt_RemoteIP";
            this.txt_RemoteIP.Size = new System.Drawing.Size(266, 22);
            this.txt_RemoteIP.TabIndex = 2;
            this.txt_RemoteIP.Leave += new System.EventHandler(this.txt_RemoteIP_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Remote IP:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Lv_Addresses);
            this.groupBox4.Location = new System.Drawing.Point(12, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(747, 126);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "連線資訊";
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
            this.Lv_Addresses.Size = new System.Drawing.Size(741, 105);
            this.Lv_Addresses.TabIndex = 1;
            this.Lv_Addresses.UseCompatibleStateImageBehavior = false;
            this.Lv_Addresses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP Address";
            this.columnHeader1.Width = 193;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Mac Address";
            this.columnHeader2.Width = 173;
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
            // check_new_tag
            // 
            this.check_new_tag.AutoSize = true;
            this.check_new_tag.Location = new System.Drawing.Point(23, 256);
            this.check_new_tag.Name = "check_new_tag";
            this.check_new_tag.Size = new System.Drawing.Size(107, 16);
            this.check_new_tag.TabIndex = 8;
            this.check_new_tag.Text = "是否有Tag異動?";
            this.check_new_tag.UseVisualStyleBackColor = true;
            this.check_new_tag.CheckedChanged += new System.EventHandler(this.check_new_tag_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "XML檔路徑:";
            // 
            // txt_XML_Path
            // 
            this.txt_XML_Path.Enabled = false;
            this.txt_XML_Path.Location = new System.Drawing.Point(211, 254);
            this.txt_XML_Path.Name = "txt_XML_Path";
            this.txt_XML_Path.Size = new System.Drawing.Size(363, 22);
            this.txt_XML_Path.TabIndex = 5;
            // 
            // btn_xml_read
            // 
            this.btn_xml_read.Enabled = false;
            this.btn_xml_read.Location = new System.Drawing.Point(580, 252);
            this.btn_xml_read.Name = "btn_xml_read";
            this.btn_xml_read.Size = new System.Drawing.Size(75, 23);
            this.btn_xml_read.TabIndex = 10;
            this.btn_xml_read.Text = "讀取XML";
            this.btn_xml_read.UseVisualStyleBackColor = true;
            this.btn_xml_read.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_export_csv
            // 
            this.btn_export_csv.Enabled = false;
            this.btn_export_csv.Location = new System.Drawing.Point(692, 254);
            this.btn_export_csv.Name = "btn_export_csv";
            this.btn_export_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_export_csv.TabIndex = 34;
            this.btn_export_csv.Text = "Export CSV";
            this.btn_export_csv.UseVisualStyleBackColor = true;
            this.btn_export_csv.Click += new System.EventHandler(this.btn_export_csv_Click);
            // 
            // btn_origin
            // 
            this.btn_origin.Location = new System.Drawing.Point(11, 434);
            this.btn_origin.Name = "btn_origin";
            this.btn_origin.Size = new System.Drawing.Size(119, 23);
            this.btn_origin.TabIndex = 35;
            this.btn_origin.Text = "回覆原始設定";
            this.btn_origin.UseVisualStyleBackColor = true;
            this.btn_origin.Click += new System.EventHandler(this.btn_origin_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(226, 433);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 36;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 460);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_origin);
            this.Controls.Add(this.btn_export_csv);
            this.Controls.Add(this.btn_xml_read);
            this.Controls.Add(this.txt_XML_Path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.check_new_tag);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_setting);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_Timeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_RemoteIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView Lv_Addresses;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.CheckBox check_new_tag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_XML_Path;
        private System.Windows.Forms.Button btn_xml_read;
        private System.Windows.Forms.Button btn_export_csv;
        private System.Windows.Forms.Button btn_origin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txt_info;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_new_CNC;
        private System.Windows.Forms.Button button2;
    }
}

