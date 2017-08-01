using Tests;
using System;
using Chronox.Utilities.Extenssions;
using System.Collections.Generic;
using Chronox.Handlers;

namespace Chronox.Debugers
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var detectionTest = new DetectionTest(new ChronoxSettings("English", "Spanish"));

            /*
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            detectionTest.TestDateTimeParsing();
            
            detectionTest.PerformTest("Book me a flight for friday at 10pm GMT-09:30 please");
            detectionTest.PerformTest("Book me a flight for tommorow at 10pm UTC+0500 please");
            detectionTest.PerformTest("Book me a flight for today at 10pm -03:30 please");
            detectionTest.PerformTest("Book me a flight for friday at 10pm ANAT please");
            detectionTest.PerformTest("Book me a flight for sunday at 10 PM please");
            detectionTest.PerformTest("Book me a flight for friday at 10 in the evening please");
            detectionTest.PerformTest("Book me a flight for tuesday at 10 in the evening UTC please");
            
            detectionTest.PerformTest("I will visit you the third day of this week eight pm in the evening");
                  
            detectionTest.PerformTest("I will visit you the third day of this week eight pm in the evening");

            detectionTest.PerformTest("Book me a flight for next week on friday at 10:30 UTC +03:00, please do not forget");
            
            detectionTest.PerformTest("I will be there the thirty-first of december in the evening, independence day and the twenty second of june in the afternoon");
            
            detectionTest.PerformTest("I will be there the 21st of september at 10:30pm, the 23rd of june 2030 at 5 in the afternoon and on the thirty-first of december in the evening");
            
            detectionTest.PerformTest("I will be there the 21st of september at 10:30pm EST, the 23rd of june 2030 at 5 in the afternoon ANAT and on the thirty-first of december in the evening");
        
            ////////////////////////////// SPANISH /////////////////////////////////////////////////////////

            detectionTest.PerformTest("te voy a visitar el lunes de la semana pasada");

            detectionTest.PerformTest("te voy a visitar el tercero dia de esta semana a las ocho pm de la anocheser");

            detectionTest.PerformTest("te voy a visitar la proxima semana en el mediodia");
            */

            //detectionTest.TestDateTimeParsing();
            detectionTest.ProcessChronoxExpression();
           

            Console.ReadKey();
        }
    }
}
