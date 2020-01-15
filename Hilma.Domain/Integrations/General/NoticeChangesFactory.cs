using System;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Extensions;
using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.Entities.Annexes;
using Hilma.Domain.Configuration;
using System.Threading;

namespace Hilma.Domain.Integrations.General
{
    /// <summary>
    /// Factory for generating changes -node for Corrigendum notice
    /// </summary>
    public class NoticeChangesFactory
    {
        private static NoticeContract _notice;
        private static NoticeContract _parent;
        private readonly ITranslationProvider _translationProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="parent"></param>
        /// <param name="translationProvider"></param>
        public NoticeChangesFactory(NoticeContract notice, NoticeContract parent, ITranslationProvider translationProvider)
        {
            _notice = notice;
            _parent = parent;

            _translationProvider = translationProvider;

            ListExtensions.Translations = _translationProvider.GetDynamicObject(CancellationToken.None).Result;
            ListExtensions.NoticeLanguage = _notice.Language;
        }

        /// <summary>
        /// Get notice changes.
        /// </summary>
        /// <returns>A list of XElements with the changes.</returns>
        public List<Change> Changes()
        {
            var changes = new List<Change>();

            // Procurement project section
            Project(_notice, _parent, changes);

            // Section I: Contracting Authority
            ContractingAuthority(_notice, _parent, changes);

            // Section II: Object
            Object(_notice, _parent, changes);

            // Section III: Legal, economic, financial and technical information
            Conditions(_notice, _parent, changes);

            // Section IV: Procedure - type, language
            Procedure(_notice, _parent, changes);

            // Section V: Award of contract
            Award(_notice, _parent, changes);

            // Section V: Results of contest
            ResultOfContest(_notice, _parent, changes);

            // Section VI: Complementary information
            ComplementaryInfo(_notice, _parent, changes);

            // Section VII: Modifications to the contract/concession
            // TED: S7-01-02: Corrections in the original notice: correction on section VII is not allowed
            //Modification(_notice, _parent, changes);

            // Sections AD1-AD4:
            AnnexChanges(_notice, _parent, changes);

            return changes;
        }

        /// <summary>
        /// Changes to project, only in agriculture notices
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="parent"></param>
        /// <param name="changes"></param>
        private static void Project(NoticeContract notice, NoticeContract parent, List<Change> changes)
        {
            var project = notice.Project;
            var parentProject = parent.Project;

            changes.AddEnum(parentProject.AgricultureWorks, project.AgricultureWorks, typeof(ProcurementProject), nameof(ProcurementProject.AgricultureWorks));
        }

        #region Section I: Contracting authority
        /// <summary>
        /// Section I: Contracting authority
        /// </summary>
        /// <param name="notice">The notice</param>
        /// <param name="parent">The parent notice</param>
        /// <param name="changes">List of changes to append to</param>
        private static void ContractingAuthority(NoticeContract notice, NoticeContract parent, List<Change> changes)
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
            changes.Add(parent.Project.JointProcurement, notice.Project.JointProcurement, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.JointProcurement));
            changes.Add(parent.Project.CentralPurchasing, notice.Project.CentralPurchasing, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.CentralPurchasing));

            // I.3 Communication
            var parentCommunicationInfo = parent.CommunicationInformation;
            var communicationInformation = notice.CommunicationInformation;
            changes.AddEnum(parentCommunicationInfo.ProcurementDocumentsAvailable, communicationInformation.ProcurementDocumentsAvailable, typeof(CommunicationInformation), nameof(CommunicationInformation.ProcurementDocumentsAvailable));

            if (!communicationInformation.DocumentsEntirelyInHilma && !parentCommunicationInfo.DocumentsEntirelyInHilma)
            {
                changes.Add(parentCommunicationInfo.ProcurementDocumentsUrl, communicationInformation.ProcurementDocumentsUrl, typeof(CommunicationInformation), nameof(CommunicationInformation.ProcurementDocumentsUrl));
            }

            changes.AddEnum(parentCommunicationInfo.AdditionalInformation, communicationInformation.AdditionalInformation, typeof(CommunicationInformation), nameof(CommunicationInformation.AdditionalInformation));

            var additionalAddress = communicationInformation.AdditionalInformation == AdditionalInformationAvailability.AddressAnother ? communicationInformation.AdditionalInformationAddress : null;
            var parentAdditionalAddress = parentCommunicationInfo.AdditionalInformation == AdditionalInformationAvailability.AddressAnother ? parentCommunicationInfo.AdditionalInformationAddress : null;

            changes.Add(parentAdditionalAddress?.Email, additionalAddress?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email));
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

            changes.Add(parentCommunicationInfo.SendTendersOption.ToTEDChangeFormat(), communicationInformation.SendTendersOption.ToTEDChangeFormat(), typeof(CommunicationInformation), nameof(CommunicationInformation.SendTendersOption));
            changes.Add(parentCommunicationInfo.ElectronicAddressToSendTenders, communicationInformation.ElectronicAddressToSendTenders, typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicAddressToSendTenders));

            var addressToSendTenders = communicationInformation.SendTendersOption == TenderSendOptions.AddressFollowing ? communicationInformation.AddressToSendTenders : null;
            var parentAddressToSendTenders = parentCommunicationInfo.SendTendersOption == TenderSendOptions.AddressFollowing ?  parentCommunicationInfo.AddressToSendTenders : null;

            changes.Add(parentAddressToSendTenders?.Email, addressToSendTenders?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email));
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
            changes.Add(parentCommunicationInfo.ElectronicCommunicationRequiresSpecialTools, communicationInformation.ElectronicCommunicationRequiresSpecialTools, typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicCommunicationRequiresSpecialTools));

            // I.4 Type of the conrtacting authority
            changes.AddEnum(parentOrg.ContractingAuthorityType, org.ContractingAuthorityType, typeof(OrganisationContract), nameof(OrganisationContract.ContractingAuthorityType));
            changes.Add(parentOrg.OtherContractingAuthorityType, org.OtherContractingAuthorityType, typeof(OrganisationContract), nameof(OrganisationContract.OtherContractingAuthorityType));

            // I.5 Main activity
            if (org.MainActivity != MainActivity.Undefined)
            {
                changes.AddEnum(parentOrg.MainActivity, org.MainActivity, typeof(OrganisationContract), nameof(OrganisationContract.MainActivity));
            }

            if(org.MainActivityUtilities != MainActivityUtilities.Undefined)
            {
                changes.AddEnum(parentOrg.MainActivityUtilities, org.MainActivityUtilities, typeof(OrganisationContract), nameof(OrganisationContract.MainActivityUtilities));
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
        private static void Object(NoticeContract notice, NoticeContract parent, List<Change> changes)
        {
            // II.1.1
            changes.Add(parent.Project.Title, notice.Project.Title, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.Title));
            changes.Add(parent.Project.ReferenceNumber, notice.Project.ReferenceNumber, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.ReferenceNumber));

            // II.1.2
            changes.AddCpv(parent.ProcurementObject.MainCpvCode, notice.ProcurementObject.MainCpvCode, typeof(ProcurementObject), nameof(ProcurementObject.MainCpvCode));

            // II.1.3
            changes.AddEnum(parent.Project.ContractType, notice.Project.ContractType, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.ContractType));

            // II.1.4
            changes.Add(parent.ProcurementObject.ShortDescription, notice.ProcurementObject.ShortDescription, typeof(ProcurementObject), nameof(ProcurementObject.ShortDescription));

            // II.1.5
            changes.Add(parent.ProcurementObject.EstimatedValue.Value, notice.ProcurementObject.EstimatedValue.Value, typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValue));
            changes.Add(parent.ProcurementObject.EstimatedValue.Currency, notice.ProcurementObject.EstimatedValue.Currency, typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValue));
            changes.Add(parent.ProcurementObject.EstimatedValue.DoesNotExceedNationalThreshold, notice.ProcurementObject.EstimatedValue.DoesNotExceedNationalThreshold, typeof(ValueRangeContract), nameof(ValueRangeContract.DoesNotExceedNationalThreshold));
            changes.Add(parent.ProcurementObject.EstimatedValueCalculationMethod, notice.ProcurementObject.EstimatedValueCalculationMethod, typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValueCalculationMethod));

            // II.1.6
            changes.Add(parent.LotsInfo.DivisionLots, notice.LotsInfo.DivisionLots, typeof(LotsInfo), nameof(LotsInfo.DivisionLots));
            changes.AddEnum(parent.LotsInfo.LotsSubmittedFor, notice.LotsInfo.LotsSubmittedFor, typeof(LotsInfo), nameof(LotsInfo.LotsSubmittedFor));
            //changes.Add(parent.LotsInfo.LotsSubmittedForQuantity, notice.LotsInfo.LotsSubmittedForQuantity, typeof(LotsInfo), nameof(LotsInfo.LotsSubmittedForQuantity));
            changes.Add(parent.LotsInfo.LotsMaxAwarded, notice.LotsInfo.LotsMaxAwarded, typeof(LotsInfo), nameof(LotsInfo.LotsMaxAwarded));
            changes.Add(parent.LotsInfo.LotsMaxAwardedQuantity, notice.LotsInfo.LotsMaxAwardedQuantity, typeof(LotsInfo), nameof(LotsInfo.LotsMaxAwardedQuantity));
            changes.Add(parent.LotsInfo.LotCombinationPossible, notice.LotsInfo.LotCombinationPossible, typeof(LotsInfo), nameof(LotsInfo.LotCombinationPossible));
            changes.Add(parent.LotsInfo.LotCombinationPossibleDescription, notice.LotsInfo.LotCombinationPossibleDescription, typeof(LotsInfo), nameof(LotsInfo.LotCombinationPossibleDescription));
            
            // II.1.7
            if(notice.ProcurementObject.TotalValue?.DisagreeToBePublished != true)
            {
                changes.Add(parent.ProcurementObject.TotalValue?.Value, notice.ProcurementObject.TotalValue?.Value, typeof(ProcurementObject), nameof(ProcurementObject.TotalValue), null, "II.1.7");
                changes.Add(parent.ProcurementObject.TotalValue?.MinValue, notice.ProcurementObject.TotalValue?.MinValue, typeof(ProcurementObject), nameof(ProcurementObject.TotalValue), null, "II.1.7", "lowest_offer");
                changes.Add(parent.ProcurementObject.TotalValue?.MaxValue, notice.ProcurementObject.TotalValue?.MaxValue, typeof(ProcurementObject), nameof(ProcurementObject.TotalValue), null, "II.1.7", "highest_offer");
            }

            // II.2
            // Existing lots (changes and additions)
            foreach(var objectDescription in notice.ObjectDescriptions)
            {
                var parentLot = parent.ObjectDescriptions?.FirstOrDefault(x => x.LotNumber == objectDescription.LotNumber) ?? new ObjectDescription();

                HandleLotChanges(changes, objectDescription.LotNumber, objectDescription, parentLot);
            }

            // Lot removals
            foreach (var parentObjectDescr in parent.ObjectDescriptions)
            {
                if(notice.ObjectDescriptions.FirstOrDefault(x => x.LotNumber == parentObjectDescr.LotNumber) == null)
                {
                    changes.Add(parentObjectDescr.LotNumber, null, typeof(ObjectDescription), nameof(ObjectDescription.LotNumber), parentObjectDescr.LotNumber);
                }
            }

            // II.3
            changes.AddDate(parent.TenderingInformation.EstimatedDateOfContractNoticePublication, notice.TenderingInformation.EstimatedDateOfContractNoticePublication, typeof(TenderingInformation), nameof(TenderingInformation.EstimatedDateOfContractNoticePublication));
        }

        private static void HandleLotChanges(List<Change> changes, string lotNumber, ObjectDescription lot, ObjectDescription parentLot)
        {
            // II.2.1
            changes.Add(parentLot.Title, lot.Title, typeof(ObjectDescription), nameof(ObjectDescription.Title), lotNumber);
            changes.Add(string.IsNullOrEmpty(parentLot.LotNumber ) ? null : parentLot?.LotNumber, lot?.LotNumber, typeof(ObjectDescription), nameof(ObjectDescription.LotNumber), lotNumber);

            // II.2.2
            // Add & change
            changes.AddAdditionalCpv(parentLot.AdditionalCpvCodes.ToList(), lot.AdditionalCpvCodes.ToList(), typeof(ObjectDescription), nameof(ObjectDescription.AdditionalCpvCodes), lotNumber);


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

            if (!lot.DisagreeAwardCriteriaToBePublished)
            {
                // II.2.5
                changes.AddEnum(parentLot.AwardCriteria.CriterionTypes, lot.AwardCriteria.CriterionTypes, typeof(AwardCriteria), nameof(AwardCriteria.CriterionTypes), lotNumber);

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
            }

            // II.2.6
            changes.Add(parentLot.EstimatedValue.Value, lot.EstimatedValue.Value, typeof(ValueContract), nameof(ValueContract.Value), lotNumber, "II.2.6");
            changes.Add(parentLot.EstimatedValue.Currency, lot.EstimatedValue.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lotNumber, "II.2.6");

            // II.2.7
            changes.Add(parentLot.TimeFrame.Months, lot.TimeFrame.Months, typeof(TimeFrame), nameof(TimeFrame.Months), lotNumber);
            changes.Add(parentLot.TimeFrame.Days, lot.TimeFrame.Days, typeof(TimeFrame), nameof(TimeFrame.Days), lotNumber);
            changes.AddDate(parentLot.TimeFrame.BeginDate, lot.TimeFrame.BeginDate, typeof(TimeFrame), nameof(TimeFrame.BeginDate), lotNumber);
            changes.AddDate(parentLot.TimeFrame.EndDate, lot.TimeFrame.EndDate, typeof(TimeFrame), nameof(TimeFrame.EndDate), lotNumber);
            changes.Add(parentLot.TimeFrame.CanBeRenewed, lot.TimeFrame.CanBeRenewed, typeof(TimeFrame), nameof(TimeFrame.CanBeRenewed), lotNumber);
            changes.Add(parentLot.TimeFrame.RenewalDescription, lot.TimeFrame.RenewalDescription, typeof(TimeFrame), nameof(TimeFrame.RenewalDescription), lotNumber);

            // II.2.8
            if(lot.QualificationSystemDuration != null && parentLot.QualificationSystemDuration != null)
            {
                changes.AddEnum(parentLot.QualificationSystemDuration.Type, lot.QualificationSystemDuration.Type, typeof(QualificationSystemDuration), nameof(QualificationSystemDuration.Type), lotNumber);
                changes.AddDate(parentLot.QualificationSystemDuration.BeginDate, lot.QualificationSystemDuration.BeginDate, typeof(QualificationSystemDuration), nameof(QualificationSystemDuration.BeginDate), lotNumber);
                changes.AddDate(parentLot.QualificationSystemDuration.EndDate, lot.QualificationSystemDuration.EndDate, typeof(QualificationSystemDuration), nameof(QualificationSystemDuration.EndDate), lotNumber);
                changes.Add(parentLot.QualificationSystemDuration?.Renewal, lot.QualificationSystemDuration?.Renewal, typeof(QualificationSystemDuration), nameof(QualificationSystemDuration.Renewal), lotNumber);
                changes.Add(parentLot.QualificationSystemDuration?.NecessaryFormalities, lot.QualificationSystemDuration?.NecessaryFormalities, typeof(QualificationSystemDuration), nameof(QualificationSystemDuration.NecessaryFormalities), lotNumber);
            }

            // II.2.9
            changes.Add(parentLot.CandidateNumberRestrictions.EnvisagedMaximumNumber, lot.CandidateNumberRestrictions.EnvisagedMaximumNumber, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedMaximumNumber), lotNumber);
            changes.Add(parentLot.CandidateNumberRestrictions.EnvisagedMinimumNumber, lot.CandidateNumberRestrictions.EnvisagedMinimumNumber, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedMinimumNumber), lotNumber);
            changes.Add(parentLot.CandidateNumberRestrictions.EnvisagedNumber, lot.CandidateNumberRestrictions.EnvisagedNumber, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedNumber), lotNumber);
            changes.Add(parentLot.CandidateNumberRestrictions.ObjectiveCriteriaForChoosing, lot.CandidateNumberRestrictions.ObjectiveCriteriaForChoosing, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.ObjectiveCriteriaForChoosing), lotNumber);

            // II.2.10
            changes.Add(parentLot.OptionsAndVariants.VariantsWillBeAccepted, lot.OptionsAndVariants.VariantsWillBeAccepted, typeof(OptionsAndVariants), nameof(OptionsAndVariants.VariantsWillBeAccepted), lotNumber);
            changes.Add(parentLot.OptionsAndVariants.PartialOffersWillBeAccepted, lot.OptionsAndVariants.PartialOffersWillBeAccepted, typeof(OptionsAndVariants), nameof(OptionsAndVariants.PartialOffersWillBeAccepted), lotNumber);

            // II.2.11
            changes.Add(parentLot.OptionsAndVariants.Options, lot.OptionsAndVariants.Options, typeof(OptionsAndVariants), nameof(OptionsAndVariants.Options), lotNumber);
            changes.Add(parentLot.OptionsAndVariants.OptionsDescription, lot.OptionsAndVariants.OptionsDescription, typeof(OptionsAndVariants), nameof(OptionsAndVariants.OptionsDescription), lotNumber);

            // II.2.12
            changes.Add(parentLot.TendersMustBePresentedAsElectronicCatalogs, lot.TendersMustBePresentedAsElectronicCatalogs, typeof(ObjectDescription), nameof(ObjectDescription.AdditionalInformation), lotNumber);

            // II.2.13
            changes.Add(parentLot.EuFunds.ProcurementRelatedToEuProgram, lot.EuFunds.ProcurementRelatedToEuProgram, typeof(EuFunds), nameof(EuFunds.ProcurementRelatedToEuProgram), lotNumber);
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
        private static void Conditions(NoticeContract notice, NoticeContract parent, List<Change> changes)
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
            changes.Add(parentConditions?.EconomicCriteriaToParticipate, conditions?.EconomicCriteriaToParticipate, typeof(ConditionsInformation), nameof(ConditionsInformation.EconomicCriteriaToParticipate));
            changes.Add(parentConditions?.EconomicCriteriaDescription, conditions?.EconomicCriteriaDescription, typeof(ConditionsInformation), nameof(ConditionsInformation.EconomicCriteriaDescription));
            changes.Add(parentConditions?.EconomicRequiredStandards, conditions?.EconomicRequiredStandards, typeof(ConditionsInformation), nameof(ConditionsInformation.EconomicRequiredStandards));
            // III.1.3
            changes.Add(parentConditions?.TechnicalCriteriaToParticipate, conditions?.TechnicalCriteriaToParticipate, typeof(ConditionsInformation), nameof(ConditionsInformation.TechnicalCriteriaToParticipate));
            changes.Add(parentConditions?.TechnicalCriteriaDescription, conditions?.TechnicalCriteriaDescription, typeof(ConditionsInformation), nameof(ConditionsInformation.TechnicalCriteriaDescription));
            changes.Add(parentConditions?.TechnicalRequiredStandards, conditions?.TechnicalRequiredStandards, typeof(ConditionsInformation), nameof(ConditionsInformation.TechnicalRequiredStandards));
            // III.1.4
            changes.Add(parentConditions?.RulesForParticipation, conditions?.RulesForParticipation, typeof(ConditionsInformation), nameof(ConditionsInformation.RulesForParticipation));
            // III.1.5
            changes.Add(parentConditions?.RestrictedToShelteredWorkshop, conditions?.RestrictedToShelteredWorkshop, typeof(ConditionsInformation), nameof(ConditionsInformation.RestrictedToShelteredWorkshop));
            changes.Add(parentConditions?.RestrictedToShelteredProgram, conditions?.RestrictedToShelteredProgram, typeof(ConditionsInformation), nameof(ConditionsInformation.RestrictedToShelteredProgram));
            changes.Add(parentConditions?.ReservedOrganisationServiceMission, conditions?.ReservedOrganisationServiceMission, typeof(ConditionsInformation), nameof(ConditionsInformation.ReservedOrganisationServiceMission));
            // III.1.6
            changes.Add(parentConditions?.DepositsRequired, conditions?.DepositsRequired, typeof(ConditionsInformation), nameof(ConditionsInformation.DepositsRequired));
            // III.1.7
            changes.Add(parentConditions?.FinancingConditions, conditions?.FinancingConditions, typeof(ConditionsInformation), nameof(ConditionsInformation.FinancingConditions));
            // III.1.8
            changes.Add(parentConditions?.LegalFormTaken, conditions?.LegalFormTaken, typeof(ConditionsInformation), nameof(ConditionsInformation.LegalFormTaken));

            // III.1.9 Qualification system conditions
            for (var a = 0; a < conditions.QualificationSystemConditions?.Length; a++)
            {
                var qualificationConditions = conditions.QualificationSystemConditions[a];
                var parentQualificationConditions = parentConditions.QualificationSystemConditions.ElementAtOrDefault(a);
                changes.Add(parentQualificationConditions?.Conditions, qualificationConditions?.Conditions, typeof(QualificationSystemCondition), nameof(QualificationSystemCondition.Conditions));
                changes.Add(parentQualificationConditions?.Methods, qualificationConditions?.Methods, typeof(QualificationSystemCondition), nameof(QualificationSystemCondition.Methods));
            }

            for (var i = conditions.QualificationSystemConditions?.Length; i < parentConditions.QualificationSystemConditions?.Length; i++)
            {
                changes.Add(parentConditions.QualificationSystemConditions[(int)i].Conditions, null, typeof(QualificationSystemCondition), nameof(QualificationSystemCondition.Conditions));
                changes.Add(parentConditions.QualificationSystemConditions[(int)i].Methods, null, typeof(QualificationSystemCondition), nameof(QualificationSystemCondition.Methods));
            }

            // III.1.10
            changes.Add(parentConditions?.CiriteriaForTheSelectionOfParticipants, conditions?.CiriteriaForTheSelectionOfParticipants, typeof(ConditionsInformation), nameof(ConditionsInformation.CiriteriaForTheSelectionOfParticipants));

            // III.2.1
            changes.Add(parentConditions?.ExecutionOfServiceIsReservedForProfession, conditions?.ExecutionOfServiceIsReservedForProfession, typeof(ConditionsInformation), nameof(ConditionsInformation.ExecutionOfServiceIsReservedForProfession));
            changes.Add(parentConditions?.ReferenceToRelevantLawRegulationOrProvision, conditions?.ReferenceToRelevantLawRegulationOrProvision, typeof(ConditionsInformation), nameof(ConditionsInformation.ReferenceToRelevantLawRegulationOrProvision));

            changes.Add(parentConditions?.ParticipationIsReservedForProfession, conditions?.ParticipationIsReservedForProfession, typeof(ConditionsInformation), nameof(ConditionsInformation.ParticipationIsReservedForProfession));
            changes.Add(parentConditions?.IndicateProfession, conditions?.IndicateProfession, typeof(ConditionsInformation), nameof(ConditionsInformation.IndicateProfession));

            // III.2.2
            changes.Add(parentConditions?.ContractPerformanceConditions, conditions?.ContractPerformanceConditions, typeof(ConditionsInformation), nameof(ConditionsInformation.ContractPerformanceConditions));
            // III.2.3
            changes.Add(parentConditions?.ObligationToIndicateNamesAndProfessionalQualifications, conditions?.ObligationToIndicateNamesAndProfessionalQualifications, typeof(ConditionsInformation), nameof(ConditionsInformation.ObligationToIndicateNamesAndProfessionalQualifications));

            // National conditions information
            var nationalConditions = notice.ConditionsInformationNational;
            var parentNationalConditions = parent.ConditionsInformationNational;

            if (nationalConditions != null || parentNationalConditions != null)
            {
                changes.Add(parentNationalConditions?.ParticipantSuitabilityCriteria, nationalConditions?.ParticipantSuitabilityCriteria, typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.ParticipantSuitabilityCriteria));
                changes.Add(parentNationalConditions?.RequiredCertifications, nationalConditions?.RequiredCertifications, typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.RequiredCertifications));
                changes.Add(parentNationalConditions?.AdditionalInformation, nationalConditions?.AdditionalInformation, typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.AdditionalInformation));
                changes.Add(parentNationalConditions?.ReservedForShelteredWorkshopOrProgram, nationalConditions?.ReservedForShelteredWorkshopOrProgram, typeof(ConditionsInformationNational), nameof(ConditionsInformationNational.ReservedForShelteredWorkshopOrProgram));
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
        private static void Procedure(NoticeContract notice, NoticeContract parent, List<Change> changes)
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
                changes.AddEnum(parentProcedureInformation.ProcedureType,
                    procedureInformation.ProcedureType, typeof(ProcedureInformation),
                    nameof(ProcedureInformation.ProcedureType));
            }

            changes.Add(parentProcedureInformation.AcceleratedProcedure, procedureInformation.AcceleratedProcedure, typeof(ProcedureInformation), nameof(ProcedureInformation.AcceleratedProcedure));
            changes.Add(parentProcedureInformation.JustificationForAcceleratedProcedure, procedureInformation.JustificationForAcceleratedProcedure, typeof(ProcedureInformation), nameof(ProcedureInformation.JustificationForAcceleratedProcedure));

            // IV 1.2
            if(parentProcedureInformation.ContestParticipants != null && procedureInformation.ContestParticipants != null)
            {
                // Contest type cannot be changed
                //changes.AddEnum(parentProcedureInformation.ContestType, procedureInformation.ContestType, typeof(ProcedureInformation), nameof(ProcedureInformation.ContestType));
                changes.AddEnum(parentProcedureInformation.ContestParticipants.Type, procedureInformation.ContestParticipants.Type, typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants));
                changes.Add(parentProcedureInformation.ContestParticipants.Value?.ToString("F0"), procedureInformation.ContestParticipants.Value?.ToString("F0"), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants), null, null, "number_participants");
                changes.Add(parentProcedureInformation.ContestParticipants.MinValue?.ToString("F0"), procedureInformation.ContestParticipants.MinValue?.ToString("F0"), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants), null, null, "min_number");
                changes.Add(parentProcedureInformation.ContestParticipants.MaxValue?.ToString("F0"), procedureInformation.ContestParticipants.MaxValue?.ToString("F0"), typeof(ProcedureInformation), nameof(ProcedureInformation.ContestParticipants), null, null, "max_number");
            }
            // IV 1.3
            var framework = procedureInformation.FrameworkAgreement;
            var parentFramework = parentProcedureInformation.FrameworkAgreement;
            if(framework != null && parentFramework != null)
            {
                changes.AddEnum(framework.FrameworkAgreementType, parentFramework.FrameworkAgreementType, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.FrameworkAgreementType));
                changes.Add(framework.IncludesFrameworkAgreement, parentFramework.IncludesFrameworkAgreement, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.IncludesFrameworkAgreement));
                changes.Add(framework.EnvisagedNumberOfParticipants, parentFramework.EnvisagedNumberOfParticipants, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.EnvisagedNumberOfParticipants));
                changes.Add(framework.IncludesDynamicPurchasingSystem, parentFramework.IncludesDynamicPurchasingSystem, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.IncludesDynamicPurchasingSystem));
                changes.Add(framework.DynamicPurchasingSystemInvolvesAdditionalPurchasers, parentFramework.DynamicPurchasingSystemInvolvesAdditionalPurchasers, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.DynamicPurchasingSystemInvolvesAdditionalPurchasers));
                changes.Add(framework.JustificationForDurationOverFourYears, parentFramework.JustificationForDurationOverFourYears, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.JustificationForDurationOverFourYears));
            }

            // IV 1.4
            changes.Add(parentProcedureInformation.ReductionRecourseToReduceNumberOfSolutions, procedureInformation.ReductionRecourseToReduceNumberOfSolutions, typeof(ProcedureInformation), nameof(ProcedureInformation.ReductionRecourseToReduceNumberOfSolutions));
            // IV 1.5
            changes.Add(parentProcedureInformation.ReserveRightToAwardWithoutNegotiations, procedureInformation.ReserveRightToAwardWithoutNegotiations, typeof(ProcedureInformation), nameof(ProcedureInformation.ReserveRightToAwardWithoutNegotiations));

            // IV 1.6
            changes.Add(parentProcedureInformation.ElectronicAuctionWillBeUsed, procedureInformation.ElectronicAuctionWillBeUsed, typeof(ProcedureInformation), nameof(ProcedureInformation.ElectronicAuctionWillBeUsed));
            changes.Add(parentProcedureInformation.AdditionalInformationAboutElectronicAuction, procedureInformation.AdditionalInformationAboutElectronicAuction, typeof(ProcedureInformation), nameof(ProcedureInformation.AdditionalInformationAboutElectronicAuction));

            // IV 1.7
            changes.Add(parentProcedureInformation.NamesOfParticipantsAlreadySelected, procedureInformation.NamesOfParticipantsAlreadySelected, typeof(ProcedureInformation), nameof(ProcedureInformation.NamesOfParticipantsAlreadySelected));

            // IV 1.8
            changes.Add(parentProcedureInformation.ProcurementGovernedByGPA, procedureInformation.ProcurementGovernedByGPA, typeof(ProcedureInformation), nameof(ProcedureInformation.ProcurementGovernedByGPA));

            // IV 1.9
            if (!procedureInformation.DisagreeCriteriaForEvaluationOfProjectsPublish)
            {
                changes.Add(parentProcedureInformation.CriteriaForEvaluationOfProjects, procedureInformation.CriteriaForEvaluationOfProjects, typeof(ProcedureInformation), nameof(ProcedureInformation.CriteriaForEvaluationOfProjects));
            }

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

            // IV.2.5 Scheduled date for start of award procedures
            changes.AddDate(parentTender.ScheduledStartDateOfAwardProcedures, tender.ScheduledStartDateOfAwardProcedures, typeof(TenderingInformation), nameof(TenderingInformation.ScheduledStartDateOfAwardProcedures));

            // IV.2.6
            changes.AddDate(parentTender.TendersMustBeValidUntil, tender.TendersMustBeValidUntil, typeof(TenderingInformation), nameof(TenderingInformation.TendersMustBeValidUntil));
            changes.Add(parentTender.TendersMustBeValidForMonths, tender.TendersMustBeValidForMonths, typeof(TenderingInformation), nameof(TenderingInformation.TendersMustBeValidForMonths));

            // IV.2.7
            changes.AddDate(parentTender.TenderOpeningConditions.OpeningDateAndTime, tender.TenderOpeningConditions.OpeningDateAndTime, typeof(TenderOpeningConditions), nameof(TenderOpeningConditions.OpeningDateAndTime));
            changes.Add(parentTender.TenderOpeningConditions.Place, tender.TenderOpeningConditions.Place, typeof(TenderOpeningConditions), nameof(TenderOpeningConditions.Place));
            changes.Add(parentTender.TenderOpeningConditions.InformationAboutAuthorisedPersons, tender.TenderOpeningConditions.InformationAboutAuthorisedPersons, typeof(TenderOpeningConditions), nameof(TenderOpeningConditions.InformationAboutAuthorisedPersons));

            // IV.3 Rewards and Jury
            var parentRewards = parent.RewardsAndJury;
            var rewards = notice.RewardsAndJury;

            // IV.3.1
            changes.Add(parentRewards?.PrizeAwarded, rewards?.PrizeAwarded, typeof(RewardsAndJury), nameof(RewardsAndJury.PrizeAwarded));
            changes.Add(parentRewards?.NumberAndValueOfPrizes, rewards?.NumberAndValueOfPrizes, typeof(RewardsAndJury), nameof(RewardsAndJury.NumberAndValueOfPrizes));

            // IV.3.2
            changes.Add(parentRewards?.DetailsOfPayments, rewards?.DetailsOfPayments, typeof(RewardsAndJury), nameof(RewardsAndJury.DetailsOfPayments));

            // IV.3.3
            changes.Add(parentRewards?.ServiceContractAwardedToWinner, rewards?.ServiceContractAwardedToWinner, typeof(RewardsAndJury), nameof(RewardsAndJury.ServiceContractAwardedToWinner));

            // IV.3.4
            changes.Add(parentRewards?.DecisionOfTheJuryIsBinding, rewards?.DecisionOfTheJuryIsBinding, typeof(RewardsAndJury), nameof(RewardsAndJury.DecisionOfTheJuryIsBinding));

            // IV.3.5
            changes.Add(parentRewards?.NamesOfSelectedMembersOfJury, rewards?.NamesOfSelectedMembersOfJury, typeof(RewardsAndJury), nameof(RewardsAndJury.NamesOfSelectedMembersOfJury));
        }
        #endregion

        #region Section V: Award of contract
        private static void Award(NoticeContract notice, NoticeContract parent, List<Change> changes)
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

        private static void HandleLotAwardChanges(List<Change> changes, ObjectDescription lot, ObjectDescription parentLot)
        {
            changes.Add(parentLot.AwardContract.ContractAwarded.ToTEDChangeFormat(), lot.AwardContract.ContractAwarded.ToTEDChangeFormat(), typeof(Award), nameof(DataContracts.Award.ContractAwarded), lot.LotNumber);

            // V.1
            changes.AddEnum(parentLot.AwardContract.NoAwardedContract.FailureReason, lot.AwardContract.NoAwardedContract.FailureReason, typeof(NonAward), nameof(NonAward.FailureReason), lot.LotNumber);
            changes.AddDate(parentLot.AwardContract.NoAwardedContract?.OriginalNoticeSentDate, lot.AwardContract.NoAwardedContract?.OriginalNoticeSentDate, typeof(NonAward), nameof(NonAward.OriginalNoticeSentDate), lot.LotNumber, "V.1.0");
            changes.Add(parentLot.AwardContract.NoAwardedContract?.OriginalEsender?.TedNoDocExt, lot.AwardContract.NoAwardedContract?.OriginalEsender?.TedNoDocExt, typeof(Esender), nameof(Esender.TedNoDocExt), lot.LotNumber, "V.1.0");

            // V.2.1
            changes.AddDate(parentLot.AwardContract.AwardedContract.ConclusionDate, lot.AwardContract.AwardedContract.ConclusionDate, typeof(ContractAward), nameof(ContractAward.ConclusionDate), lot.LotNumber);

            // V.2.2
            if (!lot.AwardContract.AwardedContract.NumberOfTenders.DisagreeTenderInformationToBePublished)
            {
                changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.Total, lot.AwardContract.AwardedContract.NumberOfTenders.Total, typeof(NumberOfTenders), nameof(NumberOfTenders.Total), lot.LotNumber);
                changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.Sme, lot.AwardContract.AwardedContract.NumberOfTenders.Sme, typeof(NumberOfTenders), nameof(NumberOfTenders.Sme), lot.LotNumber);
                changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.OtherEu, lot.AwardContract.AwardedContract.NumberOfTenders.OtherEu, typeof(NumberOfTenders), nameof(NumberOfTenders.OtherEu), lot.LotNumber);
                changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.NonEu, lot.AwardContract.AwardedContract.NumberOfTenders.NonEu, typeof(NumberOfTenders), nameof(NumberOfTenders.NonEu), lot.LotNumber);
                changes.Add(parentLot.AwardContract.AwardedContract.NumberOfTenders.Electronic, lot.AwardContract.AwardedContract.NumberOfTenders.Electronic, typeof(NumberOfTenders), nameof(NumberOfTenders.Electronic), lot.LotNumber);
            }

            // V.2.3
            // Add & change contractors
            if (!lot.AwardContract.AwardedContract.DisagreeContractorInformationToBePublished)
            {
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
                    changes.Add(parentContractor?.IsSmallMediumEnterprise, currentConrtactor?.IsSmallMediumEnterprise, typeof(ContractorContactInformation), nameof(ContractorContactInformation.IsSmallMediumEnterprise), lot.LotNumber);
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
                    changes.Add(parentContractor?.IsSmallMediumEnterprise, null, typeof(ContractorContactInformation), nameof(ContractorContactInformation.IsSmallMediumEnterprise), lot.LotNumber);
                }
            }

            // V.2.4
            changes.Add(parentLot.EstimatedValue.Value, lot.EstimatedValue.Value, typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.InitialEstimatedValueOfContract.Value, lot.AwardContract.AwardedContract.InitialEstimatedValueOfContract.Value, typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.4");
            if (lot.AwardContract.AwardedContract.FinalTotalValue.DisagreeToBePublished != true)
            {
                changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Value, lot.AwardContract.AwardedContract.FinalTotalValue.Value, typeof(ValueRangeContract), nameof(ValueRangeContract.Value), lot.LotNumber, "V.2.4");
                changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Currency, lot.AwardContract.AwardedContract.FinalTotalValue.Currency, typeof(ValueRangeContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.4");
                changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.MinValue, lot.AwardContract.AwardedContract.FinalTotalValue.MinValue, typeof(ValueRangeContract), nameof(ContractAward.FinalTotalValue.MinValue), lot.LotNumber, "V.2.4", "lowest_offer"); ;
                changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Currency, lot.AwardContract.AwardedContract.FinalTotalValue.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.4");
                changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.MaxValue, lot.AwardContract.AwardedContract.FinalTotalValue.MaxValue, typeof(ValueRangeContract), nameof(ContractAward.FinalTotalValue.MaxValue), lot.LotNumber, "V.2.4", "highest_offer");
                changes.Add(parentLot.AwardContract.AwardedContract.FinalTotalValue.Currency, lot.AwardContract.AwardedContract.FinalTotalValue.Currency, typeof(ValueRangeContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.4");
            }

            changes.Add(parentLot.AwardContract.AwardedContract.ConcessionRevenue?.Value, lot.AwardContract.AwardedContract.ConcessionRevenue?.Value, typeof(ContractAward), nameof(ContractAward.ConcessionRevenue), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.ConcessionRevenue?.Currency, lot.AwardContract.AwardedContract.ConcessionRevenue?.Currency, typeof(ContractAward), nameof(ContractAward.ConcessionRevenue), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.PricesAndPayments?.Value, lot.AwardContract.AwardedContract.PricesAndPayments?.Value, typeof(ContractAward), nameof(ContractAward.PricesAndPayments), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.PricesAndPayments?.Currency, lot.AwardContract.AwardedContract.PricesAndPayments?.Currency, typeof(ContractAward), nameof(ContractAward.PricesAndPayments), lot.LotNumber, "V.2.4");
            changes.Add(parentLot.AwardContract.AwardedContract.ConcessionValueAdditionalInformation, lot.AwardContract.AwardedContract.ConcessionValueAdditionalInformation, typeof(ContractAward), nameof(ContractAward.ConcessionValueAdditionalInformation), lot.LotNumber, "V.2.4");

            // V.2.5
            changes.Add(parentLot.AwardContract.AwardedContract.LikelyToBeSubcontracted, lot.AwardContract.AwardedContract.LikelyToBeSubcontracted, typeof(ContractAward), nameof(ContractAward.LikelyToBeSubcontracted), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.ValueOfSubcontract.Value, lot.AwardContract.AwardedContract.ValueOfSubcontract.Value, typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.ValueOfSubcontract.Currency, lot.AwardContract.AwardedContract.ValueOfSubcontract.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.ProportionOfValue, lot.AwardContract.AwardedContract.ProportionOfValue, typeof(ContractAward), nameof(ContractAward.ProportionOfValue), lot.LotNumber, "V.2.5");
            changes.Add(parentLot.AwardContract.AwardedContract.SubcontractingDescription, lot.AwardContract.AwardedContract.SubcontractingDescription, typeof(ContractAward), nameof(ContractAward.SubcontractingDescription), lot.LotNumber, "V.2.5");

            changes.Add(parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.AllOrCertainSubcontractsWillBeAwarded,
                lot.AwardContract.AwardedContract.ExAnteSubcontracting?.AllOrCertainSubcontractsWillBeAwarded,
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.AllOrCertainSubcontractsWillBeAwarded), lot.LotNumber, "V.2.5");

            changes.Add(parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted,
                lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted,
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.ShareOfContractWillBeSubcontracted), lot.LotNumber, "V.2.5");

            var showExanteParent = parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted == true;
            var showExante = lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontracted == true;

            changes.Add(showExanteParent ? parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMinPercentage : null,
                showExante ? lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMinPercentage : null,
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMinPercentage), lot.LotNumber, "V.2.5");
            changes.Add(showExanteParent ? parentLot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMaxPercentage : null,
                showExante ? lot.AwardContract.AwardedContract.ExAnteSubcontracting?.ShareOfContractWillBeSubcontractedMaxPercentage : null,
                typeof(ExAnteSubcontracting), nameof(ExAnteSubcontracting.ShareOfContractWillBeSubcontractedMaxPercentage), lot.LotNumber, "V.2.5");

            // V.2.6
            changes.Add(parentLot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Value, lot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Value, typeof(ValueContract), nameof(ValueContract.Value), lot.LotNumber, "V.2.6");
            changes.Add(parentLot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Currency, lot.AwardContract.AwardedContract.PricePaidForBargainPurchases?.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lot.LotNumber, "V.2.6");

            // V.2.8
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.CommunityOrigin, lot.AwardContract.AwardedContract.NotPublicFields.CommunityOrigin, typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.CommunityOrigin), lot.LotNumber, "V.2.8");
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.NonCommunityOrigin, lot.AwardContract.AwardedContract.NotPublicFields.NonCommunityOrigin, typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.NonCommunityOrigin), lot.LotNumber, "V.2.8");
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.Countries, lot.AwardContract.AwardedContract.NotPublicFields.Countries, typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.Countries), lot.LotNumber, "V.2.8.3");

            // V.2.10
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.AwardedToTendererWithVariant, lot.AwardContract.AwardedContract.NotPublicFields.AwardedToTendererWithVariant, typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.AwardedToTendererWithVariant), lot.LotNumber, "V.2.9");
            changes.Add(parentLot.AwardContract.AwardedContract.NotPublicFields.AbnormallyLowTendersExcluded, lot.AwardContract.AwardedContract.NotPublicFields.AbnormallyLowTendersExcluded, typeof(ContractAwardNotPublicFields), nameof(ContractAwardNotPublicFields.AwardedToTendererWithVariant), lot.LotNumber, "V.2.10");

        }
        #endregion

        #region Section V: Results of contest
        private static void ResultOfContest(NoticeContract notice, NoticeContract parent, List<Change> changes)
        {
            if(notice.Type != NoticeType.DesignContestResults)
            {
                return;
            }

            changes.Add(parent.ResultsOfContest.ContestWasTerminated, notice.ResultsOfContest.ContestWasTerminated, typeof(ResultsOfContest), nameof(ResultsOfContest.ContestWasTerminated));

            changes.Add(parent.ResultsOfContest.OriginalEsender.TedNoDocExt, notice.ResultsOfContest.OriginalEsender.TedNoDocExt, typeof(Esender), nameof(Esender.TedNoDocExt));
            changes.AddEnum(parent.ResultsOfContest.NoPrizeType, notice.ResultsOfContest.NoPrizeType, typeof(ResultsOfContest), nameof(ResultsOfContest.NoPrizeType));
            changes.AddDate(parent.ResultsOfContest.OriginalNoticeSentDate, notice.ResultsOfContest.OriginalNoticeSentDate, typeof(ResultsOfContest), nameof(ResultsOfContest.OriginalNoticeSentDate));

            changes.AddDate(parent.ResultsOfContest.DateOfJuryDecision, notice.ResultsOfContest.DateOfJuryDecision, typeof(ResultsOfContest), nameof(ResultsOfContest.DateOfJuryDecision));

            if (!notice.ResultsOfContest.DisagreeParticipantCountPublish)
            {
                changes.Add(parent.ResultsOfContest.ParticipantsContemplated, notice.ResultsOfContest.ParticipantsContemplated, typeof(ResultsOfContest), nameof(ResultsOfContest.ParticipantsContemplated));
                changes.Add(parent.ResultsOfContest.ParticipantsSme, notice.ResultsOfContest.ParticipantsSme, typeof(ResultsOfContest), nameof(ResultsOfContest.ParticipantsSme));
                changes.Add(parent.ResultsOfContest.ParticipantsForeign, notice.ResultsOfContest.ParticipantsForeign, typeof(ResultsOfContest), nameof(ResultsOfContest.ParticipantsForeign));
            }

            if (!notice.ResultsOfContest.DisagreeValuePublish)
            {
                changes.Add(parent.ResultsOfContest.ValueOfPrize.Value, notice.ResultsOfContest.ValueOfPrize.Value, typeof(ResultsOfContest), nameof(ResultsOfContest.ValueOfPrize));
                changes.Add(parent.ResultsOfContest.ValueOfPrize.Currency, notice.ResultsOfContest.ValueOfPrize.Currency, typeof(ResultsOfContest), nameof(ResultsOfContest.ValueOfPrize), null, "V.3.4");
            }
        }
        #endregion

        #region Section VI: Complementary information
        /// <summary>
        /// Section VI: Complementary information
        /// </summary>
        /// <param name="notice">The notice</param>/param>
        /// <param name="parent">The parent notice</param>
        /// <param name="changes">List of changes to append to</param>
        private static void ComplementaryInfo(NoticeContract notice, NoticeContract parent, List<Change> changes)
        {
            var comp = notice.ComplementaryInformation;
            var parentComp = parent.ComplementaryInformation;

            // VI.1
            changes.Add(parentComp.IsRecurringProcurement, comp.IsRecurringProcurement, typeof(ComplementaryInformation), nameof(ComplementaryInformation.IsRecurringProcurement));
            changes.Add(parentComp.EstimatedTimingForFurtherNoticePublish, comp.EstimatedTimingForFurtherNoticePublish, typeof(ComplementaryInformation), nameof(ComplementaryInformation.EstimatedTimingForFurtherNoticePublish));

            // VI.2
            changes.Add(parentComp.ElectronicOrderingUsed, comp.ElectronicOrderingUsed, typeof(ComplementaryInformation), nameof(ComplementaryInformation.ElectronicOrderingUsed));
            changes.Add(parentComp.ElectronicInvoicingUsed, comp.ElectronicInvoicingUsed, typeof(ComplementaryInformation), nameof(ComplementaryInformation.ElectronicInvoicingUsed));
            changes.Add(parentComp.ElectronicPaymentUsed, comp.ElectronicPaymentUsed, typeof(ComplementaryInformation), nameof(ComplementaryInformation.ElectronicPaymentUsed));

            // VI.3
            changes.Add(parentComp.AdditionalInformation, comp.AdditionalInformation, typeof(ComplementaryInformation), nameof(ComplementaryInformation.AdditionalInformation));

            // VI.4
            changes.Add(_parent.ProceduresForReview?.ReviewProcedure, _notice.ProceduresForReview?.ReviewProcedure, typeof(ProceduresForReviewInformation), nameof(ProceduresForReviewInformation.ReviewProcedure));
        }
        #endregion

        #region Section VII: Modifications to the contract/concession
        private static void Modification(NoticeContract notice, NoticeContract parent, List<Change> changes)
        {
            if(notice.Modifications == null && parent.Modifications == null)
            {
                return;
            }

            changes.Add(parent.Modifications?.Description, notice.Modifications?.Description, typeof(Modifications), nameof(Modifications.Description));

            changes.Add(parent.Modifications?.IncreaseAfterModifications.Currency, notice.Modifications?.IncreaseAfterModifications.Currency, typeof(Modifications), nameof(Modifications.IncreaseAfterModifications));
            changes.Add(parent.Modifications?.IncreaseAfterModifications.Value, notice.Modifications?.IncreaseAfterModifications.Value, typeof(Modifications), nameof(Modifications.IncreaseAfterModifications));

            changes.Add(parent.Modifications?.IncreaseBeforeModifications.Currency, notice.Modifications?.IncreaseBeforeModifications.Currency, typeof(Modifications), nameof(Modifications.IncreaseBeforeModifications));
            changes.Add(parent.Modifications?.IncreaseBeforeModifications.Value, notice.Modifications?.IncreaseBeforeModifications.Value, typeof(Modifications), nameof(Modifications.IncreaseBeforeModifications));

            if(parent.Modifications != null && notice.Modifications != null)
            {
                changes.AddEnum(parent.Modifications.Reason, notice.Modifications.Reason, typeof(Modifications), nameof(Modifications.Reason));
                changes.Add(parent.Modifications.ReasonDescriptionCircumstances, notice.Modifications.ReasonDescriptionCircumstances, typeof(Modifications), nameof(Modifications.ReasonDescriptionCircumstances));
                changes.Add(parent.Modifications.ReasonDescriptionEconomic, notice.Modifications.ReasonDescriptionEconomic, typeof(Modifications), nameof(Modifications.ReasonDescriptionEconomic));
            }
        }
        #endregion

        #region Section AD1-AD4

        public static void AnnexChanges(NoticeContract notice, NoticeContract parent, List<Change> changes)
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

        public static void AnnexD1Changes(NoticeContract notice, NoticeContract parent, List<Change> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD1
            {
                Justification = notice.Annexes?.D1?.Justification
            } : notice.Annexes.D1;
            var old = clearParentFields ? new AnnexD1
            {
                Justification = notice.Annexes?.D1?.Justification
            } : parent.Annexes.D1;

            changes.AddEnum(old.NoTenders ? old.ProcedureType : AnnexProcedureType.Undefined,
                annex.NoTenders ? annex.ProcedureType : AnnexProcedureType.Undefined, typeof(AnnexD1), nameof(AnnexD1.ProcedureType));

            changes.Add(old.SuppliesManufacturedForResearch,
                annex.SuppliesManufacturedForResearch, typeof(AnnexD1), nameof(AnnexD1.SuppliesManufacturedForResearch));

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition : ReasonForNoCompetition.Undefined,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition : ReasonForNoCompetition.Undefined, typeof(AnnexD1), nameof(AnnexD1.ReasonForNoCompetition));

            changes.Add(old.ExtremeUrgency,
                annex.ExtremeUrgency, typeof(AnnexD1), nameof(AnnexD1.ExtremeUrgency));

            changes.Add(old.AdditionalDeliveries,
                annex.AdditionalDeliveries, typeof(AnnexD1), nameof(AnnexD1.AdditionalDeliveries));

            changes.Add(old.RepetitionExisting,
                annex.RepetitionExisting, typeof(AnnexD1), nameof(AnnexD1.RepetitionExisting));

            changes.Add(old.DesignContestAward,
                annex.DesignContestAward, typeof(AnnexD1), nameof(AnnexD1.DesignContestAward));

            changes.Add(old.CommodityMarket,
                annex.CommodityMarket, typeof(AnnexD1), nameof(AnnexD1.CommodityMarket));

            changes.AddEnum(old.AdvantageousTerms ? old.AdvantageousPurchaseReason : AdvantageousPurchaseReason.Undefined,
                annex.AdvantageousTerms ? annex.AdvantageousPurchaseReason : AdvantageousPurchaseReason.Undefined, typeof(AnnexD1), nameof(AnnexD1.AdvantageousPurchaseReason));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD1), nameof(AnnexD1.Justification));
        }

        public static void AnnexD2Changes(NoticeContract notice, NoticeContract parent, List<Change> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD2
            {
                Justification = notice.Annexes?.D2?.Justification
            } : notice.Annexes.D2;
            var old = clearParentFields ? new AnnexD2
            {
                Justification = parent.Annexes?.D2?.Justification
            } : parent.Annexes.D2;

            changes.Add(old.NoTenders,
                annex.NoTenders, typeof(AnnexD2), nameof(AnnexD2.NoTenders));

            changes.Add(old.PureResearch,
                annex.PureResearch, typeof(AnnexD2), nameof(AnnexD2.PureResearch));

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition : ReasonForNoCompetition.Undefined,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition : ReasonForNoCompetition.Undefined, typeof(AnnexD2), nameof(AnnexD2.ReasonForNoCompetition));

            changes.Add(old.ExtremeUrgency,
                annex.ExtremeUrgency, typeof(AnnexD2), nameof(AnnexD2.ExtremeUrgency));

            changes.Add(old.AdditionalDeliveries,
                annex.AdditionalDeliveries, typeof(AnnexD2), nameof(AnnexD2.AdditionalDeliveries));

            changes.Add(old.RepetitionExisting,
                annex.RepetitionExisting, typeof(AnnexD2), nameof(AnnexD2.RepetitionExisting));

            changes.Add(old.DesignContestAward,
                annex.DesignContestAward, typeof(AnnexD2), nameof(AnnexD2.DesignContestAward));

            changes.Add(old.CommodityMarket,
                annex.CommodityMarket, typeof(AnnexD2), nameof(AnnexD2.CommodityMarket));

            changes.AddEnum(old.AdvantageousTerms ? old.AdvantageousPurchaseReason : AdvantageousPurchaseReason.Undefined,
                annex.AdvantageousTerms ? annex.AdvantageousPurchaseReason : AdvantageousPurchaseReason.Undefined, typeof(AnnexD2), nameof(AnnexD2.AdvantageousPurchaseReason));

            changes.Add(old.BargainPurchase,
                annex.BargainPurchase, typeof(AnnexD2), nameof(AnnexD2.BargainPurchase));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD2), nameof(AnnexD2.Justification));
        }

        public static void AnnexD3Changes(NoticeContract notice, NoticeContract parent, List<Change> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD3
            {
                Justification = notice.Annexes?.D3?.Justification
            } : notice.Annexes.D3;
            var old = clearParentFields ? new AnnexD3
            {
                Justification = notice.Annexes?.D3?.Justification
            } : parent.Annexes.D3;

            changes.AddEnum(old.NoTenders ? old.ProcedureType : AnnexProcedureType.Undefined,
                annex.NoTenders ? annex.ProcedureType : AnnexProcedureType.Undefined, typeof(AnnexD3), nameof(AnnexD3.ProcedureType));

            changes.Add(old.OtherServices,
                annex.OtherServices, typeof(AnnexD3), nameof(AnnexD3.OtherServices));

            changes.Add(old.ProductsManufacturedForResearch,
                annex.ProductsManufacturedForResearch, typeof(AnnexD3), nameof(AnnexD3.ProductsManufacturedForResearch));

            changes.Add(old.AllTenders,
                annex.AllTenders, typeof(AnnexD3), nameof(AnnexD3.AllTenders));

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition : ReasonForNoCompetition.Undefined,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition : ReasonForNoCompetition.Undefined, typeof(AnnexD3), nameof(AnnexD3.ReasonForNoCompetition));

            changes.Add(old.CrisisUrgency,
                annex.CrisisUrgency, typeof(AnnexD3), nameof(AnnexD3.CrisisUrgency));

            changes.Add(old.ExtremeUrgency,
                annex.ExtremeUrgency, typeof(AnnexD3), nameof(AnnexD3.ExtremeUrgency));

            changes.Add(old.AdditionalDeliveries,
                annex.AdditionalDeliveries, typeof(AnnexD3), nameof(AnnexD3.AdditionalDeliveries));

            changes.Add(old.RepetitionExisting,
                annex.RepetitionExisting, typeof(AnnexD3), nameof(AnnexD3.RepetitionExisting));

            changes.Add(old.CommodityMarket,
                annex.CommodityMarket, typeof(AnnexD3), nameof(AnnexD3.CommodityMarket));

            changes.AddEnum(old.AdvantageousTerms ? old.AdvantageousPurchaseReason : AdvantageousPurchaseReason.Undefined,
                annex.AdvantageousTerms ? annex.AdvantageousPurchaseReason : AdvantageousPurchaseReason.Undefined, typeof(AnnexD3), nameof(AnnexD3.AdvantageousPurchaseReason));

            changes.Add(old.MaritimeService,
                annex.MaritimeService, typeof(AnnexD3), nameof(AnnexD3.MaritimeService));

            changes.AddEnum(parent.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1Other ? old.OtherJustification : D3OtherJustificationOptions.ContractServicesListedInDirective,
                notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1Other ? annex.OtherJustification : D3OtherJustificationOptions.ContractServicesListedInDirective, typeof(AnnexD3), nameof(AnnexD3.OtherJustification));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD3), nameof(AnnexD3.Justification));
        }

        public static void AnnexD4Changes(NoticeContract notice, NoticeContract parent, List<Change> changes, bool clearAllFields, bool clearParentFields)
        {
            var annex = clearAllFields ? new AnnexD4
            {
                Justification = notice.Annexes?.D4?.Justification
            } : notice.Annexes.D4;
            var old = clearParentFields ? new AnnexD4
            {
                Justification = notice.Annexes?.D4?.Justification
            } : parent.Annexes.D4;

            changes.AddEnum(old.ProvidedByOnlyParticularOperator ? old.ReasonForNoCompetition : ReasonForNoCompetition.Undefined,
                annex.ProvidedByOnlyParticularOperator ? annex.ReasonForNoCompetition : ReasonForNoCompetition.Undefined, typeof(AnnexD3), nameof(AnnexD3.ReasonForNoCompetition));

            changes.Add(old.Justification, annex.Justification, typeof(AnnexD3), nameof(AnnexD3.Justification));
        }
        #endregion
    }
}
