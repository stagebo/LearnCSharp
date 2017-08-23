using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
/// <summary>
/// CommonFunction.cs文件
/// 2017-8-23 16:45:16
/// c#业务逻辑代码，供c++调用。
/// </summary>
namespace Common
{
    public class CommonFunction
    {
        public static int Add(int a, int b)
        {
            ISession s= null;
            try
            {
                ISessionFactory sf = new Configuration().BuildSessionFactory();
                s = sf.OpenSession();
            }
            catch  { }
            return a + b;
        }
        public void say()
        {
            Console.WriteLine("this is c# code.");
        }
    }
}