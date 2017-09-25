using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PracticeProgram
{
    public partial class DoctorExamination : UserControl
    {
        public DoctorExamination()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string uid = text_uid.Text;
            string pwd = text_pwd.Text;
            logStatus("用户名："+uid);
            logStatus("密码："+pwd);
        }
        private void logStatus(string msg)
        {
            text_status.AppendText(msg+"\r\n");
        }
    }
}
