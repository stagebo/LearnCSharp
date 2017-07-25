using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBlog.Filter
{
    public class RightAttribute: ActionFilterAttribute
    {
        public string Name { set; get; } = "123123123123s";

        //action执行之前先执行此方法  
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            HttpContext.Current.Response.Write("<br />OnOnActionExecuting:" + Name);
        }

        //action执行之后先执行此方法  
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            HttpContext.Current.Response.Write("<br />onActionExecuted:" + Name);
        }
        //actionresult执行之前执行此方法  
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            HttpContext.Current.Response.Write("<br />OnResultExecuting:" + Name);

        }

        //actionresult执行之后执行此方法  
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            HttpContext.Current.Response.Write("<br />OnResultExecuted:" + Name);

        }
    }
}