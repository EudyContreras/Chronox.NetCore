# Chronox.NetCore

#### NetCore Library for parsing relaxed and strict DateTime, TimeRange, TimeSpan and TimeSet expressions into discrete representations holding useful date and or time related data.

![Chronox NetCore NLP Image](https://github.com/EudyContreras/Chronox.NetCore/blob/master/NLP.png)


Chronox supports a large variety of relaxed date and time formats.
Here is an extensive list of most of the common date and time expressions supported by **Chronox**:

* 4pm
* 17:00
* 1800s
* night
* today
* dec 25
* Sunday
* 05/2003
* 10 to 8
* 3 hours
* 4 weeks
* 4:00:20
* at noon
* jan 1st
* sat 7am
* the day
* tonight
* 4:00 a.m
* may 27th
* mon 2:35
* november
* thursday
* tomorrow
* ten past two
* 4pm today
* afternoon
* day after
* half to 2 PM
* January 5
* last week
* next week
* right now
* this year
* yesterday
* 10/24/1979
* 2017/10/23
* 23-10-2017
* 3 jan 2000
* a week ago
* a year ago
* christmass
* friday 1pm
* in 3 hours
* in 3 weeks
* in 4 years
* jan 3 2010
* last night
* next month
* 1 oclock pm
* 3 years ago
* 7 hours ago
* at midnight
* half past 2
* Jan 21, '97
* Sept 5th, '06
* quater to 2
* Sun, Nov 21
* this second
* 1 week hence
* 22nd of june
* 4pm tomorrow
* 5:00pm yesterday
* fifth of may
* friday 13:00
* next january
* October 2006
* this morning
* this tuesday
* 4pm on monday
* december 31st
* last december
* last thursday
* next february
* next thursday
* this november
* this thursday
* tomorrow noon
* the day before
* 3 days from now
* 4 saturdays ago
* 3 saturdays after
* 3 mondays ago
* 3 mondays after at 6:39:30 PM
* next weekend
* last weekend
* the weekend,
* the weekend after
* the weekend before
* 4th of jan 2000
* 5 fridays after
* 7 days from now
* june the twelth
* tomorrow at ten
* tonight at 10pm
* within the hour
* 2 hours from now
* 3 fridays before
* 4 weeks from now
* 4pm next tuesday
* 5 hours from now
* 6 in the morning
* Fri, 21 Nov 1997      //Should I check if the 21st is a friday??
* monday the third      //Should I check if the third is a monday??
* Sunday next week
* the monday after
* tomorrow at 10pm
* tomorrow evening
* tomorrow morning
* the monday before
* one monday before
* three mondays after
* 5 days from today
* 6 tuesday morning
* december 31, 2017
* evening yesterday
* february 14, 2004
* fifth of may 2017
* last week tuesday
* one thirty two pm
* tuesday last week
* yesterday at 4:00
* 12 months from now
* 17 of april of '85
* 2 fridays from now
* 3 mondays from now
* 4 weeks from today
* 5 hours before now
* 5 minutes from now
* 8pm in the evening
* the 3 of June 2017
* the tuesday before
* the twelth of june
* third of june 2017
* thursday last week
* tomorrow at 6:45pm
* 4th day last week
* 10-23-2017 at 10 pm
* 2 hours before monday
* 4 hours to sunday
* 5 hours after tuesday
* 2 hours before noon
* 2 hours to midnight
* 2017-10-23 at 10 pm
* 24-10-2010 10:20 AM
* 5 months before now
* afternoon yesterday
* february 14th, 2004
* four weeks from now
* last sunday evening
* next monday evening
* three days from now
* 4 days to next year
* 2 days from tomorrow
* 2017/10/22, 10:20 PM
* january twenty-third
* last friday at 20:00
* last week on tuesday
* sat 7 in the evening
* Sun, Nov 2nd of 1990
* sunday november 26th      //Should i check that the 26 is a sunday
* twenty third of june
* within the next hour
* 2 hours from midnight
* 3 hours past midnight
* 31st of december 2016
* 4 days from yesterday
* 5 minutes to midnight
* 5 minutes to tomorrow
* 8pm on monday evening
* February twenty first
* next monday the third     //Should I look for a monday that falls on the third?
* the day before sunday
* the third day of july
* february twenty-eighth
* Fourth day in November
* saturday at 20:40
* the day after tomorrow
* in 3 months on the first friday
* first friday in 3 months
* 4 weeks ago on saturday
* saturday 4 weeks ago
* 4 months ago on saturday at 6pm
* saturday 4 months ago at 6pm
* the wednesday before the next
* the wednesday before the previous
* the wednesday after the previous
* the wednesday after this
* the monday after this one
* the sunday before the previous
* the sunday before the next
* the day after the next
* the week after the next
* the day before the next
* the week before the previous
* the day before the previous
* the month after the next
* the thursday before the previous
* first monday in two months
* second friday in two months
* third friday in two years
* third week in December
* 3 days before next week
* 4 days after next week
* 1 hours before midnight
* 2 hours before tomorrow
* 3:45 on tuesday morning
* 5 minutes from tomorrow
* 7 hours before tomorrow
* the day after next week
* the second day of march
* the second week of july
* tomorrow during the day
* 10 min past midnight
* 4 days before next week
* 4 days before next month
* 4 days after next month       //Does not count the first
* 4 days before next year
* last friday of next year
* last monday of the month
* the day before yesterday
* the week after next week
* 3:45 on tuesday afternoon
* first friday of this year
* the fourth day of the next week
* january twenty-third 2017
* monday the 3 of June 2017
* next sat 7 in the evening
* the day before next month
* the third day of the week
* this sat 7 in the morning
* twenty third of june 2017
* fifth of may 2016 at 20:00
* second friday of next year
* second monday of the month
* sunday november 26 in 2017
* the 3 of June 2017 at 10pm
* in 5 years and 3 months
* in five minutes and six hour
* in five minutes, six hours and 20 seconds
* in six weeks and 3 days
* 2 months, 4 week and 6 day ago
* 4 years and 6 days from now
* in 3 days and 4 hours and 30 seconds
* the day after next tuesday
* the fourth day of the year
* the last day of next month
* the third day of next week
* the week third of december
* third saturday of the year
* third Thursday in November
* monday 10:30 in the morning
* monday the 3rd of June 2017       //Should I make sure the 3rd is a monday or ignore!
* next week on monday morning
* the day before next tuesday
* the first day of next month
* the third week of next year
* third of june 2017 at 11 PM
* 3rd of December 2020 at 10pm
* December 3rd of 2022 at 12pm
* sunday november 26th in 2017      //Should I make sure the 26th is a monday or ignore!
* the 3rd of June 2017 at 10pm
* the first week of last month      //Should the week start on monday maybe?
* the second week of last month     //Should the week start on monday maybe?
* the first week of this month
* The 25 september in the year 2008
* first friday of the next month
* last week on tuesday afternoon
* the day before yesterday at 10
* independence day during the day
* last day of the following month
* next week on thursday afternoon
* the third week of next february
* this sat the 7th in the evening
* 3 months ago saturday at 5:00 pm
* third day of the following month
* twenty second day of the following month
* Friday the 21st of November 1997
* Sun, Nov 2nd of 1990 at 10:30 pm
* The 22nd of march in the year 2010
* independence day during the night
* July, 15 of 2014 10:30:20.1000 PM
* next saturday 7:00 in the evening
* second day of the following month
* this saturday at 7 in the evening
* The 25th of April in the year 2008
* the twenty sixth day of next month
* monday the 3rd of June 2017 at 20pm
* second monday of the previous month
* last monday the first in the morning
* the 31st of december of the year 2018
* the following week on wednesday night
* may seventh '97 at three in the morning
* The 21 of April in the year 2008 at 10 pm
* first friday of the following month at noon
* last monday of the previous month at midnight
* monday the 3rd of June of the year 2017 at 20pm
* the 31 of december of the year 2017 at 10:31 pm
* The thirtieth of April in the year 2008 at 10 pm
* the thirty first of december of the year 2017 at 12 am
* fourteenth of june 2010 at eleven o'clock in the evening

## How to use it?


### Future works


There are parts of this library that are yet to be finished and there are also some things which I plan to add to the library. These things will be shown here along with popular demands

- [ ] **More options to the main menu so that users have more control through the interface**.
- [ ] **Maybe an online multiplayer option for users to test their agents remotely in competitions**




### Built With


![Net.Core Logo](https://github.com/EudyContreras/Chronox.NetCore/blob/master/netcore.png)
* [Net.Core](https://en.wikipedia.org/wiki/.NET_Core)






### Contributing


Please read [Contributing](https://github.com/EudyContreras/Chronox.NetCore/blob/master/CONTRIBUTING) for details on the code base code of conduct, and the process for submitting pull requests to **OthelloFX**





### Authors


* **Eudy Contreras** 





### Contact Info


If any questions regarding this program fell free to reach me at.
EudyContrerasRosario@gmail.com







### Disclaimer

All background images including the logo were not made by me and I do not claim ownership of these images. I would like to thank the awesome artists and creators of the images for making them public. If there is any problem with the use of these images please contact me so we can solve it. Once again. props to the artists.





### License

This project is licensed under the MIT License - see the [Licence](https://github.com/EudyContreras/Chronox.NetCore/blob/master/LICENSE) file for details

------------
    The MIT License (MIT)
    
    Copyright (c) 2018 Eudy Contreras
    
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
