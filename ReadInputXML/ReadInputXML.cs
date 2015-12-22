
///////////////////////////////////////////////////////////////
// ReadInputXML.cs - define noSQL database                       //
// Ver 1.0                                                  //
// Application: Project2 for CSE681-SMA, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Lenovo Thinkpad T540p, Core-i7, Windows 10            //
// Author:      Ravichandra Malapati, CST 4-187, Syracuse University  //
//              (315) 706-3437, rmalapat@syr.edu            //
// Initial release 07-October-2015
// Original Author: Ravichandra Malapati                             //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package implements ReadInputXMLclass
 *Read Input XML is used to read the input from user in the form of XML
 *The XML will have tag <Query> </Query>
 *the user can select any type of Query
 *1. Insert
 *2. Delete
 *3. Modify
 *4. Child Query
 *The class has  the following interfaces
 * 1. public List<PackageElement> ReadInput(DBEngine<int, PackageElement> db, String input_path)
 *      The read input takes database instance and input path for the XML as parameters
 *      This function reads the input from XML parses it and calles the QueryEngine to Insert,Modify, Delete or child Query
 * The return type of this function is a list of package elements which is inturn used to convert in to XML string incase if needed to persist.
 * 2. public void ReadInputtoDatabase(List<PackageElement> list, DBEngine<int, PackageElement> database)
 *      The read InputtoDatabase is used to take input of list and insert inset or suery the in memory database
 *      
 * This class is a starter for the DBEngine package you need to create.
 * It doesn't implement many of the requirements for the db, e.g.,
 * It doesn't remove elements, doesn't persist to XML, doesn't retrieve
 * elements from an XML file, and it doesn't provide hook methods
 * for scheduled persistance.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Project2Starter
{
    public class ReadInputXML
    {
        private static List<PackageElement> packageList;
        /*to convert the object into XML string*/
        public string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }
        /*To convert the data from XML to the object format*/
        public List<PackageElement> ReadInput(DBEngine<string, PackageElement> db, String input_path)
        {
            packageList = (
                   from e in XDocument.Load(input_path).
                             Root.Elements("PackageElement")
                   select new PackageElement
                   {
                       PackageID = (string)e.Element("PackageID"),
                       Description = (string)e.Element("Description"),
                       Query = (string)e.Element("Query"),
                       Name = (string)e.Element("Name"),
                       Author = (string)e.Element("Author"),
                       Country = (string)e.Element("Country"),
                       Time = (DateTime)e.Element("Time"),
                       Time2 = (DateTime)e.Element("Time2"),
                       Payload = (string)e.Element("Payload"),
                       Childs = (
                           from o in e.Elements("Childs").Elements("Child")
                           select new Child
                           {
                               ChildID = (string)o.Element("ChildID"),
                               ChildDate = (DateTime)o.Element("ChildDate"),
                           })
                           .ToArray()
                   })
                   .ToList();

            ReadInputToDatabase(packageList, db);
            return packageList;
        }
        /*to read the input from the list of XML element structures from user into database */
        public void ReadInputToDatabase(List<PackageElement> list, DBEngine<string, PackageElement> database)
        {
            foreach (var element in packageList)
                Console.WriteLine(element.PackageID);
            for (var i = 0; i < packageList.Count; i++)
            {
                if (packageList[i].Query == "Insert")
                {
                    packageList[i].Time = DateTime.Now;
                    bool var = database.insert(packageList[i].PackageID, packageList[i]);
                    if (var)
                        Console.WriteLine("Added Key Successfully:{0} ", packageList[i].PackageID);
                }
                else if (packageList[i].Query == "Delete")
                {
                    bool t = database.Delete(packageList[i].PackageID);
                    if (t == true)
                        Console.WriteLine("Deleted Key Successfully: {0} ", packageList[i].PackageID);
                    else
                        Console.WriteLine("The Key Does not exist please recheck: {0} ", packageList[i].PackageID);
                }
                else if (packageList[i].Query == "Modify")
                {
                    packageList[i].Time = DateTime.Now;
                    bool t = database.Modify(packageList[i].PackageID, packageList[i]);
                    if (t == true)
                    {
                        Console.WriteLine("The Values of key {0} will get modified now addition and/or deletion of relationships, editing text metadata, and replacing an existing value's instance with a new instance");
                        Console.WriteLine("Modified Key Successfully: {0} ", packageList[i].PackageID);
                    }
                    else
                        Console.WriteLine("The Key Does not exist please recheck: {0} ", packageList[i].PackageID);
                }
                else if (packageList[i].Query == "ChildQuery")
                {
                    PackageElement cust = database.ChildQuery(packageList[i].PackageID);
                    foreach (var k in cust.Childs)
                        Console.WriteLine(" The child is {0}", k.ChildID);
                }
            }
        }
    }
#if true
    class TestRead
    {
        static void Main(string[] args)
        {
            ReadInputXML rxml = new ReadInputXML();
            DBEngine<string, PackageElement> db = new DBEngine<string, PackageElement>();

            List<PackageElement> CL;
            CL = rxml.ReadInput(db, "..\\..\\packagedetails_input.xml");
        }
    }
#endif



}