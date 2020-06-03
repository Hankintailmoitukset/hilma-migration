using System.Xml.Linq;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Configuration;
using Hilma.Domain.Integrations.ConfigurationFactories;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// TED F23 Social Concession Prior Information Factory - Generates TED integration XML
    /// </summary>
    public class F23Factory
    {
        private readonly NoticeContract _notice;
        private readonly string _eSenderLogin;
        private readonly string _tedContactEmail;
        private readonly string _tedSenderOrganisation;
        private readonly NoticeContractConfiguration _configuration;
        private readonly SectionHelper _helper;
        private readonly ITranslationProvider _translationProvider;

        /// <summary>
        /// F23 Social concession prior information factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedContactEmail"></param>
        /// <param name="translationProvider"></param>
        /// <param name="tedSenderOrganisation"></param>
        public F23Factory(NoticeContract notice, string eSenderLogin, string tedSenderOrganisation, string tedContactEmail, ITranslationProvider translationProvider)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedContactEmail = tedContactEmail;
            _tedSenderOrganisation = tedSenderOrganisation;
            _configuration = NoticeConfigurationFactory.CreateConfiguration(notice);
            _translationProvider = translationProvider;
            _helper = new SectionHelper(_notice, _configuration, eSenderLogin, _translationProvider);

        }

        /// <summary>
        /// Creates the XML document that is sent to TED.
        /// </summary>
        /// <returns></returns>
        public XDocument CreateForm()
        {
            return TedHelpers.CreateTedDocument( 
                    TedHelpers.LoginPart(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail),
                    NoticeBody());
        }

        private XElement NoticeBody()
        {
            return TedHelpers.Element("FORM_SECTION",
                TedHelpers.Element("F23_2014", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "F23"),
                    TedHelpers.LegalBasis(_notice),
                    TedHelpers.ElementWithAttribute("NOTICE", "TYPE", _notice.Type == NoticeType.SocialConcessionPriorInformation ? "PRI" : "CONCESSION_AWARD_CONTRACT"),
                    _helper.ContractingBody(_notice.Project, _notice.ContactPerson, _notice.CommunicationInformation, _notice.Type ),
                    _helper.ObjectContract(),
                    _helper.ConditionsInformation(),
                    _helper.Procedure(),
                    _notice.Type == NoticeType.SocialConcessionAward ? _helper.ContractAward() : null,
                    _helper.ComplementaryInformation()));
        }

    }
}
