using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DatabaseUtils.Database;
using WebBlog.Filter;

namespace WebBlog.Controllers.MessageBoard
{
    public class MessageBoardController : Controller
    {
        [Resource("123")]
        // GET: /MessageBoard/Index
        public ActionResult Index()
        {
            
            string refUrl = Request.UrlReferrer?.ToString();
            try
            {
                if (refUrl==null||!string.IsNullOrWhiteSpace(refUrl))
                {
                    return View("Page_Error_Login");
                }
            }
            
            catch (Exception e) { }

            return View("Page_MessageBoard");
        }
        public ActionResult SubmitMessage()
        {
            string message = Request.Form["content"];
            message = string.IsNullOrWhiteSpace(message) ? "空" : message;
            string nickName = string.IsNullOrWhiteSpace(Request.Form["nickName"]) ? "游客" : Request.Form["nickName"];
            string sql = $@"
                    INSERT INTO [dbo].[t_message]
                           ([f_message_id] ,[f_writer_id],[f_writer_name] ,[f_common_date]
                           ,[f_parent_message_id] ,[f_message_type],[f_message_exist]
                           ,[f_content])
                     VALUES
                           ('{Guid.NewGuid()}','' ,'{nickName}' ,'{DateTime.Now.ToString()}'
                           ,'' ,'' ,'1'
                           ,'{message}')
                    ";
            SqlConnection conn = DBUtils.GetConnection();
            SqlCommand com = new SqlCommand();

            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = sql.ToString();

            int result = com.ExecuteNonQuery();
            DBUtils.DisposeConnection(conn);

            if (result == 1)
            {
                return Content("{\"result\":1}");
            }
            return Content("{\"result\":0}");
        }
        /// <summary>
        /// post /MessageBoard/SearchCommonList
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchCommonList()
        {
            string errorString = "{\"result\":0}";
            string sql = "select * from t_message order by f_common_date desc";

            SqlConnection conn = DBUtils.GetConnection();
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, conn); //绑定的sql命令
            adapter.Fill(dataSet, "common_list"); //将数据获取后装载到dataSet中，当作一张表存储在内存中                
            if (dataSet.Tables.Count < 1)
            {
                return Content(errorString);
            }
            DataTable dt = dataSet.Tables[0];

            var result= new StringBuilder();
            result.Append("{\"result\":1,");
            result.Append("\"data\":[");
            bool start = true;
            foreach(DataRow r in dt.Rows) {
                result.Append(start?"":",");
                start = false;
                result.Append("{");
                bool start2 = true;
                foreach (DataColumn dc in dt.Columns) {
                    result.Append(start2 ? "" : ",");
                    start2 = false;
                    result.Append($"\"{dc.ColumnName}\":\"{r[dc.ColumnName]}\"");
                }

                result.Append("}");
            }
            result.Append("]}");
            return Content(result.ToString());
        }
    }
}