using BaseCSharp.CodeCollection;
using BaseCSharp.CodeCollection.SqlServer;
using PracticeProgram;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PracticeWeb.Controllers
{
    public class ExaminationController : Controller
    {
        // GET: Examination
        public ActionResult Index()
        {
            return View("Examination");
        }
        public ActionResult TestExam()
        {
            string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
            IDatabase database = new SqlDatabase(connString);
            HttpHelper http = Session["http"] as HttpHelper;
            string uid = Request.Form["uid"];
            string url = "http://api.yiboshi.com/api/study/student/usercenter/validateUserCreditBindingTerm?trainingIds=363-0&userId=185313";
            string s1=http.SendGet(url,new Dictionary<string, string>());

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
            string s8= http.SendGet(url, new Dictionary<string, string>());

            url = "http://online.yiboshi.com/online/api/exam/getExamInfo";
            string result=http.SendPost(url,new Dictionary<string, string>());

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
                asb.Append(isStart?"":",");
                isStart = false;
                var itemDic = item as Dictionary<string, object>;
                var name = itemDic["htmlContent"];
                DataTable dt = database.QueryTable("select * from t_answer where name ='"+name+"'");
                var answer = dt.Rows[0]["ans"].ToString();
                asb.Append("\""+ index + "\":\""+ answer + "\"");
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

    }
}