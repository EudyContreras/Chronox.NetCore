using System;
namespace Chronox.Handlers.Models
{
    public class Language
    {
        public string LanguageName { get; set; }

        public ChronoxLangSettings LanguageSettings { get; private set; }

        public Language(string languageName, ChronoxLangSettings languageSettings)
        {
            LanguageName = languageName;
            LanguageSettings = languageSettings;
        }
    }
}
