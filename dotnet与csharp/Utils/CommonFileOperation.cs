using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public  class CommonFileOperation
    {
        /// <summary>
        /// 解压文件操作 
        /// </summary>
        /// <param name="softwareContent">要解压的二进制</param>
        /// <param name="zipedFolder">解压后的文件路径</param>
        /// <returns></returns>
        public static bool UnZipFileOperation(byte[] softwareContent, string zipedFolder)
        {
            bool result = UnZipFileOperation(softwareContent, zipedFolder, null);
            return result;
        }

        /// <summary>
        /// 解压文件操作 
        /// </summary>
        /// <param name="softwareContent">要解压的二进制</param>
        /// <param name="zipedFolder">解压后的文件路径</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private static bool UnZipFileOperation(byte[] softwareContent, string zipedFolder, string password)
        {
            /* 防止中文路径乱码TODO */
            //ZipConstants.DefaultCodePage = "GBK";
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;
            if (!Directory.Exists(zipedFolder))
            {
                Directory.CreateDirectory(zipedFolder);
            }
            try
            {
                Stream stream = new MemoryStream(softwareContent);
                zipStream = new ZipInputStream(stream);
                if (!string.IsNullOrEmpty(password))
                {
                    zipStream.Password = password;
                }
              
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');

                        if (fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }
                        fs = System.IO.File.Create(fileName);

                        #region 一次性读取
                        //int size = 2048;
                        //byte[] data = new byte[size];// 这么写如果文件不足2048 文件输出后面会有空白
                        int size;
                        byte[] data = new byte[zipStream.Length];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                fs.Write(data, 0, data.Length);
                            }
                            else
                            {
                                break;
                            }
                        }
                        #endregion

                        #region 分段读取
                        /* 每次读取的长度 */
                        //int maxLength = 1024;
                        ///* 分段读取 */
                        //byte[] data = new byte[maxLength];
                        ////读取位置  
                        //int start = 0;
                        ////实际返回结果长度  
                        //int num = 0;
                        ////尚未读取的文件内容长度  
                        //long left = zipStream.Length;
                        //while (left > 0)
                        //{
                        //    fs.Position = start;
                        //    num = 0;
                        //    if (left < maxLength)
                        //    {
                        //        num = zipStream.Read(data, 0, Convert.ToInt32(left));
                        //    }
                        //    else
                        //    {
                        //        num = zipStream.Read(data, 0, maxLength);
                        //    }
                        //    if (num == 0)
                        //    {
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        fs.Write(data, 0, num);
                        //    }
                        //    start += num;
                        //    left -= num;
                        //}

                        #endregion
                       
                        fs.Close();
                    }
                }
            }
            catch(Exception e)
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                //GC.Collect();
                //GC.Collect(1);
            }
            return result;
        }

        /// <summary>
        /// 压缩文件或文件夹
        /// </summary>
        /// <param name="fileToZip">要压缩的文件或文件夹路径</param>
        /// <param name="zipedFile">压缩后的文件路径</param>
        /// <returns></returns>
        public static bool ZipFileOperation(string fileToZip, string zipedFile)
        {
            bool result = ZipFileOperation(fileToZip, zipedFile, null);
            return result;
        }

        /// <summary>
        /// 压缩文件或文件夹
        /// </summary>
        /// <param name="fileToZip">要压缩的文件或文件夹路径</param>
        /// <param name="zipedFile">压缩后的文件路径</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private static bool ZipFileOperation(string fileToZip, string zipedFile, string password)
        {
            /* 防止中文路径乱码TODO */
            //ZipConstants.DefaultCodePage = zip_charset;
            bool result = false;
            if (Directory.Exists(fileToZip))
            {
                result = ZipDirectory(fileToZip, zipedFile, password);
            }
            else if (System.IO.File.Exists(fileToZip))
            {
                result = ZipFiles(fileToZip, zipedFile, password);
            }
            return result;
        }

        /// <summary>
        /// 递归压缩文件夹
        /// </summary>
        /// <param name="folderToZip">要压缩的文件夹路径</param>
        /// <param name="zipStream">压缩输出流</param>
        /// <param name="parentFolderName">此文件夹的上级文件夹</param>
        /// <returns></returns>
        private static bool ZipDirectoryByIteration(string folderToZip, ZipOutputStream zipStream, string parentFolderName)
        {
            bool result = true;
            string[] folders, files;
            ZipEntry ent = null;
            FileStream fs = null;
            /* 用来检验压缩后和压缩前的文件是否一致，损坏 */
            Crc32 crc = new Crc32();
            try
            {
                //ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/"));
                /* 外层不带有文件夹 */
                if (!string.IsNullOrEmpty(parentFolderName)) {
                    ent = new ZipEntry(parentFolderName);
                    zipStream.PutNextEntry(ent);
                    zipStream.Flush();
                }

                files = Directory.GetFiles(folderToZip);
                foreach (string file in files)
                {
                    fs = System.IO.File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    //ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/" + Path.GetFileName(file)));
                    ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(file)));
                    ent.DateTime = DateTime.Now;
                    ent.Size = fs.Length;

                    fs.Close();
                    crc.Reset();
                    /* 计算文件流的crc码   */
                    crc.Update(buffer);
                    /* 把crc码放到压缩类的crc属性里，类库会自行检验。 */
                    ent.Crc = crc.Value;
                    zipStream.PutNextEntry(ent);
                    zipStream.Write(buffer, 0, buffer.Length);
                }

            }
            catch(Exception e)
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                //GC.Collect();
                //GC.Collect(1);
            }

            folders = Directory.GetDirectories(folderToZip);
            foreach (string folder in folders)
            {
                //if (!ZipDirectoryByIteration(folder, zipStream, Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/")))
                if (!ZipDirectoryByIteration(folder, zipStream, Path.Combine(parentFolderName, Path.GetFileName(folder) + "/")))
                {
                    return false;
                }
            }
            return result;
        }

        /// <summary>
        /// 压缩文件夹   
        /// </summary>
        /// <param name="folderToZip">要压缩的文件夹路径</param>
        /// <param name="zipedFile">压缩后文件路径</param>
        /// <returns></returns>
        private static bool ZipDirectory(string folderToZip, string zipedFile)
        {
            bool result = ZipDirectory(folderToZip, zipedFile, null);
            return result;
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="folderToZip">要压缩的文件夹路径</param>
        /// <param name="zipedFile">压缩后文件路径</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private static bool ZipDirectory(string folderToZip, string zipedFile, string password)
        {
            bool result = false;
            if (!Directory.Exists(folderToZip))
            {
                return result;
            }

            ZipOutputStream zipStream = new ZipOutputStream(System.IO.File.Create(zipedFile));
            zipStream.SetLevel(6);
            if (!string.IsNullOrEmpty(password))
            {
                zipStream.Password = password;
            }

            //result = ZipDirectoryByIteration(folderToZip, zipStream, Path.GetFileName(folderToZip) + "/");
            result = ZipDirectoryByIteration(folderToZip, zipStream, "");

            zipStream.Finish();
            zipStream.Close();
            return result;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToZip">要压缩的文件路径</param>
        /// <param name="zipedFile">压缩后的文件路径</param>
        /// <returns></returns>
        private static bool ZipFiles(string fileToZip, string zipedFile)
        {
            bool result = ZipFiles(fileToZip, zipedFile, null);
            return result;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToZip">要压缩的文件路径</param>
        /// <param name="zipedFile">压缩后的文件路径</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private static bool ZipFiles(string fileToZip, string zipedFile, string password)
        {
            bool result = true;
            ZipOutputStream zipStream = null;
            FileStream fs = null;
            ZipEntry ent = null;

            if (!System.IO.File.Exists(fileToZip))
            {
                return false;
            }
            try
            {
                fs = System.IO.File.OpenRead(fileToZip);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                fs = System.IO.File.Create(zipedFile);
                zipStream = new ZipOutputStream(fs);
                if (!string.IsNullOrEmpty(password))
                {
                    zipStream.Password = password;
                }
                ent = new ZipEntry(Path.GetFileName(fileToZip));
                zipStream.PutNextEntry(ent);
                /* 压缩级别0-9 */
                zipStream.SetLevel(6);
                zipStream.Write(buffer, 0, buffer.Length);

            }
            catch(Exception e)
            {
                result = false;
            }
            finally
            {
                if (zipStream != null)
                {
                    zipStream.Finish();
                    zipStream.Close();
                }
                if (ent != null)
                {
                    ent = null;
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
           // GC.Collect();
           // GC.Collect(1);
            return result;
        }

        /// <summary>
        /// 删除文件夹文件及其子文件夹文件
        /// </summary>
        /// <param name="directoryPath"></param>
        public static bool DeleteFolderOperation(string directoryPath)
        {
            bool result = true;
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            if (Directory.GetFileSystemEntries(directoryPath).Length > 0)
            {
                foreach (string file in Directory.GetFileSystemEntries(directoryPath))
                {
                    //文件夹已存在
                    if (System.IO.File.Exists(file))
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                        System.IO.File.Delete(file);
                    }
                    else
                    {
                        DeleteFolderOperation(file);
                    }
                }
                //删除文件夹 try catch
                Directory.Delete(directoryPath);
            }
            return result;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
       public static string GetDirectoryLength(string dirPath)
        {
           
            FileInfo file = new FileInfo(dirPath);
            if (!file.Exists)
            {
                return "0";
            }

            double size = file.Length;
            string softwareSize = size.ToString("0.00");
            string softwareSizeKB = (size / 1024).ToString("0.00");
            string softwareSizeMB = (size / 1024 / 1024).ToString("0.00");

            if (size < 1024)
            {
                softwareSize = softwareSize + "B";
            }
            if (file.Length < 1024 * 1024 && file.Length > 1024)
            {
                softwareSize = softwareSizeKB + "KB";
            }
            if (file.Length > 1024 * 1024)
            {
                softwareSize = softwareSizeMB + "MB";
            }
            return softwareSize;
         
        }
       
       /// <summary>
       /// 解压文件
       /// </summary>
       /// <param name="TargetFile">待解压的文件</param>
       /// <param name="fileDir">解压后存放路径</param>
       /// <returns></returns>
       public static bool unZipFile(string TargetFile, string fileDir)
       {
           try
           {
               /* 读取压缩文件(zip文件),准备解压缩 */
               ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
               ZipEntry theEntry;
               /* 解压出来的文件保存的路径 */
               string path = fileDir;

               /* 根目录下的第一级子文件夹的名称 */
               string rootDir = " ";
               while ((theEntry = s.GetNextEntry()) != null)
               {
                   /* 得到根目录下的第一级子文件夹的名称 */
                   rootDir = Path.GetDirectoryName(theEntry.Name);
                   if (rootDir.IndexOf("\\") >= 0)
                   {
                       rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                   }
                   /* 根目录下的第一级子文件夹的下的文件夹的名称 */
                   string dir = Path.GetDirectoryName(theEntry.Name);
                   /* 根目录下的文件名称 */
                   string fileName = Path.GetFileName(theEntry.Name);
                   /* 创建根目录下的子文件夹,不限制级别 */
                   if (dir != " ")
                   {
                       if (!Directory.Exists(fileDir + "\\" + dir))
                       {
                           //在指定的路径创建文件夹
                           path = fileDir + "\\" + dir;
                           Directory.CreateDirectory(path);
                       }
                   }
                   /* 根目录下的文件 */
                   else if (dir == " " && fileName != "")
                   {
                       path = fileDir;
                   }
                   else if (dir != " " && fileName != "") /* 根目录下的第一级子文件夹下的文件 */
                   {
                       /* 指定文件保存的路径 */
                       if (dir.IndexOf("\\") > 0)
                       {
                           path = fileDir + "\\" + dir;
                       }
                   }
                   /* 判断是不是需要保存在根目录下的文件 */
                   if (dir == rootDir)
                   {
                       path = fileDir + "\\" + rootDir;
                   }
                   if (fileName != String.Empty)
                   {
                       FileStream streamWriter = File.Create(path + "\\" + fileName);

                       int size = 2048;
                       byte[] data = new byte[2048];
                       while (true)
                       {
                           size = s.Read(data, 0, data.Length);
                           if (size > 0)
                           {
                               streamWriter.Write(data, 0, size);
                           }
                           else
                           {
                               break;
                           }
                       }

                       streamWriter.Close();
                   }
               }
               s.Close();

               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
        
    }
}
