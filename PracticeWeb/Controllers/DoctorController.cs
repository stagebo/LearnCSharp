using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PracticeProgram;

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
            for (int i = 0; i < count; i++)
            {
                string pid = Request.Form["pid" + i];
                string cid = Request.Form["cid" + i];

                string result = http.SendGet(submitUtl, new Dictionary<string, string>() {
                            { "trainingId",tid},
                            { "projectId",pid},
                            { "userId",uid},
                            { "courseId",cid},
                            { "score",100+""},
                            { "versionId","3.1"},
                        });
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
    }
}