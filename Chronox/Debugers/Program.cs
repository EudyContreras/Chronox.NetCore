using Tests;
using System;
using Chronox.Utilities.Extenssions;
using Chronox.Converters;
using System.Collections.Generic;
using Chronox.Handlers;
using System.Text;
using Enumerations;

namespace Chronox.Debugers
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var detectionTest = new ParsingTestBench(new ChronoxSettings("English", "Spanish"));

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
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            //detectionTest.TestDateTimeParsing();
            
            //detectionTest.TestDateTime("Book me a flight for friday at 10pm GMT-09:30 please");
            //detectionTest.TestDateTime("Book me a flight for tommorow at 10pm UTC+0500 please");
            //detectionTest.TestDateTime("Book me a flight for today at 10pm -03:30 please");
            //detectionTest.TestDateTime("Book me a flight for friday at 10pm ANAT please");
            //detectionTest.TestDateTime("Book me a flight for sunday at 10 PM please");
            //detectionTest.TestDateTime("Book me a flight for friday at 10 in the evening please");
            //detectionTest.TestDateTime("Book me a flight for tuesday at 10 in the evening UTC please");
            
            //detectionTest.TestDateTime("I will visit you the third day of this week eight pm in the evening");
                  
            //detectionTest.TestDateTime("I will visit you the third day of this week eight pm in the evening");

            //detectionTest.TestDateTime("Book me a flight for next week on friday at 10:30 UTC +03:00, please do not forget");
            
            //detectionTest.TestDateTime("I will be there the thirty-first of december in the evening, independence day and the twenty second of june in the afternoon");
            
            //detectionTest.TestDateTime("I will be there the 21st of september at 10:30pm, the 23rd of june 2030 at 5 in the afternoon and on the thirty-first of december in the evening");
            
            //detectionTest.TestDateTime("I will be there the 21st of september at 10:30pm EST, the 23rd of june 2030 at 5 in the afternoon ANAT and on the thirty-first of december in the evening");
        
            ////////////////////////////// SPANISH /////////////////////////////////////////////////////////

            //detectionTest.TestDateTime("te voy a visitar el lunes de la semana pasada");

            //detectionTest.TestDateTime("te voy a visitar el tercero dia de esta semana a las ocho pm de la anocheser");

            //detectionTest.TestDateTime("te voy a visitar la proxima semana en el mediodia");
           

            //detectionTest.TestTimeSpan("I was there for aproximately ten minutes and two seconds tops");

            //detectionTest.TestTimeSpan("I was there for aproximately five decades"); 
            
            //detectionTest.TestTimeSpan("I was there for aproximately ten minutes and two seconds tops");

            detectionTest.ProcessChronoxExpression(null);


            Console.ReadKey();
        }
    }
}
