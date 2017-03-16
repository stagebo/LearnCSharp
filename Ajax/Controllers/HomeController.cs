using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
namespace Ajax.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MyMethod()
        {
            return Content("this is my method!");
        }
        public ActionResult PersumeOfYu()
        {
            string path = Server.MapPath("../YuGuoLi.txt");
            string persumeOfYu = null;
            try { persumeOfYu = System.IO.File.ReadAllText(path); }
            catch { persumeOfYu = "加载失败！"; }
           




            return Content(persumeOfYu);
        }
    }
}
