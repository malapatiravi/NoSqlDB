///////////////////////////////////////////////////////////////
// LoadXMLDB.cs - define noSQL database                       //
// Ver 1.1                                                  //
// Application: Project2 for CSE681-SMA, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Lenovo Thinkpad T540p, Core-i7, Windows 10            //
// Author:      Ravichandra Malapati, CST 4-187, Syracuse University  //
//              (315) 706-3437, rmalapat@syr.edu            //
//Initial Release 07-Oct-2015//
// Original Author: Ravichandra Malapati                             //
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
// LoadXMLDB.cs - define noSQL database                       //
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
 * This package implements LoadXMLDB
 *Load XML to DB is used to read the persisted database XML file from previous instance of Database
 *The XML persist can be scheduled based on the input from user
 *The input can be number of writes or time In this project we used default 2-3 seconds of time for persisting
 *The database is loaded after each restart of the database system
 *1. Insert
 *
 *The class has  the following interfaces
 * 1. public List<PackageElement> LoadXML2Object(DBEngine<int, PackageElement> db, String input_path)
 *      The read input takes database instance and input path for the XML as parameters
 *      This function reads the input from XML parses it and calles the QueryEngine to Insert,Modify, Delete or child Query
 *      The return type of this function is a DBEngine<int, PackageElement>  elements
 *      The in memory database is provided as a parameter with the path of the database XML file to load
 *
 */
/*Maintenance History:
* --------------------
* ver 1.0 : 07 Oct 15
* - first release
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace Project2Starter
{
    public class LoadXMLDB

    {
        private static List<PackageElement> customerList;
        /* this function converts the inut XML file to the DBEngine key value */
        public DBEngine<string, PackageElement> LoadXML2Object(DBEngine<string, PackageElement> db, string input_path)
        {
            

            customerList = (
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
            Console.WriteLine("Loading persisted database from ..\\...\\Package_details_persist.xml .........Please wait");
            for (var i = 0; i < customerList.Count; i++)
            {

                if (customerList[i].Query == "Insert")
                {
                    db.insert(customerList[i].PackageID, customerList[i]);
                    Console.WriteLine("Loading Database Key :{0} .....Please wait ", customerList[i].PackageID);
                    Thread.Sleep(2000);
                }
            }
            
            db.PersistDB(input_path);
            
            return db;
        }
    }
#if true
    class LoadXMLDBTest
    {
        static void Main(string[] args)
        {
            LoadXMLDB load_xml_test = new LoadXMLDB();
            DBEngine<string, PackageElement> db_test = new DBEngine<string, PackageElement>();
            load_xml_test.LoadXML2Object(db_test, "..\\..\\Package_details_persist.xml" );

        }
    }
#endif


}
