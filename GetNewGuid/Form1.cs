using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetNewGuid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Guid.NewGuid().ToString().ToUpper();
            Clipboard.SetDataObject(textBox1.Text.ToString());
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f = new Form1();
            f.Show();
        }
    }
}
