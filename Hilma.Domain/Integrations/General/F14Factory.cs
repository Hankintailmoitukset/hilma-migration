using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Defence;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// TED F14 Corrigendum Notice Factory - Generates TED integration XML
    /// </summary>
    public class F14Factory
    {
        private readonly NoticeContract _notice;
        private readonly NoticeContract _parent;
        private readonly string _eSenderLogin;
        private readonly string _tedSenderOrganisation;
        private readonly string _tedContactEmail;
        private readonly ITranslationProvider _translationProvider;

        private static readonly XNamespace xmlns = "http://publications.europa.eu/resource/schema/ted/R2.0.9/reception";

        /// <summary>
        /// F02 Corrigendum Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedSenderOrganisation"></param>
        /// <param name="tedContactEmail"></param>
        /// <param name="translationProvider"></param>
        public F14Factory(NoticeContract notice, NoticeContract parent, string eSenderLogin,
            string tedSenderOrganisation, string tedContactEmail, ITranslationProvider translationProvider)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedSenderOrganisation = tedSenderOrganisation;
            _tedContactEmail = tedContactEmail;
            _parent = parent;
            _translationProvider = translationProvider;
        }

        /// <summary>
        /// Creates the XML document that is sent to TED.
        /// </summary>
        /// <returns></returns>
        public XDocument Form14()
        {
            return new XDocument(
                new XDeclaration("1.0", "utf-8", null), TedHelpers.Element("TED_ESENDERS",
                    new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.n2016), TedHelpers.n2016),
                    new XAttribute("VERSION", "R2.0.9.S03"),
                    new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.xs), TedHelpers.xs),
                    TedHelpers.LoginPart(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail),
                    NoticeBody()));
        }

        /// <summary>
        /// #  XSD name : F14_2014
        /// #  RELEASE : "R2.0.9.S03"                                                      
        /// #  Intermediate release number 007-20181030                               
        /// #  Last update : 08/06/2018 
        /// #  Form : Corrigendum
        /// </summary>
        private XElement NoticeBody()
        {
            return TedHelpers.Element("FORM_SECTION",
                TedHelpers.Element("F14_2014", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "F14"),
                    TedHelpers.LegalBasis(_notice, _parent),
                    ContractingBody(_notice.Project.Organisation, _notice.ContactPerson, _notice.CommunicationInformation),
                    ObjectContract(),
                    ComplementaryInformation(),
                    TedHelpers.Element("CHANGES",
                        ChangesToXml(new NoticeChangesFactory(_notice, _parent, _translationProvider).Changes()),
                        TedHelpers.PElement("INFO_ADD", _notice.CorrigendumAdditionalInformation))));
        }

        private List<XElement> ChangesToXml(List<Change> changes)
        {
            var result = new List<XElement>
            {
                new XElement(xmlns + "MODIFICATION_ORIGINAL", new XAttribute("PUBLICATION", "NO"))
            };

            foreach (var change in changes)
            {
                // Can be NOTHING, CPV_MAIN, CPV_ADDITIONAL, TEXT or DATE
                result.Add(
                    TedHelpers.Element("CHANGE",
                        TedHelpers.Element("WHERE",
                            TedHelpers.Element("SECTION", change.Section),
                            TedHelpers.Element("LOT_NO", change.LotNumber),
                            TedHelpers.Element("LABEL", change.Label)),
                    TedHelpers.Element("OLD_VALUE",
                        OldValue(change)
                    ),
                    TedHelpers.Element("NEW_VALUE",
                        NewValue(change)
                    )));
            }

            return result;
        }

        private static List<XElement> OldValue(Change change)
        {
            List<XElement> oldValue;
            if (change.OldText != null && change.OldText.Length > 0 && !string.IsNullOrEmpty(change.OldText[0]))
            {
                oldValue = new List<XElement> { TedHelpers.PElement("TEXT", change.OldText) };
            }
            else if (change.OldDate != null && change.OldDate != DateTime.MinValue)
            {
                oldValue = new List<XElement> { TedHelpers.DateElement("DATE", change.OldDate) };
            }
            else if (change.OldMainCpvCode != null && !string.IsNullOrEmpty(change.OldMainCpvCode.Code))
            {
                oldValue = new List<XElement> { TedHelpers.Element("CPV_MAIN",
                                TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", change.OldMainCpvCode.Code),
                                change.OldMainCpvCode.VocCodes?.Select(x => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", x.Code))) };
            }
            else if (change.OldAdditionalCpvCodes != null && change.OldAdditionalCpvCodes.Count > 0)
            {
                oldValue = change.OldAdditionalCpvCodes.Select(x =>
                             TedHelpers.Element("CPV_ADDITIONAL",
                                 TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", x.Code),
                                 x.VocCodes?.Select(y => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", y.Code)))
                        ).ToList();
            }
            else
            {
                oldValue = new List<XElement> { TedHelpers.Element("NOTHING") };
            }

            return oldValue;
        }

        private static List<XElement> NewValue(Change change)
        {
            List<XElement> newValue;
            if (change.NewText != null && change.NewText.Length > 0 && !string.IsNullOrEmpty(change.NewText[0]))
            {
                newValue = new List<XElement> { TedHelpers.PElement("TEXT", change.NewText) };
            }
            else if (change.NewDate != null && change.NewDate != DateTime.MinValue)
            {
                newValue = new List<XElement> { TedHelpers.DateElement("DATE", change.NewDate) };
            }
            else if (change.NewMainCpvCode != null && !string.IsNullOrEmpty(change.NewMainCpvCode.Code))
            {
                newValue = new List<XElement> { TedHelpers.Element("CPV_MAIN",
                                TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", change.NewMainCpvCode.Code),
                                change.NewMainCpvCode.VocCodes?.Select(x => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", x.Code))) };
            }
            else if (change.NewAdditionalCpvCodes != null && change.NewAdditionalCpvCodes.Count > 0)
            {
                newValue = change.NewAdditionalCpvCodes.Select(x =>
                             TedHelpers.Element("CPV_ADDITIONAL",
                                 TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", x.Code),
                                 x.VocCodes?.Select(y => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", y.Code)))
                        ).ToList();
            }
            else
            {
                newValue = new List<XElement> { TedHelpers.Element("NOTHING") };
            }

            return newValue;
        }

        #region Section I: Contracting authority
        /// <summary>
        /// Section I: Contracting authority
        /// </summary>
        /// <param name="organisation">The organisation</param>
        /// <param name="contactPerson">The contact person</param>
        /// <param name="communicationInformation">I.3 Communication</param>
        /// <returns>CONTRACTING_BODY XElement</returns>
        private XElement ContractingBody(OrganisationContract organisation, ContactPerson contactPerson, CommunicationInformation communicationInformation)
        {
            return TedHelpers.Element("CONTRACTING_BODY",
                    CAFields("ADDRESS_CONTRACTING_BODY", organisation, contactPerson),
                    CAFields("ADDRESS_CONTRACTING_BODY_ADDITIONAL", organisation, contactPerson)
                );
        }

        private XElement CAFields(string elementName, OrganisationContract organisation, ContactPerson contactPerson)
        {
            return TedHelpers.Element(elementName,
                    TedHelpers.Element("OFFICIALNAME", organisation.Information.OfficialName),
                    TedHelpers.Element("NATIONALID", organisation.Information.NationalRegistrationNumber),
                    TedHelpers.Element("ADDRESS", organisation.Information.PostalAddress.StreetAddress),
                    TedHelpers.Element("TOWN", organisation.Information.PostalAddress.Town),
                    TedHelpers.Element("POSTAL_CODE", organisation.Information.PostalAddress.PostalCode),
                    TedHelpers.ElementWithAttribute("COUNTRY", "VALUE", organisation.Information.PostalAddress.Country),
                    TedHelpers.Element("CONTACT_POINT", contactPerson.Name), TedHelpers.Element("PHONE", contactPerson.Phone),
                    TedHelpers.Element("E_MAIL", contactPerson.Email),
                    organisation.Information.NutsCodes.ToList().Select(x => new XElement(TedHelpers.n2016 + "NUTS", new XAttribute("CODE", x))),
                    TedHelpers.Element("URL_GENERAL", organisation.Information.MainUrl)
                );
        }
        #endregion

        #region Section II: Object
        /// <summary>
        /// Section II: Object
        /// </summary>
        /// <returns>OBJECT_CONTRACT XElement</returns>
        private XElement ObjectContract()
        {
            var shortDescr = _notice.ProcurementObject.ShortDescription;

            // Using object description in design contest, because it cannot be lotted and doesn't have ShortDescription
            if (_notice.Type == NoticeType.DesignContest || _notice.Type == NoticeType.DesignContestResults)
            {
                shortDescr = _notice.ObjectDescriptions.FirstOrDefault().DescrProcurement;
            }

            if(_notice.Type == NoticeType.DefencePriorInformation)
            {
                shortDescr = _notice.ProcurementObject.Defence.TotalQuantity;
            }

            var contract = TedHelpers.Element("OBJECT_CONTRACT",
                TedHelpers.PElement("TITLE", _notice.Project.Title),
                TedHelpers.Element("REFERENCE_NUMBER", _notice.Project.ReferenceNumber),
                TedHelpers.CpvCodeElement("CPV_MAIN", new CpvCode[] { _notice.ProcurementObject.MainCpvCode }),
                TedHelpers.ElementWithAttribute("TYPE_CONTRACT", "CTYPE", _notice.Project.ContractType.ToTEDFormat()),
                TedHelpers.PElement("SHORT_DESCR", shortDescr)
                  );

            return contract;
        }
        #endregion

        #region Section VI: Complementary information
        /// <summary>
        /// Section VI: Complementary information
        /// </summary>
        /// <returns>The COMPLEMENTARY_INFO XElement</returns>
        private XElement ComplementaryInformation()
        {
            return TedHelpers.Element("COMPLEMENTARY_INFO",
                TedHelpers.Element("DATE_DISPATCH_NOTICE", DateTime.Now.ToString("yyyy-MM-dd")),
                TedHelpers.ElementWithAttribute("ORIGINAL_ENOTICES", "PUBLICATION", "NO"),
                TedHelpers.ElementWithAttribute("ESENDER_LOGIN", "PUBLICATION", "NO", _eSenderLogin),
                TedHelpers.ElementWithAttribute("CUSTOMER_LOGIN", "PUBLICATION", "NO", _eSenderLogin),
                TedHelpers.ElementWithAttribute("NO_DOC_EXT", "PUBLICATION", "NO", _parent?.NoticeNumber),
                TedHelpers.ElementWithAttribute("DATE_DISPATCH_ORIGINAL", "PUBLICATION", "NO", TedNoticeFactory.PublishToTed || _parent.TedPublishRequestSentDate != DateTime.MinValue ? _parent.TedPublishRequestSentDate.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"))
               );
        }
        #endregion
    }
}
