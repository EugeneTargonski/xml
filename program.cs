using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace XmlGetValue
{
    class Program
    {
        public const string ext = ".out";
        public static string nodeName;
        public static string paramName;
        
        public static List<string> output = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                nodeName = args[0];
                paramName = args[1];
            }
            catch 
            {
                throw new ArgumentException("You need 2 parameters. 1st is Node Name, 2nd is Node Parameter Name");
            }
            
            var fileNames = Directory.EnumerateFiles(@".\", "*.xml").Select(Path.GetFileName);
            
            foreach (var fileName in fileNames)
            {
                ProcessDocument(fileName);
            }
        }

        private static void ProcessDocument(string fileName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xmlRoot = xDoc.DocumentElement;
            SearchNodes(xmlRoot);
            if (output.Count == 0) return;
            SaveOutput(fileName);
        }

        static void SearchNodes(XmlNode xmlRoot)
        {
            foreach (XmlNode node in xmlRoot)
            {
                if (node.Name == nodeName)
                    output.Add(node.Attributes?.GetNamedItem(paramName)?.Value ?? "");
                SearchNodes(node);
            }
        }

        private static void SaveOutput(string fileName)
        {
            using(StreamWriter sw = new StreamWriter(fileName + ext, false))
            {
                foreach (var line in output)
                    sw.WriteLine(line);
            }
        }
    }
}
