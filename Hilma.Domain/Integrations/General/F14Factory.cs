using System;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
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

        /// <summary>
        /// F02 Corrigendum Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedSenderOrganisation"></param>
        /// <param name="tedContactEmail"></param>
        public F14Factory(NoticeContract notice, NoticeContract parent, string eSenderLogin,
            string tedSenderOrganisation, string tedContactEmail)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedSenderOrganisation = tedSenderOrganisation;
            _tedContactEmail = tedContactEmail;
            _parent = parent;
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
                    TedHelpers.LoginPart(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail ),
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
                        NoticeChangesFactory.Changes(_notice, _parent),
                        TedHelpers.PElement("INFO_ADD", _notice.CorrigendumAdditionalInformation))));
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
            if (_notice.Type == Enums.NoticeType.DesignContest || _notice.Type == Enums.NoticeType.DesignContestResults)
            {
                shortDescr = _notice.ObjectDescriptions.FirstOrDefault().DescrProcurement;
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
