using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XmlGetValue
{
    class Program
    {
        public const string ext = ".out";

        public static string fileName;
        public static string nodeName;
        public static string paramName;
        
        public static List<string> output = new List<string>();

        static void SearchNodes(XmlNode xmlRoot)
        {
            foreach (XmlNode node in xmlRoot)
            {
                if (node.Name == nodeName) 
                    output.Add(node.Attributes?.GetNamedItem(paramName)?.Value??"");
                SearchNodes(node);
            }
        }
        static void Main(string[] args)
        {
            nodeName = args[0];
            paramName = args[1];
            fileName = args[2];

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xmlDocument = xDoc.DocumentElement;
            SearchNodes(xmlDocument);

            if (output.Count == 0) return;
            SaveOutput();
        }

        private static void SaveOutput()
        {
            using StreamWriter sw = new StreamWriter(fileName + ext, false);
            foreach (var line in output)
                sw.WriteLine(line);
        }
    }
}
