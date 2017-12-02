namespace ReadDbFile
{
    partial class Form1
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
            this.ReadBbBtn = new System.Windows.Forms.Button();
            this.BtnReadAdditionFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ReadBbBtn
            // 
            this.ReadBbBtn.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReadBbBtn.Location = new System.Drawing.Point(59, 36);
            this.ReadBbBtn.Name = "ReadBbBtn";
            this.ReadBbBtn.Size = new System.Drawing.Size(334, 131);
            this.ReadBbBtn.TabIndex = 0;
            this.ReadBbBtn.Text = "读取Db文件";
            this.ReadBbBtn.UseVisualStyleBackColor = true;
            this.ReadBbBtn.Click += new System.EventHandler(this.ReadBbBtn_Click);
            // 
            // BtnReadAdditionFile
            // 
            this.BtnReadAdditionFile.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnReadAdditionFile.Location = new System.Drawing.Point(59, 185);
            this.BtnReadAdditionFile.Name = "BtnReadAdditionFile";
            this.BtnReadAdditionFile.Size = new System.Drawing.Size(334, 131);
            this.BtnReadAdditionFile.TabIndex = 1;
            this.BtnReadAdditionFile.Text = "读取附加文件";
            this.BtnReadAdditionFile.UseVisualStyleBackColor = true;
            this.BtnReadAdditionFile.Click += new System.EventHandler(this.BtnReadAdditionFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 379);
            this.Controls.Add(this.BtnReadAdditionFile);
            this.Controls.Add(this.ReadBbBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ReadBbBtn;
        private System.Windows.Forms.Button BtnReadAdditionFile;
    }
}

