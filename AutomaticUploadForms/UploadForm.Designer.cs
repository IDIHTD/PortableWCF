namespace AutomaticUploadForms
{
    partial class UploadForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxProjectVersion = new System.Windows.Forms.TextBox();
            this.btnSelectePath = new System.Windows.Forms.Button();
            this.txtBoxLoadPath = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnGetFilesList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AppName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "项目名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "项目版本号：";
            // 
            // txtBoxProjectVersion
            // 
            this.txtBoxProjectVersion.Location = new System.Drawing.Point(353, 37);
            this.txtBoxProjectVersion.Name = "txtBoxProjectVersion";
            this.txtBoxProjectVersion.Size = new System.Drawing.Size(100, 21);
            this.txtBoxProjectVersion.TabIndex = 3;
            // 
            // btnSelectePath
            // 
            this.btnSelectePath.Location = new System.Drawing.Point(503, 35);
            this.btnSelectePath.Name = "btnSelectePath";
            this.btnSelectePath.Size = new System.Drawing.Size(92, 23);
            this.btnSelectePath.TabIndex = 4;
            this.btnSelectePath.Text = "选择上传路径";
            this.btnSelectePath.UseVisualStyleBackColor = true;
            this.btnSelectePath.Click += new System.EventHandler(this.btnSelectePath_Click);
            // 
            // txtBoxLoadPath
            // 
            this.txtBoxLoadPath.Location = new System.Drawing.Point(622, 37);
            this.txtBoxLoadPath.Name = "txtBoxLoadPath";
            this.txtBoxLoadPath.Size = new System.Drawing.Size(100, 21);
            this.txtBoxLoadPath.TabIndex = 5;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(798, 37);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "确认上传";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnGetFilesList
            // 
            this.btnGetFilesList.Location = new System.Drawing.Point(798, 104);
            this.btnGetFilesList.Name = "btnGetFilesList";
            this.btnGetFilesList.Size = new System.Drawing.Size(75, 23);
            this.btnGetFilesList.TabIndex = 8;
            this.btnGetFilesList.Text = "获取文件列表";
            this.btnGetFilesList.UseVisualStyleBackColor = true;
            this.btnGetFilesList.Click += new System.EventHandler(this.btnGetFilesList_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "对比客户端文件并下载";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "客户端程序名：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 101);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(245, 101);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "选择客户端路径";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(353, 101);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 13;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(111, 36);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 14;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AppName,
            this.AppVersion});
            this.dataGridView1.Location = new System.Drawing.Point(41, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(242, 150);
            this.dataGridView1.TabIndex = 15;
            // 
            // AppName
            // 
            this.AppName.DataPropertyName = "AppName";
            this.AppName.Frozen = true;
            this.AppName.HeaderText = "项目名称";
            this.AppName.Name = "AppName";
            // 
            // AppVersion
            // 
            this.AppVersion.DataPropertyName = "AppVersion";
            this.AppVersion.Frozen = true;
            this.AppVersion.HeaderText = "版本号";
            this.AppVersion.Name = "AppVersion";
            // 
            // UploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 657);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGetFilesList);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtBoxLoadPath);
            this.Controls.Add(this.btnSelectePath);
            this.Controls.Add(this.txtBoxProjectVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UploadForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxProjectVersion;
        private System.Windows.Forms.Button btnSelectePath;
        private System.Windows.Forms.TextBox txtBoxLoadPath;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnGetFilesList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppVersion;
    }
}

