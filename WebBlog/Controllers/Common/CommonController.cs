using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBlog.Controllers.Common
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// GET /Common/ErrorLogin
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorLogin()
        {
            return View("Page_Error_Login");
        }
        /// <summary>
        /// /Common/ErrorException
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorException() {
            return View("Page_Error_Exception");
        }
        /// <summary>
        /// /Common/GetHtmlEditor
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHtmlEditor() {
            return View("Part_Html_Editor");
        }
    }
}