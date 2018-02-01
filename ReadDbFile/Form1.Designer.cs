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
            this.ParseError_File_Adjust_Btn = new System.Windows.Forms.Button();
            this.Calc_Nhour_Pop_GDP_Btn = new System.Windows.Forms.Button();
            this.Add_Pop_Gdp_Btn = new System.Windows.Forms.Button();
            this.extract_Routes_City_Info = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PreVersionMode = new System.Windows.Forms.RadioButton();
            this.WalkRbtn = new System.Windows.Forms.RadioButton();
            this.RidRbtn = new System.Windows.Forms.RadioButton();
            this.PublictRbtn = new System.Windows.Forms.RadioButton();
            this.CarRbtn = new System.Windows.Forms.RadioButton();
            this.Choose_OD_EXC_BTN = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadBbBtn
            // 
            this.ReadBbBtn.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReadBbBtn.Location = new System.Drawing.Point(20, 3);
            this.ReadBbBtn.Name = "ReadBbBtn";
            this.ReadBbBtn.Size = new System.Drawing.Size(200, 91);
            this.ReadBbBtn.TabIndex = 0;
            this.ReadBbBtn.Text = "读取Route.DB文件";
            this.ReadBbBtn.UseVisualStyleBackColor = true;
            this.ReadBbBtn.Click += new System.EventHandler(this.ReadBbBtn_Click);
            // 
            // BtnReadAdditionFile
            // 
            this.BtnReadAdditionFile.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnReadAdditionFile.Location = new System.Drawing.Point(16, 253);
            this.BtnReadAdditionFile.Name = "BtnReadAdditionFile";
            this.BtnReadAdditionFile.Size = new System.Drawing.Size(99, 43);
            this.BtnReadAdditionFile.TabIndex = 1;
            this.BtnReadAdditionFile.Text = "读取附加文件";
            this.BtnReadAdditionFile.UseVisualStyleBackColor = true;
            this.BtnReadAdditionFile.Click += new System.EventHandler(this.BtnReadAdditionFile_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.Calc_Nhour_Pop_GDP_Btn);
            this.panel1.Controls.Add(this.Add_Pop_Gdp_Btn);
            this.panel1.Controls.Add(this.ReadBbBtn);
            this.panel1.Controls.Add(this.BtnReadAdditionFile);
            this.panel1.Controls.Add(this.extract_Routes_City_Info);
            this.panel1.Location = new System.Drawing.Point(276, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 310);
            this.panel1.TabIndex = 2;
            // 
            // ParseError_File_Adjust_Btn
            // 
            this.ParseError_File_Adjust_Btn.Location = new System.Drawing.Point(47, 333);
            this.ParseError_File_Adjust_Btn.Name = "ParseError_File_Adjust_Btn";
            this.ParseError_File_Adjust_Btn.Size = new System.Drawing.Size(200, 35);
            this.ParseError_File_Adjust_Btn.TabIndex = 4;
            this.ParseError_File_Adjust_Btn.Text = "给未抓取文件做格式调整";
            this.ParseError_File_Adjust_Btn.UseVisualStyleBackColor = true;
            this.ParseError_File_Adjust_Btn.Click += new System.EventHandler(this.ParseError_File_Adjust_Btn_Click);
            // 
            // Calc_Nhour_Pop_GDP_Btn
            // 
            this.Calc_Nhour_Pop_GDP_Btn.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Calc_Nhour_Pop_GDP_Btn.Location = new System.Drawing.Point(20, 176);
            this.Calc_Nhour_Pop_GDP_Btn.Name = "Calc_Nhour_Pop_GDP_Btn";
            this.Calc_Nhour_Pop_GDP_Btn.Size = new System.Drawing.Size(200, 67);
            this.Calc_Nhour_Pop_GDP_Btn.TabIndex = 3;
            this.Calc_Nhour_Pop_GDP_Btn.Text = "计算时段覆盖人口和GDP";
            this.Calc_Nhour_Pop_GDP_Btn.UseVisualStyleBackColor = true;
            this.Calc_Nhour_Pop_GDP_Btn.Click += new System.EventHandler(this.Calc_Nhour_Pop_GDP_Btn_Click);
            // 
            // Add_Pop_Gdp_Btn
            // 
            this.Add_Pop_Gdp_Btn.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Add_Pop_Gdp_Btn.Location = new System.Drawing.Point(20, 100);
            this.Add_Pop_Gdp_Btn.Name = "Add_Pop_Gdp_Btn";
            this.Add_Pop_Gdp_Btn.Size = new System.Drawing.Size(200, 67);
            this.Add_Pop_Gdp_Btn.TabIndex = 2;
            this.Add_Pop_Gdp_Btn.Text = "增加人口和GDP到db";
            this.Add_Pop_Gdp_Btn.UseVisualStyleBackColor = true;
            this.Add_Pop_Gdp_Btn.Click += new System.EventHandler(this.Add_Pop_Gdp_Btn_Click);
            // 
            // extract_Routes_City_Info
            // 
            this.extract_Routes_City_Info.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extract_Routes_City_Info.Location = new System.Drawing.Point(121, 253);
            this.extract_Routes_City_Info.Name = "extract_Routes_City_Info";
            this.extract_Routes_City_Info.Size = new System.Drawing.Size(99, 43);
            this.extract_Routes_City_Info.TabIndex = 1;
            this.extract_Routes_City_Info.Text = "制作路径表信息";
            this.extract_Routes_City_Info.UseVisualStyleBackColor = true;
            this.extract_Routes_City_Info.Click += new System.EventHandler(this.extract_Routes_City_Info_Click);
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
            // Choose_OD_EXC_BTN
            // 
            this.Choose_OD_EXC_BTN.Location = new System.Drawing.Point(47, 292);
            this.Choose_OD_EXC_BTN.Name = "Choose_OD_EXC_BTN";
            this.Choose_OD_EXC_BTN.Size = new System.Drawing.Size(200, 35);
            this.Choose_OD_EXC_BTN.TabIndex = 4;
            this.Choose_OD_EXC_BTN.Text = "选择OD_Excel文件";
            this.Choose_OD_EXC_BTN.UseVisualStyleBackColor = true;
            this.Choose_OD_EXC_BTN.Click += new System.EventHandler(this.Choose_OD_EXC_BTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 374);
            this.Controls.Add(this.ParseError_File_Adjust_Btn);
            this.Controls.Add(this.Choose_OD_EXC_BTN);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button extract_Routes_City_Info;
        private System.Windows.Forms.Button Add_Pop_Gdp_Btn;
        private System.Windows.Forms.Button Calc_Nhour_Pop_GDP_Btn;
        private System.Windows.Forms.Button ParseError_File_Adjust_Btn;
        private System.Windows.Forms.Button Choose_OD_EXC_BTN;
    }
}

