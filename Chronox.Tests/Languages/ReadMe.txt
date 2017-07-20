
  This document contains information about how the language
  files are to be used in order to achieve the best result. The
  language files work directly with the parser
  In order to provide a different laguage simply specified 
  the equivalent variations to be used by the parser when converting, 
  along with the optional regex patterns for the variations.

  Author: Eudy Contreras

  Keywords:

	-Modifiable: Tells if the the property should be or could be modified by the user
	-Unsupported: Teslls if the property is currently supported

	Language:
	
		language: The name of the language being used: [Modifiable: Yes]
		ignored: List of words or to ignore: [Modifiable: Yes]
		assumeSpace: "Determines if the parser automatically add spaces between properties: [Modifiable: Yes]"
		sectionTypes: List of types supported by sections: [Modifiable: No]
		propertyTypes: List of types supported by properties: [Modifiable: No]
		supportedSectionAbreviations: List of abreviations which could be used for creating date formats: [Modifiable: No]
		supportedDateFormats: List of date formats to look for when parsing: [Modifiable: Yes]
	
	Section:
	
		label: The name or label of the section: [Modifiable: No]
		type: The type of the section "Must be a supported type": [Modifiable: At own risk!]
	
	Property:
	
		key: The key name that identifies the property: [Modifiable: No]
		value: The value associated with the key: [Unsupported]
		type: The type of the property "Must be a supported type" : [Modifiable: At own risk!]
		pattern: The pattern associated to the variations:[Modifiable: Yes]
		variations: The user submitted synonyms or representations of the key which must also be included in the specified language: [Modifiable: Yes]

	Examples:
		  
		"language": "English"
		"ignored": ["the","of","and"],
		"key": "during"
		"pattern": "during|while|along|throughout|over|amid",
		"variations": [ "during", "while", "along", "throughout", "over", "amid" ]	  
		"supportedDateFormats": ["SimpleDateFormat:(D.O.W|W.S|N.W|W.S|M.O.Y)"], : dayOfWeek|whiteSpace|numberWord|whiteSpace|monthOfYear

	Section Type Glossary: 

		-Combined : It will combine the properties as one single group. Typically used for properties belonging to the same category
		-CombinedReversed : Same as "Combined", It will also reversed the order in which the properties are combined. Typically used to avoid overlap: Such as finding: "four" in "twenty four"
		-n/a: Not specified or not available

	Section Type Glossary: 

		-Group: It will turn the property into a recognizable group which can be refered to for information "Ignored if the parent section is of type "Combined" or "CombinedReversed""
		-Optional: Specifies that the property is optinal and may or may not be present in a date format
		-Filler: Placeholder property which binds to groups together or simply fills a space: "No information directly extracted but it is part of a date pattern"
		-Interpreted: The property is an expression or phrase which can be directly translated into a DateTime object Ex: "Tonight, Now, Last night"
		-n/a: Not specified or not available


	Regex Cheat Sheet

		Properties: monday, mon
		Regex: mon(?:day) or monday|mon : these regex expecifies that the both Mon or Moday can be matched

		Properties: one, first
		Regex: one|first : this regex specifies that either one or first can be matched

		for more information visit: https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference
  



