using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronox.Interfaces;
using Chronox.Utilities;
using System.Collections.Generic;

namespace Chronox.Tests
{
    [TestClass]
    public class ParsingTest
    {

        private IChronox Chronox = ChronoxParser.GetInstance(new ChronoxSettings("english"));

        private DateTime Reference = new DateTime(year: 2017, month: 07, day: 20, hour: 14, minute: 30, second: 00);

        private string[] Expressions = TestData.Keys.ToArray();

        private static readonly Dictionary<string, DateTime> TestData = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase)
        {
             {"4pm", DateTime.Parse("7/20/2017, 16:00:00") },
             {"4:00", DateTime.Parse("7/20/2017, 4:00:00") },
             {"7:00", DateTime.Parse("7/20/2017, 7:00:00") },
             {"14:00", DateTime.Parse("7/20/2017, 14:00:00") },
             {"17:00", DateTime.Parse("7/20/2017, 17:00:00") },
             {"1800s", DateTime.Parse("1/1/1800, 00:00:00") },
             {"night", DateTime.Parse("7/20/2017, 21:00:00") },
             {"today", DateTime.Parse("7/20/2017, 00:00:00") },
             {"dec 25", DateTime.Parse("12/25/2017, 00:00:00") },
             {"oct 06", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Sunday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"05/2003", DateTime.Parse("7/20/2017, 00:00:00") },
             {"10 to 8", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 hours", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 weeks", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4:00:00", DateTime.Parse("7/20/2017, 00:00:00") },
             {"at noon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"jan 1st", DateTime.Parse("7/20/2017, 00:00:00") },
             {"sat 7am", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tonight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4:00 a.m", DateTime.Parse("7/20/2017, 00:00:00") },
             {"may 27th", DateTime.Parse("7/20/2017, 00:00:00") },
             {"mon 2:35", DateTime.Parse("7/20/2017, 00:00:00") },
             {"november", DateTime.Parse("7/20/2017, 00:00:00") },
             {"thursday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"10 past 2", DateTime.Parse("7/20/2017, 00:00:00") },
             {"10 past 8", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4pm today", DateTime.Parse("7/20/2017, 00:00:00") },
             {"afternoon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"day after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"half to 2", DateTime.Parse("7/20/2017, 00:00:00") },
             {"January 5", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"right now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"yesterday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"10/24/1979", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2017/10/23", DateTime.Parse("7/20/2017, 00:00:00") },
             {"23-10-2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 jan 2000", DateTime.Parse("7/20/2017, 00:00:00") },
             {"a week ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"a year ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"christmass", DateTime.Parse("7/20/2017, 00:00:00") },
             {"friday 1pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"in 3 hours", DateTime.Parse("7/20/2017, 00:00:00") },
             {"in 3 weeks", DateTime.Parse("7/20/2017, 00:00:00") },
             {"in 4 years", DateTime.Parse("7/20/2017, 00:00:00") },
             {"jan 3 2010", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last night", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"1 oclock pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 years ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"31 december", DateTime.Parse("7/20/2017, 00:00:00") },
             {"7 hours ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"at midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"december 31", DateTime.Parse("7/20/2017, 00:00:00") },
             {"half past 2", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Jan 21, '97", DateTime.Parse("7/20/2017, 00:00:00") },
             {"quater to 2", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Sun, Nov 21", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this second", DateTime.Parse("7/20/2017, 00:00:00") },
             {"1 week hence", DateTime.Parse("7/20/2017, 00:00:00") },
             {"22nd of june", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4pm tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5:00pm yesterday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5th may 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"fifth of may", DateTime.Parse("7/20/2017, 00:00:00") },
             {"friday 13:00", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next january", DateTime.Parse("7/20/2017, 00:00:00") },
             {"October 2006", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this tuesday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4pm on monday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"december 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"december 31st", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last december", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last february", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last thursday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next december", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next february", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next thursday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"one oclock pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"one thirty am", DateTime.Parse("7/20/2017, 00:00:00") },
             {"third of june", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this november", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this thursday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow noon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday the 3rd", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 days from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 saturdays ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 saturdays after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 mondays ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 mondays after at 6:39:30 PM", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next weekend", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last weekend", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the weekend", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the weekend after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the weekend before", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4th of jan 2000", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 fridays after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"7 days from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"june the twelth", DateTime.Parse("7/20/2017, 00:00:00") },
             {"quater past 2pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow at ten", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tonight at 10pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"within the hour", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 fridays before", DateTime.Parse("7/20/2017, 00:00:00") },
             {"31 december 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"31st of december", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 weeks from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4pm next tuesday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 hours from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"6 in the morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Fri, 21 Nov 1997", DateTime.Parse("7/20/2017, 00:00:00") },
             {"independence day", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday the third", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Sunday next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the monday after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow at 10am", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow at 10pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 tuesdays before", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 tuesdays after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 mondays after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the monday before", DateTime.Parse("7/20/2017, 00:00:00") },
             {"one monday before", DateTime.Parse("7/20/2017, 00:00:00") },
             {"three mondays after", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 days from today", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 days from today", DateTime.Parse("7/20/2017, 00:00:00") },
             {"6 tuesday morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"december 31, 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"evening yesterday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"february 14, 2004", DateTime.Parse("7/20/2017, 00:00:00") },
             {"fifth of may 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last week tuesday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"one thirty two pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tuesday last week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"yesterday at 4:00", DateTime.Parse("7/20/2017, 00:00:00") },
             {"12 months from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"17 of april of '85", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 fridays from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 sundays from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 mondays from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 weeks from today", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 hours before now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 minutes from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"8pm in the evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"day after tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"december 31st 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"sunday november 26", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the 3 of June 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the tuesday before", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the twelth of june", DateTime.Parse("7/20/2017, 00:00:00") },
             {"third of june 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"thursday last week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow at 6:45pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4th day last week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"10-23-2017 at 10 pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours before monday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 hours to sunday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 hours after tuesday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours before noon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours to midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2017-10-23 at 10 pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"24-10-2010 10:20 AM", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 months before now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"afternoon yesterday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"february 14th, 2004", DateTime.Parse("7/20/2017, 00:00:00") },
             {"four weeks from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last sunday evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next monday evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"three days from now", DateTime.Parse("7/20/2017, 00:00:00") },
             {"yesterday afternoon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 days to next year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 days from tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2017/10/22, 10:20 PM", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 days from tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"january twenty-third", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last friday at 20:00", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last week on tuesday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"sat 7 in the evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Sun, Nov 2nd of 1990", DateTime.Parse("7/20/2017, 00:00:00") },
             {"sunday november 26th", DateTime.Parse("7/20/2017, 00:00:00") },
             {"twenty third of june", DateTime.Parse("7/20/2017, 00:00:00") },
             {"within the next hour", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours from midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours past midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 hours past midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"31st of december 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 days from yesterday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 minutes to midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 minutes to tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"8pm on monday evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"February twenty first", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next monday the third", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before sunday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the third day of july", DateTime.Parse("7/20/2017, 00:00:00") },
             {"february twenty-eighth", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Fourth day in November", DateTime.Parse("7/20/2017, 00:00:00") },
             {"saturday at 20:40.0000", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day after tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"in 3 months on the first friday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first friday in 3 months", DateTime.Parse("7/20/2017, 00:00:00") },
             {"five weeks ago on saturday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"ten months ago on saturday at 6pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 weeks ago on saturday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"saturday 4 weeks ago", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 months ago on saturday at 6pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"saturday 4 months ago at 6pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the monday after the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the tuesday after the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the monday before the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the monday before the previous", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the wednesday before the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the wednesday before the previous", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the wednesday after the previous", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the wednesday after this", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the monday after this one", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the sunday before the previous", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the sunday before the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day after the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the week after the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the week before the previous", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before the previous", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the month after the next", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first friday in two months", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the thursday before the previous", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first monday in two months", DateTime.Parse("7/20/2017, 00:00:00") },
             {"second friday in two months", DateTime.Parse("7/20/2017, 00:00:00") },
             {"second friday in one month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"third friday in two years", DateTime.Parse("7/20/2017, 00:00:00") },
             {"second monday in three months", DateTime.Parse("7/20/2017, 00:00:00") },
             {"third week in December", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 days before next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 days after next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"1 hours before midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"2 hours before tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3:45 on tuesday morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"5 minutes from tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"7 hours before tomorrow", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day after next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the second day of march", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the second week of july", DateTime.Parse("7/20/2017, 00:00:00") },
             {"tomorrow during the day", DateTime.Parse("7/20/2017, 00:00:00") },
             {"10 minutes past midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 days before next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 days before next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 days after next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"4 days before next year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last friday of next year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last monday of the month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before yesterday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the week after next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3:45 on tuesday afternoon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first friday of this year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the fourth day of the next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"january twenty-third 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday the 3 of June 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next sat 7 in the evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the third day of the week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this sat 7 in the morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"twenty third of june 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"fifth of may 2017 at 20:00", DateTime.Parse("7/20/2017, 00:00:00") },
             {"second friday of next year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"second monday of the month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"sunday november 26 in 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the 3 of June 2017 at 10pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day after next tuesday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the fourth day of the year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the last day of next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the third day of next week", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the week third of december", DateTime.Parse("7/20/2017, 00:00:00") },
             {"third saturday of the year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"third Thursday in November", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday 10:30 in the morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday the 3rd of June 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next week on monday morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before next tuesday", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the first day of next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the third week of last year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the third week of next year", DateTime.Parse("7/20/2017, 00:00:00") },
             {"third of june 2017 at 10 PM", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3rd of December 2022 at 10pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"December 3rd of 2022 at 10pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"sunday november 26th in 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the 3rd of June 2017 at 10pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the first week of last month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the second week of last month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the second week of this month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the first week of this month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"The 25 April in the year 2008", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the second week of next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first friday of the next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last week on tuesday afternoon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the day before yesterday at 10", DateTime.Parse("7/20/2017, 00:00:00") },
             {"independence day during the day", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last day of the following month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next week on thursday afternoon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the third week of next february", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this sat the 7th in the evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"3 months ago saturday at 5:00 pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first day of the following month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"twenty second day of the following month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Friday the 21st of November 1997", DateTime.Parse("7/20/2017, 00:00:00") },
             {"Sun, Nov 2nd of 1990 at 10:30 pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"The 22nd of May in the year 2010", DateTime.Parse("7/20/2017, 00:00:00") },
             {"independence day during the night", DateTime.Parse("7/20/2017, 00:00:00") },
             {"July, 15 of 2014 10:30:20.1000 PM", DateTime.Parse("7/20/2017, 00:00:00") },
             {"next saturday 7:00 in the evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"second day of the following month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"this saturday at 7 in the evening", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first monday of the previous month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"The 25th of April in the year 2008", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the twenty sixth day of next month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday the 3rd of June 2017 at 20pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"second monday of the previous month", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the 31 of december of the year 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last monday the first in the morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the 31st of december of the year 2017", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the following week on wednesday night", DateTime.Parse("7/20/2017, 00:00:00") },
             {"may seventh '97 at three in the morning", DateTime.Parse("7/20/2017, 00:00:00") },
             {"The 21 of April in the year 2008 at 10 pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"first friday of the following month at noon", DateTime.Parse("7/20/2017, 00:00:00") },
             {"last monday of the previous month at midnight", DateTime.Parse("7/20/2017, 00:00:00") },
             {"monday the 3rd of June of the year 2017 at 20pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the 31 of december of the year 2017 at 10:31 pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"The thirtieth  of April in the year 2008 at 10 pm", DateTime.Parse("7/20/2017, 00:00:00") },
             {"the thirty first of december of the year 2017 at 12 am", DateTime.Parse("7/20/2017, 00:00:00") },
             {"fourteenth of june 2010 at eleven o'clock in the evening", DateTime.Parse("7/20/2017, 00:00:00") }
         };


        [TestMethod]
        public void TestExample0()
        {
            // Expression : "4pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[0]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[0]], date);
        }

        [TestMethod]
        public void TestExample1()
        {
            // Expression : "4:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[1]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[1]], date);
        }

        [TestMethod]
        public void TestExample2()
        {
            // Expression : "7:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[2]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[2]], date);
        }

        [TestMethod]
        public void TestExample3()
        {
            // Expression : "14:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[3]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[3]], date);
        }

        [TestMethod]
        public void TestExample4()
        {
            // Expression : "17:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[4]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[4]], date);
        }

        [TestMethod]
        public void TestExample5()
        {
            // Expression : "1800s";

            var result = Chronox.ParseDateTime(Reference, Expressions[5]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[5]], date);
        }

        [TestMethod]
        public void TestExample6()
        {
            // Expression : "night";

            var result = Chronox.ParseDateTime(Reference, Expressions[6]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[6]], date);
        }

        [TestMethod]
        public void TestExample7()
        {
            // Expression : "today";

            var result = Chronox.ParseDateTime(Reference, Expressions[7]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[7]], date);
        }

        [TestMethod]
        public void TestExample8()
        {
            // Expression : "dec 25";

            var result = Chronox.ParseDateTime(Reference, Expressions[8]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[8]], date);
        }

        [TestMethod]
        public void TestExample9()
        {
            // Expression : "oct 06";

            var result = Chronox.ParseDateTime(Reference, Expressions[9]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[9]], date);
        }

        [TestMethod]
        public void TestExample10()
        {
            // Expression : "Sunday";

            var result = Chronox.ParseDateTime(Reference, Expressions[10]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[10]], date);
        }

        [TestMethod]
        public void TestExample11()
        {
            // Expression : "05/2003";

            var result = Chronox.ParseDateTime(Reference, Expressions[11]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[11]], date);
        }

        [TestMethod]
        public void TestExample12()
        {
            // Expression : "10 to 8";

            var result = Chronox.ParseDateTime(Reference, Expressions[12]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[12]], date);
        }

        [TestMethod]
        public void TestExample13()
        {
            // Expression : "3 hours";

            var result = Chronox.ParseDateTime(Reference, Expressions[13]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[13]], date);
        }

        [TestMethod]
        public void TestExample14()
        {
            // Expression : "4 weeks";

            var result = Chronox.ParseDateTime(Reference, Expressions[14]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[14]], date);
        }

        [TestMethod]
        public void TestExample15()
        {
            // Expression : "4:00:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[15]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[15]], date);
        }

        [TestMethod]
        public void TestExample16()
        {
            // Expression : "at noon";

            var result = Chronox.ParseDateTime(Reference, Expressions[16]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[16]], date);
        }

        [TestMethod]
        public void TestExample17()
        {
            // Expression : "jan 1st";

            var result = Chronox.ParseDateTime(Reference, Expressions[17]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[17]], date);
        }

        [TestMethod]
        public void TestExample18()
        {
            // Expression : "sat 7am";

            var result = Chronox.ParseDateTime(Reference, Expressions[18]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[18]], date);
        }

        [TestMethod]
        public void TestExample19()
        {
            // Expression : "the day";

            var result = Chronox.ParseDateTime(Reference, Expressions[19]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[19]], date);
        }

        [TestMethod]
        public void TestExample20()
        {
            // Expression : "tonight";

            var result = Chronox.ParseDateTime(Reference, Expressions[20]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[20]], date);
        }

        [TestMethod]
        public void TestExample21()
        {
            // Expression : "4:00 a.m";

            var result = Chronox.ParseDateTime(Reference, Expressions[21]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[21]], date);
        }

        [TestMethod]
        public void TestExample22()
        {
            // Expression : "may 27th";

            var result = Chronox.ParseDateTime(Reference, Expressions[22]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[22]], date);
        }

        [TestMethod]
        public void TestExample23()
        {
            // Expression : "mon 2:35";

            var result = Chronox.ParseDateTime(Reference, Expressions[23]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[23]], date);
        }

        [TestMethod]
        public void TestExample24()
        {
            // Expression : "november";

            var result = Chronox.ParseDateTime(Reference, Expressions[24]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[24]], date);
        }

        [TestMethod]
        public void TestExample25()
        {
            // Expression : "thursday";

            var result = Chronox.ParseDateTime(Reference, Expressions[25]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[25]], date);
        }

        [TestMethod]
        public void TestExample26()
        {
            // Expression : "tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[26]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[26]], date);
        }

        [TestMethod]
        public void TestExample27()
        {
            // Expression : "10 past 2";

            var result = Chronox.ParseDateTime(Reference, Expressions[27]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[27]], date);
        }

        [TestMethod]
        public void TestExample28()
        {
            // Expression : "10 past 8";

            var result = Chronox.ParseDateTime(Reference, Expressions[28]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[28]], date);
        }

        [TestMethod]
        public void TestExample29()
        {
            // Expression : "4pm today";

            var result = Chronox.ParseDateTime(Reference, Expressions[29]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[29]], date);
        }

        [TestMethod]
        public void TestExample30()
        {
            // Expression : "afternoon";

            var result = Chronox.ParseDateTime(Reference, Expressions[30]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[30]], date);
        }

        [TestMethod]
        public void TestExample31()
        {
            // Expression : "day after";

            var result = Chronox.ParseDateTime(Reference, Expressions[31]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[31]], date);
        }

        [TestMethod]
        public void TestExample32()
        {
            // Expression : "half to 2";

            var result = Chronox.ParseDateTime(Reference, Expressions[32]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[32]], date);
        }

        [TestMethod]
        public void TestExample33()
        {
            // Expression : "January 5";

            var result = Chronox.ParseDateTime(Reference, Expressions[33]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[33]], date);
        }

        [TestMethod]
        public void TestExample34()
        {
            // Expression : "last week";

            var result = Chronox.ParseDateTime(Reference, Expressions[34]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[34]], date);
        }

        [TestMethod]
        public void TestExample35()
        {
            // Expression : "next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[35]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[35]], date);
        }

        [TestMethod]
        public void TestExample36()
        {
            // Expression : "right now";

            var result = Chronox.ParseDateTime(Reference, Expressions[36]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[36]], date);
        }

        [TestMethod]
        public void TestExample37()
        {
            // Expression : "this year";

            var result = Chronox.ParseDateTime(Reference, Expressions[37]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[37]], date);
        }

        [TestMethod]
        public void TestExample38()
        {
            // Expression : "yesterday";

            var result = Chronox.ParseDateTime(Reference, Expressions[38]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[38]], date);
        }

        [TestMethod]
        public void TestExample39()
        {
            // Expression : "10/24/1979";

            var result = Chronox.ParseDateTime(Reference, Expressions[39]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[39]], date);
        }

        [TestMethod]
        public void TestExample40()
        {
            // Expression : "2017/10/23";

            var result = Chronox.ParseDateTime(Reference, Expressions[40]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[40]], date);
        }

        [TestMethod]
        public void TestExample41()
        {
            // Expression : "23-10-2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[41]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[41]], date);
        }

        [TestMethod]
        public void TestExample42()
        {
            // Expression : "3 jan 2000";

            var result = Chronox.ParseDateTime(Reference, Expressions[42]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[42]], date);
        }

        [TestMethod]
        public void TestExample43()
        {
            // Expression : "a week ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[43]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[43]], date);
        }

        [TestMethod]
        public void TestExample44()
        {
            // Expression : "a year ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[44]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[44]], date);
        }

        [TestMethod]
        public void TestExample45()
        {
            // Expression : "christmass";

            var result = Chronox.ParseDateTime(Reference, Expressions[45]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[45]], date);
        }

        [TestMethod]
        public void TestExample46()
        {
            // Expression : "friday 1pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[46]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[46]], date);
        }

        [TestMethod]
        public void TestExample47()
        {
            // Expression : "in 3 hours";

            var result = Chronox.ParseDateTime(Reference, Expressions[47]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[47]], date);
        }

        [TestMethod]
        public void TestExample48()
        {
            // Expression : "in 3 weeks";

            var result = Chronox.ParseDateTime(Reference, Expressions[48]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[48]], date);
        }

        [TestMethod]
        public void TestExample49()
        {
            // Expression : "in 4 years";

            var result = Chronox.ParseDateTime(Reference, Expressions[49]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[49]], date);
        }

        [TestMethod]
        public void TestExample50()
        {
            // Expression : "jan 3 2010";

            var result = Chronox.ParseDateTime(Reference, Expressions[50]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[50]], date);
        }

        [TestMethod]
        public void TestExample51()
        {
            // Expression : "last night";

            var result = Chronox.ParseDateTime(Reference, Expressions[51]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[51]], date);
        }

        [TestMethod]
        public void TestExample52()
        {
            // Expression : "next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[52]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[52]], date);
        }

        [TestMethod]
        public void TestExample53()
        {
            // Expression : "1 oclock pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[53]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[53]], date);
        }

        [TestMethod]
        public void TestExample54()
        {
            // Expression : "2 hours ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[54]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[54]], date);
        }

        [TestMethod]
        public void TestExample55()
        {
            // Expression : "3 years ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[55]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[55]], date);
        }

        [TestMethod]
        public void TestExample56()
        {
            // Expression : "31 december";

            var result = Chronox.ParseDateTime(Reference, Expressions[56]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[56]], date);
        }

        [TestMethod]
        public void TestExample57()
        {
            // Expression : "7 hours ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[57]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[57]], date);
        }

        [TestMethod]
        public void TestExample58()
        {
            // Expression : "at midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[58]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[58]], date);
        }

        [TestMethod]
        public void TestExample59()
        {
            // Expression : "december 31";

            var result = Chronox.ParseDateTime(Reference, Expressions[59]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[59]], date);
        }

        [TestMethod]
        public void TestExample60()
        {
            // Expression : "half past 2";

            var result = Chronox.ParseDateTime(Reference, Expressions[60]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[60]], date);
        }

        [TestMethod]
        public void TestExample61()
        {
            // Expression : "Jan 21, '97";

            var result = Chronox.ParseDateTime(Reference, Expressions[61]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[61]], date);
        }

        [TestMethod]
        public void TestExample62()
        {
            // Expression : "quater to 2";

            var result = Chronox.ParseDateTime(Reference, Expressions[62]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[62]], date);
        }

        [TestMethod]
        public void TestExample63()
        {
            // Expression : "Sun, Nov 21";

            var result = Chronox.ParseDateTime(Reference, Expressions[63]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[63]], date);
        }

        [TestMethod]
        public void TestExample64()
        {
            // Expression : "this second";

            var result = Chronox.ParseDateTime(Reference, Expressions[64]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[64]], date);
        }

        [TestMethod]
        public void TestExample65()
        {
            // Expression : "1 week hence";

            var result = Chronox.ParseDateTime(Reference, Expressions[65]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[65]], date);
        }

        [TestMethod]
        public void TestExample66()
        {
            // Expression : "22nd of june";

            var result = Chronox.ParseDateTime(Reference, Expressions[66]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[66]], date);
        }

        [TestMethod]
        public void TestExample67()
        {
            // Expression : "4pm tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[67]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[67]], date);
        }

        [TestMethod]
        public void TestExample68()
        {
            // Expression : "5:00pm yesterday";

            var result = Chronox.ParseDateTime(Reference, Expressions[68]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[68]], date);
        }

        [TestMethod]
        public void TestExample69()
        {
            // Expression : "5th may 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[69]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[69]], date);
        }

        [TestMethod]
        public void TestExample70()
        {
            // Expression : "fifth of may";

            var result = Chronox.ParseDateTime(Reference, Expressions[70]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[70]], date);
        }

        [TestMethod]
        public void TestExample71()
        {
            // Expression : "friday 13:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[71]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[71]], date);
        }

        [TestMethod]
        public void TestExample72()
        {
            // Expression : "next january";

            var result = Chronox.ParseDateTime(Reference, Expressions[72]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[72]], date);
        }

        [TestMethod]
        public void TestExample73()
        {
            // Expression : "October 2006";

            var result = Chronox.ParseDateTime(Reference, Expressions[73]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[73]], date);
        }

        [TestMethod]
        public void TestExample74()
        {
            // Expression : "this morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[74]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[74]], date);
        }

        [TestMethod]
        public void TestExample75()
        {
            // Expression : "this tuesday";

            var result = Chronox.ParseDateTime(Reference, Expressions[75]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[75]], date);
        }

        [TestMethod]
        public void TestExample76()
        {
            // Expression : "4pm on monday";

            var result = Chronox.ParseDateTime(Reference, Expressions[76]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[76]], date);
        }

        [TestMethod]
        public void TestExample77()
        {
            // Expression : "december 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[77]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[77]], date);
        }

        [TestMethod]
        public void TestExample78()
        {
            // Expression : "december 31st";

            var result = Chronox.ParseDateTime(Reference, Expressions[78]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[78]], date);
        }

        [TestMethod]
        public void TestExample79()
        {
            // Expression : "last december";

            var result = Chronox.ParseDateTime(Reference, Expressions[79]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[79]], date);
        }

        [TestMethod]
        public void TestExample80()
        {
            // Expression : "last february";

            var result = Chronox.ParseDateTime(Reference, Expressions[80]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[80]], date);
        }

        [TestMethod]
        public void TestExample81()
        {
            // Expression : "last thursday";

            var result = Chronox.ParseDateTime(Reference, Expressions[81]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[81]], date);
        }

        [TestMethod]
        public void TestExample82()
        {
            // Expression : "next december";

            var result = Chronox.ParseDateTime(Reference, Expressions[82]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[82]], date);
        }

        [TestMethod]
        public void TestExample83()
        {
            // Expression : "next february";

            var result = Chronox.ParseDateTime(Reference, Expressions[83]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[83]], date);
        }

        [TestMethod]
        public void TestExample84()
        {
            // Expression : "next thursday";

            var result = Chronox.ParseDateTime(Reference, Expressions[84]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[84]], date);
        }

        [TestMethod]
        public void TestExample85()
        {
            // Expression : "one oclock pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[85]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[85]], date);
        }

        [TestMethod]
        public void TestExample86()
        {
            // Expression : "one thirty am";

            var result = Chronox.ParseDateTime(Reference, Expressions[86]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[86]], date);
        }

        [TestMethod]
        public void TestExample87()
        {
            // Expression : "third of june";

            var result = Chronox.ParseDateTime(Reference, Expressions[87]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[87]], date);
        }

        [TestMethod]
        public void TestExample88()
        {
            // Expression : "this november";

            var result = Chronox.ParseDateTime(Reference, Expressions[88]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[88]], date);
        }

        [TestMethod]
        public void TestExample89()
        {
            // Expression : "this thursday";

            var result = Chronox.ParseDateTime(Reference, Expressions[89]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[89]], date);
        }

        [TestMethod]
        public void TestExample90()
        {
            // Expression : "tomorrow noon";

            var result = Chronox.ParseDateTime(Reference, Expressions[90]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[90]], date);
        }

        [TestMethod]
        public void TestExample91()
        {
            // Expression : "monday the 3rd";

            var result = Chronox.ParseDateTime(Reference, Expressions[91]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[91]], date);
        }

        [TestMethod]
        public void TestExample92()
        {
            // Expression : "the day before";

            var result = Chronox.ParseDateTime(Reference, Expressions[92]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[92]], date);
        }

        [TestMethod]
        public void TestExample93()
        {
            // Expression : "3 days from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[93]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[93]], date);
        }

        [TestMethod]
        public void TestExample94()
        {
            // Expression : "4 saturdays ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[94]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[94]], date);
        }

        [TestMethod]
        public void TestExample95()
        {
            // Expression : "3 saturdays after";

            var result = Chronox.ParseDateTime(Reference, Expressions[95]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[95]], date);
        }

        [TestMethod]
        public void TestExample96()
        {
            // Expression : "3 mondays ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[96]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[96]], date);
        }

        [TestMethod]
        public void TestExample97()
        {
            // Expression : "3 mondays after at 6:39:30 PM";

            var result = Chronox.ParseDateTime(Reference, Expressions[97]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[97]], date);
        }

        [TestMethod]
        public void TestExample98()
        {
            // Expression : "next weekend";

            var result = Chronox.ParseDateTime(Reference, Expressions[98]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[98]], date);
        }

        [TestMethod]
        public void TestExample99()
        {
            // Expression : "last weekend";

            var result = Chronox.ParseDateTime(Reference, Expressions[99]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[99]], date);
        }

        [TestMethod]
        public void TestExample100()
        {
            // Expression : "the weekend";

            var result = Chronox.ParseDateTime(Reference, Expressions[100]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[100]], date);
        }

        [TestMethod]
        public void TestExample101()
        {
            // Expression : "the weekend after";

            var result = Chronox.ParseDateTime(Reference, Expressions[101]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[101]], date);
        }

        [TestMethod]
        public void TestExample102()
        {
            // Expression : "the weekend before";

            var result = Chronox.ParseDateTime(Reference, Expressions[102]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[102]], date);
        }

        [TestMethod]
        public void TestExample103()
        {
            // Expression : "4th of jan 2000";

            var result = Chronox.ParseDateTime(Reference, Expressions[103]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[103]], date);
        }

        [TestMethod]
        public void TestExample104()
        {
            // Expression : "5 fridays after";

            var result = Chronox.ParseDateTime(Reference, Expressions[104]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[104]], date);
        }

        [TestMethod]
        public void TestExample105()
        {
            // Expression : "7 days from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[105]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[105]], date);
        }

        [TestMethod]
        public void TestExample106()
        {
            // Expression : "june the twelth";

            var result = Chronox.ParseDateTime(Reference, Expressions[106]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[106]], date);
        }

        [TestMethod]
        public void TestExample107()
        {
            // Expression : "quater past 2pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[107]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[107]], date);
        }

        [TestMethod]
        public void TestExample108()
        {
            // Expression : "tomorrow at ten";

            var result = Chronox.ParseDateTime(Reference, Expressions[108]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[108]], date);
        }

        [TestMethod]
        public void TestExample109()
        {
            // Expression : "tonight at 10pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[109]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[109]], date);
        }

        [TestMethod]
        public void TestExample110()
        {
            // Expression : "within the hour";

            var result = Chronox.ParseDateTime(Reference, Expressions[110]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[110]], date);
        }

        [TestMethod]
        public void TestExample111()
        {
            // Expression : "2 hours from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[111]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[111]], date);
        }

        [TestMethod]
        public void TestExample112()
        {
            // Expression : "3 fridays before";

            var result = Chronox.ParseDateTime(Reference, Expressions[112]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[112]], date);
        }

        [TestMethod]
        public void TestExample113()
        {
            // Expression : "31 december 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[113]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[113]], date);
        }

        [TestMethod]
        public void TestExample114()
        {
            // Expression : "31st of december";

            var result = Chronox.ParseDateTime(Reference, Expressions[114]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[114]], date);
        }

        [TestMethod]
        public void TestExample115()
        {
            // Expression : "4 weeks from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[115]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[115]], date);
        }

        [TestMethod]
        public void TestExample116()
        {
            // Expression : "4pm next tuesday";

            var result = Chronox.ParseDateTime(Reference, Expressions[116]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[116]], date);
        }

        [TestMethod]
        public void TestExample117()
        {
            // Expression : "5 hours from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[117]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[117]], date);
        }

        [TestMethod]
        public void TestExample118()
        {
            // Expression : "6 in the morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[118]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[118]], date);
        }

        [TestMethod]
        public void TestExample119()
        {
            // Expression : "Fri, 21 Nov 1997";

            var result = Chronox.ParseDateTime(Reference, Expressions[119]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[119]], date);
        }

        [TestMethod]
        public void TestExample120()
        {
            // Expression : "independence day";

            var result = Chronox.ParseDateTime(Reference, Expressions[120]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[120]], date);
        }

        [TestMethod]
        public void TestExample121()
        {
            // Expression : "monday next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[121]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[121]], date);
        }

        [TestMethod]
        public void TestExample122()
        {
            // Expression : "monday the third";

            var result = Chronox.ParseDateTime(Reference, Expressions[122]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[122]], date);
        }

        [TestMethod]
        public void TestExample123()
        {
            // Expression : "Sunday next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[123]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[123]], date);
        }

        [TestMethod]
        public void TestExample124()
        {
            // Expression : "the monday after";

            var result = Chronox.ParseDateTime(Reference, Expressions[124]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[124]], date);
        }

        [TestMethod]
        public void TestExample125()
        {
            // Expression : "tomorrow at 10am";

            var result = Chronox.ParseDateTime(Reference, Expressions[125]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[125]], date);
        }

        [TestMethod]
        public void TestExample126()
        {
            // Expression : "tomorrow at 10pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[126]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[126]], date);
        }

        [TestMethod]
        public void TestExample127()
        {
            // Expression : "tomorrow evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[127]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[127]], date);
        }

        [TestMethod]
        public void TestExample128()
        {
            // Expression : "tomorrow morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[128]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[128]], date);
        }

        [TestMethod]
        public void TestExample129()
        {
            // Expression : "2 tuesdays before";

            var result = Chronox.ParseDateTime(Reference, Expressions[129]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[129]], date);
        }

        [TestMethod]
        public void TestExample130()
        {
            // Expression : "4 tuesdays after";

            var result = Chronox.ParseDateTime(Reference, Expressions[130]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[130]], date);
        }

        [TestMethod]
        public void TestExample131()
        {
            // Expression : "2 mondays after";

            var result = Chronox.ParseDateTime(Reference, Expressions[131]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[131]], date);
        }

        [TestMethod]
        public void TestExample132()
        {
            // Expression : "the monday before";

            var result = Chronox.ParseDateTime(Reference, Expressions[132]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[132]], date);
        }

        [TestMethod]
        public void TestExample133()
        {
            // Expression : "one monday before";

            var result = Chronox.ParseDateTime(Reference, Expressions[133]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[133]], date);
        }

        [TestMethod]
        public void TestExample134()
        {
            // Expression : "three mondays after";

            var result = Chronox.ParseDateTime(Reference, Expressions[134]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[134]], date);
        }

        [TestMethod]
        public void TestExample135()
        {
            // Expression : "3 days from today";

            var result = Chronox.ParseDateTime(Reference, Expressions[135]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[135]], date);
        }

        [TestMethod]
        public void TestExample136()
        {
            // Expression : "5 days from today";

            var result = Chronox.ParseDateTime(Reference, Expressions[136]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[136]], date);
        }

        [TestMethod]
        public void TestExample137()
        {
            // Expression : "6 tuesday morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[137]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[137]], date);
        }

        [TestMethod]
        public void TestExample138()
        {
            // Expression : "december 31, 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[138]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[138]], date);
        }

        [TestMethod]
        public void TestExample139()
        {
            // Expression : "evening yesterday";

            var result = Chronox.ParseDateTime(Reference, Expressions[139]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[139]], date);
        }

        [TestMethod]
        public void TestExample140()
        {
            // Expression : "february 14, 2004";

            var result = Chronox.ParseDateTime(Reference, Expressions[140]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[140]], date);
        }

        [TestMethod]
        public void TestExample141()
        {
            // Expression : "fifth of may 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[141]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[141]], date);
        }

        [TestMethod]
        public void TestExample142()
        {
            // Expression : "last week tuesday";

            var result = Chronox.ParseDateTime(Reference, Expressions[142]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[142]], date);
        }

        [TestMethod]
        public void TestExample143()
        {
            // Expression : "one thirty two pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[143]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[143]], date);
        }

        [TestMethod]
        public void TestExample144()
        {
            // Expression : "tuesday last week";

            var result = Chronox.ParseDateTime(Reference, Expressions[144]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[144]], date);
        }

        [TestMethod]
        public void TestExample145()
        {
            // Expression : "yesterday at 4:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[145]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[145]], date);
        }

        [TestMethod]
        public void TestExample146()
        {
            // Expression : "12 months from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[146]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[146]], date);
        }

        [TestMethod]
        public void TestExample147()
        {
            // Expression : "17 of april of '85";

            var result = Chronox.ParseDateTime(Reference, Expressions[147]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[147]], date);
        }

        [TestMethod]
        public void TestExample148()
        {
            // Expression : "2 fridays from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[148]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[148]], date);
        }

        [TestMethod]
        public void TestExample149()
        {
            // Expression : "2 sundays from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[149]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[149]], date);
        }

        [TestMethod]
        public void TestExample150()
        {
            // Expression : "3 mondays from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[150]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[150]], date);
        }

        [TestMethod]
        public void TestExample151()
        {
            // Expression : "4 weeks from today";

            var result = Chronox.ParseDateTime(Reference, Expressions[151]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[151]], date);
        }

        [TestMethod]
        public void TestExample152()
        {
            // Expression : "5 hours before now";

            var result = Chronox.ParseDateTime(Reference, Expressions[152]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[152]], date);
        }

        [TestMethod]
        public void TestExample153()
        {
            // Expression : "5 minutes from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[153]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[153]], date);
        }

        [TestMethod]
        public void TestExample154()
        {
            // Expression : "8pm in the evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[154]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[154]], date);
        }

        [TestMethod]
        public void TestExample155()
        {
            // Expression : "day after tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[155]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[155]], date);
        }

        [TestMethod]
        public void TestExample156()
        {
            // Expression : "december 31st 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[156]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[156]], date);
        }

        [TestMethod]
        public void TestExample157()
        {
            // Expression : "sunday november 26";

            var result = Chronox.ParseDateTime(Reference, Expressions[157]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[157]], date);
        }

        [TestMethod]
        public void TestExample158()
        {
            // Expression : "the 3 of June 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[158]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[158]], date);
        }

        [TestMethod]
        public void TestExample159()
        {
            // Expression : "the tuesday before";

            var result = Chronox.ParseDateTime(Reference, Expressions[159]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[159]], date);
        }

        [TestMethod]
        public void TestExample160()
        {
            // Expression : "the twelth of june";

            var result = Chronox.ParseDateTime(Reference, Expressions[160]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[160]], date);
        }

        [TestMethod]
        public void TestExample161()
        {
            // Expression : "third of june 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[161]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[161]], date);
        }

        [TestMethod]
        public void TestExample162()
        {
            // Expression : "thursday last week";

            var result = Chronox.ParseDateTime(Reference, Expressions[162]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[162]], date);
        }

        [TestMethod]
        public void TestExample163()
        {
            // Expression : "tomorrow at 6:45pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[163]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[163]], date);
        }

        [TestMethod]
        public void TestExample164()
        {
            // Expression : "4th day last week";

            var result = Chronox.ParseDateTime(Reference, Expressions[164]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[164]], date);
        }

        [TestMethod]
        public void TestExample165()
        {
            // Expression : "10-23-2017 at 10 pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[165]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[165]], date);
        }

        [TestMethod]
        public void TestExample166()
        {
            // Expression : "2 hours before monday";

            var result = Chronox.ParseDateTime(Reference, Expressions[166]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[166]], date);
        }

        [TestMethod]
        public void TestExample167()
        {
            // Expression : "4 hours to sunday";

            var result = Chronox.ParseDateTime(Reference, Expressions[167]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[167]], date);
        }

        [TestMethod]
        public void TestExample168()
        {
            // Expression : "5 hours after tuesday";

            var result = Chronox.ParseDateTime(Reference, Expressions[168]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[168]], date);
        }

        [TestMethod]
        public void TestExample169()
        {
            // Expression : "2 hours before noon";

            var result = Chronox.ParseDateTime(Reference, Expressions[169]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[169]], date);
        }

        [TestMethod]
        public void TestExample170()
        {
            // Expression : "2 hours to midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[170]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[170]], date);
        }

        [TestMethod]
        public void TestExample171()
        {
            // Expression : "2017-10-23 at 10 pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[171]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[171]], date);
        }

        [TestMethod]
        public void TestExample172()
        {
            // Expression : "24-10-2010 10:20 AM";

            var result = Chronox.ParseDateTime(Reference, Expressions[172]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[172]], date);
        }

        [TestMethod]
        public void TestExample173()
        {
            // Expression : "5 months before now";

            var result = Chronox.ParseDateTime(Reference, Expressions[173]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[173]], date);
        }

        [TestMethod]
        public void TestExample174()
        {
            // Expression : "afternoon yesterday";

            var result = Chronox.ParseDateTime(Reference, Expressions[174]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[174]], date);
        }

        [TestMethod]
        public void TestExample175()
        {
            // Expression : "february 14th, 2004";

            var result = Chronox.ParseDateTime(Reference, Expressions[175]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[175]], date);
        }

        [TestMethod]
        public void TestExample176()
        {
            // Expression : "four weeks from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[176]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[176]], date);
        }

        [TestMethod]
        public void TestExample177()
        {
            // Expression : "last sunday evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[177]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[177]], date);
        }

        [TestMethod]
        public void TestExample178()
        {
            // Expression : "next monday evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[178]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[178]], date);
        }

        [TestMethod]
        public void TestExample179()
        {
            // Expression : "three days from now";

            var result = Chronox.ParseDateTime(Reference, Expressions[179]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[179]], date);
        }

        [TestMethod]
        public void TestExample180()
        {
            // Expression : "yesterday afternoon";

            var result = Chronox.ParseDateTime(Reference, Expressions[180]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[180]], date);
        }

        [TestMethod]
        public void TestExample181()
        {
            // Expression : "4 days to next year";

            var result = Chronox.ParseDateTime(Reference, Expressions[181]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[181]], date);
        }

        [TestMethod]
        public void TestExample182()
        {
            // Expression : "2 days from tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[182]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[182]], date);
        }

        [TestMethod]
        public void TestExample183()
        {
            // Expression : "2017/10/22, 10:20 PM";

            var result = Chronox.ParseDateTime(Reference, Expressions[183]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[183]], date);
        }

        [TestMethod]
        public void TestExample184()
        {
            // Expression : "5 days from tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[184]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[184]], date);
        }

        [TestMethod]
        public void TestExample185()
        {
            // Expression : "january twenty-third";

            var result = Chronox.ParseDateTime(Reference, Expressions[185]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[185]], date);
        }

        [TestMethod]
        public void TestExample186()
        {
            // Expression : "last friday at 20:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[186]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[186]], date);
        }

        [TestMethod]
        public void TestExample187()
        {
            // Expression : "last week on tuesday";

            var result = Chronox.ParseDateTime(Reference, Expressions[187]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[187]], date);
        }

        [TestMethod]
        public void TestExample188()
        {
            // Expression : "sat 7 in the evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[188]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[188]], date);
        }

        [TestMethod]
        public void TestExample189()
        {
            // Expression : "Sun, Nov 2nd of 1990";

            var result = Chronox.ParseDateTime(Reference, Expressions[189]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[189]], date);
        }

        [TestMethod]
        public void TestExample190()
        {
            // Expression : "sunday november 26th";

            var result = Chronox.ParseDateTime(Reference, Expressions[190]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[190]], date);
        }

        [TestMethod]
        public void TestExample191()
        {
            // Expression : "twenty third of june";

            var result = Chronox.ParseDateTime(Reference, Expressions[191]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[191]], date);
        }

        [TestMethod]
        public void TestExample192()
        {
            // Expression : "within the next hour";

            var result = Chronox.ParseDateTime(Reference, Expressions[192]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[192]], date);
        }

        [TestMethod]
        public void TestExample193()
        {
            // Expression : "2 hours from midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[193]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[193]], date);
        }

        [TestMethod]
        public void TestExample194()
        {
            // Expression : "2 hours past midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[194]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[194]], date);
        }

        [TestMethod]
        public void TestExample195()
        {
            // Expression : "3 hours past midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[195]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[195]], date);
        }

        [TestMethod]
        public void TestExample196()
        {
            // Expression : "31st of december 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[196]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[196]], date);
        }

        [TestMethod]
        public void TestExample197()
        {
            // Expression : "4 days from yesterday";

            var result = Chronox.ParseDateTime(Reference, Expressions[197]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[197]], date);
        }

        [TestMethod]
        public void TestExample198()
        {
            // Expression : "5 minutes to midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[198]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[198]], date);
        }

        [TestMethod]
        public void TestExample199()
        {
            // Expression : "5 minutes to tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[199]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[199]], date);
        }

        [TestMethod]
        public void TestExample200()
        {
            // Expression : "8pm on monday evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[200]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[200]], date);
        }

        [TestMethod]
        public void TestExample201()
        {
            // Expression : "February twenty first";

            var result = Chronox.ParseDateTime(Reference, Expressions[201]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[201]], date);
        }

        [TestMethod]
        public void TestExample202()
        {
            // Expression : "next monday the third";

            var result = Chronox.ParseDateTime(Reference, Expressions[202]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[202]], date);
        }

        [TestMethod]
        public void TestExample203()
        {
            // Expression : "the day before sunday";

            var result = Chronox.ParseDateTime(Reference, Expressions[203]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[203]], date);
        }

        [TestMethod]
        public void TestExample204()
        {
            // Expression : "the third day of july";

            var result = Chronox.ParseDateTime(Reference, Expressions[204]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[204]], date);
        }

        [TestMethod]
        public void TestExample205()
        {
            // Expression : "february twenty-eighth";

            var result = Chronox.ParseDateTime(Reference, Expressions[205]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[205]], date);
        }

        [TestMethod]
        public void TestExample206()
        {
            // Expression : "Fourth day in November";

            var result = Chronox.ParseDateTime(Reference, Expressions[206]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[206]], date);
        }

        [TestMethod]
        public void TestExample207()
        {
            // Expression : "saturday at 20:40.0000";

            var result = Chronox.ParseDateTime(Reference, Expressions[207]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[207]], date);
        }

        [TestMethod]
        public void TestExample208()
        {
            // Expression : "the day after tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[208]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[208]], date);
        }

        [TestMethod]
        public void TestExample209()
        {
            // Expression : "in 3 months on the first friday";

            var result = Chronox.ParseDateTime(Reference, Expressions[209]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[209]], date);
        }

        [TestMethod]
        public void TestExample210()
        {
            // Expression : "first friday in 3 months";

            var result = Chronox.ParseDateTime(Reference, Expressions[210]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[210]], date);
        }

        [TestMethod]
        public void TestExample211()
        {
            // Expression : "five weeks ago on saturday";

            var result = Chronox.ParseDateTime(Reference, Expressions[211]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[211]], date);
        }

        [TestMethod]
        public void TestExample212()
        {
            // Expression : "ten months ago on saturday at 6pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[212]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[212]], date);
        }

        [TestMethod]
        public void TestExample213()
        {
            // Expression : "4 weeks ago on saturday";

            var result = Chronox.ParseDateTime(Reference, Expressions[213]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[213]], date);
        }

        [TestMethod]
        public void TestExample214()
        {
            // Expression : "saturday 4 weeks ago";

            var result = Chronox.ParseDateTime(Reference, Expressions[214]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[214]], date);
        }

        [TestMethod]
        public void TestExample215()
        {
            // Expression : "4 months ago on saturday at 6pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[215]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[215]], date);
        }

        [TestMethod]
        public void TestExample216()
        {
            // Expression : "saturday 4 months ago at 6pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[216]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[216]], date);
        }

        [TestMethod]
        public void TestExample217()
        {
            // Expression : "the monday after the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[217]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[217]], date);
        }

        [TestMethod]
        public void TestExample218()
        {
            // Expression : "the tuesday after the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[218]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[218]], date);
        }

        [TestMethod]
        public void TestExample219()
        {
            // Expression : "the monday before the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[219]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[219]], date);
        }

        [TestMethod]
        public void TestExample220()
        {
            // Expression : "the monday before the previous";

            var result = Chronox.ParseDateTime(Reference, Expressions[220]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[220]], date);
        }

        [TestMethod]
        public void TestExample221()
        {
            // Expression : "the wednesday before the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[221]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[221]], date);
        }

        [TestMethod]
        public void TestExample222()
        {
            // Expression : "the wednesday before the previous";

            var result = Chronox.ParseDateTime(Reference, Expressions[222]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[222]], date);
        }

        [TestMethod]
        public void TestExample223()
        {
            // Expression : "the wednesday after the previous";

            var result = Chronox.ParseDateTime(Reference, Expressions[223]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[223]], date);
        }

        [TestMethod]
        public void TestExample224()
        {
            // Expression : "the wednesday after this";

            var result = Chronox.ParseDateTime(Reference, Expressions[224]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[224]], date);
        }

        [TestMethod]
        public void TestExample225()
        {
            // Expression : "the monday after this one";

            var result = Chronox.ParseDateTime(Reference, Expressions[225]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[225]], date);
        }

        [TestMethod]
        public void TestExample226()
        {
            // Expression : "the sunday before the previous";

            var result = Chronox.ParseDateTime(Reference, Expressions[226]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[226]], date);
        }

        [TestMethod]
        public void TestExample227()
        {
            // Expression : "the sunday before the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[227]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[227]], date);
        }

        [TestMethod]
        public void TestExample228()
        {
            // Expression : "the day after the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[228]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[228]], date);
        }

        [TestMethod]
        public void TestExample229()
        {
            // Expression : "the week after the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[229]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[229]], date);
        }

        [TestMethod]
        public void TestExample230()
        {
            // Expression : "the day before the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[230]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[230]], date);
        }

        [TestMethod]
        public void TestExample231()
        {
            // Expression : "the week before the previous";

            var result = Chronox.ParseDateTime(Reference, Expressions[231]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[231]], date);
        }

        [TestMethod]
        public void TestExample232()
        {
            // Expression : "the day before the previous";

            var result = Chronox.ParseDateTime(Reference, Expressions[232]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[232]], date);
        }

        [TestMethod]
        public void TestExample233()
        {
            // Expression : "the month after the next";

            var result = Chronox.ParseDateTime(Reference, Expressions[233]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[233]], date);
        }

        [TestMethod]
        public void TestExample234()
        {
            // Expression : "first friday in two months";

            var result = Chronox.ParseDateTime(Reference, Expressions[234]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[234]], date);
        }

        [TestMethod]
        public void TestExample235()
        {
            // Expression : "the thursday before the previous";

            var result = Chronox.ParseDateTime(Reference, Expressions[235]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[235]], date);
        }

        [TestMethod]
        public void TestExample236()
        {
            // Expression : "first monday in two months";

            var result = Chronox.ParseDateTime(Reference, Expressions[236]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[236]], date);
        }

        [TestMethod]
        public void TestExample237()
        {
            // Expression : "second friday in two months";

            var result = Chronox.ParseDateTime(Reference, Expressions[237]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[237]], date);
        }

        [TestMethod]
        public void TestExample238()
        {
            // Expression : "second friday in one month";

            var result = Chronox.ParseDateTime(Reference, Expressions[238]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[238]], date);
        }

        [TestMethod]
        public void TestExample239()
        {
            // Expression : "third friday in two years";

            var result = Chronox.ParseDateTime(Reference, Expressions[239]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[239]], date);
        }

        [TestMethod]
        public void TestExample240()
        {
            // Expression : "second monday in three months";

            var result = Chronox.ParseDateTime(Reference, Expressions[240]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[240]], date);
        }

        [TestMethod]
        public void TestExample241()
        {
            // Expression : "third week in December";

            var result = Chronox.ParseDateTime(Reference, Expressions[241]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[241]], date);
        }

        [TestMethod]
        public void TestExample242()
        {
            // Expression : "3 days before next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[242]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[242]], date);
        }

        [TestMethod]
        public void TestExample243()
        {
            // Expression : "4 days after next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[243]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[243]], date);
        }

        [TestMethod]
        public void TestExample244()
        {
            // Expression : "1 hours before midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[244]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[244]], date);
        }

        [TestMethod]
        public void TestExample245()
        {
            // Expression : "2 hours before tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[245]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[245]], date);
        }

        [TestMethod]
        public void TestExample246()
        {
            // Expression : "3:45 on tuesday morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[246]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[246]], date);
        }

        [TestMethod]
        public void TestExample247()
        {
            // Expression : "5 minutes from tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[247]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[247]], date);
        }

        [TestMethod]
        public void TestExample248()
        {
            // Expression : "7 hours before tomorrow";

            var result = Chronox.ParseDateTime(Reference, Expressions[248]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[248]], date);
        }

        [TestMethod]
        public void TestExample249()
        {
            // Expression : "the day after next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[249]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[249]], date);
        }

        [TestMethod]
        public void TestExample250()
        {
            // Expression : "the second day of march";

            var result = Chronox.ParseDateTime(Reference, Expressions[250]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[250]], date);
        }

        [TestMethod]
        public void TestExample251()
        {
            // Expression : "the second week of july";

            var result = Chronox.ParseDateTime(Reference, Expressions[251]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[251]], date);
        }

        [TestMethod]
        public void TestExample252()
        {
            // Expression : "tomorrow during the day";

            var result = Chronox.ParseDateTime(Reference, Expressions[252]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[252]], date);
        }

        [TestMethod]
        public void TestExample253()
        {
            // Expression : "10 minutes past midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[253]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[253]], date);
        }

        [TestMethod]
        public void TestExample254()
        {
            // Expression : "4 days before next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[254]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[254]], date);
        }

        [TestMethod]
        public void TestExample255()
        {
            // Expression : "4 days before next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[255]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[255]], date);
        }

        [TestMethod]
        public void TestExample256()
        {
            // Expression : "4 days after next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[256]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[256]], date);
        }

        [TestMethod]
        public void TestExample257()
        {
            // Expression : "4 days before next year";

            var result = Chronox.ParseDateTime(Reference, Expressions[257]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[257]], date);
        }

        [TestMethod]
        public void TestExample258()
        {
            // Expression : "last friday of next year";

            var result = Chronox.ParseDateTime(Reference, Expressions[258]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[258]], date);
        }

        [TestMethod]
        public void TestExample259()
        {
            // Expression : "last monday of the month";

            var result = Chronox.ParseDateTime(Reference, Expressions[259]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[259]], date);
        }

        [TestMethod]
        public void TestExample260()
        {
            // Expression : "the day before yesterday";

            var result = Chronox.ParseDateTime(Reference, Expressions[260]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[260]], date);
        }

        [TestMethod]
        public void TestExample261()
        {
            // Expression : "the week after next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[261]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[261]], date);
        }

        [TestMethod]
        public void TestExample262()
        {
            // Expression : "3:45 on tuesday afternoon";

            var result = Chronox.ParseDateTime(Reference, Expressions[262]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[262]], date);
        }

        [TestMethod]
        public void TestExample263()
        {
            // Expression : "first friday of this year";

            var result = Chronox.ParseDateTime(Reference, Expressions[263]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[263]], date);
        }

        [TestMethod]
        public void TestExample264()
        {
            // Expression : "the fourth day of the next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[264]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[264]], date);
        }

        [TestMethod]
        public void TestExample265()
        {
            // Expression : "january twenty-third 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[265]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[265]], date);
        }

        [TestMethod]
        public void TestExample266()
        {
            // Expression : "monday the 3 of June 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[266]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[266]], date);
        }

        [TestMethod]
        public void TestExample267()
        {
            // Expression : "next sat 7 in the evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[267]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[267]], date);
        }

        [TestMethod]
        public void TestExample268()
        {
            // Expression : "the day before next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[268]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[268]], date);
        }

        [TestMethod]
        public void TestExample269()
        {
            // Expression : "the third day of the week";

            var result = Chronox.ParseDateTime(Reference, Expressions[269]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[269]], date);
        }

        [TestMethod]
        public void TestExample270()
        {
            // Expression : "this sat 7 in the morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[270]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[270]], date);
        }

        [TestMethod]
        public void TestExample271()
        {
            // Expression : "twenty third of june 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[271]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[271]], date);
        }

        [TestMethod]
        public void TestExample272()
        {
            // Expression : "fifth of may 2017 at 20:00";

            var result = Chronox.ParseDateTime(Reference, Expressions[272]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[272]], date);
        }

        [TestMethod]
        public void TestExample273()
        {
            // Expression : "second friday of next year";

            var result = Chronox.ParseDateTime(Reference, Expressions[273]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[273]], date);
        }

        [TestMethod]
        public void TestExample274()
        {
            // Expression : "second monday of the month";

            var result = Chronox.ParseDateTime(Reference, Expressions[274]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[274]], date);
        }

        [TestMethod]
        public void TestExample275()
        {
            // Expression : "sunday november 26 in 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[275]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[275]], date);
        }

        [TestMethod]
        public void TestExample276()
        {
            // Expression : "the 3 of June 2017 at 10pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[276]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[276]], date);
        }

        [TestMethod]
        public void TestExample277()
        {
            // Expression : "the day after next tuesday";

            var result = Chronox.ParseDateTime(Reference, Expressions[277]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[277]], date);
        }

        [TestMethod]
        public void TestExample278()
        {
            // Expression : "the fourth day of the year";

            var result = Chronox.ParseDateTime(Reference, Expressions[278]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[278]], date);
        }

        [TestMethod]
        public void TestExample279()
        {
            // Expression : "the last day of next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[279]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[279]], date);
        }

        [TestMethod]
        public void TestExample280()
        {
            // Expression : "the third day of next week";

            var result = Chronox.ParseDateTime(Reference, Expressions[280]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[280]], date);
        }

        [TestMethod]
        public void TestExample281()
        {
            // Expression : "the week third of december";

            var result = Chronox.ParseDateTime(Reference, Expressions[281]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[281]], date);
        }

        [TestMethod]
        public void TestExample282()
        {
            // Expression : "third saturday of the year";

            var result = Chronox.ParseDateTime(Reference, Expressions[282]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[282]], date);
        }

        [TestMethod]
        public void TestExample283()
        {
            // Expression : "third Thursday in November";

            var result = Chronox.ParseDateTime(Reference, Expressions[283]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[283]], date);
        }

        [TestMethod]
        public void TestExample284()
        {
            // Expression : "monday 10:30 in the morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[284]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[284]], date);
        }

        [TestMethod]
        public void TestExample285()
        {
            // Expression : "monday the 3rd of June 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[285]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[285]], date);
        }

        [TestMethod]
        public void TestExample286()
        {
            // Expression : "next week on monday morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[286]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[286]], date);
        }

        [TestMethod]
        public void TestExample287()
        {
            // Expression : "the day before next tuesday";

            var result = Chronox.ParseDateTime(Reference, Expressions[287]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[287]], date);
        }

        [TestMethod]
        public void TestExample288()
        {
            // Expression : "the first day of next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[288]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[288]], date);
        }

        [TestMethod]
        public void TestExample289()
        {
            // Expression : "the third week of last year";

            var result = Chronox.ParseDateTime(Reference, Expressions[289]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[289]], date);
        }

        [TestMethod]
        public void TestExample290()
        {
            // Expression : "the third week of next year";

            var result = Chronox.ParseDateTime(Reference, Expressions[290]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[290]], date);
        }

        [TestMethod]
        public void TestExample291()
        {
            // Expression : "third of june 2017 at 10 PM";

            var result = Chronox.ParseDateTime(Reference, Expressions[291]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[291]], date);
        }

        [TestMethod]
        public void TestExample292()
        {
            // Expression : "3rd of December 2022 at 10pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[292]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[292]], date);
        }

        [TestMethod]
        public void TestExample293()
        {
            // Expression : "December 3rd of 2022 at 10pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[293]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[293]], date);
        }

        [TestMethod]
        public void TestExample294()
        {
            // Expression : "sunday november 26th in 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[294]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[294]], date);
        }

        [TestMethod]
        public void TestExample295()
        {
            // Expression : "the 3rd of June 2017 at 10pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[295]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[295]], date);
        }

        [TestMethod]
        public void TestExample296()
        {
            // Expression : "the first week of last month";

            var result = Chronox.ParseDateTime(Reference, Expressions[296]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[296]], date);
        }

        [TestMethod]
        public void TestExample297()
        {
            // Expression : "the second week of last month";

            var result = Chronox.ParseDateTime(Reference, Expressions[297]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[297]], date);
        }

        [TestMethod]
        public void TestExample298()
        {
            // Expression : "the second week of this month";

            var result = Chronox.ParseDateTime(Reference, Expressions[298]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[298]], date);
        }

        [TestMethod]
        public void TestExample299()
        {
            // Expression : "the first week of this month";

            var result = Chronox.ParseDateTime(Reference, Expressions[299]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[299]], date);
        }

        [TestMethod]
        public void TestExample300()
        {
            // Expression : "The 25 April in the year 2008";

            var result = Chronox.ParseDateTime(Reference, Expressions[300]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[300]], date);
        }

        [TestMethod]
        public void TestExample301()
        {
            // Expression : "the second week of next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[301]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[301]], date);
        }

        [TestMethod]
        public void TestExample302()
        {
            // Expression : "first friday of the next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[302]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[302]], date);
        }

        [TestMethod]
        public void TestExample303()
        {
            // Expression : "last week on tuesday afternoon";

            var result = Chronox.ParseDateTime(Reference, Expressions[303]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[303]], date);
        }

        [TestMethod]
        public void TestExample304()
        {
            // Expression : "the day before yesterday at 10";

            var result = Chronox.ParseDateTime(Reference, Expressions[304]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[304]], date);
        }

        [TestMethod]
        public void TestExample305()
        {
            // Expression : "independence day during the day";

            var result = Chronox.ParseDateTime(Reference, Expressions[305]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[305]], date);
        }

        [TestMethod]
        public void TestExample306()
        {
            // Expression : "last day of the following month";

            var result = Chronox.ParseDateTime(Reference, Expressions[306]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[306]], date);
        }

        [TestMethod]
        public void TestExample307()
        {
            // Expression : "next week on thursday afternoon";

            var result = Chronox.ParseDateTime(Reference, Expressions[307]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[307]], date);
        }

        [TestMethod]
        public void TestExample308()
        {
            // Expression : "the third week of next february";

            var result = Chronox.ParseDateTime(Reference, Expressions[308]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[308]], date);
        }

        [TestMethod]
        public void TestExample309()
        {
            // Expression : "this sat the 7th in the evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[309]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[309]], date);
        }

        [TestMethod]
        public void TestExample310()
        {
            // Expression : "3 months ago saturday at 5:00 pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[310]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[310]], date);
        }

        [TestMethod]
        public void TestExample311()
        {
            // Expression : "first day of the following month";

            var result = Chronox.ParseDateTime(Reference, Expressions[311]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[311]], date);
        }

        [TestMethod]
        public void TestExample312()
        {
            // Expression : "twenty second day of the following month";

            var result = Chronox.ParseDateTime(Reference, Expressions[312]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[312]], date);
        }

        [TestMethod]
        public void TestExample313()
        {
            // Expression : "Friday the 21st of November 1997";

            var result = Chronox.ParseDateTime(Reference, Expressions[313]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[313]], date);
        }

        [TestMethod]
        public void TestExample314()
        {
            // Expression : "Sun, Nov 2nd of 1990 at 10:30 pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[314]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[314]], date);
        }

        [TestMethod]
        public void TestExample315()
        {
            // Expression : "The 22nd of May in the year 2010";

            var result = Chronox.ParseDateTime(Reference, Expressions[315]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[315]], date);
        }

        [TestMethod]
        public void TestExample316()
        {
            // Expression : "independence day during the night";

            var result = Chronox.ParseDateTime(Reference, Expressions[316]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[316]], date);
        }

        [TestMethod]
        public void TestExample317()
        {
            // Expression : "July, 15 of 2014 10:30:20.1000 PM";

            var result = Chronox.ParseDateTime(Reference, Expressions[317]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[317]], date);
        }

        [TestMethod]
        public void TestExample318()
        {
            // Expression : "next saturday 7:00 in the evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[318]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[318]], date);
        }

        [TestMethod]
        public void TestExample319()
        {
            // Expression : "second day of the following month";

            var result = Chronox.ParseDateTime(Reference, Expressions[319]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[319]], date);
        }

        [TestMethod]
        public void TestExample320()
        {
            // Expression : "this saturday at 7 in the evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[320]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[320]], date);
        }

        [TestMethod]
        public void TestExample321()
        {
            // Expression : "first monday of the previous month";

            var result = Chronox.ParseDateTime(Reference, Expressions[321]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[321]], date);
        }

        [TestMethod]
        public void TestExample322()
        {
            // Expression : "The 25th of April in the year 2008";

            var result = Chronox.ParseDateTime(Reference, Expressions[322]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[322]], date);
        }

        [TestMethod]
        public void TestExample323()
        {
            // Expression : "the twenty sixth day of next month";

            var result = Chronox.ParseDateTime(Reference, Expressions[323]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[323]], date);
        }

        [TestMethod]
        public void TestExample324()
        {
            // Expression : "monday the 3rd of June 2017 at 20pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[324]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[324]], date);
        }

        [TestMethod]
        public void TestExample325()
        {
            // Expression : "second monday of the previous month";

            var result = Chronox.ParseDateTime(Reference, Expressions[325]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[325]], date);
        }

        [TestMethod]
        public void TestExample326()
        {
            // Expression : "the 31 of december of the year 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[326]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[326]], date);
        }

        [TestMethod]
        public void TestExample327()
        {
            // Expression : "last monday the first in the morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[327]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[327]], date);
        }

        [TestMethod]
        public void TestExample328()
        {
            // Expression : "the 31st of december of the year 2017";

            var result = Chronox.ParseDateTime(Reference, Expressions[328]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[328]], date);
        }

        [TestMethod]
        public void TestExample329()
        {
            // Expression : "the following week on wednesday night";

            var result = Chronox.ParseDateTime(Reference, Expressions[329]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[329]], date);
        }

        [TestMethod]
        public void TestExample330()
        {
            // Expression : "may seventh '97 at three in the morning";

            var result = Chronox.ParseDateTime(Reference, Expressions[330]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[330]], date);
        }

        [TestMethod]
        public void TestExample331()
        {
            // Expression : "The 21 of April in the year 2008 at 10 pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[331]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[331]], date);
        }

        [TestMethod]
        public void TestExample332()
        {
            // Expression : "first friday of the following month at noon";

            var result = Chronox.ParseDateTime(Reference, Expressions[332]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[332]], date);
        }

        [TestMethod]
        public void TestExample333()
        {
            // Expression : "last monday of the previous month at midnight";

            var result = Chronox.ParseDateTime(Reference, Expressions[333]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[333]], date);
        }

        [TestMethod]
        public void TestExample334()
        {
            // Expression : "monday the 3rd of June of the year 2017 at 20pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[334]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[334]], date);
        }

        [TestMethod]
        public void TestExample335()
        {
            // Expression : "the 31 of december of the year 2017 at 10:31 pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[335]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[335]], date);
        }

        [TestMethod]
        public void TestExample336()
        {
            // Expression : "The thirtieth  of April in the year 2008 at 10 pm";

            var result = Chronox.ParseDateTime(Reference, Expressions[336]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[336]], date);
        }

        [TestMethod]
        public void TestExample337()
        {
            // Expression : "the thirty first of december of the year 2017 at 12 am";

            var result = Chronox.ParseDateTime(Reference, Expressions[337]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[337]], date);
        }

        [TestMethod]
        public void TestExample338()
        {
            // Expression : "fourteenth of june 2010 at eleven o'clock in the evening";

            var result = Chronox.ParseDateTime(Reference, Expressions[338]);

            var date = result[0].GetCurrent().DateTime();

            Assert.AreEqual(TestData[Expressions[338]], date);
        }
    }
}
