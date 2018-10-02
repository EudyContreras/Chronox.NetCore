# Chronox.NetCore

#### NetCore Library for parsing relaxed and strict DateTime, TimeRange, TimeSpan and TimeSet expressions into discrete representations holding useful date and or time related data. 

![Chronox NetCore NLP Image](https://github.com/EudyContreras/Chronox.NetCore/blob/master/NLP.png)


**Chronox** supports a large variety of relaxed date and time formats. Here is an extensive list of most of the common date and time expressions supported by **Chronox**:


## How to use it?

#### Instantiation ####
A `Chronox` instance can be created in the following way without specifying any settings. Please note that Chronox uses the singleton pattern. Most of the work done by `Chronox` happens upon instatiation which is when data sets are loaded, sequences are created and loaded and where dictionaries are indexed.

```c#
var Chronox = ChronoxParser.GetInstance();
```
Or like this in which custom settings are specified upon instatiation.
```c#
var Chronox = ChronoxParser.GetInstance(new ChronoxSettings("english"));
```


#### Settings ####

A `ChronoxSettings` can be instantiated with the name of languages you wish to support. 
```c#
var settings = new ChronoxSettings("English", "Spanish")
```
A `ChronoxSettings` can also be instatiated with a custom collectios of sequences you wish support. For more information about sequence codes and additional ways for supporting custom sequences please refer to the #dataSet section and to the sequence library.

```c#

var sequences = new string[]{"D.O.W|D.T|T.M|T.O.D", "D.O.W|D.T|T.M|T.O.D|T.Z", "G.E|D.O.W|D.T|T.M"};
  
var sequenceCollection = new SequenceCollection(SequenceType.DateTime,sequences);
  
var settings = new ChronoxSettings(sequenceCollection,"English", "Spanish")

```

The settings is still in WIP. More options will be added and some may be removed.


| Setting                    | Values                                   | Description                                     |
| ----------------------------| ---------------------------------------- | ------------------------------------------------|
| **SearchPassCount**         | 1 - 10                                   | Number passes to perform on string.          	   
| **MinInputTextLength**      | 3 - Int.MaxValue                         | The minimum amount of text to work with  | 
| **PrefferedLanguages**      | English, Spanish, etc                    | Languages the programs will interpret          |
| **PreferedYear**            | 0 - 10000                                | The year to set upon no year found        |
| **PreferedMonth**           | 1 - 12                                   | The month to set upon no month found        |
| **PreferedDay**             | 1 - 31                                   | The day to set upon no day found    |
| **PreferedHour**            | 1 - 24                                   | The hour to set upon no hour found          |
| **PreferedMinute**          | 1 - 60                                   | The minute to set upon no minute found         |
| **PreferedSecond**          | 1 - 60                                   | The second to set upon no second found   |
| **PreferedTimeZone**        | UTC, CET, etc                            | Sets the scale of of the window.		             |
| **TimeRelationResolver**    | Future, Past, Present                    | Strict to specified relational time            |
| **AmbigousResultResolver**  | Null, Any                                | Determines what to return upon ambiguity       |
| **NoFoundResultResolver**   | Null, Implied, Current                   | What to return upon no result found        |
| **InvalidInputResolver**    | Null, Empty, Exception                   | What to return upon no invalid input       |
| **ParsingMode**             | DateTime, TimeRange, etc                 | The mode the parser should use: Guess otherwise |
| **RelaxLevel**              | Formal, Casual, Any                      | Type of strings the parse should parse		   |
| **PrefferedEndian**         | MiddleEndian, LittleEndian               | Endian to use upon ambiguous date format   |
| **PrefferedDay**            | Current, Start, End, Previous, Next      | Offset day if could not be determined     
| **StartOfWeek**             | Monday, Tuesday, etc                     | The day in which a week starts            |


#### Parsing input ####
In order to parse input `Chronox` can also be called statically like this:

```c#

var input  = "Meet me the day after tomorrow at eight in the evening at the mall near your house" 

var result = null;

result = ChronoxParser.Parse(input);

ChronoxParser.TryParse(input, out result);

```

You can also specify what you attempt to extract from the input by specifying which parsing method to use: If Parse is used the parser will attempt to determine the type. **Please** use the exact parsing for more accuracy and less overhead.

```c#

result = ChronoxParser.ParseDateTime(settings, input);
result = ChronoxParser.ParseTimeSpan(settings, input);
result = ChronoxParser.ParseTimeRange(settings, input);
result = ChronoxParser.ParseTimeSet(settings, input);

```

#### Result ####

A `ChronoxResult` contains the result data extracted by the `ChronoxParser`. The result obtain from a parsing operation is of type `IChronoxExtraction`. A result may contain many different extractions. In the case of none exact parsing a `ResultWrapper` is returned which contains the data extracted if any. In the case of **exact parsing** a predetermined extraction type is returned. An `IChronoxExtraction` contains the following properties

* `RetultType` The type of the extraction which could be a `DateTime`, `TimeSpan`, etc
* `ProcessedString`  The string after it has been processed which is stripped of the extraction areas. 
* `Original` The original string which was passed to the parser 
* `Extraction`  The actual extraction or representation of the parsed data
* `StartIndex`  The start index at which the extraction happened
* `EndIndex`  The end index at which the extraction happened

#### Result interpration ####


```c#

var referenceDate = new DateTime(year: 2018, month: 10, day: 2, hour: 2, minute: 22, second: 14);

var input  = "Meet me the day after tomorrow at eight in the evening at the mall near your house" 

result = ChronoxParser.ParseDateTime(referenceDate, input);

```

Where `refereceDate` = **{10/2/18 2:22:14 AM}**

Where `input` = **"Meet me the day after tomorrow at eight in the evening at the mall near your house"** 
  
The `result` yields :

* `DateTime`: **{2018-10-4 20:0:0:0}**
* `RetultType`: **"The type of the extraction which could be a `DateTime`, `TimeSpan`, etc"**
* `ProcessedString`:  **"Meet me the  at the mall near your house"**
* `Original`: **"Meet me the day after tomorrow at eight in the evening at the mall near your house"**
* `Extraction`:  **"day after tomorrow at eight in the evening"**
* `StartIndex`:  **12**
* `EndIndex`:  **53**

## How does Chronox works?


## How to add unsupported formats or sequences?


## How to add support for a language?


## How to add a pre-processor?


## How to define and add additonal pre-processors?


## Dealing with conflict or ambiguity:


## Known bugs:


## Potential conflicts or bugs:


## Parser result:


## Component types:

**Chronox** is meant to be able to handle date and time paring but it can also handle time ranges, recurring time sets, durations or time spans and timezone conversion.

* **ChronoxDate** : *Comonent which holds date related data*
* **ChronoxTime** : *Comonent which holds time related data*
* **ChronoxDateTime** : *Comonent which holds date and time related data*
* **ChronoxTimeRange** : *Comonent which holds time range related data*
* **ChronoxTimeSet** : *Comonent which holds recurring time event related data*
* **ChronoxTimeSpan** : *Comonent which holds durationg/span related data*
* **ChronoxTimeZon** : *Comonent which holds timezone related data*


## Things to note:

* Chronox is not thread safe but plans to be in a near future.
* If no reference date is specified, then the current date and time will be used.
* If no time is specified on any date which differs in month, day or year in relation to the reference day, the time will be set at 12am.
* If a month is specified without any specified day the day will be set to the first of said month.
* If a proceding or preciding week is specified without any specified day, the day will be set to monday both for previous or following weeks
* Time expressions are constant for now: noon = 12:00pm morning = 6:00am etc
* Expressions such as fourth week of x month will be parsed on intervals of week starting with the day where monday falls
* If no day is specified in expressions such as "next month" the day will be set to the first of said month.
* If no day is specified in expressions such as "in 4 months", the dayofweek will be left the same,


## Future works:


There are parts of this library that are yet to be finished and there are also some things which I plan to add to the library. These things will be shown here along with popular demands

- [ ] **Make into nuget package**
- [ ] **Add thread safety**
- [ ] **Improve loading performance**
- [ ] **Add better support to multi-extraction**
- [ ] **Finish adding support to TimeSpan**
- [ ] **Finish adding support to TimeSet**
- [ ] **Finish adding support to TimeRange**
- [ ] **A bunch of other things**

## Contribute:


Please read [Contributing](https://github.com/EudyContreras/Chronox.NetCore/blob/master/CONTRIBUTING) for details on the code base code of conduct, and the process for submitting pull requests to **OthelloFX**





## Authors:


* **Eudy Contreras** 





## Contact:


If any questions regarding this program fell free to reach me at.
EudyContrerasRosario@gmail.com







## Disclaimer:

* This program was made as a hoby and not for professional or commercial use in mind. This program may be subjected to architectural changes and it is not perfect. Use at own risk.

* All background images including the logo were not made by me and I do not claim ownership of these images. I would like to thank the awesome artists and creators of the images for making them public. If there is any problem with the use of these images please contact me so we can solve it. Once again. props to the artists.




## Tech used:


![Net.Core Logo](https://github.com/EudyContreras/Chronox.NetCore/blob/master/netcore.png)
* [Net.Core](https://en.wikipedia.org/wiki/.NET_Core)





## License:

This project is licensed under the MIT License - see the [Licence](https://github.com/EudyContreras/Chronox.NetCore/blob/master/LICENSE.md) file for details
