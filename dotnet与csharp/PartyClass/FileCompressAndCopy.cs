using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    class FileCompressAndCopy
    {
        /// <summary>
        /// 将整个文件夹复制到目标文件夹中。
        /// </summary>
        /// <param name="sPath">源路径</param>
        /// <param name="dPath">目标路径</param>
        /// <returns></returns>
        private bool CopyDir(string sPath, string dPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (dPath[dPath.Length - 1] != Path.DirectorySeparatorChar)
                    dPath += Path.DirectorySeparatorChar;

                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(dPath))
                    Directory.CreateDirectory(dPath);

                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法

                string[] fileList = Directory.GetFileSystemEntries(sPath);

                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                    {
                        CopyDir(file, dPath + Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        File.Copy(file, dPath + Path.GetFileName(file), true);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CompressFile(string srcFileName, string desFileName)
        {
            return true;
        }
        public static bool CopyFile(string srcFileName, string desFileName)
        {
            try
            {
                System.IO.File.Copy(srcFileName, desFileName, true);
            }
            catch (Exception ex) {
                return false;
            }
            return true;
        }

        //public void ZipFile(string strFile, string strZip)
        //{
        //    if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
        //        strFile += Path.DirectorySeparatorChar;
        //    ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
        //    s.SetLevel(6); // 0 - store only to 9 - means best compression
        //    zip(strFile, s, strFile);
        //    s.Finish();
        //    s.Close();
        //}




        //private void zip(string rootFilePath, string strFile, ICSharpCode.SharpZipLib.Zip.ZipOutputStream s, string staticFile)
        //{
        //    if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
        //    Crc32 crc = new Crc32();
        //    string[] filenames = Directory.GetFileSystemEntries(strFile);
        //    foreach (string file in filenames)
        //    {

        //        if (Directory.Exists(file))
        //        {
        //            zip(rootFilePath, file, s, staticFile);
        //        }

        //        else // 否则直接压缩文件
        //        {
        //            //打开压缩文件
        //            FileStream fs = File.OpenRead(file);

        //            byte[] buffer = new byte[fs.Length];
        //            fs.Read(buffer, 0, buffer.Length);
        //            //string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
        //            string tempfile = file.Replace(rootFilePath, "");
        //            ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(tempfile);

        //            entry.DateTime = DateTime.Now;
        //            entry.Size = fs.Length;
        //            fs.Close();
        //            crc.Reset();
        //            crc.Update(buffer);
        //            entry.Crc = crc.Value;
        //            s.PutNextEntry(entry);

        //            s.Write(buffer, 0, buffer.Length);
        //        }
        //    }
        //}
    }
}
