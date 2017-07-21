using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    /// <summary>
    /// LinqTest
    /// 2017年7月21日14:30:42
    /// </summary>
    class LinqTest
    {
        public static void Run()
        {
            List<User> l = new List<User>();
            for (int i = 10; i < 30; i++)
                l.Add(new User() { id = i, name = "user_"+i,pwd=i+"abc",address="null" });
            printList(l);
            var t = from u in l where u.id%2==0&&u.name.StartsWith("user_2") orderby u.pwd descending select u;
            printList(t);
        }
        private static void printList(IEnumerable<User> u) {
            Console.WriteLine($"***********{u}-Start****************");
            foreach (User uu in u)
                Console.WriteLine(uu.ToString());
            Console.WriteLine($"***********{u}-End****************");
        }
        private class User
        {
            public int id;
            public string name;
            public string pwd;
            public string address;
            public string ToString() {
                return $"[id:{id},name:{name},pwd:{pwd},address:{address}";
            }
        }
    }

}
