using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.Entity
{
    class Person1
    {
        public string name;
        public int age;
        public int gender; 

        public override string ToString() {
            StringBuilder re = new StringBuilder();
            re.Append("[")
                .Append(name).Append(",")
                .Append(age).Append(",")
                .Append(gender).Append("]");
            return re.ToString();
        }
    }
}
