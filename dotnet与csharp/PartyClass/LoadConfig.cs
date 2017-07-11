using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace dotnet与csharp.PartyClass
{
    class LoadConfig
    {

        public static string GetConnStrings()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../App.config");
            //XmlNode node=doc.chi
            XmlNode n = doc.ChildNodes[1].ChildNodes[1].ChildNodes[0];
            XmlElement e = (XmlElement)n;
            /*<add server="127.0.0.1" database="TDQS_DWFX" uid="st" pwd="st"/>*/
            string server = e.GetAttribute("server");
            string database = e.GetAttribute("database");
            string uid = e.GetAttribute("uid");
            string pwd = e.GetAttribute("pwd");
            string connStrings = "server=" + server + ";database=" + database + ";uid=" + uid + ";pwd=" + pwd;
            return connStrings;
        }

    }
}
