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

namespace Chronox.Debugers
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new ChronoxSettings("english");

            var detectionTest = new DetectionTest(settings);
            detectionTest.TryDectect();

            Console.ReadKey();
        }
    }
}
 