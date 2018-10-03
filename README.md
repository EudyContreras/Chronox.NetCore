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


`ChronoxSettings` is a comprehensive set of options that the `ChronoxParser` should use in order to achieve the desired results in concordance to the application in play. 


| Setting                    | Values                                   | Description                                     |
| ----------------------------| ---------------------------------------- | ------------------------------------------------|
| **SearchPassCount**         | 1 - 10                                   | Number passes to perform on string.         |   
| **MinInputTextLength**      | 3 - Int.MaxValue                         | The minimum amount of text to work with  | 
| **PrefferedLanguages**      | English, Spanish, etc                    | Languages the programs will interpret          |
| **PreferedYear**            | 0 - 10000                                | The year to set upon no year found        |
| **PreferedMonth**           | 1 - 12                                   | The month to set upon no month found        |
| **PreferedDay**             | 1 - 31                                   | The day to set upon no day found    |
| **PreferedHour**            | 1 - 24                                   | The hour to set upon no hour found          |
| **PreferedMinute**          | 1 - 60                                   | The minute to set upon no minute found         |
| **PreferedSecond**          | 1 - 60                                   | The second to set upon no second found   |
| **PreferedTimeZone**        | UTC, CET, etc                            | Sets the scale of of the window.		   |
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

A `ChronoxResult` contains the result data extracted by the parser. The result obtain from a parsing operation is of type `IChronoxExtraction`. A result may contain many different extractions. In the case of none exact parsing a `ResultWrapper` is returned which contains the data extracted if any. In the case of **exact parsing** a predetermined extraction type is returned. The extraction contains the following properties

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
  
The `output` = :

* `DateTime`: **{2018-10-4 20:0:0:0}** 
* `RetultType`: **"The type of the extraction which could be a `DateTime`, `TimeSpan`, etc"**
* `ProcessedString`:  **"Meet me the  at the mall near your house"**
* `Original`: **"Meet me the day after tomorrow at eight in the evening at the mall near your house"**
* `Extraction`:  **"day after tomorrow at eight in the evening"**
* `StartIndex`:  **12**
* `EndIndex`:  **53**

## How does Chronox works?

`Chronox` uses a combination of techniques in order to work. At the top level it uses pre-processors which will look at the string and attempt to pre-process it in order to make parsing and pattern matching easier. Eg: A pre-processor or `IChronoxScanner` is used to convert any numeric value written in words to its numeric representation. Additional pre-processors may be used in order to satisfy the locale and or parsing needs of an application.

Under the hood `Chronox` uses **REGEX** for matching specific patterns along side translation methods. Various entity extraction methods are used in order to effetively find and parse the desired data. 

**Compiling performance:**

Chronox compilation time is relative to the amount of sequences supported as well the amount of datasets added to it. The amount of variations added to a dataset will affect the the compilation time too.

**Parsing performance:**

Due to the fact that most of the work done by `Chronox` happens when it is built parsing times are pretty fast. The usual parsing time does not exceed 1 millisecond. There are some edge cases in which chronox may take up to 60 milliseconds in order to return a result. The lenght of the string and the amount of pre-processors/scanners used will also affect the parsing performance. `Chrono` in its current state can be further optimized and it will be optimized in a near future.

## Datasets and Instructions:

In order to add support to a language `Chronox` uses datasets which contain instructions which the system uses in order to parse a given input. For a DataSet template please please refere to: [Template]. The dataset allowes the user to deal with some language specific rules and exceptions.

In order to achieve the best result please read the guidelines and descriptions. The language files work directly with the parser in order to provide support for different laguages. If you wish add to support to a language simply specified the equivalent variations to be used by the parser when converting, along with the optional regex patterns for the variations.
In addition you may also provide additional information in order to inrease parsing accuracy. 

**Keywords:**

* **`Modifiable`**: Tells if the the property should be or could be modified by the user.
* **`Unsupported`**: Tells if the property is currently supported.

**Language:**
	
* **`language`**: The name of the language being used: [Modifiable: Yes]
* **`ignored`**: List of words or to ignore: [Modifiable: Yes]
* **`assumeSpace`**: "Determines if the parser automatically add spaces between properties: [Modifiable: Yes]"
* **`sectionTypes`**: List of types supported by sections: [Modifiable: No]
* **`propertyTypes`**: List of types supported by properties: [Modifiable: No]
* **`supportedSectionAbreviations`**: List of abreviations which could be used for creating date formats: [Modifiable: No]
* **`supportedDateFormats`**: List of date formats to look for when parsing: [Modifiable: Yes]
	
**Section:**
	
* **`label`**: The name or label of the section: [Modifiable: No]
* **`type`**: The type of the section "Must be a supported type": [Modifiable: At own risk!]
	
**Property:**
	
* **`key`**: The key name that identifies the property: [Modifiable: No]
* **`value`**: The value associated with the key: [Unsupported]
* **`type`**: The type of the property "Must be a supported type" : [Modifiable: At own risk!]
* **`pattern`**: The pattern associated to the variations:[Modifiable: Yes]
* **`variations`**: The user submitted synonyms or representations of the key which must also be included in the specified language: [Modifiable: Yes]

**Section Type Glossary:** 

* **`N/A`**: Not specified or not available.
* **`Combined`**: Combines the properties as one single group. Typically used for properties belonging to the same category
* **`CombinedOptional`**: Same as `Combined`. The properties will be optinal and not enforced.
* **`CombinedReversed`**: Same as `Combined`. Reverses the combination order. Typically used to avoid overlap: Such as finding: "four" in "twenty four"

**Property Type Glossary:** 

* **`N/A`**: Not specified or not available
* **`Group`**: It will turn the property into a recognizable group which can be refered to for information "Ignored if the parent section is of type "Combined" or "CombinedReversed""
* **`GroupOptional`**: It will turn the property into a recognizable group which can be refered to for information "Ignored if the parent section is of type "Combined" or "CombinedReversed""
* **`Optional`**: Specifies that the property is optinal and may or may not be present in a date format
* **`Filler`**: Placeholder property which binds groups together or simply fills a space: "No information directly extracted but it is part of a date pattern"
* **`Interpreted`**: Expression which can be directly translated into a DateTime object Ex: "Tonight, Now, Last night"

**Regex Cheat Sheet**

* **`Properties`**: monday, mon
* **`Regex`**: mon(?:day) or monday|mon : these regex expecifies that the both Mon or Moday can be matched

* **`Properties`**: one, first
* **`Regex`**: one|first : this regex specifies that either one or first can be matched

For more information about Regular Expression **REGEX** please visit: [Regex Documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference)

## How to add unsupported formats or sequences?

In order to add a date format or sequence which is not currently supported by `Chronox` all that needs to be done is to specify said format using a combination of section codes. For a full list of the available section codes please look at the table below:

Must commonly known as well as some less common formats are already supported by `Chronox`. For a full list of supported format please look at the #FormatDocument. Feel free to play around with the formats in order to add support to your format.

Formats can be submitted to `Chronox` in two ways. The first way is that a sequence collection can be added when creating a `ChronoxSettings`. 

```c#

var sequences = new string[]{"D.O.W|D.T|T.M|T.O.D", "D.O.W|D.T|T.M|T.O.D|T.Z", "G.E|D.O.W|D.T|T.M"};
  
var sequenceCollection = new SequenceCollection(SequenceType.DateTime,sequences);
  
var settings = new ChronoxSettings(sequenceCollection,"English", "Spanish")

```

The other option is to specifiy directly on the dataset which formats you wish to support.

```c#
supportedDateTimeFormats:

	D.O.W
	D.O.W|D.T|T.M
	D.O.W|D.T|T.M|T.Z
	D.O.W|D.T|T.M|T.O.D
	D.O.W|D.T|T.M|T.O.D|T.Z
	G.E|D.T.U|N.O|M.O.Y|Y.U|Y|T.O.D|T.M
	G.E|D.O.W|D.T|T.M
	N|M.O.Y|G.E|Y.U
	G.E|D.O.W|T.E
	D.O.W|N|M.O.Y
	M.O.Y|N
	D.O|N|T.U.B|T.C|T.E
	N|T.U.B|T.C|T.E|D.O

supportedTimeRangeFormats:

supportedTimespanFormats:

supportedTimeSetFormats:

```

| Section code			|  Representation description		|    Example cases				|
| ------------------------------| --------------------------------------| ----------------------------------------------|
|	        C.E  			|        Casual Expressions		|
|	        G.E   			|        Grabber Expressions		|This, next, last, etc|
|	        D.O   			|        DayOffset			|Tomorrow, Today, Yesterday, etc|
|	        T.E   			|        Time Expressions		|Noon, Midnight, etc|
|	        T.F   			|        Time Fractions			|Quater, Half, etc|
|	        T.C   			|        Time Conjointer		|Ago, to, from now, etc|
|	        I.E   			|        Interpreted Expression 	|Tonight, now, last night, etc|
|	        R.I   			|        Range Indicator		|From, etc|
|	        R.S   			|        Range Separator		|To, -, etct|
|	        N.V   			|        Numeric Value			|0,1,2,3, etc|
|	        N    			|        Number				|Twenty one, twenty-one, etc|
|	        N.O   			|        Ordinal			|21st, twenty first|
|	        D.N   			|        Decimal Number			|Thrity two point five, etc|
|	        T.O.D 			|        Time Of Day			|Monrning, afternoon, evening, night|
|	        D.O.W 			|        Days Of Week			|Monday, Tuesday, etc|
|	        S.O.Y 			|        Season Of Year			|Spring, autum, etc|
|	        M.O.Y 			|        Months Of Year			|July, December, etc|
|	        R.E.I 			|        Repeater Indicators		|Every, each, etc|
|	        D.E.I 			|        Duration Indicators		|Whole, all, half, etc|
|	        R.E   			|        Repeater Expressions		|Hourly, weekly, etc|
|	        D.E   			|        Duration Expressions		|For, etc|
|	        P.T   			|        Proximity			|Exactly, preciesly, approximately, etc|
|	        D.T.U 			|        Date Time Units		|Minute, hour, year, week, etc|
|	        T.P   			|        Time Periods			|Decade, century, milleniumn, etc|
|	        D.V   			|        Decade Values			|Nineties, senventies, etc|
|	        A.O   			|        Arithmetic Operator		|-, +|
|	        L.O   			|        Logical Operator		|And, or|
|	        D.D   			|        Date				|10/24,2018, 2018,10,24, etc|
|	        D.T   			|        Time				|22:10:33, 12:10:44, etc|
|	        D.Y   			|        Year				|2010, '98, etc|
|	        Y     			|        Year Discrete			|2010, '98, etc|
|	        M     			|        Month Discrete			|10, etc|
|	        D,     			|        Day Discrete			|24, etc|
|	        H.D,   			|        Hour Discrete			|12, etc|
|	        M.D   			|        Minute Discrete		|44, etc|
|	        S.D   			|        Second Discrete		|59, etc|
|	        D.D.B 			|        Date Big Endian		|YYYY|MM|DD|
|	        D.D.L 			|        Date Little Endian		|DD|MM|YYYY|
|	        D.D.M 			|        Date Middle Endian		|MM|DD|YYYY|
|	        W.S   			|        Expression Separator		| |
|	        D.W.T 			|        Day Of Week Type		| |
|	        T.M   			|        Time Meridiam			|AM, pm, etc|
|	        D.U.B 			|        Date Units			|year, months, weeks, days, etc|
|	        T.U.B 			|        Time Units			|Hour, minute, second, etc|
|	        Y.U   			|        Year Unit			|year, years,|
|	        M.U   			|        Month Unit			|Month, months,|
|	        W.U   			|        Week Unit			|Week, weeks, W, etc|
|	        D.U   			|        Day Unit			|Day, days, D, etc|
|	        H.U   			|        Hour Unit			|Hour, h, etc|
|	        M.I.U 			|        Minute Unit			|Minute, min, m, etc|
|	        S.U   			|        Second Unit			|Second, s, etc|
|	        T.Z"   			|        Time Zone			|UTC, EST, etc|
|	        D.T.S 			|        Time Separator			|:|
|	        D.D.S 			|        Date Separator			|/,-, etc|
|	        N.M.2.D			|        Number Max 2 Digits		|1, 12, 44, etc|
|	        N.M.4.D			|        Number Max 4 Digits		|23, 2323, 321, etc|
|	        N.M.5.D			|        Number Max 5 Digits		|232, 12, 43454, etc|

## How to define and add additonal pre-processors?


## Dealing with conflict or ambiguity:


## Known and potential bugs:


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
* If no time is specified on any date which differs in month, day or year in relation to the reference day, the time will be set to 12am.
* If a month is specified without any specified day the day will be set to the first of said month.
* If a proceding or preciding week is specified without any specified day, the day will be set to monday both for previous or following weeks
* Time expressions are constant for now: noon = 12:00pm morning = 6:00am etc
* Expressions such as fourth week of x month will be parsed on intervals of week starting with the day where monday falls
* If no day is specified in expressions such as "next month" the day will be set to the first of said month.
* If no day is specified in expressions such as "in 4 months", the dayofweek will be left the same,
* The settings is still in WIP. More options will be added and some may be removed.

## Future works:


There are parts of this library that are yet to be finished and there are also some things which I plan to add to the library. These things will be shown here along with popular demands

- [ ] **Make into nuget package**
- [ ] **Add thread safety**
- [ ] **Improve loading performance**
- [ ] **Allow post-compilation modifications to Chronox**
- [ ] **Add better support to multi-extraction**
- [ ] **Finish adding support to TimeSpan**
- [ ] **Finish adding support to TimeSet**
- [ ] **Finish adding support to TimeRange**
- [ ] **A bunch of other things**


## Supported timzones:

**Chronox** supports most if not all Timezones. If a timezone is found it will be extracted. A **Timezone** object can be added or substracted from the result in order get an accurate timezone sensitve time


| Timzone Code                    | Timezone Offset        | Description    |              UTC Offset                    |
| --------------------------------| -----------------------|----------------| -------------------------------------------|
|A  |  60  |  Alpha Time Zone  |  UTC+1
|ACDT  |  630  |  Australian Central Daylight Time  |  UTC+10:30				|
|ACST  |  570  |  Australian Central Standard Time  |  UTC+9:30|
|ACT  |  -300  |  Acre Time  |  UTC-5|
|ACWST  |  525  |  Australian Central Western Standard Time  |  UTC+8:45|
|ADT  |  -180  |  Atlantic Daylight Time  |  UTC-3|
|AEDT  |  660  |  Australian Eastern Daylight Time  |  UTC+11|
|AEST  |  600  |  Australian Eastern Standard Time  |  UTC+10|
|AFT  |  270  |  Afghanistan Time  |  UTC+4:30|
|AKDT  |  -480  |  Alaska Daylight Time  |  UTC-8|
|AKST  |  -540  |  Alaska Standard Time  |  UTC-9|
|ALMT  |  360  |  Alma-Ata Time  |  UTC+6|
|AMST  |  -180  |  Amazon Summer Time  |  UTC-3|
|AMT  |  -240  |  Amazon Time  |  UTC-4|
|ANAST  |  720  |  Anadyr Summer Time  |  UTC+12|
|ANAT  |  720  |  Anadyr Time  |  UTC+12|
|AQTT  |  300  |  Aqtobe Time  |  UTC+5|
|ART  |  -180  |  Argentina Time  |  UTC-3|
|AST  |  -240  |  Atlantic Standard Time  |  UTC-4|
|AWDT  |  540  |  Australian Western Daylight Time  |  UTC+9|
|AWST  |  480  |  Australian Western Standard Time  |  UTC+8|
|AZOST  |  0  |  Azores Summer Time  |  UTC+0|
|AZOT  |  -60  |  Azores Time  |  UTC-1|
|AZST  |  300  |  Azerbaijan Summer Time  |  UTC+5|
|AZT  |  240  |  Azerbaijan Time  |  UTC+4|
|B  |  120  |  Bravo Time Zone  |  UTC+2|
|BNT  |  480  |  Brunei Darussalam Time  |  UTC+8|
|BOT  |  -240  |  Bolivia Time  |  UTC-4|
|BRST  |  -120  |  Bras韑ia Summer Time  |  UTC-2|
|BRT  |  -180  |  Bras韑ia Time  |  UTC-3|
|BST  |  60  |  British Summer Time  |  UTC+1|
|BTT  |  360  |  Bhutan Time  |  UTC+6|
|C  |  180  |  Charlie Time Zone  |  UTC+3|
|CAST  |  480  |  Casey Time  |  UTC+8|
|CAT  |  120  |  Central Africa Time  |  UTC+2|
|CCT  |  390  |  Cocos Islands Time  |  UTC+6:30|
|CDT  |  -300  |  Central Daylight Time  |  UTC-5|
|CEST  |  120  |  Central European Summer Time  |  UTC+2|
|CET  |  60  |  Central European Time  |  UTC+1|
|CHADT  |  825  |  Chatham Island Daylight Time  |  UTC+13:45|
|CHAST  |  765  |  Chatham Island Standard Time  |  UTC+12:45|
|CHOST  |  540  |  Choibalsan Summer Time  |  UTC+9|
|CHOT  |  480  |  Choibalsan Time  |  UTC+8|
|CHUT  |  600  |  Chuuk Time  |  UTC+10|
|CIDST  |  -240  |  Cayman Islands Daylight Saving Time  |  UTC-4|
|CIST  |  -300  |  Cayman Islands Standard Time  |  UTC-5|
|CKT  |  -600  |  Cook Island Time  |  UTC-10|
|CLST  |  -180  |  Chile Summer Time  |  UTC-3|
|CLT  |  -240  |  Chile Standard Time  |  UTC-4|
|COT  |  -300  |  Colombia Time  |  UTC-5|
|CST  |  -360  |  Central Standard Time  |  UTC-6|
|CVT  |  -60  |  Cape Verde Time  |  UTC-1|
|CXT  |  420  |  Christmas Island Time  |  UTC+7|
|ChST  |  600  |  Chamorro Standard Time  |  UTC+10|
|D  |  240  |  Delta Time Zone  |  UTC+4|
|DAVT  |  420  |  Davis Time  |  UTC+7|
|DDUT  |  600  |  Dumont-d'Urville Time  |  UTC+10|
|E  |  300  |  Echo Time Zone  |  UTC+5|
|EASST  |  -300  |  Easter Island Summer Time  |  UTC-5|
|EAST  |  -360  |  Easter Island Standard Time  |  UTC-6|
|EAT  |  180  |  Eastern Africa Time  |  UTC+3|
|ECT  |  -300  |  Ecuador Time  |  UTC-5|
|EDT  |  -240  |  Eastern Daylight Time  |  UTC-4|
|EEST  |  180  |  Eastern European Summer Time  |  UTC+3|
|EET  |  120  |  Eastern European Time  |  UTC+2|
|EGST  |  0  |  Eastern Greenland Summer Time  |  UTC+0|
|EGT  |  -60  |  East Greenland Time  |  UTC-1|
|EST  |  -300  |  Eastern Standard Time  |  UTC-5|
|ET  |  -300  |  Eastern Time  |  UTC-5:00 / -4:00|
|F  |  360  |  Foxtrot Time Zone  |  UTC+6|
|FET  |  180  |  Further-Eastern European Time  |  UTC+3|
|FJST  |  780  |  Fiji Summer Time  |  UTC+13|
|FJT  |  720  |  Fiji Time  |  UTC+12|
|FKST  |  -180  |  Falkland Islands Summer Time  |  UTC-3|
|FKT  |  -240  |  Falkland Island Time  |  UTC-4|
|FNT  |  -120  |  Fernando de Noronha Time  |  UTC-2|
|G  |  420  |  Golf Time Zone  |  UTC+7|
|GALT  |  -360  |  Galapagos Time  |  UTC-6|
|GAMT  |  -540  |  Gambier Time  |  UTC-9|
|GET  |  240  |  Georgia Standard Time  |  UTC+4|
|GFT  |  -180  |  French Guiana Time  |  UTC-3|
|GILT  |  720  |  Gilbert Island Time  |  UTC+12|
|GMT  |  0  |  Greenwich Mean Time  |  UTC+0|
|GST  |  240  |  Gulf Standard Time  |  UTC+4|
|GYT  |  -240  |  Guyana Time  |  UTC-4|
|H  |  480  |  Hotel Time Zone  |  UTC+8|
|HAA  |  -180  |  Heure Avanc閑 de l'Atlantique  |  UTC-3|
|HAC  |  -300  |  Heure Avancee du Centre  |  UTC-5|
|HADT  |  -540  |  Hawaii-Aleutian Daylight Time  |  UTC-9|
|HAE  |  -240  |  Heure Avancee de l'Est  |  UTC-4|
|HAP  |  -420  |  Heure Avancee du Pacifique  |  UTC-7|
|HAR  |  -360  |  Heure Avancee des Rocheuses  | UTC-6|
|HAST  |  -600  |  Hawaii-Aleutian Standard Time  |  UTC-10|
|HAT  |  -90  |  Heure Avanc閑 de Terre-Neuve  |  UTC-1:30|
|HKT  |  480  |  Hong Kong Time  |  UTC+8|
|HLV  |  -210  |  Hora Legal de Venezuela  |  UTC-3:30|
|HNA  |  -240  |  Heure Normale de l'Atlantique  |  UTC-4|
|HNC  |  -360  |  Heure Normale du Centre  |  UTC-6|
|HNE  |  -300  |  Heure Normale de l'Est |  UTC-5 |
|HNP  |  -480  |  Heure Normale du Pacifique |  UTC-8 |
|HNR  |  -420  |  Heure Normale des Rocheuses  |  UTC-7|
|HNT  |  -150  |  Heure Normale de Terre-Neuve  |  UTC-3:30|
|HNY  |  -540  |  Heure Normale du Yukon  |  UTC-9|
|HOVST  |  480  |  Hovd Summer Time  |  UTC+8|
|HOVT  |  420  |  Hovd Time  |  UTC+7|
|I  |  540  |  India Time Zone  |  UTC+9|
|ICT  |  420  |  Indochina Time  |  UTC+7|
|IDT  |  180  |  Israel Daylight Time  |  UTC+3|
|IOT  |  360  |  Indian Chagos Time  |  UTC+6|
|IRDT  |  270  |  Iran Daylight Time  |  UTC+4:30|
|IRKST  |  540  |  Irkutsk Summer Time  |  UTC+9|
|IRKT  |  480  |  Irkutsk Time  |  UTC+8|
|IRST  |  210  |  Iran Standard Time  |  UTC+3:30|
|IST  |  60  |  Irish Standard Time  |  UTC+1|
|JST  |  540  |  Japan Standard Time  |  UTC+9|
|K  |  600  |  Kilo Time Zone  |  UTC+10|
|KGT  |  360  |  Kyrgyzstan Time  |  UTC+6|
|KOST  |  660  |  Kosrae Time  |  UTC+11|
|KRAST  |  480  |  Krasnoyarsk Summer Time  |  UTC+8|
|KRAT  |  420  |  Krasnoyarsk Time  |  UTC+7|
|KST  |  540  |  Korea Standard Time  |  UTC+9|
|KUYT  |  240  |  Kuybyshev Time  |  UTC+4|
|L  |  660  |  Lima Time Zone  |  UTC+11
|LHDT  |  660  |  Lord Howe Daylight Time  |  UTC+11|
|LHST  |  630  |  Lord Howe Standard Time  |  UTC+10:30|
|LINT  |  840  |  Line Islands Time  |  UTC+14|
|M  |  720  |  Mike Time Zone  |  UTC+12|
|MAGST  |  720  |  Magadan Summer Time  |  UTC+12|
|MAGT  |  660  |  Magadan Time  |  UTC+11|
|MART  |  -510  |  Marquesas Time  |  UTC-9:30|
|MAWT  |  300  |  Mawson Time  |  UTC+5|
|MDT  |  -360  |  Mountain Daylight Time  |  UTC-6|
|MESZ  |  120  |  Mitteleurop鋓sche Sommerzeit  |  UTC+2|
|MEZ  |  60  |  Mitteieuropaische Zeit  |  UTC+1|
|MHT  |  720  |  Marshall Islands Time  |  UTC+12|
|MMT  |  390  |  Myanmar Time  |  UTC+6:30|
|MSD  |  240  |  Moscow Daylight Time  |  UTC+4|
|MSK  |  180  |  Moscow Standard Time  |  UTC+3|
|MST  |  -420  |  Mountain Standard Time  |  UTC-7|
|MUT  |  240  |  Mauritius Time  |  UTC+4|
|MVT  |  300  |  Maldives Time  |  UTC+5|
|MYT  |  480  |  Malaysia Time  |  UTC+8|
|N  |  -60  |  November Time Zone  |  UTC-1|
|NCT  |  660  |  New Caledonia Time  |  UTC+11|
|NDT  |  -90  |  Newfoundland Daylight Time  |  UTC-2:30|
|NFT  |  660  |  Norfolk Time  |  UTC+11|
|NOVST  |  420  |  Novosibirsk Summer Time  |  UTC+7|
|NOVT  |  420  |  Novosibirsk Time  |  UTC+7|
|NPT  |  345  |  Nepal Time  |  UTC+5:45|
|NRT  |  720  |  Nauru Time  |  UTC+12|
|NST  |  -150  |  Newfoundland Standard Time  |  UTC-3:30|
|NUT  |  -660  |  Niue Time  |  UTC-11|
|NZDT  |  780  |  New Zealand Daylight Time  |  UTC+13|
|NZST  |  720  |  New Zealand Standard Time  |  UTC+12|
|O  |  -120  |  Oscar Time Zone  |  UTC-2|
|OMSST  |  420  |  Omsk Summer Time  |  UTC+7|
|OMST  |  360  |  Omsk Standard Time  |  UTC+6|
|ORAT  |  300  |  Oral Time  |  UTC+5|
|P  |  -180  |  Papa Time Zone  |  UTC-3|
|PDT  |  -420  |  Pacific Daylight Time  |  UTC-7|
|PET  |  -300  |  Peru Time  |  UTC-5|
|PETST  |  720  |  Kamchatka Summer Time  |  UTC+12|
|PETT  |  720  |  Kamchatka Time  |  UTC+12|
|PGT  |  600  |  Papua New Guinea Time  |  UTC+10|
|PHOT  |  780  |  Phoenix Island Time  |  UTC+13|
|PHT  |  480  |  Philippine Time  |  UTC+8|
|PKT  |  300  |  Pakistan Standard Time  |  UTC+5|
|PMDT  |  -120  |  Pierre & Miquelon Daylight Time  |  UTC-2|
|PMST  |  -180  |  Pierre & Miquelon Standard Time  |  UTC-3|
|PONT  |  660  |  Pohnpei Standard Time  |  UTC+11|
|PST  |  -480  |  Pacific Standard Time  |  UTC-8|
|PT  |  -480  |  Pacific Time  |  UTC-8:00 / -7:00|
|PWT  |  540  |  Palau Time  |  UTC+9|
|PYST  |  -180  |  Paraguay Summer Time  |  UTC-3|
|PYT  |  -240  |  Paraguay Time  |  UTC-4|
|Q  |  -240  |  Quebec Time Zone  |  UTC-4|
|QYZT  |  360  |  Qyzylorda Time  |  UTC+6|
|R  |  -300  |  Romeo Time Zone  |  UTC-5|
|RET  |  240  |  Reunion Time  |  UTC+4|
|ROTT  |  -180  |  Rothera Time  |  UTC-3|
|S  |  -360  |  Sierra Time Zone  |  UTC-6|
|SAKT  |  660  |  Sakhalin Time  |  UTC+11|
|SAMT  |  240  |  Samara Time  |  UTC+4|
|SAST  |  120  |  South Africa Standard Time  |  UTC+2|
|SBT  |  660  |  Solomon Islands Time  |  UTC+11|
|SCT  |  240  |  Seychelles Time  |  UTC+4|
|SGT  |  480  |  Singapore Time  |  UTC+8|
|SRET  |  660  |  Srednekolymsk Time  |  UTC+11|
|SRT  |  -180  |  Suriname Time  |  UTC-3|
|SST  |  -660  |  Samoa Standard Time  |  UTC-11|
|SYOT  |  180  |  Syowa Time  |  UTC+3|
|T  |  -420  |  Tango Time Zone  |  UTC-7|
|TAHT  |  -600  |  Tahiti Time  |  UTC-10|
|TFT  |  300  |  French Southern and Antarctic Time  |  UTC+5|
|TJT  |  300  |  Tajikistan Time  |  UTC+5|
|TKT  |  780  |  Tokelau Time  |  UTC+13|
|TLT  |  540  |  East Timor Time  |  UTC+9|
|TMT  |  300  |  Turkmenistan Time  |  UTC+5|
|TOST  |  840  |  Tonga Summer Time  |  UTC+14|
|TOT  |  780  |  Tonga Time  |  UTC+13|
|TRT  |  180  |  Turkey Time  |  UTC+3|
|TVT  |  720  |  Tuvalu Time  |  UTC+12|
|U  |  -480  |  Uniform Time Zone  |  UTC-8|
|ULAST  |  540  |  Ulaanbaatar Summer Time  |  UTC+9|
|ULAT  |  480  |  Ulaanbaatar Time  |  UTC+8|
|UTC  |  0  |  Coordinated Universal Time  |  UTC+0|
|UYST  |  -120  |  Uruguay Summer Time  |  UTC-2|
|UYT  |  -180  |  Uruguay Time  |  UTC-3|
|UZT  |  300  |  Uzbekistan Time  |  UTC+5|
|V  |  -540  |  Victor Time Zone  |  UTC-9|
|VET  |  -240  |  Venezuelan Standard Time  |  UTC-4|
|VLAST  |  660  |  Vladivostok Summer Time  |  UTC+11|
|VLAT  |  600  |  Vladivostok Time  |  UTC+10|
|VOST  |  360  |  Vostok Time  |  UTC+6|
|VUT  |  660  |  Vanuatu Time  |  UTC+11|
|W  |  -600  |  Whiskey Time Zone  |  UTC-10|
|WAKT  |  720  |  Wake Time  |  UTC+12|
|WARST  |  -180  |  Western Argentine Summer Time  |  UTC-3|
|WAST  |  120  |  West Africa Summer Time  |  UTC+2|
|WAT  |  60  |  West Africa Time  |  UTC+1|
|WEST  |  60  |  Western European Summer Time  |  UTC+1|
|WET  |  0  |  Western European Time  |  UTC+0|
|WEZ  |  0  |  Westeurop鋓sche Zeit  |  UTC+0|
|WFT  |  720  |  Wallis and Futuna Time  |  UTC+12|
|WGST  |  -120  |  Western Greenland Summer Time  |  UTC-2|
|WGT  |  -180  |  West Greenland Time  |  UTC-3|
|WIB  |  420  |  Western Indonesian Time  |  UTC+7|
|WIT  |  540  |  Eastern Indonesian Time  |  UTC+9|
|WITA  |  480  |  Central Indonesian Time  |  UTC+8|
|WST  |  780  |  West Samoa Time  |  UTC+13|
|WT  |  0  |  Western Sahara Standard Time  |  UTC+0|
|X  |  -660  |  X-ray Time Zone  |  UTC-11|
|Y  |  -720  |  Yankee Time Zone  |  UTC-12|
|YAKST  |  600  |  Yakutsk Summer Time  |  UTC+10|
|YAKT  |  540  |  Yakutsk Time  |  UTC+9|
|YAPT  |  600  |  Yap Time  |  UTC+10|
|YEKST  |  360  |  Yekaterinburg Summer Time  |  UTC+6|
|YEKT  |  300  |  Yekaterinburg Time  |  UTC+5|
|Z  |  0  |  Zulu Time Zone  |  UTC+0|


## Contribute:


Please read [Contributing](https://github.com/EudyContreras/Chronox.NetCore/blob/master/CONTRIBUTING) for details on the code base code of conduct, and the process for submitting pull requests to **OthelloFX**





## Authors and Contributors:


* **Eudy Contreras** 





## Contact:


If any questions regarding this program fell free to reach me at my [Email](EudyContrerasRosario@gmail.com)





## Disclaimer:

This program is in WIP and it was made as a hobby and it not originally intended for professional or commercial use. Although this program works as expected to some extent and can be used in professional and commercial projects I the original author will not be subjected to any liability. This program may be subjected to architectural changes and in its current state it is not perfect. Please exercise caution and use at own risk.

All background images including the logo were not made by me and I do not claim ownership of these images. I would like to thank the awesome artists and creators of the images for making them public. If there is any problem with the use of these images please contact me so we can solve it. Once again. props to the artists.



## Tech used:


![Net.Core Logo](https://github.com/EudyContreras/Chronox.NetCore/blob/master/netcore.png)
* [Net.Core](https://en.wikipedia.org/wiki/.NET_Core)





## License:

This project is licensed under the MIT License - see the [Licence](https://github.com/EudyContreras/Chronox.NetCore/blob/master/LICENSE.md) file for details
