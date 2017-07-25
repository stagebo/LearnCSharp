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
    [Right]
    public class MessageBoardController : Controller
    {
        // GET: /MessageBoard/Index
        public ActionResult Index()
        {
            return View("Page_MessageBoard");
        }
        [GlobalHandleError]
        public ActionResult Exception()
        {
            int x = int.Parse("adfd");

            return null;
        }
        /// <summary>
        /// POST /MessageBoard/SubmitMessage 提交留言信息
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SubmitMessage()
        {
            try
            {
                string message = Request.Form["content"];
                message = Server.UrlDecode(message);
                message = string.IsNullOrWhiteSpace(message) ? "空" : message;
                string nickName = string.IsNullOrWhiteSpace(Request.Form["nickName"]) ? "游客" : Request.Form["nickName"];
                nickName = Session["uid"].ToString();
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
            }
            catch (Exception e)
            {

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

            var result = new StringBuilder();
            result.Append("{\"result\":1,");
            result.Append("\"data\":[");
            bool start = true;
            foreach (DataRow r in dt.Rows)
            {
                result.Append(start ? "" : ",");
                start = false;
                result.Append("{");
                bool start2 = true;
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Append(start2 ? "" : ",");
                    start2 = false;
                    result.Append($"\"{dc.ColumnName}\":\"{Uri.EscapeUriString(r[dc.ColumnName].ToString())}\"");
                }

                result.Append("}");
            }
            result.Append("]}");
            return Content(result.ToString());
        }
        /// <summary>
        /// POST /MessageBoard/DeleteSingleCommon
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingleCommon()
        {
            try
            {
                string id = Request.Form["f_id"];
                if (string.IsNullOrWhiteSpace(id))
                {
                    string err = "{\"result\":\"0\",\"state\":\"0\"}";
                    return Content(err);
                }
                string sql = $"select top 1 [f_writer_name] from [dbo].[t_message] where f_message_id='{id}' ";
                SqlConnection conn = DBUtils.GetConnection();
                SqlCommand com = new SqlCommand();

                com.Connection = conn;
                com.CommandType = CommandType.Text;
                com.CommandText = sql.ToString();

                string data_uid = com.ExecuteScalar()?.ToString();
                if (data_uid!=null&&!Session["uid"].Equals(data_uid))
                {
                    string err = "{\"result\":\"0\",\"state\":\"1\"}";
                    return Content(err);
                }
                sql = $"delete from [dbo].[t_message] where f_message_id='{id}' ";
                com.CommandType = CommandType.Text;
                com.CommandText = sql.ToString();

                int result = com.ExecuteNonQuery();
                if (result == -1)
                {
                    string err = "{\"result\":\"0\",\"state\":\"2\"}";
                    return Content(err);
                }
                com.Dispose();
                DBUtils.DisposeConnection(conn);
            }
            catch (Exception e) { }
            string suc = "{\"result\":\"1\"}";
            return Content(suc);
        }
    }
}