# Chronox.NetCore

#### NetCore Library for parsing relaxed and strict DateTime, TimeRange, TimeSpan and TimeSet expressions into discrete representations holding useful date and or time related data.

![Chronox NetCore NLP Image](https://github.com/EudyContreras/Chronox.NetCore/blob/master/NLP.png)


Chronox supports a large variety of relaxed date and time formats.
Here is an extensive list of most of the common date and time expressions supported by **Chronox**:


## How to use it?

* #### Instantiation ####
A `Chronox` instance can be created in the following way without specifying any settings. Please note that Chronox uses the singleton pattern. Most of the work done by `Chronox` happens upon instatiation which is when data sets are loaded, sequences are created and loaded and where dictionaries are indexed.

```c#
var Chronox = ChronoxParser.GetInstance();
```
Or like this in which custom settings are specified upon instatiation.
```c#
var Chronox = ChronoxParser.GetInstance(new ChronoxSettings("english"));
```


* #### Settings ####

A ChronoxSettings can be instantiated with the name of languages you wish to support. 
```c#
var settings = new ChronoxSettings("English", "Spanish")
```
A ChronoxSettings can also be instatiated with a custom collectios of sequences you wish support. For more information about sequence codes and additional ways for supporting custom sequences please refer to the dataSet section and to the sequence library.
```c#
var sequences = new string[]{"D.O.W|D.T|T.M|T.O.D", "D.O.W|D.T|T.M|T.O.D|T.Z", "G.E|D.O.W|D.T|T.M"};
  
var sequenceCollection = new SequenceCollection(SequenceType.DateTime,sequences);
  
var settings = new ChronoxSettings(sequenceCollection,"English", "Spanish")

```
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
| **NoFoundResultResolver**   | Null, Implied, Current                   | What to return upn no result found        |
| **ParsingMode**             | DateTime, TimeRange, TimeSet, TimeSpan   | The mode the parser should use: Guess otherwise |
| **RelaxLevel**              | Formal, Casual, Any                      | Type of strings the parse should parse		   |
| **PrefferedEndian**         | MiddleEndian, LittleEndian               | Endian type for parsing ambiguous date format   |
| **PrefferedDay**            | Current, Start, End, Previous, Next      | Offset day if could not be determined     
| **StartOfWeek**             | Monday, Tuesday, etc                     | The day in which a week starts            |


## How does Chronox works?


## How to add unsupported formats or sequences?


## How to add support for a language?


## How to define your options?


## How to define and add additonal pre-processors?


## Dealing with ambiguity:


## Parser result:


## Component types:

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


## Known bugs:


## Potential conflicts or bugs:


### Future works




There are parts of this library that are yet to be finished and there are also some things which I plan to add to the library. These things will be shown here along with popular demands

- [ ] **More options to the main menu so that users have more control through the interface**.
- [ ] **Maybe an online multiplayer option for users to test their agents remotely in competitions**




### Contributing


Please read [Contributing](https://github.com/EudyContreras/Chronox.NetCore/blob/master/CONTRIBUTING) for details on the code base code of conduct, and the process for submitting pull requests to **OthelloFX**





### Authors


* **Eudy Contreras** 





### Contact Info


If any questions regarding this program fell free to reach me at.
EudyContrerasRosario@gmail.com







### Disclaimer

All background images including the logo were not made by me and I do not claim ownership of these images. I would like to thank the awesome artists and creators of the images for making them public. If there is any problem with the use of these images please contact me so we can solve it. Once again. props to the artists.




### Built With


![Net.Core Logo](https://github.com/EudyContreras/Chronox.NetCore/blob/master/netcore.png)
* [Net.Core](https://en.wikipedia.org/wiki/.NET_Core)





### License

This project is licensed under the MIT License - see the [Licence](https://github.com/EudyContreras/Chronox.NetCore/blob/master/LICENSE.md) file for details
