using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PracticeProgram;
using System.IO;
using System.Collections;
using BaseCSharp.CodeCollection;
using System.Data;
using BaseCSharp;

namespace PracticeWeb.Controllers
{
    public class ExaminationController : Controller
    {
        // GET: Examination
        public ActionResult Index()
        {
            return View("Examination");
        }
        /// <summary>
        ///  GET: /Examination/AutoExam
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoExam()
        {
            return View("AutoExam");
        }
        public ActionResult TestExam()
        {
            string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
            IDatabase database = new SqlDatabase(connString);
            HttpHelper http = Session["http"] as HttpHelper;
            string uid = Request.Form["uid"];
            string url = "http://api.yiboshi.com/api/study/student/usercenter/validateUserCreditBindingTerm"
                    + "?trainingIds=363-0&userId=185313";
            string s1 = http.SendGet(url, new Dictionary<string, string>());

            url = "http://api.yiboshi.com/api/study/student/getServerNowTime";
            string s2 = http.SendGet(url, new Dictionary<string, string>());

            url = "http://api.yiboshi.com/api/study/student/validateTrainingExam?trainingStatus=363-0&userId=185313";
            string s3 = http.SendGet(url, new Dictionary<string, string>());

            url = "http://api.yiboshi.com/api/study/public/getBeginExamUrl?userId=185313&examId=2949";
            string s4 = http.SendGet(url, new Dictionary<string, string>());

            url = "http://api.yiboshi.com/api/study/student/usercenter/exam?userId=185313&page=1&pageSize=10&examState=&bindType=2&trainingId=&examName=&psort=0";
            string s5 = http.SendGet(url, new Dictionary<string, string>());

            url = "http://online.yiboshi.com/online/api/user/login/oauth2?turl=http%3A%2F%2Fonline.yiboshi.com%2Fonline%2Fbyks%2FexamNotice.html&eurl=http%3A%2F%2Fwww.yiboshi.com%2FMyExam&ts=1507815738147&from=usercenter&fp=2C6981DCF3CDE429F87A3EF9D19DD055&userId=185313&examId=2949";
            string s6 = http.SendGet(url, new Dictionary<string, string>());

            url = "http://online.yiboshi.com/online/api/user/login/oauth2?turl=http%3A%2F%2Fonline.yiboshi.com%2Fonline%2Fbyks%2FexamNotice.html&eurl=http%3A%2F%2Fwww.yiboshi.com%2FMyExam&ts=1507815738147&from=usercenter&fp=2C6981DCF3CDE429F87A3EF9D19DD055&userId=185313&examId=2949";

            url = s4;
            string s8 = http.SendGet(url, new Dictionary<string, string>());

            url = "http://online.yiboshi.com/online/api/exam/getExamInfo";
            string result = http.SendPost(url, new Dictionary<string, string>());

            url = "http://online.yiboshi.com/online/api/exam/getPaper";//获取考试信息
            string s9 = http.SendPost(url, new Dictionary<string, string>());

            var dic = JSONHelper.JsonToDictionary(s9);
            var examid = dic["examId"].ToString();
            var exampagerid = dic["examPaperId"].ToString();
            var qlist = dic["examQuestionVOList"] as ArrayList;
            StringBuilder asb = new StringBuilder();
            asb.Append("{");
            bool isStart = true;
            int index = 1;
            foreach (var item in qlist)
            {
                asb.Append(isStart ? "" : ",");
                isStart = false;
                var itemDic = item as Dictionary<string, object>;
                var name = itemDic["htmlContent"];
                DataTable dt = database.QueryTable("select * from t_answer where name ='" + name + "'");
                var answer = dt.Rows[0]["ans"].ToString();
                asb.Append("\"" + index + "\":\"" + answer + "\"");
                index++;
            }


            asb.Append("}");



            url = "http://online.yiboshi.com/online/api/student/submitPaper";
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("mouseoutNumber", "2");
            para.Add("mouseoutTypes", "blur,resize");
            para.Add("examId", examid);
            para.Add("examPaperId", exampagerid);
            para.Add("studentId", uid);
            para.Add("userAnswers", asb.ToString());

            para.Add("notsureIds", "");

            return Content(result);
        }

        /// <summary>
        ///  /Examination/SearchAllExam
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchAllExam()
        {
            string err = "{'result':'0'}";
            try
            {
                string userid = Request.Form["userid"];
                HttpHelper http = Session["http"] as HttpHelper;
                string url = "http://api.yiboshi.com/api/study/student/usercenter/exam?userId=" + userid + "&page=1&pageSize=10&examState=&bindType=2&trainingId=&examName=&psort=0";
                string result = http.SendGet(url);
                return Content(result);
            }
            catch (Exception ex)
            {
                return Content(err);
            }
        }

        public ActionResult PassExamById()
        {
            string err = "{'result':'0'}";
            try
            {
                string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
                IDatabase database = new SqlDatabase(connString);
                HttpHelper http = Session["http"] as HttpHelper;
                string examId = Request.Form["examid"];
                string trainingStatus = Request.Form["trainingStatus"];
                string userid = Request.Form["userid"];
                string trainId = Request.Form["trainid"];


                string url = "http://api.yiboshi.com/api/study/student/usercenter/validateUserCreditBindingTerm"
                   + "?trainingIds=" + trainId + "&userId=" + userid + "";
                string s1 = http.SendGet(url, new Dictionary<string, string>());

                url = "http://api.yiboshi.com/api/study/student/getServerNowTime";
                string s2 = http.SendGet(url, new Dictionary<string, string>());

                url = "http://api.yiboshi.com/api/study/student/validateTrainingExam?trainingStatus="
                    + trainingStatus
                    + "&userId=" + userid + "";
                string s3 = http.SendGet(url, new Dictionary<string, string>());

                url = "http://api.yiboshi.com/api/study/public/getBeginExamUrl?userId=" + userid + "&examId=" + examId + "";
                string s4 = http.SendGet(url, new Dictionary<string, string>());

                url = "http://api.yiboshi.com/api/study/student/usercenter/exam"
                    + "?userId=" + userid + "&page=1&pageSize=10&examState=&bindType=2&trainingId=&examName=&psort=0";
                string s5 = http.SendGet(url, new Dictionary<string, string>());

                url = "http://online.yiboshi.com/online/api/user/login/oauth2?"
                    + "turl=http%3A%2F%2Fonline.yiboshi.com%2Fonline%2Fbyks%2FexamNotice.html"
                    + "&eurl=http%3A%2F%2Fwww.yiboshi.com%2FMyExam&ts=1507815738147"
                    + "&from=usercenter&fp=2C6981DCF3CDE429F87A3EF9D19DD055"
                    + "&userId=" + userid + "&examId=" + examId;
                //string s6 = http.SendGet(url, new Dictionary<string, string>());

                url = s4;
                string s8 = http.SendGet(url, new Dictionary<string, string>());

                url = "http://online.yiboshi.com/online/api/exam/getExamInfo";
                string result = http.SendPost(url, new Dictionary<string, string>());

                url = "http://online.yiboshi.com/online/api/student/startExam";
                string s10 = http.SendPost(url, new Dictionary<string, string>());

                url = "http://online.yiboshi.com/online/api/exam/getPaper";//获取考试信息
                string s9 = http.SendPost(url, new Dictionary<string, string>());
                var dataInfo = JSONHelper.JsonToDictionary(s9);
                var data = dataInfo["data"] as Dictionary<string, object>;
                var qlist = data["examQuestionVOList"] as ArrayList;

                StringBuilder answerSb = new StringBuilder();
                answerSb.Append("{");
                bool isStart = true;
                int index = 1;
                Random r = new Random();
                    var ansList = "A,B,C,D,E".Split(',');
                foreach (var ques in qlist)
                {
                    answerSb.Append(isStart?"":",");
                    isStart = false;
                    var qDic = ques as Dictionary<string, object>;
                    var name = "默认题目";
                    int k = r.Next(ansList.Length);
                    var ans = ansList[k];
                    try
                    {
                        name = qDic["htmlContent"].ToString();
                        DataTable dt = database.QueryTable("select * from t_answer where name = '" + name + "'");
                        if (dt == null)
                        {
                        name = name.Substring(2,name.Length-2);
                        dt = database.QueryTable(
                            "select * from t_answer where name like '%"+name+"%'");
                        }
                        ans = dt.Rows[0]["ans"].ToString();
                    }
                    catch(Exception ex)
                    {

                    }
                    answerSb.Append("\""+index+"\":\""+ans+"\"");
                    index++;

                }
                answerSb.Append("}");
                string examPaperId = "";
                var meta = dataInfo["meta"] as Dictionary<string, object>;
                var user = meta["user"] as Dictionary<string, object>;
                examPaperId = user["currentExamPaperId"].ToString();

                url = "http://online.yiboshi.com/online/api/student/submitPaper";
                Dictionary<string, string> subDic = new Dictionary<string, string>()
                {
                    { "mouseoutNumber","2"},
                    { "mouseoutTypes","blur,resize"},
                    { "examId",examId},
                    { "examPaperId",examPaperId},
                    { "studentId",userid},
                    { "userAnswers",answerSb.ToString()},
                    { "notsureIds",""}
                };
                string s11 = "";
                s11= http.SendPost(url,subDic);
                return Content(s11);
            }
            catch(Exception ex)
            {
                return Content(err);
            }
        }

        public ActionResult GetAnsByQuestionList()
        {
            string err = "{'result':'0'}";
            try
            {
                string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
                IDatabase database = new SqlDatabase(connString);
                HttpHelper http = Session["http"] as HttpHelper;

                string nameList = Request.Form["nameList"];
                string[] nList = nameList.Split(',');
                StringBuilder result = new StringBuilder();
                bool isStart = true;
                result.Append("[");
                foreach (string name in nList)
                {
                    string ans = "数据库无答案";
                    try
                    {
                        DataTable dt = database.QueryTable("select * from t_answer where name = '" + name + "'");
                        if (dt == null || dt.Rows.Count < 1)
                        {
                            throw new Exception();
                        }
                        var row = dt.Rows[0];
                        ans = row["ans"].ToString();
                    }
                    catch {

                    }
                    result.Append(isStart?"{":",{");
                    isStart = false;
                    result.Append("\"name\":\""+name+"\",");
                    result.Append("\"ans\":\""+ans+"\"");
                    result.Append("}");

                }
                result.Append("]");

                return Content(result.ToString());
            }
            catch (Exception ex)
            {
                return Content(err);
            }
        }
    }
}