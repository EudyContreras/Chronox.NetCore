using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using Chronox.Interfaces;
using Chronox;

namespace Chronox.Tests
{
    [TestClass]
    public class ParsingTest
    {

        private static IChronox Chronox = ChronoxParser.GetInstance(new ChronoxSettings("english"));

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

        private static readonly string[] Expressions = TestData.Keys.ToArray();


        [TestMethod]
        public void TestExample0()
        {
            // Date Expression : 4pm;
			// Expected Result : 7/20/2017 4:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[0]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[0]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample1()
        {
            // Date Expression : 17:00;
			// Expected Result : 7/20/2017 5:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[1]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[1]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample2()
        {
            // Date Expression : 1800s;
			// Expected Result : 1/1/1800 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[2]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[2]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample3()
        {
            // Date Expression : night;
			// Expected Result : 7/20/2017 9:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[3]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[3]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample4()
        {
            // Date Expression : today;
			// Expected Result : 7/20/2017 2:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[4]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[4]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample5()
        {
            // Date Expression : dec 25;
			// Expected Result : 12/25/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[5]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[5]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample6()
        {
            // Date Expression : Sunday;
			// Expected Result : 7/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[6]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[6]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample7()
        {
            // Date Expression : 05/2003;
			// Expected Result : 5/1/2003 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[7]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[7]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample8()
        {
            // Date Expression : 10 to 8;
			// Expected Result : 7/20/2017 7:50:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[8]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[8]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample9()
        {
            // Date Expression : 3 hours;
			// Expected Result : 7/20/2017 5:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[9]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[9]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample10()
        {
            // Date Expression : 4 weeks;
			// Expected Result : 8/17/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[10]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[10]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample11()
        {
            // Date Expression : 4:00:20;
			// Expected Result : 7/20/2017 4:00:20 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[11]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[11]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample12()
        {
            // Date Expression : at noon;
			// Expected Result : 7/20/2017 12:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[12]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[12]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample13()
        {
            // Date Expression : jan 1st;
			// Expected Result : 1/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[13]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[13]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample14()
        {
            // Date Expression : sat 7am;
			// Expected Result : 7/22/2017 7:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[14]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[14]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample15()
        {
            // Date Expression : the day;
			// Expected Result : 7/20/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[15]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[15]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample16()
        {
            // Date Expression : tonight;
			// Expected Result : 7/20/2017 9:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[16]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[16]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample17()
        {
            // Date Expression : 4:00 a.m;
			// Expected Result : 7/20/2017 4:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[17]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[17]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample18()
        {
            // Date Expression : may 27th;
			// Expected Result : 5/27/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[18]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[18]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample19()
        {
            // Date Expression : mon 2:35;
			// Expected Result : 7/17/2017 2:35:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[19]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[19]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample20()
        {
            // Date Expression : november;
			// Expected Result : 11/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[20]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[20]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample21()
        {
            // Date Expression : thursday;
			// Expected Result : 7/27/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[21]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[21]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample22()
        {
            // Date Expression : tomorrow;
			// Expected Result : 7/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[22]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[22]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample23()
        {
            // Date Expression : ten past two;
			// Expected Result : 7/20/2017 2:10:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[23]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[23]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample24()
        {
            // Date Expression : 4pm today;
			// Expected Result : 7/20/2017 4:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[24]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[24]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample25()
        {
            // Date Expression : afternoon;
			// Expected Result : 7/20/2017 1:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[25]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[25]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample26()
        {
            // Date Expression : day after;
			// Expected Result : 7/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[26]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[26]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample27()
        {
            // Date Expression : half to 2 PM;
			// Expected Result : 7/20/2017 1:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[27]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[27]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample28()
        {
            // Date Expression : January 5;
			// Expected Result : 1/5/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[28]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[28]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample29()
        {
            // Date Expression : last week;
			// Expected Result : 7/10/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[29]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[29]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample30()
        {
            // Date Expression : next week;
			// Expected Result : 7/24/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[30]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[30]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample31()
        {
            // Date Expression : right now;
			// Expected Result : 7/20/2017 2:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[31]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[31]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample32()
        {
            // Date Expression : this year;
			// Expected Result : 1/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[32]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[32]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample33()
        {
            // Date Expression : yesterday;
			// Expected Result : 7/19/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[33]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[33]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample34()
        {
            // Date Expression : 10/24/1979;
			// Expected Result : 10/24/1979 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[34]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[34]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample35()
        {
            // Date Expression : 2017/10/23;
			// Expected Result : 10/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[35]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[35]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample36()
        {
            // Date Expression : 23-10-2017;
			// Expected Result : 10/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[36]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[36]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample37()
        {
            // Date Expression : 3 jan 2000;
			// Expected Result : 1/3/2000 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[37]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[37]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample38()
        {
            // Date Expression : a week ago;
			// Expected Result : 7/13/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[38]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[38]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample39()
        {
            // Date Expression : a year ago;
			// Expected Result : 7/20/2016 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[39]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[39]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample40()
        {
            // Date Expression : christmass;
			// Expected Result : 12/25/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[40]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[40]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample41()
        {
            // Date Expression : friday 1pm;
			// Expected Result : 7/21/2017 1:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[41]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[41]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample42()
        {
            // Date Expression : in 3 hours;
			// Expected Result : 7/20/2017 5:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[42]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[42]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample43()
        {
            // Date Expression : in 3 weeks;
			// Expected Result : 8/10/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[43]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[43]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample44()
        {
            // Date Expression : in 4 years;
			// Expected Result : 7/20/2021 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[44]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[44]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample45()
        {
            // Date Expression : jan 3 2010;
			// Expected Result : 1/3/2010 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[45]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[45]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample46()
        {
            // Date Expression : last night;
			// Expected Result : 7/19/2017 9:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[46]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[46]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample47()
        {
            // Date Expression : next month;
			// Expected Result : 8/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[47]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[47]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample48()
        {
            // Date Expression : 1 oclock pm;
			// Expected Result : 7/20/2017 1:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[48]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[48]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample49()
        {
            // Date Expression : 3 years ago;
			// Expected Result : 7/20/2014 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[49]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[49]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample50()
        {
            // Date Expression : 7 hours ago;
			// Expected Result : 7/20/2017 7:30:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[50]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[50]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample51()
        {
            // Date Expression : at midnight;
			// Expected Result : 7/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[51]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[51]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample52()
        {
            // Date Expression : half past 2;
			// Expected Result : 7/20/2017 2:30:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[52]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[52]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample53()
        {
            // Date Expression : Jan 21, '97;
			// Expected Result : 1/21/1997 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[53]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[53]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample54()
        {
            // Date Expression : Sept 5th, '06;
			// Expected Result : 9/5/2006 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[54]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[54]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample55()
        {
            // Date Expression : quater to 2;
			// Expected Result : 7/20/2017 1:45:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[55]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[55]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample56()
        {
            // Date Expression : Sun, Nov 21;
			// Expected Result : 11/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[56]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[56]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample57()
        {
            // Date Expression : this second;
			// Expected Result : 7/20/2017 2:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[57]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[57]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample58()
        {
            // Date Expression : 1 week hence;
			// Expected Result : 7/27/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[58]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[58]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample59()
        {
            // Date Expression : 22nd of june;
			// Expected Result : 6/22/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[59]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[59]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample60()
        {
            // Date Expression : 4pm tomorrow;
			// Expected Result : 7/21/2017 4:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[60]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[60]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample61()
        {
            // Date Expression : 5:00pm yesterday;
			// Expected Result : 7/19/2017 5:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[61]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[61]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample62()
        {
            // Date Expression : fifth of may;
			// Expected Result : 5/5/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[62]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[62]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample63()
        {
            // Date Expression : friday 13:00;
			// Expected Result : 7/21/2017 1:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[63]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[63]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample64()
        {
            // Date Expression : next january;
			// Expected Result : 1/1/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[64]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[64]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample65()
        {
            // Date Expression : October 2006;
			// Expected Result : 10/1/2006 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[65]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[65]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample66()
        {
            // Date Expression : this morning;
			// Expected Result : 7/20/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[66]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[66]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample67()
        {
            // Date Expression : this tuesday;
			// Expected Result : 7/18/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[67]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[67]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample68()
        {
            // Date Expression : 4pm on monday;
			// Expected Result : 7/17/2017 4:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[68]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[68]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample69()
        {
            // Date Expression : december 31st;
			// Expected Result : 12/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[69]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[69]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample70()
        {
            // Date Expression : last december;
			// Expected Result : 12/1/2016 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[70]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[70]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample71()
        {
            // Date Expression : last thursday;
			// Expected Result : 7/13/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[71]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[71]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample72()
        {
            // Date Expression : next february;
			// Expected Result : 2/1/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[72]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[72]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample73()
        {
            // Date Expression : next thursday;
			// Expected Result : 7/27/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[73]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[73]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample74()
        {
            // Date Expression : this november;
			// Expected Result : 11/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[74]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[74]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample75()
        {
            // Date Expression : this thursday;
			// Expected Result : 7/20/2017 2:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[75]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[75]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample76()
        {
            // Date Expression : tomorrow noon;
			// Expected Result : 7/21/2017 12:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[76]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[76]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample77()
        {
            // Date Expression : the day before;
			// Expected Result : 7/19/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[77]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[77]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample78()
        {
            // Date Expression : 3 days from now;
			// Expected Result : 7/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[78]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[78]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample79()
        {
            // Date Expression : 4 saturdays ago;
			// Expected Result : 6/24/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[79]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[79]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample80()
        {
            // Date Expression : 3 saturdays after;
			// Expected Result : 8/5/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[80]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[80]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample81()
        {
            // Date Expression : 3 mondays ago;
			// Expected Result : 7/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[81]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[81]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample82()
        {
            // Date Expression : 3 mondays after at 6:39:30 PM;
			// Expected Result : 8/7/2017 6:39:30 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[82]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[82]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample83()
        {
            // Date Expression : next weekend;
			// Expected Result : 7/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[83]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[83]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample84()
        {
            // Date Expression : last weekend;
			// Expected Result : 7/14/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[84]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[84]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample85()
        {
            // Date Expression : the weekend;
			// Expected Result : 7/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[85]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[85]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample86()
        {
            // Date Expression : the weekend after;
			// Expected Result : 7/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[86]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[86]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample87()
        {
            // Date Expression : the weekend before;
			// Expected Result : 7/14/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[87]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[87]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample88()
        {
            // Date Expression : 4th of jan 2000;
			// Expected Result : 1/4/2000 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[88]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[88]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample89()
        {
            // Date Expression : 5 fridays after;
			// Expected Result : 8/18/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[89]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[89]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample90()
        {
            // Date Expression : 7 days from now;
			// Expected Result : 7/27/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[90]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[90]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample91()
        {
            // Date Expression : june the twelth;
			// Expected Result : 6/12/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[91]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[91]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample92()
        {
            // Date Expression : tomorrow at ten;
			// Expected Result : 7/21/2017 10:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[92]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[92]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample93()
        {
            // Date Expression : tonight at 10pm;
			// Expected Result : 7/20/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[93]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[93]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample94()
        {
            // Date Expression : within the hour;
			// Expected Result : 7/20/2017 2:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[94]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[94]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample95()
        {
            // Date Expression : 2 hours from now;
			// Expected Result : 7/20/2017 4:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[95]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[95]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample96()
        {
            // Date Expression : 3 fridays before;
			// Expected Result : 6/30/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[96]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[96]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample97()
        {
            // Date Expression : 4 weeks from now;
			// Expected Result : 8/17/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[97]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[97]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample98()
        {
            // Date Expression : 4pm next tuesday;
			// Expected Result : 7/25/2017 4:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[98]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[98]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample99()
        {
            // Date Expression : 5 hours from now;
			// Expected Result : 7/20/2017 7:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[99]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[99]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample100()
        {
            // Date Expression : 6 in the morning;
			// Expected Result : 7/20/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[100]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[100]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample101()
        {
            // Date Expression : Fri, 21 Nov 1997;
			// Expected Result : 11/21/1997 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[101]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[101]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample102()
        {
            // Date Expression : monday the third;
			// Expected Result : 7/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[102]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[102]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample103()
        {
            // Date Expression : Sunday next week;
			// Expected Result : 7/30/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[103]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[103]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample104()
        {
            // Date Expression : the monday after;
			// Expected Result : 7/24/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[104]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[104]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample105()
        {
            // Date Expression : tomorrow at 10pm;
			// Expected Result : 7/21/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[105]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[105]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample106()
        {
            // Date Expression : tomorrow evening;
			// Expected Result : 7/21/2017 6:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[106]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[106]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample107()
        {
            // Date Expression : tomorrow morning;
			// Expected Result : 7/21/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[107]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[107]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample108()
        {
            // Date Expression : the monday before;
			// Expected Result : 7/10/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[108]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[108]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample109()
        {
            // Date Expression : one monday before;
			// Expected Result : 7/17/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[109]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[109]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample110()
        {
            // Date Expression : three mondays after;
			// Expected Result : 8/7/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[110]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[110]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample111()
        {
            // Date Expression : 5 days from today;
			// Expected Result : 7/25/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[111]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[111]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample112()
        {
            // Date Expression : 6 tuesday morning;
			// Expected Result : 7/18/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[112]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[112]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample113()
        {
            // Date Expression : december 31, 2017;
			// Expected Result : 12/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[113]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[113]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample114()
        {
            // Date Expression : evening yesterday;
			// Expected Result : 7/19/2017 6:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[114]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[114]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample115()
        {
            // Date Expression : february 14, 2004;
			// Expected Result : 2/14/2004 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[115]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[115]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample116()
        {
            // Date Expression : fifth of may 2017;
			// Expected Result : 5/5/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[116]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[116]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample117()
        {
            // Date Expression : last week tuesday;
			// Expected Result : 7/11/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[117]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[117]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample118()
        {
            // Date Expression : one thirty two pm;
			// Expected Result : 7/20/2017 1:32:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[118]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[118]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample119()
        {
            // Date Expression : tuesday last week;
			// Expected Result : 7/11/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[119]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[119]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample120()
        {
            // Date Expression : yesterday at 4:00;
			// Expected Result : 7/19/2017 4:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[120]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[120]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample121()
        {
            // Date Expression : 12 months from now;
			// Expected Result : 7/20/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[121]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[121]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample122()
        {
            // Date Expression : 17 of april of '85;
			// Expected Result : 4/17/1985 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[122]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[122]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample123()
        {
            // Date Expression : 2 fridays from now;
			// Expected Result : 7/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[123]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[123]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample124()
        {
            // Date Expression : 3 mondays from now;
			// Expected Result : 8/7/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[124]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[124]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample125()
        {
            // Date Expression : 4 weeks from today;
			// Expected Result : 8/17/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[125]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[125]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample126()
        {
            // Date Expression : 5 hours before now;
			// Expected Result : 7/20/2017 9:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[126]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[126]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample127()
        {
            // Date Expression : 5 minutes from now;
			// Expected Result : 7/20/2017 2:35:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[127]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[127]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample128()
        {
            // Date Expression : 8pm in the evening;
			// Expected Result : 7/20/2017 8:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[128]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[128]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample129()
        {
            // Date Expression : the 3 of June 2017;
			// Expected Result : 6/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[129]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[129]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample130()
        {
            // Date Expression : the tuesday before;
			// Expected Result : 7/11/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[130]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[130]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample131()
        {
            // Date Expression : the twelth of june;
			// Expected Result : 6/12/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[131]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[131]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample132()
        {
            // Date Expression : third of june 2017;
			// Expected Result : 6/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[132]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[132]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample133()
        {
            // Date Expression : thursday last week;
			// Expected Result : 7/13/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[133]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[133]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample134()
        {
            // Date Expression : tomorrow at 6:45pm;
			// Expected Result : 7/21/2017 6:45:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[134]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[134]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample135()
        {
            // Date Expression : 4th day last week;
			// Expected Result : 7/13/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[135]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[135]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample136()
        {
            // Date Expression : 10-23-2017 at 10 pm;
			// Expected Result : 10/23/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[136]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[136]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample137()
        {
            // Date Expression : 2 hours before monday;
			// Expected Result : 7/16/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[137]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[137]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample138()
        {
            // Date Expression : 4 hours to sunday;
			// Expected Result : 7/22/2017 8:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[138]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[138]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample139()
        {
            // Date Expression : 5 hours after tuesday;
			// Expected Result : 7/18/2017 5:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[139]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[139]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample140()
        {
            // Date Expression : 2 hours before noon;
			// Expected Result : 7/20/2017 10:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[140]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[140]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample141()
        {
            // Date Expression : 2 hours to midnight;
			// Expected Result : 7/20/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[141]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[141]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample142()
        {
            // Date Expression : 2017-10-23 at 10 pm;
			// Expected Result : 10/23/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[142]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[142]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample143()
        {
            // Date Expression : 24-10-2010 10:20 AM;
			// Expected Result : 10/24/2010 10:20:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[143]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[143]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample144()
        {
            // Date Expression : 5 months before now;
			// Expected Result : 2/20/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[144]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[144]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample145()
        {
            // Date Expression : afternoon yesterday;
			// Expected Result : 7/19/2017 1:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[145]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[145]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample146()
        {
            // Date Expression : february 14th, 2004;
			// Expected Result : 2/14/2004 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[146]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[146]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample147()
        {
            // Date Expression : four weeks from now;
			// Expected Result : 8/17/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[147]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[147]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample148()
        {
            // Date Expression : last sunday evening;
			// Expected Result : 7/16/2017 6:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[148]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[148]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample149()
        {
            // Date Expression : next monday evening;
			// Expected Result : 7/24/2017 9:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[149]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[149]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample150()
        {
            // Date Expression : three days from now;
			// Expected Result : 7/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[150]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[150]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample151()
        {
            // Date Expression : 4 days to next year;
			// Expected Result : 12/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[151]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[151]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample152()
        {
            // Date Expression : 2 days from tomorrow;
			// Expected Result : 7/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[152]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[152]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample153()
        {
            // Date Expression : 2017/10/22, 10:20 PM;
			// Expected Result : 10/22/2017 10:20:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[153]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[153]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample154()
        {
            // Date Expression : january twenty-third;
			// Expected Result : 1/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[154]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[154]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample155()
        {
            // Date Expression : last friday at 20:00;
			// Expected Result : 7/14/2017 8:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[155]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[155]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample156()
        {
            // Date Expression : last week on tuesday;
			// Expected Result : 7/11/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[156]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[156]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample157()
        {
            // Date Expression : sat 7 in the evening;
			// Expected Result : 7/22/2017 7:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[157]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[157]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample158()
        {
            // Date Expression : Sun, Nov 2nd of 1990;
			// Expected Result : 11/2/1990 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[158]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[158]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample159()
        {
            // Date Expression : sunday november 26th;
			// Expected Result : 11/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[159]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[159]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample160()
        {
            // Date Expression : twenty third of june;
			// Expected Result : 6/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[160]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[160]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample161()
        {
            // Date Expression : within the next hour;
			// Expected Result : 7/20/2017 3:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[161]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[161]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample162()
        {
            // Date Expression : 2 hours from midnight;
			// Expected Result : 7/21/2017 2:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[162]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[162]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample163()
        {
            // Date Expression : 3 hours past midnight;
			// Expected Result : 7/21/2017 3:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[163]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[163]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample164()
        {
            // Date Expression : 31st of december 2016;
			// Expected Result : 12/31/2016 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[164]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[164]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample165()
        {
            // Date Expression : 4 days from yesterday;
			// Expected Result : 7/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[165]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[165]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample166()
        {
            // Date Expression : 5 minutes to midnight;
			// Expected Result : 7/20/2017 11:55:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[166]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[166]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample167()
        {
            // Date Expression : 5 minutes to tomorrow;
			// Expected Result : 7/20/2017 11:55:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[167]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[167]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample168()
        {
            // Date Expression : 8pm on monday evening;
			// Expected Result : 7/17/2017 8:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[168]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[168]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample169()
        {
            // Date Expression : February twenty first;
			// Expected Result : 2/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[169]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[169]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample170()
        {
            // Date Expression : next monday the third;
			// Expected Result : 7/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[170]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[170]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample171()
        {
            // Date Expression : the day before sunday;
			// Expected Result : 7/22/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[171]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[171]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample172()
        {
            // Date Expression : the third day of july;
			// Expected Result : 7/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[172]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[172]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample173()
        {
            // Date Expression : february twenty-eighth;
			// Expected Result : 2/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[173]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[173]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample174()
        {
            // Date Expression : Fourth day in November;
			// Expected Result : 11/4/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[174]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[174]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample175()
        {
            // Date Expression : saturday at 20:40;
			// Expected Result : 7/22/2017 8:40:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[175]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[175]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample176()
        {
            // Date Expression : the day after tomorrow;
			// Expected Result : 7/22/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[176]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[176]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample177()
        {
            // Date Expression : in 3 months on the first friday;
			// Expected Result : 10/6/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[177]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[177]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample178()
        {
            // Date Expression : first friday in 3 months;
			// Expected Result : 10/6/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[178]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[178]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample179()
        {
            // Date Expression : 4 weeks ago on saturday;
			// Expected Result : 6/24/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[179]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[179]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample180()
        {
            // Date Expression : saturday 4 weeks ago;
			// Expected Result : 6/24/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[180]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[180]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample181()
        {
            // Date Expression : 4 months ago on saturday at 6pm;
			// Expected Result : 3/25/2017 6:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[181]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[181]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample182()
        {
            // Date Expression : saturday 4 months ago at 6pm;
			// Expected Result : 3/25/2017 6:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[182]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[182]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample183()
        {
            // Date Expression : the wednesday before the next;
			// Expected Result : 7/19/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[183]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[183]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample184()
        {
            // Date Expression : the wednesday before the previous;
			// Expected Result : 7/12/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[184]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[184]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample185()
        {
            // Date Expression : the wednesday after the previous;
			// Expected Result : 7/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[185]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[185]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample186()
        {
            // Date Expression : the wednesday after this;
			// Expected Result : 7/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[186]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[186]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample187()
        {
            // Date Expression : the monday after this one;
			// Expected Result : 7/24/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[187]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[187]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample188()
        {
            // Date Expression : the sunday before the previous;
			// Expected Result : 7/9/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[188]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[188]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample189()
        {
            // Date Expression : the sunday before the next;
			// Expected Result : 7/16/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[189]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[189]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample190()
        {
            // Date Expression : the day after the next;
			// Expected Result : 7/22/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[190]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[190]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample191()
        {
            // Date Expression : the week after the next;
			// Expected Result : 7/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[191]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[191]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample192()
        {
            // Date Expression : the day before the next;
			// Expected Result : 7/20/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[192]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[192]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample193()
        {
            // Date Expression : the week before the previous;
			// Expected Result : 7/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[193]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[193]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample194()
        {
            // Date Expression : the day before the previous;
			// Expected Result : 7/18/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[194]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[194]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample195()
        {
            // Date Expression : the month after the next;
			// Expected Result : 9/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[195]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[195]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample196()
        {
            // Date Expression : the thursday before the previous;
			// Expected Result : 7/6/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[196]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[196]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample197()
        {
            // Date Expression : first monday in two months;
			// Expected Result : 9/4/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[197]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[197]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample198()
        {
            // Date Expression : second friday in two months;
			// Expected Result : 9/8/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[198]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[198]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample199()
        {
            // Date Expression : third friday in two years;
			// Expected Result : 1/18/2019 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[199]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[199]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample200()
        {
            // Date Expression : third week in December;
			// Expected Result : 12/15/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[200]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[200]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample201()
        {
            // Date Expression : 3 days before next week;
			// Expected Result : 7/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[201]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[201]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample202()
        {
            // Date Expression : 4 days after next week;
			// Expected Result : 7/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[202]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[202]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample203()
        {
            // Date Expression : 1 hours before midnight;
			// Expected Result : 7/20/2017 11:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[203]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[203]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample204()
        {
            // Date Expression : 2 hours before tomorrow;
			// Expected Result : 7/20/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[204]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[204]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample205()
        {
            // Date Expression : 3:45 on tuesday morning;
			// Expected Result : 7/18/2017 3:45:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[205]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[205]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample206()
        {
            // Date Expression : 5 minutes from tomorrow;
			// Expected Result : 7/21/2017 12:05:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[206]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[206]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample207()
        {
            // Date Expression : 7 hours before tomorrow;
			// Expected Result : 7/20/2017 5:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[207]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[207]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample208()
        {
            // Date Expression : the day after next week;
			// Expected Result : 7/25/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[208]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[208]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample209()
        {
            // Date Expression : the second day of march;
			// Expected Result : 3/2/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[209]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[209]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample210()
        {
            // Date Expression : the second week of july;
			// Expected Result : 7/8/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[210]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[210]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample211()
        {
            // Date Expression : tomorrow during the day;
			// Expected Result : 7/21/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[211]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[211]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample212()
        {
            // Date Expression : 10 min past midnight;
			// Expected Result : 7/21/2017 12:10:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[212]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[212]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample213()
        {
            // Date Expression : 4 days before next week;
			// Expected Result : 7/20/2017 14:30:00 AM //same day!

            var result = Chronox.ParseDateTime(Reference, Expressions[213]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[213]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample214()
        {
            // Date Expression : 4 days before next month;
			// Expected Result : 7/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[214]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[214]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample215()
        {
            // Date Expression : 4 days after next month;
			// Expected Result : 8/5/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[215]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[215]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample216()
        {
            // Date Expression : 4 days before next year;
			// Expected Result : 12/28/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[216]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[216]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample217()
        {
            // Date Expression : last friday of next year;
			// Expected Result : 12/28/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[217]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[217]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample218()
        {
            // Date Expression : last monday of the month;
			// Expected Result : 7/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[218]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[218]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample219()
        {
            // Date Expression : the day before yesterday;
			// Expected Result : 7/18/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[219]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[219]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample220()
        {
            // Date Expression : the week after next week;
			// Expected Result : 7/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[220]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[220]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample221()
        {
            // Date Expression : 3:45 on tuesday afternoon;
			// Expected Result : 7/18/2017 3:45:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[221]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[221]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample222()
        {
            // Date Expression : first friday of this year;
			// Expected Result : 1/6/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[222]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[222]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample223()
        {
            // Date Expression : the fourth day of the next week;
			// Expected Result : 7/27/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[223]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[223]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample224()
        {
            // Date Expression : january twenty-third 2017;
			// Expected Result : 1/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[224]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[224]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample225()
        {
            // Date Expression : monday the 3 of June 2017;
			// Expected Result : 6/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[225]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[225]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample226()
        {
            // Date Expression : next sat 7 in the evening;
			// Expected Result : 7/29/2017 7:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[226]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[226]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample227()
        {
            // Date Expression : the day before next month;
			// Expected Result : 7/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[227]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[227]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample228()
        {
            // Date Expression : the third day of the week;
			// Expected Result : 7/19/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[228]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[228]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample229()
        {
            // Date Expression : this sat 7 in the morning;
			// Expected Result : 7/22/2017 7:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[229]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[229]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample230()
        {
            // Date Expression : twenty third of june 2017;
			// Expected Result : 6/23/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[230]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[230]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample231()
        {
            // Date Expression : fifth of may 2016 at 20:00;
			// Expected Result : 5/5/2016 8:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[231]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[231]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample232()
        {
            // Date Expression : second friday of next year;
			// Expected Result : 1/12/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[232]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[232]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample233()
        {
            // Date Expression : second monday of the month;
			// Expected Result : 7/10/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[233]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[233]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample234()
        {
            // Date Expression : sunday november 26 in 2017;
			// Expected Result : 11/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[234]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[234]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample235()
        {
            // Date Expression : the 3 of June 2017 at 10pm;
			// Expected Result : 6/3/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[235]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[235]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample236()
        {
            // Date Expression : the day after next tuesday;
			// Expected Result : 7/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[236]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[236]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample237()
        {
            // Date Expression : the fourth day of the year;
			// Expected Result : 1/4/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[237]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[237]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample238()
        {
            // Date Expression : the last day of next month;
			// Expected Result : 8/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[238]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[238]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample239()
        {
            // Date Expression : the third day of next week;
			// Expected Result : 7/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[239]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[239]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample240()
        {
            // Date Expression : the week third of december;
			// Expected Result : 12/15/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[240]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[240]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample241()
        {
            // Date Expression : third saturday of the year;
			// Expected Result : 1/21/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[241]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[241]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample242()
        {
            // Date Expression : third Thursday in November;
			// Expected Result : 11/16/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[242]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[242]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample243()
        {
            // Date Expression : monday 10:30 in the morning;
			// Expected Result : 7/17/2017 10:30:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[243]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[243]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample244()
        {
            // Date Expression : monday the 3rd of June 2017;
			// Expected Result : 6/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[244]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[244]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample245()
        {
            // Date Expression : next week on monday morning;
			// Expected Result : 7/24/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[245]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[245]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample246()
        {
            // Date Expression : the day before next tuesday;
			// Expected Result : 7/24/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[246]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[246]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample247()
        {
            // Date Expression : the first day of next month;
			// Expected Result : 8/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[247]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[247]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample248()
        {
            // Date Expression : the third week of next year;
			// Expected Result : 1/15/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[248]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[248]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample249()
        {
            // Date Expression : third of june 2017 at 11 PM;
			// Expected Result : 6/3/2017 11:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[249]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[249]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample250()
        {
            // Date Expression : 3rd of December 2020 at 10pm;
			// Expected Result : 12/3/2020 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[250]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[250]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample251()
        {
            // Date Expression : December 3rd of 2022 at 12pm;
			// Expected Result : 12/3/2022 12:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[251]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[251]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample252()
        {
            // Date Expression : sunday november 26th in 2017;
			// Expected Result : 11/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[252]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[252]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample253()
        {
            // Date Expression : the 3rd of June 2017 at 10pm;
			// Expected Result : 6/3/2017 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[253]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[253]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample254()
        {
            // Date Expression : the first week of last month;
			// Expected Result : 6/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[254]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[254]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample255()
        {
            // Date Expression : the second week of last month;
			// Expected Result : 6/8/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[255]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[255]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample256()
        {
            // Date Expression : the first week of this month;
			// Expected Result : 7/1/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[256]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[256]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample257()
        {
            // Date Expression : The 25 september in the year 2008;
			// Expected Result : 9/25/2008 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[257]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[257]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample258()
        {
            // Date Expression : first friday of the next month;
			// Expected Result : 8/4/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[258]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[258]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample259()
        {
            // Date Expression : last week on tuesday afternoon;
			// Expected Result : 7/11/2017 1:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[259]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[259]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample260()
        {
            // Date Expression : the day before yesterday at 10;
			// Expected Result : 7/18/2017 10:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[260]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[260]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample261()
        {
            // Date Expression : independence day during the day;
			// Expected Result : 7/4/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[261]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[261]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample262()
        {
            // Date Expression : last day of the following month;
			// Expected Result : 8/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[262]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[262]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample263()
        {
            // Date Expression : next week on thursday afternoon;
			// Expected Result : 7/27/2017 1:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[263]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[263]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample264()
        {
            // Date Expression : the third week of next february;
			// Expected Result : 2/15/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[264]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[264]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample265()
        {
            // Date Expression : this sat the 7th in the evening;
			// Expected Result : 7/7/2017 6:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[265]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[265]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample266()
        {
            // Date Expression : 3 months ago saturday at 5:00 pm;
			// Expected Result : 4/22/2017 5:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[266]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[266]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample267()
        {
            // Date Expression : third day of the following month;
			// Expected Result : 8/3/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[267]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[267]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample268()
        {
            // Date Expression : twenty second day of the following month;
			// Expected Result : 8/22/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[268]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[268]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample269()
        {
            // Date Expression : Friday the 21st of November 1997;
			// Expected Result : 11/21/1997 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[269]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[269]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample270()
        {
            // Date Expression : Sun, Nov 2nd of 1990 at 10:30 pm;
			// Expected Result : 11/2/1990 10:30:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[270]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[270]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample271()
        {
            // Date Expression : The 22nd of march in the year 2010;
			// Expected Result : 3/22/2010 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[271]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[271]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample272()
        {
            // Date Expression : independence day during the night;
			// Expected Result : 7/4/2017 9:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[272]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[272]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample273()
        {
            // Date Expression : July, 15 of 2014 10:30:20.1000 PM;
			// Expected Result : 7/15/2014 10:30:20 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[273]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[273]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample274()
        {
            // Date Expression : next saturday 7:00 in the evening;
			// Expected Result : 7/29/2017 7:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[274]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[274]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample275()
        {
            // Date Expression : second day of the following month;
			// Expected Result : 8/2/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[275]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[275]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample276()
        {
            // Date Expression : this saturday at 7 in the evening;
			// Expected Result : 7/22/2017 7:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[276]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[276]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample277()
        {
            // Date Expression : The 25th of April in the year 2008;
			// Expected Result : 4/25/2008 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[277]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[277]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample278()
        {
            // Date Expression : the twenty sixth day of next month;
			// Expected Result : 8/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[278]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[278]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample279()
        {
            // Date Expression : monday the 3rd of June 2017 at 20pm;
			// Expected Result : 6/3/2017 8:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[279]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[279]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample280()
        {
            // Date Expression : second monday of the previous month;
			// Expected Result : 6/12/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[280]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[280]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample281()
        {
            // Date Expression : last monday the first in the morning;
			// Expected Result : 7/1/2017 6:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[281]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[281]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample282()
        {
            // Date Expression : the 31st of december of the year 2018;
			// Expected Result : 12/31/2018 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[282]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[282]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample283()
        {
            // Date Expression : the following week on wednesday night;
			// Expected Result : 7/26/2017 9:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[283]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[283]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample284()
        {
            // Date Expression : may seventh '97 at three in the morning;
			// Expected Result : 5/7/1997 3:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[284]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[284]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample285()
        {
            // Date Expression : The 21 of April in the year 2008 at 10 pm;
			// Expected Result : 4/21/2008 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[285]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[285]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample286()
        {
            // Date Expression : first friday of the following month at noon;
			// Expected Result : 8/4/2017 12:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[286]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[286]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample287()
        {
            // Date Expression : last monday of the previous month at midnight;
			// Expected Result : 6/26/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[287]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[287]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample288()
        {
            // Date Expression : monday the 3rd of June of the year 2017 at 20pm;
			// Expected Result : 6/3/2017 8:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[288]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[288]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample289()
        {
            // Date Expression : the 31 of december of the year 2017 at 10:31 pm;
			// Expected Result : 12/31/2017 10:31:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[289]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[289]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample290()
        {
            // Date Expression : The thirtieth of April in the year 2008 at 10 pm;
			// Expected Result : 4/30/2008 10:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[290]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[290]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample291()
        {
            // Date Expression : the thirty first of december of the year 2017 at 12 am;
			// Expected Result : 12/31/2017 12:00:00 AM

            var result = Chronox.ParseDateTime(Reference, Expressions[291]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[291]].ToString(), date.ToString());
        }

		[TestMethod]
        public void TestExample292()
        {
            // Date Expression : fourteenth of june 2010 at eleven o'clock in the evening;
			// Expected Result : 6/14/2010 11:00:00 PM

            var result = Chronox.ParseDateTime(Reference, Expressions[292]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[292]].ToString(), date.ToString());
        }
    }
}
