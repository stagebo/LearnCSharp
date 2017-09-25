using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeProgram
{
    public partial class StartFrm : Form
    {
        public StartFrm()
        {
            XSystem.Shell = this;
            this.Database = new SqliteDatabase(dataFileName);
            this.Http = new HttpHelper();
            this.Student = null;
            this.ExamInfos = new ExamInfo();
            this.ExamInfos.NotPassNum = 0;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoctorExamination doc = new DoctorExamination();
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(doc);
        }

        public static string dataFileName = AppDomain.CurrentDomain.BaseDirectory + "/../../DatabaseFile/project.Data";
        public SqliteDatabase Database { get; set; }
        public HttpHelper Http { get; set; }
        public StudentInfo Student {get;set;}
        public ExamInfo ExamInfos { get; set; }
    }
    public class ExamInfo
    {
        public int NotPassNum { get; set; }
        public int HasPassNum { get; set; }
    }
}
