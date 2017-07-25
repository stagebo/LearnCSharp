using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBlog.Filter
{
    public class ResourceAttribute : AuthorizeAttribute
    {
        private string _resouceName;
        private string _action;
        public ResourceAttribute(string name)
        {
            
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            _resouceName = name;
            //把资源名称设置成Policy名称
           // Policy = _resouceName;
        }

        public ActionResult Check(string userName) {

            return null;
        }
       
    }
}