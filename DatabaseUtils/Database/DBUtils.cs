using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BaseCSharp;
namespace DatabaseUtils.Database
{
    public class DBUtils
    {
        public static SqlConnection GetConnection()
        {
            //先打开两个类库文件
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = LoadConfig.GetConnStrings();//GetConnStrings();
            string server = "127.0.0.1",database="BlogSystem",uid="sa",pwd="st";
            con.ConnectionString = "server=" + server + ";database=" + database + ";uid=" + uid + ";pwd=" + pwd; ;
            con.Open();
            return con;
        }
        public static bool DisposeConnection(SqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
                return true;
            }
            return false;
        }
        public static string GetConnStrings()
        {
            XmlDocument doc = new XmlDocument();
            //string fileName = Microsoft.SqlServer.Server.MapPath("");
            doc.Load(@"../../App.config");
            //XmlNode node=doc.chi
            XmlNode n = doc.ChildNodes[1].ChildNodes[1].ChildNodes[0];
            XmlElement e = (XmlElement)n;
            string server = e.GetAttribute("server");
            string database = e.GetAttribute("database");
            string uid = e.GetAttribute("uid");
            string pwd = e.GetAttribute("pwd");
            string connStrings = "server=" + server + ";database=" + database + ";uid=" + uid + ";pwd=" + pwd;
            return connStrings;
        }
    }
}
