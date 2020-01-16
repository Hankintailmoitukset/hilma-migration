using System.Xml.Linq;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Integrations.Configuration;
using Hilma.Domain.Integrations.ConfigurationFactories;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.Defence
{
    /// <summary>
    /// TED F17 Contract notice for contracts in the field of defence and security - Generates TED integration XML
    /// </summary>
    public class F17Factory
    {
        private readonly NoticeContract _notice;
        private readonly string _eSenderLogin;
        private readonly string _tedContactEmail;
        private string _tedESenderOrganisation;
        private readonly NoticeContractConfiguration _configuration;
        private readonly SectionHelper _helper;

        /// <summary>
        /// F17 Defence Contract Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedESenderOrganisation">Organisation that sends notices to eSender api</param>
        /// <param name="tedContactEmail">Contact email for technical</param>
        /// <param name="translationProvider"></param>
        public F17Factory(NoticeContract notice, string eSenderLogin, string tedESenderOrganisation,
            string tedContactEmail, ITranslationProvider translationProvider)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedContactEmail = tedContactEmail;
            _tedESenderOrganisation = tedESenderOrganisation;
            _configuration = NoticeConfigurationFactory.CreateConfiguration(notice);
            _helper = new SectionHelper(_notice, _configuration, translationProvider);
        }

        /// <summary>
        /// Creates the XML document that is sent to TED.
        /// </summary>
        /// <returns></returns>
        public XDocument CreateForm()
        {
            return TedHelpers.CreateTedDocument(
                       TedHelpers.LoginPart(_notice, _eSenderLogin, _tedESenderOrganisation, _tedContactEmail),
                       NoticeBody());
        }

        /// <summary>
        /// #  XSD name : F02_2014
        /// #  RELEASE : "R2.0.9.S03"
        /// #  Intermediate release number 007-20181030
        /// #  Last update : 30/10/2018
        /// #  Form : Contract notice
        /// </summary>
        private XElement NoticeBody()
        {
            return TedHelpers.Element("FORM_SECTION",
                    TedHelpers.Element("CONTRACT_DEFENCE", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "17"), new XAttribute("VERSION", "R2.0.8.S04"),
                        TedHelpers.ElementWithAttribute("FD_CONTRACT_DEFENCE", "CTYPE", _notice.Project.ContractType.ToTEDFormat(),
                            _helper.ContractingBody(_notice.Project, _notice.ContactPerson, _notice.CommunicationInformation, _notice.Type),
                            _helper.ObjectContract(),
                            _helper.ConditionsInformation(_notice.ConditionsInformationDefence),
                            _helper.Procedure(_notice.ProcedureInformation),
                            _helper.ComplementaryInformation()
                            )
                        )
                    );
        }
    }
}
