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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PreVersionMode = new System.Windows.Forms.RadioButton();
            this.WalkRbtn = new System.Windows.Forms.RadioButton();
            this.RidRbtn = new System.Windows.Forms.RadioButton();
            this.PublictRbtn = new System.Windows.Forms.RadioButton();
            this.CarRbtn = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadBbBtn
            // 
            this.ReadBbBtn.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReadBbBtn.Location = new System.Drawing.Point(275, 39);
            this.ReadBbBtn.Name = "ReadBbBtn";
            this.ReadBbBtn.Size = new System.Drawing.Size(223, 79);
            this.ReadBbBtn.TabIndex = 0;
            this.ReadBbBtn.Text = "读取Route.DB文件";
            this.ReadBbBtn.UseVisualStyleBackColor = true;
            this.ReadBbBtn.Click += new System.EventHandler(this.ReadBbBtn_Click);
            // 
            // BtnReadAdditionFile
            // 
            this.BtnReadAdditionFile.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnReadAdditionFile.Location = new System.Drawing.Point(298, 139);
            this.BtnReadAdditionFile.Name = "BtnReadAdditionFile";
            this.BtnReadAdditionFile.Size = new System.Drawing.Size(200, 91);
            this.BtnReadAdditionFile.TabIndex = 1;
            this.BtnReadAdditionFile.Text = "读取附加文件";
            this.BtnReadAdditionFile.UseVisualStyleBackColor = true;
            this.BtnReadAdditionFile.Click += new System.EventHandler(this.BtnReadAdditionFile_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(276, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 111);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.PreVersionMode);
            this.panel2.Controls.Add(this.WalkRbtn);
            this.panel2.Controls.Add(this.RidRbtn);
            this.panel2.Controls.Add(this.PublictRbtn);
            this.panel2.Controls.Add(this.CarRbtn);
            this.panel2.Location = new System.Drawing.Point(24, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(223, 226);
            this.panel2.TabIndex = 3;
            // 
            // PreVersionMode
            // 
            this.PreVersionMode.AutoSize = true;
            this.PreVersionMode.Location = new System.Drawing.Point(31, 176);
            this.PreVersionMode.Name = "PreVersionMode";
            this.PreVersionMode.Size = new System.Drawing.Size(119, 16);
            this.PreVersionMode.TabIndex = 2;
            this.PreVersionMode.TabStop = true;
            this.PreVersionMode.Text = "Previous Version";
            this.PreVersionMode.UseVisualStyleBackColor = true;
            this.PreVersionMode.CheckedChanged += new System.EventHandler(this.WalkRbtn_CheckedChanged);
            // 
            // WalkRbtn
            // 
            this.WalkRbtn.AutoSize = true;
            this.WalkRbtn.Location = new System.Drawing.Point(31, 138);
            this.WalkRbtn.Name = "WalkRbtn";
            this.WalkRbtn.Size = new System.Drawing.Size(95, 16);
            this.WalkRbtn.TabIndex = 2;
            this.WalkRbtn.TabStop = true;
            this.WalkRbtn.Text = "Walking Mode";
            this.WalkRbtn.UseVisualStyleBackColor = true;
            this.WalkRbtn.CheckedChanged += new System.EventHandler(this.WalkRbtn_CheckedChanged);
            // 
            // RidRbtn
            // 
            this.RidRbtn.AutoSize = true;
            this.RidRbtn.Location = new System.Drawing.Point(31, 98);
            this.RidRbtn.Name = "RidRbtn";
            this.RidRbtn.Size = new System.Drawing.Size(89, 16);
            this.RidRbtn.TabIndex = 2;
            this.RidRbtn.TabStop = true;
            this.RidRbtn.Text = "Riding Mode";
            this.RidRbtn.UseVisualStyleBackColor = true;
            this.RidRbtn.CheckedChanged += new System.EventHandler(this.RidRbtn_CheckedChanged);
            // 
            // PublictRbtn
            // 
            this.PublictRbtn.AutoSize = true;
            this.PublictRbtn.Location = new System.Drawing.Point(31, 59);
            this.PublictRbtn.Name = "PublictRbtn";
            this.PublictRbtn.Size = new System.Drawing.Size(149, 16);
            this.PublictRbtn.TabIndex = 2;
            this.PublictRbtn.TabStop = true;
            this.PublictRbtn.Text = "public transport Mode";
            this.PublictRbtn.UseVisualStyleBackColor = true;
            this.PublictRbtn.CheckedChanged += new System.EventHandler(this.PublictRbtn_CheckedChanged);
            // 
            // CarRbtn
            // 
            this.CarRbtn.AutoSize = true;
            this.CarRbtn.Location = new System.Drawing.Point(31, 23);
            this.CarRbtn.Name = "CarRbtn";
            this.CarRbtn.Size = new System.Drawing.Size(71, 16);
            this.CarRbtn.TabIndex = 1;
            this.CarRbtn.TabStop = true;
            this.CarRbtn.Text = "Car Mode";
            this.CarRbtn.UseVisualStyleBackColor = true;
            this.CarRbtn.CheckedChanged += new System.EventHandler(this.CarRbtn_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 318);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BtnReadAdditionFile);
            this.Controls.Add(this.ReadBbBtn);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ReadBbBtn;
        private System.Windows.Forms.Button BtnReadAdditionFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton WalkRbtn;
        private System.Windows.Forms.RadioButton RidRbtn;
        private System.Windows.Forms.RadioButton PublictRbtn;
        private System.Windows.Forms.RadioButton CarRbtn;
        private System.Windows.Forms.RadioButton PreVersionMode;
    }
}

