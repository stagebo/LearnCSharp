namespace PracticeProgram
{
    partial class DoctorExamination
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btn_clear;
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_logout = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.text_pwd = new System.Windows.Forms.TextBox();
            this.text_uid = new System.Windows.Forms.TextBox();
            this.gv = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.text_status = new System.Windows.Forms.TextBox();
            btn_clear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_clear
            // 
            btn_clear.Location = new System.Drawing.Point(610, 20);
            btn_clear.Name = "btn_clear";
            btn_clear.Size = new System.Drawing.Size(75, 23);
            btn_clear.TabIndex = 1;
            btn_clear.Text = "清空";
            btn_clear.UseVisualStyleBackColor = true;
            btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_logout);
            this.groupBox1.Controls.Add(this.btn_login);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.text_pwd);
            this.groupBox1.Controls.Add(this.text_uid);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 43);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // btn_logout
            // 
            this.btn_logout.Enabled = false;
            this.btn_logout.Location = new System.Drawing.Point(558, 14);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(75, 23);
            this.btn_logout.TabIndex = 5;
            this.btn_logout.Text = "登出";
            this.btn_logout.UseVisualStyleBackColor = true;
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(477, 14);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 23);
            this.btn_login.TabIndex = 4;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密  码";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // text_pwd
            // 
            this.text_pwd.Location = new System.Drawing.Point(305, 14);
            this.text_pwd.Name = "text_pwd";
            this.text_pwd.Size = new System.Drawing.Size(166, 21);
            this.text_pwd.TabIndex = 2;
            this.text_pwd.Text = "391122";
            // 
            // text_uid
            // 
            this.text_uid.Location = new System.Drawing.Point(72, 14);
            this.text_uid.Name = "text_uid";
            this.text_uid.Size = new System.Drawing.Size(166, 21);
            this.text_uid.TabIndex = 1;
            this.text_uid.Text = "522225197607010066";
            // 
            // gv
            // 
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Location = new System.Drawing.Point(6, 20);
            this.gv.Name = "gv";
            this.gv.RowTemplate.Height = 23;
            this.gv.Size = new System.Drawing.Size(595, 238);
            this.gv.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.gv);
            this.groupBox2.Location = new System.Drawing.Point(3, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(708, 264);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "考试信息";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(607, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "一键通过";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(btn_clear);
            this.groupBox3.Controls.Add(this.text_status);
            this.groupBox3.Location = new System.Drawing.Point(0, 322);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(711, 219);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "状态栏";
            // 
            // text_status
            // 
            this.text_status.Location = new System.Drawing.Point(6, 20);
            this.text_status.Multiline = true;
            this.text_status.Name = "text_status";
            this.text_status.ReadOnly = true;
            this.text_status.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_status.Size = new System.Drawing.Size(598, 172);
            this.text_status.TabIndex = 0;
            // 
            // DoctorExamination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DoctorExamination";
            this.Size = new System.Drawing.Size(740, 567);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_pwd;
        private System.Windows.Forms.TextBox text_uid;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox text_status;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_logout;
    }
}
