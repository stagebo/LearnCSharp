/**
 * 时间：2017年9月22日09:38:32
 * 作者：stagebo
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    public class EmailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid">用户名、也即发送人</param>
        /// <param name="pwd">密码</param>
        /// <param name="server">邮件发送服务器</param>
        public EmailHelper(string uid, string pwd, string server, int port = 25)
        {
            this._uid = uid;
            this._pwd = pwd;
            this._server = server;
            this._port = port;
            _client = new SmtpClient(_server, _port);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiverList">收信人List</param>
        /// <param name="title">标题</param>
        /// <param name="content">正文</param>
        /// <param name="copyToList">抄送List</param>
        /// <param name="attachFilePathList">附件路径List</param>
        /// <returns>是否发送成功</returns>    
        public bool Send(List<string> receiverList, string title, string content = null, List<string> copyToList = null, List<string> attachFilePathList = null)
        {
            if (receiverList == null || title == null)
            {
                return false;
            }

            try
            {
                MailMessage message = new MailMessage();
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                message.From = new MailAddress(_uid);
                //设置收件人,可添加多个,添加方法与下面的一样
                foreach (string receiver in receiverList)
                {
                    message.To.Add(receiver);
                }
                if (copyToList != null)
                {
                    foreach (string copyTo in copyToList)
                    {
                        //设置抄送人
                        message.CC.Add("1254117589@qq.com");
                    }
                }
                //设置邮件标题
                message.Subject = title;
                //设置邮件内容
                message.Body = content == null ? "" : content;
                //添加附件
                AddAttachments(attachFilePathList, message);
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看
                SmtpClient client = new SmtpClient(_server, 25);
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(_uid, _pwd);
                //启用ssl,也就是安全发送
                client.EnableSsl = true;
                //发送邮件
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void Run()
        {
            var emailAcount = "1254117589@qq.com";
            var emailPassword = "w1254117589";
            var reciver = "1254117589@qq.com";
            var content = "this is content test.";
            var title = "Test";
            MailMessage message = new MailMessage();
            //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
            MailAddress fromAddr = new MailAddress("1254117589@qq.com");
            message.From = fromAddr;
            //设置收件人,可添加多个,添加方法与下面的一样
            message.To.Add(reciver);
            //设置抄送人
            message.CC.Add("1254117589@qq.com");
            //设置邮件标题
            message.Subject = title;
            //设置邮件内容
            message.Body = content;
            //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
            SmtpClient client = new SmtpClient("smtp.qq.com", 25);
            //设置发送人的邮箱账号和密码
            client.Credentials = new NetworkCredential(emailAcount, emailPassword);
            //启用ssl,也就是安全发送
            client.EnableSsl = true;
            //发送邮件
            client.Send(message);
        }

        #region 私有方法
        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        private void AddAttachments(List<string> filePathList, MailMessage message)
        {
            if (filePathList == null || filePathList.Count < 1 || message == null)
            {
                return;
            }
            try
            {
                Attachment data;
                ContentDisposition disposition;
                foreach (string path in filePathList)
                {
                    data = new Attachment(path, MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path);
                    disposition.ModificationDate = File.GetLastWriteTime(path);
                    disposition.ReadDate = File.GetLastAccessTime(path);
                    message.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        private string _uid;
        private string _pwd;
        private string _server;
        private int _port;
        private SmtpClient _client;
    }

    public static class EmailServer
    {
        public static string QQEmail = "smtp.qq.com";
    }
}
