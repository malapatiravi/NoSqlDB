using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project2Starter
{
    class ReadXML
    {
        
        public void readXML()
        {
            string DemoTitle = "  Demonstrate XmlTextReader Class";
            string Border = " =================================";
            Console.WriteLine("\n{0}", Border);
            Console.WriteLine(DemoTitle);
            Console.WriteLine(Border);

            XmlTextReader tr1 = null;
            try
            {
                // attempt to create reader attached to an xml file in debug directory

                tr1 = new XmlTextReader("../../lectureNote.xml");

                while (tr1.Read())
                {
                    switch (tr1.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.Write("\n  Tag: {0}", tr1.Name);
                            if (tr1.HasAttributes)
                            {
                                for (int i = 0; i < tr1.AttributeCount; i++)
                                {
                                    Console.Write("\n    attrib. = {0}", tr1.GetAttribute(i));
                                }
                            }
                            break;
                        case XmlNodeType.Text:
                            Console.Write("\n  Text: {0}", tr1.Value);
                            break;
                        case XmlNodeType.EndElement:
                            Console.Write("\n  EndTag: {0}", tr1.Value);
                            break;
                        case XmlNodeType.Attribute:
                            Console.Write("\n  Attrib: {0}", tr1.Name);
                            Console.Write(", Attrib: {0}", tr1.Value);
                            break;
                        case XmlNodeType.Comment:
                            Console.Write("\n  comment: {0}", tr1.Value);
                            break;
                        case XmlNodeType.XmlDeclaration:
                            Console.Write("\n  Declar: {0}", tr1.Name);
                            Console.Write("\n  Declar: {0}", tr1.Value);
                            break;
                        case XmlNodeType.EntityReference:
                            Console.Write("\n  Entity: {0}", tr1.Value);
                            break;
                        case XmlNodeType.Document:
                            Console.Write("\n  Docum: {0}", tr1.Value);
                            break;
                        case XmlNodeType.DocumentType:
                            Console.Write("\n  Docum: {0}", tr1.Value);
                            break;
                        case XmlNodeType.Whitespace:
                            Console.Write("\n  WhiteSpace: {0}", tr1.Value);
                            break;
                        case XmlNodeType.SignificantWhitespace:
                            Console.Write("\n  SignifWhite: {0}", tr1.Value);
                            break;
                        default:
                            Console.Write("\n  other: {0}", tr1.Value);
                            break;
                    }
                }

                XmlTextReader tr = null;
                tr = new XmlTextReader("../../lectureNote.xml");
                Console.WriteLine("\n  successfully opened lectureNote.xml");

                while (tr.Read())
                {
                    // move to next content node

                    if (tr.MoveToContent() == XmlNodeType.Element)
                    {
                        // display tag name

                        string tagName = tr.Name;
                        Console.Write("\n  tag name  = {0}", tagName);

                        // attempt to display attributes

                        if (tr.HasAttributes)
                        {
                            for (int i = 0; i < tr.AttributeCount; i++)
                            {
                                Console.Write("\n    attrib. = {0}", tr.GetAttribute(i));
                            }
                        }

                        // attempt to display text body

                        string body = "";

                        if (tr.NodeType == XmlNodeType.Text)
                        {
                            body = tr.ReadElementString();
                            Console.Write("\n    body    = {0}", body);
                        }
                    }
                }
                Console.WriteLine("\n\n  End of Demo\n\n");
            }
            catch (Exception xmlexp)
            {
                Console.WriteLine("\n  " + xmlexp.Message + "\n\n");
            }
        }
    
        static void Main(string[] args)
        {
            ReadXML r1 = new ReadXML();
            r1.readXML();

        }
    }
}
