///////////////////////////////////////////////////////////////
// Controller.cs - define noSQL database                       //
// Ver 1.0                                                  //
// Application: Project2 for CSE681-SMA, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Lenovo Thinkpad T540p, Core-i7, Windows 10            //
// Author:      Ravichandra Malapati, CST 4-187, Syracuse University  //
//              (315) 706-3437, rmalapat@syr.edu            //
// Original Author: Ravichandra Malapati                             //
///////////////////////////////////////////////////////////////
/* Package Operations:
* -------------------
* This is the starter of the project and it contains the calls to every other functionalities
* This code contains the functions for all the test cases that re required to demonstrate all the project requirements.
* This package contains the following interfaces
* 1. private void Test_R1()--"Requirement 1: Demonstrating .Net version"
* 2. private void TestR2()--Requirement 2.1-2.5: Demonstrating implement a generic key/value in-memory database"
* 3. private void TestR3()--("Requirement 3.1: Demonstrating Addidng of key value pairs");
* 4. private void TestR4()--Requirment 4.1 Modifying the key value pair to the existing Database in memory
* 5. private void TestR5()--//Requirment 5 Persisting Database
* 6. private void TestR6()--//Requirement 6 Persisting database after fixed time limit
* 7. private void TestR7_1()--////Requirement 7.3 Query The set of all keys matching a specified pattern which defaults to all keys
* 8. private void TestR7_2()--//// All keys that contain a specified string
* 9. private void TestR8()--////Requirement 8 Query The set of all keys matching a specified pattern which defaults to all keys
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
using System.Threading;
namespace Project2Starter
{
    class Controller
    {
        private static List<PackageElement> customerList_live;
        DBEngine<string, PackageElement> database_Live = new DBEngine<string, PackageElement>();
        //Loading the NoSQL Database from previously saved XML
        private void TestR1()
        {
            //Requirement 1
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 1: Demonstrating .Net version");
            Console.WriteLine("Version: {0}", Environment.Version.ToString());
            Console.WriteLine("=============================================================");
            //Loading the NoSQL Database from previously saved XML

            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 5.1 : Loading database from the existing persist file userDetail_persist.xml");
            Console.WriteLine("Please wait while the Database is loading from XML: Package_details_persist.xml");
            Console.WriteLine("=============================================================");


        }
        //Loading the NoSQL Database from previously saved XML
        private void TestR2()
        {
            LoadXMLDB load_Input_Xml = new LoadXMLDB();
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 2.1: Demonstrating implement a generic key/value in-memory database \n ");

            database_Live = load_Input_Xml.LoadXML2Object(database_Live, "..\\..\\Package_details_persist.xml");
            Console.WriteLine("Loaded the Database from XML successfully \n");
            Console.WriteLine("the Database is live");
            PackageElement cust = new PackageElement();
            database_Live.getValue("1", out cust);
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 2.2: A name string and text desciption in the metadata \n");
            Console.WriteLine("priting name string from Key Value : Key is {0}, Name is {1}, Description is ", cust.PackageID, cust.Name, cust.Description);
            Console.WriteLine("============================================================= \n ");
            Console.WriteLine("Requirement 2.3: A Time Date string in the metadata");
            Console.WriteLine("priting TimeDate string from Key Value : Key is {0}, Time is is {1}", cust.PackageID, cust.Time);
            Console.WriteLine("============================================================= \n");
            Console.WriteLine("Requirement 2.4: A child relationships with the key in the metadata \n");
            List<Child> child_test;
            child_test = cust.Childs.ToList();
            for (int i = 0; i < child_test.Count; i++)
                Console.WriteLine("priting childs of the Key Value pair : Key is {0}, one of the childs is {1}", cust.PackageID, child_test[i].ChildID);
            //Reading the input form XML provided by the user
            ReadInputXML read_Input_xml = new ReadInputXML();
            Console.WriteLine("=============================================================");
            Console.WriteLine("Please wait while the Database is reading input from XML: KeyValue pairs.xml");
            customerList_live = read_Input_xml.ReadInput(database_Live, "..\\..\\packagedetails_input.xml");
            string xml_string = read_Input_xml.ConvertObjectToXMLString(customerList_live);
            XElement xElement = XElement.Parse(xml_string);
            xElement.Save(@"F:\dump\userDetail3.xml");
        }
        //Requirment 3.1 Adding the key value pair to the existing Database in memory
        private void TestR3()
        {
            ReadInputXML read_Input_xml = new ReadInputXML();
            //Requirment 3.1 Adding the key value pair to the existing Database in memory
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 3.1: Demonstrating Addidng of key value pairs");
            Console.WriteLine("Please provide input at Controller//Addition.xml");
            customerList_live = read_Input_xml.ReadInput(database_Live, "..\\..\\Addition.xml");
            Console.WriteLine("Querying the Database for Added element 100");
            PackageElement cust2 = new PackageElement();
            bool check = database_Live.getValue("100", out cust2);
            if (check)
                Console.WriteLine("The custId is {0}", cust2.PackageID);
            else
                Console.WriteLine("The cust id does not exist");
            //Requirment 3.2 Deleting the key value pair to the existing Database in memory
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 3.2: Demonstrating Deletion of key value pairs");
            PackageElement cust3 = new PackageElement();
            Console.WriteLine("Before Deletiing the Key 100");
            bool check1 = database_Live.getValue("100", out cust3);
            if (check1)
                Console.WriteLine("The custId is {0}", cust3.PackageID);
            else
                Console.WriteLine("The cust id does not exist");
            Console.WriteLine("Please provide input at Controller\\Deletion.xml");
            customerList_live = read_Input_xml.ReadInput(database_Live, "..\\..\\Deletion.xml");
            Console.WriteLine("Querying the Database for key 100 After Deleting");
            check1 = false;
            check1 = database_Live.getValue("100", out cust3);
            if (check1)
                Console.WriteLine("The custId is {0}", cust3.PackageID);
            else
                Console.WriteLine("The cust id does not exist");
        }
        //Requirment 4.1 Modifying the key value pair to the existing Database in memory
        private void TestR4()
        {
            List<Child> child_test;
            PackageElement cust3 = new PackageElement();
            ReadInputXML read_Input_xml = new ReadInputXML();
            //Requirment 4.1 Modifying the key value pair to the existing Database in memory
            Console.WriteLine("Please provide input at ..\\..\\Modify.xml \n\n");
            Console.WriteLine("Before Modifying the Key 101");
            bool check2 = database_Live.getValue("101", out cust3);
            if (check2)
            {
                Console.WriteLine("The custId is {0}", cust3.PackageID);
                Console.WriteLine("The Desciption is {0}", cust3.Description);
                Console.WriteLine("The Name is {0}", cust3.Name);
                Console.WriteLine("The Author is {0}", cust3.Author);
                Console.WriteLine("The Country is {0}", cust3.Country);
                Console.WriteLine("The Time is {0}", cust3.Time);
                Console.WriteLine("The Payload is {0}", cust3.Payload);
                child_test = cust3.Childs.ToList();
                for (int i = 0; i < child_test.Count; i++)
                    Console.WriteLine("priting childs of the Key Value pair : Key is {0}, one of the childs is {1}", cust3.PackageID, child_test[i].ChildID);
            }
            else
                Console.WriteLine("The cust id does not exist");
            customerList_live = read_Input_xml.ReadInput(database_Live, "..\\..\\Modify.xml");
            Console.WriteLine("Querying the Database for key 101 After Modifying");
            check2 = false;
            check2 = database_Live.getValue("101", out cust3);
            if (check2)
            {
                Console.WriteLine("The custId is {0}", cust3.PackageID);
                Console.WriteLine("The Desciption is {0}", cust3.Description);
                Console.WriteLine("The Name is {0}", cust3.Name);
                Console.WriteLine("The Author is {0}", cust3.Author);
                Console.WriteLine("The Country is {0}", cust3.Country);
                Console.WriteLine("The Time is {0}", cust3.Time);
                Console.WriteLine("The Payload is {0}", cust3.Payload);
                child_test = cust3.Childs.ToList();
                for (int i = 0; i < child_test.Count; i++)
                    Console.WriteLine("priting childs of the Key Value pair : Key is {0}, one of the childs is {1}", cust3.PackageID, child_test[i].ChildID);
            }
            else
                Console.WriteLine("The cust id does not exist");
        }
        //Requirment 5 Persisting Database
        private void TestR5()
        {
            //Requirment 5 Persisting Database
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 5.2: Persisting Database to XML file");
            Console.WriteLine("Persisting the database to userDetail_persist.xml");
            string input_path = "..\\..Package_details_persist.xml";
            database_Live.PersistDB(input_path);
        }
        private void TestR6()
        {
            //Requirement 6 Persisting database after scheduling time limit
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 6: Persisting Database to XML file");
            Console.WriteLine("Persisting the database to userDetail_persist.xml");
            Console.WriteLine("The accepted time interval is 10 seconds");
            //Thread.Sleep(10000);
            string input_path = "..\\..\\Package_details_persist.xml";
            database_Live.PersistDB(input_path);
        }
        //Requirement 7.1: Query for value of a specified key
        private void TestR7_1()
        {
            List<Child> child_test;
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 7.1: Query for value of a specified key");
            Console.WriteLine("The specified Key is 101, the key can be either from XML or from user input");
            PackageElement cust4 = new PackageElement();
            bool status = database_Live.getValue("101", out cust4);
            if (status == true)
            {
                Console.WriteLine("The custId is {0}", cust4.PackageID);
                Console.WriteLine("The Desciption is {0}", cust4.Description);
                Console.WriteLine("The Name is {0}", cust4.Name);
                Console.WriteLine("The Author is {0}", cust4.Author);
                Console.WriteLine("The Country is {0}", cust4.Country);
                Console.WriteLine("The Time is {0}", cust4.Time);
                Console.WriteLine("The Payload is {0}", cust4.Payload);
                child_test = cust4.Childs.ToList();
                for (int i = 0; i < child_test.Count; i++)
                    Console.WriteLine("priting childs of the Key Value pair : Key is {0}, one of the childs is {1}", cust4.PackageID, child_test[i].ChildID);
            }
            else
                Console.WriteLine("The cust id does not exist");
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 7.2 Query the children of a specified key");
            status = database_Live.getValue("101", out cust4);
            if (status == true)
            {
                Console.WriteLine("The custId is {0}", cust4.PackageID);
                child_test = cust4.Childs.ToList();
                for (int i = 0; i < child_test.Count; i++)
                    Console.WriteLine("priting childs of the Key Value pair : Key is {0}, one of the childs is {1}", cust4.PackageID, child_test[i].ChildID);
            }
            else
                Console.WriteLine("The cust id does not exist");
        }
        //Requirement 7.3 Query The set of all keys matching a specified pattern which defaults to all keys
        private void TestR7_2()
        {
            //Requirement 7.3 Query The set of all keys matching a specified pattern which defaults to all keys
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 7.3 The set of all keys matching a specified pattern which defaults to all keys.");
            Console.WriteLine("The pattern we are searching is '1'");
            Console.WriteLine("Please wait while the database searching for keys that contains character or number '1' ");
            bool default_or_nodata;
            default_or_nodata = false;
            database_Live.getSetKeys("1", default_or_nodata);
            Console.WriteLine("Now we are not passing any pattern and hence the query should return all keys");
            default_or_nodata = true;
            database_Live.getSetKeys("0", default_or_nodata);
            //Requirement 7.4 All keys that contain a specified string in their metadata section.
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 7.4 All keys that contain a specified string in their metadata section.");
            Console.WriteLine("The pattern we are searching is 'Degapudi'");
            Console.WriteLine("Please wait while the database searching for keys that contains word 'Degapudi' ");
            PackageElement cust_var = new PackageElement();
            database_Live.getKeyswithStringValues("Degapudi");
            //Requirement 7.5 All keys that contain values written within a specified time-date interval.
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 7.5 All keys that contain values written within a specified time-date interval");
            DateTime time_input = new DateTime(2009, 8, 1, 0, 0, 0);
            Console.WriteLine("The time we are searching for is {0}", time_input);
            Console.WriteLine("Please wait while the database searching for keys that has time stamp is greater than the input time");           
            database_Live.getKeysWithTimeStampRange(time_input);

        }
        //Requirement 8 Query The set of all keys matching a specified pattern which defaults to all keys
        private void TestR8()
        {
            //Requirement 8 Query The set of all keys matching a specified pattern which defaults to all keys
            Console.WriteLine("=============================================================");
            Console.WriteLine("Requirement 8 the creation of a new immutable database constructed from the result of any query that returns a collection of keys");
            Console.WriteLine("Creating immutable database for query searching for keys that contains character or number '1' ");
            database_Live.createImmutableDatabase("1");
        }
        //Test main function
        static void Main(string[] args)
        {
            Controller TestExecutive = new Controller();
            TestExecutive.TestR1();
            TestExecutive.TestR2();
            TestExecutive.TestR3();
            TestExecutive.TestR4();
            TestExecutive.TestR5();
            TestExecutive.TestR6();
            TestExecutive.TestR7_1();
            TestExecutive.TestR7_2();
            TestExecutive.TestR8();
        }
    }
}
