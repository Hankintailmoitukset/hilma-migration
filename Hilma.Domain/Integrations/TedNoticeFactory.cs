using System;
using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Defence;
using Hilma.Domain.Integrations.General;
using Newtonsoft.Json.Linq;

namespace Hilma.Domain.Integrations
{
    /// <summary>
    /// TED Notice Factory - Generates TED integration XML
    /// </summary>
    public class TedNoticeFactory
    {
        private readonly NoticeContract _notice;
        private readonly NoticeContract _parent;
        private readonly string _tedContactEmail;

        private string _eSenderLogin;
        /// <summary>
        /// Translations
        /// </summary>
        public static JObject Translations;

        /// <summary>
        /// Needed for corrigendum notices.
        /// </summary>
        public static string NoticeLanguage;

        private string _tedSenderOrganisation;

        /// <summary>
        /// If publishin to ted - has some affect on how message is formed.
        /// </summary>
        public static bool PublishToTed;

        /// <summary>
        /// Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="tedSenderOrganisationName">Sender organisation name of TED eSender</param>
        /// <param name="tedContactEmail">The TED contact email</param>
        /// <param name="eSenderLogin">eSenderLogin</param>
        /// <param name="translations">Remote translations (Loco)</param>
        /// <param name="publishToTed">If publishing to ted</param>
        public TedNoticeFactory(NoticeContract notice, NoticeContract parent, string tedSenderOrganisationName,
            string tedContactEmail, string eSenderLogin, string translations, bool publishToTed = true)
        {
            _notice = notice;
            _parent = parent;
            _tedContactEmail = tedContactEmail;
            _tedSenderOrganisation = tedSenderOrganisationName;
            _eSenderLogin = eSenderLogin;
            Translations = string.IsNullOrEmpty(translations) ? new JObject() : JObject.Parse(translations);
            NoticeLanguage = notice.Language;
            PublishToTed = publishToTed;
        }

        /// <summary>
        /// Creates the XML document 
        /// </summary>
        /// <returns></returns>
        public XDocument CreateDocument()
        {
            // Corrigendum, if parent exists and has TED ID
            if (IsCorrigendum())
            {
                var f14Factory = new F14Factory(_notice, _parent, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                return f14Factory.Form14();
            }

            switch (_notice.Type)
            {
                case NoticeType.PriorInformation:
                case NoticeType.PriorInformationReduceTimeLimits:
                    var f01Factory = new F01Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f01Factory.CreateForm();
                case NoticeType.Contract:
                    var f02Factory = new F02Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f02Factory.CreateForm();
                case NoticeType.Undefined:
                    break;
                case NoticeType.ContractAward:
                    var f03Factory = new F03Factory(_notice, _parent, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f03Factory.CreateForm();
                case NoticeType.PeriodicIndicativeUtilities:
                    var f04Factory = new F04Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f04Factory.CreateForm();
                case NoticeType.ContractUtilities:
                    var f05Factory = new F05Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f05Factory.CreateForm();
                case NoticeType.ContractAwardUtilities:
                    var f06Factory = new F06Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f06Factory.CreateForm();
                case NoticeType.QualificationSystemUtilities:
                    break;
                case NoticeType.BuyerProfile:
                    break;
                case NoticeType.DefenceSimplifiedContract:
                    break;
                case NoticeType.DefenceConcession:
                    break;
                case NoticeType.DefenceContractConcessionnaire:
                    break;
                case NoticeType.DesignContest:
                    var f12Factory = new F12Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f12Factory.CreateForm();
                case NoticeType.DesignContestResults:
                    break;
                case NoticeType.ExAnte:
                    var f15factory = new F15Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f15factory.CreateForm();
                case NoticeType.DefencePriorInformation:
                    var f16Factory = new F16Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f16Factory.CreateForm();
                case NoticeType.DefenceContract:
                    var f17Factory = new F17Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f17Factory.CreateForm();
                case NoticeType.DefenceContractAward:
                    return new F18Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail).CreateForm();
                case NoticeType.DefenceContractSub:
                    break;
                case NoticeType.Modification:
                    var f20Factory = new F20Factory(_notice, _parent, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f20Factory.CreateForm();
                case NoticeType.SocialPriorInformation:
                case NoticeType.SocialContract:
                case NoticeType.SocialContractAward:
                    var f21Factory = new F21Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f21Factory.CreateForm();
                case NoticeType.SocialUtilities:
                case NoticeType.SocialUtilitiesPriorInformation:
                    var f22Factory = new F22Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f22Factory.CreateForm();
                case NoticeType.SocialConcessions:
                    break;
                case NoticeType.Concession:
                    var f24Factory = new F24Factory(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail);
                    return f24Factory.CreateForm();
                case NoticeType.ConcessionAward:
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Notice type: {_notice.Type} is not supported");
            }

            return null;
        }

        // Corrigendum, if parent has been published
        private bool IsCorrigendum()
        {
            return _notice.IsCorrigendum && _parent != null && _parent.State == PublishState.Published;
        }
    }
}
