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
using System.Security.Cryptography;
using System.Collections;

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

        /// <summary>
        /// 查询全部考试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (XSystem.Shell.Student == null)
            {
                MessageBox.Show("请先登录", "一键通过");
                logStatus("请先登录！");
                return;
            }
            if (XSystem.Shell.ExamInfos.NotPassNum == 0)
            {
                MessageBox.Show("已全部通过考试，无需刷题！", "一键通过");
                logStatus("已全部通过考试，无需刷题！");
                return;
            }

            string userid = XSystem.Shell.Student.id;
            passAllTrain(userid);


        }
        public void passAllTrain(string userid)
        {
            var trainResult = JSONHelper.JsonToDictionary(getAllTraining(userid));
            var data = trainResult["data"] as Dictionary<string, object>;
            var list = data["list"] as ArrayList;
            foreach (var item in list)
            {
                var train = item as Dictionary<string, object>;
                string trainID = train["id"].ToString();
                passAllExam(userid, trainID);
            }
        }
        public void passAllExam(string userid, string trainID)
        {
            string url = "http://api.yiboshi.com/api/study/student/listStudentProjCourseInfoAndStatus?userId" +
                "=userid" + userid + "&trainingId=" + trainID + "&courseState=&compulsory=&keyword=";//获取课程列表
            try
            {
                string result = XSystem.Shell.Http.SendGet(url);
                Dictionary<string, object> reDic = JSONHelper.JsonToDictionary(result);
                var data = reDic["data"] as Dictionary<string, object>;
                var list = data["list"] as ArrayList;
                foreach (object item in list)
                {
                    var pro = item as Dictionary<string, object>;
                    string totalHour = pro["totalHour"].ToString();
                    string proID = pro["id"].ToString();
                    string projectName = pro["projectName"].ToString();
                    var courseList = pro["courseList"] as ArrayList;
                    foreach (var course in courseList)
                    {
                        var couDic = course as Dictionary<string, object>;
                        string coureseID = couDic["id"].ToString();
                        string coureseName = couDic["name"].ToString();
                        string courseFieldID = couDic["courseFieldID"].ToString();

                        string trainingID = "363";

                        var courseState = couDic["ybsCourseState"] as Dictionary<string, object>;
                        string state = courseState["courseState"].ToString();
                        string practiseScore = courseState["practiseScore"].ToString();
                        if (!"100".Equals(practiseScore))
                        {
                            bool flag = submitScore(trainingID, proID, userid, coureseID);
                            if (flag)
                            {
                                logStatus(string.Format("【{0}】课程练习成功！成绩：【100】！", coureseName));
                            }
                            else
                            {
                                logStatus(string.Format("【{0}】课程练习失败！", coureseName));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logStatus(ex.Message);
            }
        }
        private bool submitScore(string trainingId, string projectId, string userId, string courseId, int score = 100, string versionId = "3.1")
        {
            string url = "http://api.yiboshi.com/api/WebApp/commitCoursePracticeScore";

            string result = XSystem.Shell.Http.SendGet(url, new Dictionary<string, string>() {
                { "trainingId",trainingId},
                { "projectId",projectId},
                { "userId",userId},
                { "courseId",courseId},
                { "score",score+""},
                { "versionId",versionId},
            });
            try
            {
                var reDic = JSONHelper.JsonToDictionary(result);
                if (reDic["result"].ToString().Equals("1"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string uid = text_uid.Text;
            string pwd = text_pwd.Text;

            logStatus("用户名：" + uid);
            logStatus("密码：" + pwd);

            if (Login(pwd, uid))
            {
                IsLogined = true;
                logStatus("登录成功！");
                text_pwd.Enabled = false;
                text_uid.Enabled = false;
                btn_login.Enabled = false;
                btn_logout.Enabled = true;
                logStatus("正在查询培训内容，大约需要一分钟...");
                SearchAllData(XSystem.Shell.Student.id);
                logStatus("查询所有培训内容完毕！");
            }
            else
            {
                logStatus("登录失败！");
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            XSystem.Shell.Student = null;
            text_pwd.Enabled = true;
            text_uid.Enabled = true;
            btn_login.Enabled = true;
            this.gv.DataSource = null;
            btn_logout.Enabled = false;
            logStatus("登出成功！");
        }

        private void SearchAllData(string userID)
        {
            XSystem.Shell.ExamInfos.NotPassNum = 0;
            XSystem.Shell.ExamInfos.HasPassNum = 0;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("课程名称"),
                new DataColumn("主讲人"),
                new DataColumn("课程状态"),
                new DataColumn("最高分数")
            });
            var trainResult = JSONHelper.JsonToDictionary(getAllTraining(userID));
            var data = trainResult["data"] as Dictionary<string, object>;
            var list = data["list"] as ArrayList;
            foreach (var item in list)
            {
                var train = item as Dictionary<string, object>;
                string trainID = train["id"].ToString();
                string trainName = train["name"].ToString();
                logStatus(string.Format("正在查询{0}培训下课程...", trainName));
                SearchData(userID, trainID, dt);
            }
            this.gv.DataSource = dt;
        }
        private void SearchData(string userid, string trainid, DataTable dt)
        {


            string url = "http://api.yiboshi.com/api/study/student/listStudentProjCourseInfoAndStatus?userId" +
               "=" + userid + "&trainingId=" + trainid + "&courseState=&compulsory=&keyword=";//获取课程列表
            try
            {
                string result = XSystem.Shell.Http.SendGet(url);
                Dictionary<string, object> reDic = JSONHelper.JsonToDictionary(result);
                var data = reDic["data"] as Dictionary<string, object>;
                var list = data["list"] as ArrayList;
                foreach (object item in list)
                {
                    var pro = item as Dictionary<string, object>;
                    string totalHour = pro["totalHour"].ToString();
                    string proID = pro["id"].ToString();
                    string projectName = pro["projectName"].ToString();
                    var courseList = pro["courseList"] as ArrayList;
                    foreach (var course in courseList)
                    {
                        var couDic = course as Dictionary<string, object>;
                        string coureseID = couDic["id"].ToString();
                        string coureseName = couDic["name"].ToString();
                        string courseFieldID = couDic["courseFieldID"].ToString();

                        var teacherInfo = couDic["ybsRbacUser"] as Dictionary<string, object>;
                        string teacherName = teacherInfo["nickName"].ToString();

                        string state = "";
                        string practiseScore = "未学习";
                        if (couDic.ContainsKey("ybsCourseState"))
                        {
                            var courseState = couDic["ybsCourseState"] as Dictionary<string, object>;
                             state = courseState["courseState"].ToString();
                            practiseScore = courseState["practiseScore"].ToString();
                            if (!"100".Equals(practiseScore))
                            {
                                XSystem.Shell.ExamInfos.NotPassNum++;
                            }
                            else
                            {
                                XSystem.Shell.ExamInfos.HasPassNum++;
                            }
                        }
                        dt.Rows.Add(coureseName, teacherName, state, practiseScore);





                    }
                }
            }
            catch (Exception ex)
            {
                logStatus(ex.Message);
            }
            logStatus(string.Format("当前一共{0}道题目，其中{1}道已通过，{2}道未通过",
                XSystem.Shell.ExamInfos.HasPassNum + XSystem.Shell.ExamInfos.NotPassNum,
                XSystem.Shell.ExamInfos.HasPassNum, XSystem.Shell.ExamInfos.NotPassNum));

        }

        private bool Login(string pwd, string uid)
        {
            string loginUrl = "http://api.yiboshi.com/api/study/student/login";
            pwd = EncriptHelper.MD5Encrypt32(pwd);
            Dictionary<string, string> paramList = new Dictionary<string, string>() {
                { "username",uid},
                { "password",pwd}
            };

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

        private void logStatus(string msg)
        {
            text_status.AppendText(msg + "\r\n");
        }

        public bool IsLogined { get; set; }




        private void btn_clear_Click(object sender, EventArgs e)
        {

            string url = "http://api.yiboshi.com/api/study/project/getProjDict?type=1";//获取国家级、省级等
            url = "http://api.yiboshi.com/api/category/listDisciplineCategoryAndChild";//获取科室
            url = "http://api.yiboshi.com/api/study/student/existAssignOrg";//不清楚
            url = "http://api.yiboshi.com/api/study/student/listStudentProjCourseInfoAndStatus?userId" +
                "=51424&trainingId=363&courseState=&compulsory=&keyword=";//获取课程列表


            text_status.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (XSystem.Shell.Student != null)
            {
                string result = getAllTraining(XSystem.Shell.Student.id);
                logStatus(result);
            }
            else
            {
                logStatus("请登录！");
            }
        }


    }

    public class StudentInfo
    {
        public string id { get; set; }
        public string companyName { get; set; }
        public string userName { get; set; }
        public string realName { get; set; }
        public string sex { get; set; }
        public string sertID { get; set; }
        public string orgID { get; set; }
        public string phone { get; set; }
        public string personType { get; set; }
        public string spid1 { get; set; }
        public string spid2 { get; set; }
        public string department { get; set; }
        public string nation { get; set; }
        public string oldid { get; set; }
    }
}
