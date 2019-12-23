using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.Extensions;

namespace Hilma.Domain.Integrations.Extensions
{
    /// <summary>
    /// Extensions for List
    /// </summary>
    public static class ListExtensions
    {
        public static JObject Translations { get; set; }

        public static string NoticeLanguage { get; set; }

        /// <summary>
        /// Adds an XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Overload for section</param>
        /// <param name="translationKey"></param>
        public static void Add(this List<Change> list, string originalValue, string newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            if (originalValue == newValue || (string.IsNullOrEmpty(originalValue) && string.IsNullOrEmpty(newValue)))
            {
                return;
            }

            list.Add(ChangeText(new string[] { originalValue }, new string[] { newValue }, type, property, lotNum, section, translationKey));
        }

        /// <summary>
        /// Adds an XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Overload for section</param>
        /// <param name="translationKey"></param>
        public static void Add(this List<Change> list, bool originalValue, bool newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            if (originalValue == newValue)
            {
                return;
            }

            list.Add(ChangeText(new string[] { originalValue.ToYesNo(NoticeLanguage) }, new string[] { newValue.ToYesNo(NoticeLanguage) }, type, property, lotNum, section, translationKey));
        }

        /// <summary>
        /// Adds an XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Overload for section</param>
        /// <param name="translationKey"></param>
        public static void Add(this List<Change> list, int originalValue, int newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            if (originalValue.ToString() == newValue.ToString())
            {
                return;
            }

            list.Add(ChangeText(new string[] { originalValue.ToString() }, new string[] { newValue.ToString() }, type, property, lotNum, section, translationKey));
        }
        /// <summary>
        /// Adds an XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Overload for section</param>
        /// <param name="translationKey"></param>
        public static void Add(this List<Change> list, int? originalValue, int? newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            if (originalValue.ToString() == newValue.ToString())
            {
                return;
            }

            list.Add(ChangeText(new string[] { originalValue.ToString() }, new string[] { newValue.ToString() }, type, property, lotNum, section, translationKey));
        }

        /// <summary>
        /// Adds an XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Overload for section</param>
        /// <param name="translationKey"></param>
        public static void Add(this List<Change> list, decimal? originalValue, decimal? newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            if (originalValue.ToString("F2") == newValue.ToString("F2"))
            {
                return;
            }

            list.Add(ChangeText(new string[] { originalValue.ToString("F2") }, new string[] { newValue.ToString("F2") }, type, property, lotNum, section, translationKey));
        }

        /// <summary>
        /// Adds an XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Overload for section</param>
        /// <param name="translationKey"></param>
        public static void Add(this List<Change> list, bool? originalValue, bool? newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            if (originalValue == newValue)
            {
                return;
            }

            list.Add(ChangeText(new string[] { originalValue.HasValue ? originalValue.Value.ToYesNo(NoticeLanguage) : "" }, new string[] { newValue.HasValue ? newValue.Value.ToYesNo(NoticeLanguage) : "" }, type, property, lotNum, section, translationKey));
        }

        public static void Add(this List<Change> list, string[] originalValue, string[] newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            string oldValueString = originalValue?.ToParagraphedString();
            string newValueString = newValue?.ToParagraphedString();

            if (oldValueString == newValueString || ( originalValue == null && newValue == null ) || !originalValue.HasAnyContent() && !newValue.HasAnyContent())
            {
                return;
            }

            list.Add(ChangeText(originalValue, newValue, type, property, lotNum, section, translationKey));
        }

        public static void AddNuts(this List<Change> list, string[] originalValue, string[] newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            string oldValueString = originalValue?.ToParagraphedString();
            string newValueString = newValue?.ToParagraphedString();

            if (oldValueString == newValueString || (originalValue == null && newValue == null) || !originalValue.HasAnyContent() && !newValue.HasAnyContent())
            {
                return;
            }

            list.Add(ChangeText(originalValue, newValue, type, property, lotNum, section, translationKey));
        }

        /// <summary>
        /// Adds an XElement to the list, if there is a change in the enum.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section"></param>
        public static void AddEnum<T>(this List<Change> list, T originalValue, T newValue, Type type, string property, int? lotNum = null, string section = null) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (originalValue.ToTedChangeFormatGeneric() == newValue.ToTedChangeFormatGeneric())
            {
                return;
            }

            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;

            var oldTranslated = string.IsNullOrEmpty(originalValue.ToTedChangeFormatGeneric()) ? string.Empty : (string)Translations[NoticeLanguage?.ToLongLang()][originalValue.ToTedChangeFormatGeneric()];
            var newTranslated = string.IsNullOrEmpty(newValue.ToTedChangeFormatGeneric()) ? string.Empty : (string)Translations[NoticeLanguage?.ToLongLang()][newValue.ToTedChangeFormatGeneric()];

            list.Add(new Change
            {
                Section = section ?? tedAttribute?.Section,
                LotNumber = lotNum,
                Label = (string)Translations[NoticeLanguage?.ToLongLang()][tedAttribute.Label],
                OldText = new string[] { oldTranslated },
                NewText = new string[] { newTranslated }
            });
        }

        /// <summary>
        /// Adds an XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Section override</param>
        public static void AddDate(this List<Change> list, DateTime? originalValue, DateTime? newValue, Type type, string property, int? lotNum = null, string section = null)
        {
            if (originalValue.GetValueOrDefault().Date == newValue.GetValueOrDefault().Date
                && originalValue.GetValueOrDefault().Hour == newValue.GetValueOrDefault().Hour
                && originalValue.GetValueOrDefault().Minute == newValue.GetValueOrDefault().Minute)
            {
                return;
            }

            list.Add(ChangeDate(originalValue, newValue, type, property, lotNum, section));
        }

        /// <summary>
        /// Adds a CPV XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Section overload</param>
        public static void AddCpv(this List<Change> list, CpvCode originalValue, CpvCode newValue, Type type, string property, int? lotNum = null, string section = null)
        {
            var originalVocs = originalValue.VocCodes?.Select(x => x.Code) ?? new string[0];
            var newVocs = newValue.VocCodes?.Select(x => x.Code) ?? new string[0];
            if (originalValue.Code == newValue.Code && !(originalVocs.Except(newVocs).Any() || newVocs.Except(originalVocs).Any()))
            {
                return;
            }

            list.Add(ChangeCpv(originalValue, newValue, type, property, lotNum, section));
        }

        /// <summary>
        /// Adds a CPV XElement to the list, if there is a change.
        /// </summary>
        /// <param name="list">The list</param>
        /// <param name="originalValue">Original value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type</param>
        /// <param name="property">Property</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Section overload</param>
        public static void AddAdditionalCpv(this List<Change> list, List<CpvCode> originalValue, List<CpvCode> newValue, Type type, string property, int? lotNum = null, string section = null)
        {
            if (originalValue.All(origCpv => newValue.Any(newCpv => newCpv.Code.Equals(origCpv.Code)
            &&
            (origCpv.VocCodes != null && newCpv.VocCodes != null &&
            !(origCpv.VocCodes.Select(origVoc => origVoc.Code).Except(newCpv.VocCodes.Select(newVoc => newVoc.Code)).Any() ||
                newCpv.VocCodes.Select(newVoc => newVoc.Code).Except(origCpv.VocCodes.Select(origVoc => origVoc.Code)).Any()))
            )))
            {
                return;
            }

            list.Add(ChangeAdditionalCpv(originalValue, newValue, type, property, lotNum, section));
        }

        /// <summary>
        /// Invokes change text with pure parameters. Some times we just don't have a field that changes, but something deducted from other fields...
        /// </summary>
        /// <param name="list"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="lotNum"></param>
        /// <param name="section"></param>
        /// <param name="translationKey"></param>
        /// <param name="translateValues"></param>
        public static void AddRaw(this List<Change> list, string[] oldValue, string[] newValue, int? lotNum = null, string section = null, string translationKey = null, bool translateValues = false)
        {
            var dictionary = Translations[NoticeLanguage?.ToLongLang()];
            var translation = (string)dictionary[translationKey];

            var translatedOldValues = oldValue?.Select(x => translateValues ? (string) dictionary[x] : x).ToArray();
            var translatedNewValues = newValue?.Select(x => translateValues ? (string) dictionary[x] : x).ToArray();

            list.Add(new Change
            {
                Section = section ?? section,
                LotNumber = lotNum,
                Label = translation,
                OldText = translatedOldValues,
                NewText = translatedNewValues
            });
        }

        private static Change ChangeNuts(string[] oldValue, string[] newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;
            JToken languageTranslations = Translations[NoticeLanguage?.ToLongLang()];
            translationKey = translationKey ?? tedAttribute.Label;

            var translation = languageTranslations != null ? (string)languageTranslations[translationKey] : translationKey;

            return new Change
            {
                Section = section ?? tedAttribute?.Section,
                LotNumber = lotNum,
                Label = (string)Translations[NoticeLanguage?.ToLongLang()][tedAttribute.Label],
                OldNutsCodes = oldValue,
                NewNutsCodes = newValue
            };
        }

        private static Change ChangeText(string[] oldValue, string[] newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;
            JToken languageTranslations = Translations[NoticeLanguage?.ToLongLang()];
            translationKey = translationKey ?? tedAttribute.Label;
            
            var translation = languageTranslations != null ? (string)languageTranslations[translationKey] : translationKey;

            return new Change
            {
                Section = section ?? tedAttribute?.Section,
                LotNumber = lotNum,
                Label = (string)Translations[NoticeLanguage?.ToLongLang()][tedAttribute.Label],
                OldText = oldValue,
                NewText = newValue
            };
        }

        private static Change ChangeDate(DateTime? oldValue, DateTime? newValue, Type type, string property, int? lotNo = null, string section = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;

            return new Change
            {
                Section = section ?? tedAttribute?.Section,
                LotNumber = lotNo,
                Label = (string)Translations[NoticeLanguage?.ToLongLang()][tedAttribute.Label],
                OldDate = oldValue,
                NewDate = newValue
            };
        }

        private static Change ChangeCpv(CpvCode oldValue, CpvCode newValue, Type type, string property, int? lotNo = null, string section = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;

            return new Change
            {
                Section = section ?? tedAttribute?.Section,
                LotNumber = lotNo,
                Label = (string)Translations[NoticeLanguage?.ToLongLang()][tedAttribute.Label],
                OldMainCpvCode = oldValue,
                NewMainCpvCode = newValue
            };
        }

        private static Change ChangeAdditionalCpv(List<CpvCode> oldValue, List<CpvCode> newValue, Type type, string property, int? lotNo = null, string section = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;

            return new Change
            {
                Section = section ?? tedAttribute?.Section,
                LotNumber = lotNo,
                Label = (string)Translations[NoticeLanguage?.ToLongLang()][tedAttribute.Label],
                OldAdditionalCpvCodes = oldValue,
                NewAdditionalCpvCodes = newValue
            };
        }
    }
}
