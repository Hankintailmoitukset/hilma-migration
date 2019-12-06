using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Hilma.Domain.Integrations.HilmaMigration
{

    public class NoticeXMLParser
    {
        private readonly XNamespace _nutsSchema = "http://publications.europa.eu/resource/schema/ted/2016/nuts";

        public NoticeContract ParseNotice(INoticeImportModel importedNotice)
        {
            try
            {

                var doc = XDocument.Parse(importedNotice.Notice);
                XElement formSection = doc.Root.Element("FORM_SECTION");
                NoticeType noticeType = ParserNoticeType(importedNotice, out bool isCorrigendum);

                XElement formElement;

                if (isCorrigendum && importedNotice.FormNumber == "14")
                {
                    formElement = formSection?.Element("F14_2014");
                }
                else
                {
                    formElement = ResolveFormElement(noticeType, formSection, importedNotice);
                }

                if (formElement == null)
                {
                  throw new Exception("Form element could not be resolved");
                }

                var noticeNumber = importedNotice.NoticeNumber;

                if (noticeType.IsNational())
                {
                    return ParseNationalNotices(formElement, noticeNumber, importedNotice, noticeType, isCorrigendum);
                }

                return ParseEuNotice(importedNotice, noticeType, formElement, noticeNumber, isCorrigendum);

            }
            catch (Exception e)
            {
                //if (this.log != null)
                //    this.log.LogError(e, "Error while parsing notice xml");
                throw;
            }

        }

        private NoticeContract ParseEuNotice(INoticeImportModel importedNotice, NoticeType noticeType, XElement formElement, string noticeNumber, bool isCorrigendum)
        {
            if (noticeType == NoticeType.Undefined && !isCorrigendum)
            {
                throw new NotImplementedException("NoticeType is not supported. Type was " + formElement.Element("NOTICE")?.Attribute("TYPE")?.Value);
            }

            var objectContract = formElement.Element("OBJECT_CONTRACT");
            var awardContract = formElement.Element("AWARD_CONTRACT");
            var contractingBody = formElement.Element("CONTRACTING_BODY");
            var procedure = formElement.Element("PROCEDURE");
            var complementaryInfo = formElement.Element("COMPLEMENTARY_INFO");
            var reviewBodyAddress = complementaryInfo?.Element("ADDRESS_REVIEW_BODY");
            var lefti = formElement.Element("LEFTI");
            var directive = formElement.Element("LEGAL_BASIS")?.Attribute("VALUE")?.Value;
            var changes = formElement?.Element("CHANGES");

            XElement addressContractinBody = contractingBody.Element("ADDRESS_CONTRACTING_BODY");

            var notice = new NoticeContract
            {
                NoticeNumber = noticeNumber,
                CreatorId = null,
                Type = noticeType,
                LegalBasis = directive,
                Project = ParseProject(_nutsSchema, formElement, objectContract, addressContractinBody, contractingBody, noticeType, directive),
                ComplementaryInformation = ParseComplementaryInformation(complementaryInfo),
                DateCreated = importedNotice.HilmaSubmissionDate,
                Language = formElement.Attribute("LG")?.Value ?? "FI",
                ContactPerson = ParseContactPerson(addressContractinBody),
                ProcurementObject = ParseProcurementObject(objectContract),
                ObjectDescriptions = ParseObjectDescription(_nutsSchema, objectContract, awardContract).ToArray(),
                DatePublished = importedNotice.HilmaPublishedDate,
                ProcedureInformation = ParseProcedureInformation(procedure),
                AttachmentInformation = new AttachmentInformation()
                {
                    ValidationState = ValidationState.Valid
                },
                TenderingInformation = ParseTenderingInformation(objectContract, procedure),

                ConditionsInformation = ParseConditionsInformation(lefti),
                CommunicationInformation = ParseCommunicationInformation(_nutsSchema, contractingBody),
                LotsInfo = ParseLotsInfo(objectContract),
                ProceduresForReview = ParseProceduresForReview(reviewBodyAddress, complementaryInfo),
                IsLatest = true,
                Attachments = new string[0],
                TedPublishState = importedNotice.IsPublishedInTed ? TedPublishState.PublishedInTed : TedPublishState.Undefined,
                TedSubmissionId = importedNotice.TedSubmissionId,
                NoticeOjsNumber = importedNotice.NoticeOjsNumber,
                TedPublicationInfo = ParseTedPublicationInfo(importedNotice),
                State = PublishState.Published,
                PreviousNoticeOjsNumber = procedure?.Element("NOTICE_NUMBER_OJ")?.Value ?? complementaryInfo?.Element("NOTICE_NUMBER_OJ")?.Value,
                CorrigendumPreviousNoticeNumber = complementaryInfo?.Element("NO_DOC_EXT")?.Value,
                IsCorrigendum = isCorrigendum,
                Annexes = ParseAnnex(procedure)

            };

            if (importedNotice.FormNumber == "14")
            {
                notice.Changes = ParseChanges(changes);
                notice.CorrigendumAdditionalInformation = ParsePElements(changes?.Element("INFO_ADD"));
            }

            return notice;
        }

        private static Annex ParseAnnex(XElement procedure)
        {
            var annex = new Annex() {};
            if(procedure != null)
            { 

                XElement ptAwardWithoutCall = procedure.Element("PT_AWARD_CONTRACT_WITHOUT_CALL");
                XElement ptNegotiatedWithoutpublicataion = procedure.Element("PT_NEGOTIATED_WITHOUT_PUBLICATION");//form15
                if (ptAwardWithoutCall != null)
                {
                    annex.D1 = new Entities.Annexes.AnnexD1()
                    {
                        Justification = ParsePElements(ptAwardWithoutCall.Element("D_JUSTIFICATION")),
                        AdvantageousPurchaseReason = ptAwardWithoutCall.Element("D_FROM_LIQUIDATOR_CREDITOR") != null ? AdvantageousPurchaseReason.DFromReceivers :
                                                     ptAwardWithoutCall.Element("D_FROM_WINDING_PROVIDER") != null ? AdvantageousPurchaseReason.DFromWindingSupplier :
                                                     AdvantageousPurchaseReason.Undefined,
                        ExtremeUrgency = ptAwardWithoutCall.Element("D_EXTREME_URGENCY") != null,
                        ReasonForNoCompetition = ptAwardWithoutCall.Element("D_TECHNICAL") != null ? ReasonForNoCompetition.DTechnical :
                                                           ptAwardWithoutCall.Element("D_ARTISTIC") != null ? ReasonForNoCompetition.DArtistic :
                                                           ptAwardWithoutCall.Element("D_PROTECT_RIGHTS") != null ? ReasonForNoCompetition.DProtectRights :
                                                            ReasonForNoCompetition.Undefined,
                        ProcedureType = ptAwardWithoutCall.Element("D_PROC_OPEN") !=null ? AnnexProcedureType.DProcOpen :
                                        ptAwardWithoutCall.Element("D_PROC_RESTRICTED") != null ? AnnexProcedureType.DProcRestricted :
                                        AnnexProcedureType.Undefined,
                        RepetitionExisting = ptAwardWithoutCall.Element("D_REPETITION_EXISTING") !=null
                    };

                    if(annex.D1.ProcedureType != AnnexProcedureType.Undefined)
                    {
                        annex.D1.NoTenders = true;
                    }

                    if (annex.D1.ReasonForNoCompetition != ReasonForNoCompetition.Undefined)
                    {
                        annex.D1.ProvidedByOnlyParticularOperator = true;
                    }

                    if (annex.D1.AdvantageousPurchaseReason != AdvantageousPurchaseReason.Undefined)
                    {
                        annex.D1.AdvantageousTerms = true;
                    }



                }

            }
            return annex;
        }

        private static List<Change> ParseChanges(XElement changes)
        {
            if (changes == null)
            {
                return null;
            }
            List<Change> changesList = new List<Change>();

            foreach (var change in changes?.Elements("CHANGE"))
            {
                var Change = new Change()
                {
                    Section = change?.Element("WHERE")?.Element("SECTION")?.Value,
                    Label = change?.Element("WHERE")?.Element("LABEL")?.Value,
                    LotNumber = ParseInt(change?.Element("WHERE")?.Element("LOT_NO")?.Value),

                  
                };

                var oldValue = change?.Element("OLD_VALUE");
                var newValue = change?.Element("NEW_VALUE");

                if ( oldValue != null && newValue != null)
                {
                    var type = oldValue.Descendants().First().Name.LocalName;
                    switch (type)
                    {
                        case "TEXT":
                            Change.OldText = ParsePElements(oldValue?.Element("TEXT"));
                            Change.NewText = ParsePElements(newValue?.Element("TEXT"));
                            break;
                        case "DATE":
                            Change.OldDate = ParseChangeDateTime(oldValue);
                            Change.NewDate = ParseChangeDateTime(newValue);
                            break;
                        case "CPV_ADDITIONAL":
                            Change.NewAdditionalCpvCodes = ParseAdditionalCpvCodes(oldValue).ToList();
                            Change.OldAdditionalCpvCodes = ParseAdditionalCpvCodes(newValue).ToList();
                            break;
                        case "CPV_MAIN":
                            Change.NewMainCpvCode = ParseCpvCode(oldValue);
                            Change.OldMainCpvCode = ParseCpvCode(newValue);
                            break;
                    }

                }
                changesList.Add(Change);
            }

            return changesList;
        }

        private static DateTime? ParseChangeDateTime(XElement change)
        {
            DateTime? date = ParseDate(change?.Element("DATE")?.Value);
            string time = change?.Element("TIME")?.Value;

            if (!string.IsNullOrEmpty(time))
            {
                var timeValue = DateTime.ParseExact(time, "H:mm", null);
                date += timeValue.TimeOfDay;
            }

            return date;
        }

        private NoticeContract ParseNationalNotices(XElement formElement, string noticeNumber, INoticeImportModel importedNotice, NoticeType noticeType, bool isCorrigendum) //Form 99
        {
            var domesticContract = formElement.Element("FD_DOMESTIC_CONTRACT");
            var domesticNoticeType = domesticContract?.Element("DOMESTIC_NOTICE_TYPE");


            if (noticeType == NoticeType.Undefined && !isCorrigendum)
            {
                throw new NotImplementedException("NoticeType is not supported. Type was " + domesticNoticeType?.Elements().First().Name.LocalName);
            }

            var domesticAuthorityInformation = domesticContract?.Element("DOMESTIC_AUTHORITY_INFORMATION");
            var domesticObjectContract = domesticContract?.Element("DOMESTIC_OBJECT_INFORMATION");
            var domesticContractRC = domesticContract?.Element("DOMESTIC_CONTRACT_RELATING_CONDITIONS");
            var domesticNameAddresses = domesticContract?.Element("DOMESTIC_AUTHORITY_INFORMATION")?.Element("DOMESTIC_NAME_ADDRESSES");
            var organisationInformation = ParseContractingBodyInformation(_nutsSchema, domesticNameAddresses);

            var notice = new NoticeContract()
            {
                PreviousNoticeOjsNumber = domesticNoticeType?.Element("CORRIGENDUM_NOTICE")?.Element("DOMESTIC_ORIGINAL_NOTICE")?.Attribute("NO_DOC_EXT")?.Value,
                IsCorrigendum = isCorrigendum,
                CreatorId = null,
                NoticeNumber = noticeNumber,
                Language = "FI",
                Type = ParseNationalNoticeType(domesticNoticeType.Elements().First().Name.LocalName),
                ContactPerson = ParseContactPerson(domesticNameAddresses),
                IsLatest = true,
                Project = new ProcurementProjectContract()
                {
                    Title = domesticObjectContract?.Element("TITLE_CONTRACT")?.Value,
                    ReferenceNumber = domesticObjectContract?.Element("FILE_REFERENCE_NUMBER")?.Value,

                    Organisation = new OrganisationContract()
                    {
                        Information = organisationInformation,
                        ContractingAuthorityType = FromTEDFormatContractingAuthorityType(domesticAuthorityInformation?.Element("DOMESTIC_TYPE_OF_CONTRACTING").Descendants().First().Name.ToString()),
                        OtherContractingAuthorityType = domesticAuthorityInformation?.Element("DOMESTIC_TYPE_OF_CONTRACTING")?.Element("OTHER")?.Value,

                    },
                    ProcurementCategory = ProcurementCategory.Public,
                    ContractType = ParseContractType(domesticContract.Attribute("DOMESTIC_CTYPE").Value),
                    CentralPurchasing = domesticAuthorityInformation?.Element("PURCHASING_ON_BEHALF")?.Attribute("VALUE")?.Value == "YES",
                    ValidationState = ValidationState.Valid,
                    Publish = PublishType.ToHilma
                },
                CommunicationInformation = ParseNationalCommunicationInformation(domesticAuthorityInformation, organisationInformation),
                ProcurementObject = new ProcurementObject()
                {
                    MainCpvCode = new CpvCode
                    {
                        Code = domesticObjectContract?.Element("CPV")?.Element("CPV_MAIN").Element("CPV_CODE")?.Attribute("CODE")?.Value,
                        VocCodes = domesticObjectContract?.Element("CPV")?.Element("CPV_MAIN").Elements("CPV_SUPPLEMENTARY_CODE").Select(s => new VocCode { Code = s.Attribute("CODE")?.Value }).ToArray()
                    },
                    EstimatedValue = ParseNationalEstimatedValue(domesticObjectContract),
                    ValidationState = ValidationState.Valid
                },
                ObjectDescriptions = ParseNationalObjectDescriptions(_nutsSchema, domesticContract, domesticContractRC, domesticObjectContract).ToArray(), //ParseObjectDescription(_nutsSchema, domesticObjectContract, null).ToArray(),
                ProcedureInformation = new ProcedureInformation()
                {
                    ProcedureType = ParseNationalProcedureTypes(domesticContract?.Element("DOMESTIC_PROCEDURE_DEFINITION")?.Element("DOMESTIC_TYPE_OF_PROCEDURE")),
                    FrameworkAgreement = new FrameworkAgreementInformation()
                    {
                        IncludesFrameworkAgreement = domesticContract?.Element("DOMESTIC_PROCEDURE_DEFINITION")?.Element("FRAMEWORK_AGREEMENT_IS_ESTABLISH")?.Attribute("VALUE")?.Value == "YES" ? true : false,
                    },
                    National = new ProcedureInformationNational()
                    {
                        OtherProcedure = ParsePElements(domesticContract?.Element("DOMESTIC_PROCEDURE_DEFINITION")?.Element("DOMESTIC_PROCEDURE_DEFINITION")),
                        AdditionalProcedureInformation = ParsePElements(domesticContractRC.Element("SELECTION_CRITERIA_INFORMATION"))
                    }
                },
                ConditionsInformationNational = new ConditionsInformationNational()
                {
                    ParticipantSuitabilityCriteria = ParsePElements(domesticContractRC.Element("SELECTION_CRITERIA")),
                    RequiredCertifications = ParsePElements(domesticContractRC.Element("SELECTION_CRITERIA_CERTIFICATIONS")),
                    ReservedForShelteredWorkshopOrProgram = domesticContract?.Element("DOMESTIC_PROCEDURE_DEFINITION")?.Element("RESERVED_CONTRACTS")?.Attribute("VALUE").Value == "YES"
                },
                ConditionsInformation = new ConditionsInformation(),
                TenderingInformation = new TenderingInformation()
                {
                    TendersOrRequestsToParticipateDueDateTime = ParseNationalReceiptLimitDate(domesticContractRC),
                    ValidationState = ValidationState.Valid
                },
                ProceduresForReview = ParseProceduresForReview(domesticContract?.Element("DOMESTIC_ADDRESS_REVIEW_BODY"), null),
                ComplementaryInformation = new ComplementaryInformation()
                {
                    AdditionalInformation = domesticContractRC?.Descendants("ADDITIONAL_INFORMATION")?.SelectMany(i => ParsePElements(i)).ToArray() ?? new string[0],
                },
                LotsInfo = new LotsInfo()
                {
                    DivisionLots = domesticContract?.Element("DOMESTIC_PROCEDURE_DEFINITION")?.Element("DIVISION_INTO_LOTS")?.Attribute("VALUE").Value == "YES",
                },
                AttachmentInformation = new AttachmentInformation(),
                Attachments = new string[0],
                State = PublishState.Published,
                DatePublished = importedNotice.HilmaPublishedDate
            };

            if (notice.Project.Organisation.ContractingAuthorityType == ContractingAuthorityType.OtherType)
            {
                if (string.IsNullOrEmpty(notice.Project.Organisation.OtherContractingAuthorityType))
                {
                    notice.Project.Organisation.OtherContractingAuthorityType = "Ei määritelty";
                }
            }

            notice.Project.Organisation.Information.NutsCodes = new[]{
                    domesticContract?.Element("DOMESTIC_OBJECT_INFORMATION")?.Element(_nutsSchema + "NUTS")?.Attribute("CODE")?.Value
                   };

            return notice;
        }

        private static ProcedureType ParseNationalProcedureTypes(XElement domesticProcedureInformation)
        {
            if (domesticProcedureInformation == null)
            {
                return ProcedureType.Undefined;
            }
            switch (domesticProcedureInformation?.Elements()?.First()?.Name.LocalName)
            {
                case "OPEN":
                    return ProcedureType.ProctypeOpen;
                case "RESTRICTED":
                    return ProcedureType.ProctypeRestricted;
                case "NEGOTIATED":
                    return ProcedureType.ProctypeNegotiation;
                case "COMPETITIVE_DIALOGUE":
                    return ProcedureType.ProctypeCompDialogue;
                case "INNOVATION_PARTNERSHIP":
                    return ProcedureType.ProctypeInnovation;
                case "DESIGN_CONTEST":
                    return ProcedureType.Undefined;
                case "DPS":
                    return ProcedureType.Undefined;
                case "OTHER":
                    return ProcedureType.ProctypeOther;
                default:
                    return ProcedureType.Undefined;
            }
        }

        private CommunicationInformation ParseNationalCommunicationInformation(XElement domesticAuthorityInformation, ContractBodyContactInformation organisationInformation)
        {
            var sendToTenders = domesticAuthorityInformation?.Element("DOMESTIC_TENDERS_REQUESTS_APPLICATIONS_MUST_BE_SENT_TO");
            var communicationInformation = new CommunicationInformation()
            {
                SendTendersOption = ParseNationalSendTendersOption(sendToTenders),
            };

            switch (communicationInformation.SendTendersOption)
            {
                case TenderSendOptions.AddressSendTenders:
                    communicationInformation.ElectronicAddressToSendTenders = sendToTenders?.Element("URL_DOCUMENT")?.Value;
                    break;
                case TenderSendOptions.AddressOrganisation:
                    communicationInformation.AddressToSendTenders = organisationInformation;
                    break;
                case TenderSendOptions.AddressFollowing:
                    communicationInformation.AddressToSendTenders = ParseContractingBodyInformation(_nutsSchema, sendToTenders);
                    break;
                default:
                    break;
            }

            return communicationInformation;
        }

        private static TenderSendOptions ParseNationalSendTendersOption(XElement domesticTendersOption)
        {
            string localName = domesticTendersOption?.Elements()?.First()?.Name?.LocalName;
            if(localName == null)
            {
                return TenderSendOptions.Undefined;
            }
            switch (localName)
            {
                case "IDEM":
                    return TenderSendOptions.AddressOrganisation;
                case "URL_DOCUMENT":
                    return TenderSendOptions.AddressSendTenders;
                default:
                    return TenderSendOptions.AddressFollowing;
            }

        }

        private static ValueRangeContract ParseNationalEstimatedValue(XElement domesticObjectContract)
        {
            var domesticCostRange = domesticObjectContract?.Element("DOMESTIC_COSTS_RANGE_AND_CURRENCY");
            return new ValueRangeContract()
            {
                Currency = domesticCostRange?.Attribute("CURRENCY")?.Value,
                MinValue = ParseDecimal(domesticCostRange?.Element("RANGE_VALUE_COST")?.Element("LOW_VALUE")?.Value),
                MaxValue = ParseDecimal(domesticCostRange?.Element("RANGE_VALUE_COST")?.Element("HIGH_VALUE")?.Value),
                Type = domesticCostRange != null ? ContractValueType.Range : ContractValueType.Undefined,
                DisagreeToBePublished = domesticCostRange?.Attribute("IS_PUBLIC")?.Value == "NO" ? false :
                                     domesticCostRange?.Attribute("IS_PUBLIC")?.Value == "YES" ? true : new bool?(),
            };
        }

        private static NoticeType ParseNationalNoticeType(string noticeType)
        {
            switch (noticeType)
            {
                case "REQUEST_FOR_INFORMATION":
                    return NoticeType.NationalPriorInformation;
                case "DOMESTIC_CONTRACT":
                    return NoticeType.NationalContract;
                default:
                    return NoticeType.Undefined;
            }

        }

        private static DateTime? ParseNationalReceiptLimitDate(XElement domesticContractRC)
        {
            if (domesticContractRC?.Element("RECEIPT_LIMIT_DATE") == null)
            {
                return null;
            }

            var year = ParseInt(domesticContractRC?.Element("RECEIPT_LIMIT_DATE")?.Element("YEAR")?.Value);
            var month = ParseInt(domesticContractRC?.Element("RECEIPT_LIMIT_DATE")?.Element("MONTH")?.Value);
            var day = ParseInt(domesticContractRC?.Element("RECEIPT_LIMIT_DATE")?.Element("DAY")?.Value);
            var timeString = domesticContractRC?.Element("RECEIPT_LIMIT_DATE")?.Element("TIME")?.Value;
            var date = new DateTime(year, month, day);

            if (!string.IsNullOrEmpty(timeString))
            {
                var time = DateTime.ParseExact(timeString, "H:mm", null);
                date += time.TimeOfDay;
            }

            return date;
        }

        private static IEnumerable<ObjectDescription> ParseNationalObjectDescriptions(XNamespace nutsSchema, XElement objectContract, XElement domesticContractRC, XElement domesticObjectContract)
        {
            return objectContract?.Descendants("DOMESTIC_CONTRACT_RELATING_CONDITIONS").Select((objectDescription, index) =>
            {
                var objectDescriptionModel = new ObjectDescription()
                {
                    LotNumber = index + 1,
                    AwardCriteria = new AwardCriteria()
                    {
                        QualityCriteria = ParseNationalAwardCriteria(domesticContractRC?.Element("AWARD_CRITERIA")?.Element("DOMESTIC_AC_PRICE_QUALITY")?.Element("DOMESTIC_CRITERIA_STATED_BELOW")?.Elements("AC_DEFINITION")),
                        CriterionTypes = ParseNationalCostAndQualityCriteria(domesticContractRC?.Element("AWARD_CRITERIA")),
                        CostCriteria = domesticContractRC?.Element("AWARD_CRITERIA")?.Element("DOMESTIC_AC_COST") != null ? new[]
                        {
                            new AwardCriterionDefinition()
                            {
                                Criterion = "Edullisimmat kustannukset",
                                Weighting = "100 %"
                            }
                        } : new AwardCriterionDefinition[0]
                    },

                    TendersMustBePresentedAsElectronicCatalogs = objectDescription.Element("DOMESTIC_ECATALOG_REQUIRED") != null,
                    DescrProcurement = ParsePElements(domesticObjectContract.Element("SHORT_CONTRACT_DESCRIPTION")),
                    OptionsAndVariants = new OptionsAndVariants()
                    {
                        VariantsWillBeAccepted = objectContract?.Element("DOMESTIC_PROCEDURE_DEFINITION")?.Element("ACCEPTED_VARIANTS")?.Attribute("VALUE").Value == "YES"
                    },
                    NutsCodes = ParseNationalNuts(domesticObjectContract, nutsSchema),
                    MainCpvCode = new CpvCode
                    {
                        Code = domesticObjectContract?.Element("CPV")?.Element("CPV_MAIN").Element("CPV_CODE")?.Attribute("CODE")?.Value,
                        VocCodes = domesticObjectContract?.Element("CPV")?.Element("CPV_MAIN").Elements("CPV_SUPPLEMENTARY_CODE").Select(s => new VocCode { Code = s.Attribute("CODE")?.Value }).ToArray()
                    }

                };
                return objectDescriptionModel;
            });

        }

        private static string[] ParseNationalNuts(XElement domesticObjectContract, XNamespace nutsSchema)
        {
            List<string> nutsList = new List<string>();
            foreach (var nuts in domesticObjectContract.Elements(nutsSchema + "NUTS"))
            {
                nutsList.Add(nuts?.Attribute("CODE")?.Value);
            }
            return nutsList.ToArray();
        }

        private static AwardCriterionType ParseNationalCostAndQualityCriteria(XElement awardCriteria)
        {
            if (awardCriteria == null)
            {
                return AwardCriterionType.Undefined;
            }
            var awardCriteriaType = awardCriteria?.Descendants()?.First();
            switch (awardCriteriaType.Name.LocalName)
            {
                case "DOMESTIC_AC_PRICE_QUALITY":
                    if (awardCriteriaType.Element("CRITERIA_STATED_IN_OTHER_DOCUMENT") != null)
                    {
                        return AwardCriterionType.DescriptiveCriteria;
                    }
                    return AwardCriterionType.PriceAndQualityCriteria;
                case "LOWEST_PRICE":
                    return AwardCriterionType.PriceCriterion;
                case "DOMESTIC_AC_COST":
                    return AwardCriterionType.CostCriterion;
                default:
                    return AwardCriterionType.Undefined;
            }

        }

        private static AwardCriterionDefinition[] ParseNationalAwardCriteria(IEnumerable<XElement> acDefinitions)
        {
            List<AwardCriterionDefinition> awardCriterionDefinitions = new List<AwardCriterionDefinition>();

            if (acDefinitions == null)
            {
                return awardCriterionDefinitions.ToArray();
            }

            foreach (var ac in acDefinitions)
            {
                awardCriterionDefinitions.Add(
                    new AwardCriterionDefinition()
                    {
                        Criterion = ac.Element("AC_CRITERION")?.Value,
                        Weighting = ac.Element("AC_WEIGHTING")?.Value
                    });
            }

            return awardCriterionDefinitions.ToArray();
        }

        private static TedPublicationInfo ParseTedPublicationInfo(INoticeImportModel importedNotice)
        {
            return new TedPublicationInfo()
            {
                Publication_date = importedNotice.TedPublishedDate.GetValueOrDefault(),
                No_doc_ojs = importedNotice.NoticeOjsNumber,
                Ojs_number = importedNotice.NoticeOjsNumber,
                Ted_links = new TedLinks()
            };
        }

        private static ProceduresForReviewInformation ParseProceduresForReview(XElement reviewBodyAddress, XElement complementaryInfo)
        {
            if (reviewBodyAddress == null)
            {
                return new ProceduresForReviewInformation(){};
            }
            return new ProceduresForReviewInformation()
            {
                ReviewBody = new ContractBodyContactInformation()
                {
                    OfficialName = reviewBodyAddress?.Element("OFFICIALNAME")?.Value,
                    PostalAddress = ParsePostalAddress(reviewBodyAddress),
                    Email = reviewBodyAddress?.Element("E_MAIL")?.Value != null ? reviewBodyAddress?.Element("E_MAIL")?.Value : "",
                    MainUrl = reviewBodyAddress?.Element("URL")?.Value,
                    TelephoneNumber = reviewBodyAddress?.Element("PHONE")?.Value,
                },
                ReviewProcedure = ParsePElements(complementaryInfo?.Element("REVIEW_PROCEDURE"), 0),
                ValidationState = ValidationState.Valid
            };
        }

        private static LotsInfo ParseLotsInfo(XElement objectContract)
        {
            var quantityOfLots = objectContract.Elements("OBJECT_DESCR")?.Count() ?? 1;
            if (quantityOfLots == 1)
            {
                return new LotsInfo()
                {
                    LotsSubmittedFor = LotsSubmittedFor.LotOneOnly,
                    ValidationState = ValidationState.Valid
                };
            }

            var lotsInfo = new LotsInfo()
            {
                DivisionLots = objectContract.Element("LOT_DIVISION") != null,
                LotCombinationPossible = objectContract.Element("LOT_DIVISION")?.Element("LOT_COMBINING_CONTRACT_RIGHT") != null,
                LotCombinationPossibleDescription = ParsePElements(objectContract.Element("LOT_DIVISION")?.Element("LOT_COMBINING_CONTRACT_RIGHT"), 0),
                LotsMaxAwarded = objectContract.Element("LOT_DIVISION")?.Element("LOT_MAX_ONE_TENDERER") != null,
                LotsMaxAwardedQuantity = ParseInt(objectContract.Element("LOT_DIVISION")?.Element("LOT_MAX_ONE_TENDERER")?.Value ?? "0"),
                QuantityOfLots = objectContract.Elements("OBJECT_DESCR")?.Count() ?? 1,
                LotsSubmittedFor = ParseLotsSubmittedFor(objectContract),
                LotsSubmittedForQuantity = ParseInt(objectContract.Element("LOT_MAX_ONE_TENDERER")?.Value),
                ValidationState = ValidationState.Valid,
            };


            if (lotsInfo.LotsSubmittedFor == LotsSubmittedFor.LotsMax)
            {
                lotsInfo.LotsSubmittedForQuantity = ParseInt(objectContract.Element("LOT_DIVISION")?.Element("LOT_MAX_NUMBER")?.Value);
            }
            return lotsInfo;
        }

        private static LotsSubmittedFor ParseLotsSubmittedFor(XElement objectContract)
        {
            return objectContract.Element("LOT_DIVISION")?.Element("LOT_ALL") != null ? LotsSubmittedFor.LotsAll :
                   objectContract.Element("LOT_DIVISION")?.Element("LOT_MAX_NUMBER") != null ? LotsSubmittedFor.LotsMax :
                   objectContract.Element("LOT_DIVISION")?.Element("LOT_ONE_ONLY") != null ? LotsSubmittedFor.LotOneOnly :
                   LotsSubmittedFor.Undefined;
        }

        private static CommunicationInformation ParseCommunicationInformation(XNamespace nutsSchema, XElement contractingBody)
        {
            return new CommunicationInformation()
            {
                AdditionalInformation = contractingBody?.Element("ADDRESS_FURTHER_INFO_IDEM") != null ? AdditionalInformationAvailability.AddressToAbove : contractingBody?.Element("ADDRESS_FURTHER_INFO") != null ? AdditionalInformationAvailability.AddressAnother: AdditionalInformationAvailability.Undefined,
                AdditionalInformationAddress = contractingBody?.Element("ADDRESS_FURTHER_INFO") != null ? ParseContractingBodyInformation(nutsSchema, contractingBody?.Element("ADDRESS_FURTHER_INFO")) : null,
                ProcurementDocumentsAvailable = contractingBody?.Element("DOCUMENT_FULL") != null ? ProcurementDocumentAvailability.AddressObtainDocs : contractingBody?.Element("DOCUMENT_RESTRICTED") != null ? ProcurementDocumentAvailability.DocsRestricted : ProcurementDocumentAvailability.Undefined,
                ProcurementDocumentsUrl = contractingBody?.Element("URL_DOCUMENT")?.Value,
                ElectronicAddressToSendTenders = contractingBody?.Element("URL_PARTICIPATION")?.Value,
                AddressToSendTenders = contractingBody?.Element("ADDRESS_PARTICIPATION") != null ? ParseContractingBodyInformation(nutsSchema, contractingBody?.Element("ADDRESS_PARTICIPATION")) : null,
               
                ElectronicCommunicationInfoUrl = contractingBody.Element("URL_TOOL")?.Value,
                ValidationState = ValidationState.Valid,
                SendTendersOption = contractingBody?.Element("URL_PARTICIPATION") != null ? TenderSendOptions.AddressSendTenders :
                                    contractingBody?.Element("ADDRESS_PARTICIPATION") != null ? TenderSendOptions.AddressFollowing :
                                    contractingBody?.Element("ADDRESS_PARTICIPATION_IDEM") != null ? TenderSendOptions.AddressOrganisation :
                                    TenderSendOptions.Undefined
            };

        }

        private static ConditionsInformation ParseConditionsInformation(XElement lefti)
        {
            if (lefti == null)
            {
                return new ConditionsInformation();
            }
            var conditionsInformation = new ConditionsInformation()
            {
                ProfessionalSuitabilityRequirements = ParsePElements(lefti?.Element("SUITABILITY"), 0),
                TechnicalCriteriaToParticipate = lefti?.Element("TECHNICAL_CRITERIA_DOC") != null ? true : false,
                EconomicCriteriaDescription = ParsePElements(lefti?.Element("ECONOMIC_FINANCIAL_INFO"), 0),
                EconomicRequiredStandards = ParsePElements(lefti?.Element("ECONOMIC_FINANCIAL_MIN_LEVEL"), 0),
                TechnicalCriteriaDescription = ParsePElements(lefti?.Element("TECHNICAL_PROFESSIONAL_INFO"), 0),
                TechnicalRequiredStandards = ParsePElements(lefti?.Element("TECHNICAL_PROFESSIONAL_MIN_LEVEL"), 0),
                ValidationState = ValidationState.Valid,
                ReferenceToRelevantLawRegulationOrProvision = ParsePElements(lefti?.Element("REFERENCE_TO_LAW"), 0),
                ExecutionOfServiceIsReservedForProfession = lefti?.Element("PARTICULAR_PROFESSION") != null,
                ObligationToIndicateNamesAndProfessionalQualifications = lefti?.Element("PERFORMANCE_STAFF_QUALIFICATION") != null,
                
                CiriteriaForTheSelectionOfParticipants = ParsePElements(lefti.Element("CRITERIA_SELECTION")),
                EconomicCriteriaToParticipate = lefti?.Element("ECONOMIC_CRITERIA_DOC") != null ? true : false,
                ParticipationIsReservedForProfession = lefti?.Element("PARTICULAR_PROFESSION") != null ? true : false,

                DepositsRequired = ParsePElements(lefti?.Element("DEPOSIT_GUARANTEE_REQUIRED")),
                FinancingConditions = ParsePElements(lefti?.Element("MAIN_FINANCING_CONDITION")),
                LegalFormTaken = ParsePElements(lefti?.Element("LEGAL_FORM")),

                ContractPerformanceConditions = ParsePElements(lefti?.Element("PERFORMANCE_CONDITIONS")),
                RestrictedToShelteredProgram = lefti?.Element("RESTRICTED_SHELTERED_PROGRAM") !=null,
                RulesForParticipation = ParsePElements(lefti?.Element("RULES_CRITERIA")),
                RestrictedToShelteredWorkshop = lefti?.Element("RESTRICTED_SHELTERED_WORKSHOP") != null,
                ReservedOrganisationServiceMission = lefti?.Element("RESERVED_ORGANISATIONS_SERVICE_MISSION") !=null
            };

            return conditionsInformation;
        }

        private TenderingInformation ParseTenderingInformation(XElement objectContract, XElement procedure)
        {
            var tenderingInformation = new TenderingInformation();

            if (objectContract == null && procedure == null)
            {
                return null;
            }

            if (objectContract != null)
            {
                tenderingInformation.EstimatedDateOfContractNoticePublication = ParseDate(objectContract.Element("DATE_PUBLICATION_NOTICE")?.Value);
                tenderingInformation.ValidationState = ValidationState.Valid;
            }
            if (procedure != null)
            {
                tenderingInformation.TendersOrRequestsToParticipateDueDateTime = BuildDateTime(procedure?.Element("DATE_RECEIPT_TENDERS")?.Value, procedure?.Element("TIME_RECEIPT_TENDERS")?.Value);
                tenderingInformation.Languages = ParseLanguages(procedure);
                tenderingInformation.TendersMustBeValidForMonths = ParseInt(procedure.Element("DURATION_TENDER_VALID")?.Value ?? null);
                tenderingInformation.TendersMustBeValidOption = procedure.Element("DATE_TENDER_VALID") != null ? TendersMustBeValidOption.Date :
                                           procedure.Element("DURATION_TENDER_VALID") != null ? TendersMustBeValidOption.Months :
                                           TendersMustBeValidOption.TimeNotSet;
                tenderingInformation.TendersMustBeValidUntil = ParseDate(procedure.Element("DATE_TENDER_VALID")?.Value);
                tenderingInformation.TenderOpeningConditions = new TenderOpeningConditions()
                {
                    OpeningDateAndTime = BuildDateTime(
                                            procedure.Element("OPENING_CONDITION")?.Element("DATE_OPENING_TENDERS").Value,
                                            procedure.Element("OPENING_CONDITION")?.Element("TIME_OPENING_TENDERS").Value
                                         ),
                    InformationAboutAuthorisedPersons = ParsePElements(procedure.Element("OPENING_CONDITION")?.Element("INFO_ADD"), 0),
                    Place = ParsePElements(procedure.Element("OPENING_CONDITION")?.Element("PLACE"), 0)
                };
                tenderingInformation.TendersMustBeValidUntil = ParseDate(procedure.Element("DATE_TENDER_VALID")?.Value);
                tenderingInformation.TendersMustBeValidOption = procedure.Element("DATE_TENDER_VALID") != null ? TendersMustBeValidOption.Date : procedure.Element("DURATION_TENDER_VALID")?.Attribute("TYPE").Value == "MONTH" ? TendersMustBeValidOption.Months : TendersMustBeValidOption.TimeNotSet;
                tenderingInformation.ValidationState = ValidationState.Valid;
            }

            return tenderingInformation;
        }

        private static string[] ParseLanguages(XElement procedure)
        {
            List<string> languages = new List<string>();

            if (procedure.Element("LANGUAGES") != null)
            {
                foreach (var item in procedure.Element("LANGUAGES")?.Elements("LANGUAGE"))
                {
                    languages.Add(item.Attribute("VALUE")?.Value);
                }
            }

            return languages.ToArray();
        }

        public static DateTime? BuildDateTime(string dateString, string timeString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            var date = ParseDate(dateString);

            if (!string.IsNullOrEmpty(timeString))
            {
                var time = DateTime.ParseExact(timeString, "H:mm", null);
                date += time.TimeOfDay;
            }

            return date;
        }

        private static ProcedureInformation ParseProcedureInformation(XElement procedure)
        {
            if (procedure == null)
            {
                return null;
            }
            return new ProcedureInformation()
            {
                ProcedureType = ParseProcedureType(procedure),
                ProcurementGovernedByGPA = procedure?.Element("CONTRACT_COVERED_GPA") != null ? true : false,
                FrameworkAgreement = ParseFrameworkAgreementInformation(procedure),
                AcceleratedProcedure = procedure?.Element("ACCELERATED_PROC") != null,
                JustificationForAcceleratedProcedure = ParsePElements(procedure?.Element("ACCELERATED_PROC"), 0),
                UrlNationalProcedure = procedure?.Element("URL_NATIONAL_PROCEDURE")?.Value,
                MainFeaturesAward = ParsePElements(procedure?.Element("MAIN_FEATURES_AWARD")),
                ValidationState = ValidationState.Valid
            };
        }

        private static ProcedureType ParseProcedureType(XElement procedure)
        {

            if (procedure?.Element("PT_OPEN") != null) return ProcedureType.ProctypeOpen;
            if (procedure?.Element("PT_RESTRICTED") != null) return ProcedureType.ProctypeRestricted;
            if (procedure?.Element("PT_COMPETITIVE_NEGOTIATION") != null) return ProcedureType.ProctypeCompNegotiation;
            if (procedure?.Element("PT_COMPETITIVE_DIALOGUE") != null) return ProcedureType.ProctypeCompDialogue;
            if (procedure?.Element("PT_INNOVATION_PARTNERSHIP") != null) return ProcedureType.ProctypeInnovation;
            if (procedure?.Element("PT_INVOLVING_NEGOTIATION") != null) return ProcedureType.ProctypeNegotiationsInvolved;
            if (procedure?.Element("PT_NEGOTIATED_WITH_PRIOR_CALL") != null) return ProcedureType.ProctypeNegotWCall;
            if (procedure?.Element("PT_AWARD_CONTRACT_WITHOUT_CALL") != null)
            {
                if (procedure?.Element("PT_AWARD_CONTRACT_WITHOUT_CALL")?.Element("D_OUTSIDE_SCOPE") != null) return ProcedureType.ProctypeNegotiatedWoNotice;
                return ProcedureType.AwardWoPriorPubD1;
            }

            return ProcedureType.Undefined;
        }

        private static FrameworkAgreementInformation ParseFrameworkAgreementInformation(XElement procedure)
        {
            return new FrameworkAgreementInformation()
            {
                IncludesFrameworkAgreement = procedure?.Element("FRAMEWORK") != null,
                FrameworkAgreementType = procedure?.Element("FRAMEWORK")?.Element("SINGLE_OPERATOR") != null ? FrameworkAgreementType.FrameworkSingle :
                                         procedure?.Element("FRAMEWORK")?.Element("SEVERAL_OPERATORS") != null ? FrameworkAgreementType.FrameworkSeveral :
                                         FrameworkAgreementType.Undefined,
                EnvisagedNumberOfParticipants = procedure?.Element("FRAMEWORK")?.Element("NB_PARTICIPANTS") != null ?
                                                ParseInt(procedure?.Element("FRAMEWORK")?.Element("NB_PARTICIPANTS")?.Value) : new int?(),

                DynamicPurchasingSystemWasTerminated = procedure?.Element("TERMINATION_DPS") != null,
                IncludesDynamicPurchasingSystem = procedure?.Element("DPS") !=null
                
            };
        }

        private static ProcurementObject ParseProcurementObject(XElement objectContract)
        {
            return new ProcurementObject
            {
                MainCpvCode = new CpvCode
                {
                    Code = objectContract.Element("CPV_MAIN").Element("CPV_CODE")?.Attribute("CODE")?.Value,
                    VocCodes = objectContract.Element("CPV_MAIN").Elements("CPV_SUPPLEMENTARY_CODE").Select(s => new VocCode { Code = s.Attribute("CODE")?.Value }).ToArray()
                },
                ShortDescription = ParsePElements(objectContract.Element("SHORT_DESCR"), 4000),
                ValidationState = ValidationState.Valid,
                EstimatedValue = ParseValueRangeContract(objectContract.Element("VAL_ESTIMATED_TOTAL"), null ),
                TotalValue = ParseValueRangeContract(objectContract.Element("VAL_TOTAL"), objectContract.Element("VAL_RANGE_TOTAL"))
            };
        }

        private static decimal? ParseDecimal(string value)
        {

            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
            return decimal.TryParse(value, NumberStyles.Float, numberFormatInfo, out decimal result) ? result : new decimal?();
        }

        private static CpvCode ParseCpvCode(XElement objectContract)
        {
            return new CpvCode
            {
                Code = objectContract?.Element("CPV_MAIN")?.Element("CPV_CODE")?.Attribute("CODE")?.Value
            };
        }

        private static ContactPerson ParseContactPerson(XElement addressContractinBody)
        {
            if (addressContractinBody == null)
            {
                return null;
            }
            return new ContactPerson()
            {
                Email = addressContractinBody.Element("E_MAIL")?.Value != null ?
                        addressContractinBody.Element("E_MAIL")?.Value :
                        addressContractinBody?.Element("E_MAILS")?.Element("E_MAIL")?.Value,
                Name = addressContractinBody.Element("CONTACT_POINT")?.Value,
                Phone = addressContractinBody.Element("PHONE")?.Value
            };
        }

        private static int ParseInt(string value)
        {
            int.TryParse(value, out int parsedValue);
            return parsedValue;
        }

        private static IEnumerable<ObjectDescription> ParseObjectDescription(XNamespace nutsSchema, XElement objectContract, XElement awardContract)
        {
            foreach (XElement objectDescription in objectContract.Descendants("OBJECT_DESCR"))
            {
                ObjectDescription objectDescriptionModel = new ObjectDescription()
                {
                    AdditionalCpvCodes = ParseAdditionalCpvCodes(objectDescription),
                    Title = objectDescription.Element("TITLE")?.Element("P")?.Value,
                    LotNumber = ParseInt(objectDescription.Element("LOT_NO")?.Value.Replace(".", "") ?? "1"),
                    MainsiteplaceWorksDelivery = ParsePElements(objectDescription.Element("MAIN_SITE"), 0),
                    NutsCodes = ParseNutsCodes(nutsSchema, objectDescription),
                    DescrProcurement = ParsePElements(objectDescription?.Element("SHORT_DESCR"), 4000),
                    AwardCriteria = ParseCriterionTypes(objectDescription?.Element("AC")),
                    EuFunds = ParseEuFunds(objectDescription),
                    TimeFrame = ParseTimeFrame(objectDescription),
                    OptionsAndVariants = ParseOptionsAndVariants(objectDescription),
                    TendersMustBePresentedAsElectronicCatalogs = objectDescription.Element("ECATALOGUE_REQUIRED") != null,
                    AdditionalInformation = ParsePElements(objectDescription.Element("INFO_ADD"), 0),
                    ValidationState = ValidationState.Valid,
                    EstimatedValue = ParseValueRangeContract(objectDescription.Element("VAL_OBJECT"), null),
                    AwardContract = new Award(),
                    CandidateNumberRestrictions = new CandidateNumberRestrictions()
                    {
                        Selected = objectDescription.Element("NB_ENVISAGED_CANDIDATE") != null ?
                                   EnvisagedParticipantsOptions.EnvisagedNumber :
                                   objectDescription.Element("NB_MAX_LIMIT_CANDIDATE") != null || objectDescription.Element("NB_MIN_LIMIT_CANDIDATE") != null ?
                                   EnvisagedParticipantsOptions.Range :
                                   EnvisagedParticipantsOptions.Undefined,
                        EnvisagedMaximumNumber = ParseInt(objectDescription.Element("NB_MAX_LIMIT_CANDIDATE")?.Value),
                        EnvisagedMinimumNumber = ParseInt(objectDescription.Element("NB_MIN_LIMIT_CANDIDATE")?.Value),
                        EnvisagedNumber = ParseInt(objectDescription.Element("NB_ENVISAGED_CANDIDATE")?.Value),
                        ObjectiveCriteriaForChoosing = ParsePElements(objectDescription.Element("CRITERIA_CANDIDATE"), 0)
                    },

                };

                if (awardContract != null)
                {
                    objectDescriptionModel.AwardContract = ParseAwardContract(awardContract, nutsSchema);
                }

                yield return objectDescriptionModel;
            }
        }

        private static Award ParseAwardContract(XElement awardContract, XNamespace nutsSchema)
        {

            var awardedContract = awardContract.Element("AWARDED_CONTRACT");
            var noAwardedContract = awardContract.Element("NO_AWARDED_CONTRACT");
            var award = new Award()
            {
                AwardedContract = ParseAwardedContract(awardedContract),
                ContractAwarded = ParseContractAwardedType(awardContract),
                NoAwardedContract = ParseNoAwardedContract(noAwardedContract)
            };


            if (awardedContract?.Element("CONTRACTORS") != null)
            {
                foreach (var item in awardedContract?.Element("CONTRACTORS")?.Elements("CONTRACTOR"))
                {
                    var addressContractor = item.Element("ADDRESS_CONTRACTOR");
                    award.AwardedContract.Contractors.Add(new ContractorContactInformation()
                    {
                        OfficialName = addressContractor.Element("OFFICIALNAME")?.Value,
                        NationalRegistrationNumber = addressContractor.Element("NATIONALID")?.Value,
                        PostalAddress = ParsePostalAddress(addressContractor),
                        NutsCodes = ParseNutsCodes(nutsSchema, addressContractor),
                        IsSmallMediumEnterprise = item.Element("SME") != null ? true : false,
                        MainUrl = addressContractor.Element("URL")?.Value,
                        Email = addressContractor.Element("E_MAIL")?.Value,
                        TelephoneNumber = addressContractor.Element("PHONE")?.Value,
                        ValidationState = ValidationState.Valid
                    });

                }

            }

            return award;

        }

        private static ContractAwarded ParseContractAwardedType(XElement awardedContract)
        {
            return awardedContract.Element("NO_AWARDED_CONTRACT") != null ? ContractAwarded.NoAwardedContract :
                   awardedContract.Element("AWARDED_CONTRACT") != null ? ContractAwarded.AwardedContract :
                   ContractAwarded.Undefined;
        }

        private static ContractAward ParseAwardedContract(XElement awardedContract)
        {

            var contractAward = new ContractAward()
            {
                ContractTitle = awardedContract?.Parent?.Element("TITLE").Value,

                LikelyToBeSubcontracted = awardedContract?.Element("LIKELY_SUBCONTRACTED") != null ? true : false,
                ConclusionDate = ParseDate(awardedContract?.Element("DATE_CONCLUSION_CONTRACT")?.Value),

                ValueOfSubcontract = ParseValueContract(awardedContract?.Element("VAL_SUBCONTRACTING")),
                FinalTotalValue = new ValueRangeContract()
                {
                    Currency = awardedContract?.Element("VALUES")?.Element("VAL_RANGE_TOTAL")?.Attribute("CURRENCY")?.Value != null ?
                               awardedContract?.Element("VALUES")?.Element("VAL_RANGE_TOTAL")?.Attribute("CURRENCY")?.Value :
                               awardedContract?.Element("VALUES")?.Element("VAL_TOTAL")?.Attribute("CURRENCY")?.Value,
                    Value = ParseDecimal(awardedContract?.Element("VALUES")?.Element("VAL_TOTAL")?.Value),
                    MaxValue = ParseDecimal(awardedContract?.Element("VALUES")?.Element("VAL_RANGE_TOTAL")?.Element("HIGH")?.Value),
                    MinValue = ParseDecimal(awardedContract?.Element("VALUES")?.Element("VAL_RANGE_TOTAL")?.Element("LOW")?.Value),
                    Type = ParseContractValueType(awardedContract),
                    DisagreeToBePublished = awardedContract?.Element("VALUES")?.Attribute("PUBLICATION")?.Value == "YES" ? false : true,

                },
                InitialEstimatedValueOfContract = new ValueContract
                {
                    Value = ParseDecimal(awardedContract?.Element("VALUES")?.Element("VAL_ESTIMATED_TOTAL")?.Value),
                    Currency = awardedContract?.Element("VALUES")?.Element("VAL_ESTIMATED_TOTAL")?.Attribute("CURRENCY")?.Value
                },
                NotPublicFields = new ContractAwardNotPublicFields()
                {
                    CommunityOrigin = awardedContract?.Element("COUNTRY_ORIGIN")?.Element("COMMUNITY_ORIGIN") != null ? true : false,
                    Countries = new string[] { awardedContract?.Element("COUNTRY_ORIGIN")?.Element("NON_COMMUNITY_ORIGIN")?.Attribute("VALUE").Value },
                    NonCommunityOrigin = awardedContract?.Element("COUNTRY_ORIGIN")?.Element("NON_COMMUNITY_ORIGIN") != null,
                    AbnormallyLowTendersExcluded = awardedContract?.Element("TENDERS_EXCLUDED") != null ? true : false,
                    AwardedToTendererWithVariant = awardedContract?.Element("AWARDED_TENDERER_VARIANT") != null ? true : false,
                    
                },

                NumberOfTenders = ParseNumberOfTenders(awardedContract?.Element("TENDERS")),
                ProportionOfValue = ParseInt(awardedContract?.Element("PCT_SUBCONTRACTING")?.Value),
                SubcontractingDescription = ParsePElements(awardedContract?.Element("INFO_ADD_SUBCONTRACTING"), 0),
                PricePaidForBargainPurchases = new ValueContract()
                {
                    Currency = awardedContract?.Element("VAL_BARGAIN_PURCHASE")?.Attribute("CURRENCY")?.Value,
                    Value = ParseDecimal(awardedContract?.Element("VAL_BARGAIN_PURCHASE")?.Value)
                }
            };



            return contractAward;
        }

        private static ContractValueType ParseContractValueType(XElement awardedContract)
        {
            return awardedContract?.Element("VALUES")?.Element("VAL_TOTAL") != null ? ContractValueType.Exact :
                   awardedContract?.Element("VALUES")?.Element("VAL_RANGE_TOTAL") != null ? ContractValueType.Range :
                   ContractValueType.Undefined;
        }

        private static NonAward ParseNoAwardedContract(XElement noAwardedContract)
        {
            var procurementDiscontinued = noAwardedContract?.Element("PROCUREMENT_DISCONTINUED");

            return new NonAward()
            {
                FailureReason = procurementDiscontinued != null ? ProcurementFailureReason.AwardDiscontinued :
                                noAwardedContract?.Element("PROCUREMENT_UNSUCCESSFUL") != null ? ProcurementFailureReason.AwardNoTenders :
                                ProcurementFailureReason.Undefined,
                OriginalEsender = new Esender()
                {
                    CustomerLogin = procurementDiscontinued?.Element("CUSTOMER_LOGIN")?.Value,
                    TedNoDocExt = procurementDiscontinued?.Element("NO_DOC_EXT")?.Value,
                    Login = procurementDiscontinued?.Element("ESENDER_LOGIN")?.Value

                },
                OriginalNoticeSentDate = ParseDate(procurementDiscontinued?.Element("DATE_DISPATCH_ORIGINAL")?.Value),
                OriginalNoticeSentVia = procurementDiscontinued?.Element("ORIGINAL_TED_ESENDER") != null ? NoticeDeliveryMethod.IcarEsender :
                                        procurementDiscontinued?.Element("ORIGINAL_ENOTICES") != null ? NoticeDeliveryMethod.IcarEnotices :
                                        procurementDiscontinued?.Element("ORIGINAL_OTHER_MEANS") != null ? NoticeDeliveryMethod.OtherMeans : NoticeDeliveryMethod.Undefined,
                OriginalNoticeSentViaOther = procurementDiscontinued?.Element("ORIGINAL_OTHER_MEANS")?.Value
            };
        }

        private static NumberOfTenders ParseNumberOfTenders(XElement tenders)
        {
            return new NumberOfTenders()
            {
                Sme = ParseInt(tenders?.Element("NB_TENDERS_RECEIVED_SME")?.Value),
                OtherEu = ParseInt(tenders?.Element("NB_TENDERS_RECEIVED_SME")?.Value),
                NonEu = ParseInt(tenders?.Element("NB_TENDERS_RECEIVED_NON_EU")?.Value),
                Electronic = ParseInt(tenders?.Element("NB_TENDERS_RECEIVED_EMEANS")?.Value),
                Total = ParseInt(tenders?.Element("NB_TENDERS_RECEIVED")?.Value)
            };
        }

        private static string[] ParseNutsCodes(XNamespace nutsSchema, XElement objectDescription)
        {
            return objectDescription?.Elements(nutsSchema + "NUTS")?.Select(p => p.Attribute("CODE")?.Value).Distinct().ToArray();
        }

        private static EuFunds ParseEuFunds(XElement objectDescription)
        {
            return new EuFunds()
            {
                ProcurementRelatedToEuProgram = objectDescription.Element("EU_PROGR_RELATED") != null,
                ProjectIdentification = objectDescription.Element("EU_PROGR_RELATED")?.Elements("P").Select(p => p.Value).ToArray()
            };
        }

        private static TimeFrame ParseTimeFrame(XElement objectDescription)
        {
            var timeFrame = new TimeFrame()
            {
                BeginDate = ParseDate(objectDescription.Element("DATE_START")?.Value),
                EndDate = ParseDate(objectDescription.Element("DATE_END")?.Value),
                CanBeRenewed = objectDescription.Element("NO_RENEWAL") != null ? false : objectDescription.Element("RENEWAL") != null ? true : false,
                Months = 0,
                Days = 0,
                RenewalDescription = ParsePElements(objectDescription.Element("RENEWAL_DESCR"), 0),
                Type = objectDescription.Element("DURATION")?.Attribute("TYPE")?.Value == "MONTH" ? TimeFrameType.Months :
                       objectDescription.Element("DURATION")?.Attribute("TYPE")?.Value == "DAY" ? TimeFrameType.Days :
                       objectDescription.Element("DATE_START") != null ? TimeFrameType.BeginAndEndDate :
                       TimeFrameType.Undefined
            };

            if (timeFrame.Type == TimeFrameType.Months)
            {
                timeFrame.Months = ParseInt(objectDescription.Element("DURATION")?.Value);
            }
            if (timeFrame.Type == TimeFrameType.Days)
            {
                timeFrame.Days = ParseInt(objectDescription.Element("DURATION")?.Value);
            }

            return timeFrame;
        }

        private static CpvCode[] ParseAdditionalCpvCodes(XElement objectDescription)
        {
            return objectDescription?.Elements("CPV_ADDITIONAL")?.Select(element => new CpvCode
            {
                Code = element?.Element("CPV_CODE")?.Attribute("CODE")?.Value,
                VocCodes = element?.Elements("CPV_SUPPLEMENTARY_CODE")?.Select(s => new VocCode { Code = s.Attribute("CODE")?.Value }).ToArray()
            }).ToArray();
        }

        private static OptionsAndVariants ParseOptionsAndVariants(XElement objectDescription)
        {
            OptionsAndVariants optionsAndVariants = new OptionsAndVariants()
            {
                VariantsWillBeAccepted = objectDescription.Element("NO_ACCEPTED_VARIANTS") != null ? false :
                                         objectDescription.Element("ACCEPTED_VARIANTS") != null ? true :
                                         false,
                Options = objectDescription.Element("OPTIONS") != null ? true :
                          objectDescription.Element("NO_OPTIONS") != null ? false :
                          false
            };

            if (optionsAndVariants.Options)
            {
                optionsAndVariants.OptionsDescription = ParsePElements(objectDescription.Element("OPTIONS_DESCR"), 0);
            }

            return optionsAndVariants;
        }

        private static ValueContract ParseValueContract(XElement valueElement)
        {
            if (valueElement != null)
            {
                return new ValueContract()
                {
                    Currency = valueElement.Attribute("CURRENCY")?.Value,
                    Value = ParseDecimal(valueElement.Value)
                };
            }
            return new ValueContract();
        }

        private static ValueRangeContract ParseValueRangeContract(XElement valueElement, XElement rangeElement = null)
        {
            if (valueElement != null)
            {
                return new ValueRangeContract()
                {
                    Currency = valueElement.Attribute("CURRENCY")?.Value,
                    Value = ParseDecimal(valueElement.Value),
                    Type = valueElement.Name.LocalName == "VAL_TOTAL" ? ContractValueType.Exact : ContractValueType.Undefined,
                    DisagreeToBePublished = valueElement?.Attribute("PUBLICATION")?.Value == "NO" ? true : false ,
                };
            }else if (rangeElement != null)
            {
              return new ValueRangeContract()
              {
                Currency = rangeElement?.Attribute("CURRENCY")?.Value,
                MaxValue = ParseDecimal(rangeElement?.Element("HIGH")?.Value),
                MinValue = ParseDecimal(rangeElement?.Element("LOW")?.Value),
                Type = ContractValueType.Range,
                DisagreeToBePublished = rangeElement?.Attribute("PUBLICATION")?.Value == "NO" ? true : false,
              };
            }

            return new ValueRangeContract();

        }

        private static AwardCriteria ParseCriterionTypes(XElement acElement)
        {
            var ac = new AwardCriteria()
            {
                CriterionTypes = AwardCriterionType.Undefined
            };

            if (acElement == null)
            {
                return ac;
            };

            var hasDescriptiveCriteria = acElement?.Element("AC_PROCUREMENT_DOC") != null;

            if (hasDescriptiveCriteria)
            {
                ac.CriterionTypes = AwardCriterionType.DescriptiveCriteria;
                return ac;
            };

            Func<XElement, AwardCriterionDefinition> parseCriterion = (XElement acc) => new AwardCriterionDefinition()
            {
                Criterion = acc.Element("AC_CRITERION")?.Value,
                Weighting = acc.Element("AC_WEIGHTING")?.Value
            };

            if (acElement.Elements("AC_QUALITY").Any())
            {
                ac.CriterionTypes |= AwardCriterionType.QualityCriterion;
                ac.QualityCriteria = acElement.Elements("AC_QUALITY").Select(parseCriterion).ToArray();
            }

            if (acElement.Elements("AC_PRICE").Any())
            {
                ac.CriterionTypes |= AwardCriterionType.PriceCriterion;
                ac.PriceCriterion = parseCriterion(acElement.Element("AC_PRICE"));
            }

            if (acElement.Elements("AC_COST").Any())
            {
                ac.CriterionTypes |= AwardCriterionType.CostCriterion;
                ac.CostCriteria = acElement.Elements("AC_COST").Select(parseCriterion).ToArray();
            }

            return ac;
        }

        private static ComplementaryInformation ParseComplementaryInformation(XElement complementaryInfo)
        {
            return new ComplementaryInformation()
            {
                AdditionalInformation = ParsePElements(complementaryInfo?.Element("INFO_ADD"), 0),
                IsRecurringProcurement = complementaryInfo?.Element("RECURRENT_PROCUREMENT") != null,
                ElectronicInvoicingUsed = complementaryInfo?.Element("EINVOICING") != null,
                ElectronicOrderingUsed = complementaryInfo?.Element("EORDERING") != null,
                ElectronicPaymentUsed = complementaryInfo?.Element("EPAYMENT") != null,
                EstimatedTimingForFurtherNoticePublish = ParsePElements(complementaryInfo?.Element("ESTIMATED_TIMING"), 0),
                ValidationState = ValidationState.Valid
            };
        }

        private static ProcurementProjectContract ParseProject(XNamespace nutsSchema, XElement f01, XElement objectContract, XElement addressContractinBody, XElement contractingBody, NoticeType noticeType, string directive)
        {
            var procurementProjectContract = new ProcurementProjectContract()
            {
                Title = objectContract.Element("TITLE")?.Element("P")?.Value,
                Organisation = ParseOrganisation(nutsSchema, addressContractinBody, contractingBody, noticeType),
                ContractType = ParseContractType(objectContract.Element("TYPE_CONTRACT").Attribute("CTYPE").Value),
                ValidationState = ValidationState.Valid,
                CentralPurchasing = contractingBody.Element("CENTRAL_PURCHASING") != null ? true : false,
                ReferenceNumber = objectContract.Element("REFERENCE_NUMBER")?.Value,
                ProcurementCategory = GetProcurementCategoryByDirective(directive),
                JointProcurement = contractingBody.Element("JOINT_PROCUREMENT_INVOLVED") != null,
                ProcurementLaw = ParsePElements(contractingBody.Element("PROCUREMENT_LAW")),
                Publish = PublishType.ToTed
            };

            foreach (var additionalAddressInfo in contractingBody.Elements("ADDRESS_CONTRACTING_BODY_ADDITIONAL"))
            {
                procurementProjectContract.CoPurchasers.Add(ParseContractingBodyInformation(nutsSchema, additionalAddressInfo));
            }

            return procurementProjectContract;

        }

        private static OrganisationContract ParseOrganisation(XNamespace nutsSchema, XElement addressContractinBody,
          XElement contractingBody, NoticeType noticeType)
        {
          var organisationContract = new OrganisationContract()
          {
            Information = ParseContractingBodyInformation(nutsSchema, addressContractinBody),


          };

          if (noticeType.IsUtilities())
          {

            if (contractingBody.Element("CE_ACTIVITY_OTHER") != null)
            {
              organisationContract.MainActivityUtilities = MainActivityUtilities.OtherActivity;
              organisationContract.OtherMainActivity = contractingBody.Element("CE_ACTIVITY_OTHER")?.Value;
            }
            else if (contractingBody.Element("CE_ACTIVITY") != null)
            {
              organisationContract.MainActivityUtilities =
                FromTEDFormat(contractingBody.Element("CE_ACTIVITY")?.Attribute("VALUE")?.Value);
            }
            else if (contractingBody.Element("CA_ACTIVITY_OTHER") != null)
            {
              organisationContract.MainActivity = MainActivity.OtherActivity;
              organisationContract.OtherMainActivity = contractingBody.Element("CA_ACTIVITY_OTHER")?.Value;
            }
            else
            {
              organisationContract.MainActivity =
                FromTEDFormatMainActivity(contractingBody.Element("CA_ACTIVITY")?.Attribute("VALUE")?.Value);
            }
          }
          else
          {
            if (contractingBody.Element("CA_TYPE_OTHER") != null)
            {
              organisationContract.ContractingAuthorityType = ContractingAuthorityType.OtherType;
              organisationContract.OtherContractingAuthorityType = contractingBody.Element("CA_TYPE_OTHER")?.Value;
            }
            else if (contractingBody.Element("CA_TYPE") != null)
            {
              organisationContract.ContractingAuthorityType =
                FromTEDFormatContractingAuthorityType(contractingBody.Element("CA_TYPE")?.Attribute("VALUE")?.Value);
            }

            if (contractingBody.Element("CA_ACTIVITY_OTHER") != null)
            {
              organisationContract.MainActivity = MainActivity.OtherActivity;
              organisationContract.OtherMainActivity = contractingBody.Element("CA_ACTIVITY_OTHER")?.Value;
            }
            else
            {
              organisationContract.MainActivity =
                FromTEDFormatMainActivity(contractingBody.Element("CA_ACTIVITY")?.Attribute("VALUE")?.Value);
            }

          }

          return organisationContract;
        }


        private static ProcurementCategory GetProcurementCategoryByDirective(string value)
        {
            switch (value)
            {
                case "32009L0081":
                    return ProcurementCategory.Defence;
                case "32014L0023":
                    return ProcurementCategory.Lisence;
                case "32014L0024":
                    return ProcurementCategory.Public;
                case "32014L0025":
                    return ProcurementCategory.Utility;
                default:
                    return ProcurementCategory.Public;
            }
        }

        private static ContractingAuthorityType FromTEDFormatContractingAuthorityType(string type)
        {
            switch (type)
            {
                case "STATE_AUTHORITY":
                case "MINISTRY":
                    return ContractingAuthorityType.MaintypeMinistry;
                case "NATIONAL_AGENCY":
                    return ContractingAuthorityType.MaintypeNatagency;
                case "REGIONAL_AUTHORITY":
                case "MUNICIPALITY":
                    return ContractingAuthorityType.MaintypeLocalauth;
                case "REGIONAL_AGENCY":
                    return ContractingAuthorityType.MaintypeLocalagency;
                case "STATE_ENTERPRISE":
                case "BODY_PUBLIC":
                    return ContractingAuthorityType.MaintypePublicbody;
                case "EU_INSTITUTION":
                    return ContractingAuthorityType.MaintypeEu;
                case "STATE_CHURCH":
                    return ContractingAuthorityType.MaintypeChurch;
                case "OTHER":
                    return ContractingAuthorityType.OtherType;
                default:
                    return ContractingAuthorityType.Undefined;
            }
        }

        private static MainActivityUtilities FromTEDFormat(string activity)
        {
            switch (activity)
            {
                case "PRODUCTION_TRANSPORT_DISTRIBUTION_GAS_HEAT":
                    return MainActivityUtilities.MainactivGasProduct;
                case "ELECTRICITY":
                    return MainActivityUtilities.MainactivElectricity;
                case "EXPLORATION_EXTRACTION_GAS_OIL":
                    return MainActivityUtilities.MainactivGasExplor;
                case "EXPLORATION_EXTRACTION_COAL_OTHER_SOLID_FUEL":
                    return MainActivityUtilities.MainactivCoal;
                case "WATER":
                    return MainActivityUtilities.MainactivWater;
                case "POSTAL_SERVICES":
                    return MainActivityUtilities.MainactivPostal;
                case "RAILWAY_SERVICES":
                    return MainActivityUtilities.MainactivRailway;
                case "URBAN_RAILWAY_TRAMWAY_TROLLEYBUS_BUS_SERVICES":
                    return MainActivityUtilities.MainactivBus;
                case "PORT_RELATED_ACTIVITIES":
                    return MainActivityUtilities.MainactivPort;
                case "AIRPORT_RELATED_ACTIVITIES":
                    return MainActivityUtilities.MainactivAirportrelated;
                default:
                    return MainActivityUtilities.Undefined;
            }
        }
        private static MainActivity FromTEDFormatMainActivity(string activity)
        {
            switch (activity)
            {
                case "GENERAL_PUBLIC_SERVICES":
                    return MainActivity.MainactivGeneral;
                case "DEFENCE":
                    return MainActivity.MainactivDefence;
                case "ECONOMIC_AND_FINANCIAL_AFFAIRS":
                    return MainActivity.MainactivEconomic;
                case "EDUCATION":
                    return MainActivity.MainactivEducation;
                case "ENVIRONMENT":
                    return MainActivity.MainactivEnvironment;
                case "HEALTH":
                    return MainActivity.MainactivHealth;
                case "HOUSING_AND_COMMUNITY_AMENITIES":
                    return MainActivity.MainactivHousing;
                case "PUBLIC_ORDER_AND_SAFETY":
                    return MainActivity.MainactivSafety;
                case "RECREATION_CULTURE_AND_RELIGION":
                    return MainActivity.MainactivCulture;
                case "SOCIAL_PROTECTION":
                    return MainActivity.MainactivSocial;
                default:
                    return MainActivity.Undefined;
            }
        }

        private static ContractType ParseContractType(string value)
        {
            switch (value)
            {
                case "SUPPLIES":
                    return ContractType.Supplies;
                case "SERVICES":
                    return ContractType.Services;
                case "SOCIALSERVICES":
                    return ContractType.SocialServices;
                case "WORKS":
                    return ContractType.Works;
                //   case "DESIGN_CONTEST":      -return?
                case "EDUCATION_SERVICES_WITH_EMPLOYMENT_AUTHORITY":
                    return ContractType.EducationalServices;
                default:
                    return ContractType.Undefined;
            }
        }

        private static DateTime? ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return new DateTime?();
            }
            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private static ContractBodyContactInformation ParseContractingBodyInformation(XNamespace nutsSchema, XElement addressContractinBody)
        {
            return new ContractBodyContactInformation()
            {
                OfficialName = addressContractinBody.Element("OFFICIALNAME")?.Value != null ?
                               addressContractinBody.Element("OFFICIALNAME")?.Value :
                               addressContractinBody.Element("ORGANISATION")?.Element("OFFICIALNAME")?.Value,
                NationalRegistrationNumber = addressContractinBody.Element("NATIONALID")?.Value != null ?
                                             addressContractinBody.Element("NATIONALID")?.Value :
                                             addressContractinBody.Element("ORGANISATION")?.Element("NATIONALID")?.Value,
                PostalAddress = ParsePostalAddress(addressContractinBody),
                ContactPerson = addressContractinBody.Element("CONTACT_POINT")?.Value,
                TelephoneNumber = addressContractinBody.Element("PHONE")?.Value,
                Email = addressContractinBody.Element("E_MAIL")?.Value != null ? addressContractinBody.Element("E_MAIL")?.Value :
                        addressContractinBody.Element("E_MAILS")?.Element("E_MAIL")?.Value != null ? addressContractinBody.Element("E_MAILS")?.Element("E_MAIL")?.Value : "",
                NutsCodes = new[] {
                    addressContractinBody.Element(nutsSchema + "NUTS")?.Attribute("CODE")?.Value
                },
                MainUrl = addressContractinBody.Element("URL_GENERAL")?.Value,
                BuyerProfileUrl = addressContractinBody.Element("URL_BUYER")?.Value,
                ValidationState = ValidationState.Valid
            };
        }

        private static PostalAddress ParsePostalAddress(XElement addressData)
        {
            return new PostalAddress
            {
                StreetAddress = addressData?.Element("ADDRESS")?.Value,
                Town = addressData?.Element("TOWN")?.Value,
                PostalCode = addressData?.Element("POSTAL_CODE")?.Value,
                Country = addressData?.Element("COUNTRY")?.Attribute("VALUE")?.Value
            };
        }

        private static string[] ParsePElements(XElement pElements, int lengthLimit = 0)
        {
            if (pElements == null)
            {
                return new string[0];
            }

            var paragraphArray = pElements.Elements("P").Select(p => p.Value).ToArray();
            var totalLength = 0;

            if (lengthLimit > 0)
            {
                return paragraphArray.TakeWhile(p => (totalLength += p.Length) < lengthLimit).ToArray();
            }

            return paragraphArray;
        }



        private static NoticeType ParserNoticeType(INoticeImportModel editaNotice, out bool isCorrigendum)
        {
            isCorrigendum = false;
            NoticeType noticeType;
            switch (editaNotice.FormNumber)
            {
                case "1":
                    if (editaNotice.NoticeType == "PRI_REDUCING_TIME_LIMITS".ToLower())
                    {
                        noticeType = NoticeType.PriorInformationReduceTimeLimits;
                    }
                    else
                    {
                        noticeType = NoticeType.PriorInformation;
                    }
                    break;
                case "2":
                    noticeType = NoticeType.Contract;
                    break;
                case "3":
                    noticeType = NoticeType.ContractAward;
                    break;
                case "5":
                    noticeType = NoticeType.ContractUtilities;
                    break;
                case "6":
                    noticeType = NoticeType.ContractAwardUtilities;
                    break;
                case "14":
                    noticeType = NoticeType.Undefined;
                    isCorrigendum = true;
                    break;
                case "99":
                    switch (editaNotice.NoticeType)
                    {
                        case "domestic_contract":
                            noticeType = NoticeType.NationalContract;
                            break;
                        case "request_for_information":
                            noticeType = NoticeType.NationalPriorInformation;
                            break;
                        case "procurement_discontinued":
                            noticeType = NoticeType.Undefined;
                            break;
                        case "corrigendum_notice":
                            noticeType = NoticeType.NationalContract;
                            isCorrigendum = true;
                            break;
                        default:
                            noticeType = NoticeType.Undefined;
                            break;
                    }
                    break;
                case "21":
                    switch (editaNotice.NoticeType)
                    {
                        case "contract":
                            noticeType = NoticeType.SocialContract;
                            break;
                        case "award_contract":
                            noticeType = NoticeType.SocialContractAward;
                            break;
                        case "pri_only":
                            noticeType = NoticeType.SocialPriorInformation;
                            break;
                        default:
                            noticeType = NoticeType.Undefined;
                            break;
                    }
                    break;
                default:
                    noticeType = NoticeType.Undefined;
                    break;
            }
            return noticeType;
        }

        private XElement ResolveFormElement(NoticeType noticeType, XElement formSection, INoticeImportModel form)
        {
            switch (noticeType)
            {
                case NoticeType.PriorInformation:
                case NoticeType.PriorInformationReduceTimeLimits:
                    return formSection?.Element("F01_2014");
                case NoticeType.Contract:
                    return formSection?.Element("F02_2014");
                case NoticeType.ContractAward:
                    return formSection?.Element("F03_2014");
                case NoticeType.ContractUtilities:
                    return formSection?.Element("F05_2014");
                case NoticeType.ContractAwardUtilities:
                    return formSection?.Element("F06_2014");
                case NoticeType.SocialContract:
                case NoticeType.SocialPriorInformation:
                case NoticeType.SocialContractAward:
                    return formSection?.Element("F21_2014");
                case NoticeType.NationalContract:
                case NoticeType.NationalPriorInformation:
                    return formSection?.Element("DOMESTIC_CONTRACT"); //form 99
                default:
                    throw new NotSupportedException($"Notice type: {noticeType} , noticeNumber:{form.NoticeNumber}, formNumber:{form.FormNumber}");
            }
        }
    }
}
