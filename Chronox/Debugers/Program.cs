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

            var detectionTest = new DetectionTest(new ChronoxSettings("English"));
            
           
            detectionTest.PerformTest("Book me a flight for friday at 10pm ANAT please");
            detectionTest.PerformTest("Book me a flight for friday at 10pm GMT-09:30 please");
            detectionTest.PerformTest("Book me a flight for tommorow at 10pm UTC+0500 please");
            detectionTest.PerformTest("Book me a flight for today at 10pm -03:30 please"); 
           

            //detectionTest.PerformTest("I will be there the 21st of september at 10:30pm, the 23rd of june 2030 at 5 in the afternoon and on the thirty-first of december in the evening");

            Console.ReadKey();
        }
    }
}
