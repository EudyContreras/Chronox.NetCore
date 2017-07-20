﻿using Chronox;
using Chronox.Scanners;
using Chronox.Utilities.Extenssions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Chronox.Interfaces;

namespace Tests
{
    class DetectionTest
    {
        ChronoxSettings settings;

        /*Things to note
         * - if no time specify any date which differes in month or day or year will be set at 12am
         * - if a month is specified without any specified day the day will be set to the first
         * - if a proceding or preciding week is specified wihout any specified day the day will be set to monday both for previous or following weeks
         * - time expressions are constant: noon = 12:00pm morning = 6:00am etc
         * - expressions such as fourth week of x month will be parsed on intervals of week starting with day where monday falls
         * - expressions such as next month if no day specified the day will be set to the first
         * - expressions such as in 4 months if no day specified the dayofweek will be left the same
         * */

         string[] TestedAndWorking =
         {
            /*
             "4pm",
             "4:00",
             "7:00",
             "14:00",
             "17:00",
             "1800s",
             "night", //9pm constant that maybe the users can define
             "today", 
             "dec 25",
             "oct 06", //06 parsed as month !!
             "Sunday", 
             "05/2003",
             "10 to 8",
             "3 hours",
             "4 weeks", 
             "4:00:00",
             "at noon", //1 pm constant that maybe the user can define
             "jan 1st",
             "sat 7am",
             "the day",
             "tonight", //current day at 9:pm constant that maybe the user can define
             "4:00 a.m",
             "may 27th",
             "mon 2:35",
             "november", 
             "thursday",
             "tomorrow",
             "10 past 2",
             "10 past 8",
             "4pm today",
             "afternoon",
             "day after",
             "half to 2",
             "January 5",
             "last week", 
             "next week", 
             "right now",
             "this year",
             "yesterday", 
             "10/24/1979", //Middle endian
             "2017/10/23", //Big endian
             "23-10-2017", //litten endian
             "3 jan 2000",
             "a week ago",
             "a year ago",
             "christmass",
             "friday 1pm",
             "in 3 hours",
             "in 3 weeks",
             "in 4 years",
             "jan 3 2010",
             "last night",
             "next month",
             "1 oclock pm",
             "2 hours ago",
             "3 years ago",
             "31 december",
             "7 hours ago",
             "at midnight",
             "december 31",
             "half past 2",
             "Jan 21, '97",
             "quater to 2",
             "Sun, Nov 21",
             "this second",
             "1 week hence",
             "22nd of june",
             "4pm tomorrow",
             "5:00pm yesterday",
             "5th may 2017",
             "fifth of may",
             "friday 13:00",
             "next january",
             "October 2006",
             "this morning",
             "this tuesday",
             "4pm on monday",
             "december 2017",
             "december 31st",
             "last december",
             "last february",
             "last thursday",
             "next december",
             "next february",
             "next thursday",
             "one oclock pm",
             "one thirty am",
             "third of june",
             "this november",
             "this thursday",
             "tomorrow noon",
             "monday the 3rd",         
             "the day before",
             "3 days from now", 
             "4 saturdays ago",
             "3 saturdays after",
             "3 mondays ago",
             "3 mondays after at 6:39:30 PM",
             "next weekend",
             "last weekend",
             "the weekend",
             "the weekend after",
             "the weekend before",
             "4th of jan 2000",
             "5 fridays after",
             "7 days from now",
             "june the twelth",
             "quater past 2pm",
             "tomorrow at ten",
             "tonight at 10pm",
             "within the hour",
             "2 hours from now",
             "3 fridays before",
             "31 december 2017",
             "31st of december",
             "4 tuesdays after",
             "4 weeks from now",
             "4pm next tuesday",
             "5 hours from now",
             "6 in the morning",
             "Fri, 21 Nov 1997",
             "independence day",
             "monday next week",
             "monday the third",
             "Sunday next week",
             "the monday after",
             "tomorrow at 10am",
             "tomorrow at 10pm",
             "tomorrow evening",
             "tomorrow morning",
             "2 tuesdays before",
             "4 tuesdays after",
             "2 mondays after",
             "the monday after",
             "the monday before", //Parsed as the monday of the week before not the current !!
             "one monday before", //Parsed as the monday of the current week if the current day is not monday!!
             "three mondays after",
             "3 days from today",
             "5 days from today",
             "6 tuesday morning",
             "december 31, 2017",
             "evening yesterday",
             "february 14, 2004",
             "fifth of may 2017",
             "last week tuesday",
             "one thirty two pm",
             "tuesday last week",
             "yesterday at 4:00",
             "12 months from now",
             "17 of april of '85",
             "2 fridays from now",
             "2 sundays from now",
             "3 mondays from now",
             "4 weeks from today",
             "5 hours before now",
             "5 minutes from now",
             "8pm in the evening",
             "day after tomorrow",
             "december 31st 2017",
             "sunday november 26",
             "the 3 of June 2017",
             "the tuesday before",
             "the twelth of june",
             "third of june 2017",
             "thursday last week",
             "tomorrow at 6:45pm",
             "4th day last week",
             "10-23-2017 at 10 pm",
             "2 hours before monday", // The upcoming monday or the monday of the current week ? Parsed as the monday of the current week            
             "4 hours to sunday",
             "5 hours after tuesday",
             "2 hours before noon",
             "2 hours to midnight",
             "2017-10-23 at 10 pm",
             "24-10-2010 10:20 AM",
             "5 months before now",
             "afternoon yesterday",
             "february 14th, 2004",
             "four weeks from now",
             "last sunday evening",
             "next monday evening",
             "three days from now",
             "yesterday afternoon",
             "4 days to next year",
             "2 days from tomorrow",
             "2017/10/22, 10:20 PM",
             "5 days from tomorrow",
             "january twenty-third",
             "last friday at 20:00",
             "last week on tuesday",
             "sat 7 in the evening",
             "Sun, Nov 2nd of 1990",
             "sunday november 26th",
             "twenty third of june",
             "within the next hour",
             "2 hours from midnight",
             "2 hours past midnight",
             "3 hours past midnight",
             "31st of december 2017",
             "4 days from yesterday",
             "5 minutes to midnight
             "5 minutes to tomorrow",
             "8pm on monday evening",
             "February twenty first",
             "next monday the third",
             "the day before sunday",
             "the third day of july",",
             "1 day before next week",
             "4 days after next week",
             "february twenty-eighth",
             "Fourth day in November",
             "saturday at 20:40.0000",
             "the day after tomorrow",
             "in 3 months on the first friday",
             "first friday in 3 months",
             "five weeks ago on saturday",
             "ten months ago on saturday at 6pm",
             "4 weeks ago on saturday",
             "saturday 4 weeks ago",
             "4 months ago on saturday at 6pm",
             "saturday 4 months ago at 6pm",
             "the monday after the next",
             "the tuesday after the next",
             "the monday before the next",
             "the monday before the previous",
             "the wednesday before the next",
             "the wednesday before the previous",
             "the wednesday after the previous",
             "the wednesday after this",
             "the monday after this one",
             "the sunday before the previous",
             "the sunday before the next",
             "the day after the next",
             "the week after the next",
             "the day before the next",
             "the week before the previous",
             "the day before the previous",
             "the month after the next",
             "first friday in two months",
             "the thursday before the previous",
             "first monday in two months",
             "second friday in two months",
             "second friday in one month",
             "third friday in two years",
             "second monday in three months",
             "third week in December",
             "3 days before next week",
             "4 days after next week",
             "1 hours before midnight",
             "2 hours before tomorrow",
             "3:45 on tuesday morning",
             "5 minutes from tomorrow",
             "7 hours before tomorrow",
             "the day after next week",
             "the second day of march",
             "the second week of july",
             "tomorrow during the day",
             "10 minutes past midnight",
             "4 days before next week",
             "4 days before next month",
             "4 days after next month",
             "4 days before next year",
             "last friday of next year",
             "last monday of the month",
             "the day before yesterday",
             "the week after next week",
             "3:45 on tuesday afternoon",
             "first friday of this year",
             "the fourth day of the next week",
             "january twenty-third 2017",
             "monday the 3 of June 2017",
             "next sat 7 in the evening",
             "the day before next month",
             "the third day of the week",
             "this sat 7 in the morning",
             "twenty third of june 2017",
             "fifth of may 2017 at 20:00",
             "second friday of next year",
             "second monday of the month",
             "sunday november 26 in 2017",
             "the 3 of June 2017 at 10pm",*/
             "the day after next tuesday",
             "the day before next tuesday",
             "the fourth day of the year",
             "the last day of next month",
             "the third day of next week",
             "the week third of december",
             "third saturday of the year",
             "third Thursday in November",
             "monday 10:30 in the morning",
             "monday the 3rd of June 2017",
             "next week on monday morning",
             "the day before next tuesday",
             "the first day of next month",
             "the third week of last year",
             "the third week of next year",
             "third of june 2017 at 10 PM",
             "3rd of December 2022 at 10pm",
             "December 3rd of 2022 at 10pm",
             "sunday november 26th in 2017",
             "the 3rd of June 2017 at 10pm",
             "the first week of last month",
             "the second week of last month",
             "the second week of this month",
             "the first week of this month",
             "The 25 April in the year 2008",
             "the second week of next month",
             "first friday of the next month",
             "last week on tuesday afternoon",
             "the day before yesterday at 10",
             "independence day during the day",
             "last day of the following month",
             "next week on thursday afternoon",
             "the third week of next february",
             "this sat the 7th in the evening",
             "3 months ago saturday at 5:00 pm",
             "first day of the following month",
             "twenty second day of the following month",
             "Friday the 21st of November 1997",
             "Sun, Nov 2nd of 1990 at 10:30 pm",
             "The 22nd of May in the year 2010",
             "independence day during the night",
             "July, 15 of 2014 10:30:20.1000 PM",
             "next saturday 7:00 in the evening",
             "second day of the following month",
             "this saturday at 7 in the evening",
             "first monday of the previous month",
             "The 25th of April in the year 2008",
             "the twenty sixth day of next month",
             "monday the 3rd of June 2017 at 20pm",
             "second monday of the previous month",
             "the 31 of december of the year 2017",
             "last monday the first in the morning",
             "the 31st of december of the year 2017",
             "the following week on wednesday night",
             "may seventh '97 at three in the morning",
             "The 21 of April in the year 2008 at 10 pm",
             "first friday of the following month at noon",
             "last monday of the previous month at midnight",
             "monday the 3rd of June of the year 2017 at 20pm",
             "the 31 of december of the year 2017 at 10:31 pm",
             "The thirtieth  of April in the year 2008 at 10 pm",
             "the thirty first of december of the year 2017 at 12 am",
             "fourteenth of june 2010 at eleven o'clock in the evening"
         };

         string[] ProblematicNeedsFixing =
         {
              
             /* exact timing checks */
             "3 hour after next tuesday",
             "6 hours before next monday",
             "in five minutes and six hour",
             "in five minutes, six hours and 20 seconds",
             "in six weeks and 3 days",
             "in 3 days and 4 hours and 30 seconds"

             /*To add support:
              * at sunset
              * at sunrise              
              * etc
              * */

    };

        public DetectionTest(ChronoxSettings settings)
        {
            this.settings = settings;
        }
         
        public void TryDectect()
        {
            //var count = TestedAndWorking.Length;

            var parser = ChronoxParser.GetInstance(new ChronoxSettings("english"));


            foreach (var format in TestedAndWorking)
            {
                var text = $"I will come visit you {format}";

                //var date = ((ChronoxDateTimeExtraction)parser.Parse(settings, text).Results[0]).GetCurrent().DateTime();

                var date = parser.ParseDateTime(settings, text)[0].GetCurrent().DateTime();

                Console.WriteLine($"{format} | {date}");
                Console.WriteLine();
            }
        }
    }
}
