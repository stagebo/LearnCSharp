namespace ImageDeal
{
    partial class ImageDevide
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.选择图片 = new System.Windows.Forms.Label();
            this.txb_file_name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(46, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(422, 377);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // 选择图片
            // 
            this.选择图片.AutoSize = true;
            this.选择图片.Location = new System.Drawing.Point(44, 21);
            this.选择图片.Name = "选择图片";
            this.选择图片.Size = new System.Drawing.Size(53, 12);
            this.选择图片.TabIndex = 1;
            this.选择图片.Text = "选择图片";
            // 
            // txb_file_name
            // 
            this.txb_file_name.Location = new System.Drawing.Point(118, 21);
            this.txb_file_name.Name = "txb_file_name";
            this.txb_file_name.Size = new System.Drawing.Size(350, 21);
            this.txb_file_name.TabIndex = 2;
            this.txb_file_name.Click += new System.EventHandler(this.txb_file_name_Clicked);
            // 
            // ImageDevide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 577);
            this.Controls.Add(this.txb_file_name);
            this.Controls.Add(this.选择图片);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ImageDevide";
            this.Text = "ImageDevide";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label 选择图片;
        private System.Windows.Forms.TextBox txb_file_name;
    }
}