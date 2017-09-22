/**
 * 时间：2017年9月22日09:38:32
 * 作者：stagebo
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
     /// <summary>
     /// ftp文件操作类
     /// author：wyb 
     /// 时间：2017年7月27日14:20:49
     /// </summary>  
    class FtpHelper
    {
        /// <summary>
        /// Ftp文件操作类构造函数
        /// </summary>
        /// <param name="host">ftp主机地址</param>
        /// <param name="userName">ftp登录用户名</param>
        /// <param name="password">ftp登录密码</param>
        public FtpHelper(string host, string userName, string password)
        {
            this.hostname = host;
            this.username = userName;
            this.password = password;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localFileName">本地文件全名</param>
        /// <param name="targetDir">ftp服务器文件路径</param>
        /// <param name="targetName">ftp服务器文件名</param>
        /// <returns></returns>
        public bool UploadFile(string localFileName, string targetDir, string targetName)
        {
            /*判断本地文件是否存在*/
            if (!File.Exists(localFileName))
            {
                return false;
            }
            targetName = targetName.Trim();
            FileInfo fileInfo = new FileInfo(localFileName);
            if (fileInfo == null || string.IsNullOrWhiteSpace(targetName) || string.IsNullOrWhiteSpace(hostname))
            {
                return false;
            }

            string URI = "FTP://" + hostname + "/" + targetDir + "/" + targetName;
            //获取ftp对象。
            FtpWebRequest ftp = GetRequest(URI);
            if (ftp == null)
            {
                return false;
            }
            //设置FTP命令 设置所要执行的FTP命令
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            //指定文件传输的数据类型
            ftp.UseBinary = true;
            ftp.UsePassive = true;

            //告诉ftp文件大小
            ftp.ContentLength = fileInfo.Length;
            //缓冲大小设置为2KB
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            //打开一个文件流 (System.IO.FileStream) 去读上传的文件
            using (FileStream fs = fileInfo.OpenRead())
            {
                try
                {
                    //把上传的文件写入流
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            //每次读文件流的2KB
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }

                }
                catch
                {
                    return false;
                }
                finally
                {
                    fs.Close();
                }

            }
            ftp = null;
            return true;
            #region
            /*****
             *FtpWebResponse
             * ****/
            //FtpWebResponse ftpWebResponse = (FtpWebResponse)ftp.GetResponse();
            #endregion 
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localFileName">本地文件全名</param>
        /// <param name="ftpDir"></param>
        /// <param name="ftpFile"></param>
        /// <returns></returns>
        public bool DownloadFile(string localFileName, string ftpDir, string ftpFile)
        {
            if (string.IsNullOrWhiteSpace(hostname)
                || string.IsNullOrWhiteSpace(ftpFile)
                || string.IsNullOrWhiteSpace(localFileName))
            {
                return false;
            }
            string URI = "FTP://" + hostname + "/" + ftpDir + "/" + ftpFile;
            FtpWebRequest ftp = GetRequest(URI);
            if (ftp == null)
            {
                return false; ;
            }
            ftp.Method = WebRequestMethods.Ftp.DownloadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = false;
            try
            {
                using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        //loop to read & write to file
                        using (FileStream fs = new FileStream(localFileName, FileMode.CreateNew))
                        {
                            try
                            {
                                byte[] buffer = new byte[2048];
                                int read = 0;
                                do
                                {
                                    read = responseStream.Read(buffer, 0, buffer.Length);
                                    fs.Write(buffer, 0, read);
                                } while (!(read == 0));
                                responseStream.Close();
                                fs.Flush();
                                fs.Close();
                            }
                            catch (Exception)
                            {
                                /*传输未完成，删除文件*/
                                File.Delete(localFileName);
                                return false;
                            }
                            finally
                            {
                                fs.Close(); ;
                            }
                        }
                        responseStream.Close();
                    }
                    response.Close();
                }
            }
            catch
            {
                return false;
            }
            ftp = null;
            return true;
        }

        /// <summary>
        /// 搜索远程文件
        /// </summary>
        /// <param name="targetDir"></param>
        /// <param name="SearchPattern"></param>
        /// <returns></returns>
        public List<string> ListDirectory(string targetDir, string SearchPattern)
        {
            List<string> result = new List<string>();
            try
            {
                string URI = "FTP://" + hostname + "/" + targetDir + "/" + SearchPattern;

                FtpWebRequest ftp = GetRequest(URI);
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                ftp.UsePassive = true;
                ftp.UseBinary = true;


                string str = GetStringResponse(ftp);
                str = str.Replace("\r\n", "\r").TrimEnd('\r');
                str = str.Replace("\n", "\r");
                if (str != string.Empty)
                    result.AddRange(str.Split('\r'));

                return result;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftp"></param>
        /// <returns></returns>
        private string GetStringResponse(FtpWebRequest ftp)
        {
            //Get the result, streaming to a string
            string result = "";
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                long size = response.ContentLength;
                using (Stream datastream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(datastream, System.Text.Encoding.Default))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }

                    datastream.Close();
                }

                response.Close();
            }

            return result;
        }

        /// 在ftp服务器上创建目录
        /// </summary>
        /// <param name="dirName">创建的目录名称</param>
        public bool MakeDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + hostname + "/" + dirName;
                FtpWebRequest ftp = GetRequest(uri);
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dirName">删除的目录名</param>
        public bool delDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + hostname + "/" + dirName;
                FtpWebRequest ftp = GetRequest(uri);
                ftp.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="currentFilename">当前目录名称</param>
        /// <param name="newFilename">重命名目录名称</param>
        /// <param name="ftpServerIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public bool Rename(string currentFilename, string newFilename)
        {
            try
            {

                FileInfo fileInf = new FileInfo(currentFilename);
                string uri = "ftp://" + hostname + "/" + fileInf.Name;
                FtpWebRequest ftp = GetRequest(uri);
                ftp.Method = WebRequestMethods.Ftp.Rename;
                ftp.RenameTo = newFilename;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

                response.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 向Ftp服务器上传文件并创建和本地相同的目录结构
        /// 遍历目录和子目录的文件
        /// </summary>
        /// <param name="file"></param>
        //private void GetFileSystemInfos(FileSystemInfo file)
        //{
        //    string getDirecName = file.Name;
        //    if (!ftpIsExistsFile(getDirecName, "192.168.0.172", "Anonymous", "") && file.Name.Equals(FileName))
        //    {
        //        MakeDir(getDirecName, "192.168.0.172", "Anonymous", "");
        //    }
        //    if (!file.Exists) return;
        //    DirectoryInfo dire = file as DirectoryInfo;
        //    if (dire == null) return;
        //    FileSystemInfo[] files = dire.GetFileSystemInfos();

        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        FileInfo fi = files[i] as FileInfo;
        //        if (fi != null)
        //        {
        //            DirectoryInfo DirecObj = fi.Directory;
        //            string DireObjName = DirecObj.Name;
        //            if (FileName.Equals(DireObjName))
        //            {
        //                UploadFile(fi, DireObjName, "192.168.0.172", "Anonymous", "");
        //            }
        //            else
        //            {
        //                Match m = Regex.Match(files[i].FullName, FileName + "+.*" + DireObjName);
        //                //UploadFile(fi, FileName+"/"+DireObjName, "192.168.0.172", "Anonymous", "");
        //                UploadFile(fi, m.ToString(), "192.168.0.172", "Anonymous", "");
        //            }
        //        }
        //        else
        //        {
        //            string[] ArrayStr = files[i].FullName.Split('\\');
        //            string finame = files[i].Name;
        //            Match m = Regex.Match(files[i].FullName, FileName + "+.*" + finame);
        //            //MakeDir(ArrayStr[ArrayStr.Length - 2].ToString() + "/" + finame, "192.168.0.172", "Anonymous", "");
        //            MakeDir(m.ToString(), "192.168.0.172", "Anonymous", "");
        //            GetFileSystemInfos(files[i]);
        //        }
        //    }
        //}

        /// <summary>
        /// 判断ftp服务器上该目录是否存在
        /// </summary>
        /// <param name="dirName">目录名</param>
        /// <returns></returns>
        private bool ftpIsExistsFile(string dirName)
        {
            bool flag = true;
            try
            {
                string uri = "ftp://" + hostname + "/" + dirName;
                FtpWebRequest ftp = GetRequest(uri);
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        ///  获取执行结果
        /// </summary>
        /// <param name="URI"></param>
        /// <returns></returns>
        private FtpWebRequest GetRequest(string URI)
        {
            //根据服务器信息FtpWebRequest创建类的对象
            FtpWebRequest result;
            try
            {
                result = (FtpWebRequest)FtpWebRequest.Create(URI);
            }
            catch
            {
                return null;
            }
            //提供身份验证信息
            result.Credentials = new NetworkCredential(username, password);
            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            result.KeepAlive = false;
            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="targetDir">目标路径</param>
        /// <param name="targetName">目标文件名</param>
        /// <returns></returns>
        public bool DeleteFile(string targetDir, string targetName)
        {
            string URI = "FTP://" + hostname + "/" + targetDir + "/" + targetName;
            //获取ftp对象。
            try
            {

                FtpWebRequest ftp = GetRequest(URI);
                ftp = GetRequest(URI);
                ftp.Method = WebRequestMethods.Ftp.DeleteFile; //删除
                ftp.GetResponse();
            }
            catch
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 主机地址
        /// </summary>
        private string hostname = null;
        /// <summary>
        /// ftp主机登录用户名
        /// </summary>
        private string username = null;
        /// <summary>
        /// ftp主机登录密码
        /// </summary>
        private string password = null;
    }
}
