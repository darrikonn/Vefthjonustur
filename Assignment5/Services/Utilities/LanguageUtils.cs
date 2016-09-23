namespace CoursesAPI.Services.Utilities {
    using System.Linq;
    using System.Collections.Generic;
    using Exceptions;

    public static class LanguageUtils {
        public enum Language {English, Icelandic};

        private static readonly List<string> _supportedLanguages = new List<string> {"is-IS", "en-US", 
            "en-GB", "en-IE", "en-AU", "en-CA"};

        public static Language GetLanguage(string lang) {
            if (string.IsNullOrEmpty(lang)) {
                return Language.Icelandic;
            }

            if (!_supportedLanguages.Any(l => lang.StartsWith(l))) {
                throw new AppObjectNotFoundException("Language not supported\n");
            }

            return lang == _supportedLanguages[0] 
                ? Language.Icelandic
                : Language.English;
        }
    }
}
