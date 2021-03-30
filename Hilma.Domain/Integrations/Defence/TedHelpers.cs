using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;

namespace Hilma.Domain.Integrations.Defence
{
    /// <summary>
    /// Helpers methods to build XML elements for TED
    /// </summary>
    public class TedHelpers
    {
        public static readonly XNamespace Reception = "http://publications.europa.eu/resource/schema/ted/R2.0.8/reception";
        public static readonly XNamespace n2016 = "http://publications.europa.eu/resource/schema/ted/2021/nuts";
        public static readonly XNamespace xs = "http://www.w3.org/2001/XMLSchema-instance";
        
        internal static XDocument CreateTedDocument(params XElement[] xElements)
        {
            return new XDocument(new XDeclaration("1.0", "UTF-8", null),
                    Element("TED_ESENDERS",
                     new XAttribute(XNamespace.Xmlns + nameof(xs), xs),
                     xElements));
        } 

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
                ElementWithAttribute("LOGIN", "CLASS", "B",
                    Element("ESENDER_LOGIN", eSenderLogin)),
                Element("USER",
                    Element("USER_E_MAILS",
                    ElementWithAttribute("USER_E_MAIL", "TYPE", "FUNCTIONAL", notice.ContactPerson?.Email ?? tedContactEmail))),
                Element("NO_DOC_EXT", notice.NoticeNumber));
        }
              

        /// <summary>
        /// CA/CE/Concessionaire profile
        /// </summary>
        /// <param name="elementName">Name of element</param>
        /// <param name="organisation">Organisation</param>
        /// <param name="contactPerson">Contact person</param>
        /// <returns>Xelement</returns>
        public static XElement INC_01(string elementName, OrganisationContract organisation, ContactPerson contactPerson)
        {
            return Element(elementName,
                    Element("ORGANISATION",
                        Element("OFFICIALNAME", organisation?.Information?.OfficialName),
                        Element("NATIONALID", organisation?.Information?.NationalRegistrationNumber)
                    ),
                    Element("ADDRESS", organisation?.Information?.PostalAddress?.StreetAddress),
                    Element("TOWN", organisation?.Information?.PostalAddress?.Town),
                    Element("POSTAL_CODE", organisation?.Information?.PostalAddress?.PostalCode),
                    ElementWithAttribute("COUNTRY", "VALUE", organisation?.Information?.PostalAddress?.Country),
                    !string.IsNullOrEmpty(contactPerson.Name) ? Element("CONTACT_POINT", contactPerson?.Name) : null,
                    Element("PHONE", contactPerson?.Phone),
                    Element("E_MAILS",
                        Element("E_MAIL", contactPerson?.Email)
                    )
                );
        }

        /// <summary>
        /// INC01 without contact person and emails
        /// </summary>
        /// <param name="elementName">Name of element</param>
        /// <param name="organisation">Organisation</param>
        /// <returns>Xelement</returns>
        public static XElement INC_01(string elementName, OrganisationContract organisation)
        {
            var information = organisation?.Information;
            return Element(elementName,
                    Element("ORGANISATION",
                        Element("OFFICIALNAME", information?.OfficialName),
                        Element("NATIONALID", information?.NationalRegistrationNumber)
                    ),
                    Element("ADDRESS", information?.PostalAddress?.StreetAddress),
                    Element("TOWN", information?.PostalAddress?.Town),
                    Element("POSTAL_CODE", information?.PostalAddress?.PostalCode),
                    ElementWithAttribute("COUNTRY", "VALUE", information?.PostalAddress?.Country)
                );
        }

        /// <summary>
        /// Conact data
        /// </summary>
        /// <param name="information">ContractBodyContactInformation</param>
        /// <returns>Xelement</returns>
        public static XElement INC_04(ContractBodyContactInformation information)
        {
            if (information == null)
                return null;

            return Element("CONTACT_DATA",
                        Element("ORGANISATION",
                            Element("OFFICIALNAME", information.OfficialName),
                            Element("NATIONALID", information.NationalRegistrationNumber)
                        ),
                        Element("ADDRESS", information.PostalAddress?.StreetAddress),
                        Element("TOWN", information.PostalAddress?.Town),
                        Element("POSTAL_CODE", information.PostalAddress?.PostalCode),
                        ElementWithAttribute("COUNTRY", "VALUE", information.PostalAddress?.Country),
                        Element("CONTACT_POINT", information.ContactPerson),
                        Element("PHONE", information.TelephoneNumber),
                        Element("E_MAILS",
                            Element("E_MAIL", information.Email)
                        ),
                        Element("URL", information.MainUrl)
                    );
        }

        /// <summary>
        /// Address (section 5)
        /// </summary>
        /// <param name="contractor">Contractor contat information</param>
        /// <returns>Xelement</returns>
        public static XElement INC_05(ContractorContactInformation contractor)
        {
            return Element("CONTACT_DATA_WITHOUT_RESPONSIBLE_NAME",
                   Element("ORGANISATION",
                        Element("OFFICIALNAME", contractor.OfficialName),
                        Element("NATIONALID", contractor.NationalRegistrationNumber)
                    ),
                    Element("ADDRESS", contractor.PostalAddress?.StreetAddress),
                    Element("TOWN", contractor.PostalAddress?.Town),
                    Element("POSTAL_CODE", contractor.PostalAddress?.PostalCode),
                    ElementWithAttribute("COUNTRY", "VALUE", contractor.PostalAddress?.Country),
                    Element("E_MAILS",
                         Element("E_MAIL", contractor.Email)
                    ),
                    Element("PHONE", contractor.TelephoneNumber),
                    Element("URL", contractor.MainUrl)
                );
        }
        /// <summary>
        /// Address (section 5)
        /// </summary>
        /// <param name="organisation">Contractor contat information</param>
        /// <returns>Xelement</returns>
        public static XElement INC_05(ContractBodyContactInformation organisation)
        {
            return Element("CONTACT_DATA_WITHOUT_RESPONSIBLE_NAME",
                   Element("ORGANISATION",
                        Element("OFFICIALNAME", organisation.OfficialName),
                        Element("NATIONALID", organisation.NationalRegistrationNumber)
                    ),
                    Element("ADDRESS", organisation.PostalAddress?.StreetAddress),
                    Element("TOWN", organisation.PostalAddress?.Town),
                    Element("POSTAL_CODE", organisation.PostalAddress?.PostalCode),
                    ElementWithAttribute("COUNTRY", "VALUE", organisation.PostalAddress?.Country),
                    Element("E_MAILS",
                         Element("E_MAIL", organisation.Email)
                    ),
                    Element("PHONE", organisation.TelephoneNumber),
                    Element("URL", organisation.MainUrl)
                );
        }

        private static string FormatNoticeTedId(DateTime dateCreated, int noticeId)
        {
            return $"{dateCreated.Year}-{(noticeId%1000000):D6}";
        }

        /// <summary>
        /// 2019-000000-000 	vuosi-id-lot
        /// </summary>
        /// <param name="notice">Notice conrtact</param>
        /// <param name="lotNumber">lot number</param>
        /// <returns></returns>
        public static string GetContractNumber(NoticeContract notice, int lotNumber)
        {
            return $"{DateTime.Now.Year}-{(notice.Id % 1000000):D6}-{(lotNumber % 1000):D3}";
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
                ? new XElement(elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), elementValue) : null;
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
                ? new XElement(elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), elementValue?.ToString("yyyy-MM-dd")) : null;
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
                ? new XElement(elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue), value.Where(a => a != null)) : null;
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
            foreach(var code in codes.Where( c=> !string.IsNullOrWhiteSpace(c.Code)))
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
                ? new XElement(elementName.ToUpper(), new XAttribute(attributeName.ToUpper(), attributeValue)) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(string name, string value)
        {
            return !string.IsNullOrEmpty(value?.Trim()) ? new XElement(name.ToUpper(), value) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement PElement(string name, string[] value)
        {
            if (value == null || !value.Any() || value.All( string.IsNullOrEmpty ))
                return null;

            return new XElement(name.ToUpper(), value.Where( v => !string.IsNullOrEmpty(v)).Select( p => new XElement("P", p)));
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

            return new XElement(name.ToUpper(), new XElement("P", value));
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
            return !(value is string) && value != null || value is string str && !string.IsNullOrEmpty(str?.Trim()) ? new XElement(name.ToUpper(), attribute, value) : null;
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
            return !minValue.HasValue || value >= minValue.Value ? new XElement(name.ToUpper(), value) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(string name, int? value)
        {
            return value.HasValue ? new XElement(name.ToUpper(), value) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XElement Element(string name, params object[] value)
        {
            return value != null && value.Any(a => a != null) ? new XElement(name.ToUpper(), value.Where(a => a != null)) : null;
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
            return new XElement(name.ToUpper());
        }

        public static XElement DateElement(string name, DateTime? value)
        {
            return value != null && value != default(DateTime) ? Element(name.ToUpper(),
                    Element("DAY", value?.Day),
                    Element("MONTH", value?.Month),
                    Element("YEAR", value?.Year))
                    : null;
        }

        public static XElement DateTimeElement(string name, DateTime? value)
        {
            return value != null && value != default(DateTime) ? new XElement(name.ToUpper(),
                    new XElement("DAY", value?.Day),
                    new XElement("MONTH", value?.Month),
                    new XElement("YEAR", value?.Year),
                    new XElement("TIME", value?.Hour.ToString("00.##") + ":" + value?.Minute.ToString("00.##")))
                    : null;
        }

        public static XDocument SetRootNamespace(XDocument document)
        {
            SetDefaultXmlNamespace( document.Root, Reception);
            return document;
        }

        private static void SetDefaultXmlNamespace(XElement xelem, XNamespace xmlns)
        {
            if (xelem.Name.NamespaceName == string.Empty)
                xelem.Name = xmlns + xelem.Name.LocalName;
            foreach (var e in xelem.Elements())
                SetDefaultXmlNamespace(e,xmlns);
        }

    }
}
