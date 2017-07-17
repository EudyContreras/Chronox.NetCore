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

namespace Chronox.Debugers
{
    class Program
    {
        static void Main(string[] args)
        {
            ChronoxOption options = new ChronoxOption("english");
            

            DetectionTest detectionTest = new DetectionTest(options);

            detectionTest.TryDectect();

            Console.ReadKey();
        }
    }
}
 