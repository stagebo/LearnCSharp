using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonFunction.Utils;
using DatabaseUtils.Database;

namespace WebBlog.Controllers.MessageBoard
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Page_Login");
        }
        public ActionResult Register()
        {
            return View("Page_Register");
        }
        /// <summary>
        /// GET /Home/SignUp
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUp()
        {
            string errorString = "{\"result\":0}";
            string successString = "{\"result\":1}";
            string uid = Request.Form["uid"];
            string pwd = Request.Form["pwd"];

            if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(pwd))
            {
                return Content(errorString);
            }
            SqlConnection conn = DBUtils.GetConnection();
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = $"select count(*) from [dbo].[t_user] where f_uid='{uid}'";
            if (Convert.ToInt32(com.ExecuteScalar()) > 0)
            {
                return Content(errorString);
            }
            pwd = StringUtils.MD5Encrypt(pwd);
            string sql = $@"INSERT INTO [dbo].[t_user]
                                   ([f_id],[f_uid],[f_pwd] ,[f_reg_date] ,[f_exist])
                             VALUES
                                   ('{Guid.NewGuid()}','{uid}','{pwd}','{DateTime.Now}',1)";



            com.CommandText = sql.ToString();

            int result = com.ExecuteNonQuery();
            DBUtils.DisposeConnection(conn);

            if (result != -1)
            {
                return Content(successString);
            }
            return Content(errorString);
        }


        public ActionResult Validate()
        {
            string errorString = "{\"result\":0}";
            string successString = "{\"result\":1}";
            string uid = Request.Form["uid"];
            string pwd = Request.Form["pwd"];

            if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(pwd))
            {
                return Content(errorString);
            }
            pwd = StringUtils.MD5Encrypt(pwd);
            SqlConnection conn = DBUtils.GetConnection();
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = $"select count(*)  from [dbo].[t_user] where f_uid='{uid}' and f_pwd='{pwd}'";
            try
            {
                if (Convert.ToInt32(com.ExecuteScalar()) > 0)
                {
                    Session.Add("uid",uid);
                    DBUtils.DisposeConnection(conn);
                    return Content(successString);
                }
            }
            catch (Exception e) { }
            DBUtils.DisposeConnection(conn);

            return Content(errorString);
        }
    }
}


//CREATE TABLE[dbo].[t_message](
//	[f_message_id]
//[varchar](50) NOT NULL,

//[f_writer_id] [varchar](50) NULL,
//	[f_writer_name]
//[varchar](50) NULL,
//	[f_common_date]
//[datetime]
//NULL,
//	[f_parent_message_id]
//[varchar](50) NULL,
//	[f_message_type]
//[int]
//NULL,
//	[f_message_exist]
//[int]
//NOT NULL,

//    [f_content] [text]
//NULL,
// CONSTRAINT[PK_t_message] PRIMARY KEY CLUSTERED
//(
//   [f_message_id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]

//GO
//CREATE TABLE[dbo].[t_user](
//	[f_id]
//[varchar](50) NOT NULL,

//[f_uid] [varchar](50) NOT NULL,

//[f_pwd] [varchar](50) NOT NULL,

//[f_reg_date] [datetime]
//NOT NULL,

//[f_email] [varchar](50) NULL,
//	[f_phone]
//[varchar](50) NULL,
//	[f_age]
//[int]
//NULL,
//	[f_gender]
//[int]
//NULL,
//	[f_address]
//[varchar](128) NULL,
//	[f_exist]
//[int]
//NOT NULL,
// CONSTRAINT[PK_t_user] PRIMARY KEY CLUSTERED
//(
//   [f_id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY]

//GO