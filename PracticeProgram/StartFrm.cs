using BaseCSharp;
using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
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

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoctorExamination doc = new DoctorExamination();
            //this.panel1.Controls.Clear();
            //this.panel1.Controls.Add(doc);
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
            result = Http.SendPost(url, paramList);
            result = moniVideo();
            this.txb_username.Text = result;
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
            return Http.SendPost(url, new Dictionary<string, string>());

        }
        public static string dataFileName = AppDomain.CurrentDomain.BaseDirectory + "/../../DatabaseFile/project.Data";
        public SqliteDatabase Database { get; set; }
        public HttpHelper Http { get; set; }
        public StudentInfo Student { get; set; }
        public ExamInfo ExamInfos { get; set; }

        public bool IsLobin = false;

        public ArrayList trainList = null;
        public ArrayList CourseList = null;

        private void button1_Click_1(object sender, EventArgs e)
        {
            string uid = txb_username.Text;
            string pwd = txb_password.Text;

            logStatus("用户名：" + uid);

            if (Login(pwd, uid))
            {
                IsLobin = true;
                txb_username.Enabled = false;
                txb_password.Enabled = false;
                btn_login.Enabled = false;
                btn_login.Enabled = false;
                logStatus("正在查询培训内容...");
                SearchAllData(XSystem.Shell.Student.id);
                logStatus("查询所有培训内容完毕！");
                welcome.Text = "欢迎您：" + Student.realName;
            }
            else
            {
                logStatus("登录失败！");
            }
        }
        public void logStatus(string msg)
        {
            if (rtb_log == null || rtb_log.IsDisposed)
            {
                return;
            }
            rtb_log.Focus();
            rtb_log.AppendText(msg + "\n");
        }

        private bool Login(string pwd, string uid)
        {
            string loginUrl = "http://api.yiboshi.com/api/study/student/login";
            pwd = EncriptHelper.MD5Encrypt32(pwd);
            //pwd = "a008aa83f9f52700237f9ecb93159a5b";
            Dictionary<string, string> paramList = new Dictionary<string, string>() {
                { "username",uid},
                { "password",pwd}
            };
            //522121199002200068
            //a008aa83f9f52700237f9ecb93159a5b
            //a08aa83f9f5270237f9ecb93159a5b
            string result = XSystem.Shell.Http.SendPost(loginUrl, paramList);
            Dictionary<string, object> reDic = JSONHelper.JsonToDictionary(result);
            if (reDic.ContainsKey("status") && int.Parse(reDic["status"].ToString()) == 200)
            {
                #region 提取个人信息
                try
                {
                    StudentInfo stdInfo = new StudentInfo();
                    var data = reDic["data"] as Dictionary<string, object>;
                    var userInfo = data["studentInfo"] as Dictionary<string, object>;
                    stdInfo.id = userInfo["id"].ToString();
                    stdInfo.companyName = (userInfo["ybsCompCompany"] as Dictionary<string, object>)["companyName"].ToString();
                    stdInfo.userName = userInfo["userName"].ToString();
                    stdInfo.realName = userInfo["realName"].ToString(); ;
                    stdInfo.sex = userInfo["sex"].ToString();
                    stdInfo.sertID = userInfo["sertId"].ToString();
                    stdInfo.orgID = userInfo["orgId"].ToString();
                    stdInfo.phone = userInfo["mobile"].ToString();
                    stdInfo.personType = userInfo["personType"].ToString();
                    stdInfo.spid1 = userInfo["specialtyId1"].ToString();
                    stdInfo.spid2 = userInfo["specialtyId2"].ToString();
                    stdInfo.department = userInfo["id"].ToString();
                    stdInfo.nation = userInfo["nation"].ToString();
                    stdInfo.oldid = userInfo["oldid"].ToString();
                    XSystem.Shell.Student = stdInfo;
                }
                catch (Exception ex)
                {

                }
                #endregion
                return true;
            }
            return false;
        }



        private void SearchAllData(string userID)
        {
            //XSystem.Shell.ExamInfos.NotPassNum = 0;
            //XSystem.Shell.ExamInfos.HasPassNum = 0;
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[] {
            //    new DataColumn("课程名称"),
            //    new DataColumn("主讲人"),
            //    new DataColumn("课程状态"),
            //    new DataColumn("最高分数")
            //});
            if (trainList == null)
            {
                var trainResult = JSONHelper.JsonToDictionary(getAllTraining(userID));
                var data = trainResult["data"] as Dictionary<string, object>;
                var list = data["list"] as ArrayList;
                trainList = list;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("id");
            foreach (var item in trainList)
            {
                var item_d = item as Dictionary<String, Object>;
                var tid = item_d["id"].ToString();
                var tname = item_d["name"].ToString();
                DataRow row = dt.NewRow();
                row["name"] = tname;
                row["id"] = tid;
                dt.Rows.Add(row);
            }
            cmb_train.DataSource = dt;
            cmb_train.DisplayMember = "name";
            cmb_train.ValueMember = "id";
            //cmb_train.SelectedIndex = 0;
        }


        private string getAllTraining(string userid)
        {
            string url = "http://api.yiboshi.com/api/study/student/listStudentTraining";
            //userId =84201&excludeExpire=true&trainingWay=1";
            Dictionary<string, string> paraList = new Dictionary<string, string>()
            {
                {"userId",userid },
                {"excludeExpire","true" },//是否排除过期项目
                {"trainingWay","2" }
            };
            string result = XSystem.Shell.Http.SendGet(url, paraList);
            return result;
        }

        private string getAllProject(string userid, string trainid)
        {
            string url = string.Format(
                "http://api.yiboshi.com/api/study/student/listStudentProjCourseInfoAndStatus?userId={0}&trainingId={1}&courseState=&compulsory=&keyword=",
                userid, trainid);//获取课程项目

            string result = XSystem.Shell.Http.SendGet(url);
            return result;
        }

        private string getAllCourse(string userid, string trainid)
        {
            string url = string.Format(
                "http://api.yiboshi.com/api/study/student/listStudentProjCourseInfoAndStatus?userId={0}&trainingId={1}&courseState=&compulsory=&keyword=",
                userid, trainid);//获取课程项目

            string result = XSystem.Shell.Http.SendGet(url);
            return result;
        }

        private void cmb_train_SelectedIndexChanged(object sender, EventArgs e)
        {
            String userid = XSystem.Shell.Student.id;
            string trainid = cmb_train.SelectedValue.ToString();
            DataRowView x = (DataRowView)cmb_train.SelectedItem;
            object[] m = x.Row.ItemArray;
            trainid = m[1].ToString();

            if (CourseList == null)
            {
                var trainResult = JSONHelper.JsonToDictionary(getAllProject(userid, trainid));
                var data = trainResult["data"] as Dictionary<string, object>;
                var list = data["list"] as ArrayList;
                CourseList = list;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("id");
            foreach (var item in CourseList)
            {
                var item_d = item as Dictionary<String, Object>;
                var tid = item_d["id"].ToString();
                var tname = item_d["projectName"].ToString();
                DataRow row = dt.NewRow();
                row["name"] = tname;
                row["id"] = tid;
                dt.Rows.Add(row);
            }
            cmb_project.DataSource = dt;
            cmb_project.DisplayMember = "name";
            cmb_project.ValueMember = "id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_project_SelectedIndexChanged(object sender, EventArgs e)
        {
            String userid = XSystem.Shell.Student.id;
            DataRowView x = (DataRowView)cmb_train.SelectedItem;
            string trainid = x.Row.ItemArray[1].ToString();

            DataRowView pl = (DataRowView)cmb_project.SelectedItem;
            string projectid = pl.Row.ItemArray[1].ToString();

            ArrayList cl = new ArrayList();
            foreach (var item in CourseList)
            {
                var item_d = item as Dictionary<String, Object>;
                var tid = item_d["id"].ToString();
                if (tid.Equals(projectid))
                {
                    cl = item_d["courseList"] as ArrayList;
                    break;
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("名称");
            dt.Columns.Add("教师");
            dt.Columns.Add("分数");
            foreach (var item in cl)
            {
                var item_d = item as Dictionary<String, Object>;
                var ybsCourseState = item_d["ybsCourseState"] as Dictionary<String, Object>;
                var ybsRbacUser = item_d["ybsRbacUser"] as Dictionary<string, object>;


                var cid = item_d["id"].ToString();
                var cname = item_d["name"].ToString();
                var tname = ybsRbacUser["nickName"].ToString();
                var score = ybsCourseState["practiseScore"].ToString();
                dt.Rows.Add(new Object[] { cid, cname, tname, score });

            }
            course_grid.DataSource = dt;



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public bool isBusy = false;

        private void btn_pass_Click(object sender, EventArgs e)
        {
            if (XSystem.Shell.Student == null) {
                logStatus("请先登录");
                MessageBox.Show("请先登录");
                return;
            }

            if (isBusy) {
                MessageBox.Show("当前任务正在进行，请等待");
                logStatus("当前任务正在进行，请等待");
                return;
            }
            isBusy = true;
            Thread t = new Thread(passAll);
            t.Start();

        }
        void passAll() {

            string userid = XSystem.Shell.Student.id;

            var trainResult = JSONHelper.JsonToDictionary(getAllTraining(userid));
            var data = trainResult["data"] as Dictionary<string, object>;
            var trainList = data["list"] as ArrayList;

            foreach (var train_i in trainList) {

                var train = train_i as Dictionary<string, object>;
                var tid = train["id"].ToString();
                var tname = train["name"].ToString();
                var proResult = JSONHelper.JsonToDictionary(getAllProject(userid, tid));
                var pdata = proResult["data"] as Dictionary<string, object>;
                var pList = pdata["list"] as ArrayList;

                logStatus(string.Format("找到培训{0}",tname));

                foreach (var project_i in pList) {
                    var project = project_i as Dictionary<string, object>;
                    var pid = project["id"].ToString();
                    var pname = project["projectName"].ToString();
                    var cList = project["courseList"] as ArrayList;
                    logStatus(string.Format("找到项目{0}", pname));
                    foreach (var course_i in cList) {
                        var course = course_i as Dictionary<string, object>;
                        var cid = course["id"].ToString();
                        var cname = course["name"].ToString();
                        var cfid = course["courseFieldID"].ToString();
                        var state_d = course["ybsCourseState"] as Dictionary<string, object>;
                        var score = state_d["practiseScore"].ToString();
                        logStatus(string.Format("找到课程{0}",cname));
                        if (!"100".Equals(score))
                        {
                            passExam(userid,tid,pid,cid,cfid);
                        }
                        logStatus(string.Format("课程{0}已通过", cname));

                    }
                }

            }
            isBusy = false;





        }
        void passExam(string userid, string tid, string pid, string cid, string cfid) {
            string url = string.Format("http://api.yiboshi.com/api/WebApp/commitCoursePracticeScore?trainingId={0}&projectId={1}&userId={2}&courseId={3}&score=100&versionId=3.1"
                ,tid,pid,userid,cid);


            string result = XSystem.Shell.Http.SendGet(url);
            logStatus(result);
        }
    }
    public class ExamInfo
    {
        public int NotPassNum { get; set; }
        public int HasPassNum { get; set; }
    }
}
