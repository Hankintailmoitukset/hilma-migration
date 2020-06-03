using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using AutoMapper;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Extensions;
using Hilma.Domain.Integrations;
using Hilma.Domain.Integrations.Defence;
using System.Runtime.InteropServices;

namespace Hilma.Domain.Validators
{
    public class NoticeValidator
    {
        public IEnumerable<string> ValidationErrors => _validationErrors.AsReadOnly();

        private readonly Notice _notice;
        private readonly List<string> _validationErrors = new List<string>();
        private static readonly XNamespace XmlnsGeneral = "http://publications.europa.eu/resource/schema/ted/R2.0.9/reception";
        private static readonly XNamespace XmlnsDefence = "http://publications.europa.eu/resource/schema/ted/R2.0.8/reception";
        private readonly IMapper _mapper;
        private readonly ITranslationProvider _translationProvider;

        /// <summary>
        /// The notice validator constructor
        /// </summary>
        /// <param name="notice">The notice to be validated</param>
        /// <param name="mapper">Mapper needed for TED schema validation</param>
        /// <param name="translationProvider">Translation provider needed for </param>
        public NoticeValidator(Notice notice, IMapper mapper, ITranslationProvider translationProvider)
        {
            _notice = notice ?? throw new ArgumentNullException(nameof(notice));
            _mapper = mapper;
            _translationProvider = translationProvider;
        }

        public bool Validate(out string tedXml)
        {
            if( _notice.IsCorrigendum) {

                if( _notice.Parent == null ) {
                    
                    _validationErrors.Add( "Corrigendum notice should have parent set. Parent is null.");
                    tedXml = string.Empty;
                    return false;
                } 

                // Validate that the notice is not expired
                bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                var localFinnishTimezone = isWindows ? TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time") : TimeZoneInfo.FindSystemTimeZoneById("Europe/Helsinki");
                var timeInLocalTimezone = TimeZoneInfo.ConvertTime(DateTime.UtcNow, localFinnishTimezone);
                var expirationTime = _notice.Parent.TenderingInformation?.TendersOrRequestsToParticipateDueDateTime; 

                if( expirationTime.HasValue && expirationTime.Value <= timeInLocalTimezone ) {
                    
                    _validationErrors.Add($"Corrigendum notice cannot be made to notice that has expired TendersOrRequestsToParticipateDueDateTime: ExpirationTime: {expirationTime}, Current local time: {timeInLocalTimezone}");
                    
                    tedXml = string.Empty;
                    return false; 
                }

            } 
            
            if (_notice.Type.IsNational())
            {
                // Do not validate national notices
                tedXml = null;
                return true;
            }

            
            var path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            var schema = new XmlSchemaSet { XmlResolver = new XmlUrlResolver() };
            var nameSpace = XmlnsGeneral;
            var isDefence = _notice.Type.IsDefence();

            if (isDefence)
            {
                schema.Add(XmlnsDefence.ToString(), Path.Combine(path, "Validators", "TedSchema", "208Defence", "TED_ESENDERS.xsd"));
                nameSpace = XmlnsDefence;
            }
            else
            {
                schema.Add(XmlnsGeneral.ToString(), Path.Combine(path, "Validators", "TedSchema", "209General", "TED_ESENDERS.xsd"));
            }
            schema.Compile();

            var noticeContract = _mapper.Map<NoticeContract>(_notice);
            var parent = _notice.Parent != null ? _mapper.Map<NoticeContract>(_notice.Parent) : new NoticeContract();
            var xDoc = new TedNoticeFactory(noticeContract, parent, "Validator", "validator@validator.com", "TEDEXXXX", _translationProvider).CreateDocument();
            var copy = new XDocument(xDoc);

            if (isDefence)
            {
                // Add namespace attribute to fix the validation
                copy = TedHelpers.SetRootNamespace(copy);
            }
                 
            copy.Validate(schema, (sender, e) => { _validationErrors.Add(e.Message + "\n"); });

           

            // We don't want to send the login part in the response.
            if (isDefence)
            {
                // defense version does not use root namespace in ted xml
                var descendants = xDoc.Descendants("FORM_SECTION");
                tedXml = string.Join("\n", descendants);
            } else
            {                
                var descendants = xDoc.Descendants(nameSpace + "FORM_SECTION");
                tedXml = string.Join("\n", descendants);
            }
            
            var teSchemaValid = Valid(!_validationErrors.Any(), "TED message formed:\n" + tedXml);

            return ValidateAll(teSchemaValid, ValidateValueFields(noticeContract));
        }

        public bool ValidateAll(params bool[] validationResults)
        {
            return validationResults.All(r => r);
        }

        public bool ValidateAll(IEnumerable<bool> validationResults)
        { 
            return validationResults.All(r => r);
        }
        #region Partial validators

        public bool ValidateValueFields(NoticeContract notice)
        {
            bool VerifyDisagreeToPublish(ValueRangeContract value, string path)
            {
                if ((value?.DisagreeToBePublished ?? false) == false)
                {
                    return true;
                }

                _validationErrors.Add($"Value disagreeToBePublished == true for {path} is not valid for notice type {notice.Type}");

                return false;
            }

            bool VerifyProcurementEstimatedValue(ValueRangeContract estimatedValue, string path)
            {
                // Only national notices can hide procurement estimated value value
                if (notice.Type.IsNational())
                {
                    return true;
                }

                return VerifyDisagreeToPublish(estimatedValue, path);
            }

            bool VerifyTotalValue(ValueRangeContract totalValue, string path)
            {
                // Only ContractAwardUtilities can hide total value
                if (notice.Type == NoticeType.ContractAwardUtilities)
                {
                    return true;
                }
                
                return VerifyDisagreeToPublish(totalValue, path);
            }
            
            return ValidateAll(
                VerifyProcurementEstimatedValue( notice.ProcurementObject?.EstimatedValue, "notice.EstimatedValue"),
                VerifyDisagreeToPublish(notice.ProcurementObject?.Defence?.TotalQuantityOrScope, $"notice.Defence.TotalQuantityOrScope"),
                VerifyTotalValue(notice.ProcurementObject?.TotalValue, "notice.TotalValue" ),
                ValidateAll( notice.ObjectDescriptions.Select( (o,ix) => ValidateAll(
                    VerifyDisagreeToPublish(o.EstimatedValue,
                        $"notice.ObjectDescriptions[{ix}].EstimatedValue"),
                    VerifyTotalValue(
                        o.AwardContract?.AwardedContract?.FinalTotalValue,
                        $"notice.ObjectDescriptions[{ix}].AwardContract.AwardedContract.FinalTotalValue"))
                )));
        }

        public bool Validate(TenderingInformation info)
        {
            if (!Valid(info != null, "TenderingInformation"))
            {
                return false;
            }

            if (_notice.Type != NoticeType.Contract)
            {
                return true;
            }

            var procedureType = _notice.ProcedureInformation.ProcedureType;
            var isOpenProcedure = procedureType == ProcedureType.ProctypeOpen;

            var openingConditions = info.TenderOpeningConditions;
            return ValidateAll(Valid(info.TendersOrRequestsToParticipateDueDateTime != null, "TenderingInformation.TendersOrRequestsToParticipateDueDateTime"),
                               Valid(info.Languages.Any(), "TenderingInformation.Languages"),
                               Valid(!isOpenProcedure || openingConditions != null, "TenderingInformation.TenderOpeningConditions"),
                               Valid(!isOpenProcedure || openingConditions?.OpeningDateAndTime != null && openingConditions?.OpeningDateAndTime > info.TendersOrRequestsToParticipateDueDateTime, "TenderingInformation.TenderOpeningConditions.OpeningDateAndTime")
                              );
        }

        public bool Validate(CommunicationInformation info) =>
            ValidateAll(Valid(info != null, "CommunicationInformation"),
                        Valid(_notice.Type != NoticeType.Contract || info?.ProcurementDocumentsAvailable != ProcurementDocumentAvailability.Undefined,
                                "CommunicationInformation.ProcurementDocumentsAvailable"),
                        Valid(info?.AdditionalInformation != AdditionalInformationAvailability.Undefined || _notice.Type == NoticeType.NationalAgricultureContract,
                                "CommunicationInformation.AdditionalInformation"),
                        Valid(info?.AdditionalInformation != AdditionalInformationAvailability.AddressAnother ||
                                Validate("CommunicationInformation.AdditionalInformationAddress", info.AdditionalInformationAddress),
                                         "CommunicationInformation.AdditionalInformation+AddressAnother"),
                        Valid(info?.SendTendersOption != TenderSendOptions.AddressSendTenders || !string.IsNullOrWhiteSpace(info.ElectronicAddressToSendTenders),
                                "CommunicationInformation.SendTendersOption+ElectronicAddressToSendTenders"),
                        Valid(_notice.Type != NoticeType.Contract || info.SendTendersOption != TenderSendOptions.Undefined,
                                "CommunicationInformation.SendTendersOption"),
                        Valid(info != null && (!info.ElectronicCommunicationRequiresSpecialTools ||
                                !string.IsNullOrWhiteSpace(info.ElectronicCommunicationInfoUrl)),
                                "CommunicationInformation: ElectronicCommunicationInfoUrl"));



        public bool Validate(string name, ContractBodyContactInformation info)
        {
            return ValidateAll(Valid(info != null, $"{name}"),
                               Valid(info != null && info.NutsCodes?.Any() == true, $"{name}.NutsCodes"),
                               Valid(!string.IsNullOrWhiteSpace(info.OfficialName), $"{name}.OfficialName"),
                               Valid(!string.IsNullOrEmpty(info.MainUrl), $"{name}.MainUrl"),
                               Valid(!string.IsNullOrEmpty(info.Email), $"{name}.Email"),
                               Validate(info.PostalAddress));
        }

        public bool Validate(ContactPerson person)
        {
            if (_notice.Type == NoticeType.ConcessionAward)
            {
                return true;
            }
            return Valid(person != null && !string.IsNullOrWhiteSpace(person.Email), "ContactPerson");
        }

        public bool ValidateObjectDescriptions(Notice notice)
        {
            var expectedNumberOfDescriptions = _notice.LotsInfo.DivisionLots ? _notice.LotsInfo.QuantityOfLots : 1;

            return ValidateAll(
                Valid(_notice.ObjectDescriptions != null, "ObjectDescriptions"),
                Valid(_notice.ObjectDescriptions?.Length == expectedNumberOfDescriptions, "Object description count does not match"),
                   _notice.ObjectDescriptions?.All(Validate) ?? false);
        }

        public bool Validate(ObjectDescription objectDescription) =>
            ValidateAll(Valid((objectDescription.DescrProcurement).HasAnyContent() || _notice.Type.IsDefence(), "ObjectDescription.DescrProcurement"),
                Valid(objectDescription.NutsCodes.Any()
                      || _notice.Type.IsDefence()
                      || _notice.Type == NoticeType.DesignContest
                      || _notice.Type == NoticeType.NationalDesignContest
                      || _notice.Type == NoticeType.NationalAgricultureContract, "ObjectDescription.NutsCodes"),
                Valid(!objectDescription.TimeFrame.CanBeRenewed || (objectDescription.TimeFrame.CanBeRenewed && (objectDescription.TimeFrame?.RenewalDescription).HasAnyContent()), "ObjectDescription.TimeFrame.RenewalDescription"),
                Valid(!(objectDescription.AwardCriteria.CriterionTypes == AwardCriterionType.DescriptiveCriteria && _notice.Type == NoticeType.ContractAward), "Award criteria cannot be descriptive for contract award."),
                Valid((_notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1 ||
                    _notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1Other ||
                    _notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD4 ||
                    _notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD4Other) ? objectDescription.AwardContract?.ContractAwarded == ContractAwarded.AwardedContract : true, "Contract must be awarded if procedure type = awarded without prior publication")
            );

        public bool Validate(ProcurementProjectContract project, NoticeType type)
        {
            return ValidateAll(Valid(project != null && project.Id != 0, "ProcurementProjectContract"),
                               Valid(project?.ContractType != ContractType.Undefined, "ProcurementProjectContract.ContractType"),
                               Valid(!string.IsNullOrWhiteSpace(project?.Title), "ProcurementProjectContract.Title"),
                               Validate(project?.Organisation, type, project));
        }

        public bool Validate(ProcedureInformation info)
        {
            if (_notice.Type != NoticeType.Contract)
            {
                return true;
            }

            var canAccelerateTypes = new[]
                {ProcedureType.ProctypeOpen, ProcedureType.ProctypeRestricted, ProcedureType.ProctypeCompNegotiation};
            return ValidateAll(Valid(info != null, $"ProcedureInformation"),
                Valid(info != null && info.ProcedureType != ProcedureType.Undefined, "ProcedureInformation.ProcedureType"),
                Valid(info != null && (!info.AcceleratedProcedure || canAccelerateTypes.Contains(info?.ProcedureType ?? ProcedureType.Undefined) && (info?.JustificationForAcceleratedProcedure).HasAnyContent()), "ProcedureInformation.AcceleratedProcedure + JustificationForAcceleratedProcedure"),
                Validate(info.FrameworkAgreement, info)
                );
        }

        public bool Validate(FrameworkAgreementInformation frameworkAgreement, ProcedureInformation procedure)
        {
            if (_notice.Type != NoticeType.Contract)
            {
                return true;
            }

            return ValidateAll(
                Valid(frameworkAgreement != null, "FrameworkAgreement null"),
                Valid(!(frameworkAgreement?.IncludesDynamicPurchasingSystem ?? true) ||
                         procedure.ProcedureType == ProcedureType.ProctypeRestricted,
                      "Procedure type must be rescricted when IncludesDynamicPurchasingSystem"),
                 Valid(!(frameworkAgreement?.IncludesDynamicPurchasingSystem ?? true) ||
                         _notice.Project.CentralPurchasing || !frameworkAgreement.DynamicPurchasingSystemInvolvesAdditionalPurchasers,
                      "DynamicPurchasingSystemInvolvesAdditionalPurchasers cannot be selected when Project.CentralPurchasing is not set")


            );
        }

        public bool Valid(bool isValid, string objectName)
        {
            if (!isValid)
            {
                _validationErrors.Add(objectName);
            }

            return isValid;
        }

        public bool Validate(ProcurementObject procurementObject)
        {
            return ValidateAll(Valid(procurementObject != null, "ProcurementObject"),
                               Valid(!string.IsNullOrEmpty(procurementObject?.MainCpvCode?.Code) || _notice.Type == NoticeType.NationalAgricultureContract, "ProcurementObject.MainCpvCode"),
                               Valid(string.IsNullOrEmpty(_notice.PreviousNoticeOjsNumber) || Regex.Match(_notice.PreviousNoticeOjsNumber, @"(19|20)\d{2}\/S (((00)?[1-9])|([0]?[1-9][0-9])|(1[0-9][0-9])|(2[0-5][0-9]))-\d{6}").Success, "PreviousNoticeOjsNumber format incorrect"),
                               Valid(procurementObject?.ShortDescription.HasAnyContent() == true
                                      || _notice.Type.IsDefence()
                                      || _notice.Type == NoticeType.DesignContest
                                      || _notice.Type == NoticeType.NationalDesignContest
                                      || _notice.Type == NoticeType.NationalAgricultureContract, "ProcurementObject.ShortDescription"));
        }

        public bool Validate(OrganisationContract organisation, NoticeType type, ProcurementProjectContract project)
        {
            var isUtilitiesNotice = type.IsUtilities() || (type == NoticeType.ExAnte && project.ProcurementCategory == ProcurementCategory.Utility);
            return ValidateAll(Valid(organisation != null, "Organisation"),
                               Valid(isUtilitiesNotice
                                ? organisation?.MainActivityUtilities != MainActivityUtilities.Undefined
                                : (organisation?.MainActivity != MainActivity.Undefined || organisation?.ContractingAuthorityType == ContractingAuthorityType.MaintypeFarmer), "Organisation.MainActivity"),
                               Valid(organisation?.ContractingAuthorityType != ContractingAuthorityType.Undefined, "Organisation.ContractingAuthorityType"),
                               Valid(organisation?.Id != Guid.Empty, "Organisation.Id"),
                               Validate(organisation?.Information));

        }

        public bool Validate(PostalAddress postalAddress)
        {
            return ValidateAll(Valid(postalAddress != null, "PostalAddress"),
                               Valid(!string.IsNullOrWhiteSpace(postalAddress?.Country), "PostalAddress.Country"),
                               Valid(!string.IsNullOrWhiteSpace(postalAddress?.Town), "PostalAddress.Town"));
        }

        public bool Validate(ContractBodyContactInformation info)
        {
            return ValidateAll(Valid(info != null, "Information"),
                Valid(!string.IsNullOrWhiteSpace(info?.NationalRegistrationNumber), "Information.NationalRegistrationNumber"),
                Valid(info != null && info.NutsCodes.Any(), "Information.NutsCodes"),
                Valid(!string.IsNullOrWhiteSpace(info?.OfficialName), "Information.OfficialName"),
                Valid(!string.IsNullOrWhiteSpace(info?.MainUrl), "ContactInformation.MainUrl"),
                Validate(info?.PostalAddress));
        }

        public bool Validate(LotsInfo lotsInfo)
        {
            return Valid(lotsInfo != null &&
                   (!lotsInfo.DivisionLots || lotsInfo.QuantityOfLots >= 2), "LotsInfo.QuantityOfLots");
        }


        private bool Validate(Modifications modifications)
        {
            if (_notice.Type != NoticeType.Modification)
            {
                return true;
            }

            return ValidateAll(Valid(modifications.Description != null, "Modifications.Description"),
                    Valid(modifications.Reason != ModificationReason.Undefined, "Modifications.Reason"));
        }
        #endregion
    }
}
