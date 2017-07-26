using Chronox;
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
         * - if no time specify any date which differs in month, day or year will be set at 12am
         * - if a month is specified without any specified day the day will be set to the first
         * - if a proceding or preciding week is specified wihout any specified day the day will be set to monday both for previous or following weeks
         * - time expressions are constant: noon = 12:00pm morning = 6:00am etc
         * - expressions such as fourth week of x month will be parsed on intervals of week starting with day where monday falls
         * - expressions such as next month if no day specified the day will be set to the first
         * - expressions such as in 4 months if no day specified the dayofweek will be left the same
         * */


        /*Ways to express Time Zones

        /* 
         * - 10:15 pm GMT+0000 (GMT)
         * - 10:15 pm GMT+0900 (JST)
         * - 10:15 pm +0500
         * - 10:15 pm +05:00
         * - 10:15 pm UTC-05:00
         * - 10:15 pm EST
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

        private static string[] ProblematicNeedsFixing =
        {

             /*To add support:
              * at sunset
              * at sunrise              
              * etc
              * */

        };

        private static DateTime Reference = new DateTime(year: 2017, month: 07, day: 20, hour: 14, minute: 30, second: 00);

        private static readonly Dictionary<string, DateTime> TestData = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase)
        {

            { "4pm", DateTime.Parse("7/20/2017, 16:00:00") },

            { "17:00", DateTime.Parse("7/20/2017, 17:00:00") },

            { "1800s", DateTime.Parse("1/1/1800, 00:00:00") },

            { "night", DateTime.Parse("7/20/2017, 21:00:00") },

            { "today", DateTime.Parse("7/20/2017, 14:30:00") },

            { "dec 25", DateTime.Parse("12/25/2017, 00:00:00") },

            { "Sunday", DateTime.Parse("7/23/2017, 00:00:00") },

            { "05/2003", DateTime.Parse("05/1/2003, 00:00:00") },

            { "10 to 8", DateTime.Parse("7/20/2017, 7:50:00") },

            { "3 hours", DateTime.Parse("7/20/2017, 17:30:00") },

            { "4 weeks", DateTime.Parse("8/17/2017, 00:00:00") },

            { "4:00:20", DateTime.Parse("7/20/2017, 4:00:20") },

            { "at noon", DateTime.Parse("7/20/2017, 12:00:00") },

            { "jan 1st", DateTime.Parse("1/1/2017, 00:00:00") },

            { "sat 7am", DateTime.Parse("7/22/2017, 7:00:00") },

            { "the day", DateTime.Parse("7/20/2017, 6:00:00") },

            { "tonight", DateTime.Parse("7/20/2017, 21:00:00") },

            { "4:00 a.m", DateTime.Parse("7/20/2017, 4:00:00") },

            { "may 27th", DateTime.Parse("5/27/2017, 00:00:00") },

            { "mon 2:35", DateTime.Parse("7/17/2017, 2:35:00") },

            { "november", DateTime.Parse("11/1/2017, 00:00:00") },

            { "thursday", DateTime.Parse("7/27/2017, 00:00:00") },

            { "tomorrow", DateTime.Parse("7/21/2017, 00:00:00") },

            { "ten past two", DateTime.Parse("7/20/2017, 2:10:00") },

            { "4pm today", DateTime.Parse("7/20/2017, 16:00:00") },

            { "afternoon", DateTime.Parse("7/20/2017, 13:00:00") },

            { "day after", DateTime.Parse("7/21/2017, 00:00:00") },

            { "half to 2 PM", DateTime.Parse("7/20/2017, 13:30:00") },

            { "January 5", DateTime.Parse("1/5/2017, 00:00:00") },

            { "last week", DateTime.Parse("7/10/2017, 00:00:00") },

            { "next week", DateTime.Parse("7/24/2017, 00:00:00") },

            { "right now", DateTime.Parse("7/20/2017, 14:30:00") },

            { "this year", DateTime.Parse("1/1/2017, 00:00:00") },

            { "yesterday", DateTime.Parse("7/19/2017, 00:00:00") },

            { "10/24/1979", DateTime.Parse("10/24/1979, 00:00:00") },

            { "2017/10/23", DateTime.Parse("10/23/2017, 00:00:00") },

            { "23-10-2017", DateTime.Parse("10/23/2017, 00:00:00") },

            { "3 jan 2000", DateTime.Parse("1/3/2000, 00:00:00") },

            { "a week ago", DateTime.Parse("7/13/2017, 00:00:00") },

            { "a year ago", DateTime.Parse("7/20/2016, 00:00:00") },

            { "christmass", DateTime.Parse("12/25/2017, 00:00:00") },

            { "friday 1pm", DateTime.Parse("7/21/2017, 13:00:00") },

            { "in 3 hours", DateTime.Parse("7/20/2017, 17:30:00") },

            { "in 3 weeks", DateTime.Parse("8/10/2017, 00:00:00") },

            { "in 4 years", DateTime.Parse("7/20/2021, 00:00:00") },

            { "jan 3 2010", DateTime.Parse("1/3/2010, 00:00:00") },

            { "last night", DateTime.Parse("7/19/2017, 21:00:00") },

            { "next month", DateTime.Parse("8/1/2017, 00:00:00") },

            { "1 oclock pm", DateTime.Parse("7/20/2017, 13:00:00") },

            { "3 years ago", DateTime.Parse("7/20/2014, 00:00:00") },

            { "7 hours ago", DateTime.Parse("7/20/2017, 7:30:00") },

            { "at midnight", DateTime.Parse("7/21/2017, 00:00:00") },

            { "half past 2", DateTime.Parse("7/20/2017, 2:30:00") },

            { "Jan 21, '97", DateTime.Parse("1/21/1997, 00:00:00") },

            { "Sept 5th, '06", DateTime.Parse("9/5/2006, 00:00:00") }, // Limited support

            { "quater to 2", DateTime.Parse("7/20/2017, 1:45:00") },

            { "Sun, Nov 21", DateTime.Parse("11/21/2017, 00:00:00") }, //Should i check if 21st is a Sunday?

            { "this second", DateTime.Parse("7/20/2017, 14:30:00") },

            { "1 week hence", DateTime.Parse("7/27/2017, 00:00:00") },

            { "22nd of june", DateTime.Parse("6/22/2017, 00:00:00") },

            { "4pm tomorrow", DateTime.Parse("7/21/2017, 16:00:00") },

            { "5:00pm yesterday", DateTime.Parse("7/19/2017, 17:00:00") },

            { "fifth of may", DateTime.Parse("5/5/2017, 00:00:00") },

            { "friday 13:00", DateTime.Parse("7/21/2017, 13:00:00") },

            { "next january", DateTime.Parse("1/1/2018, 00:00:00") },

            { "October 2006", DateTime.Parse("10/1/2006, 00:00:00") },

            { "this morning", DateTime.Parse("7/20/2017, 6:00:00") },

            { "this tuesday", DateTime.Parse("7/18/2017, 00:00:00") },

            { "4pm on monday", DateTime.Parse("7/17/2017, 16:00:00") },

            { "december 31st", DateTime.Parse("12/31/2017, 00:00:00") },

            { "last december", DateTime.Parse("12/1/2016, 00:00:00") },

            { "last thursday", DateTime.Parse("7/13/2017, 00:00:00") },

            { "next february", DateTime.Parse("2/1/2018, 00:00:00") },

            { "next thursday", DateTime.Parse("7/27/2017, 00:00:00") },

            { "this november", DateTime.Parse("11/1/2017, 00:00:00") },

            { "this thursday", DateTime.Parse("7/20/2017, 14:30:00") },

            { "tomorrow noon", DateTime.Parse("7/21/2017, 12:00:00") },

            { "the day before", DateTime.Parse("7/19/2017, 00:00:00") },

            { "3 days from now", DateTime.Parse("7/23/2017, 00:00:00") },

            { "4 saturdays ago", DateTime.Parse("6/24/2017, 00:00:00") },

            { "3 saturdays after", DateTime.Parse("8/5/2017, 00:00:00") },

            { "3 mondays ago", DateTime.Parse("7/3/2017, 00:00:00") },

            { "3 mondays after at 6:39:30 PM", DateTime.Parse("8/7/2017, 18:39:30") },

            { "next weekend", DateTime.Parse("7/28/2017, 00:00:00") },

            { "last weekend", DateTime.Parse("7/14/2017, 00:00:00") },

            { "the weekend", DateTime.Parse("7/21/2017, 00:00:00") },

            { "the weekend after", DateTime.Parse("7/28/2017, 00:00:00") },

            { "the weekend before", DateTime.Parse("7/14/2017, 00:00:00") },

            { "4th of jan 2000", DateTime.Parse("1/4/2000, 00:00:00") },

            { "5 fridays after", DateTime.Parse("8/18/2017, 00:00:00") },

            { "7 days from now", DateTime.Parse("7/27/2017, 00:00:00") },

            { "june the twelth", DateTime.Parse("6/12/2017, 00:00:00") },

            { "tomorrow at ten", DateTime.Parse("7/21/2017, 10:00:00") },

            { "tonight at 10pm", DateTime.Parse("7/20/2017, 22:00:00") },

            { "within the hour", DateTime.Parse("7/20/2017, 14:30:00") },

            { "2 hours from now", DateTime.Parse("7/20/2017, 16:30:00") },

            { "3 fridays before", DateTime.Parse("6/30/2017, 00:00:00") },

            { "4 weeks from now", DateTime.Parse("8/17/2017, 00:00:00") },

            { "4pm next tuesday", DateTime.Parse("7/25/2017, 16:00:00") },

            { "5 hours from now", DateTime.Parse("7/20/2017, 19:30:00") },

            { "6 in the morning", DateTime.Parse("7/20/2017, 6:00:00") },

            { "Fri, 21 Nov 1997", DateTime.Parse("11/21/1997, 00:00:00") },//Should I check if the 21st is a friday??

            { "monday the third", DateTime.Parse("7/3/2017, 00:00:00") }, //Should I check if the third is a monday??

            { "Sunday next week", DateTime.Parse("7/30/2017, 00:00:00") },

            { "the monday after", DateTime.Parse("7/24/2017, 00:00:00") },

            { "tomorrow at 10pm", DateTime.Parse("7/21/2017, 22:00:00") },

            { "tomorrow evening", DateTime.Parse("7/21/2017, 18:00:00") },

            { "tomorrow morning", DateTime.Parse("7/21/2017, 6:00:00") },

            { "the monday before", DateTime.Parse("7/10/2017, 00:00:00") },// ??

            { "one monday before", DateTime.Parse("7/17/2017, 00:00:00") },// ??

            { "three mondays after", DateTime.Parse("8/7/2017, 00:00:00") },

            { "5 days from today", DateTime.Parse("7/25/2017, 00:00:00") },

            { "6 tuesday morning", DateTime.Parse("7/18/2017, 6:00:00") },

            { "december 31, 2017", DateTime.Parse("12/31/2017, 00:00:00") },

            { "evening yesterday", DateTime.Parse("7/19/2017, 18:00:00") },

            { "february 14, 2004", DateTime.Parse("2/14/2004, 00:00:00") },

            { "fifth of may 2017", DateTime.Parse("5/5/2017, 00:00:00") },

            { "last week tuesday", DateTime.Parse("7/11/2017, 00:00:00") },

            { "one thirty two pm", DateTime.Parse("7/20/2017, 13:32:00") },

            { "tuesday last week", DateTime.Parse("7/11/2017, 00:00:00") },

            { "yesterday at 4:00", DateTime.Parse("7/19/2017, 4:00:00") },

            { "12 months from now", DateTime.Parse("7/20/2018, 00:00:00") },

            { "17 of april of '85", DateTime.Parse("4/17/1985, 00:00:00") },

            { "2 fridays from now", DateTime.Parse("7/28/2017, 00:00:00") },

            { "3 mondays from now", DateTime.Parse("8/7/2017, 00:00:00") },

            { "4 weeks from today", DateTime.Parse("8/17/2017, 00:00:00") },

            { "5 hours before now", DateTime.Parse("7/20/2017, 9:30:00") },

            { "5 minutes from now", DateTime.Parse("7/20/2017, 14:35:00") },

            { "8pm in the evening", DateTime.Parse("7/20/2017, 20:00:00") },

            { "the 3 of June 2017", DateTime.Parse("6/3/2017, 00:00:00") },

            { "the tuesday before", DateTime.Parse("7/11/2017, 00:00:00") },

            { "the twelth of june", DateTime.Parse("6/12/2017, 00:00:00") },

            { "third of june 2017", DateTime.Parse("6/3/2017, 00:00:00") },

            { "thursday last week", DateTime.Parse("7/13/2017, 00:00:00") },

            { "tomorrow at 6:45pm", DateTime.Parse("7/21/2017, 18:45:00") },

            { "4th day last week", DateTime.Parse("7/13/2017, 00:00:00") },

            { "10-23-2017 at 10 pm", DateTime.Parse("10/23/2017, 22:00:00") },

            { "2 hours before monday", DateTime.Parse("7/16/2017, 22:00:00") },

            { "4 hours to sunday", DateTime.Parse("7/22/2017, 20:00:00") },

            { "5 hours after tuesday", DateTime.Parse("7/18/2017, 5:00:00") },

            { "2 hours before noon", DateTime.Parse("7/20/2017, 10:00:00") },

            { "2 hours to midnight", DateTime.Parse("7/20/2017, 22:00:00") },

            { "2017-10-23 at 10 pm", DateTime.Parse("10/23/2017, 22:00:00") },

            { "24-10-2010 10:20 AM", DateTime.Parse("10/24/2010, 10:20:00") },

            { "5 months before now", DateTime.Parse("2/20/2017, 00:00:00") },

            { "afternoon yesterday", DateTime.Parse("7/19/2017, 13:00:00") },

            { "february 14th, 2004", DateTime.Parse("2/14/2004, 00:00:00") },

            { "four weeks from now", DateTime.Parse("8/17/2017, 00:00:00") },

            { "last sunday evening", DateTime.Parse("7/16/2017, 18:00:00") },

            { "next monday evening", DateTime.Parse("7/24/2017, 18:00:00") },

            { "three days from now", DateTime.Parse("7/23/2017, 00:00:00") },

            { "4 days to next year", DateTime.Parse("12/28/2017, 00:00:00") },

            { "2 days from tomorrow", DateTime.Parse("7/23/2017, 00:00:00") },

            { "2017/10/22, 10:20 PM", DateTime.Parse("10/22/2017, 22:20:00") },

            { "january twenty-third", DateTime.Parse("1/23/2017, 00:00:00") },

            { "last friday at 20:00", DateTime.Parse("7/14/2017, 20:00:00") },

            { "last week on tuesday", DateTime.Parse("7/11/2017, 00:00:00") },

            { "sat 7 in the evening", DateTime.Parse("7/22/2017, 19:00:00") },

            { "Sun, Nov 2nd of 1990", DateTime.Parse("11/2/1990, 00:00:00") },

            { "sunday november 26th", DateTime.Parse("11/26/2017, 00:00:00") }, //Should i check that the 26 is a sunday

            { "twenty third of june", DateTime.Parse("6/23/2017, 00:00:00") },

            { "within the next hour", DateTime.Parse("7/20/2017, 15:30:00") },

            { "2 hours from midnight", DateTime.Parse("7/21/2017, 2:00:00") },

            { "3 hours past midnight", DateTime.Parse("7/21/2017, 3:00:00") },

            { "31st of december 2016", DateTime.Parse("12/31/2016, 00:00:00") },

            { "4 days from yesterday", DateTime.Parse("7/23/2017, 00:00:00") },

            { "5 minutes to midnight", DateTime.Parse("7/20/2017, 23:55:00") },

            { "5 minutes to tomorrow", DateTime.Parse("7/20/2017, 23:55:00") },

            { "8pm on monday evening", DateTime.Parse("7/17/2017, 20:00:00") },

            { "February twenty first", DateTime.Parse("2/21/2017, 00:00:00") },

            { "next monday the third", DateTime.Parse("7/3/2017, 00:00:00") }, //Should I look for a monday that falls on the third?

            { "the day before sunday", DateTime.Parse("7/22/2017, 00:00:00") },

            { "the third day of july", DateTime.Parse("7/3/2017, 00:00:00") },

            { "february twenty-eighth", DateTime.Parse("2/28/2017, 00:00:00") },

            { "Fourth day in November", DateTime.Parse("11/4/2017, 00:00:00") },

            { "saturday at 20:40", DateTime.Parse("7/22/2017, 20:40:00") },

            { "the day after tomorrow", DateTime.Parse("7/22/2017, 00:00:00") },

            { "in 3 months on the first friday", DateTime.Parse("10/6/2017, 00:00:00") },

            { "first friday in 3 months", DateTime.Parse("10/6/2017, 00:00:00") },

            { "4 weeks ago on saturday", DateTime.Parse("6/24/2017, 00:00:00") },

            { "saturday 4 weeks ago", DateTime.Parse("6/24/2017, 00:00:00") },

            { "4 months ago on saturday at 6pm", DateTime.Parse("3/25/2017, 18:00:00") },

            { "saturday 4 months ago at 6pm", DateTime.Parse("3/25/2017, 18:00:00") },

            { "the wednesday before the next", DateTime.Parse("7/19/2017, 00:00:00") },

            { "the wednesday before the previous", DateTime.Parse("7/12/2017, 00:00:00") },

            { "the wednesday after the previous", DateTime.Parse("7/26/2017, 00:00:00") },

            { "the wednesday after this", DateTime.Parse("7/26/2017, 00:00:00") },

            { "the monday after this one", DateTime.Parse("7/24/2017, 00:00:00") },

            { "the sunday before the previous", DateTime.Parse("7/9/2017, 00:00:00") },

            { "the sunday before the next", DateTime.Parse("7/16/2017, 00:00:00") },

            { "the day after the next", DateTime.Parse("7/22/2017, 00:00:00") },

            { "the week after the next", DateTime.Parse("7/31/2017, 00:00:00") },

            { "the day before the next", DateTime.Parse("7/20/2017, 14:30:00") },

            { "the week before the previous", DateTime.Parse("7/3/2017, 00:00:00") },

            { "the day before the previous", DateTime.Parse("7/18/2017, 00:00:00") },

            { "the month after the next", DateTime.Parse("9/1/2017, 00:00:00") },

            { "the thursday before the previous", DateTime.Parse("7/6/2017, 00:00:00") },

            { "first monday in two months", DateTime.Parse("9/4/2017, 00:00:00") },

            { "second friday in two months", DateTime.Parse("9/8/2017, 00:00:00") },

            { "third friday in two years", DateTime.Parse("1/18/2019, 00:00:00") },

            { "third week in December", DateTime.Parse("12/15/2017, 00:00:00") },

            { "3 days before next week", DateTime.Parse("7/21/2017, 00:00:00") },

            { "4 days after next week", DateTime.Parse("7/28/2017, 00:00:00") },

            { "1 hours before midnight", DateTime.Parse("7/20/2017, 23:00:00") },

            { "2 hours before tomorrow", DateTime.Parse("7/20/2017, 22:00:00") },

            { "3:45 on tuesday morning", DateTime.Parse("7/18/2017, 3:45:00") },

            { "5 minutes from tomorrow", DateTime.Parse("7/21/2017, 00:05:00") },

            { "7 hours before tomorrow", DateTime.Parse("7/20/2017, 17:00:00") },

            { "the day after next week", DateTime.Parse("7/25/2017, 00:00:00") },

            { "the second day of march", DateTime.Parse("3/2/2017, 00:00:00") },

            { "the second week of july", DateTime.Parse("7/8/2017, 00:00:00") },

            { "tomorrow during the day", DateTime.Parse("7/21/2017, 6:00:00") },

            { "10 min past midnight", DateTime.Parse("7/21/2017, 00:10:00") },

            { "4 days before next week", DateTime.Parse("7/20/2017, 14:30:00") },

            { "4 days before next month", DateTime.Parse("7/28/2017, 00:00:00") },

            { "4 days after next month", DateTime.Parse("8/5/2017, 00:00:00") }, //Does not count the first

            { "4 days before next year", DateTime.Parse("12/28/2017, 00:00:00") },

            { "last friday of next year", DateTime.Parse("12/28/2018, 00:00:00") },

            { "last monday of the month", DateTime.Parse("7/31/2017, 00:00:00") },

            { "the day before yesterday", DateTime.Parse("7/18/2017, 00:00:00") },

            { "the week after next week", DateTime.Parse("7/31/2017, 00:00:00") },

            { "3:45 on tuesday afternoon", DateTime.Parse("7/18/2017, 15:45:00") },

            { "first friday of this year", DateTime.Parse("1/6/2017, 00:00:00") },

            { "the fourth day of the next week", DateTime.Parse("7/27/2017, 00:00:00") },

            { "january twenty-third 2017", DateTime.Parse("1/23/2017, 00:00:00") },

            { "monday the 3 of June 2017", DateTime.Parse("6/3/2017, 00:00:00") },

            { "next sat 7 in the evening", DateTime.Parse("7/29/2017, 19:00:00") },

            { "the day before next month", DateTime.Parse("7/31/2017, 00:00:00") },

            { "the third day of the week", DateTime.Parse("7/19/2017, 00:00:00") },

            { "this sat 7 in the morning", DateTime.Parse("7/22/2017, 7:00:00") },

            { "twenty third of june 2017", DateTime.Parse("6/23/2017, 00:00:00") },

            { "fifth of may 2016 at 20:00", DateTime.Parse("5/5/2016, 20:00:00") },

            { "second friday of next year", DateTime.Parse("1/12/2018, 00:00:00") },

            { "second monday of the month", DateTime.Parse("7/10/2017, 00:00:00") },

            { "sunday november 26 in 2017", DateTime.Parse("11/26/2017, 00:00:00") },

            { "the 3 of June 2017 at 10pm", DateTime.Parse("6/3/2017, 22:00:00") },

            { "in 5 years and 3 months", DateTime.Parse("10/20/2022, 00:00:00") },

            { "in five minutes and six hour", DateTime.Parse("7/20/2017, 20:35:00") },

            { "in five minutes, six hours and 20 seconds", DateTime.Parse("7/20/2017, 20:35:20") },

            { "in six weeks and 3 days", DateTime.Parse("9/3/2017, 00:00:00") },

            { "2 months, 4 week and 6 day ago", DateTime.Parse("4/16/2017, 00:00:00") },

            { "4 years and 6 days from now", DateTime.Parse("7/26/2021, 00:00:00") },

            { "in 3 days and 4 hours and 30 seconds", DateTime.Parse("7/23/2017, 18:30:30") },

            { "the day after next tuesday", DateTime.Parse("7/26/2017, 00:00:00") },

            { "the fourth day of the year", DateTime.Parse("1/4/2017, 00:00:00") },

            { "the last day of next month", DateTime.Parse("8/31/2017, 00:00:00") },

            { "the third day of next week", DateTime.Parse("7/26/2017, 00:00:00") },

            { "the week third of december", DateTime.Parse("12/15/2017, 00:00:00") },

            { "third saturday of the year", DateTime.Parse("1/21/2017, 00:00:00") },

            { "third Thursday in November", DateTime.Parse("11/16/2017, 00:00:00") },

            { "monday 10:30 in the morning", DateTime.Parse("7/17/2017, 10:30:00") },

            { "monday the 3rd of June 2017", DateTime.Parse("6/3/2017, 00:00:00") }, //Should I make sure the 3rd is a monday or ignore!

            { "next week on monday morning", DateTime.Parse("7/24/2017, 6:00:00") },

            { "the day before next tuesday", DateTime.Parse("7/24/2017, 00:00:00") },

            { "the first day of next month", DateTime.Parse("8/1/2017, 00:00:00") },

            { "the third week of next year", DateTime.Parse("1/15/2018, 00:00:00") },

            { "third of june 2017 at 11 PM", DateTime.Parse("6/3/2017, 23:00:00") },

            { "3rd of December 2020 at 10pm", DateTime.Parse("12/3/2020, 22:00:00") },

            { "December 3rd of 2022 at 12pm", DateTime.Parse("12/3/2022, 12:00:00") },

            { "sunday november 26th in 2017", DateTime.Parse("11/26/2017, 00:00:00") }, //Should I make sure the 26th is a monday or ignore!

            { "the 3rd of June 2017 at 10pm", DateTime.Parse("6/3/2017, 22:00:00") },

            { "the first week of last month", DateTime.Parse("6/1/2017, 00:00:00") }, //Should the week start on monday maybe?

            { "the second week of last month", DateTime.Parse("6/8/2017, 00:00:00") }, //Should the week start on monday maybe?

            { "the first week of this month", DateTime.Parse("7/1/2017, 00:00:00") }, //Should the week start on monday maybe?

            { "The 25 september in the year 2008", DateTime.Parse("9/25/2008, 00:00:00") },

            { "first friday of the next month", DateTime.Parse("8/4/2017, 00:00:00") },

            { "last week on tuesday afternoon", DateTime.Parse("7/11/2017, 13:00:00") },

            { "the day before yesterday at 10", DateTime.Parse("7/18/2017, 10:00:00") },

            { "independence day during the day", DateTime.Parse("7/4/2017, 6:00:00") },

            { "last day of the following month", DateTime.Parse("8/31/2017, 00:00:00") },

            { "next week on thursday afternoon", DateTime.Parse("7/27/2017, 13:00:00") },

            { "the third week of next february", DateTime.Parse("2/15/2018, 00:00:00") }, //Check if that should land on a monday

            { "this sat the 7th in the evening", DateTime.Parse("7/7/2017, 18:00:00") },

            { "3 months ago saturday at 5:00 pm", DateTime.Parse("4/22/2017, 17:00:00") },

            { "third day of the following month", DateTime.Parse("8/3/2017, 00:00:00") },

            { "twenty second day of the following month", DateTime.Parse("8/22/2017, 00:00:00") },

            { "Friday the 21st of November 1997", DateTime.Parse("11/21/1997, 00:00:00") },

            { "Sun, Nov 2nd of 1990 at 10:30 pm", DateTime.Parse("11/2/1990, 22:30:00") },

            { "The 22nd of march in the year 2010", DateTime.Parse("3/22/2010, 00:00:00") },

            { "independence day during the night", DateTime.Parse("7/4/2017, 21:00:00") },

            { "July, 15 of 2014 10:30:20.1000 PM", DateTime.Parse("7/15/2014, 22:30:20") },

            { "next saturday 7:00 in the evening", DateTime.Parse("7/29/2017, 19:00:00") },

            { "second day of the following month", DateTime.Parse("8/2/2017, 00:00:00") },

            { "this saturday at 7 in the evening", DateTime.Parse("7/22/2017, 19:00:00") },

            { "The 25th of April in the year 2008", DateTime.Parse("4/25/2008, 00:00:00") },

            { "the twenty sixth day of next month", DateTime.Parse("8/26/2017, 00:00:00") },

            { "monday the 3rd of June 2017 at 20pm", DateTime.Parse("6/3/2017, 20:00:00") },

            { "second monday of the previous month", DateTime.Parse("6/12/2017, 00:00:00") },

            { "last monday the first in the morning", DateTime.Parse("7/1/2017, 6:00:00") },

            { "the 31st of december of the year 2018", DateTime.Parse("12/31/2018, 00:00:00") },

            { "the following week on wednesday night", DateTime.Parse("7/26/2017, 21:00:00") },

            { "may seventh '97 at three in the morning", DateTime.Parse("5/7/1997, 3:00:00") },

            { "The 21 of April in the year 2008 at 10 pm", DateTime.Parse("4/21/2008, 22:00:00") },

            { "first friday of the following month at noon", DateTime.Parse("8/4/2017, 12:00:00") },

            { "last monday of the previous month at midnight", DateTime.Parse("6/26/2017, 00:00:00") },

            { "monday the 3rd of June of the year 2017 at 20pm", DateTime.Parse("6/3/2017, 20:00:00") },

            { "the 31 of december of the year 2017 at 10:31 pm", DateTime.Parse("12/31/2017, 22:31:00") },

            { "The thirtieth of April in the year 2008 at 10 pm", DateTime.Parse("4/30/2008, 22:00:00") },

            { "the thirty first of december of the year 2017 at 12 am", DateTime.Parse("12/31/2017, 00:00:00") },

            { "fourteenth of june 2010 at eleven o'clock in the evening", DateTime.Parse("6/14/2010, 23:00:00") }
         };

        public DetectionTest(ChronoxSettings settings)
        {
            this.settings = settings;
        }

        public void TestDateTimeParsing()
        {
            var chronox = ChronoxParser.GetInstance(settings);

            var allPassed = true;

            foreach (var expression in TestData.Keys)
            {
                var text = $"I will come and visit you on {expression}";

                var result = chronox.ParseDateTime(Reference, text);

                var date = result?[0].DateTime.ToDateTime();

                AreEqual(expression, TestData[expression].ToString(), date.ToString(), ref allPassed);
            }

            if (allPassed)
            {
                Console.WriteLine("All passed!");
            }
        }

        public void TryDectect()
        {
            var chronox = ChronoxParser.GetInstance(settings);

            foreach (var expression in TestData.Keys)
            {
                var result = chronox.ParseDateTime(Reference, expression);

                var date = result?[0].DateTime.ToDateTime();

                Console.WriteLine($"{expression} | {date}");
                Console.WriteLine();
            }
        }

        public void PerformTest(string expression)
        {
            var chronox = ChronoxParser.GetInstance(settings);

            var result = chronox.ParseDateTime(expression);

            var date = result?[0].DateTime.ToDateTime();

            Console.WriteLine($"{expression} | {date} | {result?[0]?.TimeZone}");
            Console.WriteLine();
        }

        private void AreEqual(string expression, string expected, string actual, ref bool allPassed)
        {
            if (string.Compare(expected, actual) != 0)
            {
                Console.WriteLine(string.Concat("Expression: ",expression, "  FAILED!"));
                Console.WriteLine(string.Concat("Expected: ", expected));
                Console.WriteLine(string.Concat("Result: ", actual));
                Console.WriteLine();

                allPassed = false;
            }
        }

        public static void Generate(string[] expressions, DateTime[] ExpectedResults, string path)
        {
            var lines = File.ReadAllLines(path);

            var newLines = new List<string>();

            for (int i = 0; i < expressions.Length; i++)
            {
                foreach (var line in lines)
                {
                    var newLine = line;

                    newLine = newLine.Replace("&", ExpectedResults[i].ToString(), true);
                    newLine = newLine.Replace("#", expressions[i], true);
                    newLine = newLine.Replace("?", i.ToString(), true);

                    newLines.Add(newLine);
                }
            }

            File.AppendAllLines(path, newLines);
        }
    }
}