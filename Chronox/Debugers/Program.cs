using Tests;
using System;
using Chronox.Utilities.Extenssions;
using Chronox.Converters;
using Chronox.Wrappers;
using System.Collections.Generic;
using Chronox.Handlers;
using System.Text;
using Enumerations;
using Chronox.Interfaces;
using Chronox.Handlers.Models;

namespace Chronox.Debugers
{
    class Program
    {
        static void Main(string[] args)
        {
            var languages = new Language[]{
                new Language("English", ChronoxLangSettings.Default)
            };

            var reference = new DateTime(year: 2017, month: 07, day: 20, hour: 14, minute: 30, second: 00);

            var detectionTest = new ParsingTestBench(new ChronoxSettings(languages));

            detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();

            //////////////////////////// ENGLISH /////////////////////////////////////////////////////////

            //detectionTest.TestDateTime("Book me a flight for friday at 10pm GMT-09:30");
            //detectionTest.TestDateTime("Book me a flight for tommorow at 10pm UTC+0500");
            //detectionTest.TestDateTime("Book me a flight for yesterday at 10pm UTC+0500");
            //detectionTest.TestDateTime("Book me a flight for today at 10pm +03:30");
            //detectionTest.TestDateTime("Book me a flight for friday at 10pm ANAT");
            //detectionTest.TestDateTime("Book me a flight for sunday at 10 PM");
            //detectionTest.TestDateTime("Book me a flight for friday at 10 in the evening");
            //detectionTest.TestDateTime("Book me a flight for tuesday at 10 in the evening UTC");
            //detectionTest.TestDateTime("I will visit you the third day of this week at eight pm in the evening");
            //detectionTest.TestDateTime("I was gonna meet her at around 4 in the evening the day after tomorrow");
            //detectionTest.TestDateTime("Book me a flight for next week on friday at 10:30 UTC +03:00");
            //detectionTest.TestDateTime("Meet me the day after tomorrow at eight in the evening ");
            //detectionTest.TestDateTime("I will be there the thirty-first of december in the evening, independence day and the twenty second of june in the afternoon");
            //detectionTest.TestDateTime("I will be there the 21st of september at 10:30pm, the 23rd of june 2030 at 5 in the afternoon and on the thirty-first of december in the evening");
            //detectionTest.TestDateTime("I will be there the 21st of september at 10:30pm EST, the 23rd of june 2030 at 5 in the afternoon ANAT and on the thirty-first of december in the evening");

            //////////////////////////// SPANISH /////////////////////////////////////////////////////////

            //detectionTest.TestDateTime("te voy a visitar el lunes de la semana pasada");
            //detectionTest.TestDateTime("te voy a visitar el tercero dia de esta semana a las ocho pm de la anocheser");
            //detectionTest.TestDateTime("te voy a visitar la proxima semana en el mediodia");

            //detectionTest.TestTimeSpan("I was there for aproximately ten minutes and two seconds");
            //detectionTest.TestTimeSpan("I was there for aproximately five decades"); 
            //detectionTest.TestTimeSpan("I was there for aproximately ten minutes and two seconds");


            //var expression = "ten past two";// "day after at 10:30:20.1000 P.M";

            //Console.WriteLine($"Given reference date: {reference.ToString()}");

            //detectionTest.TestDateTime(reference, $"I will come and visit you on {expression}. so please be ready!");
            //detectionTest.TestDateTime(reference, $"I will come and visit you on {expression}, so please be ready!");

            //detectionTest.ProcessChronoxExpression(null);

            Console.ReadLine();
        }
    }
}
