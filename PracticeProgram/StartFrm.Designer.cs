namespace PracticeProgram
{
    partial class StartFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.welcome = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txb_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_pass = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_project = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_train = new System.Windows.Forms.ComboBox();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.course_grid = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.course_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.welcome);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txb_password);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txb_username);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_login);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1139, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // welcome
            // 
            this.welcome.AutoSize = true;
            this.welcome.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.welcome.Location = new System.Drawing.Point(727, 23);
            this.welcome.Name = "welcome";
            this.welcome.Size = new System.Drawing.Size(0, 27);
            this.welcome.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(766, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 27);
            this.label5.TabIndex = 5;
            // 
            // txb_password
            // 
            this.txb_password.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_password.Location = new System.Drawing.Point(474, 20);
            this.txb_password.Name = "txb_password";
            this.txb_password.Size = new System.Drawing.Size(100, 38);
            this.txb_password.TabIndex = 4;
            this.txb_password.Text = "123456";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(378, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txb_username
            // 
            this.txb_username.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_username.Location = new System.Drawing.Point(146, 20);
            this.txb_username.Name = "txb_username";
            this.txb_username.Size = new System.Drawing.Size(202, 38);
            this.txb_username.TabIndex = 2;
            this.txb_username.Text = "15085927614";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名";
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_login.Location = new System.Drawing.Point(604, 13);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(82, 52);
            this.btn_login.TabIndex = 0;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_pass);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmb_project);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmb_train);
            this.groupBox2.Location = new System.Drawing.Point(24, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 280);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单";
            // 
            // btn_pass
            // 
            this.btn_pass.Location = new System.Drawing.Point(392, 208);
            this.btn_pass.Name = "btn_pass";
            this.btn_pass.Size = new System.Drawing.Size(82, 52);
            this.btn_pass.TabIndex = 5;
            this.btn_pass.Text = "一键通过";
            this.btn_pass.UseVisualStyleBackColor = true;
            this.btn_pass.Click += new System.EventHandler(this.btn_pass_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "选择项目";
            // 
            // cmb_project
            // 
            this.cmb_project.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_project.FormattingEnabled = true;
            this.cmb_project.Location = new System.Drawing.Point(34, 146);
            this.cmb_project.Name = "cmb_project";
            this.cmb_project.Size = new System.Drawing.Size(440, 35);
            this.cmb_project.TabIndex = 6;
            this.cmb_project.SelectedIndexChanged += new System.EventHandler(this.cmb_project_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "培训培训";
            // 
            // cmb_train
            // 
            this.cmb_train.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_train.FormattingEnabled = true;
            this.cmb_train.Location = new System.Drawing.Point(34, 66);
            this.cmb_train.Name = "cmb_train";
            this.cmb_train.Size = new System.Drawing.Size(440, 35);
            this.cmb_train.TabIndex = 0;
            this.cmb_train.SelectedIndexChanged += new System.EventHandler(this.cmb_train_SelectedIndexChanged);
            // 
            // rtb_log
            // 
            this.rtb_log.Enabled = false;
            this.rtb_log.Location = new System.Drawing.Point(24, 421);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(500, 212);
            this.rtb_log.TabIndex = 2;
            this.rtb_log.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.course_grid);
            this.groupBox3.Location = new System.Drawing.Point(567, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(584, 534);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "培训情况";
            // 
            // course_grid
            // 
            this.course_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.course_grid.Location = new System.Drawing.Point(6, 66);
            this.course_grid.Name = "course_grid";
            this.course_grid.RowTemplate.Height = 23;
            this.course_grid.Size = new System.Drawing.Size(572, 462);
            this.course_grid.TabIndex = 0;
            // 
            // StartFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 699);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "StartFrm";
            this.Text = "医博士Passer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.course_grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txb_password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_train;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_project;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView course_grid;
        private System.Windows.Forms.Button btn_pass;
        private System.Windows.Forms.Label welcome;
        private System.Windows.Forms.Label label5;
    }
}

