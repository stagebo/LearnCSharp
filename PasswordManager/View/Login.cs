using PasswordManager.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager.View
{
    public partial class view_login : Form
    {
        public view_login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string uid = t_user.Text;
            string pwd = t_password.Text;
            if (LoginControl.Login(uid, pwd))
            {
                this.Visible = false;
                
                new view_main().ShowDialog();
               // Application.ExitThread();   
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
            t_password.PasswordChar = '*';
        }
    }
}
