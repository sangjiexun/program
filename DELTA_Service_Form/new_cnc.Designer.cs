namespace DELTA_Service_Form
{
    partial class new_cnc
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lv_Addresses = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox_Different_LocalIP = new System.Windows.Forms.CheckBox();
            this.txt_Timeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_RemoteIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_new_connectInformation = new System.Windows.Forms.Button();
            this.LV_ALLCNC = new System.Windows.Forms.ListView();
            this.LocalIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RemoteIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Timeout = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lv_Addresses);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(866, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新增額外機台";
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
            this.Lv_Addresses.Size = new System.Drawing.Size(860, 149);
            this.Lv_Addresses.TabIndex = 2;
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
            this.columnHeader2.Width = 174;
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
            // checkBox_Different_LocalIP
            // 
            this.checkBox_Different_LocalIP.AutoSize = true;
            this.checkBox_Different_LocalIP.Location = new System.Drawing.Point(15, 6);
            this.checkBox_Different_LocalIP.Name = "checkBox_Different_LocalIP";
            this.checkBox_Different_LocalIP.Size = new System.Drawing.Size(123, 16);
            this.checkBox_Different_LocalIP.TabIndex = 1;
            this.checkBox_Different_LocalIP.Text = "使用不同的Local IP";
            this.checkBox_Different_LocalIP.UseVisualStyleBackColor = true;
            this.checkBox_Different_LocalIP.CheckedChanged += new System.EventHandler(this.checkBox_Different_LocalIP_CheckedChanged);
            // 
            // txt_Timeout
            // 
            this.txt_Timeout.Location = new System.Drawing.Point(443, 217);
            this.txt_Timeout.Name = "txt_Timeout";
            this.txt_Timeout.Size = new System.Drawing.Size(92, 22);
            this.txt_Timeout.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "TimeOut:";
            // 
            // txt_RemoteIP
            // 
            this.txt_RemoteIP.Location = new System.Drawing.Point(83, 214);
            this.txt_RemoteIP.Name = "txt_RemoteIP";
            this.txt_RemoteIP.Size = new System.Drawing.Size(266, 22);
            this.txt_RemoteIP.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Remote IP:";
            // 
            // btn_new_connectInformation
            // 
            this.btn_new_connectInformation.Location = new System.Drawing.Point(689, 215);
            this.btn_new_connectInformation.Name = "btn_new_connectInformation";
            this.btn_new_connectInformation.Size = new System.Drawing.Size(186, 23);
            this.btn_new_connectInformation.TabIndex = 9;
            this.btn_new_connectInformation.Text = "新增連線資訊";
            this.btn_new_connectInformation.UseVisualStyleBackColor = true;
            this.btn_new_connectInformation.Click += new System.EventHandler(this.btn_new_connectInformation_Click);
            // 
            // LV_ALLCNC
            // 
            this.LV_ALLCNC.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LocalIP,
            this.RemoteIP,
            this.Timeout});
            this.LV_ALLCNC.Location = new System.Drawing.Point(12, 261);
            this.LV_ALLCNC.Name = "LV_ALLCNC";
            this.LV_ALLCNC.Size = new System.Drawing.Size(863, 197);
            this.LV_ALLCNC.TabIndex = 10;
            this.LV_ALLCNC.UseCompatibleStateImageBehavior = false;
            this.LV_ALLCNC.View = System.Windows.Forms.View.Details;
            // 
            // LocalIP
            // 
            this.LocalIP.Text = "LocalIP";
            this.LocalIP.Width = 405;
            // 
            // RemoteIP
            // 
            this.RemoteIP.Text = "RemoteIP";
            this.RemoteIP.Width = 389;
            // 
            // Timeout
            // 
            this.Timeout.Text = "Timeout";
            this.Timeout.Width = 65;
            // 
            // new_cnc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 470);
            this.Controls.Add(this.LV_ALLCNC);
            this.Controls.Add(this.btn_new_connectInformation);
            this.Controls.Add(this.txt_Timeout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_RemoteIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_Different_LocalIP);
            this.Controls.Add(this.groupBox1);
            this.Name = "new_cnc";
            this.Text = "new_cnc";
            this.Load += new System.EventHandler(this.new_cnc_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView Lv_Addresses;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.CheckBox checkBox_Different_LocalIP;
        private System.Windows.Forms.TextBox txt_Timeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_RemoteIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_new_connectInformation;
        private System.Windows.Forms.ListView LV_ALLCNC;
        public System.Windows.Forms.ColumnHeader LocalIP;
        public System.Windows.Forms.ColumnHeader RemoteIP;
        public System.Windows.Forms.ColumnHeader Timeout;
    }
}