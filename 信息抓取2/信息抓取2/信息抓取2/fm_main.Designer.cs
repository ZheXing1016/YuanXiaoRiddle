namespace 信息抓取2
{
    partial class fm_main
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
            this.bt_regular = new System.Windows.Forms.Button();
            this.bt_do = new System.Windows.Forms.Button();
            this.dt_EndData = new System.Windows.Forms.DateTimePicker();
            this.dt_StartData = new System.Windows.Forms.DateTimePicker();
            this.ck_DataForm = new System.Windows.Forms.CheckBox();
            this.lb_hold = new System.Windows.Forms.ListBox();
            this.lb_do = new System.Windows.Forms.ListBox();
            this.bt_go = new System.Windows.Forms.Button();
            this.bt_goall = new System.Windows.Forms.Button();
            this.bt_backall = new System.Windows.Forms.Button();
            this.bt_back = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_regular
            // 
            this.bt_regular.Location = new System.Drawing.Point(400, 28);
            this.bt_regular.Name = "bt_regular";
            this.bt_regular.Size = new System.Drawing.Size(75, 23);
            this.bt_regular.TabIndex = 11;
            this.bt_regular.Text = "规则编辑";
            this.bt_regular.UseVisualStyleBackColor = true;
            this.bt_regular.Click += new System.EventHandler(this.button2_Click);
            // 
            // bt_do
            // 
            this.bt_do.Location = new System.Drawing.Point(303, 28);
            this.bt_do.Name = "bt_do";
            this.bt_do.Size = new System.Drawing.Size(75, 23);
            this.bt_do.TabIndex = 10;
            this.bt_do.Text = "执行";
            this.bt_do.UseVisualStyleBackColor = true;
            this.bt_do.Click += new System.EventHandler(this.bt_do_Click);
            // 
            // dt_EndData
            // 
            this.dt_EndData.CustomFormat = "yyyy-MM-dd";
            this.dt_EndData.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_EndData.Location = new System.Drawing.Point(158, 30);
            this.dt_EndData.Name = "dt_EndData";
            this.dt_EndData.Size = new System.Drawing.Size(129, 21);
            this.dt_EndData.TabIndex = 9;
            // 
            // dt_StartData
            // 
            this.dt_StartData.CustomFormat = "yyyy-MM-dd";
            this.dt_StartData.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_StartData.Location = new System.Drawing.Point(12, 30);
            this.dt_StartData.Name = "dt_StartData";
            this.dt_StartData.Size = new System.Drawing.Size(129, 21);
            this.dt_StartData.TabIndex = 8;
            // 
            // ck_DataForm
            // 
            this.ck_DataForm.AutoSize = true;
            this.ck_DataForm.Location = new System.Drawing.Point(12, 8);
            this.ck_DataForm.Name = "ck_DataForm";
            this.ck_DataForm.Size = new System.Drawing.Size(84, 16);
            this.ck_DataForm.TabIndex = 7;
            this.ck_DataForm.Text = "单日期模式";
            this.ck_DataForm.UseVisualStyleBackColor = true;
            this.ck_DataForm.CheckedChanged += new System.EventHandler(this.ck_DataForm_CheckedChanged);
            // 
            // lb_hold
            // 
            this.lb_hold.FormattingEnabled = true;
            this.lb_hold.ItemHeight = 12;
            this.lb_hold.Location = new System.Drawing.Point(12, 84);
            this.lb_hold.Name = "lb_hold";
            this.lb_hold.Size = new System.Drawing.Size(135, 268);
            this.lb_hold.TabIndex = 12;
            // 
            // lb_do
            // 
            this.lb_do.FormattingEnabled = true;
            this.lb_do.ItemHeight = 12;
            this.lb_do.Location = new System.Drawing.Point(202, 84);
            this.lb_do.Name = "lb_do";
            this.lb_do.Size = new System.Drawing.Size(135, 268);
            this.lb_do.TabIndex = 13;
            // 
            // bt_go
            // 
            this.bt_go.Location = new System.Drawing.Point(156, 125);
            this.bt_go.Name = "bt_go";
            this.bt_go.Size = new System.Drawing.Size(40, 23);
            this.bt_go.TabIndex = 14;
            this.bt_go.Text = ">";
            this.bt_go.UseVisualStyleBackColor = true;
            this.bt_go.Click += new System.EventHandler(this.bt_go_Click);
            // 
            // bt_goall
            // 
            this.bt_goall.Location = new System.Drawing.Point(156, 164);
            this.bt_goall.Name = "bt_goall";
            this.bt_goall.Size = new System.Drawing.Size(40, 23);
            this.bt_goall.TabIndex = 15;
            this.bt_goall.Text = ">>";
            this.bt_goall.UseVisualStyleBackColor = true;
            this.bt_goall.Click += new System.EventHandler(this.bt_goall_Click);
            // 
            // bt_backall
            // 
            this.bt_backall.Location = new System.Drawing.Point(156, 247);
            this.bt_backall.Name = "bt_backall";
            this.bt_backall.Size = new System.Drawing.Size(40, 23);
            this.bt_backall.TabIndex = 16;
            this.bt_backall.Text = "<<";
            this.bt_backall.UseVisualStyleBackColor = true;
            this.bt_backall.Click += new System.EventHandler(this.bt_backall_Click);
            // 
            // bt_back
            // 
            this.bt_back.Location = new System.Drawing.Point(156, 207);
            this.bt_back.Name = "bt_back";
            this.bt_back.Size = new System.Drawing.Size(40, 23);
            this.bt_back.TabIndex = 17;
            this.bt_back.Text = "<";
            this.bt_back.UseVisualStyleBackColor = true;
            this.bt_back.Click += new System.EventHandler(this.bt_back_Click);
            // 
            // fm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 437);
            this.Controls.Add(this.bt_back);
            this.Controls.Add(this.bt_backall);
            this.Controls.Add(this.bt_goall);
            this.Controls.Add(this.bt_go);
            this.Controls.Add(this.lb_do);
            this.Controls.Add(this.lb_hold);
            this.Controls.Add(this.bt_regular);
            this.Controls.Add(this.bt_do);
            this.Controls.Add(this.dt_EndData);
            this.Controls.Add(this.dt_StartData);
            this.Controls.Add(this.ck_DataForm);
            this.Name = "fm_main";
            this.Text = "页面信息抓取";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fm_main_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_regular;
        private System.Windows.Forms.Button bt_do;
        private System.Windows.Forms.DateTimePicker dt_EndData;
        private System.Windows.Forms.DateTimePicker dt_StartData;
        private System.Windows.Forms.CheckBox ck_DataForm;
        private System.Windows.Forms.ListBox lb_hold;
        private System.Windows.Forms.ListBox lb_do;
        private System.Windows.Forms.Button bt_go;
        private System.Windows.Forms.Button bt_goall;
        private System.Windows.Forms.Button bt_backall;
        private System.Windows.Forms.Button bt_back;

    }
}

