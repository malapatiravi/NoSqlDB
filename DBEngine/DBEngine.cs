
///////////////////////////////////////////////////////////////
// DBEngine.cs - define noSQL database                       //
// Ver 1.2                                                   //
// Application: Project2 for CSE681-SMA, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Lenovo Thinkpad T540p, Core-i7, Windows 10            //
// Author:      Ravichandra Malapati, CST 4-187, Syracuse University  //
//              (315) 706-3437, rmalapat@syr.edu            //
// Original Author: Dr. Fawcett                             //
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
// DBEngine.cs - define noSQL database                       //
// Ver 1.2                                                   //
// Application: Demonstration for CSE687-OOD, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Dell XPS2700, Core-i7, Windows 10            //
// Author:      Jim Fawcett, CST 4-187, Syracuse University  //
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package implements DBEngine<Key, Value> where Value
 * is the DBElement<key, Data> type.
 *
 * This class is a starter for the DBEngine package you need to create.
 * It doesn't implement many of the requirements for the db, e.g.,
 * It doesn't remove elements, doesn't persist to XML, doesn't retrieve
 * elements from an XML file, and it doesn't provide hook methods
 * for scheduled persistance.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs, and
 *                 UtilityExtensions.cs only if you enable the test stub
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.2 : 24 Sep 15
 * - removed extensions methods and tests in test stub
 * - testing is now done in DBEngineTest.cs to avoid circular references
 * ver 1.1 : 15 Sep 15
 * - fixed a casting bug in one of the extension methods
 * ver 1.0 : 08 Sep 15
 * - first release
 *
 */
//todo add placeholders for Shard
//todo add reference to class text XML content

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Threading;

namespace Project2Starter
{
    public class DBEngine<Key, Value>
    {
        private Dictionary<Key, Value> dbStore;
        public DBEngine()
        {
            dbStore = new Dictionary<Key, Value>();
        }
        /*To insert data into the database*/
        public bool insert(Key key, Value val)
        {
            if (dbStore.Keys.Contains(key))
            {
                Console.WriteLine("The database already contains the key {0} you are trying to insert, Please use modify", key);
                return false;
            }
            else
            {
                Console.WriteLine("Please wait while the key {0} is being inserted in database ", key);
                dbStore[key] = val;
                Console.WriteLine("key {0} is inserted", key);
                return true;
            }

        }
        /*To get the value from the database*/
        public bool getValue(Key key, out Value val)
        {
            if (dbStore.Keys.Contains(key))
            {
                val = dbStore[key];
                return true;
            }
            else
            {
                Console.WriteLine("The database doesnot contain the key {0} you are requesting", key);
                val = default(Value);
                return false;
            }

        }
        /*To delete the data from the database*/
        public bool Delete(Key key)
        {
            if (dbStore.Keys.Contains(key))
            {
                dbStore.Remove(key);
                Console.WriteLine("The Key {0} is removed succesfully from the database", key);
                return true;
            }
            else
            {
                Console.WriteLine("The key is not present in database, could you please recheck and Query");
                return false;
            }

        }
        /*To query the childs of a key data into the database*/
        public Value ChildQuery(Key key)
        {
            if (dbStore.Keys.Contains(key))
            {
                Console.WriteLine("Please wait while the key {0} is being Queried from database ", key);
                Value val = dbStore[key];
                return val;
            }
            else
            {
                Console.WriteLine("The database doesnot contain the key {0} you are requesting", key);
                Value val = default(Value);
                return val;
            }

        }
        /*To modify the values and metadata of a key data into the database*/
        public bool Modify(Key key, Value val)
        {
            if (dbStore.Keys.Contains(key))
            {
                Console.WriteLine("Please wait while the key {0} is being modified in the database ", key);
                dbStore[key] = val;
                return true;
            }
            else
            {
                Console.WriteLine("The database doesnot contain the key {0} you are requesting", key);
                return false;
            }

        }
        /*To persist the database into XML */
        public void PersistDB(string input_path)
        {
            string xmlString = null;
            var fields = dbStore.Values.ToList();
            Console.WriteLine("Persisting The Database..... Please wait");
            Thread.Sleep(2000);
            XmlSerializer xmlSerializer = new XmlSerializer(fields.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, fields);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            XElement xElement = XElement.Parse(xmlString);
            xElement.Save(input_path);
        }
        /*To Query a set of keys based on the patter matching*/
        public void getSetKeys(Key key1, bool default_or_nodata)
        {
            PackageElement item = new PackageElement();
            foreach (var i in dbStore.Keys)
            {
                if (default_or_nodata)
                {
                    Value v = dbStore[i];
                    item = v as PackageElement;
                    Console.WriteLine("The key is: {0}", item.PackageID);
                }
                else if (i.ToString().Contains(key1.ToString()))
                {
                    Value v = dbStore[i];
                    item = v as PackageElement;
                    Console.WriteLine("The key is: {0}", item.PackageID);
                }
            }
        }
        /*To Query a set of keys based on the patter matching in Values either in metadata or payload*/
        public void getKeyswithStringValues(string value_input)
        {
            PackageElement item = new PackageElement();
            foreach (var i in dbStore.Keys)
            {
                Value v = dbStore[i];
                item = v as PackageElement;
                if (item.Author.Contains(value_input) || item.Description.Contains(value_input) || item.Query.Contains(value_input)
                    || item.Name.Contains(value_input) || item.Country.Contains(value_input) || item.Payload.Contains(value_input))
                    Console.WriteLine("The customer Id is: {0}", item.PackageID);
            }
        }
        /*to Query the keys whose date time stamps are in a range*/
        public void getKeysWithTimeStampRange(DateTime time_Input)
        {
            PackageElement cust_item = new PackageElement();
            foreach (var i in dbStore.Keys)
            {
                Value val = dbStore[i];
                cust_item = val as PackageElement;
                int result_compare = DateTime.Compare(time_Input, cust_item.Time);
                if (result_compare <= 0)
                    Console.WriteLine("time stamp of the key {0} is between the input time stamp  which is {1} and present time which is {2}", cust_item.PackageID, time_Input, cust_item.Time);
            }
        }
        /*to create a immutable database*/
        public void createImmutableDatabase(Key key1)
        {
            var desiredResults = new Dictionary<Key, string>();

            Console.WriteLine("Creating immutable database... and will be persisted to");
            PackageElement item = new PackageElement();
            foreach (var i in dbStore.Keys)
            {
                if (i.ToString().Contains(key1.ToString()))
                {
                    Value v = dbStore[i];
                    item = v as PackageElement;
                    Console.WriteLine("The key is: {0}", item.PackageID);
                    desiredResults.Add(i, item.Name);
                    Console.WriteLine("Adding key {0} to immutable database: ", item.PackageID);
                    Console.WriteLine("The database can not be edited and it is immutable database");
                    Console.ReadKey();
                }
            }
        }
        public IEnumerable<Key> Keys()
        {
            return dbStore.Keys;
        }
        /*
         * More functions to implement here
         */
    }

#if (TEST_DBENGINE)

    class TestDBEngine
    {
        static void Main(string[] args)
        {
            "Testing DBEngine Package".title('=');
            WriteLine();

            Write("\n  All testing of DBEngine class moved to DBEngineTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");
            Write("\n\n");
        }
    }
#endif
}
