using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Exceptions;
using Hilma.Domain.Extensions;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// TED F20 Modification Notice Factory - Generates TED integration XML
    /// </summary>
    public class F20Factory
    {
        private readonly NoticeContract _notice;
        private readonly NoticeContract _parent;
        private readonly string _eSenderLogin;
        private readonly string _tedContactEmail;
        private readonly string _tedSenderOrganisation;

        /// <summary>
        /// F03 Contract Award Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedContactEmail"></param>
        /// <param name="tedSenderOrganisation"></param>
        public F20Factory(NoticeContract notice, NoticeContract parent, string eSenderLogin, string tedSenderOrganisation, string tedContactEmail)
        {
            _notice = notice;
            _parent = parent;
            _eSenderLogin = eSenderLogin;
            _tedContactEmail = tedContactEmail;
            _tedSenderOrganisation = tedSenderOrganisation;
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
                    new XAttribute("VERSION", "R2.0.9.S04"),
                    new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.xs), TedHelpers.xs),
                    TedHelpers.LoginPart(_notice, _eSenderLogin, _tedSenderOrganisation, _tedContactEmail),
                    NoticeBody()));
        }

        /// <summary>
        /// #  XSD name : F01_2014
        /// #  RELEASE : "R2.0.9.S04"
        /// #  Intermediate release number 007-20181030
        /// #  Last update : 30/10/2018
        /// #  Form : Prior information notice
        /// </summary>
        private XElement NoticeBody()
        {
            return TedHelpers.Element("FORM_SECTION",
                TedHelpers.Element("F20_2014", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "F20"),
                    TedHelpers.LegalBasis(_notice, _parent),
                    ContractingBody(_notice.Project.Organisation, _notice.ContactPerson, _notice.CommunicationInformation),
                    ObjectContract(),
                    Procedure(),
                    ContractAward(),
                    ComplementaryInformation(),
                    Modifications()));
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
                    TedHelpers.ADDRS1("ADDRESS_CONTRACTING_BODY", organisation, contactPerson)
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
            var contract = TedHelpers.Element("OBJECT_CONTRACT",
                TedHelpers.PElement("TITLE", _notice.Project?.Title),
                TedHelpers.Element("REFERENCE_NUMBER", _notice.Project?.ReferenceNumber),
                TedHelpers.CpvCodeElement("CPV_MAIN", new CpvCode[] { _notice.ProcurementObject?.MainCpvCode }),
                TedHelpers.ElementWithAttribute("TYPE_CONTRACT", "CTYPE", _notice.Project?.ContractType.ToTEDFormat())
                  );

            contract.Add(ObjectDescriptions());

            return contract;
        }

        /// <summary>
        /// Section II.2: Description
        /// </summary>
        /// <returns></returns>
        private XElement ObjectDescriptions()
        {
            var lot = _notice.ObjectDescriptions?.FirstOrDefault();
            if (lot == null)
                return null;

            return TedHelpers.Element("OBJECT_DESCR",
                TedHelpers.PElement("TITLE", lot.Title),
                TedHelpers.Element("LOT_NO", lot.LotNumber),
                TedHelpers.CpvCodeElement("CPV_ADDITIONAL", lot.AdditionalCpvCodes),
                lot.NutsCodes.Select(n => new XElement(TedHelpers.n2016 + "NUTS", new XAttribute("CODE", n))),
                TedHelpers.PElement("MAIN_SITE", lot.MainsiteplaceWorksDelivery),
                TedHelpers.PElement("SHORT_DESCR", lot.DescrProcurement),
                Duration(lot.TimeFrame, _notice.ProcedureInformation?.FrameworkAgreement?.JustificationForDurationOverFourYears, _notice.ProcedureInformation?.FrameworkAgreement?.JustificationForDurationOverEightYears),
                lot.EuFunds?.ProcurementRelatedToEuProgram ?? false ? TedHelpers.PElement("EU_PROGR_RELATED", lot.EuFunds?.ProjectIdentification) : TedHelpers.Element("NO_EU_PROGR_RELATED")
                );
        }

        private IEnumerable<XElement> Duration(TimeFrame timeFrame,string[] justificationIsOverFourYears, string[] justificationIsOverEightYears)
        {
            if (timeFrame == null)
                yield break;

            switch (timeFrame.Type)
            {
                case TimeFrameType.Days:
                    yield return TedHelpers.ElementWithAttribute("DURATION", "TYPE", "DAY", timeFrame.Days);
                    break;
                case TimeFrameType.Months:
                    yield return TedHelpers.ElementWithAttribute("DURATION", "TYPE", "MONTH", timeFrame.Months);
                    break;
                case TimeFrameType.BeginAndEndDate:
                    yield return TedHelpers.DateElement("DATE_START", timeFrame.BeginDate);
                    yield return TedHelpers.DateElement("DATE_END", timeFrame.EndDate);
                    break;
                default:
                    throw new HilmaMalformedRequestException("Undefined is not allowed value for time frame type");
            }

            if (justificationIsOverFourYears != null && justificationIsOverFourYears.HasAnyContent() && timeFrame.IsOverFourYears)
            {
                yield return TedHelpers.PElement("JUSTIFICATION", justificationIsOverFourYears);
            }
            if(justificationIsOverEightYears != null && justificationIsOverEightYears.HasAnyContent() && timeFrame.IsOverEightYears)
            {
                yield return TedHelpers.PElement("JUSTIFICATION", justificationIsOverEightYears);
            }
        }
        #endregion

        #region Section IV: Procedure
        /// <summary>
        /// Section IV: Procedure
        /// </summary>
        /// <returns>Procedure element</returns>
        private XElement Procedure()
        {
            return TedHelpers.Element("PROCEDURE", TedHelpers.Element("NOTICE_NUMBER_OJ", _notice.PreviousNoticeOjsNumber));
        }
        #endregion

        #region Section V: Award of contract
        private XElement ContractAward()
        {
            var lot = _notice.ObjectDescriptions.FirstOrDefault();
            if (lot == null)
            {
                return null;
            }

            return TedHelpers.Element("AWARD_CONTRACT",
                    TedHelpers.Element("CONTRACT_NO", lot.AwardContract?.AwardedContract?.ContractNumber),
                    _notice.LotsInfo.DivisionLots ? TedHelpers.Element("LOT_NO", lot.LotNumber.ToString()) : null,
                    TedHelpers.PElement("TITLE", lot.AwardContract?.AwardedContract?.ContractTitle),
                    TedHelpers.Element("AWARDED_CONTRACT",
                        TedHelpers.DateElement("DATE_CONCLUSION_CONTRACT", lot.AwardContract?.AwardedContract?.ConclusionDate <= DateTime.Now ? lot.AwardContract?.AwardedContract?.ConclusionDate : DateTime.Now),
                        TedHelpers.Element("CONTRACTORS",
                            lot.AwardContract?.AwardedContract?.Contractors?.Count > 1 ? TedHelpers.Element("AWARDED_TO_GROUP") : TedHelpers.Element("NO_AWARDED_TO_GROUP"),
                            lot.AwardContract?.AwardedContract?.Contractors?.Select((contractor, a) =>
                            TedHelpers.Element("CONTRACTOR",
                                TedHelpers.ADDRS5(contractor),
                                contractor.IsSmallMediumEnterprise ? TedHelpers.Element("SME") : TedHelpers.Element("NO_SME")))
                        ),
                            TedHelpers.Element("VALUES",
                                lot.AwardContract?.AwardedContract?.FinalTotalValue.Value > 0 ?
                                    TedHelpers.ElementWithAttribute("VAL_TOTAL", "CURRENCY", lot.AwardContract.AwardedContract.FinalTotalValue.Currency, lot.AwardContract.AwardedContract.FinalTotalValue.Value)
                                : null)));
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
                TedHelpers.PElement("INFO_ADD", _notice.ComplementaryInformation?.AdditionalInformation),
                TedHelpers.ADDRS6("ADDRESS_REVIEW_BODY", _notice.ProceduresForReview?.ReviewBody),
                TedHelpers.PElement("REVIEW_PROCEDURE", _notice.ProceduresForReview?.ReviewProcedure),
                TedHelpers.DateElement("DATE_DISPATCH_NOTICE", DateTime.Now));
        }
        #endregion

        #region Section VII: Modifications to the contract/concession
        private XElement Modifications()
        {
            var modifications = _notice.Modifications;
            if (modifications == null)
            {
                return null;
            }

            try
            {
                return TedHelpers.Element("MODIFICATIONS_CONTRACT",
                    TedHelpers.Element("DESCRIPTION_PROCUREMENT",
                        TedHelpers.CpvCodeElement("CPV_MAIN", new CpvCode[] {modifications.MainCpvCode}),
                        TedHelpers.CpvCodeElement("CPV_ADDITIONAL", modifications.AdditionalCpvCodes),
                        modifications.NutsCodes.Select(n =>
                            new XElement(TedHelpers.n2016 + "NUTS", new XAttribute("CODE", n))),
                        TedHelpers.PElement("MAIN_SITE", modifications.MainsiteplaceWorksDelivery),
                        TedHelpers.PElement("SHORT_DESCR", modifications.DescrProcurement),
                        Duration(modifications.TimeFrame, modifications.JustificationForDurationOverFourYears,
                            modifications.JustificationForDurationOverEightYears),
                        TedHelpers.Element("VALUES", TedHelpers.ElementWithAttribute("VAL_TOTAL", "CURRENCY",
                            modifications.TotalValue?.Currency, modifications.TotalValue?.Value)),
                        TedHelpers.Element("CONTRACTORS",
                            modifications.Contractors?.Count > 1
                                ? TedHelpers.Element("AWARDED_TO_GROUP")
                                : TedHelpers.Element("NO_AWARDED_TO_GROUP"),
                            modifications.Contractors?.Select((contractor, a) =>
                                TedHelpers.Element("CONTRACTOR",
                                    TedHelpers.ADDRS5(contractor),
                                    contractor.IsSmallMediumEnterprise
                                        ? TedHelpers.Element("SME")
                                        : TedHelpers.Element("NO_SME"))))
                    ),
                    TedHelpers.Element("INFO_MODIFICATIONS",
                        TedHelpers.PElement("SHORT_DESCR", modifications.Description),
                        modifications.Reason == ModificationReason.ModNeedForAdditional
                            ? TedHelpers.PElement("ADDITIONAL_NEED", modifications.ReasonDescriptionEconomic)
                            : TedHelpers.PElement("UNFORESEEN_CIRCUMSTANCE",
                                modifications.ReasonDescriptionCircumstances),
                        TedHelpers.Element("VALUES",
                            TedHelpers.ElementWithAttribute("VAL_TOTAL_BEFORE", "CURRENCY",
                                modifications.IncreaseBeforeModifications?.Currency,
                                modifications.IncreaseBeforeModifications?.Value),
                            TedHelpers.ElementWithAttribute("VAL_TOTAL_AFTER", "CURRENCY",
                                modifications.IncreaseAfterModifications?.Currency,
                                modifications.IncreaseAfterModifications?.Value)))
                );
            }
            catch(HilmaMalformedRequestException e)
            {
                throw new HilmaMalformedRequestException( $"Invalid content in notice.Modifications: {e.Message}");
            }
        }
        #endregion
    }
}
