namespace tree_test
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtNodeInfo = new System.Windows.Forms.TextBox();
            this.btn_confilm_checkbox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(3, 1);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(397, 452);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // txtNodeInfo
            // 
            this.txtNodeInfo.Location = new System.Drawing.Point(406, 1);
            this.txtNodeInfo.Multiline = true;
            this.txtNodeInfo.Name = "txtNodeInfo";
            this.txtNodeInfo.Size = new System.Drawing.Size(389, 452);
            this.txtNodeInfo.TabIndex = 1;
            // 
            // btn_confilm_checkbox
            // 
            this.btn_confilm_checkbox.Location = new System.Drawing.Point(13, 460);
            this.btn_confilm_checkbox.Name = "btn_confilm_checkbox";
            this.btn_confilm_checkbox.Size = new System.Drawing.Size(387, 23);
            this.btn_confilm_checkbox.TabIndex = 2;
            this.btn_confilm_checkbox.Text = "確認勾選";
            this.btn_confilm_checkbox.UseVisualStyleBackColor = true;
            this.btn_confilm_checkbox.Click += new System.EventHandler(this.btn_confilm_checkbox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 482);
            this.Controls.Add(this.btn_confilm_checkbox);
            this.Controls.Add(this.txtNodeInfo);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox txtNodeInfo;
        private System.Windows.Forms.Button btn_confilm_checkbox;
    }
}

