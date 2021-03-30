using System.Xml.Linq;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Configuration;
using Hilma.Domain.Integrations.ConfigurationFactories;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// TED F15 Ex Ante Notice Factory - Generates TED integration XML
    /// </summary>
    public class F15Factory
    {
        private readonly NoticeContract _notice;
        private readonly string _eSenderLogin;
        private readonly string _tedContactEmail;
        private readonly string _tedESenderOrganisation;
        private readonly NoticeContractConfiguration _configuration;
        private readonly SectionHelper _helper;
        private readonly AnnexHelper _annexHelper;
        private readonly ITranslationProvider _translationProvider;

        /// <summary>
        /// F15 Contract Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedESenderOrganisation">Organisation that sends notices to eSender api</param>
        /// <param name="tedContactEmail">Contact email for technical</param>
        /// <param name="translationProvider"></param>
        public F15Factory(NoticeContract notice, string eSenderLogin, string tedESenderOrganisation,
            string tedContactEmail, ITranslationProvider translationProvider)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedContactEmail = tedContactEmail;
            _tedESenderOrganisation = tedESenderOrganisation;
            _configuration = NoticeConfigurationFactory.CreateConfiguration(notice);
            _translationProvider = translationProvider;
            _helper = new SectionHelper(_notice, _configuration, eSenderLogin, _translationProvider);
            _annexHelper = new AnnexHelper(_notice, _configuration.Annexes);
        }

        /// <summary>
        /// Creates the XML document that is sent to TED.
        /// </summary>
        /// <returns></returns>
        public XDocument CreateForm() =>
            new XDocument(
                new XDeclaration("1.0", "utf-8", null), TedHelpers.Element("TED_ESENDERS",
                    new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.n2016), TedHelpers.n2016),
                    new XAttribute("VERSION", "R2.0.9.S04"),
                    new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.xs), TedHelpers.xs),
                    TedHelpers.LoginPart(_notice, _eSenderLogin, _tedESenderOrganisation, _tedContactEmail),
                    NoticeBody()));

        /// <summary>
        /// F15_2014
        /// </summary>
        private XElement NoticeBody() =>
            TedHelpers.Element(
                "FORM_SECTION", TedHelpers.Element(
                    "F15_2014", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "F15"),
                    TedHelpers.LegalBasis(_notice),
                    _helper.ContractingBody(_notice.Project,
                        _notice.ContactPerson,
                        _notice.CommunicationInformation,
                        _notice.Type,
                        true),
                    ObjectContract(),
                    Procedure(),
                    _helper.ContractAward(),
                    _helper.ComplementaryInformation()
                )
            );

        private XElement Procedure() =>
            TedHelpers.Element("PROCEDURE",
                TedHelpers.Element(_helper.DirectiveSelector(), _annexHelper.SelectAnnexD()),
                _notice.ProcedureInformation.FrameworkAgreement.IncludesFrameworkAgreement
                    ? TedHelpers.Element("FRAMEWORK")
                    : null,
                _notice.ProcedureInformation.ProcurementGovernedByGPA
                    ? TedHelpers.Element("CONTRACT_COVERED_GPA")
                    : TedHelpers.Element("NO_CONTRACT_COVERED_GPA"),
                string.IsNullOrEmpty(_notice.PreviousNoticeOjsNumber)
                    ? null
                    : TedHelpers.Element("NOTICE_NUMBER_OJ", _notice.PreviousNoticeOjsNumber)
            );




        private XElement ObjectContract()
        {
            var configuration = _configuration.ProcurementObject;
            var projectConfig = _configuration.Project;
            var procuremenObject = _notice.ProcurementObject;

            var contract = TedHelpers.Element("OBJECT_CONTRACT",
                projectConfig.Title
                    ? TedHelpers.PElement("TITLE", _notice.Project.Title)
                    : null,
                projectConfig.ReferenceNumber
                    ? TedHelpers.Element("REFERENCE_NUMBER", _notice.Project.ReferenceNumber)
                    : null,
                configuration.MainCpvCode.Code
                    ? TedHelpers.CpvCodeElement("CPV_MAIN", new [] { procuremenObject.MainCpvCode })
                    : null,
                projectConfig.ContractType
                    ? TedHelpers.ElementWithAttribute("TYPE_CONTRACT", "CTYPE", _notice.Project.ContractType.ToTEDFormat(_notice.Project.ProcurementCategory))
                    : null,
                configuration.ShortDescription
                    ? TedHelpers.PElement("SHORT_DESCR", procuremenObject.ShortDescription)
                    : null,
                procuremenObject.TotalValue != null ? procuremenObject.TotalValue.Type == ContractValueType.Exact
                    ? TedHelpers.Element("VAL_TOTAL",
                        _notice.Project.ProcurementCategory == ProcurementCategory.Utility
                            ? new XAttribute("PUBLICATION", (!procuremenObject.TotalValue?.DisagreeToBePublished ?? false).ToYesNo("EN").ToUpperInvariant())
                            : null,
                        new XAttribute("CURRENCY", procuremenObject.TotalValue.Currency),
                        procuremenObject.TotalValue.Value)
                    : TedHelpers.Element("VAL_RANGE_TOTAL",
                        _notice.Project.ProcurementCategory == ProcurementCategory.Utility
                            ? new XAttribute("PUBLICATION", (!procuremenObject.TotalValue.DisagreeToBePublished ?? false).ToYesNo("EN").ToUpperInvariant())
                            : null,
                        new XAttribute("CURRENCY", procuremenObject.TotalValue.Currency),
                        TedHelpers.Element("LOW", procuremenObject.TotalValue.MinValue),
                        TedHelpers.Element("HIGH", procuremenObject.TotalValue.MaxValue)) : null ,
                _helper.LotDivision(_notice.LotsInfo),
                _helper.ObjectDescriptions(_notice.ObjectDescriptions)
            );

            return contract;
        }
    }
}
