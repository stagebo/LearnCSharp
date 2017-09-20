using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    class SearchChar
    {
        public SearchChar()
        {
            mList = new Dictionary<string, int>()
            {
                { "java",1},
                { "jsp",1},
                { "js",1}
            };
            tempChar = new StringBuilder();

        }
        public Dictionary<string, int> mList = null;
        public StringBuilder tempChar { get; set; }
        public List<KeyValuePair<string, int>> getChar(char c)
        {
            List<KeyValuePair<string, int>> result;
            string temp;
            if (c == '#')
            {
                temp = tempChar.ToString();
                tempChar = new StringBuilder();
                if (mList.ContainsKey(temp))
                {
                    mList[temp] += 1;
                }
                else
                {
                    mList.Add(temp, 1);
                }

            }
            else
            {
                tempChar.Append(c);
                temp = tempChar.ToString();
            }
            result = mList.Cast<KeyValuePair<string, int>>()
                 .Where(kvp => kvp.Key.ToString().StartsWith(temp))
                 .ToList();

            var r = from kp in result
                    orderby kp.Value descending
                    orderby kp.Value
                    select kp;
            return r.ToList();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                Console.WriteLine("please input a char to search,type # to end a word.");
                string str = Console.ReadLine();
                if (str.Length < 1)
                {
                    continue;
                }
                char aim = str.ToCharArray()[0];
                Console.WriteLine("the search aim is " + this.tempChar.ToString() + aim);
                List<KeyValuePair<string, int>> re = this.getChar(aim);
                Console.WriteLine("the result is:");
                string temp = "            key:{0},times:{1}";
                int i = 0;
                foreach (var kp in re)
                {
                    if (i++ > 2) break;
                    Console.WriteLine(string.Format(temp, kp.Key, kp.Value));
                }
                Console.WriteLine("the source list is:");
                foreach (var kp in this.mList)
                {
                    Console.WriteLine(string.Format(temp, kp.Key, kp.Value));
                }
                Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");

            }
        }
    }
}
