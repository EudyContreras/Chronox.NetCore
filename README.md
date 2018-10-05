# Chronox.NetCore

#### NetCore Library for parsing relaxed and strict DateTime, TimeRange, TimeSpan and TimeSet expressions into discrete representations holding useful date and or time related data. 

![Chronox NetCore NLP Image](https://github.com/EudyContreras/Chronox.NetCore/blob/master/NLP.png)


**Chronox** supports a large variety of relaxed date and time formats. For an extensive list of most of the common date and time expressions supported by **Chronox**: please visit the [Supported DateTime Formats](#https://github.com/EudyContreras/Chronox.NetCore/blob/master/SUPPORT.md) document. Other documents showing the supported TimeRange, TimeSpan, and TimeSet formats will soon be available. Please look below for a definition of each of the supported types.

### Definitions: ###

* **DateTime:** Any data representing a date and time. Ex: **`The 25th of may of the year 2014 at 10pm`**
	
* **TimeSpan:** Any data represeting a duration or span. Ex: **`For 40 minutes and 21 seconds`**

* **TimeRange:** Any data represeting  a time range. Ex **`From the third to the 8th of november next month`**

* **TimeSet:** Any data representing a time set or reccurring event. Ex **`Every second day in the month of september`**

## Table of content ##

- **[How to use it ?](#how-to-use-it)**
  - **[Instantiation](#instantiation)**
  - **[Settings](#settings)**
  - **[Parsing input](#parsing-input)**
  - **[Result](#result)**
  - **[Result interpratation](#result-interpratation)**
- **[How does Chronox works ?](#how-does-chronox-works)**
  - **[Compiling performance](#compiling-performance)**
  - **[Parsing performance](#parsing-performance)**
- **[Datasets and Instructions](#datasets-and-instructions)**
  - **[Keywords](#keywords)**
  - **[Language](#language)**
  - **[Section](#section)**
  - **[Property](#property)**
  - **[Section Type Glossary](#section-type-glossary)**
  - **[Propertyy Type Glossary](#property-type-glossary)**
  - **[Regex Cheat Sheet](#regex-cheat-sheet)**
-**[How to add unsupported formats or sequences?](#how-to-add-unsupported-formats-or-sequences)**
  - **[Adding a sequence collection](#adding-a-sequence-collection)**
  - **[Specifiying format support](#specifiying-format-support)**
  - **[Format code cheat sheet](#format-code-cheat-sheet)**
- **[How to define and add additonal pre-processors](#how-to-define-and-add-additonal-pre-processors)**
- **[Dealing with conflict or ambiguity](#dealing-with-conflict-or-ambiguity)**
- **[Known and potential bugs](#known-and-potential-bugs)**
- **[Component types](#component-types)**
- **[Things to note](#things-to-note)**
- **[Future works](#future-works)**
- **[Supported timezones](#supported-timezones)**
- **[Contribute](#contribute)**
- **[Acknowledgements](#acknowledgements)**
  - **[Authors](#authors)**
  - **[Contributors](#contributors)**
  - **[Inspiration](#inspiration)**
- **[Contact](#contact)**
- **[Disclaimers](#disclaimers)**
- **[Technologies used](#technologies-used)**
- **[License](#license)**


## How to use it? ##

#### Instantiation ####

Please note that Chronox uses the singleton pattern. Most of the work done by `Chronox` happens upon instatiation which is when data sets are loaded, sequences are created and where dictionaries are indexed.

A `Chronox` instance can be created in the following way without specifying any settings.

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
A `ChronoxSettings` can also be instatiated with a custom collectios of sequences that you wish support. For more information about sequence codes and additional ways for supporting custom sequences please refer to the [Datasets and Instructions](#datasets-and-instructions) section and to the [Format code cheat sheet](#format-code-cheat-sheet).

```c#

var sequences = new string[]{"D.O.W|D.T|T.M|T.O.D", "D.O.W|D.T|T.M|T.O.D|T.Z", "G.E|D.O.W|D.T|T.M"};
  
var sequenceCollection = new SequenceCollection(SequenceType.DateTime,sequences);
  
var settings = new ChronoxSettings(sequenceCollection,"English", "Spanish")

```


`ChronoxSettings` is a comprehensive set of options that the `ChronoxParser` uses in order to achieve the desired results in concordance to the needs of the application at hand.  For a list of all the currently available options/preferences that `ChronoxSettings` features please refer to the table below.


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
| **RelaxLevel**              | Formal, Casual, Any                      | Type of strings the parse should parse	   |
| **PrefferedEndian**         | MiddleEndian, LittleEndian               | Endian to use upon ambiguous date format   |
| **PrefferedDay**            | Current, Start, End, Previous, Next      | Offset day if could not be determined     
| **StartOfWeek**             | Monday, Tuesday, etc                     | The day in which a week starts            |


#### Parsing input ####

In order to parse input `Chronox` can also be called statically in the following way:

```c#

var input  = "Meet me the day after tomorrow at eight in the evening at the mall near your house" 

var result = null;

result = ChronoxParser.Parse(input);

ChronoxParser.TryParse(input, out result);

```

You can also specify what you would like to attempt to extract from the input by specifying which parsing method to use: If `ChronoxParser.Parse` is used the parser will attempt to determine the type. **Please** use the exact parsing methods for more accuracy and less overhead. When using exact parsing all othe parsing types and formats are ignored.

```c#

result = ChronoxParser.ParseDateTime(settings, input);
result = ChronoxParser.ParseTimeSpan(settings, input);
result = ChronoxParser.ParseTimeRange(settings, input);
result = ChronoxParser.ParseTimeSet(settings, input);

```

#### Result ####

A `ChronoxResult` contains the result data extracted by the parser. The result obtain from a parsing operation is of type `IChronoxExtraction`. A result may contain many different extractions. In the case of none exact parsing when calling the `ChronoxParser.Parse` a `ResultWrapper` is returned which contains the data extracted if any. In the case of **exact parsing**, a predetermined extraction type is returned. The extraction contains the following properties:

* `RetultType` The type of the extraction which could be a `DateTime`, `TimeSpan`, etc.
* `ProcessedString`  The string after it has been processed which is stripped of the extraction areas. 
* `Original` The original string which was passed to the parser. 
* `Extraction`  The actual extraction or representation of the parsed data.
* `StartIndex`  The start index at which the extraction happened.
* `EndIndex`  The end index at which the extraction happened.

#### Result interpratation ####

The following code shows a scenario where a reference date is used. The parser will then attempt to parse the input and return a result relative to the passed reference date.

Given the following case:

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

## How does Chronox works? ##

`Chronox` uses a combination of different techniques in order to extract desired time related entities. At the top level `Chronox` uses pre-processors which will scan the string and attempt to pre-process it in order to make parsing and pattern matching easier. Eg: A pre-processor or `IChronoxScanner` is used to convert any numeric value written in words to its numeric representation. Eg: "Two hundred and fifty-five becomes 255". Additional pre-processors may be used in order to satisfy the locale and or parsing needs of an application.

Under the hood `Chronox` uses **REGEX** for matching specific patterns along the side some translation and entithy extraction methods. Various entity extraction methods are used in order to effetively find and parse the desired data. Regex patterns are automatically generated using a generator. Theses regex patterns are then used in order to find the desired data.

#### Compiling performance: ####

Chronox compilation time is relative to the amount of sequences/formats supported as well the amount of datasets added to it. The amount of variations added to a dataset will affect the the compilation time too.

#### Parsing performance: ####

Due to the fact that most of the work done by `Chronox` happens when it is built, parsing times are in most cases pretty fast. The usual parsing time does not exceed 1 millisecond. There are some edge cases in which chronox may take up to 60 milliseconds in order to return a result. The lenght of the string and the amount of pre-processors/scanners used will also affect the parsing performance. `Chrono` in its current state can be further optimized and it will be optimized in a near future.

## Datasets and Instructions:

In order to add support to a language `Chronox` uses datasets which contain instructions which the system uses in order to parse a given input. For a DataSet template please please refere to: [Template](https://github.com/EudyContreras/Chronox.NetCore/blob/master/TEMPLATE.txt). The dataset allows the user to deal with some language specific rules and exceptions.

In order to achieve the best result please read the guidelines and descriptions. The language files work directly with the parser in order to provide support for different languages. If you wish add to support to a language simply specified the equivalent variations to be used by the parser when converting, along with the optional regex patterns for the variations. If no regex pattern is provided to the variations it will then be generated by `Chronox`. You may write the patterns yourself in order to decrese the work that needs to be done by `Chronox` upon construction. In addition you may also provide additional information in order to inrease parsing accuracy. 

#### Keywords: ####

* **`Modifiable`**: Tells if the the property should be or could be modified by the user.
* **`Unsupported`**: Tells if the property is currently supported.

#### Language: ####
	
* **`language`**: The name of the language being used: [Modifiable: Yes]
* **`ignored`**: List of words or to ignore: [Modifiable: Yes]
* **`assumeSpace`**: "Determines if the parser automatically add spaces between properties: [Modifiable: Yes]"
* **`sectionTypes`**: List of types supported by sections: [Modifiable: No]
* **`propertyTypes`**: List of types supported by properties: [Modifiable: No]
* **`supportedSectionAbreviations`**: List of abreviations which could be used for creating date formats: [Modifiable: No]
* **`supportedDateFormats`**: List of date formats to look for when parsing: [Modifiable: Yes]
	
#### Section: ####
	
* **`label`**: The name or label of the section: [Modifiable: No]
* **`type`**: The type of the section "Must be a supported type": [Modifiable: At own risk!]
	
#### Property: ####
	
* **`key`**: The key name that identifies the property: [Modifiable: No]
* **`value`**: The value associated with the key: [Unsupported]
* **`type`**: The type of the property "Must be a supported type" : [Modifiable: At own risk!]
* **`pattern`**: The pattern associated to the variations:[Modifiable: Yes]
* **`variations`**: The user submitted synonyms or representations of the key which must also be included in the specified language: [Modifiable: Yes]

#### Section Type Glossary: #### 

* **`N/A`**: Not specified or not available.
* **`Combined`**: Combines the properties as one single group. Typically used for properties belonging to the same category
* **`CombinedOptional`**: Same as `Combined`. The properties will be optinal and not enforced.
* **`CombinedReversed`**: Same as `Combined`. Reverses the combination order. Typically used to avoid overlap: Such as finding: "four" in "twenty four"

#### Property Type Glossary: #### 

* **`N/A`**: Not specified or not available
* **`Group`**: It will turn the property into a recognizable group which can be refered to for information "Ignored if the parent section is of type "Combined" or "CombinedReversed""
* **`GroupOptional`**: It will turn the property into a recognizable group which can be refered to for information "Ignored if the parent section is of type "Combined" or "CombinedReversed""
* **`Optional`**: Specifies that the property is optional and may or may not be present in a date format
* **`Filler`**: Placeholder property which binds groups together or simply fills a space: "No information directly extracted but it is part of a date pattern"
* **`Interpreted`**: Expression which can be directly translated into a DateTime object Ex: "Tonight, Now, Last night"

#### Regex Cheat Sheet: ####

* **`Properties`**: monday, mon
* **`Regex`**: mon(?:day) or monday|mon : these regex expecifies that the both Mon or Moday can be matched

* **`Properties`**: one, first
* **`Regex`**: one|first : this regex specifies that either one or first can be matched

For more information about Regular Expression **REGEX** please visit: [Regex Documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference)

## How to add unsupported formats or sequences? ##

In order to add a date format or sequence which is not currently supported by `Chronox` all that needs to be done is to specify said format using a combination of section codes. For a full list of the available section codes please look at the [Format code cheat sheet](#format-code-cheat-sheet):

Most commonly used as well as some less commonly used formats are already supported by `Chronox`. For a full list of supported format please look at the [Sequneces](https://github.com/EudyContreras/Chronox.NetCore/blob/master/SEQUENCES.md) document. Feel free to add support to your custom format.

#### Adding a sequence collection: ####

Formats can be submitted to `Chronox` in two ways. The first way is through a sequence collection. A `SequenceCollection can be added when creating a `ChronoxSettings`. 

```c#

var sequences = new string[]{"D.O.W|D.T|T.M|T.O.D", "D.O.W|D.T|T.M|T.O.D|T.Z", "G.E|D.O.W|D.T|T.M"};
  
var sequenceCollection = new SequenceCollection(SequenceType.DateTime,sequences);
  
var settings = new ChronoxSettings(sequenceCollection,"English", "Spanish")

```

#### Specifiying format support: ####

The other option is to specifiy directly on the dataset which formats you wish to support.

```js
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

#### Format code cheat sheet: ####

The table below shows the full list of supported section codes along with their representation and example cases. 

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

A pre-processor or `scanner` allows the user to specifiy an operation to be performed on the input before the actual parsing is performed. Pre-processing the input may improve both the parsing performance and the parsing accuracy by removing noise from the input. As previously mentioned `Chronox` uses a pre-processor in order to convert numbers written in words to actual numeric values before the parsing is performed. 

In order to add a pre-processor you must simply implement the `IChronoxScanner` interface and add your logic to the Scan method. The method returns a `ScanWrapper` which contains information about the scan result. A scanner tag must also be specified. 

**The Interface:**

```c#

    public interface IChronoxScanner
    {
        string ScannerTag
        ScanWrapper Scan(ChronoxSettings option, string expression);
    }

```

**The result**

A scan result yields a `ScanWrapper` containing information about the scan. A scan will also contain a list of `ReplaceWrappers` which encapsulate information about what was replaced by the scanner and where did the replace happened. Please look at the templates below for a better idea of the results a scan operation will yield.

```c#

    public class ScanWrapper
    {
        public List<ReplaceWrapper> ResultWrappers { get; set; } = new List<ReplaceWrapper>();

        public string ScannedExpression { get; set; } 

        public string NormalizedExpression { get; set; }

        public string ScannerTag { get; set; }
    }
    
    
    public class ReplaceWrapper
    {
        public IndexWrapper ReplacementPosition { get; set; }

        public IndexWrapper OriginalPosition { get; set; }  

        public string ReplacerTag { get; set; }

        public string TextOriginal { get; set; }

        public string TextReplacement { get; set; }
    }

```


## Dealing with conflict or ambiguity:
	
When parsing a date conflicts or ambiguity may occur due to the fact that maybe formats are too similar. In said case `Chronox` will then let the user decide which result to choose. Conflicts can be avoided by not supporting formats that create amibiguity. Methods for dealing with conflic and ambiguity are still a work in progress and are subjected to change and improvements.

## Known and potential bugs:

More testing needed! Coming soon...

## Component types:

**Chronox** is meant to be able to handle date and time paring but it can also handle time ranges, recurring time sets, durations or time spans and timezone conversion. Here is a list of the components used by `Chronox`.

* **ChronoxDate** : *Comonent which holds date related data*
* **ChronoxTime** : *Comonent which holds time related data*
* **ChronoxDateTime** : *Comonent which holds date and time related data*
* **ChronoxTimeRange** : *Comonent which holds time range related data*
* **ChronoxTimeSet** : *Comonent which holds recurring time event related data*
* **ChronoxTimeSpan** : *Comonent which holds durationg/span related data*
* **ChronoxTimeZone** : *Comonent which holds timezone related data*


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


There are parts of this library that are yet to be finished and there are also some things which I plan to add to the library. These things will be shown here along with popular demands.

- [ ] **Make into nuget package**
- [ ] **Add thread safety**
- [ ] **Improve loading performance**
- [ ] **Allow post-compilation modifications to Chronox**
- [ ] **Add better support to multi-extraction**
- [ ] **Finish adding support to TimeSpan**
- [ ] **Finish adding support to TimeSet**
- [ ] **Finish adding support to TimeRange**
- [ ] **A bunch of other things**


## Supported timezones: ##

**Chronox** supports most if not all TimeZones. If a timeZone is found it will be extracted. A **Timezone** object can be added or substracted from the result in order get an accurate timeZone sensitve time. For a list of the supported TimeZones please look at [TimeZones](https://github.com/EudyContreras/Chronox.NetCore/blob/master/TIMEZONES.md)


## Contribute:


Please read [Contributing](https://github.com/EudyContreras/Chronox.NetCore/blob/master/CONTRIBUTING) for details on the code base code of conduct, and the process for submitting pull requests to **Chronox**



## Acknowledgements: ##


### Authors: ###
* **Eudy Contreras** 

### Contributors: ###
* **Eudy Contreras** 

### Inspiration: ###
[Wanasit](https://github.com/wanasit) whose project served as inspiration.


## Contact: ##


If you have any questions regarding this program feel free to reach me at my [Email](EudyContrerasRosario@gmail.com)





## Disclaimers:

This program is a work in progress and it was made as a hobby and it not originally intended for professional or commercial use. Although this program works as expected to some extent and can be used in professional and commercial projects, I the original author will not be subjected to any liability cause by errors produced by this program. This program may be subjected to architectural changes and in its current state it is not perfect. Please exercise caution and use at own risk.

All background images including the logo were not made by me and I do not claim ownership of these images. I would like to thank the awesome artists and creators of the images for making them public. If there is any problem with the use of these images please contact me so we can solve it. Once again. props to the artists.



## Technologies used:


![Net.Core Logo](https://github.com/EudyContreras/Chronox.NetCore/blob/master/netcore.png)
* [Net.Core](https://en.wikipedia.org/wiki/.NET_Core)





## License:

This project is licensed under the MIT License - see the [Licence](https://github.com/EudyContreras/Chronox.NetCore/blob/master/LICENSE.md) file for details
