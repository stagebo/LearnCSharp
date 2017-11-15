using BaseCSharp;
using BaseCSharp.CodeCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeWeb.Controllers
{
    public class CommonController : Controller
    {
        public static string dbName =
            @"D:\vs workplace\LearnCCSharp\File\\ybsWeb.Data";
        
        public static IDatabase database = new SqlliteHelp(dbName);
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
    }
}