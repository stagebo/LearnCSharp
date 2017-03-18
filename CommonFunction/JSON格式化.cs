using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFunction
{
    class JSON格式化
    {
        /// <summary>
        /// 给一个json字符串，返回json字符串穿插回车和空格之后的字符串
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public void Run()
        {
            Console.WriteLine(FormatJson(JSONString.JSON));
            
        }
        public string FormatJson(string json)
        {
            //TODO

            return json;
        }
    }
    static class JSONString     
    {
        public static string JSON = "";
    }
}
