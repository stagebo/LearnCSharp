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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static System.Net.Mime.MediaTypeNames;

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
            string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
            IDatabase database = /*new SqlDatabase(connString)*/CommonController.database;;


            string err = "{\"result\":\"0\"}";
            string uid = Request.Form["uid"];
            string pwd = Request.Form["pwd"];

            DataTable dts = database.QueryTable("select * from t_ybsUser where uid = '" + uid + "'");
            try {
                pwd = dts.Rows[0]["pwd"].ToString();
            } catch { }

            string password = pwd;
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
                //登陆成功，用户名入库
                if (result.Contains("studentInfo"))
                {
                    string sql = "select * from t_ybsUser where uid = '{0}'";
                    DataTable dt = database.QueryTable(string.Format(sql, uid));
                    if (dt.Rows.Count > 0)
                    {
                        sql = "update t_ybsUser set pwd = '{0}' where uid = '{1}' ";
                        int reInt = database.Execute(string.Format(sql, password, uid));
                    }
                    else
                    {
                        sql = "insert into t_ybsUser (uid,pwd) values('{0}','{1}')";
                        int reInt = database.Execute(string.Format(sql, uid, password));
                    }
                }
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

            try
            {
                string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
                IDatabase database = /*new SqlDatabase(connString)*/CommonController.database;;



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
                string target = @"C:\Users\wyb\Desktop\秋秋\" + Guid.NewGuid() + ".txt";

                for (int i = 0; i < count; i++)
                {
                    string pid = Request.Form["pid" + i];
                    string cid = Request.Form["cid" + i];
                    string courseFieldId = Request.Form["courseFieldId" + i];
                    string result = http.SendGet(submitUtl, new Dictionary<string, string>() {
                            { "trainingId",tid},
                            { "projectId",pid},
                            { "userId",uid},
                            { "courseId",cid},
                            { "score",100+""},
                            { "versionId","3.1"},
                        });
                    //提取答案
                    string ans = http.SendGet("http://examapi.yiboshi.com/course/practices/" +
                        courseFieldId + "?callback=P");
                    int ss = ans.IndexOf('{');
                    try
                    {
                        string temp = ans.Substring(ss, ans.Length - 2 - ss);
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
                            ansSb.Append(stem + ":");
                            ansSb.Append(answer + "----");
                            string[] ansNameList = new string[5];
                            int ansIndex = 0;
                            foreach (var opt in optsList)
                            {
                                var optDic = opt as Dictionary<string, object>;
                                var selects = optDic["opt"];
                                var isans = optDic["isAns"];
                                var ctnt = optDic["ctnt"];
                                var optid = optDic["id"];

                                ansSb.Append(selects + ":" + ctnt + ",");
                                ansNameList[ansIndex++] = ctnt.ToString();
                            }
                            string sql = @"
                            delete from t_question where
                            name = '{0}';
                            insert into t_question 
                            ([name],[ans],[A],[B],[C],[D],[E],[qid],[ana]) values
                            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');";
                            int reInt = database.Execute(string.Format(sql,
                                stem, answer, ansNameList[0], ansNameList[1],
                                ansNameList[2], ansNameList[3], ansNameList[4],
                                qid, ana));
                            ansSb.Append("\r\n");
                            //
                            DataTable dt = database.QueryTable
                                ("select * from t_answer where name = '" + stem + "'");
                            if (dt.Rows.Count < 1)
                            {
                                int r = database.Execute(
                                    "INSERT INTO[t_answer]([name],[ans])VALUES('" + stem + "','" + answer + "')");
                            }
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

                StringBuilder sb = new StringBuilder();
                sb.Append("{\"result\":\"1\",\"success\":\"" + success + "\",\"fail\":\"" + fail + "\"}");
                return Content(sb.ToString());
            }
            catch (Exception ex)
            {
                return Content(err);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ExtAnswer()
        {
            return null;
        }

        /// <summary>
        /// /Doctor/DownAnswer
        /// </summary>
        /// <returns></returns>
        public ActionResult DownAnswer()
        {
            try
            {
                string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
                IDatabase database = /*new SqlDatabase(connString)*/CommonController.database;;
                DataTable dt = database.QueryTable("select * from t_question ");
                if (dt == null || dt.Rows.Count < 1)
                {
                    return null;
                }
                IWorkbook wb = new XSSFWorkbook();
                new OfficeHelper().ImportToWorkbook(dt, ref wb);
                string target = System.AppDomain.CurrentDomain.BaseDirectory +"Files\\"
                    +  Guid.NewGuid().ToString()+".xlsx";
                FileStream fs = null;
                try
                {
                    using (fs = new FileStream(target, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                    {
                        wb.Write(fs);
                    }
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
                return File(target, "application/xls");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// /Doctor/GetUserList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserList()
        {
            string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
            IDatabase database = /*new SqlDatabase(connString)*/CommonController.database;;
            var dt = database.QueryTable("select * from t_ybsUser");
            StringBuilder re = new StringBuilder();
            if (dt == null || dt.Rows.Count < 1)
            {
                return null;
            }
            re.Append("[");
            bool isStart = true;
            foreach (DataRow row in dt.Rows)
            {
                string uid = "";
                try
                {
                    uid = row["uid"].ToString();
                }
                catch { }
                re.Append(isStart?"{":",{");
                isStart = false;
                re.Append(string.Format("\"uid\":\"{0}\"",uid));
                re.Append("}");
            }
            re.Append("]");
            return Content(re.ToString());
        }
    }
}