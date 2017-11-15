using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCSharp.CodeCollection;


namespace BaseCSharp.PartyClass
{
    class DatabaseHelperTest
    {
        public void Run()
        {
            string connString = "Data Source=127.0.0.1;Initial Catalog=stagebo;Persist Security Info=True;User ID=sa;PWD=st";
            IDatabase database = new SqlDatabase(connString);// CommonController.database;;
            int r = database.Execute(@"
                                INSERT INTO[dbo].[t_test]([id],[content])
                                     VALUES(1, 'dfsdfjkdfldfsdfkd;lf')");
            if (r < 1)
            {
                Console.WriteLine("插入错误！");
            }
            else
            {
                Console.WriteLine("insert succeed!");
            }
        }
    }
}
