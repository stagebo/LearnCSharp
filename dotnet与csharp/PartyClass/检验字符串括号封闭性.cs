using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    class 检验字符串括号封闭性
    {
        public static void Run() 
        {
            string s = "{\"result\":\"1\",\"examCount\":\"1\",\"examList\":[{\"Exam\":{\"ExamID\":\"16dd919c-180e-411e-94d9-c20e25719a56\",\"ExamName\":\"%E4%B8%87%E6%B0%B8%E6%B3%A2-%E6%B5%8B%E8%AF%95%E8%80%83%E7%AB%99%E4%BF%A1%E6%81%AF%E5%B1%95%E7%A4%BA\",\"ExamStartTime\":\"2016-12-15%2008%3A02%3A00\",\"ExamEndTime\":\"2017-12-15%2009%3A03%3A00\",\"ExamCreateTime\":\"2016-12-15%2009%3A03%3A00\",\"ExamCreatorUserIdentityID\":\"2b950347-c185-4ac0-a525-3de84186a2aa\",\"ExamDelayReleaseTime\":\"2016-12-16%2008%3A02%3A00\",\"ExamType\":\"0\",\"ExamState\":\"2\",\"ExamVerificationCode\":\"662255\",\"ExamDescription\":\"%E5%8B%BF%E5%88%A0-%E4%B8%87%E6%B0%B8%E6%B3%A2%E6%B5%8B%E8%AF%95%E8%80%83%E7%AB%99%E4%BF%A1%E6%81%AF%E5%B1%95%E7%A4%BA%E4%B8%93%E7%94%A8%E8%80%83%E8%AF%95\",\"ExamIsExist\":\"1\",\"ExamNumber1\":null,\"ExamNumber2\":null,\"ExamNumber3\":null,\"ExamNumber4\":null,\"ExamNumber5\":null,\"ExamNumber6\":null,\"ExamNumber7\":null,\"ExamNumber8\":null,\"ExamNumber9\":null,\"ExamNumber10\":null,\"ExamString1\":null,\"ExamString2\":null,\"ExamString3\":null,\"ExamString4\":null,\"ExamString5\":null,\"ExamString6\":null,\"ExamString7\":null,\"ExamString8\":null,\"ExamString9\":null,\"ExamString10\":null,\"ExamDatetime1\":null,\"ExamDatetime2\":null,\"ExamDatetime3\":null,\"ExamDatetime4\":null,\"ExamDatetime5\":null,\"ExamDatetime6\":null,\"ExamDatetime7\":null,\"ExamDatetime8\":null,\"ExamDatetime9\":null,\"ExamDatetime10\":null},\"ExamTimePeriodStartDate\":\"2017-12-30\",\"ExamTimePeriodStartTime\":\"2017%2F12%2F30%204%3A00%3A00\",\"ExamTimePeriodEndTime\":\"2017%2F12%2F30%206%3A00%3A00\",\"Days\":\"2\",\"TotalDays\":\"2\",\"PersonInCharge\":\"\",\"StudentCount\":\"6\",\"FinishedStudentCount\":\"0\",\"ExamStatus\":\"2\"}]}";
            List<int> l;
            bool f = CheckString(s,out l);
            Console.WriteLine(f);

            Console.ReadKey();
        }
        public static bool CheckString(string json,out List<int> l)
        {
            l = new List<int>();
            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("输入字符串是空白字符串~请重新输入！");
                return false;
            }
            Stack<char> s = new Stack<char>();
            char[] c = json.ToArray<char>();
            for (int i = 0; i < c.Length;i++ )
            {
                char ch = c[i];
                if (ch == '{' || ch == '[')
                {
                    s.Push(ch);
                }
                else if (ch == '}')
                {
                    if (s.Count>0&&s.Pop() != '{')
                    {
                        l.Add(i);
                        return false;
                    }
                }
                else if (ch == ']')
                {
                    if (s.Count >0 && s.Pop() != '[')
                    {
                        l.Add(i);
                        return false;
                    }
                }
            }
            if (s.Count<=0)
            {
                return true;
            }
            return false;
        }
    }
}
