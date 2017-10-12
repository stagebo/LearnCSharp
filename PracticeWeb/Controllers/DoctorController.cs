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
using BaseCSharp.CodeCollection.SqlServer;

namespace PracticeWeb.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View("DoctorExam");
        }

        public ActionResult Login()
        {
            string err = "{\"result\":\"0\"}";
            string uid = Request.Form["uid"];
            string pwd = Request.Form["pwd"];
            if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(pwd))
            {
                return Content(err);
            }
            string loginUrl = "http://api.yiboshi.com/api/study/student/login";
            HttpHelper http = new HttpHelper();
            pwd = EncriptHelper.MD5Encrypt32(pwd);
            //pwd = "a008aa83f9f52700237f9ecb93159a5b";
            //      "a08aa83f9f5270237f9ecb93159a5b"
            Dictionary<string, string> paramList = new Dictionary<string, string>() {
                { "username",uid},
                { "password",pwd}
                //"54ea5aec6ebb71a07ece56aae5c7deaa" 391122
                //e10adc3949ba59abbe56e057f20f883e 391122
                // 54ea5aec6ebb71a07ece56aae5c7deaa
            };
            try
            {
                string result = http.SendPost(loginUrl, paramList);
                Session["http"] = http;
                return Content(result);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// /Doctor/SearchAllTraining
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchAllTraining()
        {
            string err = "{\"result\":\"0\"}";
            string userid = Request.Form["userid"];
            string url = "http://api.yiboshi.com/api/study/student/listStudentTraining";
            Dictionary<string, string> paraList = new Dictionary<string, string>()
            {
                {"userId",userid },
                {"excludeExpire","true" },//是否排除过期项目
                {"trainingWay","1" }
            };
            try
            {
                var http = Session["http"] as HttpHelper;
                if (http == null)
                {
                    return Content(err);
                }
                string result = http.SendGet(url, paraList);
                return Content(result);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// /Doctor/GetAllCourse
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCourse()
        {
            string err = "{\"result\":\"0\"}";
            string userid = Request.Form["userid"];
            string tid = Request.Form["tid"];
            try
            {
                var http = Session["http"] as HttpHelper;
                if (http == null)
                {
                    return Content(err);
                }
                string url = "http://api.yiboshi.com/api/study/student/listStudentProjCourseInfoAndStatus?userId=" +
                 userid + "&trainingId=" + tid + "&courseState=&compulsory=&keyword=";//获取课程列表
                string result = http.SendGet(url);
                return Content(result);

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult PassExam()
        {
            string err = "{\"result\":\"0\"}";

            string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
            IDatabase database = new SqlDatabase(connString);
            


            int count;
            if (!int.TryParse(Request.Form["count"], out count))
            {
                return Content(err);
            }
            string submitUtl = "http://api.yiboshi.com/api/WebApp/commitCoursePracticeScore";
            HttpHelper http = Session["http"] as HttpHelper;
            List<Dictionary<string, string>> scoreList = new List<Dictionary<string, string>>();
            int success = 0, fail = 0;
            string tid = Request.Form["tid"];
            string uid = Request.Form["uid"];

            StringBuilder ansSb = new StringBuilder();
            string target = @"C:\Users\wyb\Desktop\秋秋\"+Guid.NewGuid()+".txt";
            
            for (int i = 0; i < count; i++)
            {
                string pid = Request.Form["pid" + i];
                string cid = Request.Form["cid" + i];
                string courseFieldId = Request.Form["courseFieldId"+i];
                string result = http.SendGet(submitUtl, new Dictionary<string, string>() {
                            { "trainingId",tid},
                            { "projectId",pid},
                            { "userId",uid},
                            { "courseId",cid},
                            { "score",100+""},
                            { "versionId","3.1"},
                        });
                //提取答案
                string ans = http.SendGet("http://examapi.yiboshi.com/course/practices/"+
                    courseFieldId + "?callback=P");
                int ss = ans.IndexOf('{');
                try
                {
                    string temp = ans.Substring(ss, ans.Length - 2-ss);
                    var ansDic = JSONHelper.JsonToDictionary(temp);
                    var dataList = ansDic["data"] as ArrayList;
                    var dl = ansDic["data"] as ArrayList;
                    foreach (object item in dataList)
                    {
                        var itemDic = item as Dictionary<string, object>;
                        var ana = itemDic["ana"];
                        var answer = itemDic["ans"];
                        var qid = itemDic["qid"];
                        var stem = itemDic["stem"];
                        var optsList = itemDic["opts"] as ArrayList;
                        ansSb.Append(stem+":");
                        ansSb.Append(answer + "----");
                        foreach (var opt in optsList)
                        {
                            var optDic = opt as Dictionary<string, object>;
                            var selects = optDic["opt"];
                            var isans = optDic["isAns"];
                            var ctnt = optDic["ctnt"];
                            var optid = optDic["id"];
                            ansSb.Append(selects+":"+ctnt+",");
                        }
                        ansSb.Append("\r\n");
                        //
                        int r = database.Execute(
                            "INSERT INTO[t_answer]([name],[ans])VALUES('"+ stem + "','"+ answer + "')");
                        var t=database.QueryTable("select * from t_user");
                    }
                }
                catch { }


               
                if (result.Contains("1"))
                {
                    success++;
                }
                else
                {
                    fail++;
                }
            }
            try
            {
                FileStream fs = new FileStream(target, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(ansSb.ToString());
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch { }


            StringBuilder sb = new StringBuilder();
            sb.Append("{\"result\":\"1\",\"success\":\"" + success + "\",\"fail\":\"" + fail + "\"}");
            return Content(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ExtAnswer()
        {
            return null;
        }
    }
}