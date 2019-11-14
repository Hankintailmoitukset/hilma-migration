using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Configuration;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// Helper for creating TED xml documents.
    /// </summary>
    public class SectionHelper : TedHelpers
    {
        private readonly NoticeContract _notice;
        private readonly NoticeContractConfiguration _configuration;
        private readonly string _esenderLogin;
        private readonly AnnexHelper _annexHelper;

        /// <summary>
        /// Public constructor that sets the notice and configuration.
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="configuration"></param>
        /// <param name="tedEsenderLogin"></param>
        public SectionHelper(NoticeContract notice, NoticeContractConfiguration configuration, string tedEsenderLogin)
        {
            _notice = notice;
            _configuration = configuration;
            _esenderLogin = tedEsenderLogin;
            _annexHelper = new AnnexHelper(_notice, _configuration.Annexes);
        }


        /// <summary>
        /// Section I: Contracting authority
        /// </summary>
        /// <param name="project">Procurement project</param>
        /// <param name="contactPerson">The contact person</param>
        /// <param name="communicationInformation">I.3 Communication</param>
        /// <param name="type">Form type</param>
        /// <param name="hideJoint">Hide JointProcurement section</param>
        /// <returns>CONTRACTING_BODY XElement</returns>
        public XElement ContractingBody(
            ProcurementProjectContract project,
            ContactPerson contactPerson,
            CommunicationInformation communicationInformation,
            NoticeType type,
            bool hideJoint = false)
        {
            var organisation = project.Organisation;
            var isUtilitiesNotice = type.IsUtilities();

            return Element("CONTRACTING_BODY",
                ADDRS1("ADDRESS_CONTRACTING_BODY", organisation, contactPerson),
                hideJoint ? null : JointProcurement(project),
                CommunicationInformation(communicationInformation, type),
                isUtilitiesNotice
                    ? null
                    : (organisation.ContractingAuthorityType == ContractingAuthorityType.OtherType ||
                    organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeChurch ||
                    organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeFarmer
                        ? OtherContractingAuthorityType(organisation)
                        : ElementWithAttribute("CA_TYPE", "VALUE", organisation.ContractingAuthorityType.ToTEDFormat())),
                isUtilitiesNotice
                    ? (organisation.MainActivityUtilities == MainActivityUtilities.OtherActivity
                        ? Element("CE_ACTIVITY_OTHER", organisation.OtherMainActivity)
                        : ElementWithAttribute("CE_ACTIVITY", "VALUE", organisation.MainActivityUtilities.ToTEDFormat()))
                    : (organisation.MainActivity == MainActivity.OtherActivity ||
                    organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeFarmer
                        ? OtherMainActivity(organisation)
                        : ElementWithAttribute("CA_ACTIVITY", "VALUE", organisation.MainActivity.ToTEDFormat()))
            );
        }

        /// <summary>
        /// Xml helper for communications information section.
        /// </summary>
        /// <param name="communicationInformation"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<XElement> CommunicationInformation(CommunicationInformation communicationInformation, NoticeType type)
        {
            var config = _configuration.CommunicationInformation;

            if (type.IsContractAward())
            {
                yield break;
            }

            if (config.ProcurementDocumentsAvailable)
            {
                yield return communicationInformation.ProcurementDocumentsAvailable == ProcurementDocumentAvailability.AddressObtainDocs ? Element("DOCUMENT_FULL") : communicationInformation.ProcurementDocumentsAvailable == ProcurementDocumentAvailability.DocsRestricted ? Element("DOCUMENT_RESTRICTED"): null;
            }

            if (config.ProcurementDocumentsUrl)
            {
                yield return Element("URL_DOCUMENT", communicationInformation.ProcurementDocumentsUrl);
            }

            if (config.AdditionalInformation)
            {
                if (communicationInformation.AdditionalInformation == AdditionalInformationAvailability.AddressToAbove)
                {
                    yield return Element("ADDRESS_FURTHER_INFO_IDEM");
                }
                else
                {
                    yield return ADDRS1("ADDRESS_FURTHER_INFO", communicationInformation.AdditionalInformationAddress);
                }
            }

            if (config.SendTendersOption)
            {
                yield return communicationInformation.SendTendersOption == TenderSendOptions.AddressSendTenders ? Element("URL_PARTICIPATION", communicationInformation.ElectronicAddressToSendTenders) : null;
                yield return communicationInformation.SendTendersOption == TenderSendOptions.AddressFollowing ? ADDRS1("ADDRESS_PARTICIPATION", communicationInformation.AddressToSendTenders) : communicationInformation.SendTendersOption == TenderSendOptions.AddressOrganisation ? Element("ADDRESS_PARTICIPATION_IDEM"):null;
            }

            if (config.ElectronicCommunicationRequiresSpecialTools)
            {
                yield return communicationInformation.ElectronicCommunicationRequiresSpecialTools ? Element("URL_TOOL", communicationInformation.ElectronicCommunicationInfoUrl) : null;
            }
        }

        public XElement OtherContractingAuthorityType(OrganisationContract organisation)
        {
            string elementValue = null;
            if (organisation.ContractingAuthorityType == ContractingAuthorityType.OtherType)
            {
                elementValue = organisation.OtherContractingAuthorityType;
            } else if (organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeChurch)
            {
                elementValue = "maintype_church".GetTranslation();
            } else if (organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeFarmer)
            {
                elementValue = "maintype_farmer".GetTranslation();
            }

            return Element("CA_TYPE_OTHER", elementValue);
        }

        public XElement OtherMainActivity(OrganisationContract organisation)
        {
            string elementValue = null;

            if (organisation.MainActivity == MainActivity.OtherActivity)
            {
                elementValue = organisation.OtherMainActivity;
            } else if (organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeFarmer)
            {
                elementValue = "mainactivity_agriculture".GetTranslation();
            }

            return Element("CA_ACTIVITY_OTHER", elementValue);
        }

        private IEnumerable<XElement> JointProcurement(ProcurementProjectContract project)
        {
            var elements = new List<XElement>();
            if (project.JointProcurement)
            {
                project.CoPurchasers.ForEach(x => elements.Add(ADDRS1("ADDRESS_CONTRACTING_BODY_ADDITIONAL", new OrganisationContract
                {
                    Information = x
                },
                    new ContactPerson { Email = x.Email, Phone = x.TelephoneNumber, Name = x.ContactPerson })));
                elements.Add(project.JointProcurement ? Element("JOINT_PROCUREMENT_INVOLVED") : null);
                elements.Add(PElement("PROCUREMENT_LAW", project.ProcurementLaw));
            }
            elements.Add(project.CentralPurchasing ? Element("CENTRAL_PURCHASING") : null);
            return elements;
        }

        /// <summary>
        /// Xml writer for lots division section.
        /// </summary>
        /// <param name="lotsInfo"></param>
        /// <returns></returns>
        public XElement LotDivision(LotsInfo lotsInfo)
        {
            var configuration = _configuration.LotsInfo;

            // NO_LOT_DIVISION element should not exist on SocialUtilitiesPriorInformation in case of notice not being lotted
            if ((_notice.Type == NoticeType.SocialUtilitiesPriorInformation ||
                 _notice.Type == NoticeType.SocialUtilities ||
                 _notice.Type == NoticeType.DesignContest)
                && !lotsInfo.DivisionLots)
            {
                return null;
            }

            if (!configuration.DivisionLots || !lotsInfo.DivisionLots || lotsInfo.QuantityOfLots == 0)
            {
                return Element("NO_LOT_DIVISION");
            }

            return new XElement(Xmlns + "LOT_DIVISION",
                configuration.LotsSubmittedFor ? LotTendersMayBeSubmittedFor(lotsInfo.LotsSubmittedFor, lotsInfo.LotsMaxAwardedQuantity) : null,
                configuration.LotsMaxAwarded && lotsInfo.LotsMaxAwarded ? Element("LOT_MAX_ONE_TENDERER", lotsInfo.LotsSubmittedForQuantity, 1) : null,
                configuration.LotCombinationPossible && lotsInfo.LotCombinationPossible ? PElement("LOT_COMBINING_CONTRACT_RIGHT", lotsInfo.LotCombinationPossibleDescription) : null);

        }

        private XElement LotTendersMayBeSubmittedFor(LotsSubmittedFor lotsInfoLotsSubmittedFor, int lotsInfoLotsMaxAwardedQuantity)
        {
            switch (lotsInfoLotsSubmittedFor)
            {
                case LotsSubmittedFor.LotsAll:
                    return Element("LOT_ALL");
                case LotsSubmittedFor.LotsMax:
                    return Element("LOT_MAX_NUMBER", lotsInfoLotsMaxAwardedQuantity);
                case LotsSubmittedFor.LotOneOnly:
                    return Element("LOT_ONE_ONLY");
                default:
                    return null;
            }
        }

        #region Section II: Object
        /// <summary>
        /// Section II: Object
        /// </summary>
        /// <returns>OBJECT_CONTRACT XElement</returns>
        public XElement ObjectContract()
        {
            var configuration = _configuration.ProcurementObject;
            var projectConfig = _configuration.Project;
            var procurementObject = _notice.ProcurementObject;

            var showObjectNumber = (_notice.Type.IsPriorInformation() ||
                _notice.Type.IsUtilities() ||
                _notice.Type.IsSocial()) && _notice.Type != NoticeType.ContractAwardUtilities;

            var contract = Element("OBJECT_CONTRACT", showObjectNumber ? new XAttribute("ITEM", 1) : null,
                    projectConfig.Title ? PElement("TITLE", _notice.Project.Title) : null,
                    projectConfig.ReferenceNumber ? Element("REFERENCE_NUMBER", _notice.Project.ReferenceNumber) : null,
                    configuration.MainCpvCode.Code ? CpvCodeElement("CPV_MAIN", new [] { procurementObject.MainCpvCode }) : null,
                    projectConfig.ContractType ? ElementWithAttribute("TYPE_CONTRACT", "CTYPE", _notice.Project.ContractType.ToTEDFormat()) : null,
                    configuration.ShortDescription ? PElement("SHORT_DESCR", procurementObject.ShortDescription) : null,
                    configuration.TotalValue.Type && _notice.ObjectDescriptions.Any(x => x.AwardContract?.ContractAwarded == ContractAwarded.AwardedContract)
                        ? ValueTotal(procurementObject.TotalValue, true) : null,
                    configuration.EstimatedValue.Value && procurementObject.EstimatedValue.Value > 0 ?  Element("VAL_ESTIMATED_TOTAL", attribute: new XAttribute("CURRENCY", _notice.ProcurementObject.EstimatedValue.Currency), value: _notice.ProcurementObject.EstimatedValue.Value) : null,
                    LotDivision(_notice.LotsInfo),
                    ObjectDescriptions(_notice.ObjectDescriptions),
                    _configuration.TenderingInformation.EstimatedDateOfContractNoticePublication ? Element("DATE_PUBLICATION_NOTICE", _notice.TenderingInformation?.EstimatedDateOfContractNoticePublication?.ToString("yyyy-MM-dd")) : null
                );

            return contract;
        }

        private XElement ValueTotal(ValueRangeContract totalValue, bool addPublicationAttribute)
        {
            var publicationAttribute = _configuration.ProcurementObject.TotalValue.DisagreeToBePublished && addPublicationAttribute && totalValue.DisagreeToBePublished != null
                ? new XAttribute("PUBLICATION", (bool) totalValue.DisagreeToBePublished ? "NO" : "YES")
                : null;
            var currencyAttribute = new XAttribute("CURRENCY", totalValue.Currency);

            if (totalValue.Type == ContractValueType.Exact)
            {
                return Element("VAL_TOTAL", publicationAttribute, currencyAttribute, totalValue.Value);
            }
            else if (totalValue.Type == ContractValueType.Range)
            {
                return Element("VAL_RANGE_TOTAL", publicationAttribute, currencyAttribute, Element("LOW", totalValue.MinValue), Element("HIGH", totalValue.MaxValue));
            }
            return null;
        }

        #region Section II: Object
        private XElement ObjectDescription(int lotNumber, ObjectDescription objectDescription)
        {
            var config = _configuration.ObjectDescriptions;

            var awardCriteriaAttribute = config.DisagreeAwardCriteriaToBePublished
                ? _notice.Type == NoticeType.ExAnte // if ex ante notice...
                    ? _notice.Project.ProcurementCategory == ProcurementCategory.Utility
                        ? new XAttribute("PUBLICATION",
                            objectDescription.DisagreeAwardCriteriaToBePublished
                                ? "NO"
                                : "YES") // ...only for utility directive
                        : null
                    : new XAttribute("PUBLICATION",
                        objectDescription.DisagreeAwardCriteriaToBePublished ? "NO" : "YES") // others just obey config
                : null; 

            var hideItemNumber = _notice.Type == NoticeType.DesignContest;

            return Element("OBJECT_DESCR", !hideItemNumber ? new XAttribute("ITEM", lotNumber) : null,
                config.Title ? PElement("TITLE", objectDescription.Title) : null,
                config.LotNumber && _notice.LotsInfo.DivisionLots ? Element("LOT_NO", lotNumber) : null,
                config.AdditionalCpvCodes.Code ? CpvCodeElement("CPV_ADDITIONAL", objectDescription.AdditionalCpvCodes) : null,
                config.NutsCodes ? NutsCodes(objectDescription.NutsCodes) : null,
                config.MainsiteplaceWorksDelivery ? PElement("MAIN_SITE", objectDescription.MainsiteplaceWorksDelivery) : null,
                config.DescrProcurement ? PElement("SHORT_DESCR", objectDescription.DescrProcurement) : null,
                _notice.Type == NoticeType.ExAnte
                    ? Element(DirectiveSelector(),
                        Element("AC", awardCriteriaAttribute, AwardCriteriaExAnte(objectDescription)))
                    : config.AwardCriteria?.CriterionTypes ?? false ? Element("AC", awardCriteriaAttribute, AwardCriteria(objectDescription)) : null,
                config.EstimatedValue.Value && objectDescription.EstimatedValue.Value > 0 ? ElementWithAttribute("VAL_OBJECT", "CURRENCY", objectDescription.EstimatedValue.Currency, objectDescription.EstimatedValue.Value.GetValueOrDefault()) : null,
                config.TimeFrame.Type ? Duration(objectDescription.TimeFrame) : null,
                config.CandidateNumberRestrictions.Selected ? Candidates(objectDescription.CandidateNumberRestrictions) : null,
                OptionsAndVariants(objectDescription.OptionsAndVariants),
                config.TendersMustBePresentedAsElectronicCatalogs && objectDescription.TendersMustBePresentedAsElectronicCatalogs ? Element("ECATALOGUE_REQUIRED") : null,
                config.EuFunds.ProcurementRelatedToEuProgram
                    ? objectDescription.EuFunds.ProcurementRelatedToEuProgram
                        ? PElement("EU_PROGR_RELATED", objectDescription.EuFunds.ProjectIdentification)
                        : Element("NO_EU_PROGR_RELATED")
                    : null,
                config.AdditionalInformation ? PElement("INFO_ADD", objectDescription.AdditionalInformation) : null
              );
        }

        private IEnumerable<XElement> OptionsAndVariants(OptionsAndVariants optionsAndVariants)
        {
            var config = _configuration.ObjectDescriptions.OptionsAndVariants;

            if (config.VariantsWillBeAccepted && optionsAndVariants.VariantsWillBeAccepted)
            {
                yield return Element("ACCEPTED_VARIANTS");
            }
            else if (config.VariantsWillBeAccepted && !_notice.Type.IsPriorInformation())
            {
                yield return Element("NO_ACCEPTED_VARIANTS");
            }
            if (config.Options && optionsAndVariants.Options)
            {
                yield return Element("OPTIONS");
                yield return PElement("OPTIONS_DESCR", optionsAndVariants.OptionsDescription);

            }
            else if (config.Options && !_notice.Type.IsPriorInformation())
            {
                yield return Element("NO_OPTIONS");
            }

        }

        private XElement Renewals(bool canBeRenewed)
    {
      if (_notice.Type.IsPriorInformation())
            {
                return canBeRenewed ? Element("RENEWAL") : null;
      }

      return Element(canBeRenewed ? "RENEWAL" : "NO_RENEWAL");
    }

    #endregion

    private IEnumerable<XElement> Candidates(CandidateNumberRestrictions candidates)
        {
            if (_notice.ProcedureInformation.ProcedureType == ProcedureType.ProctypeOpen)
            {
                yield break;
            }

            switch (candidates.Selected)
            {
                case EnvisagedParticipantsOptions.EnvisagedNumber:
                    yield return Element("NB_ENVISAGED_CANDIDATE", candidates.EnvisagedNumber);
                    break;
                case EnvisagedParticipantsOptions.Range:
                    yield return Element("NB_MIN_LIMIT_CANDIDATE", candidates.EnvisagedMinimumNumber);
                    yield return Element("NB_MAX_LIMIT_CANDIDATE", candidates.EnvisagedMaximumNumber);
                    break;
                default:
                    yield break;
            }

            yield return PElement("CRITERIA_CANDIDATE", candidates.ObjectiveCriteriaForChoosing);
        }

        private IEnumerable<XElement> Duration(TimeFrame timeFrame)
        {
            switch (timeFrame.Type)
            {
                case TimeFrameType.Days:
                    yield return ElementWithAttribute("DURATION", "TYPE", "DAY", timeFrame.Days);
                    break;
                case TimeFrameType.Months:
                    yield return ElementWithAttribute("DURATION", "TYPE", "MONTH", timeFrame.Months);
                    break;
                case TimeFrameType.BeginAndEndDate:
                    yield return DateElement("DATE_START", timeFrame.BeginDate);
                    yield return DateElement("DATE_END", timeFrame.EndDate);
                    break;
            }

            if (_configuration.ObjectDescriptions.TimeFrame.CanBeRenewed)
            {
                yield return Renewals(timeFrame.CanBeRenewed);
            }

            if (timeFrame.CanBeRenewed && _configuration.ObjectDescriptions.TimeFrame.CanBeRenewed)
            {
                yield return PElement("RENEWAL_DESCR", timeFrame.RenewalDescription);
            }
        }

        /// <summary>
        ///     Iterates over all the object descriptions and produces an IEnumerable of XElements.
        /// </summary>
        /// <param name="objectDescriptions">Collection of dbo object descriptions</param>
        /// <returns>The object descriptions XElements collection</returns>
        public IEnumerable<XElement> ObjectDescriptions(ObjectDescription[] objectDescriptions)
        {
            for (int i = 0; i < objectDescriptions.Length; i++)
            {
                yield return ObjectDescription(i + 1, objectDescriptions[i]);
            }
        }


        private IEnumerable<XElement> NutsCodes(string[] nutsCodes)
        {
            return nutsCodes.Select(
                n => new XElement(n2016 + "NUTS", new XAttribute("CODE", n)));
        }

        private IEnumerable<XElement> AwardCriteria(ObjectDescription objectDescription)
        {
            var config = _configuration.ObjectDescriptions.AwardCriteria;
            if (!config.CriterionTypes)
            {
                yield break;
            }

            var awardCriteria = objectDescription.AwardCriteria;
            var awardCriteriaTypes = awardCriteria.CriterionTypes;
            if (awardCriteriaTypes.HasFlag(AwardCriterionType.DescriptiveCriteria) ||
                awardCriteriaTypes.HasFlag(AwardCriterionType.AwardCriteriaInDocs))
            {
                yield return Element("AC_PROCUREMENT_DOC");
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
                    yield return Element("AC_PRICE",
                        Element("AC_WEIGHTING", awardCriteria.PriceCriterion.Weighting));
                } else if (awardCriteriaTypes.HasFlag(AwardCriterionType.PriceCriterion))
                {
                    yield return Element("AC_PRICE");
                }
                if (awardCriteriaTypes.HasFlag(AwardCriterionType.AwardCriteriaDescrBelow))
                {
                    foreach (var criteria in objectDescription.AwardCriteria.Criterion)
                    {
                        yield return Element("AC_CRITERION", criteria);
                    }
                }
            }
        }

        /// <summary>
        /// Helper to select directive element string from procurement category.
        /// </summary>
        /// <returns></returns>
        public string DirectiveSelector()
        {
            if (_notice.Project.ProcurementCategory == ProcurementCategory.Defence)
            {
                return "DIRECTIVE_2009_81_EC";
            }
            else if (_notice.Project.ProcurementCategory == ProcurementCategory.Lisence)
            {
                return "DIRECTIVE_2014_23_EU";
            }
            else if (_notice.Project.ProcurementCategory == ProcurementCategory.Utility)
            {
                return "DIRECTIVE_2014_25_EU";
            }

            return "DIRECTIVE_2014_24_EU";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDescription"></param>
        /// <returns></returns>
        private IEnumerable<XElement> AwardCriteriaExAnte(ObjectDescription objectDescription)
        {
            var config = _configuration.ObjectDescriptions.AwardCriteria;
            if (!config.CriterionTypes)
            {
                yield break;
            }

            if (_notice.Project.ProcurementCategory == ProcurementCategory.Defence)
            {
                if (objectDescription.AwardCriteria.CriterionTypes == AwardCriterionType.LowestPrice)
                {
                    yield return Element("AC_PRICE");
                }
                else if (objectDescription.AwardCriteria.CriterionTypes == AwardCriterionType.EconomicallyAdvantageous)
                {
                    foreach (var criteria in objectDescription.AwardCriteria.CostCriteria)
                    {
                        yield return Criterion("AC_CRITERIA", criteria);
                    }
                }
            }
            else if (_notice.Project.ProcurementCategory == ProcurementCategory.Lisence)
            {
                foreach (var criteria in objectDescription.AwardCriteria.Criterion)
                {
                    yield return Element("AC_CRITERION", criteria);
                }
            }
            else
            {

                var awardCriteria = objectDescription.AwardCriteria;
                var awardCriteriaTypes = awardCriteria.CriterionTypes;
                if (awardCriteriaTypes.HasFlag(AwardCriterionType.DescriptiveCriteria))
                {
                    yield return Element("AC_PROCUREMENT_DOC");
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

                    if (awardCriteriaTypes.HasFlag(AwardCriterionType.PriceCriterion) &&
                        awardCriteriaTypes.HasFlag(AwardCriterionType.QualityCriterion))
                    {
                        yield return Element("AC_PRICE",
                            Element("AC_WEIGHTING", awardCriteria.PriceCriterion.Weighting));
                    }
                    else if (awardCriteriaTypes.HasFlag(AwardCriterionType.PriceCriterion))
                    {
                        yield return Element("AC_PRICE");
                    }
                }
            }
        }

        private static XElement Criterion(string name, AwardCriterionDefinition qualityCriterion)
        {
            return Element(name,
                Element("AC_CRITERION", qualityCriterion.Criterion),
                Element("AC_WEIGHTING", qualityCriterion.Weighting)
            );
        }

        /// <summary>
        /// Section III: Legal, economic, financial and technical information
        /// </summary>
        /// <returns>Conditions element</returns>
        public XElement ConditionsInformation()
        {
            var config = _configuration.ConditionsInformation;
            if (config == null)
            {
                return null;
            }

            var conditions = Element("LEFTI");
            if (config.ProfessionalSuitabilityRequirements)
            {
            conditions.Add(PElement("SUITABILITY", _notice.ConditionsInformation.ProfessionalSuitabilityRequirements));
            }
            if (config.EconomicCriteriaDescription && !_notice.ConditionsInformation.EconomicCriteriaToParticipate)
            {
                conditions.Add(PElement("ECONOMIC_FINANCIAL_INFO", _notice.ConditionsInformation.EconomicCriteriaDescription));
                conditions.Add(PElement("ECONOMIC_FINANCIAL_MIN_LEVEL", _notice.ConditionsInformation.EconomicRequiredStandards));

            }
            else if (config.EconomicCriteriaDescription)
            {
                conditions.Add(Element("ECONOMIC_CRITERIA_DOC"));
            }

            if (config.TechnicalCriteriaToParticipate && !_notice.ConditionsInformation.TechnicalCriteriaToParticipate)
            {
                conditions.Add(PElement("TECHNICAL_PROFESSIONAL_INFO", _notice.ConditionsInformation.TechnicalCriteriaDescription));
                conditions.Add(PElement("TECHNICAL_PROFESSIONAL_MIN_LEVEL", _notice.ConditionsInformation.TechnicalRequiredStandards));
            }
            else if (config.TechnicalCriteriaToParticipate)
            {
                conditions.Add(Element("TECHNICAL_CRITERIA_DOC"));

            }

            if (config.RulesForParticipation)
            {
                conditions.Add(PElement("RULES_CRITERIA", _notice.ConditionsInformation.RulesForParticipation));
            }



            if (config.RestrictedToShelteredWorkshop && _notice.ConditionsInformation.RestrictedToShelteredWorkshop)
            {
                conditions.Add(Element("RESTRICTED_SHELTERED_WORKSHOP"));
            }

            if (config.RestrictedToShelteredProgram && _notice.ConditionsInformation.RestrictedToShelteredProgram)
            {
                conditions.Add(Element("RESTRICTED_SHELTERED_PROGRAM"));
            }

            if (config.ReservedOrganisationServiceMission &&
                _notice.ConditionsInformation.ReservedOrganisationServiceMission &&
                CpvCodeMetadata.SpecialSocialCodes.Contains(_notice.ProcurementObject.MainCpvCode.Code))
            {
                conditions.Add(Element("RESERVED_ORGANISATIONS_SERVICE_MISSION"));
            }

            if (config.CiriteriaForTheSelectionOfParticipants)
            {
                if (_notice.ProcedureInformation.ContestType == ContestType.TypeRestricted)
                {
                    conditions.Add(PElement("CRITERIA_SELECTION", _notice.ConditionsInformation.CiriteriaForTheSelectionOfParticipants));
                }
            }

            if (config.ParticipationIsReservedForProfession)
            {
                if (_notice.ConditionsInformation.ParticipationIsReservedForProfession)
                {
                    conditions.Add(PElement("PARTICULAR_PROFESSION", _notice.ConditionsInformation.IndicateProfession));
                }
                else
                {
                    conditions.Add(Element("NO_PARTICULAR_PROFESSION"));
                }
            }

            if (config.ExecutionOfServiceIsReservedForProfession && _notice.Project.ContractType == ContractType.Services && _notice.ConditionsInformation.ExecutionOfServiceIsReservedForProfession)
            {
                conditions.Add(ElementWithAttribute("PARTICULAR_PROFESSION", "CTYPE", "SERVICES"));
                conditions.Add(PElement("REFERENCE_TO_LAW", _notice.ConditionsInformation.ReferenceToRelevantLawRegulationOrProvision));
            }
            if (config.ContractPerformanceConditions)
            {
                conditions.Add(PElement("PERFORMANCE_CONDITIONS", _notice.ConditionsInformation.ContractPerformanceConditions));
            }
            if (config.ObligationToIndicateNamesAndProfessionalQualifications && _notice.ConditionsInformation.ObligationToIndicateNamesAndProfessionalQualifications)
            {
                conditions.Add(Element("PERFORMANCE_STAFF_QUALIFICATION"));
            }

            return conditions;
        }

        /// <summary>
        /// Section IV: Procedure
        /// </summary>
        /// <returns>Procedure element</returns>
        public XElement Procedure()
        {
            var configuration = _configuration.ProcedureInformation;

            var procedure = Element("PROCEDURE");
            var procedureInformation = _notice.ProcedureInformation;
            var type = procedureInformation?.ProcedureType;

            if (procedureInformation == null || configuration == null)
            {
                return null;
            }

            if (configuration.ProcedureType) {
                  switch (type)
                {
                    case ProcedureType.ProctypeOpen:
                        procedure.Add(Element("PT_OPEN"));
                        break;
                    case ProcedureType.ProctypeRestricted:
                        procedure.Add(Element("PT_RESTRICTED"));
                        break;
                    case ProcedureType.ProctypeCompNegotiation:
                        procedure.Add(Element("PT_COMPETITIVE_NEGOTIATION"));
                        break;
                    case ProcedureType.ProctypeCompDialogue:
                        procedure.Add(Element("PT_COMPETITIVE_DIALOGUE"));
                        break;
                    case ProcedureType.ProctypeInnovation:
                        procedure.Add(Element("PT_INNOVATION_PARTNERSHIP"));
                        break;
                    case ProcedureType.ProctypeNegotiationsInvolved:
                        procedure.Add(Element("PT_INVOLVING_NEGOTIATION"));
                        break;
                    case ProcedureType.AwardWoPriorPubD1:
                    case ProcedureType.AwardWoPriorPubD1Other:
                        procedure.Add(_annexHelper.SelectAnnexD());
                        break;
                }
            }

            if (configuration.AcceleratedProcedure && procedureInformation?.AcceleratedProcedure == true)
            {
                procedure.Add(PElement("ACCELERATED_PROC", _notice.ProcedureInformation.JustificationForAcceleratedProcedure));
            }

            if (configuration.ContestType)
            {
                switch (procedureInformation?.ContestType)
                {
                    case ContestType.Open:
                        procedure.Add(Element("PT_OPEN"));
                        break;
                    case ContestType.TypeRestricted:
                        procedure.Add(Element("PT_RESTRICTED"));
                        break;
                }
            }

            if (configuration.ContestParticipants.Type && procedureInformation?.ContestType == ContestType.TypeRestricted)
            {
                if (procedureInformation.ContestParticipants.Type == ContractValueType.Range)
                {
                    procedure.Add(Element("NB_MIN_PARTICIPANTS", procedureInformation.ContestParticipants.MinValue.ToString("F0")));
                    procedure.Add(Element("NB_MAX_PARTICIPANTS", procedureInformation.ContestParticipants.MaxValue.ToString("F0")));
                }
                else
                {
                    procedure.Add(Element("NB_PARTICIPANTS", procedureInformation.ContestParticipants.Value.ToString("F0")));
                }
            }

            if (configuration.FrameworkAgreement.IncludesFrameworkAgreement && _notice.ProcedureInformation.FrameworkAgreement.IncludesFrameworkAgreement)
            {
                var participants = _notice.ProcedureInformation.FrameworkAgreement.EnvisagedNumberOfParticipants;
                procedure.Add(Element("FRAMEWORK",
                    configuration.FrameworkAgreement.EnvisagedNumberOfParticipants ? (participants != null && participants > 1
                        ? Element("SEVERAL_OPERATORS")
                        : Element("SINGLE_OPERATOR")) : null,
                    configuration.FrameworkAgreement.EnvisagedNumberOfParticipants ? Element("NB_PARTICIPANTS", participants) : null,
                    configuration.FrameworkAgreement.JustificationForDurationOverFourYears ? PElement("JUSTIFICATION", _notice.ProcedureInformation.FrameworkAgreement.JustificationForDurationOverFourYears) : null));
            }
            else if (configuration.FrameworkAgreement.IncludesDynamicPurchasingSystem && _notice.ProcedureInformation.FrameworkAgreement.IncludesDynamicPurchasingSystem)
            {
                procedure.Add(Element("DPS"));
                procedure.Add(_notice.ProcedureInformation.FrameworkAgreement.DynamicPurchasingSystemInvolvesAdditionalPurchasers ? Element("DPS_ADDITIONAL_PURCHASERS") : null);
            }

            if (configuration.ReductionRecourseToReduceNumberOfSolutions && type == ProcedureType.ProctypeCompNegotiation || type == ProcedureType.ProctypeCompDialogue || type == ProcedureType.ProctypeInnovation)
            {
                procedure.Add(_notice.ProcedureInformation.ReductionRecourseToReduceNumberOfSolutions ? Element("REDUCTION_RECOURSE") : null);
            }

            if (configuration.ReserveRightToAwardWithoutNegotiations && type == ProcedureType.ProctypeCompNegotiation)
            {
                procedure.Add(_notice.ProcedureInformation.ReserveRightToAwardWithoutNegotiations ? Element("RIGHT_CONTRACT_INITIAL_TENDERS") : null);
            }

            if(configuration.ElectronicAuctionWillBeUsed && _notice.ProcedureInformation.ElectronicAuctionWillBeUsed)
            {
                procedure.Add(Element("EAUCTION_USED"));
                if (configuration.AdditionalInformationAboutElectronicAuction)
                {
                    procedure.Add(PElement("INFO_ADD_EAUCTION", _notice.ProcedureInformation.AdditionalInformationAboutElectronicAuction));
                }
            }

            // IV.1.7
            if (configuration.NamesOfParticipantsAlreadySelected && procedureInformation.ContestType == ContestType.TypeRestricted)
            {
                foreach(var participant in _notice.ProcedureInformation.NamesOfParticipantsAlreadySelected)
                {
                    procedure.Add(Element("PARTICIPANT_NAME", participant));
                }
            }

            // IV.1.8
            if (configuration.ProcurementGovernedByGPA)
            {
                if(_notice.Type == NoticeType.Concession && _notice.Project.ContractType == ContractType.Works)
                {
                    procedure.Add(_notice.ProcedureInformation.ProcurementGovernedByGPA ?
                        ElementWithAttribute("CONTRACT_COVERED_GPA", "CTYPE", "WORKS") :
                        ElementWithAttribute("NO_CONTRACT_COVERED_GPA", "CTYPE", "WORKS"));
                }
                if(_notice.Type != NoticeType.Concession)
                {
                    procedure.Add(_notice.ProcedureInformation.ProcurementGovernedByGPA ?
                        Element("CONTRACT_COVERED_GPA") :
                        Element("NO_CONTRACT_COVERED_GPA"));
                }
            }

            // IV.1.9
            if (configuration.CriteriaForEvaluationOfProjects)
            {
                procedure.Add(PElement("CRITERIA_EVALUATION", _notice.ProcedureInformation.CriteriaForEvaluationOfProjects));
            }

            // IV.1.10
            if (configuration.UrlNationalProcedure)
            {
                procedure.Add(Element("URL_NATIONAL_PROCEDURE", _notice.ProcedureInformation.UrlNationalProcedure));
            }

            // IV.1.11
            if (configuration.MainFeaturesAward)
            {
                procedure.Add(PElement("MAIN_FEATURES_AWARD", _notice.ProcedureInformation.MainFeaturesAward));
            }


            if (_configuration.PreviousNoticeOjsNumber)
            {
                procedure.Add(Element("NOTICE_NUMBER_OJ", _notice.PreviousNoticeOjsNumber));
            }

            if (_configuration.TenderingInformation.TendersOrRequestsToParticipateDueDateTime)
            {
                procedure.Add(DateElement("DATE_RECEIPT_TENDERS", _notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime));
                procedure.Add(TimeElement("TIME_RECEIPT_TENDERS", _notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime));
            }
            if (_configuration.TenderingInformation.EstimatedDateOfInvitations)
            {
                if((type != ProcedureType.ProctypeOpen && _notice.Type != NoticeType.DesignContest) ||
                (_notice.Type == NoticeType.DesignContest && procedureInformation.ContestType == ContestType.TypeRestricted))
                {
                    procedure.Add(DateElement("DATE_DISPATCH_INVITATIONS", _notice.TenderingInformation.EstimatedDateOfInvitations));
                }
            }

            if (_configuration.TenderingInformation.Languages && _notice.TenderingInformation.Languages.Any())
            {
                procedure.Add(Element("LANGUAGES", _notice.TenderingInformation.Languages.Select(x => ElementWithAttribute("LANGUAGE", "VALUE", x))));
            }

            if (_configuration.TenderingInformation.TendersMustBeValidOption)
            {
                switch (_notice.TenderingInformation.TendersMustBeValidOption)
                {
                    case TendersMustBeValidOption.Months:
                        if (_notice.TenderingInformation.TendersMustBeValidForMonths != null)
                        {
                            procedure.Add(ElementWithAttribute("DURATION_TENDER_VALID", "TYPE", "MONTH", _notice.TenderingInformation.TendersMustBeValidForMonths));
                        }
                        break;
                    case TendersMustBeValidOption.Date:
                        procedure.Add(DateElement("DATE_TENDER_VALID", _notice.TenderingInformation.TendersMustBeValidUntil));
                        break;
                    default:
                        break;
                }
            }

            if (_configuration.TenderingInformation.TenderOpeningConditions.OpeningDateAndTime && type == ProcedureType.ProctypeOpen)
            {
                procedure.Add(Element("OPENING_CONDITION",
                    DateElement("DATE_OPENING_TENDERS", _notice.TenderingInformation.TenderOpeningConditions.OpeningDateAndTime),
                    TimeElement("TIME_OPENING_TENDERS", _notice.TenderingInformation.TenderOpeningConditions.OpeningDateAndTime),
                    PElement("PLACE", _notice.TenderingInformation.TenderOpeningConditions.Place),
                    PElement("INFO_ADD", _notice.TenderingInformation.TenderOpeningConditions.InformationAboutAuthorisedPersons)));
            }

            if(_configuration.RewardsAndJury.PrizeAwarded)
            {
                if (_notice.RewardsAndJury.PrizeAwarded)
                {
                    procedure.Add(Element("PRIZE_AWARDED"));
                    procedure.Add(PElement("NUMBER_VALUE_PRIZE", _notice.RewardsAndJury.NumberAndValueOfPrizes));
                }
                else
                {
                    procedure.Add(Element("NO_PRIZE_AWARDED"));
                }

                procedure.Add(PElement("DETAILS_PAYMENT", _notice.RewardsAndJury.DetailsOfPayments));

                if (_notice.RewardsAndJury.ServiceContractAwardedToWinner)
                {
                    procedure.Add(Element("FOLLOW_UP_CONTRACTS"));
                }
                else
                {
                    procedure.Add(Element("NO_FOLLOW_UP_CONTRACTS"));
                }

                if (_notice.RewardsAndJury.DecisionOfTheJuryIsBinding)
                {
                    procedure.Add(Element("DECISION_BINDING_CONTRACTING"));
                }
                else
                {
                    procedure.Add(Element("NO_DECISION_BINDING_CONTRACTING"));
                }

                foreach(var member in _notice.RewardsAndJury.NamesOfSelectedMembersOfJury)
                {
                    procedure.Add(Element("MEMBER_NAME", member));
                }
            }

            return procedure.HasElements ? procedure : null;
        }

        #endregion

        #region Section V: Award of contract

        /// <summary>
        /// Section V: Award of contract
        /// </summary>
        /// <returns>award of contract element</returns>
        public IEnumerable<XElement> ContractAward()
        {
            return _notice.ObjectDescriptions.Select((lot, i) => {
                var awardedContract = lot.AwardContract.AwardedContract;
                var noAwardedContract = lot.AwardContract.NoAwardedContract;
                var awardedContractConfig = _configuration.ObjectDescriptions.AwardContract.AwardedContract;

                var hideTenders = _notice.Type == NoticeType.ExAnte;

                return Element("AWARD_CONTRACT", new XAttribute("ITEM", lot.LotNumber),
                    Element("CONTRACT_NO", GetContractNumber(_notice, lot.LotNumber)),
                    _notice.LotsInfo.DivisionLots ? Element("LOT_NO", lot.LotNumber.ToString()) : null,
                    awardedContractConfig.ContractTitle ? PElement("TITLE", lot.AwardContract.AwardedContract.ContractTitle) : null,
                    // PElement("TITLE", lot.Title),
                    lot.AwardContract.ContractAwarded == ContractAwarded.AwardedContract ?
                    Element("AWARDED_CONTRACT",
                        DateElement("DATE_CONCLUSION_CONTRACT", awardedContract.ConclusionDate <= DateTime.Now ? awardedContract.ConclusionDate : DateTime.Now),
                        hideTenders
                            ? null
                            : Element("TENDERS", awardedContractConfig.NumberOfTenders.DisagreeTenderInformationToBePublished
                                ? new XAttribute("PUBLICATION", awardedContract.NumberOfTenders.DisagreeTenderInformationToBePublished ? "NO" : "YES")
                                : null,
                                Element("NB_TENDERS_RECEIVED", awardedContract.NumberOfTenders.Total),
                                Element("NB_TENDERS_RECEIVED_SME", awardedContract.NumberOfTenders.Sme ?? 0),
                                Element("NB_TENDERS_RECEIVED_OTHER_EU", awardedContract.NumberOfTenders.OtherEu ?? 0),
                                Element("NB_TENDERS_RECEIVED_NON_EU", awardedContract.NumberOfTenders.NonEu ?? 0),
                                Element("NB_TENDERS_RECEIVED_EMEANS", awardedContract.NumberOfTenders.Electronic ?? 0)),
                        Element("CONTRACTORS",
                            awardedContractConfig.DisagreeContractorInformationToBePublished
                                ? new XAttribute("PUBLICATION", awardedContract.DisagreeContractorInformationToBePublished ? "NO" : "YES")
                                : null,
                            awardedContract.Contractors.Count > 1 ? Element("AWARDED_TO_GROUP") : _notice.Type != NoticeType.SocialContractAward ? Element("NO_AWARDED_TO_GROUP") : null,
                            awardedContract.Contractors.Select((contractor, a) =>
                                Element("CONTRACTOR",
                                    ADDRS5(contractor),
                                    contractor.IsSmallMediumEnterprise ? Element("SME") : _notice.Type != NoticeType.SocialContractAward ? Element("NO_SME") : null
                                )
                            )
                        ),
                        Element("VALUES", awardedContractConfig.FinalTotalValue.DisagreeToBePublished && awardedContract.FinalTotalValue.DisagreeToBePublished != null ? new XAttribute("PUBLICATION", awardedContract.FinalTotalValue.DisagreeToBePublished == false ? "YES" : "NO") : null,
                            awardedContract.InitialEstimatedValueOfContract.Value != null ?
                                ElementWithAttribute("VAL_ESTIMATED_TOTAL", "CURRENCY", awardedContract.InitialEstimatedValueOfContract.Currency, awardedContract.InitialEstimatedValueOfContract.Value) : null,
                            ValueTotal(awardedContract.FinalTotalValue, false)
                        ),
                        awardedContract.LikelyToBeSubcontracted && awardedContractConfig.LikelyToBeSubcontracted ?
                        new List<XElement> {
                            Element("LIKELY_SUBCONTRACTED"),
                            ElementWithAttribute("VAL_SUBCONTRACTING", "CURRENCY", awardedContract.ValueOfSubcontract.Currency, awardedContract.ValueOfSubcontract.Value),
                            Element("PCT_SUBCONTRACTING", (int?)awardedContract.ProportionOfValue),
                            PElement("INFO_ADD_SUBCONTRACTING", awardedContract.SubcontractingDescription),
                            awardedContract.ExAnteSubcontracting != null && awardedContractConfig.ExAnteSubcontracting != null
                                ? Element("DIRECTIVE_2009_81_EC",
                                     awardedContract.ExAnteSubcontracting.AllOrCertainSubcontractsWillBeAwarded ? Element("AWARDED_SUBCONTRACTING") : null,
                                    awardedContract.ExAnteSubcontracting.ShareOfContractWillBeSubcontracted
                                        ? Element("PCT_RANGE_SHARE_SUBCONTRACTING",
                                            awardedContract.ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMinPercentage > 0 ? Element("MIN", (int)awardedContract.ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMinPercentage) : null,
                                            awardedContract.ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMinPercentage > 0 ? Element("MAX", (int)awardedContract.ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMaxPercentage) : null)
                                        : null)
                                : null
                        } : null,
                        awardedContractConfig.PricePaidForBargainPurchases.Value && awardedContract.PricePaidForBargainPurchases.Value != null ?
                            ElementWithAttribute("VAL_BARGAIN_PURCHASE", "CURRENCY",
                                awardedContract.PricePaidForBargainPurchases.Currency,
                                awardedContract.PricePaidForBargainPurchases.Value
                            ) : null,
                        awardedContractConfig.NotPublicFields.CommunityOrigin ? ElementWithAttribute("NB_CONTRACT_AWARDED", "PUBLICATION", "NO", awardedContract.Contractors.Count) : null,
                        awardedContractConfig.NotPublicFields.CommunityOrigin ?
                            Element("COUNTRY_ORIGIN", new XAttribute("PUBLICATION", "NO"),
                                awardedContract.NotPublicFields.CommunityOrigin ? Element("COMMUNITY_ORIGIN") : null,
                                awardedContract.NotPublicFields.NonCommunityOrigin ? awardedContract.NotPublicFields.Countries.Select(country =>
                                    ElementWithAttribute("NON_COMMUNITY_ORIGIN", "VALUE", country)
                                ) : null
                            ) : null,
                        awardedContractConfig.NotPublicFields.AwardedToTendererWithVariant ?
                            ElementWithAttribute(
                                awardedContract.NotPublicFields.AwardedToTendererWithVariant ? "AWARDED_TENDERER_VARIANT": "NO_AWARDED_TENDERER_VARIANT",
                                "PUBLICATION",
                                "NO"
                            ) : null,
                        awardedContractConfig.NotPublicFields.AbnormallyLowTendersExcluded ?
                            ElementWithAttribute(
                                awardedContract.NotPublicFields.AbnormallyLowTendersExcluded ? "TENDERS_EXCLUDED" : "NO_TENDERS_EXCLUDED",
                                "PUBLICATION",
                                "NO"
                            ) : null
                    ) : Element("NO_AWARDED_CONTRACT",
                        noAwardedContract.FailureReason == ProcurementFailureReason.AwardNoTenders ?
                            Element("PROCUREMENT_UNSUCCESSFUL") :
                            Element("PROCUREMENT_DISCONTINUED",
                                noAwardedContract.FailureReason == ProcurementFailureReason.AwardDiscontinued ?
                                    ElementWithAttribute("ORIGINAL_TED_ESENDER", "PUBLICATION", "NO") : null,
                                    new List<XElement>
                                    {
                                        ElementWithAttribute("ESENDER_LOGIN", "PUBLICATION", "NO", String.IsNullOrEmpty(noAwardedContract.OriginalEsender?.Login)
                                            ? _esenderLogin
                                            : noAwardedContract.OriginalEsender?.Login),
                                        ElementWithAttribute("NO_DOC_EXT", "PUBLICATION", "NO", noAwardedContract.OriginalEsender?.TedNoDocExt),
                                    },
                                    DateElementWithAttribute("DATE_DISPATCH_ORIGINAL", "PUBLICATION", "NO", noAwardedContract.OriginalNoticeSentDate)
                            )
                        )
                    );
            });
        }
        #endregion

        #region Section VI: Complementary information

        /// <summary>
        /// Section VI: Complementary information
        /// </summary>
        /// <returns>The complementary section</returns>
        public XElement ComplementaryInformation()
        {
            var complementaryInfo = Element("COMPLEMENTARY_INFO");
            var configuration = _configuration.ComplementaryInformation;

            if (configuration.IsRecurringProcurement && _notice.ComplementaryInformation.IsRecurringProcurement)
            {
                complementaryInfo.Add(Element("RECURRENT_PROCUREMENT"));
                complementaryInfo.Add(PElement("ESTIMATED_TIMING", _notice.ComplementaryInformation.EstimatedTimingForFurtherNoticePublish));
            }
            else if (configuration.IsRecurringProcurement)
            {
                complementaryInfo.Add(Element("NO_RECURRENT_PROCUREMENT"));
            }

            if (configuration.ElectronicOrderingUsed && _notice.ComplementaryInformation.ElectronicOrderingUsed)
            {
                complementaryInfo.Add(Element("EORDERING"));
            }

            if (configuration.ElectronicInvoicingUsed && _notice.ComplementaryInformation.ElectronicInvoicingUsed)
            {
                complementaryInfo.Add(Element("EINVOICING"));
            }

            if (configuration.ElectronicPaymentUsed && _notice.ComplementaryInformation.ElectronicPaymentUsed)
            {
                complementaryInfo.Add(Element("EPAYMENT"));
            }
            if (configuration.AdditionalInformation)
            {
                complementaryInfo.Add(PElement("INFO_ADD", _notice.ComplementaryInformation.AdditionalInformation));
            }

            if (_configuration.ProceduresForReview.ReviewBody.OfficialName)
            {
                complementaryInfo.Add(ADDRS6("ADDRESS_REVIEW_BODY", _notice.ProceduresForReview.ReviewBody));
            }
            if (_configuration.ProceduresForReview.ReviewProcedure)
            {
                complementaryInfo.Add(PElement("REVIEW_PROCEDURE", _notice.ProceduresForReview?.ReviewProcedure));
            }

            complementaryInfo.Add(DateElement("DATE_DISPATCH_NOTICE", DateTime.Now));

            return complementaryInfo;
        }

        #endregion
    }
}
