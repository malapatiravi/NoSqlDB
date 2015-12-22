///////////////////////////////////////////////////////////////
// LoadXMLDB.cs - define noSQL database                       //
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
* This is the element struture of the database the is build using the DBEngine
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
/*
the Batabase elements are defined here
the package element contains the meta data and the key values
The key is Package Id and it is to be provided by the used during entry
*/
namespace Project2Starter
{
    // This is the Child Db elemnt to store the child XML node
    public class Child
    {
        public string ChildID { get; set; }
        public DateTime ChildDate { get; set; }

    }
    // this is the DB elemnt for storing the XML node in the database
    public class PackageElement
    {
        public string PackageID { get; set; }
        public string Description { get; set; }
        public string Query { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Country { get; set; }
        public DateTime Time { get; set; }
        public DateTime Time2 { get; set; }
        public string Payload { get; set; }
        public Child[] Childs { get; set; }
    }
    // This is just a pdefinition of the package elements and can not be tested
    //
#if true
    class PackageElementListTest
    {

        static void Main(string[] args)
        {
            // creating the instance of the the class package element
            PackageElement pl = new PackageElement();
            Child child = new Child();

        }
    }
#endif

}
