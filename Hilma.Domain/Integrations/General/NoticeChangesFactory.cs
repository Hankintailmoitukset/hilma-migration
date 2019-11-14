using System;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hilma.Domain.Entities.Annexes;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// Factory for generating changes -node for Corrigendum notice
    /// </summary>
    public class NoticeChangesFactory
    {
        // R2.0.8 doesn't have corrigendums.
        private static readonly XNamespace xmlns = "http://publications.europa.eu/resource/schema/ted/R2.0.9/reception";

        // Notice language
        private static string Language;

        /// <summary>
        /// Get notice changes.
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice, if applicable</param>
        /// <returns>A list of XElements with the changes.</returns>
        public static List<XElement> Changes(NoticeContract notice, NoticeContract parent)
        {
            var changes = new List<XElement>
            {
                parent.TedValidationErrors?.Any() == true ?
                new XElement(xmlns + "PUBLICATION_TED", new XAttribute("PUBLICATION", "NO")) :
                new XElement(xmlns + "MODIFICATION_ORIGINAL", new XAttribute("PUBLICATION", "NO"))
            };

            Language = notice.Language;

            // Procurement project section
            Project(notice, parent, changes);

            // Section I: Contracting Authority
            ContractingAuthority(notice, parent, changes);

            // Section II: Object
            Object(notice, parent, changes);

            // Section III: Legal, economic, financial and technical information
            Conditions(notice, parent, changes);

            // Section IV: Procedure - type, language
            Procedure(notice, parent, changes);

            // Section V: Award of contract
            Award(notice, parent, changes);

            // Section VI: Complementary information
            ComplementaryInfo(notice, parent, changes);

            // Section VII: Modifications to the contract/concession
            Modification(notice, parent, changes);

            // Sections AD1-AD4:
            AnnexChanges(notice, parent, changes);

            return changes;
        }

        /// <summary>
        /// Changes to project, only in agriculture notices
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="parent"></param>
        /// <param name="changes"></param>
        private static void Project(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            var project = notice.Project;
            var parentProject = parent.Project;

            changes.AddEnum(parentProject.AgricultureWorks.ToTedChangeFormatGeneric(), project.AgricultureWorks.ToTedChangeFormatGeneric(), typeof(ProcurementProject), nameof(ProcurementProject.AgricultureWorks));
        }

        #region Section I: Contracting authority
        /// <summary>
        /// Section I: Contracting authority
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="changes">List of changes to append to</param>
        private static void ContractingAuthority(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            var org = notice.Project.Organisation;
            var parentOrg = parent.Project.Organisation;

            // I.1 Name and addresses
            changes.Add(parentOrg.Information?.OfficialName, org.Information.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName));
            changes.Add(parentOrg.Information?.NationalRegistrationNumber, org.Information.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber));
            changes.Add(parentOrg.Information?.NutsCodes?[0], org.Information.NutsCodes?[0], typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NutsCodes));

            // Default Ted section is I.1 for PostalAddress. Use overload if used elsewhere
            changes.Add(parentOrg.Information?.PostalAddress?.StreetAddress, org.Information?.PostalAddress?.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress));
            changes.Add(parentOrg.Information?.PostalAddress?.Town, org.Information?.PostalAddress?.Town, typeof(PostalAddress), nameof(PostalAddress.Town));
            changes.Add(parentOrg.Information?.PostalAddress?.PostalCode, org.Information?.PostalAddress?.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode));
            changes.Add(parentOrg.Information?.PostalAddress?.Country, org.Information?.PostalAddress?.Country, typeof(PostalAddress), nameof(PostalAddress.Country));
            changes.Add(parentOrg.Information?.Email, org.Information?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email));
            changes.Add(parentOrg.Information?.MainUrl, org.Information?.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl));
            changes.Add(parentOrg.Information?.TelephoneNumber, org.Information?.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber));

            changes.Add(parent.ContactPerson?.Name, notice.ContactPerson?.Name, typeof(ContactPerson), nameof(ContactPerson.Name));

            // I.2 Joint procurement
            changes.Add(parent.Project.JointProcurement.ToYesNo(notice.Language), notice.Project.JointProcurement.ToYesNo(notice.Language), typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.JointProcurement));
            changes.Add(parent.Project.CentralPurchasing.ToYesNo(notice.Language), notice.Project.CentralPurchasing.ToYesNo(notice.Language), typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.CentralPurchasing));

            // I.3 Communication
            var parentCommunicationInfo = parent.CommunicationInformation;
            var communicationInformation = notice.CommunicationInformation;
            changes.AddEnum(parentCommunicationInfo.ProcurementDocumentsAvailable.ToTEDChangeFormat(), communicationInformation.ProcurementDocumentsAvailable.ToTEDChangeFormat(), typeof(CommunicationInformation), nameof(CommunicationInformation.ProcurementDocumentsAvailable));

            if (!communicationInformation.DocumentsEntirelyInHilma && !parentCommunicationInfo.DocumentsEntirelyInHilma)
            {
                changes.Add(parentCommunicationInfo.ProcurementDocumentsUrl, communicationInformation.ProcurementDocumentsUrl, typeof(CommunicationInformation), nameof(CommunicationInformation.ProcurementDocumentsUrl));
            }

            changes.AddEnum(parentCommunicationInfo.AdditionalInformation.ToTEDChangeFormat(), communicationInformation.AdditionalInformation.ToTEDChangeFormat(), typeof(CommunicationInformation), nameof(CommunicationInformation.AdditionalInformation));

            var additionalAddress = communicationInformation.AdditionalInformation == AdditionalInformationAvailability.AddressAnother ? communicationInformation.AdditionalInformationAddress : null;
            var parentAdditionalAddress = parentCommunicationInfo.AdditionalInformation == AdditionalInformationAvailability.AddressAnother ? parentCommunicationInfo.AdditionalInformationAddress : null;

            changes.Add(parentAdditionalAddress?.BuyerProfileUrl, additionalAddress?.BuyerProfileUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.BuyerProfileUrl));
            changes.Add(parentAdditionalAddress?.Email, additionalAddress?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.BuyerProfileUrl));
            changes.Add(parentAdditionalAddress?.MainUrl, additionalAddress?.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl));
            changes.Add(parentAdditionalAddress?.NationalRegistrationNumber, additionalAddress?.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber));

            if (parentAdditionalAddress != null && parentAdditionalAddress.NutsCodes != null && parentAdditionalAddress.NutsCodes.Any())
            {
                changes.Add(parentAdditionalAddress?.NutsCodes[0], additionalAddress?.NutsCodes[0], typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NutsCodes));
            }
            changes.Add(parentAdditionalAddress?.OfficialName, additionalAddress?.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName));
            changes.Add(parentAdditionalAddress?.PostalAddress?.StreetAddress, additionalAddress?.PostalAddress?.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), null, "I.3");
            changes.Add(parentAdditionalAddress?.PostalAddress?.Town, additionalAddress?.PostalAddress?.Town, typeof(PostalAddress), nameof(PostalAddress.Town), null, "I.3");
            changes.Add(parentAdditionalAddress?.PostalAddress?.PostalCode, additionalAddress?.PostalAddress.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), null, "I.3");
            changes.Add(parentAdditionalAddress?.PostalAddress?.Country, additionalAddress?.PostalAddress.Country, typeof(PostalAddress), nameof(PostalAddress.Country), null, "I.3");
            changes.Add(parentAdditionalAddress?.TelephoneNumber, additionalAddress?.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber));

            changes.AddEnum(parentCommunicationInfo.SendTendersOption.ToTEDChangeFormat(), communicationInformation.SendTendersOption.ToTEDChangeFormat(), typeof(CommunicationInformation), nameof(CommunicationInformation.SendTendersOption));
            changes.Add(parentCommunicationInfo.ElectronicAddressToSendTenders, communicationInformation.ElectronicAddressToSendTenders, typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicAddressToSendTenders));

            var addressToSendTenders = communicationInformation.SendTendersOption == TenderSendOptions.AddressFollowing ? communicationInformation.AddressToSendTenders : null;
            var parentAddressToSendTenders = parentCommunicationInfo.SendTendersOption == TenderSendOptions.AddressFollowing ?  parentCommunicationInfo.AddressToSendTenders : null;

            changes.Add(parentAddressToSendTenders?.BuyerProfileUrl, addressToSendTenders?.BuyerProfileUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.BuyerProfileUrl));
            changes.Add(parentAddressToSendTenders?.Email, addressToSendTenders?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.BuyerProfileUrl));
            changes.Add(parentAddressToSendTenders?.MainUrl, addressToSendTenders?.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl));
            changes.Add(parentAddressToSendTenders?.NationalRegistrationNumber, addressToSendTenders?.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber));
            if (parentAddressToSendTenders != null && parentAddressToSendTenders.NutsCodes != null && parentAddressToSendTenders.NutsCodes.Any())
            {
                changes.Add(parentAddressToSendTenders?.NutsCodes[0], addressToSendTenders?.NutsCodes[0], typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NutsCodes));
            }
            changes.Add(parentAddressToSendTenders?.OfficialName, addressToSendTenders?.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName));
            changes.Add(parentAddressToSendTenders?.PostalAddress.StreetAddress, addressToSendTenders?.PostalAddress.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), null, "I.3");
            changes.Add(parentAddressToSendTenders?.PostalAddress.Town, addressToSendTenders?.PostalAddress?.Town, typeof(PostalAddress), nameof(PostalAddress.Town), null, "I.3");
            changes.Add(parentAddressToSendTenders?.PostalAddress.PostalCode, addressToSendTenders?.PostalAddress?.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), null, "I.3");
            changes.Add(parentAddressToSendTenders?.PostalAddress.Country, addressToSendTenders?.PostalAddress?.Country, typeof(PostalAddress), nameof(PostalAddress.Country), null, "I.3");
            changes.Add(parentAddressToSendTenders?.TelephoneNumber, addressToSendTenders?.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber));

            changes.Add(parentCommunicationInfo.ElectronicCommunicationInfoUrl, communicationInformation.ElectronicCommunicationInfoUrl, typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicCommunicationInfoUrl));
            changes.Add(parentCommunicationInfo.ElectronicCommunicationRequiresSpecialTools.ToYesNo(notice.Language), communicationInformation.ElectronicCommunicationRequiresSpecialTools.ToYesNo(notice.Language), typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicCommunicationRequiresSpecialTools));

            // I.4 Type of the conrtacting authority
            changes.AddEnum(parentOrg.ContractingAuthorityType.ToTEDChangeFormat(), org.ContractingAuthorityType.ToTEDChangeFormat(), typeof(OrganisationContract), nameof(OrganisationContract.ContractingAuthorityType));
            changes.Add(parentOrg.OtherContractingAuthorityType, org.OtherContractingAuthorityType, typeof(OrganisationContract), nameof(OrganisationContract.OtherContractingAuthorityType));

            // I.5 Main activity
            if (org.MainActivity != MainActivity.Undefined)
            {
                changes.AddEnum(parentOrg.MainActivity.ToTEDChangeFormat(), org.MainActivity.ToTEDChangeFormat(), typeof(OrganisationContract), nameof(OrganisationContract.MainActivity));
            }

            if(org.MainActivityUtilities != MainActivityUtilities.Undefined)
            {
                changes.AddEnum(parentOrg.MainActivityUtilities.ToTEDChangeFormat(), org.MainActivityUtilities.ToTEDChangeFormat(), typeof(OrganisationContract), nameof(OrganisationContract.MainActivityUtilities));
            }

            changes.Add(parentOrg.OtherMainActivity, org.OtherMainActivity, typeof(OrganisationContract), nameof(OrganisationContract.OtherMainActivity));
        }
        #endregion

        #region Section II: Object
        /// <summary>
        /// Section II: Object
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="changes">List of changes to append to</param>
        private static void Object(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            // II.1.1
            changes.Add(parent.Project.Title, notice.Project.Title, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.Title));
            changes.Add(parent.Project.ReferenceNumber, notice.Project.ReferenceNumber, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.ReferenceNumber));

            // II.1.2
            changes.AddCpv(parent.ProcurementObject.MainCpvCode, notice.ProcurementObject.MainCpvCode, typeof(ProcurementObject), nameof(ProcurementObject.MainCpvCode), true);

            // II.1.3
            changes.AddEnum(parent.Project.ContractType.ToTEDChangeFormat(), notice.Project.ContractType.ToTEDChangeFormat(), typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.ContractType));

            // II.1.4
            changes.Add(parent.ProcurementObject.ShortDescription, notice.ProcurementObject.ShortDescription, typeof(ProcurementObject), nameof(ProcurementObject.ShortDescription));

            // II.1.5
            changes.Add(parent.ProcurementObject.EstimatedValue.Value.ToString("F2"), notice.ProcurementObject.EstimatedValue.Value.ToString("F2"), typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValue));
            changes.Add(parent.ProcurementObject.EstimatedValue.Currency, notice.ProcurementObject.EstimatedValue.Currency, typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValue));
            changes.Add(parent.ProcurementObject.EstimatedValue.DoesNotExceedNationalThreshold?.ToYesNo(notice.Language) ?? "", notice.ProcurementObject.EstimatedValue.DoesNotExceedNationalThreshold?.ToYesNo(notice.Language) ?? "", typeof(ValueRangeContract), nameof(ValueRangeContract.DoesNotExceedNationalThreshold));


            // II.1.6
            changes.Add(parent.LotsInfo.DivisionLots.ToYesNo(notice.Language), notice.LotsInfo.DivisionLots.ToYesNo(notice.Language), typeof(LotsInfo), nameof(LotsInfo.DivisionLots));
            changes.AddEnum(parent.LotsInfo.LotsSubmittedFor.ToTEDChangeFormat(), notice.LotsInfo.LotsSubmittedFor.ToTEDChangeFormat(), typeof(LotsInfo), nameof(LotsInfo.LotsSubmittedFor));
            changes.Add(parent.LotsInfo.LotsSubmittedForQuantity.ToString(), notice.LotsInfo.LotsSubmittedForQuantity.ToString(), typeof(LotsInfo), nameof(LotsInfo.LotsSubmittedForQuantity));
            changes.Add(parent.LotsInfo.LotsMaxAwarded.ToYesNo(notice.Language), notice.LotsInfo.LotsMaxAwarded.ToYesNo(notice.Language), typeof(LotsInfo), nameof(LotsInfo.LotsMaxAwarded));
            changes.Add(parent.LotsInfo.LotsMaxAwardedQuantity.ToString(), notice.LotsInfo.LotsMaxAwardedQuantity.ToString(), typeof(LotsInfo), nameof(LotsInfo.LotsMaxAwardedQuantity));
            changes.Add(parent.LotsInfo.LotCombinationPossible.ToYesNo(notice.Language), notice.LotsInfo.LotCombinationPossible.ToYesNo(notice.Language), typeof(LotsInfo), nameof(LotsInfo.LotCombinationPossible));
            changes.Add(parent.LotsInfo.LotCombinationPossibleDescription, notice.LotsInfo.LotCombinationPossibleDescription, typeof(LotsInfo), nameof(LotsInfo.LotCombinationPossibleDescription));
            
            // II.1.7
            changes.Add(parent.ProcurementObject.TotalValue?.DisagreeToBePublished?.ToYesNo(notice.Language), notice.ProcurementObject.TotalValue?.DisagreeToBePublished?.ToYesNo(notice.Language), typeof(ProcurementObject), nameof(ProcurementObject.TotalValue.DisagreeToBePublished));
            changes.Add(((int?)parent.ProcurementObject.TotalValue?.Value)?.ToString(), ((int?)notice.ProcurementObject.TotalValue?.Value)?.ToString(), typeof(ProcurementObject), nameof(ProcurementObject.TotalValue), null, "II.1.7");

            // II.2
            // Existing lots (changes and additions)
            for (var i = 0; i < notice.ObjectDescriptions.Length; i++)
            {
                var lotNumber = i + 1;
                var parentLot = new ObjectDescription();
                if (parent.ObjectDescriptions.Length > i)
                {
                    parentLot = parent.ObjectDescriptions[i];
                }

                HandleLotChanges(changes, lotNumber, notice.ObjectDescriptions[i], parentLot, notice.Language);
            }

            // II.3
            changes.AddDate(parent.TenderingInformation.EstimatedDateOfContractNoticePublication, notice.TenderingInformation.EstimatedDateOfContractNoticePublication, typeof(TenderingInformation), nameof(TenderingInformation.EstimatedDateOfContractNoticePublication));

            // Lot removals
            for (var i = notice.ObjectDescriptions.Length; i < parent.ObjectDescriptions.Length; i++)
            {
                var lotNumber = i + 1;

                changes.Add(lotNumber.ToString(), null, typeof(ObjectDescription), nameof(ObjectDescription.LotNumber), lotNumber);
            }
        }

        private static void HandleLotChanges(List<XElement> changes, int lotNumber, ObjectDescription lot, ObjectDescription parentLot, string noticeLanguage)
        {
            // II.2.1
            changes.Add(parentLot.Title, lot.Title, typeof(ObjectDescription), nameof(ObjectDescription.Title), lotNumber);
            changes.Add(parentLot.LotNumber == 0 ? null : parentLot.LotNumber.ToString(), lot.LotNumber.ToString(), typeof(ObjectDescription), nameof(ObjectDescription.LotNumber), lotNumber);

            // II.2.2
            // Add & change
            for (var a = 0; a < lot.AdditionalCpvCodes.Length; a++)
            {
                var cpv = lot.AdditionalCpvCodes[a];

                var parentCpv = new CpvCode { VocCodes = new VocCode[0] };
                if (parentLot.AdditionalCpvCodes.Length > a)
                {
                    parentCpv = parentLot.AdditionalCpvCodes[a];
                }

                changes.AddCpv(parentCpv, cpv, typeof(ObjectDescription), nameof(ObjectDescription.AdditionalCpvCodes), false, lotNumber);
            }

            // II.2.3
            // Add & change
            for (var a = 0; a < lot.NutsCodes.Length; a++)
            {
                var nut = lot.NutsCodes[a];
                var originalNut = parentLot.NutsCodes.ElementAtOrDefault(a);

                changes.Add(originalNut, nut, typeof(ObjectDescription), nameof(ObjectDescription.NutsCodes), lotNumber);
            }

            // Removals
            for (var i = lot.NutsCodes.Length; i < parentLot.NutsCodes.Length; i++)
            {
                changes.Add(parentLot.NutsCodes[i], null, typeof(ObjectDescription), nameof(ObjectDescription.NutsCodes), lotNumber);
            }

            changes.Add(parentLot.MainsiteplaceWorksDelivery, lot.MainsiteplaceWorksDelivery, typeof(ObjectDescription), nameof(ObjectDescription.MainsiteplaceWorksDelivery), lotNumber);

            // II.2.4
            changes.Add(parentLot.DescrProcurement, lot.DescrProcurement, typeof(ObjectDescription), nameof(ObjectDescription.DescrProcurement), lotNumber);

            changes.Add(parentLot.DisagreeAwardCriteriaToBePublished.ToYesNo(noticeLanguage), lot.DisagreeAwardCriteriaToBePublished.ToYesNo(noticeLanguage), typeof(ObjectDescription), nameof(ObjectDescription.DisagreeAwardCriteriaToBePublished), lotNumber);

            // II.2.5
            changes.AddEnum(parentLot.AwardCriteria.CriterionTypes.ToTEDChangeFormat(), lot.AwardCriteria.CriterionTypes.ToTEDChangeFormat(), typeof(AwardCriteria), nameof(AwardCriteria.CriterionTypes), lotNumber);

            // Add & change QualityCriteria
            for (var a = 0; a < lot.AwardCriteria.QualityCriteria.Length; a++)
            {
                var currentCriteria = lot.AwardCriteria.QualityCriteria[a];
                var parentCriteria = parentLot.AwardCriteria.QualityCriteria.ElementAtOrDefault(a);
                changes.Add(parentCriteria?.Criterion, currentCriteria?.Criterion, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Criterion), lotNumber);
                changes.Add(parentCriteria?.Weighting, currentCriteria?.Weighting, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Weighting), lotNumber);
            }

            // Removals QualityCriteria
            for (var i = lot.AwardCriteria.QualityCriteria.Length; i < parentLot.AwardCriteria.QualityCriteria.Length; i++)
            {
                changes.Add(parentLot.AwardCriteria.QualityCriteria[i].Criterion, string.Empty, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Criterion), lotNumber);
                changes.Add(parentLot.AwardCriteria.QualityCriteria[i].Weighting, string.Empty, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Weighting), lotNumber);
            }

            // Add & change CostCriteria
            for (var a = 0; a < lot.AwardCriteria.CostCriteria.Length; a++)
            {
                //TODO(TuomasT): Cost criteria better translations
                var parentCriteria = parentLot.AwardCriteria.CostCriteria.ElementAtOrDefault(a);
                var currentCriteria = lot.AwardCriteria.CostCriteria[a];
                changes.Add(parentCriteria?.Criterion, currentCriteria?.Criterion, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Criterion), lotNumber);
                changes.Add(parentCriteria?.Weighting, currentCriteria?.Weighting, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Weighting), lotNumber);
            }

            // Removals CostCriteria
            for (var i = lot.AwardCriteria.CostCriteria.Length; i < parentLot.AwardCriteria.CostCriteria.Length; i++)
            {
                changes.Add(parentLot.AwardCriteria.CostCriteria[i].Criterion, string.Empty, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Criterion), lotNumber);
                changes.Add(parentLot.AwardCriteria.CostCriteria[i].Weighting, string.Empty, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Weighting), lotNumber);
            }

            changes.Add(parentLot.AwardCriteria.PriceCriterion.Criterion, lot.AwardCriteria.PriceCriterion.Criterion, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Criterion), lotNumber);
            changes.Add(parentLot.AwardCriteria.PriceCriterion.Weighting, lot.AwardCriteria.PriceCriterion.Weighting, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Weighting), lotNumber);

            // II.2.6
            changes.Add(parentLot.EstimatedValue.Value.ToString("F2"), lot.EstimatedValue.Value.ToString("F2"), typeof(ValueContract), nameof(ValueContract.Value), lotNumber, "II.2.6");
            changes.Add(parentLot.EstimatedValue.Currency.ToString(), lot.EstimatedValue.Currency.ToString(), typeof(ValueContract), nameof(ValueContract.Currency), lotNumber, "II.2.6");

            // II.2.7
            changes.Add(parentLot.TimeFrame.Months?.ToString(), lot.TimeFrame.Months?.ToString(), typeof(TimeFrame), nameof(TimeFrame.Months), lotNumber);
            changes.Add(parentLot.TimeFrame.Days?.ToString(), lot.TimeFrame.Days?.ToString(), typeof(TimeFrame), nameof(TimeFrame.Days), lotNumber);
            changes.AddDate(parentLot.TimeFrame.BeginDate, lot.TimeFrame.BeginDate, typeof(TimeFrame), nameof(TimeFrame.BeginDate), lotNumber);
            changes.AddDate(parentLot.TimeFrame.EndDate, lot.TimeFrame.EndDate, typeof(TimeFrame), nameof(TimeFrame.EndDate), lotNumber);
            changes.Add(parentLot.TimeFrame.CanBeRenewed.ToYesNo(noticeLanguage), lot.TimeFrame.CanBeRenewed.ToYesNo(noticeLanguage), typeof(TimeFrame), nameof(TimeFrame.CanBeRenewed), lotNumber);
            changes.Add(parentLot.TimeFrame.RenewalDescription, lot.TimeFrame.RenewalDescription, typeof(TimeFrame), nameof(TimeFrame.RenewalDescription), lotNumber);

            // II.2.9
            changes.Add(parentLot.CandidateNumberRestrictions.EnvisagedMaximumNumber.ToString(), lot.CandidateNumberRestrictions.EnvisagedMaximumNumber.ToString(), typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedMaximumNumber), lotNumber);
            changes.Add(parentLot.CandidateNumberRestrictions.EnvisagedMinimumNumber.ToString(), lot.CandidateNumberRestrictions.EnvisagedMinimumNumber.ToString(), typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedMinimumNumber), lotNumber);
            changes.Add(parentLot.CandidateNumberRestrictions.EnvisagedNumber.ToString(), lot.CandidateNumberRestrictions.EnvisagedNumber.ToString(), typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedNumber), lotNumber);
            changes.Add(parentLot.CandidateNumberRestrictions.ObjectiveCriteriaForChoosing, lot.CandidateNumberRestrictions.ObjectiveCriteriaForChoosing, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.ObjectiveCriteriaForChoosing), lotNumber);

            // II.2.10
            changes.Add(parentLot.OptionsAndVariants.VariantsWillBeAccepted.ToYesNo(noticeLanguage), lot.OptionsAndVariants.VariantsWillBeAccepted.ToYesNo(noticeLanguage), typeof(OptionsAndVariants), nameof(OptionsAndVariants.VariantsWillBeAccepted), lotNumber);
            changes.Add(parentLot.OptionsAndVariants.PartialOffersWillBeAccepted.ToYesNo(noticeLanguage), lot.OptionsAndVariants.PartialOffersWillBeAccepted.ToYesNo(noticeLanguage), typeof(OptionsAndVariants), nameof(OptionsAndVariants.PartialOffersWillBeAccepted), lotNumber);

            // II.2.11
            changes.Add(parentLot.OptionsAndVariants.Options.ToYesNo(noticeLanguage), lot.OptionsAndVariants.Options.ToYesNo(noticeLanguage), typeof(OptionsAndVariants), nameof(OptionsAndVariants.Options), lotNumber);
            changes.Add(parentLot.OptionsAndVariants.OptionsDescription, lot.OptionsAndVariants.OptionsDescription, typeof(OptionsAndVariants), nameof(OptionsAndVariants.OptionsDescription), lotNumber);

            // II.2.12
            changes.Add(parentLot.TendersMustBePresentedAsElectronicCatalogs.ToYesNo(noticeLanguage), lot.TendersMustBePresentedAsElectronicCatalogs.ToYesNo(noticeLanguage), typeof(ObjectDescription), nameof(ObjectDescription.AdditionalInformation), lotNumber);

            // II.2.13
            changes.Add(parentLot.EuFunds.ProcurementRelatedToEuProgram.ToYesNo(noticeLanguage), lot.EuFunds.ProcurementRelatedToEuProgram.ToYesNo(noticeLanguage), typeof(EuFunds), nameof(EuFunds.ProcurementRelatedToEuProgram), lotNumber);
            changes.Add(parentLot.EuFunds.ProjectIdentification, lot.EuFunds.ProjectIdentification, typeof(EuFunds), nameof(EuFunds.ProjectIdentification), lotNumber);

            // II.2.14
            changes.Add(parentLot.AdditionalInformation, lot.AdditionalInformation, typeof(ObjectDescription), nameof(ObjectDescription.AdditionalInformation), lotNumber);
        }
        #endregion

        #region Section III: Legal, economic, financial and technical information
        /// <summary>
        /// Section III: Legal, economic, financial and technical information
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="changes">List of changes to append to</param>
        private static void Conditions(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            if (parent.ConditionsInformation == null || notice.ConditionsInformation == null)
            {
                return;
            }

            var conditions = notice.ConditionsInformation;
            var parentConditions = parent.ConditionsInformation;
            // III.1.1
            changes.Add(parentConditions?.ProfessionalSuitabilityRequirements, conditions?.ProfessionalSuitabilityRequirements, typeof(ConditionsInformation), nameof(ConditionsInformation.ProfessionalSuitabilityRequirements));
            // III.1.2
            changes.Add(parentConditions?.EconomicCriteriaToParticipate.ToYesNo(notice.Language), conditions?.EconomicCriteriaToParticipate.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.EconomicCriteriaToParticipate));
            changes.Add(parentConditions?.EconomicCriteriaDescription, conditions?.EconomicCriteriaDescription, typeof(ConditionsInformation), nameof(ConditionsInformation.EconomicCriteriaDescription));
            changes.Add(parentConditions?.EconomicRequiredStandards, conditions?.EconomicRequiredStandards, typeof(ConditionsInformation), nameof(ConditionsInformation.EconomicRequiredStandards));
            // III.1.3
            changes.Add(parentConditions?.TechnicalCriteriaToParticipate.ToYesNo(notice.Language), conditions?.TechnicalCriteriaToParticipate.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.TechnicalCriteriaToParticipate));
            changes.Add(parentConditions?.TechnicalCriteriaDescription, conditions?.TechnicalCriteriaDescription, typeof(ConditionsInformation), nameof(ConditionsInformation.TechnicalCriteriaDescription));
            changes.Add(parentConditions?.TechnicalRequiredStandards, conditions?.TechnicalRequiredStandards, typeof(ConditionsInformation), nameof(ConditionsInformation.TechnicalRequiredStandards));
            // III.1.4
            changes.Add(parentConditions?.RulesForParticipation, conditions?.RulesForParticipation, typeof(ConditionsInformation), nameof(ConditionsInformation.RulesForParticipation));
            // III.1.5
            changes.Add(parentConditions?.RestrictedToShelteredWorkshop.ToYesNo(notice.Language), conditions?.RestrictedToShelteredWorkshop.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.RestrictedToShelteredWorkshop));
            changes.Add(parentConditions?.RestrictedToShelteredProgram.ToYesNo(notice.Language), conditions?.RestrictedToShelteredProgram.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.TechnicalRequiredStandards));
            changes.Add(parentConditions?.ReservedOrganisationServiceMission.ToYesNo(notice.Language), conditions?.ReservedOrganisationServiceMission.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.ReservedOrganisationServiceMission));
            // III.1.6
            changes.Add(parentConditions?.DepositsRequired, conditions?.DepositsRequired, typeof(ConditionsInformation), nameof(ConditionsInformation.DepositsRequired));
            // III.1.7
            changes.Add(parentConditions?.FinancingConditions, conditions?.FinancingConditions, typeof(ConditionsInformation), nameof(ConditionsInformation.FinancingConditions));
            // III.1.8
            changes.Add(parentConditions?.LegalFormTaken, conditions?.LegalFormTaken, typeof(ConditionsInformation), nameof(ConditionsInformation.LegalFormTaken));
            // III.1.10
            changes.Add(parentConditions?.CiriteriaForTheSelectionOfParticipants, conditions?.CiriteriaForTheSelectionOfParticipants, typeof(ConditionsInformation), nameof(ConditionsInformation.CiriteriaForTheSelectionOfParticipants));

            // III.2.1
            changes.Add(parentConditions?.ExecutionOfServiceIsReservedForProfession.ToYesNo(notice.Language), conditions?.ExecutionOfServiceIsReservedForProfession.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.ExecutionOfServiceIsReservedForProfession));
            changes.Add(parentConditions?.ReferenceToRelevantLawRegulationOrProvision, conditions?.ReferenceToRelevantLawRegulationOrProvision, typeof(ConditionsInformation), nameof(ConditionsInformation.ReferenceToRelevantLawRegulationOrProvision));

            changes.Add(parentConditions?.ParticipationIsReservedForProfession.ToYesNo(notice.Language), conditions?.ParticipationIsReservedForProfession.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.ParticipationIsReservedForProfession));
            changes.Add(parentConditions?.IndicateProfession, conditions?.IndicateProfession, typeof(ConditionsInformation), nameof(ConditionsInformation.IndicateProfession));

            // III.2.2
            changes.Add(parentConditions?.ContractPerformanceConditions, conditions?.ContractPerformanceConditions, typeof(ConditionsInformation), nameof(ConditionsInformation.ContractPerformanceConditions));
            // III.2.3
            changes.Add(parentConditions?.ObligationToIndicateNamesAndProfessionalQualifications.ToYesNo(notice.Language), conditions?.ObligationToIndicateNamesAndProfessionalQualifications.ToYesNo(notice.Language), typeof(ConditionsInformation), nameof(ConditionsInformation.ObligationToIndicateNamesAndProfessionalQualifications));

            // National conditions information
            var nationalConditions = notice.ConditionsInformationNational;
            var parentNationalConditions = parent.ConditionsInformationNational;

            if (nationalConditions != null || parentNationalConditions != null)
            {
                changes.Add(parentNationalConditions?.ParticipantSuitabilityCriteria, nationalConditions?.ParticipantSuitabilityCriteria, typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.ParticipantSuitabilityCriteria));
                changes.Add(parentNationalConditions?.RequiredCertifications, nationalConditions?.RequiredCertifications, typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.RequiredCertifications));
                changes.Add(parentNationalConditions?.AdditionalInformation, nationalConditions?.AdditionalInformation, typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.AdditionalInformation));
                changes.Add(parentNationalConditions?.ReservedForShelteredWorkshopOrProgram.ToYesNo(notice.Language), nationalConditions?.ReservedForShelteredWorkshopOrProgram.ToYesNo(notice.Language), typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.ReservedForShelteredWorkshopOrProgram));
            }
        }
        #endregion

        #region Section IV: Procedure
        /// <summary>
        /// Section IV: Procedure
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="changes">List of changes to append to</param>
        private static void Procedure(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            if (parent.ProcedureInformation == null || notice.ProcedureInformation == null)
            {
                return;
            }

            var parentProcedureInformation = parent.ProcedureInformation;
            var procedureInformation = notice.ProcedureInformation;

            // IV 1.1 Cannot change procedure type except on ex-ante notice!
            if (notice.Type == NoticeType.ExAnte)
            {
                changes.AddEnum(parentProcedureInformation.ProcedureType.ToTedChangeFormatGeneric(),
                    procedureInformation.ProcedureType.ToTedChangeFormatGeneric(), typeof(ProcedureInformation),
                    nameof(ProcedureInformation.ProcedureType));
            }

            changes.Add(parentProcedureInformation.AcceleratedProcedure.ToYesNo(notice.Language), procedureInformation.AcceleratedProcedure.ToYesNo(notice.Language), typeof(ProcedureInformation), nameof(ProcedureInformation.AcceleratedProcedure));
            changes.Add(parentProcedureInformation.JustificationForAcceleratedProcedure, procedureInformation.JustificationForAcceleratedProcedure, typeof(ProcedureInformation), nameof(ProcedureInformation.JustificationForAcceleratedProcedure));

            // IV 1.2 Cannot change contest type!
            //changes.AddEnum(parentProcedureInformation.ContestType.ToString(), procedureInformation.ContestType.ToString(), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestType));
            changes.AddEnum(parentProcedureInformation.ContestParticipants?.Type.ToString(), procedureInformation.ContestParticipants?.Type.ToString(), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants));
            changes.Add(parentProcedureInformation.ContestParticipants?.Value?.ToString("F0"), procedureInformation.ContestParticipants?.Value?.ToString("F0"), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants), null, null, "number_participants");
            changes.Add(parentProcedureInformation.ContestParticipants?.MinValue?.ToString("F0"), procedureInformation.ContestParticipants?.MinValue?.ToString("F0"), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants), null, null, "min_number");
            changes.Add(parentProcedureInformation.ContestParticipants?.MaxValue?.ToString("F0"), procedureInformation.ContestParticipants?.MaxValue?.ToString("F0"), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants), null, null, "max_number");

            // IV 1.3
            var framework = procedureInformation.FrameworkAgreement;
            var parentFramework = parentProcedureInformation.FrameworkAgreement;
            changes.AddEnum(framework?.FrameworkAgreementType.ToString(), parentFramework?.FrameworkAgreementType.ToString(), typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.FrameworkAgreementType));
            changes.Add(framework?.IncludesFrameworkAgreement.ToYesNo(notice.Language), parentFramework?.IncludesFrameworkAgreement.ToYesNo(notice.Language), typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.IncludesFrameworkAgreement));
            changes.Add(framework?.EnvisagedNumberOfParticipants.ToString(), parentFramework?.EnvisagedNumberOfParticipants.ToString(), typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.EnvisagedNumberOfParticipants));
            changes.Add(framework?.IncludesDynamicPurchasingSystem.ToYesNo(notice.Language), parentFramework?.IncludesDynamicPurchasingSystem.ToYesNo(notice.Language), typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.IncludesDynamicPurchasingSystem));
            changes.Add(framework?.DynamicPurchasingSystemInvolvesAdditionalPurchasers.ToYesNo(notice.Language), parentFramework?.DynamicPurchasingSystemInvolvesAdditionalPurchasers.ToYesNo(notice.Language), typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.DynamicPurchasingSystemInvolvesAdditionalPurchasers));
            changes.Add(framework?.JustificationForDurationOverFourYears, parentFramework?.JustificationForDurationOverFourYears, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.JustificationForDurationOverFourYears));

            // IV 1.4
            changes.Add(parentProcedureInformation.ReductionRecourseToReduceNumberOfSolutions.ToYesNo(notice.Language), procedureInformation.ReductionRecourseToReduceNumberOfSolutions.ToYesNo(notice.Language), typeof(ProcedureInformation), nameof(ProcedureInformation.ReductionRecourseToReduceNumberOfSolutions));
            // IV 1.5
            changes.Add(parentProcedureInformation.ReserveRightToAwardWithoutNegotiations.ToYesNo(notice.Language), procedureInformation.ReserveRightToAwardWithoutNegotiations.ToYesNo(notice.Language), typeof(ProcedureInformation), nameof(ProcedureInformation.ReserveRightToAwardWithoutNegotiations));

            // IV 1.6
            changes.Add(parentProcedureInformation.ElectronicAuctionWillBeUsed.ToYesNo(notice.Language), procedureInformation.ElectronicAuctionWillBeUsed.ToYesNo(notice.Language), typeof(ProcedureInformation), nameof(ProcedureInformation.ElectronicAuctionWillBeUsed));
            changes.Add(parentProcedureInformation.AdditionalInformationAboutElectronicAuction, procedureInformation.AdditionalInformationAboutElectronicAuction, typeof(ProcedureInformation), nameof(ProcedureInformation.AdditionalInformationAboutElectronicAuction));

            // IV 1.7
            changes.Add(parentProcedureInformation.NamesOfParticipantsAlreadySelected, procedureInformation.NamesOfParticipantsAlreadySelected, typeof(ProcedureInformation), nameof(ProcedureInformation.NamesOfParticipantsAlreadySelected));

            // IV 1.8
            changes.Add(parentProcedureInformation.ProcurementGovernedByGPA.ToYesNo(notice.Language), procedureInformation.ProcurementGovernedByGPA.ToYesNo(notice.Language), typeof(ProcedureInformation), nameof(ProcedureInformation.ProcurementGovernedByGPA));

            // IV 1.9
            changes.Add(parentProcedureInformation.CriteriaForEvaluationOfProjects, procedureInformation.CriteriaForEvaluationOfProjects, typeof(ProcedureInformation), nameof(ProcedureInformation.CriteriaForEvaluationOfProjects));

            // IV.1.10
            changes.Add(parentProcedureInformation.UrlNationalProcedure, procedureInformation.UrlNationalProcedure, typeof(ProcedureInformation), nameof(ProcedureInformation.UrlNationalProcedure));
            // IV.1.11
            changes.Add(parentProcedureInformation.MainFeaturesAward, procedureInformation.MainFeaturesAward, typeof(ProcedureInformation), nameof(ProcedureInformation.MainFeaturesAward));

            // IV.2.1
            if (!string.IsNullOrEmpty(notice.PreviousNoticeOjsNumber) && !string.IsNullOrEmpty(parent.PreviousNoticeOjsNumber))
            {
                changes.Add(parent.PreviousNoticeOjsNumber, notice.PreviousNoticeOjsNumber, typeof(NoticeContract), nameof(NoticeContract.PreviousNoticeOjsNumber));
            }
            var parentTender = parent.TenderingInformation;
            var tender = notice.TenderingInformation;

            // IV.2.2
            changes.AddDate(parentTender.TendersOrRequestsToParticipateDueDateTime, tender.TendersOrRequestsToParticipateDueDateTime, typeof(TenderingInformation), nameof(TenderingInformation.TendersOrRequestsToParticipateDueDateTime));

            // estimated time frame in national agriculture notices
            string oldDateRange = $"{parentTender.EstimatedExecutionTimeFrame.BeginDate?.ToString("dd.MM.yyyy")} - {parentTender.EstimatedExecutionTimeFrame.EndDate?.ToString("dd.MM.yyyy")}";
            string newDateRange = $"{tender.EstimatedExecutionTimeFrame.BeginDate?.ToString("dd.MM.yyyy")} - {tender.EstimatedExecutionTimeFrame.EndDate?.ToString("dd.MM.yyyy")}";
            changes.Add(oldDateRange, newDateRange, typeof(TenderingInformation), nameof(TenderingInformation.EstimatedExecutionTimeFrame));

            // IV.2.3
            changes.AddDate(parentTender.EstimatedDateOfInvitations, tender.EstimatedDateOfInvitations, typeof(TenderingInformation), nameof(TenderingInformation.EstimatedDateOfInvitations));

            // IV.2.4 Languages
            for (var i = 0; i < tender.Languages?.Length; i++)
            {
                var parentLanguage = parentTender.Languages.ElementAtOrDefault(i);
                changes.Add(parentLanguage, tender.Languages[i], typeof(TenderingInformation), nameof(TenderingInformation.Languages));
            }

            // Language removals
            for (var i = tender.Languages?.Length; i < parentTender.Languages?.Length; i++)
            {
                changes.Add(parentTender.Languages[(int)i], null, typeof(TenderingInformation), nameof(TenderingInformation.Languages));
            }
          
            // IV.2.6
            changes.AddDate(parentTender.TendersMustBeValidUntil, tender.TendersMustBeValidUntil, typeof(TenderingInformation), nameof(TenderingInformation.TendersMustBeValidUntil));
            changes.Add(parentTender.TendersMustBeValidForMonths?.ToString(), tender.TendersMustBeValidForMonths?.ToString(), typeof(TenderingInformation), nameof(TenderingInformation.TendersMustBeValidForMonths));

            // IV.2.7
            changes.AddDate(parentTender.TenderOpeningConditions.OpeningDateAndTime, tender.TenderOpeningConditions.OpeningDateAndTime, typeof(TenderOpeningConditions), nameof(TenderOpeningConditions.OpeningDateAndTime));
            changes.Add(parentTender.TenderOpeningConditions.Place, tender.TenderOpeningConditions.Place, typeof(TenderOpeningConditions), nameof(TenderOpeningConditions.Place));
            changes.Add(parentTender.TenderOpeningConditions.InformationAboutAuthorisedPersons, tender.TenderOpeningConditions.InformationAboutAuthorisedPersons, typeof(TenderOpeningConditions), nameof(TenderOpeningConditions.InformationAboutAuthorisedPersons));

            // IV.3 Rewards and Jury
            var parentRewards = parent.RewardsAndJury;
            var rewards = notice.RewardsAndJury;

            // IV.3.1
            changes.Add(parentRewards?.PrizeAwarded.ToYesNo(notice.Language), rewards?.PrizeAwarded.ToYesNo(notice.Language), typeof(RewardsAndJury), nameof(RewardsAndJury.PrizeAwarded));
            changes.Add(parentRewards?.NumberAndValueOfPrizes, rewards?.NumberAndValueOfPrizes, typeof(RewardsAndJury), nameof(RewardsAndJury.NumberAndValueOfPrizes));

            // IV.3.2
            changes.Add(parentRewards?.DetailsOfPayments, rewards?.DetailsOfPayments, typeof(RewardsAndJury), nameof(RewardsAndJury.DetailsOfPayments));

            // IV.3.3
            changes.Add(parentRewards?.ServiceContractAwardedToWinner.ToYesNo(notice.Language), rewards?.ServiceContractAwardedToWinner.ToYesNo(notice.Language), typeof(RewardsAndJury), nameof(RewardsAndJury.ServiceContractAwardedToWinner));

            // IV.3.4
            changes.Add(parentRewards?.DecisionOfTheJuryIsBinding.ToYesNo(notice.Language), rewards?.DecisionOfTheJuryIsBinding.ToYesNo(notice.Language), typeof(RewardsAndJury), nameof(RewardsAndJury.DecisionOfTheJuryIsBinding));

            // IV.3.5
            changes.Add(parentRewards?.NamesOfSelectedMembersOfJury, rewards?.NamesOfSelectedMembersOfJury, typeof(RewardsAndJury), nameof(RewardsAndJury.NamesOfSelectedMembersOfJury));
        }
        #endregion

        #region Section V: Award of contract
        private static void Award(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            // Handle only changes - lots cannot be removed or added.
            if (notice.Type.IsContractAward() || notice.Type == NoticeType.ExAnte)
            {
                for (var i = 0; i < notice.ObjectDescriptions.Length; i++)
                {
                    HandleLotAwardChanges(changes, notice.ObjectDescriptions[i], parent.ObjectDescriptions[i]);
                }
            }
        }

        private static void HandleLotAwardChanges(List<XElement> changes, ObjectDescription lot, ObjectDescription parentLot)
        {
            changes.AddEnum(parentLot.AwardContract.ContractAwarded.ToTEDChangeFormat(), lot.AwardContract.ContractAwarded.ToTEDChangeFormat(), typeof(Award), nameof(DataContracts.Award.ContractAwarded), lot.LotNumber);

            // V.1
            changes.AddEnum(parentLot.AwardContract.NoAwardedContract.FailureReason.ToTEDChangeFormat(), lot.AwardContract.NoAwardedContract.FailureReason.ToTEDChangeFormat(), typeof(NonAward), nameof(NonAward.FailureReason), lot.LotNumber);
            changes.AddDate(parentLot.AwardContract.NoAwardedContract?.OriginalNoticeSentDate, lot.AwardContract.NoAwardedContract?.OriginalNoticeSentDate, typeof(NonAward), nameof(NonAward.OriginalNoticeSentDate), lot.LotNumber, "V.1.0");
            changes.Add(parentLot.AwardContract.NoAwardedContract?.OriginalEsender?.TedNoDocExt, lot.AwardContract.NoAwardedContract?.OriginalEsender?.TedNoDocExt, typeof(Esender), nameof(Esender.TedNoDocExt), lot.LotNumber, "V.1.0");

            // V.2.1
            changes.AddDate(parentLot.AwardContract.AwardedContract.ConclusionDate, lot.AwardContract.AwardedContract.ConclusionDate, typeof(ContractAward), nameof(ContractAward.ConclusionDate), lot.LotNumber);

            // V.2.2
            changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.DisagreeTenderInformationToBePublished.ToYesNo(Language), lot.AwardContract.AwardedContract.NumberOfTenders.DisagreeTenderInformationToBePublished.ToYesNo(Language), typeof(NumberOfTenders), nameof(NumberOfTenders.DisagreeTenderInformationToBePublished), lot.LotNumber);
            changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.Total.ToString(), lot.AwardContract.AwardedContract.NumberOfTenders.Total.ToString(), typeof(NumberOfTenders), nameof(NumberOfTenders.Total), lot.LotNumber);
            changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.Sme.ToString(), lot.AwardContract.AwardedContract.NumberOfTenders.Sme.ToString(), typeof(NumberOfTenders), nameof(NumberOfTenders.Sme), lot.LotNumber);
            changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.OtherEu.ToString(), lot.AwardContract.AwardedContract.NumberOfTenders.OtherEu.ToString(), typeof(NumberOfTenders), nameof(NumberOfTenders.OtherEu), lot.LotNumber);
            changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.NonEu.ToString(), lot.AwardContract.AwardedContract.NumberOfTenders.NonEu.ToString(), typeof(NumberOfTenders), nameof(NumberOfTenders.NonEu), lot.LotNumber);
            changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.Electronic.ToString(), lot.AwardContract.AwardedContract.NumberOfTenders.Electronic.ToString(), typeof(NumberOfTenders), nameof(NumberOfTenders.Electronic), lot.LotNumber);

            // V.2.3
            // Add & change contractors
            changes.Add(parentLot.AwardContract.AwardedContract.DisagreeContractorInformationToBePublished.ToYesNo(Language), lot.AwardContract.AwardedContract.DisagreeContractorInformationToBePublished.ToYesNo(Language), typeof(NumberOfTenders), nameof(NumberOfTenders.DisagreeTenderInformationToBePublished), lot.LotNumber);
            for (var a = 0; a < lot.AwardContract.AwardedContract.Contractors?.Count; a++)
            {
                var parentContractor = parentLot.AwardContract.AwardedContract.Contractors.ElementAtOrDefault(a);
                var currentConrtactor = lot.AwardContract.AwardedContract.Contractors[a];
                changes.Add(parentContractor?.OfficialName, currentConrtactor?.OfficialName, typeof(ContractorContactInformation), nameof(ContractorContactInformation.OfficialName), lot.LotNumber);
                changes.Add(parentContractor?.NationalRegistrationNumber, currentConrtactor?.NationalRegistrationNumber, typeof(ContractorContactInformation), nameof(ContractorContactInformation.NationalRegistrationNumber), lot.LotNumber);
                changes.Add(parentContractor?.PostalAddress.StreetAddress, currentConrtactor?.PostalAddress.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.PostalAddress.Town, currentConrtactor?.PostalAddress.Town, typeof(PostalAddress), nameof(PostalAddress.Town), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.PostalAddress.PostalCode, currentConrtactor?.PostalAddress.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.PostalAddress.Country, currentConrtactor?.PostalAddress.Country, typeof(PostalAddress), nameof(PostalAddress.Country), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.NutsCodes.Any() == true ? parentContractor?.NutsCodes[0] : string.Empty, currentConrtactor?.NutsCodes.Any() == true ? currentConrtactor?.NutsCodes[0] : string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.NutsCodes), lot.LotNumber);
                changes.Add(parentContractor?.Email, currentConrtactor?.Email, typeof(ContractorContactInformation), nameof(ContractorContactInformation.Email), lot.LotNumber);
                changes.Add(parentContractor?.TelephoneNumber, currentConrtactor?.TelephoneNumber, typeof(ContractorContactInformation), nameof(ContractorContactInformation.TelephoneNumber), lot.LotNumber);
                changes.Add(parentContractor?.MainUrl, currentConrtactor?.MainUrl, typeof(ContractorContactInformation), nameof(ContractorContactInformation.MainUrl), lot.LotNumber);
                changes.Add(parentContractor?.IsSmallMediumEnterprise.ToYesNo(Language), currentConrtactor?.IsSmallMediumEnterprise.ToYesNo(Language), typeof(ContractorContactInformation), nameof(ContractorContactInformation.IsSmallMediumEnterprise), lot.LotNumber);
            }

            // Contractor removals
            for (var i = lot.AwardContract.AwardedContract.Contractors?.Count; i < parentLot.AwardContract.AwardedContract.Contractors?.Count; i++)
            {
                var parentContractor = parentLot.AwardContract.AwardedContract.Contractors[(int)i];
                changes.Add(parentContractor?.OfficialName, string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.OfficialName), lot.LotNumber);
                changes.Add(parentContractor?.NationalRegistrationNumber, string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.NationalRegistrationNumber), lot.LotNumber);
                changes.Add(parentContractor?.PostalAddress.StreetAddress, string.Empty, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.PostalAddress.Town, string.Empty, typeof(PostalAddress), nameof(PostalAddress.Town), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.PostalAddress.PostalCode, string.Empty, typeof(PostalAddress), nameof(PostalAddress.PostalCode), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.PostalAddress.Country, string.Empty, typeof(PostalAddress), nameof(PostalAddress.Country), lot.LotNumber, "V.2.3");
                changes.Add(parentContractor?.NutsCodes[0], string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.NutsCodes), lot.LotNumber);
                changes.Add(parentContractor?.Email, string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.Email), lot.LotNumber);
                changes.Add(parentContractor?.TelephoneNumber, string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.TelephoneNumber), lot.LotNumber);
                changes.Add(parentContractor?.MainUrl, string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.MainUrl), lot.LotNumber);
                changes.Add(parentContractor?.IsSmallMediumEnterprise.ToYesNo(Language), string.Empty, typeof(ContractorContactInformation), nameof(ContractorContactInformation.IsSmallMediumEnterprise), lot.LotNumber);
            }

            // V.2.4
            changes.Add(parentLot.EstimatedValue.Value.ToString("F2"), lot.EstimatedValue.Value.ToString("F2"), typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.InitialEstimatedValueOfContract.Value.ToString("F2"), lot.AwardContract.AwardedContract.InitialEstimatedValueOfContract.Value.ToString("F2"), typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.DisagreeToBePublished?.ToYesNo(Language), lot.AwardContract.AwardedContract.FinalTotalValue.DisagreeToBePublished?.ToYesNo(Language), typeof(ValueRangeContract), nameof(ValueRangeContract.DisagreeToBePublished), lot.LotNumber);
            changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Value.ToString("F2"), lot.AwardContract.AwardedContract.FinalTotalValue.Value.ToString("F2"), typeof(ValueRangeContract), nameof(ValueRangeContract.Value), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Currency, lot.AwardContract.AwardedContract.FinalTotalValue.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.MinValue.ToString("F2"), lot.AwardContract.AwardedContract.FinalTotalValue.MinValue.ToString("F2"), typeof(ContractAward), nameof(ContractAward.FinalTotalValue.MinValue), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Currency, lot.AwardContract.AwardedContract.FinalTotalValue.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.MaxValue.ToString("F2"), lot.AwardContract.AwardedContract.FinalTotalValue.MaxValue.ToString("F2"), typeof(ContractAward), nameof(ContractAward.FinalTotalValue.MaxValue), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Currency, lot.AwardContract.AwardedContract.FinalTotalValue.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.4");

            // V.2.5
            changes.Add(parentLot.AwardContract.AwardedContract.LikelyToBeSubcontracted.ToYesNo(Language), lot.AwardContract.AwardedContract.LikelyToBeSubcontracted.ToYesNo(Language), typeof(ContractAward), nameof(ContractAward.LikelyToBeSubcontracted), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.ValueOfSubcontract.Value.ToString("F2"), lot.AwardContract.AwardedContract.ValueOfSubcontract.Value.ToString("F2"), typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.ValueOfSubcontract.Currency, lot.AwardContract.AwardedContract.ValueOfSubcontract.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.ProportionOfValue.ToString("F2"), lot.AwardContract.AwardedContract.ProportionOfValue.ToString("F2"), typeof(ContractAward), nameof(ContractAward.ProportionOfValue), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.SubcontractingDescription, lot.AwardContract.AwardedContract.SubcontractingDescription, typeof(ContractAward), nameof(ContractAward.SubcontractingDescription), lot.LotNumber, "V.2.5");

            changes.Add(parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.AllOrCertainSubcontractsWillBeAwarded.ToYesNo(Language),
                lot.AwardContract.AwardedContract.ExAnteSubcontracting?.AllOrCertainSubcontractsWillBeAwarded.ToYesNo(Language),
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.AllOrCertainSubcontractsWillBeAwarded), lot.LotNumber, "V.2.5");

            changes.Add(parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted.ToYesNo(Language),
                lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted.ToYesNo(Language),
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.ShareOfContractWillBeSubcontracted), lot.LotNumber, "V.2.5");

            var showExanteParent = parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted == true;
            var showExante = lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted == true;

            changes.Add(showExanteParent ? ((int?)parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMinPercentage)?.ToString() : null,
                showExante ? ((int?)lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMinPercentage)?.ToString() : null,
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMinPercentage), lot.LotNumber, "V.2.5");
            changes.Add(showExanteParent ? ((int?)parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMaxPercentage)?.ToString() : null,
                showExante ? ((int?)lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMaxPercentage)?.ToString() : null,
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMaxPercentage), lot.LotNumber, "V.2.5");

            // V.2.6
            changes.Add(parentLot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Value.ToString("F2"), lot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Value.ToString("F2"), typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.6");
            changes.Add(parentLot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Currency, lot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.6");

            // V.2.8
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.CommunityOrigin.ToYesNo(Language), lot.AwardContract.AwardedContract.NotPublicFields.CommunityOrigin.ToYesNo(Language), typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.CommunityOrigin), lot.LotNumber, "V.2.8");
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.NonCommunityOrigin.ToYesNo(Language), lot.AwardContract.AwardedContract.NotPublicFields.NonCommunityOrigin.ToYesNo(Language), typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.NonCommunityOrigin), lot.LotNumber, "V.2.8");
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.Countries, lot.AwardContract.AwardedContract.NotPublicFields.Countries, typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.Countries), lot.LotNumber, "V.2.8.3");

            // V.2.10
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.AwardedToTendererWithVariant.ToYesNo(Language), lot.AwardContract.AwardedContract.NotPublicFields.AwardedToTendererWithVariant.ToYesNo(Language), typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.AwardedToTendererWithVariant), lot.LotNumber, "V.2.9");
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.AbnormallyLowTendersExcluded.ToYesNo(Language), lot.AwardContract.AwardedContract.NotPublicFields.AbnormallyLowTendersExcluded.ToYesNo(Language), typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.AwardedToTendererWithVariant), lot.LotNumber, "V.2.10");

        }
    #endregion

    #region Section VI: Complementary information
    /// <summary>
    /// Section VI: Complementary information
    /// </summary>
    /// <param name="notice">The notice </param>/param>
    /// <param name="parent">The parent notice</param>
    /// <param name="changes">List of changes to append to</param>
    private static void ComplementaryInfo(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            var comp = notice.ComplementaryInformation;
            var parentComp = parent.ComplementaryInformation;

            // VI.1
            changes.Add(parentComp.IsRecurringProcurement.ToYesNo(notice.Language), comp.IsRecurringProcurement.ToYesNo(notice.Language), typeof(ComplementaryInformation), nameof(ComplementaryInformation.IsRecurringProcurement));
            changes.Add(parentComp.EstimatedTimingForFurtherNoticePublish, comp.EstimatedTimingForFurtherNoticePublish, typeof(ComplementaryInformation), nameof(ComplementaryInformation.EstimatedTimingForFurtherNoticePublish));

            // VI.2
            changes.Add(parentComp.ElectronicOrderingUsed.ToYesNo(notice.Language), comp.ElectronicOrderingUsed.ToYesNo(notice.Language), typeof(ComplementaryInformation), nameof(ComplementaryInformation.ElectronicOrderingUsed));
            changes.Add(parentComp.ElectronicInvoicingUsed.ToYesNo(notice.Language), comp.ElectronicInvoicingUsed.ToYesNo(notice.Language), typeof(ComplementaryInformation), nameof(ComplementaryInformation.ElectronicInvoicingUsed));
            changes.Add(parentComp.ElectronicPaymentUsed.ToYesNo(notice.Language), comp.ElectronicPaymentUsed.ToYesNo(notice.Language), typeof(ComplementaryInformation), nameof(ComplementaryInformation.ElectronicPaymentUsed));

            // VI.3
            changes.Add(parentComp.AdditionalInformation, comp.AdditionalInformation, typeof(ComplementaryInformation), nameof(ComplementaryInformation.AdditionalInformation));

            // VI.4
            // TODO(AriN): Janne teki tätä? Lisää, kun refaktoroitu

        }
        #endregion

        #region Section VII: Modifications to the contract/concession
        private static void Modification(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            changes.Add(parent.Modifications?.Description, notice.Modifications?.Description, typeof(Modifications), nameof(Modifications.Description));

            changes.Add(parent.Modifications?.IncreaseAfterModifications.Currency, notice.Modifications?.IncreaseAfterModifications.Currency, typeof(Modifications), nameof(Modifications.IncreaseAfterModifications));
            changes.Add(parent.Modifications?.IncreaseAfterModifications.Value.ToString("F2"), notice.Modifications?.IncreaseAfterModifications.Value.ToString("F2"), typeof(Modifications), nameof(Modifications.IncreaseAfterModifications));

            changes.Add(parent.Modifications?.IncreaseBeforeModifications.Currency, notice.Modifications?.IncreaseBeforeModifications.Currency, typeof(Modifications), nameof(Modifications.IncreaseBeforeModifications));
            changes.Add(parent.Modifications?.IncreaseBeforeModifications.Value.ToString("F2"), notice.Modifications?.IncreaseBeforeModifications.Value.ToString("F2"), typeof(Modifications), nameof(Modifications.IncreaseBeforeModifications));

            changes.AddEnum(parent.Modifications?.Reason.ToTEDChangeFormat(), notice.Modifications?.Reason.ToTEDChangeFormat(), typeof(Modifications), nameof(Modifications.Reason));
            changes.Add(parent.Modifications?.ReasonDescriptionCircumstances, notice.Modifications?.ReasonDescriptionCircumstances, typeof(Modifications), nameof(Modifications.ReasonDescriptionCircumstances));
            changes.Add(parent.Modifications?.ReasonDescriptionEconomic, notice.Modifications?.ReasonDescriptionEconomic, typeof(Modifications), nameof(Modifications.ReasonDescriptionEconomic));
        }
        #endregion

        #region Section AD1-AD4

        public static void AnnexChanges(NoticeContract notice, NoticeContract parent, List<XElement> changes)
        {
            if (notice.Type == NoticeType.ExAnte)
            {
                var noticeNoAnnex =
                    notice.ProcedureInformation.ProcedureType != ProcedureType.AwardWoPriorPubD1 &&
                    notice.ProcedureInformation.ProcedureType != ProcedureType.AwardWoPriorPubD4;

                var parentNoAnnex =
                    parent.ProcedureInformation.ProcedureType != ProcedureType.AwardWoPriorPubD1 &&
                    parent.ProcedureInformation.ProcedureType != ProcedureType.AwardWoPriorPubD4;

                if (notice.Project.ProcurementCategory != parent.Project.ProcurementCategory)
                {
                    throw new ArgumentException("Ex ante notice cannot corrigendum project category!");
                }

                var annexType = "";
                if (notice.Project.ProcurementCategory == ProcurementCategory.Public)
                {
                    AnnexD1Changes(notice, parent, changes, noticeNoAnnex, parentNoAnnex);
                    annexType = "AD1";
                }
                else if (notice.Project.ProcurementCategory == ProcurementCategory.Utility)
                {
                    AnnexD2Changes(notice, parent, changes, noticeNoAnnex, parentNoAnnex);
                    annexType = "AD2";
                }
                else if (notice.Project.ProcurementCategory == ProcurementCategory.Defence)
                {
                    AnnexD3Changes(notice, parent, changes, noticeNoAnnex, parentNoAnnex);
                    annexType = "AD3";
                }
                else if (notice.Project.ProcurementCategory == ProcurementCategory.Lisence)
                {
                    AnnexD4Changes(notice, parent, changes, noticeNoAnnex, parentNoAnnex);
                    annexType = "AD4";
                }
                else
                {
                    throw new ArgumentException("Procurement type not suitable for exAnte notice!");
                }

                if (noticeNoAnnex != parentNoAnnex)
                {
                    changes.AddRaw(
                        !parentNoAnnex ? null : new[] {"d_outside_scope"},
                        !noticeNoAnnex ? null : new[] {"d_outside_scope"},
                        null,
                        $"{annexType}.2",
                        "d_outside_scope",
                        true);
                }
            }

        }

        public static void AnnexD1Changes(NoticeContract notice, NoticeContract parent, List<XElement> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD1
            {
                Justification = notice.Annexes?.D1?.Justification
            } : notice.Annexes.D1;
            var old = clearParentFields ? new AnnexD1
            {
                Justification = notice.Annexes?.D1?.Justification
            } : parent.Annexes.D1;

            changes.AddEnum(old.NoTenders ? old.ProcedureType.ToTedChangeFormatGeneric() : string.Empty,
                annex.NoTenders ? annex.ProcedureType.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD1), nameof(AnnexD1.ProcedureType));

            changes.Add(old.SuppliesManufacturedForResearch.ToYesNo(notice.Language),
                annex.SuppliesManufacturedForResearch.ToYesNo(notice.Language), typeof(AnnexD1), nameof(AnnexD1.SuppliesManufacturedForResearch));

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD1), nameof(AnnexD1.ReasonForNoCompetition));

            changes.Add(old.ExtremeUrgency.ToYesNo(notice.Language),
                annex.ExtremeUrgency.ToYesNo(notice.Language), typeof(AnnexD1), nameof(AnnexD1.ExtremeUrgency));

            changes.Add(old.AdditionalDeliveries.ToYesNo(notice.Language),
                annex.AdditionalDeliveries.ToYesNo(notice.Language), typeof(AnnexD1), nameof(AnnexD1.AdditionalDeliveries));

            changes.Add(old.RepetitionExisting.ToYesNo(notice.Language),
                annex.RepetitionExisting.ToYesNo(notice.Language), typeof(AnnexD1), nameof(AnnexD1.RepetitionExisting));

            changes.Add(old.DesignContestAward.ToYesNo(notice.Language),
                annex.DesignContestAward.ToYesNo(notice.Language), typeof(AnnexD1), nameof(AnnexD1.DesignContestAward));

            changes.Add(old.CommodityMarket.ToYesNo(notice.Language),
                annex.CommodityMarket.ToYesNo(notice.Language), typeof(AnnexD1), nameof(AnnexD1.CommodityMarket));

            changes.AddEnum(old.AdvantageousTerms ? old.AdvantageousPurchaseReason.ToTedChangeFormatGeneric() : string.Empty,
                annex.AdvantageousTerms ? annex.AdvantageousPurchaseReason.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD1), nameof(AnnexD1.AdvantageousPurchaseReason));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD1), nameof(AnnexD1.Justification));
        }

        public static void AnnexD2Changes(NoticeContract notice, NoticeContract parent, List<XElement> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD2
            {
                Justification = notice.Annexes?.D2?.Justification
            } : notice.Annexes.D2;
            var old = clearParentFields ? new AnnexD2
            {
                Justification = parent.Annexes?.D2?.Justification
            } : parent.Annexes.D2;

            changes.Add(old.NoTenders.ToYesNo(notice.Language),
                annex.NoTenders.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.NoTenders));

            changes.Add(old.PureResearch.ToYesNo(notice.Language),
                annex.PureResearch.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.PureResearch));

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD2), nameof(AnnexD2.ReasonForNoCompetition));

            changes.Add(old.ExtremeUrgency.ToYesNo(notice.Language),
                annex.ExtremeUrgency.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.ExtremeUrgency));

            changes.Add(old.AdditionalDeliveries.ToYesNo(notice.Language),
                annex.AdditionalDeliveries.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.AdditionalDeliveries));

            changes.Add(old.RepetitionExisting.ToYesNo(notice.Language),
                annex.RepetitionExisting.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.RepetitionExisting));

            changes.Add(old.DesignContestAward.ToYesNo(notice.Language),
                annex.DesignContestAward.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.DesignContestAward));

            changes.Add(old.CommodityMarket.ToYesNo(notice.Language),
                annex.CommodityMarket.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.CommodityMarket));

            changes.AddEnum(old.AdvantageousTerms ? old.AdvantageousPurchaseReason.ToTedChangeFormatGeneric() : string.Empty,
                annex.AdvantageousTerms ? annex.AdvantageousPurchaseReason.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD2), nameof(AnnexD2.AdvantageousPurchaseReason));

            changes.Add(old.BargainPurchase.ToYesNo(notice.Language),
                annex.BargainPurchase.ToYesNo(notice.Language), typeof(AnnexD2), nameof(AnnexD2.BargainPurchase));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD2), nameof(AnnexD2.Justification));
        }

        public static void AnnexD3Changes(NoticeContract notice, NoticeContract parent, List<XElement> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD3
            {
                Justification = notice.Annexes?.D3?.Justification
            } : notice.Annexes.D3;
            var old = clearParentFields ? new AnnexD3
            {
                Justification = notice.Annexes?.D3?.Justification
            } : parent.Annexes.D3;

            changes.AddEnum(old.NoTenders ? old.ProcedureType.ToTedChangeFormatGeneric() : string.Empty,
                annex.NoTenders ? annex.ProcedureType.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD3), nameof(AnnexD3.ProcedureType));

            changes.Add(old.OtherServices.ToYesNo(notice.Language),
                annex.OtherServices.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.OtherServices));

            changes.Add(old.ProductsManufacturedForResearch.ToYesNo(notice.Language),
                annex.ProductsManufacturedForResearch.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.ProductsManufacturedForResearch));

            changes.Add(old.AllTenders.ToYesNo(notice.Language),
                annex.AllTenders.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.AllTenders));

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD3), nameof(AnnexD3.ReasonForNoCompetition));

            changes.Add(old.CrisisUrgency.ToYesNo(notice.Language),
                annex.CrisisUrgency.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.CrisisUrgency));

            changes.Add(old.ExtremeUrgency.ToYesNo(notice.Language),
                annex.ExtremeUrgency.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.ExtremeUrgency));

            changes.Add(old.AdditionalDeliveries.ToYesNo(notice.Language),
                annex.AdditionalDeliveries.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.AdditionalDeliveries));

            changes.Add(old.RepetitionExisting.ToYesNo(notice.Language),
                annex.RepetitionExisting.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.RepetitionExisting));

            changes.Add(old.CommodityMarket.ToYesNo(notice.Language),
                annex.CommodityMarket.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.CommodityMarket));

            changes.AddEnum(old.AdvantageousTerms ? old.AdvantageousPurchaseReason.ToTedChangeFormatGeneric() : string.Empty,
                annex.AdvantageousTerms ? annex.AdvantageousPurchaseReason.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD3), nameof(AnnexD3.AdvantageousPurchaseReason));

            changes.Add(old.MaritimeService.ToYesNo(notice.Language),
                annex.MaritimeService.ToYesNo(notice.Language), typeof(AnnexD3), nameof(AnnexD3.MaritimeService));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD3), nameof(AnnexD3.Justification));
        }

        public static void AnnexD4Changes(NoticeContract notice, NoticeContract parent, List<XElement> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD4
            {
                Justification = notice.Annexes?.D4?.Justification
            } : notice.Annexes.D4;
            var old = clearParentFields ? new AnnexD4
            {
                Justification = notice.Annexes?.D4?.Justification
            } : parent.Annexes.D4;

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition.ToTedChangeFormatGeneric() : string.Empty, typeof(AnnexD3), nameof(AnnexD3.ReasonForNoCompetition));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD3), nameof(AnnexD3.Justification));
        }

        #endregion
    }
}
