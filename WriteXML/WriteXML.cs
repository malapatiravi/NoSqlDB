using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Project2Starter
{
    class WriteXML
    {
        public void writeXML()
        {
            string DemoTitle = "  Demonstrate XmlTextWriter Class";
            string Border = " =================================";
            Console.WriteLine("\n{0}", Border);
            Console.WriteLine(DemoTitle);
            Console.WriteLine(Border);

            XmlTextWriter tw = null;
            try
            {
                // attempt to create reader attached to an xml file in debug directory

                tw = new XmlTextWriter("demoTextWriter.xml", null);
                Console.WriteLine("\n  successfully opened demoTextWriter.xml");

                tw.Formatting = Formatting.Indented;
                tw.WriteStartDocument();
                tw.WriteStartElement("test");
                tw.WriteStartElement("child1");
                tw.WriteAttributeString("attrib1", "value1");
                tw.WriteString("first body string");
                tw.WriteEndElement();
                tw.WriteElementString("child2", "second body string");
                tw.WriteStartElement("child3");
                tw.WriteAttributeString("attrib2", "hasChildren");
                tw.WriteElementString("grandChild1", "gc body");
                tw.WriteElementString("grandChild2", "gc body");
                tw.WriteEndElement();
                tw.WriteEndDocument();
                tw.Flush();
                tw.Close();

                string path = Directory.GetCurrentDirectory();
                string[] fileEntries = Directory.GetFiles(path, "demoTextWriter.xml");
                StreamReader Reader = new StreamReader(fileEntries[0]);
                string line;
                while ((line = Reader.ReadLine()) != null)
                {
                    Console.Write("\n  {0}", line);
                }
                Console.WriteLine("\n\n  End of Demo\n\n");
            }
            catch (XmlException xmlexp)
            {
                Console.WriteLine(xmlexp.Message);
            }
        }

    
    static void Main(string[] args)
        {
        WriteXML wr1 = new WriteXML();
        wr1.writeXML();
        }
    }
}
