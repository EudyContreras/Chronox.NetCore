
## Settings and preferences ##
</br><br>
`ChronoxSettings` is a comprehensive set of options that the `ChronoxParser` should use in order to achieve the desired results in concordance to the application in play. 
</br><br>

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
