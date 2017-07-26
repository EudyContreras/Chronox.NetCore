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

            var settings = new ChronoxSettings
            {
                PrefferedLanguages = new string[]{ "English"}
            };

            var detectionTest = new DetectionTest(settings);

            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();

            Console.ReadKey();
        }
    }
}
