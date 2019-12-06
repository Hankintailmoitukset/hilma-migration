using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.Integrations.General;
using Hilma.Domain.Extensions;
using Newtonsoft.Json.Linq;

namespace Hilma.Domain.Integrations.Extensions
{
    /// <summary>
    /// Extensions for List
    /// </summary>
    public static class ListExtensions
    {
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
        public static void Add(this List<XElement> list, string originalValue, string newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            if (originalValue == newValue || (string.IsNullOrEmpty(originalValue) && string.IsNullOrEmpty(newValue)))
            {
                return;
            }

            list.Add(ChangeText(new string[] { originalValue }, new string[] { newValue }, type, property, lotNum, section, translationKey));
        }

        public static void Add(this List<XElement> list, string[] originalValue, string[] newValue, Type type, string property, int? lotNum = null, string section = null, string translationKey = null)
        {
            string oldValueString = originalValue?.ToParagraphedString();
            string newValueString = newValue?.ToParagraphedString();

            if (oldValueString == newValueString || ( originalValue == null && newValue == null ) || !originalValue.HasAnyContent() && !newValue.HasAnyContent())
            {
                return;
            }

            list.Add(ChangeText(originalValue, newValue, type, property, lotNum, section, translationKey));
        }

        public static string GetTranslation(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : (string)TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()]?[value]??value;
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
        public static void AddEnum(this List<XElement> list, string originalValue, string newValue, Type type, string property, int? lotNum = null)
        {
            if (originalValue == newValue)
            {
                return;
            }
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;

            var oldTranslated = string.IsNullOrEmpty(originalValue) ? string.Empty : (string)TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()][originalValue];
            var newTranslated = string.IsNullOrEmpty(newValue) ? string.Empty : (string)TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()][newValue];

            list.Add(Element("CHANGE",
                        Element("WHERE",
                            Element("SECTION", tedAttribute?.Section),
                            Element("LOT_NO", lotNum?.ToString()),
                            Element("LABEL", (string)TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()][tedAttribute.Label])),
                        Element("OLD_VALUE",
                            string.IsNullOrEmpty(oldTranslated) ? new XElement(TedHelpers.Xmlns + "NOTHING") : PElement("TEXT", oldTranslated)),
                        Element("NEW_VALUE",
                            string.IsNullOrEmpty(newTranslated) ? new XElement(TedHelpers.Xmlns + "NOTHING") : PElement("TEXT", newTranslated))));
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
        public static void AddDate(this List<XElement> list, DateTime? originalValue, DateTime? newValue, Type type, string property, int? lotNum = null, string section = null)
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
        /// <param name="isMainCpv">Is main CPV?</param>
        /// <param name="lotNum">Lot number</param>
        /// <param name="section">Section overload</param>
        public static void AddCpv(this List<XElement> list, CpvCode originalValue, CpvCode newValue, Type type, string property, bool isMainCpv, int? lotNum = null, string section = null)
        {
            var originalVocs = originalValue.VocCodes?.Select(x => x.Code) ?? new string[0];
            var newVocs = newValue.VocCodes?.Select(x => x.Code) ?? new string[0];
            if (originalValue.Code == newValue.Code && !(originalVocs.Except(newVocs).Any() || newVocs.Except(originalVocs).Any()))
            {
                return;
            }

            list.Add(ChangeCpv(originalValue, newValue, type, property, isMainCpv, lotNum, section));
        }

        /// <summary>
        /// Invokes change text with pure parameters. Some times we just don't have a field that changes, but something deducted from other fields...
        /// </summary>
        /// <param name="list"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <param name="notNum"></param>
        /// <param name="section"></param>
        /// <param name="translationKey"></param>
        public static void AddRaw(this List<XElement> list, string[] oldValue, string[] newValue, int? notNum = null, string section = null, string translationKey = null, bool translateValues = false)
        {
            var dictionary = TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()];
            var translation = (string)dictionary[translationKey];

            var translatedOldValues = oldValue?.Select(x => translateValues ? (string) dictionary[x] : x).ToArray();
            var translatedNewValues = newValue?.Select(x => translateValues ? (string) dictionary[x] : x).ToArray();

            list.Add(Element("CHANGE",
                Element("WHERE",
                    Element("SECTION", section),
                    Element("LOT_NO", notNum?.ToString()),
                    Element("LABEL", translation)),
                Element("OLD_VALUE",
                    // Can be NOTHING, CPV_MAIN, CPV_ADDITIONAL, TEXT or DATE
                    translatedOldValues?.Any() == true ? PElement("TEXT", translatedOldValues) : new XElement(TedHelpers.Xmlns + "NOTHING")),
                Element("NEW_VALUE",
                    translatedNewValues?.Any() == true ? PElement("TEXT", translatedNewValues) : new XElement(TedHelpers.Xmlns + "NOTHING"))));
        }

        private static XElement ChangeText(string[] oldValue, string[] newValue, Type type, string property, int? notNum = null, string section = null, string translationKey = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;
            JToken languageTranslations = TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()];
            translationKey = translationKey ?? tedAttribute.Label;
            
            var translation = languageTranslations != null ? (string)languageTranslations[translationKey] : translationKey;

            return Element("CHANGE",
                    Element("WHERE",
                        Element("SECTION", section ?? tedAttribute?.Section),
                        Element("LOT_NO", notNum?.ToString()),
                        Element("LABEL", translation)),
                    Element("OLD_VALUE",
                        // Can be NOTHING, CPV_MAIN, CPV_ADDITIONAL, TEXT or DATE
                        oldValue == null || oldValue.Any() ? PElement("TEXT", oldValue) : new XElement(TedHelpers.Xmlns + "NOTHING")),
                    Element("NEW_VALUE",
                        newValue == null || newValue.Any() ? PElement("TEXT", newValue) : new XElement(TedHelpers.Xmlns + "NOTHING")));
        }

        private static XElement ChangeDate(DateTime? oldValue, DateTime? newValue, Type type, string property, int? lotNo = null, string section = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;

            return Element("CHANGE",
                    Element("WHERE",
                        Element("SECTION", section ?? tedAttribute?.Section),
                        Element("LOT_NO", lotNo?.ToString()),
                        Element("LABEL", (string)TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()][tedAttribute.Label])),
                    Element("OLD_VALUE",
                        oldValue == null ? new List<XElement>() { new XElement(TedHelpers.Xmlns + "NOTHING") } :
                            new List<XElement>() {
                                Element("DATE", oldValue.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                                Element("TIME", oldValue.Value.ToString("hh:mm", CultureInfo.InvariantCulture))
                            }),
                    Element("NEW_VALUE",
                        newValue == null ? new List<XElement>() { new XElement(TedHelpers.Xmlns + "NOTHING") } :
                            new List<XElement>() {
                                Element("DATE", newValue.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                                Element("TIME", newValue.Value.ToString("hh:mm", CultureInfo.InvariantCulture))
                            }));
        }

        private static XElement ChangeCpv(CpvCode oldValue, CpvCode newValue, Type type, string property, bool isMainCpv, int? lotNo = null, string section = null)
        {
            var tedAttribute = type.GetProperty(property).GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(CorrigendumLabelAttribute)) as CorrigendumLabelAttribute;

            return Element("CHANGE",
                    Element("WHERE",
                        Element("SECTION", section ?? tedAttribute?.Section),
                        Element("LOT_NO", lotNo?.ToString()),
                        Element("LABEL", (string)TedNoticeFactory.Translations[TedNoticeFactory.NoticeLanguage?.ToLongLang()][tedAttribute.Label])),
                    Element("OLD_VALUE",
                        string.IsNullOrEmpty(oldValue.Code) ? new XElement(TedHelpers.Xmlns + "NOTHING") :
                        Element(isMainCpv ? "CPV_MAIN" : "CPV_ADDITIONAL",
                            ElementWithAttribute("CPV_CODE", "CODE", oldValue.Code),
                            oldValue.VocCodes?.Select(x => ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", x.Code)))),
                    Element("NEW_VALUE",
                        string.IsNullOrEmpty(newValue.Code) ? new XElement(TedHelpers.Xmlns + "NOTHING") :
                        Element(isMainCpv ? "CPV_MAIN" : "CPV_ADDITIONAL",
                            ElementWithAttribute("CPV_CODE", "CODE", newValue.Code),
                            newValue.VocCodes?.Select(x => ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", x.Code)))));
        }

        private static XElement Element(string name, params object[] value)
        {
            return value != null && value.Any(a => a != null) ? new XElement(TedHelpers.Xmlns + name.ToUpper(), value.Where(a => a != null)) : null;
        }

        private static XElement Element(string name, string value)
        {
            return !string.IsNullOrEmpty(value?.Trim()) ? new XElement(TedHelpers.Xmlns + name.ToUpper(), value) : null;
        }

        private static XElement ElementWithAttribute(string elementName, string attributeName, string attributeValue, string elementValue = null)
        {
            return !string.IsNullOrWhiteSpace(attributeValue)
                ? new XElement(TedHelpers.Xmlns + elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), elementValue) : null;
        }
        private static XElement PElement(string name, string value)
        {
            return !string.IsNullOrEmpty(value?.Trim()) ? new XElement(TedHelpers.Xmlns + name.ToUpper(), new XElement(TedHelpers.Xmlns + "P", value)) : null;
        }


        private static XElement PElement(string name, string[] value)
        {
            if (value == null || !value.Any())
                return null;

            return new XElement(TedHelpers.Xmlns + name.ToUpper(), value.Select(p => new XElement(TedHelpers.Xmlns + "P", p)));
        }
    }
}
