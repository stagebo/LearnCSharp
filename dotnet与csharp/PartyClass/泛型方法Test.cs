using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    /// <summary>
    /// 模拟NHibernate处理数据三层业务结构，梳理流程，简化代码-2017年7月31日09:21:07
    /// </summary>
    class 泛型方法Test
    {
        public void Run()
        {
            new BLL().AddModel<Mod>(new Mod());
        }

    }

    #region  BLL
    class BBLL
    {
        public void AddModel<T>(T t) where T : M
        {
            Console.WriteLine("BaseBLL Method Invoked!");
            new DAL().InsertModel<T>(t);
        }
    }
    class BLL : BBLL
    {

    }
    #endregion

    #region DAL
    class DDAL
    {
        public void InsertModel<T>(T t) where T : M
        {
            Console.WriteLine("BaseDAL Method Invoked!");
        }
    }
    class DAL : DDAL
    {

    }
    #endregion

    #region Model
    class M
    {

    }
    class Mod : M
    {

    }
    #endregion

}
