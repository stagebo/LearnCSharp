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
        private void btn_signup_Click(object sender, EventArgs e)
        {
            string url = "https://www.wish.com/api/email-signup";
            Dictionary<string, string> paramList = new Dictionary<string, string>() {
                {"first_name", "aaa" },
                {"last_name", "bbb" },
                { "email", "165201521@qq.com"},
                {"password", "123456" },
                { "_buckets",""},
                { "_experiments",""},
                { "X-XSRFToken","2|20db4224|0b4287ed122eefcefacb8fa9befc26b2|1506390801"},
                { "_xsrf","false"}
            };
            url = "http://api.yiboshi.com/api/study/student/authJWT";
            //   Accept: application / json, text / plain, */*
            //Accept-Encoding:gzip, deflate
            //Accept-Language:zh-CN,zh;q=0.8,en;q=0.6
            //Authorization:Bearer eyJhbGciOiJIUzI1NiIsImp3dFR5cGUiOiJTVFVEWSIsInR5cCI6IkpXVCJ9.eyJ1aWQiOjg0MjAxLCJzdWIiOiI1MjAxMDMxOTcwMTAwODUyMlgiLCJhdWQiOiJnZW5lcmFsIiwiaXNzIjoiWUhCSiIsImV4cCI6MTUwNzAyNjY5MSwiaWF0IjoxNTA2NDIxODg4fQ.ITLUEnzXA0DTsu7AN6rekKv_ZkbthP3L5ngjezl8RzU
            //Connection:keep-alive
            //Content-Length:0
            //Content-Type:application/x-www-form-urlencoded
            //Cookie:_ga=GA1.2.1073343014.1506421874; _gid=GA1.2.1967641350.1506421874
            //Host:api.yiboshi.com
            //Origin:http://www.yiboshi.com
            //Referer:http://www.yiboshi.com/videoPlayer?uId=84201&tId=362&pId=2338&cId=2874
            //User-Agent:Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36
            //Get  获取视频评论
            url = "http://api.yiboshi.com/api/comment/list?typeId=1_2874&status=1&pageNum=1&beginDate=&endDate=";
            string result = this.Http.SendPost(url, paramList);
            //Get 获取视频列表，教师信息
            url = "http://api.yiboshi.com/api/study/courseware/getCourseWareByCourse?userId=84201&trainingId=362&projId=2338&courseId=2874";
            result = this.Http.SendGet(url);
            url = "http://api.yiboshi.com/api/study/course/syncCourseStatus";
            result = Http.SendPost(url,paramList);
            result = moniVideo();
            this.textBox1.Text = result;
        }
        private string moniVideo()
        {
            string url = "http://www.yiboshi.com/videoPlayer?uId=84201&tId=366&pId=2364&cId=3291";
            Http.SendGet(url);
            url = "https://www.vipkdy.com/1.gif?u=82088bb4612f2fc428672b54fb194a61&c=_360-ty-vipkdy&v=1.0.0.9&v1=5.1.0.33&v2=5.1.0.33&s=3&t=8&sd=2&bl=2%2C&de=120%2C163%2C&mv=0&a=%E4%B8%AD%E5%9B%BD-%E5%8D%8E%E5%8C%97-%E5%A4%A9%E6%B4%A5%E5%B8%82-%E5%A4%A9%E6%B4%A5%E5%B8%82&tms=1506423392292";
            Http.SendGet(url);
            url = "http://m2151.looyu.com/monitor/s?c=e&i=53832&v=bf01340f0034b7b5ff1c75b2ee70fcb17c&p=17952257078&x=1506423437562";
            Http.SendGet(url);
            //url = "http://source.yiboshi.com/CourseWareMp4/f5403fc648b6bfdb3aec5b9fb424b1c1_sd.mp4";
            Http.SendGet(url);
            url = "http://api.yiboshi.com/api/study/course/syncCourseStatus";
            return Http.SendPost(url,new Dictionary<string, string>());

        }
        public static string dataFileName = AppDomain.CurrentDomain.BaseDirectory + "/../../DatabaseFile/project.Data";
        public SqliteDatabase Database { get; set; }
        public HttpHelper Http { get; set; }
        public StudentInfo Student { get; set; }
        public ExamInfo ExamInfos { get; set; }

    }
    public class ExamInfo
    {
        public int NotPassNum { get; set; }
        public int HasPassNum { get; set; }
    }
}
