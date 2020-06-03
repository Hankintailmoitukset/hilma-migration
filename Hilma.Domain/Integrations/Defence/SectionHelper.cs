using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Extensions;
using Hilma.Domain.Integrations.Configuration;
using Hilma.Domain.Integrations.Extensions;

namespace Hilma.Domain.Integrations.Defence
{
    /// <summary>
    /// Section helper for defence contracts
    /// </summary>
    public class SectionHelper : TedHelpers
    {
        private readonly NoticeContract _notice;
        private readonly NoticeContractConfiguration _configuration;
        private static string _noticeType;
        private readonly AnnexHelper _annexHelper;
        private readonly ITranslationProvider _translationProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="configuration">Config</param>
        /// <param name="translationProvider">Translations</param>
        public SectionHelper(NoticeContract notice, NoticeContractConfiguration configuration, ITranslationProvider translationProvider)
        {
            _notice = notice;
            _configuration = configuration;
            _annexHelper = new AnnexHelper(_notice, _configuration.Annexes);
            _translationProvider = translationProvider;

            _noticeType = "PRIOR_INFORMATION";
            if (_notice.Type == NoticeType.DefenceContract)
            {
                _noticeType = "CONTRACT";
            }
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                _noticeType = "CONTRACT_AWARD";
            }
        }

        #region Section I: Contracting authority/entity
        /// <summary>
        /// Section I: Contracting authority
        /// </summary>
        /// <param name="project">Procurement project</param>
        /// <param name="contactPerson">The contact person</param>
        /// <param name="communicationInformation">I.3 Communication</param>
        /// <param name="type">Form type</param>
        /// <returns>CONTRACTING_BODY XElement</returns>
        public XElement ContractingBody(ProcurementProjectContract project, ContactPerson contactPerson, CommunicationInformation communicationInformation, NoticeType type)
        {
            var organisation = project.Organisation;

            // init to priorinfo
            var rootElement = "AUTHORITY_PRIOR_INFORMATION_DEFENCE";

            if (_notice.Type == NoticeType.DefenceContract)
            {
                rootElement = "CONTRACTING_AUTHORITY_INFORMATION_DEFENCE";
            }
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                rootElement = "CONTRACTING_AUTHORITY_INFORMATION_CONTRACT_AWARD_DEFENCE";
            }

            return Element(rootElement,
                    Element("NAME_ADDRESSES_CONTACT_" + _noticeType,
                        INC_01("CA_CE_CONCESSIONAIRE_PROFILE", organisation, contactPerson),
                        Element("INTERNET_ADDRESSES_" + _noticeType, InternetAddresses(communicationInformation)),
                        CommunicationInformation(communicationInformation, type)
                    ),
                    Element("TYPE_AND_ACTIVITIES_OR_CONTRACTING_ENTITY_AND_PURCHASING_ON_BEHALF",
                        CaTypeAndActivities(project?.Organisation),
                        JointProcurement(project)
                    ));
        }

        #region Section I helpers
        private XElement JointProcurement(ProcurementProjectContract project)
        {
            XElement element;
            if (project == null)
                return null;

            if (project.JointProcurement)
            {
                var joints = new List<XElement>();
                project.CoPurchasers?.ForEach(x => joints.Add(INC_01("CONTACT_DATA_OTHER_BEHALF_CONTRACTING_AUTORITHY",
                    new OrganisationContract
                    {
                        Information = x
                    })));
                element = Element("PURCHASING_ON_BEHALF_YES", joints);
            }
            else
            {
                element = Element("PURCHASING_ON_BEHALF_NO");
            }

            return Element("PURCHASING_ON_BEHALF", element);
        }

        /// <summary>
        /// I.2	Type of the contracting authority
        /// </summary>
        /// <param name="organisation"></param>
        /// <returns></returns>
        private IEnumerable<XElement> CaTypeAndActivities(OrganisationContract organisation)
        {
            XElement typeElement;
            if (organisation == null)
                yield break;

            if (organisation.ContractingAuthorityType == ContractingAuthorityType.OtherType)
            {
                typeElement = ElementWithAttribute("TYPE_OF_CONTRACTING_AUTHORITY_OTHER", "VALUE", "OTHER", organisation.OtherContractingAuthorityType);
            }
            else
            {
                typeElement = ElementWithAttribute("TYPE_OF_CONTRACTING_AUTHORITY", "VALUE", organisation.ContractingAuthorityType.ToTEDFormat());
            }

            XElement activityElement;

            if (_notice.Project.ProcurementCategory == ProcurementCategory.Utility)
            {
                if (organisation.ContractingAuthorityType == ContractingAuthorityType.OtherType)
                {
                    activityElement = ElementWithAttribute("ACTIVITY_OF_CONTRACTING_ENTITY_OTHER", "VALUE", "OTHER", organisation.OtherMainActivity);
                }
                else
                {
                    activityElement = ElementWithAttribute("ACTIVITY_OF_CONTRACTING_ENTITY", "VALUE", organisation.MainActivityUtilities.ToTEDFormat());
                }

                // yield return Element("TYPE_AND_ACTIVITIES", typeElement);
                yield return Element("ACTIVITIES_OF_CONTRACTING_ENTITY", activityElement);
            }
            else
            {
                if (organisation.OtherMainActivity != null)
                {
                    activityElement = ElementWithAttribute("TYPE_OF_ACTIVITY_OTHER", "VALUE", "OTHER", organisation.OtherMainActivity);
                }
                else
                {
                    activityElement = ElementWithAttribute("TYPE_OF_ACTIVITY", "VALUE", organisation.MainActivity.ToTEDFormat());
                }

                yield return Element("TYPE_AND_ACTIVITIES", typeElement, activityElement);
            }
        }

        /// <summary>
        /// I.1.12-15	Internet address(es)
        /// </summary>
        /// <param name="communication">CommunicationInformation</param>
        /// <returns>Xelement</returns>
        private IEnumerable<XElement> InternetAddresses(CommunicationInformation communication)
        {
            var config = _configuration.CommunicationInformation;

            if (config.AdditionalInformationAddress.MainUrl)
            {
                yield return Element("URL_GENERAL", _notice.Project?.Organisation?.Information?.MainUrl);
            }

            if (config.ElectronicAccess)
            {
                yield return Element("URL_INFORMATION", communication.ElectronicAccess);
            }

            if (config.SendTendersOption)
            {
                yield return communication.SendTendersOption == TenderSendOptions.AddressSendTenders ? Element("URL_PARTICIPATE", communication.ElectronicAddressToSendTenders) : null;
            }

        }

        private IEnumerable<XElement> CommunicationInformation(CommunicationInformation communicationInformation, NoticeType type)
        {
            if (communicationInformation == null)
                yield break;

            var config = _configuration.CommunicationInformation;

            if (config.AdditionalInformation)
            {
                if (communicationInformation.AdditionalInformation == AdditionalInformationAvailability.AddressToAbove)
                {
                    yield return Element("FURTHER_INFORMATION", Element("IDEM"));
                }
                else if (communicationInformation.AdditionalInformation == AdditionalInformationAvailability.AddressAnother)
                {
                    yield return Element("FURTHER_INFORMATION", INC_04(communicationInformation.AdditionalInformationAddress));
                }
            }

            if (config.SpecsAndAdditionalDocuments)
            {
                if (communicationInformation.SpecsAndAdditionalDocuments == SpecificationsAndAdditionalDocuments.AddressToAbove)
                {
                    yield return Element("SPECIFICATIONS_AND_ADDITIONAL_DOCUMENTS", Element("IDEM"));
                }
                else if (communicationInformation.SpecsAndAdditionalDocuments == SpecificationsAndAdditionalDocuments.AddressAnother)
                {
                    yield return Element("SPECIFICATIONS_AND_ADDITIONAL_DOCUMENTS", INC_04(communicationInformation.SpecsAndAdditionalDocumentsAddress));
                }
            }

            if (config.SendTendersOption)
            {
                yield return communicationInformation.SendTendersOption == TenderSendOptions.AddressFollowing
                    ? Element("TENDERS_REQUESTS_APPLICATIONS_MUST_BE_SENT_TO", INC_04(communicationInformation.AddressToSendTenders))
                    : communicationInformation.SendTendersOption == TenderSendOptions.AddressOrganisation
                    ? Element("TENDERS_REQUESTS_APPLICATIONS_MUST_BE_SENT_TO", Element("IDEM")) :
                    null;
            }
        }
        #endregion
        #endregion

        #region Section II: Object
        /// <summary>
        /// Section II: Object
        /// </summary>
        /// <returns>OBJECT_CONTRACT_INFORMATION_DEFENCE XElement</returns>
        public XElement ObjectContract()
        {
            var configuration = _configuration.ProcurementObject;

            // init to priorinfo
            var rootElement = "OBJECT_WORKS_SUPPLIES_SERVICES_PRIOR_INFORMATION";
            var descriptionElement = "DESCRIPTION_CONTRACT_INFORMATION_DEFENCE";

            if (_notice.Type == NoticeType.DefenceContract)
            {
                rootElement = "OBJECT_CONTRACT_INFORMATION_DEFENCE";
            }
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                rootElement = "OBJECT_CONTRACT_INFORMATION_CONTRACT_AWARD_NOTICE_DEFENCE";
                descriptionElement = "DESCRIPTION_AWARD_NOTICE_INFORMATION_DEFENCE";
            }

            if (_notice.Type == NoticeType.DefencePriorInformation)
            {
                return Element(rootElement,
                    NoticeObject()
                );
            }
            else
            {
                var defence = _notice.ProcurementObject.Defence;
                if (defence == null)
                    return null;

                return Element(rootElement,
                        Element(descriptionElement,
                            NoticeObject()),
                            configuration.Defence.TotalQuantityOrScope.Value && defence.TotalQuantityOrScope?.Type != ContractValueType.Undefined ?
                                ScopeAndOptions() : null,

                            configuration.Defence.TimeFrame.Type && (defence.TimeFrame?.Type != null && defence.TimeFrame?.Type != TimeFrameType.Undefined) ?

                                Element("PERIOD_WORK_DATE_STARTING", Duration(defence.TimeFrame)) : null,
                            configuration.TotalValue.Type && _notice.ProcurementObject?.TotalValue?.Type != ContractValueType.Undefined ?
                                Element("TOTAL_FINAL_VALUE",
                                    ElementWithAttribute("COSTS_RANGE_AND_CURRENCY_WITH_VAT_RATE", "CURRENCY", _notice.ProcurementObject?.TotalValue?.Currency,
                                    _notice.ProcurementObject?.TotalValue?.Type == ContractValueType.Range ?
                                        Element("RANGE_VALUE_COST",
                                            Element("LOW_VALUE", _notice.ProcurementObject?.TotalValue?.MinValue),
                                            Element("HIGH_VALUE", _notice.ProcurementObject.TotalValue.MaxValue)) :
                                        Element("VALUE_COST", _notice.ProcurementObject?.TotalValue?.Value),
                                        Element("EXCLUDING_VAT"))) : null);
            }
        }

        private IEnumerable<XElement> NoticeObject()
        {
            var configuration = _configuration.ProcurementObject;
            var projectConfig = _configuration.Project;
            var defObject = _notice.ProcurementObject.Defence;

            if (defObject == null)
            {
                yield break;
            }

            if (projectConfig.Title)
            {
                yield return PElement("TITLE_CONTRACT", _notice.Project?.Title);
            }

            if (_notice.Type == NoticeType.DefencePriorInformation)
            {
                yield return Element("TYPE_CONTRACT_PLACE_DELIVERY_DEFENCE",
                                Element("TYPE_CONTRACT_PI_DEFENCE",
                                    ElementWithAttribute("TYPE_CONTRACT", "VALUE", _notice.Project?.ContractType.ToTEDFormat()),
                                    Element("SERVICE_CATEGORY_DEFENCE", _notice.Project?.DefenceCategory?.Code)),
                                Element("SITE_OR_LOCATION",
                                    PElement("LABEL", defObject.MainsiteplaceWorksDelivery),
                                    NutsCodes(defObject.NutsCodes)));
            }
            if (_notice.Type == NoticeType.DefenceContract)
            {
                yield return Element("TYPE_CONTRACT_DEFENCE",
                                ElementWithAttribute("TYPE_CONTRACT", "VALUE", _notice.Project?.ContractType.ToTEDFormat()),
                                TypeContract(_notice.Project));
            }
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                yield return Element("TYPE_CONTRACT_W_PUB_DEFENCE",
                                ElementWithAttribute("TYPE_CONTRACT", "VALUE", _notice.Project?.ContractType.ToTEDFormat()),
                                TypeContract(_notice.Project));
            }

            // Prior info has nuts set in TYPE_CONTRACT_PLACE_DELIVERY_DEFENCE
            if (_notice.Type != NoticeType.DefencePriorInformation)
            {
                yield return Element("LOCATION_NUTS",
                            PElement("LOCATION", defObject.MainsiteplaceWorksDelivery),
                            NutsCodes(defObject.NutsCodes));
            }

            yield return FrameworkAgreementType(defObject.FrameworkAgreement);
            yield return FrameworkAgreement(defObject.FrameworkAgreement);

            if (_notice.Type != NoticeType.DefencePriorInformation)
            {
                yield return PElement("SHORT_CONTRACT_DESCRIPTION", _notice.ProcurementObject?.ShortDescription);
            }

            if (_notice.Type == NoticeType.DefencePriorInformation)
            {
                yield return Element("QUANTITY_SCOPE_WORKS_DEFENCE",
                    PElement("TOTAL_QUANTITY_OR_SCOPE", _notice.ProcurementObject?.ShortDescription),
                    _notice.ProcurementObject.EstimatedValue.Type != ContractValueType.Undefined ?
                    ElementWithAttribute("COSTS_RANGE_AND_CURRENCY", "CURRENCY", _notice.ProcurementObject?.EstimatedValue?.Currency,
                        _notice.ProcurementObject?.EstimatedValue?.Type == ContractValueType.Range ?
                        Element("RANGE_VALUE_COST", Element("LOW_VALUE", _notice.ProcurementObject?.EstimatedValue?.MinValue), Element("HIGH_VALUE", _notice.ProcurementObject?.EstimatedValue?.MaxValue)) :
                        Element("VALUE_COST", _notice.ProcurementObject?.EstimatedValue?.Value)) : null,
                    LotDivision(_notice.LotsInfo));
            }

            if (_notice.ProcurementObject?.MainCpvCode != null)
            {
                var cpvCodes = _notice.ProcurementObject?.MainCpvCode != null ? new CpvCode[] { _notice.ProcurementObject?.MainCpvCode } : new CpvCode[0];
                yield return Element("CPV",
                    CpvCodeElement("CPV_MAIN", cpvCodes),
                    CpvCodeElement("CPV_ADDITIONAL", defObject.AdditionalCpvCodes));
            }

            if (_notice.Type == NoticeType.DefenceContract)
            {
                yield return SubContracting(defObject.Subcontract);

                yield return LotDivision(_notice.LotsInfo);
            }

            if (_configuration.ProcurementObject.Defence.OptionsAndVariants.VariantsWillBeAccepted)
            {
                yield return ElementWithAttribute("ACCEPTED_VARIANTS", "VALUE", defObject?.OptionsAndVariants?.VariantsWillBeAccepted ?? false ? "YES" : "NO");
            }

            // Prior information is a special bird
            if (_configuration.ProcurementObject.Defence.TimeFrame.ScheduledStartDateOfAwardProcedures && defObject?.TimeFrame != null)
            {
                yield return Element("SCHEDULED_DATE_PERIOD",
                    DateElement("PROCEDURE_DATE_STARTING", defObject?.TimeFrame?.ScheduledStartDateOfAwardProcedures),
                    defObject.TimeFrame.Type != TimeFrameType.Undefined ? Element("PERIOD_WORK_DATE_STARTING", Duration(defObject.TimeFrame)) : null);
            }

            // Append time limit to prior info (doesn't exist in form, but was wanted in Hilma)
            if (_notice.Type == NoticeType.DefencePriorInformation && _notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime != null)
            {
                var translations = _translationProvider.GetDynamicObject(CancellationToken.None).Result;
                defObject.AdditionalInformation = defObject.AdditionalInformation.Append(
                    $"{translations[_notice.Language.ToLongLang()]["limit_applications_tenders"]}: {_notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime.Value.ToString("d.M.yyyy HH:mm")}").ToArray();
            }

            if (configuration.Defence.AdditionalInformation)
            {
                yield return PElement("ADDITIONAL_INFORMATION", defObject.AdditionalInformation);
            }
        }

        private XElement ScopeAndOptions()
        {
            var defObj = _notice.ProcurementObject.Defence;

            if (defObj == null)
            {
                return null;
            }

            var renewals = defObj.Renewals == null ? null :
                new List<XElement>() {
                    defObj.Renewals.Amount?.Type == ContractValueType.Range ?
                        Element("NUMBER_POSSIBLE_RENEWALS_RANGE",
                            Element("NUMBER_POSSIBLE_RENEWALS_RANGE_MIN", (int)defObj.Renewals.Amount?.MinValue.GetValueOrDefault()),
                            Element("NUMBER_POSSIBLE_RENEWALS_RANGE_MAX", (int)defObj.Renewals.Amount?.MaxValue.GetValueOrDefault())) :
                    defObj.Renewals.Amount?.Type == ContractValueType.Exact ?
                        Element("NUMBER_POSSIBLE_RENEWALS", (int)defObj.Renewals.Amount?.Value.GetValueOrDefault()) : null,
                    defObj.Renewals.SubsequentContract?.Type == TimeFrameType.Days ?
                        Element("TIME_FRAME_SUBSEQUENT_CONTRACTS_DAY", defObj.Renewals.SubsequentContract?.Days) :
                    defObj.Renewals.SubsequentContract?.Type == TimeFrameType.Months ?
                        Element("TIME_FRAME_SUBSEQUENT_CONTRACTS_MONTH", defObj.Renewals.SubsequentContract?.Months) : null};

            return Element("QUANTITY_SCOPE",
                    Element("NATURE_QUANTITY_SCOPE",
                        PElement("TOTAL_QUANTITY_OR_SCOPE", defObj.TotalQuantity),
                        ElementWithAttribute("COSTS_RANGE_AND_CURRENCY", "CURRENCY", defObj.TotalQuantityOrScope?.Currency,
                                                defObj.TotalQuantityOrScope.Type == ContractValueType.Range ?
                                                Element("RANGE_VALUE_COST", Element("LOW_VALUE", defObj.TotalQuantityOrScope?.MinValue), Element("HIGH_VALUE", defObj.TotalQuantityOrScope?.MaxValue)) :
                                                Element("VALUE_COST", defObj.TotalQuantityOrScope?.Value))),
                    defObj.OptionsAndVariants.Options ?
                        Element("OPTIONS",
                            PElement("OPTION_DESCRIPTION", defObj.OptionsAndVariants?.OptionsDescription),
                            defObj.OptionsAndVariants?.OptionType == TimeFrameType.Months ?
                                Element("PROVISIONAL_TIMETABLE_MONTH", defObj.OptionsAndVariants?.OptionsMonths) :
                                Element("PROVISIONAL_TIMETABLE_DAY", defObj.OptionsAndVariants?.OptionsDays))
                        : Element("NO_OPTIONS"),
                    defObj.Renewals.CanBeRenewed ?
                        renewals != null && renewals.Any(x => x != null) ? Element("RECURRENT_CONTRACT", renewals) : Element("RECURRENT_CONTRACT")
                             :
                        Element("NO_RECURRENT_CONTRACT"));
        }

        private XElement SubContracting(SubcontractingInformation subcontract)
        {
            if (subcontract == null)
            {
                return null;
            }

            return Element("SUBCONTRACTING",
                     ElementWithAttribute("INDICATE_ANY_SHARE", "VALUE", subcontract.TendererHasToIndicateShare ? "YES" : "NO"),
                     ElementWithAttribute("INDICATE_ANY_CHANGE", "VALUE", subcontract.TendererHasToIndicateChange ? "YES" : "NO"),
                     ElementWithAttribute("SUBCONTRACT_AWARD_PART", "VALUE", subcontract.CaMayOblige ? "YES" : "NO"),
                    subcontract.SuccessfulTenderer ?
                        Element("SUBCONTRACT_SHARE",
                            Element("MIN_PERCENTAGE", subcontract.SuccessfulTendererMin),
                            Element("MAX_PERCENTAGE", subcontract.SuccessfulTendererMax)
                        ) : null,
                    ElementWithAttribute("IDENTIFY_SUBCONTRACT", "VALUE", subcontract.SuccessfulTendererToSpecify ? "YES" : "NO")
                    );
        }

        private XElement FrameworkAgreementType(FrameworkAgreementInformation agreement)
        {
            if (agreement == null)
            {
                return null;
            }


            if (_notice.Type == NoticeType.DefencePriorInformation)
            {
                return ElementWithAttribute("FRAMEWORK_AGREEMENT", "VALUE", agreement.IncludesFrameworkAgreement ? "YES" : "NO");
            }
            if (_notice.Type == NoticeType.DefenceContract && agreement.IncludesFrameworkAgreement)
            {
                return ElementWithAttribute("NOTICE_INVOLVES_DEFENCE", "VALUE", "ESTABLISHMENT_FRAMEWORK_AGREEMENT");
            }
            if (_notice.Type == NoticeType.DefenceContractAward && agreement.IncludesConclusionOfFrameworkAgreement)
            {
                return ElementWithAttribute("NOTICE_INVOLVES_DESC_DEFENCE", "VALUE", "CONCLUSION_FRAMEWORK_AGREEMENT");
            }


            return null;
        }

        private XElement FrameworkAgreement(FrameworkAgreementInformation agreement)
        {
            if (agreement == null)
                return null;

            var config = _configuration.ProcurementObject.Defence.FrameworkAgreement;

            if (config.FrameworkAgreementType && agreement.FrameworkAgreementType != Entities.FrameworkAgreementType.Undefined)
            {
                var frameworkOperators = agreement.FrameworkAgreementType == Entities.FrameworkAgreementType.FrameworkSeveral ?
                       SeveralOperators(agreement) :
                       Element("SINGLE_OPERATOR");


                return Element("F17_FRAMEWORK",
                       frameworkOperators,
                       agreement.Duration?.Type == TimeFrameType.Years ?
                          Element("DURATION_FRAMEWORK_YEAR", agreement.Duration.Years) :
                          Element("DURATION_FRAMEWORK_MONTH", agreement.Duration.Months),
                       PElement("JUSTIFICATION", agreement.JustificationForDurationOverSevenYears),

                       agreement.EstimatedTotalValue?.Type == ContractValueType.Range ?
                       Element("TOTAL_ESTIMATED",
                       ElementWithAttribute("COSTS_RANGE_AND_CURRENCY", "CURRENCY", agreement.EstimatedTotalValue?.Currency,
                       agreement.EstimatedTotalValue?.Type == ContractValueType.Range ?
                       Element("RANGE_VALUE_COST", Element("LOW_VALUE", agreement.EstimatedTotalValue?.MinValue), Element("HIGH_VALUE", agreement.EstimatedTotalValue?.MaxValue)) :
                       Element("VALUE_COST", agreement.EstimatedTotalValue.Value)),

                       PElement("FREQUENCY_AWARDED_CONTRACTS", agreement.FrequencyAndValue)) :
                       null

                       );
            }

            return null;
        }

        private static XElement SeveralOperators(FrameworkAgreementInformation agreement)
        {
            var several = Element("SEVERAL_OPERATORS", agreement.FrameworkEnvisagedType == FrameworkEnvisagedType.FrameworkEnvisagedExact ?
                                   Element("NUMBER_PARTICIPANTS", agreement.EnvisagedNumberOfParticipants) :
                                   Element("MAX_NUMBER_PARTICIPANTS", agreement.EnvisagedNumberOfParticipants));

            return several ?? Element("SEVERAL_OPERATORS");
        }

        private XElement TypeContract(ProcurementProjectContract project)
        {
            switch (project.ContractType)
            {
                case ContractType.Works:
                    return Element("TYPE_WORK_CONTRACT",
                        project.DefenceWorks.HasFlag(Works.Design) ? Element("DESIGN_EXECUTION") : null,
                        project.DefenceWorks.HasFlag(Works.Execution) ? Element("EXECUTION") : null,
                        project.DefenceWorks.HasFlag(Works.Realisation) ? Element("REALISATION_REQUIREMENTS_SPECIFIED_CONTRACTING_AUTHORITIES") : null);
                case ContractType.Supplies:
                    return ElementWithAttribute("TYPE_SUPPLIES_CONTRACT", "VALUE", project.DefenceSupplies.ToTEDFormat());
                case ContractType.Services:
                    if (_notice.Type == NoticeType.DefenceContractAward)
                    {
                        var codeInt = int.Parse(project.DefenceCategory?.Code ?? "0");
                        return Element("SERVICE_CATEGORY_PUB_DEFENCE", codeInt > 20 ? project.DefenceCategory.Code + (project.DisagreeToPublishNoticeBasedOnDefenceServiceCategory4 == true ? "N" : "Y") : project.DefenceCategory.Code);
                    }
                    else
                    {
                        return Element("SERVICE_CATEGORY_DEFENCE", project.DefenceCategory?.Code);
                    }
            }

            return null;
        }

        private XElement LotDivision(LotsInfo lotsInfo)
        {
            var configuration = _configuration.LotsInfo;
            var rootElement = "F16";
            if (_notice.Type == NoticeType.DefenceContract)
            {
                rootElement = "F17";
            }
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                rootElement = "F18";
            }
            if (!configuration.DivisionLots || !lotsInfo.DivisionLots || lotsInfo.QuantityOfLots == 0)
            {
                return Element(rootElement + "_DIVISION_INTO_LOTS", Element("DIV_INTO_LOT_NO"));
            }

            return Element(rootElement + "_DIVISION_INTO_LOTS",
                    configuration.LotsSubmittedFor ?
                    ElementWithAttribute(rootElement + "_DIV_INTO_LOT_YES", "VALUE", LotTendersMayBeSubmittedFor(lotsInfo.LotsSubmittedFor),
                        ObjectDescriptions(_notice.ObjectDescriptions)) :
                    Element(rootElement + "_DIV_INTO_LOT_YES",
                        ObjectDescriptions(_notice.ObjectDescriptions)));

        }

        private string LotTendersMayBeSubmittedFor(LotsSubmittedFor lotsInfoLotsSubmittedFor)
        {
            switch (lotsInfoLotsSubmittedFor)
            {
                case LotsSubmittedFor.LotsAll:
                    return "ALL_LOTS";
                case LotsSubmittedFor.LotsMax:
                    return "ONE_OR_MORE_LOT";
                case LotsSubmittedFor.LotOneOnly:
                    return "ONE_LOT_ONLY";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Lot
        /// </summary>
        private XElement ObjectDescription(int index, ObjectDescription objectDescription)
        {
            var config = _configuration.ObjectDescriptions;

            var rootElement = "LOT_PRIOR_INFORMATION";
            var additionalElement = "ADDITIONAL_INFORMATION";
            if (_notice.Type == NoticeType.DefenceContract)
            {
                rootElement = "F17_ANNEX_B";
                additionalElement = "ADDITIONAL_INFORMATION_ABOUT_LOTS";
            }

            var cpvCodes = objectDescription.MainCpvCode != null ? new CpvCode[] { objectDescription.MainCpvCode } : new CpvCode[0];

            return Element(rootElement, new XAttribute("ITEM", index + 1),
                config.LotNumber ? Element("LOT_NUMBER", objectDescription.LotNumber) : null,
                config.Title ? Element("LOT_TITLE", objectDescription.Title) : null,
                config.DescrProcurement ? PElement("LOT_DESCRIPTION", objectDescription.DescrProcurement) : null,
                config.MainCpvCode.Code ? Element("CPV",
                                            CpvCodeElement("CPV_MAIN", cpvCodes),
                                            CpvCodeElement("CPV_ADDITIONAL", objectDescription.AdditionalCpvCodes)) : null,
                config.QuantityOrScope && objectDescription.EstimatedValue.Type != ContractValueType.Undefined ?
                                        Element("NATURE_QUANTITY_SCOPE",
                                            PElement("TOTAL_QUANTITY_OR_SCOPE", objectDescription.QuantityOrScope),
                                            ElementWithAttribute("COSTS_RANGE_AND_CURRENCY", "CURRENCY", objectDescription.EstimatedValue?.Currency,
                                                objectDescription.EstimatedValue.Type == ContractValueType.Range ?
                                                Element("RANGE_VALUE_COST",
                                                    Element("LOW_VALUE", objectDescription.EstimatedValue?.MinValue),
                                                    Element("HIGH_VALUE", objectDescription.EstimatedValue?.MaxValue)) :
                                                Element("VALUE_COST", objectDescription.EstimatedValue?.Value))) : null,

                // Prior information is a special bird
                config.TimeFrame.ScheduledStartDateOfAwardProcedures ?
                    Element("SCHEDULED_DATE_PERIOD",
                        DateElement("PROCEDURE_DATE_STARTING", objectDescription.TimeFrame?.ScheduledStartDateOfAwardProcedures),
                        objectDescription?.TimeFrame?.Type != TimeFrameType.Undefined ?
                            Element("PERIOD_WORK_DATE_STARTING", Duration(objectDescription.TimeFrame)) : null)
                    : null,
                PElement(additionalElement, objectDescription.AdditionalInformation)
              );
        }

        private IEnumerable<XElement> ObjectDescriptions(ObjectDescription[] objectDescriptions)
        {
            return objectDescriptions.Select((description, i) => ObjectDescription(i, description));
        }

        private XElement Duration(TimeFrame timeFrame)
        {
            switch (timeFrame.Type)
            {
                case TimeFrameType.Days:
                    return Element("DAYS", timeFrame.Days);
                case TimeFrameType.Months:
                    return Element("MONTHS", timeFrame.Months);
                case TimeFrameType.BeginAndEndDate:
                    return Element("INTERVAL_DATE",
                            DateElement("START_DATE", timeFrame.BeginDate),
                            DateElement("END_DATE", timeFrame.EndDate)
                            );
                default:
                    throw new ArgumentOutOfRangeException(nameof(timeFrame.Type), "Undefined is not allowed value");
            }
        }


        private IEnumerable<XElement> NutsCodes(string[] nutsCodes)
        {
            if (nutsCodes == null)
            {
                return null;
            }
            return nutsCodes.Select(n => ElementWithAttribute("NUTS", "CODE", n));
        }

        private static XElement Criterion(string name, AwardCriterionDefinition qualityCriterion)
        {
            return Element(name,
                Element("AC_CRITERION", qualityCriterion.Criterion),
                Element("AC_WEIGHTING", qualityCriterion.Weighting)
            );
        }
        #endregion

        #region Section III: Legal, economic, financial and technical information
        /// <summary>
        /// Section III: Legal, economic, financial and technical information
        /// </summary>
        /// <returns>Conditions element</returns>
        public XElement ConditionsInformation(ConditionsInformationDefence conditions)
        {
            var config = _configuration.ConditionsInformationDefence;


            if (config == null || conditions == null)
            {
                return null;
            }

            var section3 = Element("LEFTI_" + _noticeType + (_notice.Type == NoticeType.DefenceContract ? "_DEFENCE" : null));

            // Can't use config, because directive is crap.
            if (_notice.Type == NoticeType.DefencePriorInformation)
            {
                section3.Add(PElement("MAIN_FINANCING_CONDITIONS", conditions.FinancingConditions));
                section3.Add(Element("RESERVED_CONTRACTS",
                     conditions.RestrictedToShelteredWorkshops ? Element("RESTRICTED_TO_SHELTERED_WORKSHOPS") : null,
                     conditions.RestrictedToShelteredProgrammes ? Element("RESTRICTED_TO_FRAMEWORK") : null
                     ));

                return section3;
            }

            // III.1) Conditions relating to the contract
            var conditionsElement = Element("CONTRACT_RELATING_CONDITIONS");

            if (config.DepositsRequired)
            {
                conditionsElement.Add(PElement("DEPOSITS_GUARANTEES_REQUIRED", conditions.DepositsRequired));
            }

            conditionsElement.Add(PElement("MAIN_FINANCING_CONDITIONS", conditions.FinancingConditions));

            if (config.LegalFormTaken)
            {
                conditionsElement.Add(PElement("LEGAL_FORM", conditions.LegalFormTaken));
            }

            if (!conditions.OtherParticularConditions.HasAnyContent())
            {
                conditionsElement.Add(Element("NO_EXISTENCE_OTHER_PARTICULAR_CONDITIONS"));
            }
            else
            {
                conditionsElement.Add(PElement("EXISTENCE_OTHER_PARTICULAR_CONDITIONS", conditions.OtherParticularConditions));
            }

            conditionsElement.Add(DateElement("CLEARING_LAST_DATE", conditions.SecurityClearanceDate));

            section3.Add(conditionsElement);


            // Section III.2) Conditions for participation
            var participationElement = Element("F17_CONDITIONS_FOR_PARTICIPATION");

            if (config.PersonalSituationOfEconomicOperators)
            {
                // III.2.1
                participationElement.Add(PElement("ECONOMIC_OPERATORS_PERSONAL_SITUATION", conditions.PersonalSituationOfEconomicOperators));
                participationElement.Add(PElement("ECONOMIC_OPERATORS_PERSONAL_SITUATION_SUBCONTRACTORS", conditions.PersonalSituationOfSubcontractors));

                // III.2.2
                participationElement.Add(Element("F17_ECONOMIC_FINANCIAL_CAPACITY",
                                            PElement("EAF_CAPACITY_INFORMATION", conditions.EconomicCriteriaOfEconomicOperators),
                                            PElement("EAF_CAPACITY_MIN_LEVEL", conditions.EconomicCriteriaOfEconomicOperatorsMinimum)));
                participationElement.Add(Element("F17_ECONOMIC_FINANCIAL_CAPACITY_SUBCONTRACTORS",
                                            PElement("EAF_CAPACITY_INFORMATION", conditions.EconomicCriteriaOfSubcontractors),
                                            PElement("EAF_CAPACITY_MIN_LEVEL", conditions.EconomicCriteriaOfSubcontractorsMinimum)));

                // III.2.3
                participationElement.Add(Element("TECHNICAL_CAPACITY_LEFTI",
                                            PElement("T_CAPACITY_INFORMATION", conditions.TechnicalCriteriaOfEconomicOperators),
                                            PElement("T_CAPACITY_MIN_LEVEL", conditions.TechnicalCriteriaOfEconomicOperatorsMinimum)));
                participationElement.Add(Element("TECHNICAL_CAPACITY_LEFTI_SUBCONTRACTORS",
                                            PElement("T_CAPACITY_INFORMATION", conditions.TechnicalCriteriaOfSubcontractors),
                                            PElement("T_CAPACITY_MIN_LEVEL", conditions.TechnicalCriteriaOfSubcontractorsMinimum)));

                // III.2.4
                participationElement.Add(Element("RESERVED_CONTRACTS",
                    conditions.RestrictedToShelteredWorkshops ? Element("RESTRICTED_TO_SHELTERED_WORKSHOPS") : null,
                    conditions.RestrictedToShelteredProgrammes ? Element("RESTRICTED_TO_FRAMEWORK") : null
                    ));

                section3.Add(participationElement);
            }

            // Section III.3) Conditions specific to services contracts
            var serviceContractsElement = Element("SERVICES_CONTRACTS_SPECIFIC_CONDITIONS");

            if (config.RestrictedToParticularProfession)
            {
                if (conditions.RestrictedToParticularProfession)
                {
                    serviceContractsElement.Add(PElement("EXECUTION_SERVICE_RESERVED_PARTICULAR_PROFESSION", conditions.RestrictedToParticularProfessionLaw));
                }
                else
                {
                    serviceContractsElement.Add(Element("NO_EXEC_SERVICE_RESERVED_PARTICULAR_PROFESSION"));
                }

                serviceContractsElement.Add(
                    ElementWithAttribute("REQUESTS_NAMES_PROFESSIONAL_QUALIFICATIONS", "VALUE", conditions.StaffResponsibleForExecution ? "YES" : "NO"));

                section3.Add(serviceContractsElement);
            }

            return section3;
        }
        #endregion

        #region Section IV: Procedure
        /// <summary>
        /// Section IV: Procedure
        /// </summary>
        /// <returns>Procedure element</returns>
        public XElement Procedure(ProcedureInformation procedureInformation)
        {
            if (procedureInformation == null)
            {
                return null;
            }

            var procedureInformationDefence = procedureInformation.Defence;
            if (procedureInformationDefence == null)
            {
                return null;
            }


            var configuration = _configuration.ProcedureInformation;
            var tenderConfig = _configuration.TenderingInformation;

            if (configuration == null)
            {
                return null;
            }

            var noticeType = "CONTRACT_NOTICE";
            var form = "F17";
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                noticeType = "CONTRACT_AWARD";
                form = "F18";
            }

            var type = procedureInformation.ProcedureType;

            // IV.1) Type of procedure
            var procedureType = TypeOfProcedureContract(procedureInformation, noticeType, type, form);

            // AWARD_CRITERIA_CONTRACT_AWARD_NOTICE_INFORMATION_DEFENCE/F18_IS_ELECTRONIC_AUCTION_USABLE/@VALUE
            // IV.2) Award criteria
            XElement awardCriteria = null;

            if (procedureInformationDefence.AwardCriteria?.CriterionTypes != AwardCriterionTypeDefence.Undefined)
            {
                if (_notice.Type == NoticeType.DefenceContractAward)
                {
                    awardCriteria = Element("AWARD_CRITERIA_CONTRACT_AWARD_NOTICE_INFORMATION_DEFENCE",
                        Element("AWARD_CRITERIA_DETAIL_F18",
                           procedureInformationDefence.AwardCriteria?.CriterionTypes == AwardCriterionTypeDefence.LowestPrice ?
                           Element("LOWEST_PRICE") :
                           Element("MOST_ECONOMICALLY_ADVANTAGEOUS_TENDER_SHORT",
                            procedureInformationDefence.AwardCriteria?.Criteria?.Select((x, i) => Element("CRITERIA_DEFINITION", Element("ORDER_C", i + 1), Element("CRITERIA", x.Criterion), Element("WEIGHTING", x.Weighting))))),
                        ElementWithAttribute("F18_IS_ELECTRONIC_AUCTION_USABLE", "VALUE", procedureInformation.ElectronicAuctionWillBeUsed ? "YES" : "NO"));

                }
                else
                {
                    awardCriteria = Element("AWARD_CRITERIA_CONTRACT_NOTICE_INFORMATION",
                    Element("AWARD_CRITERIA_DETAIL",
                       procedureInformationDefence.AwardCriteria?.CriterionTypes == AwardCriterionTypeDefence.LowestPrice ?
                       Element("LOWEST_PRICE") : Element("MOST_ECONOMICALLY_ADVANTAGEOUS_TENDER",
                        procedureInformationDefence.AwardCriteria?.EconomicCriteriaTypes == AwardCriterionTypeDefence.CriteriaBelow ?
                        Element("CRITERIA_STATED_BELOW",
                            procedureInformationDefence.AwardCriteria?.Criteria?.Select((x, i) => Element("CRITERIA_DEFINITION", Element("ORDER_C", i + 1), Element("CRITERIA", x.Criterion), Element("WEIGHTING", x.Weighting))))
                        : Element("CRITERIA_STATED_IN_OTHER_DOCUMENT"))),
                    Element("IS_ELECTRONIC_AUCTION_USABLE", procedureInformation.ElectronicAuctionWillBeUsed ? PElement("USE_ELECTRONIC_AUCTION", procedureInformation.AdditionalInformationAboutElectronicAuction) : Element("NO_USE_ELECTRONIC_AUCTION")));

                }
            }

            // IV.3) Administrative information
            var defenceAdministrativeInformation = _notice.TenderingInformation?.Defence;
            if (defenceAdministrativeInformation == null)
            {
                return null;
            }

            var administrativeInfo = Element("ADMINISTRATIVE_INFORMATION_" + noticeType + "_DEFENCE",
                PElement("FILE_REFERENCE_NUMBER", _notice.Project?.ReferenceNumber),
                Element("PREVIOUS_PUBLICATION_INFORMATION_NOTICE_" + form,
                    !defenceAdministrativeInformation.PreviousPublicationExists ? Element("NO_PREVIOUS_PUBLICATION_EXISTS_" + form) :
                    Element("PREVIOUS_PUBLICATION_EXISTS_" + form,
                        _configuration.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber.Number
                        ? Element("PREVIOUS_PUBLICATION_NOTICE_" + form,
                            ElementWithAttribute("PRIOR_INFORMATION_NOTICE_" + form, "CHOICE", "PRIOR_INFORMATION_NOTICE"),
                            Element("NOTICE_NUMBER_OJ", defenceAdministrativeInformation.PreviousPriorInformationNoticeOjsNumber?.Number),
                            DateElement("DATE_OJ", defenceAdministrativeInformation.PreviousPriorInformationNoticeOjsNumber?.Date)) : null,
                        defenceAdministrativeInformation.HasPreviousContractNoticeOjsNumber && _configuration.TenderingInformation.Defence.HasPreviousContractNoticeOjsNumber
                        ? Element("CNT_NOTICE_INFORMATION_" + form,
                            ElementWithAttribute("CNT_NOTICE_INFORMATION_S_" + form, "VALUE", "CONTRACT_NOTICE"),
                            Element("NOTICE_NUMBER_OJ", defenceAdministrativeInformation.PreviousContractNoticeOjsNumber?.Number),
                            DateElement("DATE_OJ", defenceAdministrativeInformation.PreviousContractNoticeOjsNumber?.Date)) : null,
                        defenceAdministrativeInformation.HasPreviousExAnteOjsNumber && _configuration.TenderingInformation.Defence.HasPreviousExAnteOjsNumber
                        ? Element("EX_ANTE_NOTICE_INFORMATION",
                            ElementWithAttribute("EX_ANTE_NOTICE_INFORMATION_S", "VALUE", "VOLUNTARY_EX_ANTE_TRANSPARENCY_NOTICE"),
                            Element("NOTICE_NUMBER_OJ", defenceAdministrativeInformation.PreviousExAnteOjsNumber?.Number),
                            DateElement("DATE_OJ", defenceAdministrativeInformation.PreviousExAnteOjsNumber?.Date)) : null,
                        Element("NO_OTHER_PREVIOUS_PUBLICATION"))),
                _notice.ProcedureInformation.ProcedureType == ProcedureType.ProctypeCompDialogue ?
                    Element("CONDITIONS_OBTAINING_SPECIFICATIONS",
                        DateElement("TIME_LIMIT", defenceAdministrativeInformation.TimeLimitForReceipt),
                        !defenceAdministrativeInformation.PayableDocuments ? Element("NO_PAYABLE_DOCUMENTS") :
                        Element("PAYABLE_DOCUMENTS",
                            ElementWithAttribute("DOCUMENT_COST", "CURRENCY", defenceAdministrativeInformation.DocumentPrice?.Currency, defenceAdministrativeInformation.DocumentPrice?.Value),
                            PElement("DOCUMENT_METHOD_OF_PAYMENT", defenceAdministrativeInformation.PaymentTermsAndMethods))) :
                            null,

                tenderConfig.TendersOrRequestsToParticipateDueDateTime ? DateTimeElement("RECEIPT_LIMIT_DATE", _notice.TenderingInformation?.TendersOrRequestsToParticipateDueDateTime) : null,

                tenderConfig.EstimatedDateOfInvitations ? DateElement("DISPATCH_INVITATIONS_DATE", _notice.TenderingInformation?.EstimatedDateOfInvitations) : null,

                tenderConfig.Defence.Languages ? Element("LANGUAGE",
                    new List<XElement>() {
                    defenceAdministrativeInformation.LanguageType == LanguageType.AnyOfficialEu ? ElementWithAttribute("LANGUAGE_ANY_EC", "VALUE", "YES") : null },
                    defenceAdministrativeInformation.LanguageType == LanguageType.SelectedEu ? defenceAdministrativeInformation.Languages?.Select(x => ElementWithAttribute("LANGUAGE_EC", "VALUE", x)).ToList() : null,
                    defenceAdministrativeInformation.OtherLanguage ? PElement("LANGUAGE_OTHER", defenceAdministrativeInformation.OtherLanguages) : null
                    ) : null);

            return Element("PROCEDURE_DEFINITION_" + noticeType + (_notice.Type == NoticeType.DefenceContractAward ? "_NOTICE" : null) + "_DEFENCE",
                    procedureType,
                    awardCriteria,
                    administrativeInfo);
        }

        private XElement TypeOfProcedureContract(ProcedureInformation procedureInformation, string noticeType, ProcedureType type, string form)
        {
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                if (procedureInformation == null)
                    return null;

                var procedure = Element("TYPE_OF_PROCEDURE_CONTRACT_AWARD_DEFENCE");

                switch (type)
                {
                    case ProcedureType.ProctypeRestricted when procedureInformation.AcceleratedProcedure:
                        procedure.Add(Element("PT_ACCELERATED_RESTRICTED"));
                        break;
                    case ProcedureType.ProctypeNegotiation when procedureInformation.AcceleratedProcedure:
                        procedure.Add(Element("PT_ACCELERATED_NEGOTIATED"));
                        break;
                    case ProcedureType.ProctypeRestricted:
                        procedure.Add(Element("PT_RESTRICTED"));
                        break;
                    case ProcedureType.ProctypeCompDialogue:
                        procedure.Add(Element("PT_COMPETITIVE_DIALOGUE"));
                        break;
                    case ProcedureType.ProctypeNegotiation:
                        procedure.Add(Element("PT_NEGOTIATED_WITH_PUBLICATION_CONTRACT_NOTICE"));
                        break;
                    case ProcedureType.AwardWoPriorPubD1:
                    case ProcedureType.AwardWoPriorPubD1Other:
                        procedure.Add(Element("F18_PT_NEGOTIATED_WITHOUT_PUBLICATION_CONTRACT_NOTICE", Element("ANNEX_D", _annexHelper.SelectAnnexD())));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                return procedure;
            }
            else
            {
                return Element("TYPE_OF_PROCEDURE_DEFENCE",
                    Element("TYPE_OF_PROCEDURE_DETAIL_FOR_" + noticeType + "_DEFENCE",
                        procedureInformation.AcceleratedProcedure && type == ProcedureType.ProctypeRestricted ?
                            Element("PT_ACCELERATED_RESTRICTED_CHOICE",
                                PElement("PTAR_JUSTIFICATION", procedureInformation.JustificationForAcceleratedProcedure)) :
                            procedureInformation.AcceleratedProcedure && type == ProcedureType.ProctypeNegotiation ?
                                Element(form + "_PT_ACCELERATED_NEGOTIATED",
                                    PElement("PTAN_JUSTIFICATION", procedureInformation.JustificationForAcceleratedProcedure)) :
                                Element(procedureInformation.ProcedureType.ToTEDFormat())
                    ),
                    procedureInformation.Defence?.CandidateNumberRestrictions?.Selected != EnvisagedParticipantsOptions.Undefined ?
                        Element("MAXIMUM_NUMBER_INVITED",
                            Candidates(procedureInformation.Defence?.CandidateNumberRestrictions)) : null,
                    ElementWithAttribute("REDUCTION_OF_THE_NUMBER", "VALUE", procedureInformation.ReductionRecourseToReduceNumberOfSolutions ? "YES" : "NO")
                );
            }
        }

        private IEnumerable<XElement> Candidates(CandidateNumberRestrictions candidates)
        {
            if (candidates == null)
                yield break;

            switch (candidates.Selected)
            {
                case EnvisagedParticipantsOptions.EnvisagedNumber:
                    yield return Element("OPE_ENVISAGED_NUMBER", candidates.EnvisagedNumber);
                    break;
                case EnvisagedParticipantsOptions.Range:
                    yield return Element("OPE_MINIMUM_NUMBER", candidates.EnvisagedMinimumNumber);
                    yield return Element("OPE_MAXIMUM_NUMBER", candidates.EnvisagedMaximumNumber);
                    break;
                default:
                    yield break;
            }

            yield return PElement("OPE_OBJECTIVE_CRITERIA", candidates.ObjectiveCriteriaForChoosing);
        }
        #endregion

        #region Section V: Contract Award
        internal IEnumerable<XElement> ContractAward(ContractAwardDefence[] awards)
        {
            if (awards == null)
                return Enumerable.Empty<XElement>();

            return awards.Select((award, i) => Element("AWARD_OF_CONTRACT_DEFENCE",
                Element("CONTRACT_NUMBER", award.ContractNumber),
                _notice.LotsInfo.DivisionLots ? Element("LOT_NUMBER", award.LotNumber) : null,
                PElement("CONTRACT_TITLE", award.LotTitle),

                DateElement("CONTRACT_AWARD_DATE", award.ContractAwardDecisionDate),

                award.NumberOfTenders?.Total != 0 ?
                Element("OFFERS_RECEIVED_NUMBER", award.NumberOfTenders?.Total) : null,

                award.NumberOfTenders?.Electronic != 0 ?
                Element("OFFERS_RECEIVED_NUMBER_MEANING", award.NumberOfTenders?.Electronic) : null,

                Element("ECONOMIC_OPERATOR_NAME_ADDRESS",
                    INC_05(award.Contractor)),

                // V.4

                Element("CONTRACT_VALUE_INFORMATION",
                    award.EstimatedValue?.Value != null ?
                        ElementWithAttribute("INITIAL_ESTIMATED_TOTAL_VALUE_CONTRACT", "CURRENCY", award.EstimatedValue?.Currency,
                            Element("VALUE_COST", award.EstimatedValue?.Value),
                            Element("EXCLUDING_VAT")) : null,

                    award.FinalTotalValue?.Value != null || award.LowestOffer?.Value != null ?
                        ElementWithAttribute("COSTS_RANGE_AND_CURRENCY_WITH_VAT_RATE", "CURRENCY", award.ContractValueType == ContractValueType.Range ? award.LowestOffer?.Currency : award.FinalTotalValue?.Currency,
                        award.ContractValueType == ContractValueType.Range ?
                            Element("RANGE_VALUE_COST",
                                Element("LOW_VALUE", award.LowestOffer?.Value),
                                Element("HIGH_VALUE", award.HighestOffer?.Value)) :
                            Element("VALUE_COST", award.FinalTotalValue?.Value),
                            Element("EXCLUDING_VAT")) : null,

                    award.AnnualOrMonthlyValue.Type != TimeFrameType.Undefined ?
                        Element("MORE_INFORMATION_IF_ANNUAL_MONTHLY",
                            award.AnnualOrMonthlyValue.Type == TimeFrameType.Months ?
                                Element("NUMBER_OF_MONTHS", award.AnnualOrMonthlyValue?.Months) :
                                Element("NUMBER_OF_YEARS", award.AnnualOrMonthlyValue?.Years)) : null),


                // V.5
                Element("MORE_INFORMATION_TO_SUB_CONTRACTED",
                    award.LikelyToBeSubcontracted ?
                            Element("CONTRACT_LIKELY_SUB_CONTRACTED_WITH_DEFENCE", award.ValueOfSubcontractNotKnown ? Element("UNKNOWN_VALUE") :
                                ElementWithAttribute("EXCLUDING_VAT_VALUE", "CURRENCY", award.ValueOfSubcontract.Currency, award.ValueOfSubcontract.Value),
                                Element("EXCLUDING_VAT_PRCT", award.ProportionOfValue),
                                PElement("ADDITIONAL_INFORMATION", award.SubcontractingDescription),
                                Element("SUBCONTRACT_DEFENCE",
                                    ElementWithAttribute("SUBCONTRACT_AWARD_PART", "VALUE", award.AllOrCertainSubcontractsWillBeAwarded ? "YES" : "NO"),
                                    award.ShareOfContractWillBeSubcontracted ?
                                    Element("SUBCONTRACT_SHARE",
                                        Element("MIN_PERCENTAGE", award.ShareOfContractWillBeSubcontractedMinPercentage),
                                        Element("MAX_PERCENTAGE", award.ShareOfContractWillBeSubcontractedMaxPercentage)
                                    )
                            : null))
                            : Element("NO_CONTRACT_LIKELY_SUB_CONTRACTED")
                    )
                ));
        }
        #endregion

        #region Section VI: Complementary information

        /// <summary>
        /// Section VI: Complementary information
        /// </summary>
        /// <returns>The complementary section</returns>
        public XElement ComplementaryInformation()
        {
            var rootElement = "COMPLEMENTARY_INFORMATION_CONTRACT_NOTICE";
            if (_notice.Type == NoticeType.DefenceContractAward)
            {
                rootElement = "COMPLEMENTARY_INFORMATION_CONTRACT_AWARD";
            }
            if (_notice.Type == NoticeType.DefencePriorInformation)
            {
                rootElement = "OTH_INFO_PRIOR_INFORMATION";
            }

            var complementaryInfo = Element(rootElement);
            var config = _configuration.ComplementaryInformation;
            var compl = _notice.ComplementaryInformation;

            if (config.IsRecurringProcurement && compl.IsRecurringProcurement)
            {
                complementaryInfo.Add(PElement("RECURRENT_PROCUREMENT", compl.EstimatedTimingForFurtherNoticePublish));
            }
            else if (config.IsRecurringProcurement)
            {
                complementaryInfo.Add(Element("NO_RECURRENT_PROCUREMENT"));
            }

            var complDefence = compl.Defence;
            if (complDefence == null)
                return complementaryInfo;

            if (config.Defence.EuFunds.ProcurementRelatedToEuProgram)
            {
                complementaryInfo.Add(complDefence?.EuFunds?.ProcurementRelatedToEuProgram ?? false ?
                    PElement("RELATES_TO_EU_PROJECT_YES", complDefence.EuFunds?.ProjectIdentification) : Element("RELATES_TO_EU_PROJECT_NO"));
            }

            if (config.AdditionalInformation)
            {
                string[] additionalInformation = compl.AdditionalInformation;
                if (_notice.HasAttachments)
                {
                    var translations = _translationProvider.GetDynamicObject(CancellationToken.None).Result;
                    string attachmentInfoText = translations != null ? translations[_notice.Language.ToLongLang()]["TED_noticeHasAttachments"] :
                        "This notice has links and/or attachments listed in hankintailmoitukset.fi";

                    attachmentInfoText = attachmentInfoText.Replace("{procurementProjectId}", _notice.ProcurementProjectId.ToString());
                    attachmentInfoText = attachmentInfoText.Replace("{noticeId}", _notice.Id.ToString());

                    Array.Resize(ref additionalInformation, additionalInformation.Length + 1);
                    additionalInformation[additionalInformation.Length - 1] = attachmentInfoText;
                }
                complementaryInfo.Add(PElement("ADDITIONAL_INFORMATION", additionalInformation));
            }

            if (_configuration.ProceduresForReview != null && _configuration.ProceduresForReview.ReviewBody.OfficialName)
            {
                complementaryInfo.Add(Element("PROCEDURES_FOR_APPEAL",
                    Element("APPEAL_PROCEDURE_BODY_RESPONSIBLE",
                        INC_05(_notice.ProceduresForReview.ReviewBody)
                    ),
                    Element("LODGING_OF_APPEALS",
                        PElement("LODGING_OF_APPEALS_PRECISION", _notice.ProceduresForReview?.ReviewProcedure))));
            }

            if (config.Defence.TaxLegislationUrl)
            {
                var legislation = Element("INFORMATION_REGULATORY_FRAMEWORK");
                legislation.Add(Element("TAX_LEGISLATION",
                        PElement("TAX_LEGISLATION_VALUE", complDefence.TaxLegislationUrl),
                        complDefence.TaxLegislationInfoProvided ?
                            INC_04(complDefence.TaxLegislation) :
                            null));

                legislation.Add(Element("ENVIRONMENTAL_PROTECTION_LEGISLATION",
                        PElement("ENVIRONMENTAL_PROTECTION_LEGISLATION_VALUE", complDefence.EnvironmentalProtectionUrl),
                        complDefence.EnvironmentalProtectionInfoProvided ? INC_04(complDefence.EnvironmentalProtection) : null));

                legislation.Add(Element("EMPLOYMENT_PROTECTION_WORKING_CONDITIONS",
                        PElement("EMPLOYMENT_PROTECTION_WORKING_CONDITIONS_VALUE", complDefence.EmploymentProtectionUrl),
                        complDefence.EmploymentProtectionInfoProvided ? INC_04(complDefence.EmploymentProtection) : null));

                complementaryInfo.Add(legislation);
            }

            complementaryInfo.Add(DateElement("NOTICE_DISPATCH_DATE", DateTime.Now));

            return complementaryInfo;
        }
        #endregion
    }
}
