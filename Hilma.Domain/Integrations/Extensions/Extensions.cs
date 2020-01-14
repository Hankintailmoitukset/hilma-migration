using Hilma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hilma.Domain.Integrations.Extensions
{
    public static class Extensions
    {
        public static string ToYesNo(this bool b, string lang)
        {
            switch (lang)
            {
                case "EN":
                    return (b ? "Yes" : "No");
                case "SV":
                    return (b ? "Ja" : "Nej");
                default:
                    return (b ? "Kyllä" : "Ei");
            }
        }
        public static string ToParagraphedString(this string[] value)
        {
            return string.Join("\n", value);
        }

        public static string ToLongLang(this string lang)
        {
            switch (lang)
            {
                case "SV":
                    return "sv-SE";
                case "EN":
                    return "en-GB";
                default:
                    return "fi-FI";
            }
        }

        public static string ToString(this decimal? data, string format, string nullResult = "")
        {
            return data.HasValue ? data.Value.ToString(format) : nullResult;
        }

        /// <summary>
        /// Gets the dto changes from TED XML changes
        /// </summary>
        /// <param name="document">TED changes</param>
        /// <returns>dto changes</returns>
        public static List<Change> GetChanges(this XmlDocument document)
        {
            var result = new List<Change>();
            var jsonObject = JObject.Parse(JsonConvert.SerializeXmlNode(document));
            var changes = jsonObject.Value<JToken>("CHANGES")["CHANGE"];

            if(changes == null)
            {
                return result;
            }

            if(changes.Type == JTokenType.Array)
            {
                foreach (var xmlChange in changes)
                {
                    GetChange(result, xmlChange);
                }
            }
            else
            {
                GetChange(result, changes);
            }

            return result;
        }

        private static void GetChange(List<Change> result, JToken xmlChange)
        {
            var where = xmlChange.Value<JToken>("WHERE");
            var newValue = xmlChange.Value<JToken>("NEW_VALUE");
            var oldValue = xmlChange.Value<JToken>("OLD_VALUE");

            var newParagraphs = newValue?.Value<JToken>("TEXT");
            var oldParagraphs = oldValue?.Value<JToken>("TEXT");

            var newDate = newValue?.Value<string>("DATE");
            var oldDate = oldValue?.Value<string>("DATE");
            var newTime = newValue?.Value<string>("TIME");
            var oldTime = oldValue?.Value<string>("TIME");

            var newMainCpv = newValue?.Value<JToken>("CPV_MAIN");
            var oldMainCpv = oldValue?.Value<JToken>("CPV_MAIN");

            var newAdditionalCpvs = newValue?.Value<JToken>("CPV_ADDITIONAL");
            var oldAdditionalCpvs = oldValue?.Value<JToken>("CPV_ADDITIONAL");

            var change = new Change
            {
                Section = where["SECTION"]?.ToString(),
                Label = where.Value<string>("LABEL"),
                LotNumber = where.Value<string>("LOT_NO"),
                NewText = newParagraphs != null ? GetParagraphs(newParagraphs) : null,
                OldText = oldParagraphs != null ? GetParagraphs(oldParagraphs) : null,
                NewDate = newDate != null ? GetDate(newDate, newTime) : null,
                OldDate = oldDate != null ? GetDate(oldDate, oldTime) : null,
                NewMainCpvCode = newMainCpv != null ? GetCpvCode(newMainCpv) : null,
                OldMainCpvCode = oldMainCpv != null ? GetCpvCode(oldMainCpv) : null,
                NewAdditionalCpvCodes = newAdditionalCpvs != null ? GetCpvCodes(newAdditionalCpvs) : null,
                OldAdditionalCpvCodes = oldAdditionalCpvs != null ? GetCpvCodes(oldAdditionalCpvs) : null
            };
            result.Add(change);
        }

        private static List<CpvCode> GetCpvCodes(JToken additionalCpvs)
        {
            if (additionalCpvs.Type == JTokenType.Array)
            {
                return additionalCpvs.Select(x => GetCpvCode(x)).ToList();
            }
            else
            {
                return new List<CpvCode>() { GetCpvCode(additionalCpvs) };
            }
        }

        private static DateTime? GetDate(string newDate, string newTime)
        {
            return DateTime.Parse($"{newDate} {newTime}"); 
        }

        private static string[] GetParagraphs(JToken value)
        {
            var paragraphs = value.Value<JToken>("P");
            if (paragraphs.Type == JTokenType.Array)
            {
                return paragraphs.Select(x => x.ToString()).ToArray();
            }
            else
            {
                return new string[] { paragraphs.ToString() };
            }
        }

        private static CpvCode GetCpvCode(JToken value)
        {
            var cpv = new CpvCode
            {
                Code = value["CPV_CODE"]["@CODE"].ToString()
            };

            var vocCodes = value.Value<JToken>("CPV_SUPPLEMENTARY_CODE");

            if (vocCodes != null)
            {
                if(vocCodes.Type == JTokenType.Array)
                {
                    cpv.VocCodes = vocCodes.Select(x => new VocCode { Code = x.Value<string>("@CODE") }).ToArray();
                }
                else
                {
                    cpv.VocCodes = new VocCode[1]
                    {
                         new VocCode{ Code = vocCodes.Value<string>("@CODE") }
                    };
                }
            }

            return cpv;
        }
    }
}
