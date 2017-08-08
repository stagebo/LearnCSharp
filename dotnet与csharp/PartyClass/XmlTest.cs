using System;
using System.Xml;

namespace BaseCSharp
{
    class XmlTest : ITest
    {
        public void Run()
        {
            string fileName = "G:/TestFiles/book.config";
            this.Write(fileName);
            this.Read(fileName);
        }
        public void Write(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Root");
            root.SetAttribute("Name", "BookCollection");
            XmlElement book1 = doc.CreateElement("Book");
            book1.SetAttribute("BookName", "三国演义");
            XmlElement book2 = doc.CreateElement("Book");
            book2.SetAttribute("BookName", "水浒传");
            root.AppendChild(book1);
            root.AppendChild(book2);
            doc.AppendChild(root);

            doc.Save(fileName);
        }

        public void Read(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            var root=doc.GetElementsByTagName("Root")[0].ChildNodes[0].Attributes["BookName"];
            Console.WriteLine("BookName="+root.Value);
        }
    }
}
