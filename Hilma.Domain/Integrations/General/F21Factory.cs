using System;
using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Integrations.Configuration;
using Hilma.Domain.Integrations.ConfigurationFactories;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// Generates TED integration xml for social contracts (F21)
    /// </summary>
    public class F21Factory
    {
        private readonly NoticeContract _notice;
        private readonly string _eSenderLogin;
        private readonly string _tedContactEmail;
        private readonly string _tedSenderOrganisation;
        private readonly NoticeContractConfiguration _configuration;
        private readonly SectionHelper _helper;

        /// <summary>
        /// F01 Contract Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedContactEmail"></param>
        /// <param name="tedSenderOrganisation"></param>
        public F21Factory(NoticeContract notice, string eSenderLogin, string tedSenderOrganisation, string tedContactEmail)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedContactEmail = tedContactEmail;
            _tedSenderOrganisation = tedSenderOrganisation;
            _configuration = NoticeConfigurationFactory.CreateConfiguration(notice);
            _helper = new SectionHelper(_notice, _configuration, eSenderLogin);
        }

        public XDocument CreateForm() =>
            TedHelpers.CreateTedDocument(
                TedHelpers.LoginPart(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail),
                NoticeBody());

        private XElement NoticeBody() =>
            TedHelpers.Element("FORM_SECTION",
                TedHelpers.Element("F21_2014", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "F21"),
                    TedHelpers.LegalBasis(_notice),
                    NoticeType(),
                    _helper.ContractingBody(_notice.Project, _notice.ContactPerson, _notice.CommunicationInformation, _notice.Type),
                    _helper.ObjectContract(),
                    _helper.ConditionsInformation(),
                    _helper.Procedure(),
                    _notice.Type == Enums.NoticeType.SocialContractAward ? _helper.ContractAward() : null,
                    _helper.ComplementaryInformation()));

        private XElement NoticeType()
        {
            string noticeType;
            switch (_notice.Type)
            {
                case Enums.NoticeType.SocialContract:
                    noticeType = "CONTRACT";
                    break;
                case Enums.NoticeType.SocialPriorInformation:
                    noticeType = "PRI_ONLY";
                    break;
                case Enums.NoticeType.SocialContractAward:
                    noticeType = "AWARD_CONTRACT";
                    break;
                default:
                    throw new NotImplementedException();
            }

            return TedHelpers.ElementWithAttribute("NOTICE", "TYPE", noticeType);
        }
    }
}
