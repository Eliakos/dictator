﻿using System;
using System.Collections.Generic;
using fastJSON;
using Dictator;

namespace Dictator.ConsoleTests
{
    public static class Examples
    {
        public static void GeneralUsage()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "foo string value")
                .Int("bar", 12345)
                .String("embedded.foo", "embedded foo string value");
            
            // document object would look in JSON representation as follows:
            // {
            //     "foo": "foo string value",
            //     "bar": 12345,
            //     "embedded": {
            //         "foo": "embedded foo string value"
            //      }
            // }
                
            if (document.IsString("foo") && document.IsInt("bar") && document.IsString("embedded.foo"))
            {
                var foo = document.String("foo");
                var bar = document.Int("bar");
                var embeddedFoo = document.String("embedded.foo");
                
                Console.WriteLine("{0}, {1}, {2}", foo, bar, embeddedFoo);
            }
        }
        
        public static void SimpleSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "string value")
                .Int("bar", 123)
                .Long("longBar", 12345)
                .Object("null", null);
            
            // standard get operations
            var str = document.String("foo");
            var number = document.Int("bar");
            var longNumber = document.Long("longBar");
            var nullObject = document.Object("null");
            
            // values are automatically converted if possible
            var strNumber = document.String("longBar");
            var intNumber = document.Int("longBar");
            
            // generic get operation
            var bar = document.Object<int>("bar");
            
            Console.WriteLine(bar);
        }
        
        public static void DateTimeSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                // stored as native DateTime object
                .DateTime("dateTime1", DateTime.UtcNow)
                // stored in default yyyy-MM-ddTHH:mm:ss.fffZ string format
                .DateTime("dateTime2", DateTime.UtcNow, DateTimeFormat.String)
                // stored in unix timestamp format as long type
                .DateTime("dateTime3", DateTime.UtcNow, DateTimeFormat.UnixTimeStamp);
            
            // standard get operations
            var dateTime = document.DateTime("dateTime1");
            var dateTimeAsString = document.String("dateTime2");
            var dateTimeAsLong = document.Long("dateTime3");
            
            // automatic conversion
            var stringConvertedToDateTime = document.DateTime("dateTime2");
            var longConvertedToDateTime = document.DateTime("dateTime3");
            
            Console.WriteLine(dateTime.ToString());
            Console.WriteLine(dateTimeAsString);
            Console.WriteLine(dateTimeAsLong);
            Console.WriteLine(stringConvertedToDateTime.ToString());
            Console.WriteLine(longConvertedToDateTime.ToString());
        }
        
        public static void EnumSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                // stored as native enum
                .Enum("enum1", DateTimeFormat.Object)
                // stored as integer
                .Enum("enum2", DateTimeFormat.Object, EnumFormat.Integer)
                // stored as string
                .Enum("enum3", DateTimeFormat.Object, EnumFormat.String);
            
            // standard get operation
            var enum1 = document.Enum<DateTimeFormat>("enum1");
            // retrieve enum from integer value
            var enum2 = document.Enum<DateTimeFormat>("enum2");
            // retrieve enum from string value
            var enum3 = document.Enum<DateTimeFormat>("enum3");
            
            Console.WriteLine(enum1.ToString());
            Console.WriteLine(enum2.ToString());
            Console.WriteLine(enum3.ToString());
        }
        
        public static void ListSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                .List("list1", new List<int>() { 1, 2, 3 });
            
            var list1 = document.List<int>("list1");
            var list1Size = document.ListSize("list1");
            
            list1.ForEach(x => Console.WriteLine(x));
            
            Console.WriteLine(list1Size);
        }
        
        public static void NestedSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                .String("string1", "string value 1")
                .String("foo.string2", "string value 2")
                .String("foo.bar.string3", "string value 3");
            
            // JSON equivalent:
            // {
            //     "string1": "string value 1",
            //     "foo": {
            //         "string2": "string value 2",
            //         "bar": {
            //             "string3": "string value 3"
            //         }
            //     }
            // }
            
            var string1 = document.String("string1");
            var string2 = document.String("foo.string2");
            var string3 = document.String("foo.bar.string3");
            
            Console.WriteLine(string1);
            Console.WriteLine(string2);
            Console.WriteLine(string3);
        }
    }
}