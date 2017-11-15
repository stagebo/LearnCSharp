using BaseCSharp.CodeCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    public class CreateMd5PwdLib
    {
        IDatabase _database = null;
        public CreateMd5PwdLib()
        {
            string connString = "Data Source=127.0.0.1;Initial Catalog=BlogSystem;Persist Security Info=True;User ID=sa;PWD=st";
            _database = new SqlDatabase(connString);//CommonController.database;; ;
        }
        public void Run()
        {
            //CreateNumPwd();
        }
        public void CreateNumPwd()
        {
            string sql = "delete from tp_md5lib where name = '{0}';insert into tp_md5lib values ('{0}','{1}','{2}','{3}')";
            for (int i = 0; i <= 9999999; i++)
            {
                string name = getAlignStringOfNum(i, 7);
                string pwd16 = MD5Utils.MD5Encrypt16(name);
                string pwd32 = MD5Utils.MD5Encrypt32(name);
                string pwd64 = MD5Utils.MD5Encrypt64(name);
                try
                {
                    int result = _database.Execute(string.Format(sql, name, pwd16, pwd32, pwd64));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("密码库创建完毕");

        }
        public string getAlignStringOfNum(int i, int len)
        {
            if ((i + "").Length > len)
            {
                string str = (i + "").Substring(0, len);
                return str;
            }
            if ((i + "").Length == len)
            {
                return i + "";
            }
            else
            {
                string result = i + "";
                int gap = len - result.Length;
                for (int k = 0; k < gap; k++)
                {
                    result = "0" + result;
                }
                return result;
            }

        }
    }
}
