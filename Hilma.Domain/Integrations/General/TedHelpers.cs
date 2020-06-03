using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;

namespace Hilma.Domain.Integrations.General
{

    /// <summary>
    /// Helpers methods to build XML elements for TED
    /// </summary>
    public class TedHelpers
    {
        public static readonly XNamespace Xmlns = "http://publications.europa.eu/resource/schema/ted/R2.0.9/reception";
        public static readonly XNamespace n2016 = "http://publications.europa.eu/resource/schema/ted/2016/nuts";
        public static readonly XNamespace xs = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="eSenderLogin"></param>
        /// <param name="tedSenderOrganisation"></param>
        /// <param name="tedContactEmail"></param>
        /// <returns></returns>
        public static XElement LoginPart(NoticeContract notice, string eSenderLogin, string tedSenderOrganisation, string tedContactEmail)
        {
            return Element("SENDER",
                Element("IDENTIFICATION",
                    Element("ESENDER_LOGIN", eSenderLogin),
                      Element("NO_DOC_EXT", notice.NoticeNumber)),
                Element("CONTACT", Element("ORGANISATION", tedSenderOrganisation),
                    Element("COUNTRY", new XAttribute("VALUE", "FI")),
                    Element("E_MAIL", notice.ContactPerson?.Email ?? tedContactEmail)));
        }

        /// <summary>
        /// Legal basis. Used in forms F01-F03
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">Needed if noticetype = modification</param>
        /// <returns></returns>
        public static List<XElement> LegalBasis(NoticeContract notice, NoticeContract parent = null)
        {

            string directive = string.IsNullOrEmpty( notice.LegalBasis ) ? DirectiveMapper.GetDirective(notice, parent) : notice.LegalBasis;
            return new List<XElement>
            {
                ElementWithAttribute("LEGAL_BASIS", "VALUE", directive)
            };
        }
        
        internal static XDocument CreateTedDocument(params XElement[] xElements)
        {
            return new XDocument(
                 new XDeclaration("1.0", "utf-8", null), TedHelpers.Element("TED_ESENDERS",
                     new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.n2016), TedHelpers.n2016),
                     new XAttribute("VERSION", "R2.0.9.S03"),
                     new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.xs), TedHelpers.xs),
                     xElements));
        } 
        /// <summary>
        /// Contracting authority fields
        /// </summary>
        /// <param name="elementName">Name of element(ADDRESS_CONTRACTING_BODY or ADDRESS_CONTRACTING_BODY_ADDITIONAL)</param>
        /// <param name="organisation">Organisation</param>
        /// <param name="contactPerson">Contact person</param>
        /// <returns>Xelement</returns>
            public static XElement ADDRS1(string elementName, OrganisationContract organisation, ContactPerson contactPerson)
        {
            if (organisation == null)
            {
                return null;
            }
            
            return Element(elementName,
                    Element("OFFICIALNAME", organisation.Information?.OfficialName),
                    Element("NATIONALID", organisation.Information?.NationalRegistrationNumber),
                    Element("ADDRESS", organisation.Information?.PostalAddress?.StreetAddress),
                    Element("TOWN", organisation.Information?.PostalAddress?.Town),
                    Element("POSTAL_CODE", organisation.Information?.PostalAddress?.PostalCode),
                    ElementWithAttribute("COUNTRY", "VALUE", organisation.Information?.PostalAddress?.Country),
                    !string.IsNullOrEmpty(contactPerson?.Name) ? Element("CONTACT_POINT", contactPerson?.Name) : null,
                    Element("PHONE", contactPerson?.Phone),
                    Element("E_MAIL", contactPerson?.Email),
                    organisation?.Information?.NutsCodes.ToList().Select(x => new XElement(n2016 + "NUTS", new XAttribute("CODE", x))),
                    Element("URL_GENERAL", organisation.Information?.MainUrl)
                );
        }

        /// <summary>
        /// ADDR-S1
        /// </summary>
        /// <param name="elementName">Name of element(ADDRESS_CONTRACTING_BODY or ADDRESS_CONTRACTING_BODY_ADDITIONAL)</param>
        /// <param name="information">Contract body contact</param>
        /// <returns>XElement</returns>
        public static XElement ADDRS1(string elementName, ContractBodyContactInformation information)
        {
            if (information == null)
            {
                return null;
            }
            return Element(elementName,
                Element("OFFICIALNAME", information.OfficialName),
                Element("NATIONALID", information.NationalRegistrationNumber),
                Element("ADDRESS", information.PostalAddress?.StreetAddress),
                Element("TOWN", information.PostalAddress?.Town),
                Element("POSTAL_CODE", information.PostalAddress?.PostalCode),
                ElementWithAttribute("COUNTRY", "VALUE", information.PostalAddress?.Country),
                Element("E_MAIL", information.Email),
                information.NutsCodes?.ToList().Select(x => new XElement(n2016 + "NUTS", new XAttribute("CODE", x))),
                Element("URL_GENERAL", information.MainUrl)
            );
        }

        /// <summary>
        /// Address (section 5)
        /// </summary>
        /// <param name="contractor">Contractor contat information</param>
        /// <param name="rootElement">Root element</param>
        /// <returns>Xelement</returns>
        public static XElement ADDRS5(ContractorContactInformation contractor, string rootElement = "ADDRESS_CONTRACTOR")
        {
            if (contractor == null)
                return null;

            return Element(rootElement,
                    Element("OFFICIALNAME", contractor.OfficialName),
                    Element("NATIONALID", contractor.NationalRegistrationNumber),
                    Element("ADDRESS", contractor.PostalAddress?.StreetAddress),
                    Element("TOWN", contractor.PostalAddress?.Town),
                    Element("POSTAL_CODE", contractor.PostalAddress?.PostalCode),
                    ElementWithAttribute("COUNTRY", "VALUE", contractor.PostalAddress?.Country),
                    Element("PHONE", contractor.TelephoneNumber),
                    Element("E_MAIL", contractor.Email),
                    contractor.NutsCodes.ToList().Select(x => new XElement(n2016 + "NUTS", new XAttribute("CODE", x))),
                    Element("URL", contractor.MainUrl)
                );
        }
        /// <summary>
        /// ADDR-S6 Review body
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        public static XElement ADDRS6(string elementName, ContractBodyContactInformation information)
        {
            if (information == null)
                return null;

            return Element(elementName,
                Element("OFFICIALNAME", information.OfficialName),
                Element("ADDRESS", information.PostalAddress?.StreetAddress),
                Element("TOWN", information.PostalAddress?.Town),
                Element("POSTAL_CODE", information.PostalAddress?.PostalCode),
                ElementWithAttribute("COUNTRY", "VALUE", information.PostalAddress?.Country),
                Element("PHONE", information.TelephoneNumber),
                Element("E_MAIL", information.Email),
                Element("URL", information.MainUrl)
            );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="elementValue"></param>
        /// <returns></returns>
        public static XElement ElementWithAttribute(string elementName, string attributeName, string attributeValue, string elementValue = null)
        {
            return !string.IsNullOrWhiteSpace(attributeValue)
                ? new XElement(Xmlns + elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), elementValue) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="elementValue"></param>
        /// <returns></returns>
        public static XElement DateElementWithAttribute(string elementName, string attributeName, string attributeValue, DateTime? elementValue = null)
        {
            return !string.IsNullOrWhiteSpace(attributeValue)
                ? new XElement(Xmlns + elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), elementValue?.ToString("yyyy-MM-dd")) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement ElementWithAttribute(string elementName, string attributeName, string attributeValue, params object[] value)
        {
            return !string.IsNullOrWhiteSpace(attributeValue)
                ? new XElement(Xmlns + elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), value.Where(a => a != null)) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static List<XElement> CpvCodeElement(string elementName, CpvCode[] codes){
            if( codes == null || !codes.Any())
                return null;

            var elements = new List<XElement>();
            foreach(var code in codes.Where( c=> !string.IsNullOrWhiteSpace(c?.Code)))
            {
                elements.Add(Element(elementName, ElementWithAttribute("CPV_CODE", "CODE", code.Code),
                    code.VocCodes?.Select(v => ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", v.Code))));
            }

            return elements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static XElement ElementWithAttribute(string elementName, string attributeName, object attributeValue)
        {
            return attributeValue != null
                ? new XElement(Xmlns + elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue)) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(string name, string value)
        {
            return !string.IsNullOrEmpty(value?.Trim()) ? new XElement(Xmlns + name.ToUpper(), value) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement PElement(string name, string[] value)
        {
            if (value == null || !value.Any() || value.All(string.IsNullOrEmpty))
            {
                return null;
            }

            return new XElement(Xmlns + name.ToUpper(), value.Where(x => x.Length > 0).Select(p => new XElement(Xmlns + "P", p)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement PElement(string name, string value)
        {
            if (value == null || string.IsNullOrEmpty(value))
                return null;

            return new XElement(Xmlns + name.ToUpper(), new XElement(Xmlns + "P", value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="elementValue"></param>
        /// <returns></returns>
        public static XElement PElementWithAttribute(string elementName, string attributeName, string attributeValue, string[] elementValue = null)
        {
            return !string.IsNullOrWhiteSpace(attributeValue)
                ? new XElement(Xmlns + elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), elementValue.Where(x => x.Length > 0).Select(p => new XElement(Xmlns + "P", p))) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(string name, XAttribute attribute, object value)
        {
            return !(value is string) && value != null || value is string str && !string.IsNullOrEmpty(str?.Trim()) ? new XElement(Xmlns + name.ToUpper(), attribute, value) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        public static XElement Element(string name, int value, int? minValue = null)
        {
            return !minValue.HasValue || value >= minValue.Value ? new XElement(Xmlns + name.ToUpper(), value) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(string name, int? value)
        {
            return value.HasValue ? new XElement(Xmlns + name.ToUpper(), value) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(string name, params object[] value)
        {
            return value != null && value.Any(a => a != null) ? new XElement(Xmlns + name.ToUpper(), value.Where(a => a != null)) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XElement Element(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }
            return new XElement(Xmlns + name.ToUpper());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(XName name, params object[] value)
        {
            return value != null && value.Any(a => a != null) ? new XElement(name, value.Where(a => a != null)) : null;
        }

        public static XElement DateElement(string name, DateTime? value)
        {
            return value != null ? new XElement(Xmlns + name.ToUpper(), value?.ToString("yyyy-MM-dd")) : null;
        }

        public static XElement TimeElement(string name, DateTime? value)
        {
            var timeElement = value.HasValue
                ? new XElement(Xmlns + name.ToUpper(), value?.ToString("HH':'mm"))
                : null;
            return timeElement;
        }
    }
}
