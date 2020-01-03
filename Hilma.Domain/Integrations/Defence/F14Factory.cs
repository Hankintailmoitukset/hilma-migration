using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.Defence
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

        private static readonly XNamespace xmlns = "http://publications.europa.eu/resource/schema/ted/R2.0.8/reception";

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
        public XDocument CreateForm()
        {
            return new XDocument(
                new XDeclaration("1.0", "utf-8", null), TedHelpers.Element("TED_ESENDERS",
                    new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.n2016), TedHelpers.n2016),
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
                TedHelpers.Element("ADDITIONAL_INFORMATION_CORRIGENDUM", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "14"), new XAttribute("VERSION", "R2.0.8.S04"),
                    TedHelpers.Element("FD_ADDITIONAL_INFORMATION_CORRIGENDUM",
                        ContractingBody(_notice.Project.Organisation, _notice.ContactPerson, _notice.CommunicationInformation),
                        ObjectContract(),
                        ProcedureInformation(),
                        ComplementaryInformation())));
        }

        private List<XElement> ChangesToXml(List<Change> changes)
        {
            var result = new List<XElement>();

            foreach (var change in changes)
            {
                var (newElement, newType) = NewValue(change);
                var (oldElement, oldType) = OldValue(change);
                // Can be CPV_MAIN, CPV_ADDITIONAL, NUTS, DATE_TIME or TEXT
                result.Add(
                    TedHelpers.Element("CORR",
                        newType != ChangeType.Undefined && oldType == ChangeType.Undefined ?
                        TedHelpers.ElementWithAttribute("ADD", "OBJECT", newType.ToString().ToUpper(),
                            TedHelpers.Element("WHERE", $"{change.Section}: {change.Label}"),
                            TedHelpers.Element("NEW_VALUE", newElement)) : null,
                        newType == ChangeType.Undefined && oldType != ChangeType.Undefined ?
                        TedHelpers.ElementWithAttribute("DELETE", "OBJECT", oldType.ToString().ToUpper(),
                            TedHelpers.Element("WHERE", $"{change.Section}: {change.Label}"),
                            TedHelpers.Element("OLD_VALUE", oldElement)) : null,
                        newType != ChangeType.Undefined && oldType != ChangeType.Undefined ?
                        TedHelpers.ElementWithAttribute("REPLACE", "OBJECT", newType.ToString().ToUpper(),
                            TedHelpers.Element("WHERE", $"{change.Section}: {change.Label}"),
                            TedHelpers.Element("OLD_VALUE", oldElement),
                            TedHelpers.Element("NEW_VALUE", newElement)) : null));
            }

            return result;
        }

        private static Tuple<List<XElement>, ChangeType> OldValue(Change change)
        {
            List<XElement> oldValue;
            ChangeType type;
            if (change.OldText != null && change.OldText.Length > 0 && !string.IsNullOrEmpty(change.OldText[0]))
            {
                type = ChangeType.Text;
                oldValue = new List<XElement> { TedHelpers.PElement("TEXT", change.OldText) };
            }
            else if (change.OldNutsCodes != null && change.OldNutsCodes.Length > 0 && !string.IsNullOrEmpty(change.OldNutsCodes[0]))
            {
                type = ChangeType.Nuts;
                oldValue = new List<XElement> { TedHelpers.PElement("NUTS", change.OldText) };
            }
            else if (change.OldDate != null && change.OldDate != DateTime.MinValue)
            {
                type = ChangeType.Date;
                oldValue = new List<XElement> { TedHelpers.DateTimeElement("DATE_TIME", change.OldDate) };
            }
            else if (change.OldMainCpvCode != null && !string.IsNullOrEmpty(change.OldMainCpvCode.Code))
            {
                type = ChangeType.Cpv;
                oldValue = new List<XElement> { TedHelpers.Element("CPV_MAIN",
                                TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", change.OldMainCpvCode.Code),
                                change.OldMainCpvCode.VocCodes?.Select(x => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", x.Code))) };
            }
            else if (change.OldAdditionalCpvCodes != null && change.OldAdditionalCpvCodes.Count > 0)
            {
                type = ChangeType.Cpv;
                oldValue = change.OldAdditionalCpvCodes.Select(x =>
                             TedHelpers.Element("CPV_ADDITIONAL",
                                 TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", x.Code),
                                 x.VocCodes?.Select(y => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", y.Code)))
                        ).ToList();
            }
            else
            {
                type = ChangeType.Undefined;
                oldValue = null;
            }

            return new Tuple<List<XElement>, ChangeType>(oldValue, type);
        }

        private static Tuple<List<XElement>, ChangeType> NewValue(Change change)
        {
            List<XElement> newValue;
            ChangeType type;
            if (change.NewText != null && change.NewText.Length > 0 && !string.IsNullOrEmpty(change.NewText[0]))
            {
                type = ChangeType.Text;
                newValue = new List<XElement> { TedHelpers.PElement("TEXT", change.NewText) };
            }
            else if (change.NewNutsCodes != null && change.NewNutsCodes.Length > 0 && !string.IsNullOrEmpty(change.NewNutsCodes[0]))
            {
                type = ChangeType.Nuts;
                newValue = new List<XElement> { TedHelpers.PElement("NUTS", change.NewText) };
            }
            else if (change.NewDate != null && change.NewDate != DateTime.MinValue)
            {
                type = ChangeType.Date;
                newValue = new List<XElement> { TedHelpers.DateTimeElement("DATE_TIME", change.NewDate) };
            }
            else if (change.NewMainCpvCode != null && !string.IsNullOrEmpty(change.NewMainCpvCode.Code))
            {
                type = ChangeType.Cpv;
                newValue = new List<XElement> { TedHelpers.Element("CPV_MAIN",
                                TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", change.NewMainCpvCode.Code),
                                change.NewMainCpvCode.VocCodes?.Select(x => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", x.Code))) };
            }
            else if (change.NewAdditionalCpvCodes != null && change.NewAdditionalCpvCodes.Count > 0)
            {
                type = ChangeType.Cpv;
                newValue = change.NewAdditionalCpvCodes.Select(x =>
                             TedHelpers.Element("CPV_ADDITIONAL",
                                 TedHelpers.ElementWithAttribute("CPV_CODE", "CODE", x.Code),
                                 x.VocCodes?.Select(y => TedHelpers.ElementWithAttribute("CPV_SUPPLEMENTARY_CODE", "CODE", y.Code)))
                        ).ToList();
            }
            else
            {
                type = ChangeType.Undefined;
                newValue = null;
            }

            return new Tuple<List<XElement>, ChangeType>(newValue, type);
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
            return TedHelpers.Element("AUTH_ENTITY_ICAR",
                TedHelpers.Element("NAME_ADDRESSES_CONTACT_ICAR",
                        TedHelpers.INC_01("CA_CE_CONCESSIONAIRE_PROFILE", organisation, contactPerson),
                        TedHelpers.Element("INTERNET_ADDRESSES_ICAR",
                            TedHelpers.Element("URL_GENERAL", _notice.Project.Organisation.Information.MainUrl),
                            TedHelpers.Element("URL_INFORMATION", communicationInformation.ElectronicAccess))),
                // DIRECTIVE_2004_17 = CA
                // DIRECTIVE_2004_18 = CE
                TedHelpers.ElementWithAttribute("TYPE_OF_PURCHASING_BODY", "VALUE", "DIRECTIVE_2004_17"));
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
            var contract = TedHelpers.Element("OBJECT_ICAR",
                TedHelpers.Element("DESCRIPTION_ICAR",
                    TedHelpers.PElement("TITLE_CONTRACT", _notice.Project.Title),
                    TedHelpers.PElement("SHORT_DESCRIPTION_CONTRACT", _notice.ProcurementObject.ShortDescription),
                    TedHelpers.Element("CPV",
                        TedHelpers.CpvCodeElement("CPV_MAIN", new CpvCode[] { _notice.ProcurementObject.MainCpvCode }),
                        TedHelpers.CpvCodeElement("CPV_ADDITIONAL", _notice.ProcurementObject.Defence.AdditionalCpvCodes))));

            return contract;
        }
        #endregion

        #region Section IV: Procedure Information
        /// <summary>
        /// Section IV: Procedure Information
        /// </summary>
        /// <returns>The PROCEDURES_ICAR XElement</returns>
        private XElement ProcedureInformation()
        {
            var previousOjs = _notice.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber;
            if(_notice.Type == NoticeType.DefenceContractAward)
            {
                previousOjs = _notice.TenderingInformation.Defence.PreviousContractNoticeOjsNumber;
            }
            if(_notice.Type == NoticeType.ExAnte)
            {
                previousOjs = _notice.TenderingInformation.Defence.PreviousExAnteOjsNumber;
            }
            return TedHelpers.Element("PROCEDURES_ICAR",
                TedHelpers.Element("TYPE_OF_PROCEDURE_CORRIGENDUM",
                    TedHelpers.ElementWithAttribute("TYPE_OF_PROCEDURE_DETAIL_FOR_ICAR", "VALUE", ProcedureType())),
                TedHelpers.Element("ADMINISTRATIVE_INFORMATION",
                    TedHelpers.PElement("FILE_REFERENCE_NUMBER", _notice.Project.ReferenceNumber),
                    TedHelpers.Element("SIMAP_ESENDER_NOTICE_REFERENCE",
                        TedHelpers.ElementWithAttribute("SIMAP_ESENDER", "VALUE", "OJS_ESENDER"),
                        TedHelpers.ElementWithAttribute("LOGIN", "CLASS", "B",
                            TedHelpers.Element("ESENDER_LOGIN", _eSenderLogin),
                            TedHelpers.Element("CUSTOMER_LOGIN", _eSenderLogin)),
                        TedHelpers.Element("NO_DOC_EXT", _notice.NoticeNumber)),
                    TedHelpers.Element("NOTICE_PUBLICATION",
                        TedHelpers.Element("NOTICE_NUMBER_OJ", previousOjs.Number),
                        TedHelpers.DateElement("DATE_OJ", previousOjs.Date)),
                    TedHelpers.DateElement("ORIGINAL_DISPATCH_DATE", TedNoticeFactory.PublishToTed || _parent.TedPublishRequestSentDate != DateTime.MinValue ? _parent.TedPublishRequestSentDate : DateTime.Now))
               );
        }

        private string ProcedureType()
        {
            switch (_notice.ProcedureInformation.ProcedureType)
            {
                case Entities.ProcedureType.ProctypeOpen:
                    return "OPEN";
                case Entities.ProcedureType.ProctypeRestricted when _notice.ProcedureInformation.AcceleratedProcedure:
                    return "ACCELERATED_RESTRICTED";
                case Entities.ProcedureType.ProctypeRestricted:
                    return "RESTRICTED";
                case Entities.ProcedureType.ProctypeNegotiation when _notice.ProcedureInformation.AcceleratedProcedure:
                    return "ACCELERATED_NEGOTIATED";
                case Entities.ProcedureType.ProctypeNegotiation:
                    return "NEGOTIATED";
                case Entities.ProcedureType.ProctypeCompDialogue:
                    return "COMPETITIVE_DIALOGUE";
                case Entities.ProcedureType.AwardWoPriorPubD1:
                case Entities.ProcedureType.AwardWoPriorPubD1Other:
                    return "AWARD_WITHOUT_PRIOR_PUBLICATION";
                default: return "";
            }
        }
        #endregion


        #region Section VI: COMPLEMENTARY INFORMATION
        /// <summary>
        /// Section IV: COMPLEMENTARY INFORMATION
        /// </summary>
        /// <returns>The COMPLEMENTARY_ICAR XElement</returns>
        private XElement ComplementaryInformation()
        {
            return TedHelpers.Element("COMPLEMENTARY_ICAR",
                TedHelpers.Element("NOTICE_INVOLVES_ICAR",
                    TedHelpers.Element("CORRECTION_ADDITIONAL_INFO",
                        TedHelpers.Element("CORRECTION"),
                        TedHelpers.Element("INFORMATION_CORRECTED_ADDED",
                            TedHelpers.Element("MODIFICATION_ORIGINAL_PUBLICATION_TED",
                                TedHelpers.Element("MODIFICATION_ORIGINAL")),
                            TedHelpers.Element("ORIGINAL_NOTICE_CORRESPONDING_TENDER",
                                TedHelpers.Element("ORIGINAL_NOTICE",
                                    ChangesToXml(new DefenceChangesFactory(_notice, _parent, _translationProvider).Changes()),
                                    TedHelpers.Element("ADDR_CORR",
                                    TedHelpers.Element("WHERE", $"I.1"),
                                    TedHelpers.Element("MODIFIED_ADDRESS",
                                        TedHelpers.INC_01("CA_CE_CONCESSIONAIRE_PROFILE", _notice.Project.Organisation, _notice.ContactPerson),
                                        TedHelpers.Element("MODIFIED_INTERNET",
                                            TedHelpers.Element("URL_INFORMATION", _notice.CommunicationInformation.ElectronicAccess),
                                            TedHelpers.Element("URL_PARTICIPATE", _notice.CommunicationInformation.ElectronicAddressToSendTenders))))))))),
                TedHelpers.PElement("OTHER_ADDITIONAL_INFO", _notice.CorrigendumAdditionalInformation),
                TedHelpers.DateElement("NOTICE_DISPATCH_DATE", DateTime.Now));

        }
        #endregion

        private enum ChangeType
        {
            Undefined,
            Date,
            Text,
            Cpv,
            Nuts
        }
    }
}
