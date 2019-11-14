using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// TED F05 Contract Notice Utilities Factory - Generates TED integration XML
    /// </summary>
    public class F05Factory
    {
        private readonly NoticeContract _notice;
        private readonly string _eSenderLogin;
        private readonly string _tedContactEmail;
        private string _tedESenderOrganisation;

        /// <summary>
        /// F05 Contract Notice factory constructor.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="eSenderLogin">The TED esender login</param>
        /// <param name="tedESenderOrganisation">Organisation that sends notices to eSender api</param>
        /// <param name="tedContactEmail">Contact email for technical</param>
        public F05Factory(NoticeContract notice, string eSenderLogin, string tedESenderOrganisation,
            string tedContactEmail)
        {
            _notice = notice;
            _eSenderLogin = eSenderLogin;
            _tedContactEmail = tedContactEmail;
            _tedESenderOrganisation = tedESenderOrganisation;
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
                    new XAttribute("VERSION", "R2.0.9.S03"),
                    new XAttribute(XNamespace.Xmlns + nameof(TedHelpers.xs), TedHelpers.xs),
                    TedHelpers.LoginPart(_notice, _eSenderLogin,_tedESenderOrganisation, _tedContactEmail ),
                    NoticeBody()));
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
            return TedHelpers.Element(
                       "FORM_SECTION", TedHelpers.Element(
                           "F05_2014", new XAttribute("LG", _notice.Language), new XAttribute("CATEGORY", "ORIGINAL"), new XAttribute("FORM", "F05"),
                                TedHelpers.LegalBasis(_notice),
                                ContractingBody(_notice.Project.Organisation, _notice.ContactPerson, _notice.CommunicationInformation),
                                ObjectContract(),
                                ConditionsInformation(),
                                Procedure(),
                                ComplementaryInformation()
                            )
                       );
        }

        #region Section I: Contracting authority
        /// <summary>
        /// Section I: Contracting authority
        /// </summary>
        /// <param name="organisation">Organisation contract</param>
        /// <param name="contactPerson">Contact person</param>
        /// <param name="communicationInformation">I.3 Communication</param>
        /// <returns>CONTRACTING_BODY XElement</returns>
        private XElement ContractingBody(OrganisationContract organisation, ContactPerson contactPerson, CommunicationInformation communicationInformation)
        {
            return TedHelpers.Element("CONTRACTING_BODY",
                    TedHelpers.ADDRS1("ADDRESS_CONTRACTING_BODY", organisation, contactPerson),

                    JointProcurement(_notice),

                    _notice.CommunicationInformation.ProcurementDocumentsAvailable == ProcurementDocumentAvailability.AddressObtainDocs ? TedHelpers.Element("DOCUMENT_FULL") : TedHelpers.Element("DOCUMENT_RESTRICTED"),
                    TedHelpers.Element("URL_DOCUMENT", _notice.CommunicationInformation.ProcurementDocumentsUrl),

                    AddressFurtherInfo(communicationInformation),

                    _notice.CommunicationInformation.SendTendersOption == TenderSendOptions.AddressSendTenders ? TedHelpers.Element("URL_PARTICIPATION", _notice.CommunicationInformation.ElectronicAddressToSendTenders) : null,
                    _notice.CommunicationInformation.SendTendersOption == TenderSendOptions.AddressFollowing ? TedHelpers.ADDRS1("ADDRESS_PARTICIPATION", _notice.CommunicationInformation.AddressToSendTenders)
                        : TedHelpers.Element("ADDRESS_PARTICIPATION_IDEM"),

                    _notice.CommunicationInformation.ElectronicCommunicationRequiresSpecialTools ? TedHelpers.Element("URL_TOOL", _notice.CommunicationInformation.ElectronicCommunicationInfoUrl) : null,

                    organisation.MainActivityUtilities == MainActivityUtilities.OtherActivity ? TedHelpers.Element("CE_ACTIVITY_OTHER", organisation.OtherMainActivity)
                        : TedHelpers.ElementWithAttribute("CE_ACTIVITY", "VALUE", organisation.MainActivityUtilities.ToTEDFormat())
                );
        }

        #region Contracting authority helpers
        private IEnumerable<XElement> JointProcurement(NoticeContract notice)
        {
            var elements = new List<XElement>();
            if (notice.Project.JointProcurement)
            {
                notice.Project.CoPurchasers.ForEach(x => elements.Add(TedHelpers.ADDRS1("ADDRESS_CONTRACTING_BODY_ADDITIONAL", new OrganisationContract
                    {
                        Information = x
                    },
                    new ContactPerson { Email = x.Email, Phone = x.TelephoneNumber, Name = x.ContactPerson })));
                elements.Add(notice.Project.JointProcurement ? TedHelpers.Element("JOINT_PROCUREMENT_INVOLVED") : null);
                elements.Add(TedHelpers.PElement("PROCUREMENT_LAW", notice.Project.ProcurementLaw));
            }
            elements.Add(notice.Project.CentralPurchasing ? TedHelpers.Element("CENTRAL_PURCHASING") : null);
            return elements;
        }

        private XElement AddressFurtherInfo(CommunicationInformation communicationInformation)
        {
            if (communicationInformation.AdditionalInformation == AdditionalInformationAvailability.AddressToAbove)
            {
                return TedHelpers.Element("ADDRESS_FURTHER_INFO_IDEM");
            }

            return TedHelpers.ADDRS1("ADDRESS_FURTHER_INFO", communicationInformation.AdditionalInformationAddress);
        }

        #endregion
        #endregion

        #region Section II: Object
        /// <summary>
        /// Section II: Object
        /// </summary>
        /// <returns>OBJECT_CONTRACT XElement</returns>
        private XElement ObjectContract()
        {
            var contract = TedHelpers.Element("OBJECT_CONTRACT",
                    TedHelpers.PElement("TITLE", _notice.Project.Title),
                    TedHelpers.Element("REFERENCE_NUMBER", _notice.Project.ReferenceNumber),
                    TedHelpers.CpvCodeElement("CPV_MAIN", new CpvCode[] { _notice.ProcurementObject.MainCpvCode }),
                    TedHelpers.ElementWithAttribute("TYPE_CONTRACT", "CTYPE", _notice.Project.ContractType.ToTEDFormat()),
                    TedHelpers.PElement("SHORT_DESCR", _notice.ProcurementObject.ShortDescription),
                    _notice.ProcurementObject.EstimatedValue.Value > 0
                        ? TedHelpers.Element("VAL_ESTIMATED_TOTAL", attribute: new XAttribute("CURRENCY", _notice.ProcurementObject.EstimatedValue.Currency), value: _notice.ProcurementObject.EstimatedValue.Value)
                        : null,
                    LotDivision(),
                    ObjectDescriptions()
                );

            contract.Add();

            return contract;
        }

        #region Object helpers
        private XElement LotDivision()
        {
            if (!_notice.LotsInfo.DivisionLots || _notice.LotsInfo.QuantityOfLots == 0)
            {
                return TedHelpers.Element("NO_LOT_DIVISION");
            }

            return new XElement(TedHelpers.Xmlns + "LOT_DIVISION",
                LotTendersMayBeSubmittedFor(_notice.LotsInfo.LotsSubmittedFor, _notice.LotsInfo.LotsMaxAwardedQuantity),
                _notice.LotsInfo.LotsMaxAwarded ? TedHelpers.Element("LOT_MAX_ONE_TENDERER", _notice.LotsInfo.LotsSubmittedForQuantity, 1) : null,
                _notice.LotsInfo.LotCombinationPossible ? TedHelpers.PElement("LOT_COMBINING_CONTRACT_RIGHT", _notice.LotsInfo.LotCombinationPossibleDescription) : null);
        }

        private XElement ObjectDescription(int lotNumber, ObjectDescription objectDescription)
        {
            return TedHelpers.Element("OBJECT_DESCR", new XAttribute("ITEM", lotNumber),
                TedHelpers.PElement("TITLE", objectDescription.Title),
                TedHelpers.Element("LOT_NO", (_notice.LotsInfo.DivisionLots) ? (lotNumber).ToString() : null),
                TedHelpers.CpvCodeElement("CPV_ADDITIONAL", objectDescription.AdditionalCpvCodes),
                NutsCodes(objectDescription.NutsCodes),
                TedHelpers.PElement("MAIN_SITE", objectDescription.MainsiteplaceWorksDelivery),
                TedHelpers.PElement("SHORT_DESCR", objectDescription.DescrProcurement),
                TedHelpers.Element("AC", AwardCriteria(objectDescription)),
                objectDescription.EstimatedValue.Value > 0 ? TedHelpers.ElementWithAttribute("VAL_OBJECT", "CURRENCY", objectDescription.EstimatedValue.Currency, objectDescription.EstimatedValue.Value.GetValueOrDefault()) : null,
                Duration(objectDescription.TimeFrame),
                Candidates(objectDescription.CandidateNumberRestrictions),
                OptionsAndVariants(objectDescription.OptionsAndVariants),
                objectDescription.TendersMustBePresentedAsElectronicCatalogs ? TedHelpers.Element("ECATALOGUE_REQUIRED") : null,
                objectDescription.EuFunds.ProcurementRelatedToEuProgram ? TedHelpers.PElement("EU_PROGR_RELATED", objectDescription.EuFunds.ProjectIdentification) : TedHelpers.Element("NO_EU_PROGR_RELATED"),
                TedHelpers.PElement("INFO_ADD", objectDescription.AdditionalInformation)
              );
        }

        private XElement LotTendersMayBeSubmittedFor(LotsSubmittedFor lotsInfoLotsSubmittedFor, int lotsInfoLotsMaxAwardedQuantity)
        {
            if (_notice.Type == NoticeType.PriorInformation)
            {
                return null;
            }

            switch (lotsInfoLotsSubmittedFor)
            {
                case LotsSubmittedFor.LotsAll:
                    return TedHelpers.Element("LOT_ALL");
                case LotsSubmittedFor.LotsMax:
                    return TedHelpers.Element("LOT_MAX_NUMBER", lotsInfoLotsMaxAwardedQuantity);
                case LotsSubmittedFor.LotOneOnly:
                    return TedHelpers.Element("LOT_ONE_ONLY");
                default:
                    return null;
            }
        }

        private IEnumerable<XElement> ObjectDescriptions()
        {
            for (int i = 0; i < _notice.ObjectDescriptions.Length; i++)
            {
                yield return ObjectDescription(i + 1, _notice.ObjectDescriptions[i]);
            }
        }

        private IEnumerable<XElement> OptionsAndVariants(OptionsAndVariants optionsAndVariants)
        {
            yield return TedHelpers.Element(optionsAndVariants.VariantsWillBeAccepted ? "ACCEPTED_VARIANTS" : "NO_ACCEPTED_VARIANTS");
            yield return TedHelpers.Element(optionsAndVariants.Options ? "OPTIONS" : "NO_OPTIONS");
            if (optionsAndVariants.Options)
            {
                yield return TedHelpers.PElement("OPTIONS_DESCR", optionsAndVariants.OptionsDescription);
            }
        }

        private IEnumerable<XElement> Candidates(CandidateNumberRestrictions candidates)
        {
            if (_notice.ProcedureInformation.ProcedureType == ProcedureType.ProctypeOpen)
            {
                yield break;
            }

            switch (candidates.Selected)
            {
                case EnvisagedParticipantsOptions.EnvisagedNumber:
                    yield return TedHelpers.Element("NB_ENVISAGED_CANDIDATE", candidates.EnvisagedNumber);
                    break;
                case EnvisagedParticipantsOptions.Range:
                    yield return TedHelpers.Element("NB_MIN_LIMIT_CANDIDATE", candidates.EnvisagedMinimumNumber);
                    yield return TedHelpers.Element("NB_MAX_LIMIT_CANDIDATE", candidates.EnvisagedMaximumNumber);
                    break;
                default:
                    yield break;
            }

            yield return TedHelpers.PElement("CRITERIA_CANDIDATE", candidates.ObjectiveCriteriaForChoosing);
        }

        private static IEnumerable<XElement> Duration(TimeFrame timeFrame)
        {
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
                    throw new ArgumentOutOfRangeException(nameof(timeFrame.Type), "Undefined is not allowed value");
            }

            yield return TedHelpers.Element(timeFrame.CanBeRenewed ? "RENEWAL" : "NO_RENEWAL");
            if (timeFrame.CanBeRenewed)
            {
                yield return TedHelpers.PElement("RENEWAL_DESCR", timeFrame.RenewalDescription);
            }
        }

        private IEnumerable<XElement> AwardCriteria(ObjectDescription objectDescription)
        {
            var awardCriteria = objectDescription.AwardCriteria;
            var awardCriteriaTypes = awardCriteria.CriterionTypes;
            if (awardCriteriaTypes.HasFlag(AwardCriterionType.DescriptiveCriteria))
            {
                yield return TedHelpers.Element("AC_PROCUREMENT_DOC");
            }
            else
            {
                if (awardCriteriaTypes.HasFlag(AwardCriterionType.QualityCriterion))
                {
                    foreach (var qualityCriterion in awardCriteria.QualityCriteria)
                    {
                        yield return Criterion("AC_QUALITY", qualityCriterion);
                    }
                }
                if (awardCriteriaTypes.HasFlag(AwardCriterionType.CostCriterion))
                {
                    foreach (var criterion in awardCriteria.CostCriteria)
                    {
                        yield return Criterion("AC_COST", criterion);
                    }
                    yield break;
                }
                if (awardCriteriaTypes.HasFlag(AwardCriterionType.PriceCriterion) && awardCriteriaTypes.HasFlag(AwardCriterionType.QualityCriterion))
                {
                    yield return TedHelpers.Element("AC_PRICE",
                        TedHelpers.Element("AC_WEIGHTING", awardCriteria.PriceCriterion.Weighting));
                }
                else if (awardCriteriaTypes.HasFlag(AwardCriterionType.PriceCriterion))
                {
                    yield return TedHelpers.Element("AC_PRICE");
                }
            }
        }

        private IEnumerable<XElement> NutsCodes(string[] nutsCodes)
        {
            return nutsCodes.Select(
                n => new XElement(TedHelpers.n2016 + "NUTS", new XAttribute("CODE", n)));
        }

        private static XElement Criterion(string name, AwardCriterionDefinition qualityCriterion)
        {
            return TedHelpers.Element(name,
                TedHelpers.Element("AC_CRITERION", qualityCriterion.Criterion),
                TedHelpers.Element("AC_WEIGHTING", qualityCriterion.Weighting)
            );
        }
        #endregion
        #endregion

        #region Section III: Legal, economic, financial and technical information
        /// <summary>
        /// Section III: Legal, economic, financial and technical information
        /// </summary>
        /// <returns>Conditions element</returns>
        private XElement ConditionsInformation()
        {
            var conditions = TedHelpers.Element("LEFTI");
            conditions.Add(TedHelpers.PElement("SUITABILITY", _notice.ConditionsInformation.ProfessionalSuitabilityRequirements));
            if (!_notice.ConditionsInformation.EconomicCriteriaToParticipate)
            {
                conditions.Add(TedHelpers.PElement("ECONOMIC_FINANCIAL_INFO", _notice.ConditionsInformation.EconomicCriteriaDescription));
                conditions.Add(TedHelpers.PElement("ECONOMIC_FINANCIAL_MIN_LEVEL", _notice.ConditionsInformation.EconomicRequiredStandards));
            }
            else
            {
                conditions.Add(TedHelpers.Element("ECONOMIC_CRITERIA_DOC"));
            }

            if (!_notice.ConditionsInformation.TechnicalCriteriaToParticipate)
            {
                conditions.Add(TedHelpers.PElement("TECHNICAL_PROFESSIONAL_INFO", _notice.ConditionsInformation.TechnicalCriteriaDescription));
                conditions.Add(TedHelpers.PElement("TECHNICAL_PROFESSIONAL_MIN_LEVEL", _notice.ConditionsInformation.TechnicalRequiredStandards));
            }
            else
            {
                conditions.Add(TedHelpers.Element("TECHNICAL_CRITERIA_DOC"));
            }

            conditions.Add(TedHelpers.PElement("RULES_CRITERIA", _notice.ConditionsInformation.RulesForParticipation));

            if (_notice.ConditionsInformation.RestrictedToShelteredWorkshop)
            {
                conditions.Add(TedHelpers.Element("RESTRICTED_SHELTERED_WORKSHOP"));
            }

            if (_notice.ConditionsInformation.RestrictedToShelteredProgram)
            {
                conditions.Add(TedHelpers.Element("RESTRICTED_SHELTERED_PROGRAM"));
            }

            conditions.Add(TedHelpers.PElement("DEPOSIT_GUARANTEE_REQUIRED", _notice.ConditionsInformation.DepositsRequired));
            conditions.Add(TedHelpers.PElement("MAIN_FINANCING_CONDITION", _notice.ConditionsInformation.FinancingConditions));
            conditions.Add(TedHelpers.PElement("LEGAL_FORM", _notice.ConditionsInformation.LegalFormTaken));

            if ((_notice.Project.ContractType == ContractType.Services || _notice.Project.ContractType == ContractType.SocialServices)
                && _notice.ConditionsInformation.ExecutionOfServiceIsReservedForProfession)
            {
                conditions.Add(TedHelpers.ElementWithAttribute("PARTICULAR_PROFESSION", "CTYPE", "SERVICES"));
                conditions.Add(TedHelpers.PElement("REFERENCE_TO_LAW", _notice.ConditionsInformation.ReferenceToRelevantLawRegulationOrProvision));
            }


            return conditions;
        }
        #endregion

        #region Section IV: Procedure
        /// <summary>
        /// Section IV: Procedure
        /// </summary>
        /// <returns>Procedure element</returns>
        private XElement Procedure()
        {
            var procedure = TedHelpers.Element("PROCEDURE");

            var type = _notice.ProcedureInformation.ProcedureType;

            switch (type)
            {
                case ProcedureType.ProctypeOpen:
                    procedure.Add(TedHelpers.Element("PT_OPEN"));
                    break;
                case ProcedureType.ProctypeRestricted:
                    procedure.Add(TedHelpers.Element("PT_RESTRICTED"));
                    break;
                case ProcedureType.ProctypeNegotWCall:
                    procedure.Add(TedHelpers.Element("PT_NEGOTIATED_WITH_PRIOR_CALL"));
                    break;
                case ProcedureType.ProctypeCompDialogue:
                    procedure.Add(TedHelpers.Element("PT_COMPETITIVE_DIALOGUE"));
                    break;
                case ProcedureType.ProctypeInnovation:
                    procedure.Add(TedHelpers.Element("PT_INNOVATION_PARTNERSHIP"));
                    break;
            }

            var participants = _notice.ProcedureInformation.FrameworkAgreement.EnvisagedNumberOfParticipants;

            procedure.Add(TedHelpers.Element("FRAMEWORK",
                participants != null && participants > 1 ? TedHelpers.Element("SEVERAL_OPERATORS") : TedHelpers.Element("SINGLE_OPERATOR"),
                TedHelpers.Element("NB_PARTICIPANTS", participants),
                TedHelpers.PElement("JUSTIFICATION", _notice.ProcedureInformation.FrameworkAgreement.JustificationForDurationOverFourYears)));

            procedure.Add(_notice.ProcedureInformation.FrameworkAgreement.IncludesDynamicPurchasingSystem ? TedHelpers.Element("DPS") : null);

            procedure.Add(_notice.ProcedureInformation.FrameworkAgreement.IncludesDynamicPurchasingSystem && _notice.ProcedureInformation.FrameworkAgreement.DynamicPurchasingSystemInvolvesAdditionalPurchasers ? TedHelpers.Element("DPS_ADDITIONAL_PURCHASERS") : null);

            if(type == ProcedureType.ProctypeCompNegotiation || type == ProcedureType.ProctypeCompDialogue || type == ProcedureType.ProctypeInnovation)
            {
                procedure.Add(_notice.ProcedureInformation.ReductionRecourseToReduceNumberOfSolutions ? TedHelpers.Element("REDUCTION_RECOURSE") : null);
            }

            procedure.Add(_notice.ProcedureInformation.ElectronicAuctionWillBeUsed ? TedHelpers.Element("EAUCTION_USED") : null);
            if (_notice.ProcedureInformation.ElectronicAuctionWillBeUsed)
            {
                procedure.Add(TedHelpers.PElement("INFO_ADD_EAUCTION", _notice.ProcedureInformation.AdditionalInformationAboutElectronicAuction));
            }

            procedure.Add(_notice.ProcedureInformation.ProcurementGovernedByGPA ? TedHelpers.Element("CONTRACT_COVERED_GPA") : TedHelpers.Element("NO_CONTRACT_COVERED_GPA"));

            procedure.Add(TedHelpers.Element("NOTICE_NUMBER_OJ", _notice.PreviousNoticeOjsNumber));

            procedure.Add(TedHelpers.DateElement("DATE_RECEIPT_TENDERS", _notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime));
            procedure.Add(TedHelpers.TimeElement("TIME_RECEIPT_TENDERS", _notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime));

            if(type != ProcedureType.ProctypeOpen)
            {
                procedure.Add(TedHelpers.DateElement("DATE_DISPATCH_INVITATIONS", _notice.TenderingInformation.EstimatedDateOfInvitations));
            }

            procedure.Add(TedHelpers.Element("LANGUAGES", _notice.TenderingInformation.Languages.Select(x => TedHelpers.ElementWithAttribute("LANGUAGE", "VALUE", x))));

            switch( _notice.TenderingInformation.TendersMustBeValidOption )
            {
                case   TendersMustBeValidOption.Months:
                    if(_notice.TenderingInformation.TendersMustBeValidForMonths != null)
                    {
                        procedure.Add(TedHelpers.ElementWithAttribute("DURATION_TENDER_VALID", "TYPE", "MONTH", _notice.TenderingInformation.TendersMustBeValidForMonths));
                    }
                break;
                case TendersMustBeValidOption.Date:
                        procedure.Add(TedHelpers.DateElement("DATE_TENDER_VALID", _notice.TenderingInformation.TendersMustBeValidUntil));
                break;
                default:
                break;
            }

            if (type == ProcedureType.ProctypeOpen)
            {
                procedure.Add(TedHelpers.Element("OPENING_CONDITION",
                    TedHelpers.DateElement("DATE_OPENING_TENDERS", _notice.TenderingInformation.TenderOpeningConditions.OpeningDateAndTime),
                    TedHelpers.TimeElement("TIME_OPENING_TENDERS", _notice.TenderingInformation.TenderOpeningConditions.OpeningDateAndTime),
                    TedHelpers.PElement("PLACE", _notice.TenderingInformation.TenderOpeningConditions.Place),
                    TedHelpers.PElement("INFO_ADD", _notice.TenderingInformation.TenderOpeningConditions.InformationAboutAuthorisedPersons)));
            }

            return procedure;
        }
        #endregion

        #region Section VI: Complementary information
        /// <summary>
        /// Section VI: Complementary information
        /// </summary>
        /// <returns>The complementary section</returns>
        private XElement ComplementaryInformation()
        {
            var complementaryInfo = TedHelpers.Element("COMPLEMENTARY_INFO");

            if (_notice.ComplementaryInformation.IsRecurringProcurement)
            {
                complementaryInfo.Add(TedHelpers.Element("RECURRENT_PROCUREMENT"));
                complementaryInfo.Add(TedHelpers.PElement("ESTIMATED_TIMING", _notice.ComplementaryInformation.EstimatedTimingForFurtherNoticePublish));
            }
            else
            {
                complementaryInfo.Add(TedHelpers.Element("NO_RECURRENT_PROCUREMENT"));
            }

            if (_notice.ComplementaryInformation.ElectronicOrderingUsed)
            {
                complementaryInfo.Add(TedHelpers.Element("EORDERING"));
            }

            if (_notice.ComplementaryInformation.ElectronicInvoicingUsed)
            {
                complementaryInfo.Add(TedHelpers.Element("EINVOICING"));
            }

            if (_notice.ComplementaryInformation.ElectronicPaymentUsed)
            {
                complementaryInfo.Add(TedHelpers.Element("EPAYMENT"));
            }

            complementaryInfo.Add(TedHelpers.PElement("INFO_ADD", _notice.ComplementaryInformation.AdditionalInformation));

            complementaryInfo.Add(TedHelpers.ADDRS6("ADDRESS_REVIEW_BODY", _notice.ProceduresForReview.ReviewBody));
            complementaryInfo.Add(TedHelpers.PElement("REVIEW_PROCEDURE", _notice.ProceduresForReview?.ReviewProcedure));

            complementaryInfo.Add(TedHelpers.DateElement("DATE_DISPATCH_NOTICE", DateTime.Now));

            return complementaryInfo;
        }
        #endregion
    }
}
