using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Integrations.Configuration;
using Hilma.Domain.Integrations.ConfigurationFactories;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// TED F06 Contract Award Utilities Factory - Generates TED integration XML
    /// </summary>
    public class F06Factory
    {
        private readonly NoticeContract _notice;
        private readonly string _eSenderLogin;
        private readonly string _tedContactEmail;
        private readonly string _tedSenderOrganisation;
        private readonly NoticeContractConfiguration _configuration;
        private readonly SectionHelper _helper;

        /// <summary>
        /// F06 Contract Award Utilities factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedContactEmail"></param>
        /// <param name="tedSenderOrganisation"></param>
        public F06Factory(NoticeContract notice, string eSenderLogin, string tedSenderOrganisation, string tedContactEmail)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedContactEmail = tedContactEmail;
            _tedSenderOrganisation = tedSenderOrganisation;
            _configuration = NoticeConfigurationFactory.CreateConfiguration(notice);
            _helper = new SectionHelper(_notice, _configuration, eSenderLogin);

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

        // <summary>
        //  XSD name : F06_2014
        //  RELEASE : "R2.0.9.S03"                                                      
        //  Intermediate release number 007-20181030                               
        //  Last update : 30/10/2018
        //  Form : Contract award notice - utilities
        // </summary>
        private XElement NoticeBody()
        {
            return TedHelpers.Element("FORM_SECTION",
                TedHelpers.Element("F06_2014", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "F06"),
                    TedHelpers.LegalBasis(_notice),
                    _helper.ContractingBody(_notice.Project, _notice.ContactPerson, _notice.CommunicationInformation, _notice.Type),
                    _helper.ObjectContract(),
                    _helper.Procedure(),
                    _helper.ContractAward(),
                    _helper.ComplementaryInformation()));
        }

    }
}
