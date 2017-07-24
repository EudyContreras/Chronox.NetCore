using Chronox;
using Chronox.Converters;
using Chronox.Handlers;
using Chronox.Helpers;
using Chronox.Helpers.Interpreters;
using Chronox.Helpers.Patterns;
using Chronox.Resolutions.Resolvers;
using Chronox.Scanners;
using Chronox.Utilities;
using Chronox.Utilities.Extenssions;
using Tests;
using System;
using System.Collections.Generic;
using Chronox.Constants;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronox.Debugers
{
    class Program
    {

        static void Main(string[] args)
        {
            
            var settings = new ChronoxSettings();

            var detectionTest = new DetectionTest(settings);

            detectionTest.TestDateTimeParsing();
            

            //File.WriteAllLines("TimeZoneInfoFixed.txt", newLines);


            /*Ways to express Time Zones
            /* 
             * - 10:15 pm GMT+0000 (GMT)
             * - 10:15 pm GMT+0900 (JST)
             * - 10:15 pm +0500
             * - 10:15 pm +05:00
             * - 10:15 pm UTC-05:00
             * - 10:15 pm EST
             * - 
             */

            /*
            Console.WriteLine("Count: " + Definitions.Converters.TIMEZONE_OFFSETS.Count);
            Console.WriteLine("Local TimeZone ID: "+ TimeZoneInfo.Local.Id);
            Console.WriteLine("Local TimeZone Name: " +TimeZoneInfo.Local.DisplayName);
            Console.WriteLine("Local Date UTC Offset: "+TimeZoneInfo.Local.GetUtcOffset(DateTime.Now));
            Console.WriteLine("Local Date: " + DateTime.Now);
            Console.WriteLine("New York Time: " +ChronoxDateTimeUtility.AddOffset(DateTime.Now, Definitions.Converters.TIMEZONE_OFFSETS["EDT"].Offset));
            Console.WriteLine("Australia Time: " + ChronoxDateTimeUtility.AddOffset(DateTime.Now, Definitions.Converters.TIMEZONE_OFFSETS["AEST"].Offset));
            */

            
            Console.ReadKey();
        }
    }
}
