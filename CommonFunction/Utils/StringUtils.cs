using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonFunction.Utils
{
    public class StringUtils
    {
        public static void Test()
        {
            Console.WriteLine("this is dll test!");
        }

        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            return System.Text.Encoding.Default.GetString(result);
        }

        public static string GetRandomString(int length)
        {
            int l = length;
            char[] str = "abcdefghijklmnopqrstuvwxyz0123456789".ToArray<char>();
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            while (l-- > 0) {
                sb.Append(str[r.Next(length)]);
            }
            return sb.ToString() ;
        }
        public static string GetRandomString()
        {
            return GetRandomString(6);
        }
    }
}
