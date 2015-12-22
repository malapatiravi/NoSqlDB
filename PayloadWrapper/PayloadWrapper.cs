///////////////////////////////////////////////////////////////
// PayloadWrapper.cs - Wrapper supports cloning, ToString()  //
// Ver 1.0                                                   //
// Application: Demonstration for CSE687-OOD, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Dell XPS2700, Core-i7, Windows 10            //
// Author:      Jim Fawcett, CST 4-187, Syracuse University  //
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package shows how to write a wrapper class for the
 * DBElement's payload that provides cloning, ToString(), 
 * and ToXml() where that is possible, e.g., we have access 
 * to all the members.
 *
 * The package also demonstrates how to clone and test
 * the entire DBElement.  This should eventually be pulled
 * into an extension method for DBElements.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   PayloadWrapper, DBExtensions.cs, UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 24 Sep 15
 * - first release
 *
 */
 //ToDo: try removing the Key generic parameter.  I don't think it's begin used
 //ToDo: Test PayloadWrapper.Equals

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project2Starter
{
  public abstract class PayloadWrapper<key, Data> where Data : new()
  {
    public Data theWrappedData { get; set; } = new Data();
    abstract public PayloadWrapper<key, Data> clone();
    public abstract override bool Equals(object obj);
    public abstract override int GetHashCode();
    override abstract public string ToString();
    abstract public string ToXml();
  }

  public class ListOfStrings : PayloadWrapper<string, List<string>>
  {
    public override PayloadWrapper<string, List<string>> clone()
    {
      ListOfStrings los = new ListOfStrings();
      los.theWrappedData = new List<string>();
      foreach (string item in theWrappedData)
        los.theWrappedData.Add(item);
      return los;
    }
    public override bool Equals(object obj)
    {
      PayloadWrapper<string, List<string>> plw = obj as PayloadWrapper<string, List<string>>;
      if (theWrappedData.Count() != plw.theWrappedData.Count())
        return false;
      for (int i = 0; i < theWrappedData.Count(); ++i)
        if (theWrappedData[i] != plw.theWrappedData[i])
          return false;
      return true;
    }
    public override int GetHashCode()
    {
      return theWrappedData.GetHashCode();
    }
    public override string ToString()
    {
      StringBuilder accum = new StringBuilder();
      bool first = true;
      foreach (string item in theWrappedData)
      {
        if (first)
        {
          accum.Append(string.Format(" {0}", item));
          first = false;
        }
        else
        {
          accum.Append(string.Format(", {0}", item));
        }
      }
      accum.Append("\n");
      return accum.ToString();
    }
    public override string ToXml()
    {
      StringBuilder accum = new StringBuilder();
      accum.Append("<payload>");
      foreach (string item in theWrappedData)
        accum.Append(string.Format("\n  <item>{0}</item>", item));
      accum.Append("\n</payload>");
      return accum.ToString();
    }
  }
  class TestPayLoadWrapper
  {
    static void showEquality(object obj1, object obj2, string msg)
    {
      Write("\n  {0}", msg);
      if (obj1.Equals(obj2))  // same state
        Write("\n    objects have same state");
      else
        Write("\n    objects do not have same state");

      if (ReferenceEquals(obj1, obj2)) // same reference value, e.g., same object
        Write("\n    both are references to same object");
      else
        Write("\n    different objects");
    }
    static void showDateTimeEquality(DateTime obj1, DateTime obj2, string msg)
    {
      Write("\n  {0}", msg);
      if (obj1.ToString() == obj2.ToString())  // same state
        Write("\n    DateTimes have same state");
      else
        Write("\n    DateTimes do not have same state");

      if (ReferenceEquals(obj1, obj2)) // same reference value, e.g., same object
        Write("\n    both are references to same object");
      else
        Write("\n    different objects");
    }
    static void showListEquality<T>(IEnumerable<T> obj1, IEnumerable<T> obj2, string msg)
    {
      Write("\n  {0}", msg);
      if (obj1.SequenceEqual(obj2))  // same state
        Write("\n    Lists have same state");
      else
        Write("\n    Lists do not have same state");

      if (ReferenceEquals(obj1, obj2)) // same reference value, e.g., same object
        Write("\n    both are references to same object");
      else
        Write("\n    different objects");
    }
    static void Main(string[] args)
    {
      "Testing Payload Wrapper".title('=');
      WriteLine();

      ListOfStrings los = new ListOfStrings();
      los.theWrappedData.AddRange(new List<string> { "one", "two", "three" });

      "showing List of strings:".title();
      foreach (string item in los.theWrappedData)
        Write("\n  {0}", item);
      WriteLine();

      "Output from PayloadWrapper.ToString()".title();
      Write("\n{0}", los.ToString());
      //WriteLine();

      "output from PayloadWrapper.ToXml()".title();
      Write("\n{0}", los.ToXml());
      WriteLine();

      "output from elem.showElement<string, ListOfStrings>()".title();
      DBElement<string, ListOfStrings> elem = new DBElement<string, ListOfStrings>();
      elem.name = "test element";
      elem.descr = "created to test PayloadWrapper";
      elem.payload = new ListOfStrings();
      elem.payload.theWrappedData = new List<string> { "alpha", "beta", "gamma", "delta", "epsilon" };
      Write(elem.showElement<string, ListOfStrings>());

      /////////////////////////////////////////////////////////////////////
      // The sequence of statements below should be turned into an
      // extension method: static public void clone(this List<string>)
      //
      "test cloning elem".title('=');
      WriteLine();
      DBElement<string, ListOfStrings> cloned = new DBElement<string, ListOfStrings>();
      cloned.name = String.Copy(elem.name);
      cloned.descr = String.Copy(elem.descr);
      cloned.timeStamp = DateTime.Parse(elem.timeStamp.ToString());
      cloned.children = new List<string>();
      cloned.children.AddRange(elem.children);
      cloned.payload = elem.payload.clone() as ListOfStrings;
      "output from elem.showElement<string, ListOfStrings>()".title();
      Write(elem.showElement<string, ListOfStrings>());
      "output from cloned.showElement<string, ListOfStrings>()".title();
      Write(cloned.showElement<string, ListOfStrings>());

      //"Hashcodes are not reliable object identifiers".title();
      //Write(string.Format("\n  hashcode of elem           = {0}", elem.GetHashCode()));
      //Write(string.Format("\n  hashcode of elem.name      = {0}", elem.name.GetHashCode()));
      //Write(string.Format("\n  hashcode of elem.descr     = {0}", elem.descr.GetHashCode()));
      //Write(string.Format("\n  hashcode of elem.payload   = {0}", elem.payload.GetHashCode()));
      //WriteLine();

      //Write(string.Format("\n  hashcode of cloned         = {0}", cloned.GetHashCode()));
      //Write(string.Format("\n  hashcode of cloned.name    = {0}", cloned.name.GetHashCode()));
      //Write(string.Format("\n  hashcode of cloned.descr   = {0}", cloned.descr.GetHashCode()));
      //Write(string.Format("\n  hashcode of cloned.payload = {0}", cloned.payload.GetHashCode()));
      //WriteLine();

      "testing for equality immediately after cloning".title();
      showEquality(cloned.name, elem.name, "cloned.name vs. elem.name");
      showEquality(cloned.descr, elem.descr, "cloned.descr vs. elem.descr");
      showDateTimeEquality(cloned.timeStamp, elem.timeStamp, "cloned.timeStamp vs. elem.timeStamp");
      showListEquality(cloned.children, elem.children, "cloned.children vs. elem.children");
      showListEquality<string>(cloned.payload.theWrappedData, elem.payload.theWrappedData, "cloned.payload vs. elem.payload");
      WriteLine();

      "modifying clone's properties".title();
      WriteLine();
      cloned.name = cloned.name + " modified";
      cloned.descr = cloned.descr + " modified";
      cloned.timeStamp = DateTime.Today;
      cloned.children.AddRange(new List<string> { "key11", "key13" });
      cloned.payload.theWrappedData.AddRange(new List<string> { "zeta", "eta" });
      "output from elem.showElement<string, ListOfStrings>()".title();
      Write(elem.showElement<string, ListOfStrings>());
      "output from cloned.showElement<string, ListOfStrings>()".title();
      Write(cloned.showElement<string, ListOfStrings>());

      "Testing equality after modifying clone's values".title();
      showEquality(cloned.name, elem.name, "cloned.name vs. elem.name");
      showEquality(cloned.descr, elem.descr, "cloned.descr vs. elem.descr");
      showDateTimeEquality(cloned.timeStamp, elem.timeStamp, "cloned.timeStamp vs. elem.timeStamp");
      showListEquality(cloned.children, elem.children, "cloned.children vs. elem.children");
      showListEquality<string>(cloned.payload.theWrappedData, elem.payload.theWrappedData, "cloned.payload vs. elem.payload");

      Write("\n\n");
    }
  }
}
