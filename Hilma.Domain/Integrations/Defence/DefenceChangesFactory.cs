using System;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Extensions;
using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.Configuration;
using System.Threading;

namespace Hilma.Domain.Integrations.Defence
{
    /// <summary>
    /// Factory for generating changes -node for Corrigendum notice
    /// </summary>
    public class DefenceChangesFactory
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
        public DefenceChangesFactory(NoticeContract notice, NoticeContract parent, ITranslationProvider translationProvider)
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

            // Section VI: Complementary information
            ComplementaryInfo(_notice, _parent, changes);

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
            changes.Add(parentOrg.Information?.OfficialName, org.Information.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName), section: "I.1");
            changes.Add(parentOrg.Information?.NationalRegistrationNumber, org.Information.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber), section: "I.1");
            changes.AddNuts(parentOrg.Information?.NutsCodes, org.Information.NutsCodes, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NutsCodes), section: "I.1");

            // Default Ted section is I.1 for PostalAddress. Use overload if used elsewhere
            changes.Add(parentOrg.Information?.PostalAddress?.StreetAddress, org.Information?.PostalAddress?.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress));
            changes.Add(parentOrg.Information?.PostalAddress?.Town, org.Information?.PostalAddress?.Town, typeof(PostalAddress), nameof(PostalAddress.Town));
            changes.Add(parentOrg.Information?.PostalAddress?.PostalCode, org.Information?.PostalAddress?.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode));
            changes.Add(parentOrg.Information?.PostalAddress?.Country, org.Information?.PostalAddress?.Country, typeof(PostalAddress), nameof(PostalAddress.Country));
            changes.Add(parentOrg.Information?.Email, org.Information?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email));
            changes.Add(parentOrg.Information?.MainUrl, org.Information?.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl));
            changes.Add(parentOrg.Information?.TelephoneNumber, org.Information?.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber));

            changes.Add(parent.ContactPerson?.Name, notice.ContactPerson?.Name, typeof(ContactPerson), nameof(ContactPerson.Name));

            // I.1 Communication
            var parentCommunicationInfo = parent.CommunicationInformation;
            var communicationInformation = notice.CommunicationInformation;
            changes.AddEnum(parentCommunicationInfo.ProcurementDocumentsAvailable, communicationInformation.ProcurementDocumentsAvailable, typeof(CommunicationInformation), nameof(CommunicationInformation.ProcurementDocumentsAvailable), section: "I.1");

            if (!communicationInformation.DocumentsEntirelyInHilma && !parentCommunicationInfo.DocumentsEntirelyInHilma)
            {
                changes.Add(parentCommunicationInfo.ProcurementDocumentsUrl, communicationInformation.ProcurementDocumentsUrl, typeof(CommunicationInformation), nameof(CommunicationInformation.ProcurementDocumentsUrl), section: "I.1");
            }

            changes.AddEnum(parentCommunicationInfo.AdditionalInformation, communicationInformation.AdditionalInformation, typeof(CommunicationInformation), nameof(CommunicationInformation.AdditionalInformation), section: "I.1");

            var additionalAddress = communicationInformation.AdditionalInformation == AdditionalInformationAvailability.AddressAnother ? communicationInformation.AdditionalInformationAddress : null;
            var parentAdditionalAddress = parentCommunicationInfo.AdditionalInformation == AdditionalInformationAvailability.AddressAnother ? parentCommunicationInfo.AdditionalInformationAddress : null;

            changes.Add(parentAdditionalAddress?.Email, additionalAddress?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email), section: "I.1");
            changes.Add(parentAdditionalAddress?.MainUrl, additionalAddress?.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl), section: "I.1");
            changes.Add(parentAdditionalAddress?.NationalRegistrationNumber, additionalAddress?.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber), section: "I.1");

            if (parentAdditionalAddress != null && parentAdditionalAddress.NutsCodes != null && parentAdditionalAddress.NutsCodes.Any())
            {
                changes.AddNuts(parentAdditionalAddress?.NutsCodes, additionalAddress?.NutsCodes, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NutsCodes), section: "I.1");
            }
            changes.Add(parentAdditionalAddress?.OfficialName, additionalAddress?.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName), section: "I.1");
            changes.Add(parentAdditionalAddress?.PostalAddress?.StreetAddress, additionalAddress?.PostalAddress?.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), section: "I.1");
            changes.Add(parentAdditionalAddress?.PostalAddress?.Town, additionalAddress?.PostalAddress?.Town, typeof(PostalAddress), nameof(PostalAddress.Town), section: "I.1");
            changes.Add(parentAdditionalAddress?.PostalAddress?.PostalCode, additionalAddress?.PostalAddress.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), section: "I.1");
            changes.Add(parentAdditionalAddress?.PostalAddress?.Country, additionalAddress?.PostalAddress.Country, typeof(PostalAddress), nameof(PostalAddress.Country), section: "I.1");
            changes.Add(parentAdditionalAddress?.TelephoneNumber, additionalAddress?.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber), section: "I.1");

            changes.Add(parentCommunicationInfo.SendTendersOption.ToTEDChangeFormat(), communicationInformation.SendTendersOption.ToTEDChangeFormat(), typeof(CommunicationInformation), nameof(CommunicationInformation.SendTendersOption), section: "I.1");
            changes.Add(parentCommunicationInfo.ElectronicAddressToSendTenders, communicationInformation.ElectronicAddressToSendTenders, typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicAddressToSendTenders), section: "I.1");

            var addressToSendTenders = communicationInformation.SendTendersOption == TenderSendOptions.AddressFollowing ? communicationInformation.AddressToSendTenders : null;
            var parentAddressToSendTenders = parentCommunicationInfo.SendTendersOption == TenderSendOptions.AddressFollowing ? parentCommunicationInfo.AddressToSendTenders : null;

            changes.Add(parentAddressToSendTenders?.Email, addressToSendTenders?.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email), section: "I.1");
            changes.Add(parentAddressToSendTenders?.MainUrl, addressToSendTenders?.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl), section: "I.1");
            changes.Add(parentAddressToSendTenders?.NationalRegistrationNumber, addressToSendTenders?.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber), section: "I.1");
            if (parentAddressToSendTenders != null && parentAddressToSendTenders.NutsCodes != null && parentAddressToSendTenders.NutsCodes.Any())
            {
                changes.AddNuts(parentAddressToSendTenders?.NutsCodes, addressToSendTenders?.NutsCodes, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NutsCodes), section: "I.1");
            }
            changes.Add(parentAddressToSendTenders?.OfficialName, addressToSendTenders?.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName), section: "I.1");
            changes.Add(parentAddressToSendTenders?.PostalAddress.StreetAddress, addressToSendTenders?.PostalAddress.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), section: "I.1");
            changes.Add(parentAddressToSendTenders?.PostalAddress.Town, addressToSendTenders?.PostalAddress?.Town, typeof(PostalAddress), nameof(PostalAddress.Town), section: "I.1");
            changes.Add(parentAddressToSendTenders?.PostalAddress.PostalCode, addressToSendTenders?.PostalAddress?.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), section: "I.1");
            changes.Add(parentAddressToSendTenders?.PostalAddress.Country, addressToSendTenders?.PostalAddress?.Country, typeof(PostalAddress), nameof(PostalAddress.Country), section: "I.1");
            changes.Add(parentAddressToSendTenders?.TelephoneNumber, addressToSendTenders?.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber), section: "I.1");

            changes.Add(parentCommunicationInfo.ElectronicCommunicationInfoUrl, communicationInformation.ElectronicCommunicationInfoUrl, typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicCommunicationInfoUrl), section: "I.1");
            changes.Add(parentCommunicationInfo.ElectronicCommunicationRequiresSpecialTools, communicationInformation.ElectronicCommunicationRequiresSpecialTools, typeof(CommunicationInformation), nameof(CommunicationInformation.ElectronicCommunicationRequiresSpecialTools), section: "I.1");

            // I.2 Type of the conrtacting authority
            changes.AddEnum(parentOrg.ContractingAuthorityType, org.ContractingAuthorityType, typeof(OrganisationContract), nameof(OrganisationContract.ContractingAuthorityType), section: "I.2");
            changes.Add(parentOrg.OtherContractingAuthorityType, org.OtherContractingAuthorityType, typeof(OrganisationContract), nameof(OrganisationContract.OtherContractingAuthorityType), section: "I.2");

            // I.3 Main activity
            if (org.MainActivity != MainActivity.Undefined)
            {
                changes.AddEnum(parentOrg.MainActivity, org.MainActivity, typeof(OrganisationContract), nameof(OrganisationContract.MainActivity), section: "I.3");
            }

            if (org.MainActivityUtilities != MainActivityUtilities.Undefined)
            {
                changes.AddEnum(parentOrg.MainActivityUtilities, org.MainActivityUtilities, typeof(OrganisationContract), nameof(OrganisationContract.MainActivityUtilities), section: "I.3");
            }

            changes.Add(parentOrg.OtherMainActivity, org.OtherMainActivity, typeof(OrganisationContract), nameof(OrganisationContract.OtherMainActivity), section: "I.3");

            // I.4 Joint procurement
            changes.Add(parent.Project.JointProcurement, notice.Project.JointProcurement, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.JointProcurement), section: "I.4");
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
            // II.1
            changes.Add(parent.Project.Title, notice.Project.Title, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.Title), section: "II.1");

            // II.2 or II.1.2 Type of contract and location of works, place of delivery or of performance
            var typeAndLocationSection = notice.Type == NoticeType.DefencePriorInformation ? "II.2" : "II.1.2";
            changes.AddEnum(parent.Project.ContractType, notice.Project.ContractType, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.ContractType), section: typeAndLocationSection);
            changes.Add(parent.ProcurementObject.Defence.MainsiteplaceWorksDelivery, notice.ProcurementObject.Defence.MainsiteplaceWorksDelivery, typeof(ProcurementObjectDefence), nameof(ProcurementObjectDefence.MainsiteplaceWorksDelivery), section: typeAndLocationSection);

            var frameworkSection = notice.Type == NoticeType.DefencePriorInformation ? "II.3" : "II.1.3";
            var framework = notice.ProcurementObject.Defence.FrameworkAgreement;
            var parentFramework = parent.ProcurementObject.Defence.FrameworkAgreement;

            changes.Add(parentFramework.IncludesFrameworkAgreement, framework.IncludesFrameworkAgreement, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.IncludesFrameworkAgreement), section: frameworkSection);

            if (framework != null && parentFramework != null)
            {
                changes.AddEnum(parentFramework.FrameworkAgreementType, framework.FrameworkAgreementType, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.FrameworkAgreementType), section: "II.1.4");
                changes.Add(parentFramework.EnvisagedNumberOfParticipants, framework.EnvisagedNumberOfParticipants, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.EnvisagedNumberOfParticipants), section: "II.1.4");
                changes.Add(parentFramework.Duration.Days, framework.Duration.Days, typeof(TimeFrame), nameof(TimeFrame.Years), section: "II.1.4");
                changes.Add(parentFramework.Duration.Months, framework.Duration.Months, typeof(TimeFrame), nameof(TimeFrame.Years), section: "II.1.4");
                changes.Add(parentFramework.JustificationForDurationOverSevenYears, framework.JustificationForDurationOverSevenYears, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.JustificationForDurationOverSevenYears));
                changes.Add(parentFramework.EstimatedTotalValue.Value, framework.EstimatedTotalValue.Value, typeof(ValueRangeContract), nameof(ValueRangeContract.Value), section: "II.1.4");
                changes.Add(parentFramework.EstimatedTotalValue.MinValue, framework.EstimatedTotalValue.MinValue, typeof(ValueRangeContract), nameof(ValueRangeContract.MinValue), section: "II.1.4");
                changes.Add(parentFramework.EstimatedTotalValue.MaxValue, framework.EstimatedTotalValue.MaxValue, typeof(ValueRangeContract), nameof(ValueRangeContract.MaxValue), section: "II.1.4");
                changes.Add(parentFramework.EstimatedTotalValue.Currency, framework.EstimatedTotalValue.Currency, typeof(ValueRangeContract), nameof(ValueRangeContract.Currency), section: "II.1.4");
                changes.Add(parentFramework.FrequencyAndValue, framework.FrequencyAndValue, typeof(FrameworkAgreementInformation), nameof(FrameworkAgreementInformation.FrequencyAndValue));
            }

            // II.1.5 for contract and II.1.4 for award
            var shortDescrSection = notice.Type == NoticeType.DefenceContract ? "II.1.5" : "II.1.4";
            changes.Add(parent.ProcurementObject.ShortDescription, notice.ProcurementObject.ShortDescription, typeof(ProcurementObject), nameof(ProcurementObject.ShortDescription), section: shortDescrSection);

            changes.Add(parent.ProcurementObject.EstimatedValue.Value, notice.ProcurementObject.EstimatedValue.Value, typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValue), section: "II.4");
            changes.Add(parent.ProcurementObject.EstimatedValue.MinValue, notice.ProcurementObject.EstimatedValue.MinValue, typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValue), section: "II.4");
            changes.Add(parent.ProcurementObject.EstimatedValue.MaxValue, notice.ProcurementObject.EstimatedValue.MaxValue, typeof(ProcurementObject), nameof(ProcurementObject.EstimatedValue), section: "II.4");
            changes.Add(parent.ProcurementObject.EstimatedValue.Currency, notice.ProcurementObject.EstimatedValue.Currency, typeof(ValueRangeContract), nameof(ValueRangeContract.Currency), section: "II.4");

            // CPV
            var cpvSection = "";
            switch (notice.Type)
            {
                case NoticeType.DefencePriorInformation: cpvSection = "II.5"; break;
                case NoticeType.DefenceContract: cpvSection = "II.1.6"; break;
                case NoticeType.DefenceContractAward: cpvSection = "II.1.5"; break;
            }

            changes.AddCpv(parent.ProcurementObject.MainCpvCode, notice.ProcurementObject.MainCpvCode, typeof(ProcurementObject), nameof(ProcurementObject.MainCpvCode), section: cpvSection);
            changes.AddAdditionalCpv(parent.ProcurementObject.Defence.AdditionalCpvCodes.ToList(), parent.ProcurementObject.Defence.AdditionalCpvCodes.ToList(), typeof(ObjectDescription), nameof(ObjectDescription.AdditionalCpvCodes), section: cpvSection);

            // II.6 (prior notice)
            changes.AddDate(parent.ProcurementObject.Defence.TimeFrame.ScheduledStartDateOfAwardProcedures, notice.ProcurementObject.Defence.TimeFrame.ScheduledStartDateOfAwardProcedures, typeof(TimeFrame), nameof(TimeFrame.ScheduledStartDateOfAwardProcedures));

            // II.7 (prior notice)
            changes.Add(parent.ProcurementObject.Defence.AdditionalInformation, notice.ProcurementObject.Defence.AdditionalInformation, typeof(ProcurementObjectDefence), nameof(ProcurementObjectDefence.AdditionalInformation));

            // Back to contract notice..
            // II.1.7) Information about subcontracting
            changes.Add(parent.ProcurementObject.Defence.Subcontract.TendererHasToIndicateShare, notice.ProcurementObject.Defence.Subcontract.TendererHasToIndicateShare, typeof(SubcontractingInformation), nameof(SubcontractingInformation.TendererHasToIndicateShare));
            changes.Add(parent.ProcurementObject.Defence.Subcontract.TendererHasToIndicateChange, notice.ProcurementObject.Defence.Subcontract.TendererHasToIndicateChange, typeof(SubcontractingInformation), nameof(SubcontractingInformation.TendererHasToIndicateChange));
            changes.Add(parent.ProcurementObject.Defence.Subcontract.CaMayOblige, notice.ProcurementObject.Defence.Subcontract.CaMayOblige, typeof(SubcontractingInformation), nameof(SubcontractingInformation.CaMayOblige));
            changes.Add(parent.ProcurementObject.Defence.Subcontract.SuccessfulTenderer, notice.ProcurementObject.Defence.Subcontract.SuccessfulTenderer, typeof(SubcontractingInformation), nameof(SubcontractingInformation.TendererHasToIndicateShare));
            changes.Add(parent.ProcurementObject.Defence.Subcontract.SuccessfulTendererMin, notice.ProcurementObject.Defence.Subcontract.SuccessfulTendererMin, typeof(SubcontractingInformation), nameof(SubcontractingInformation.SuccessfulTendererMin));
            changes.Add(parent.ProcurementObject.Defence.Subcontract.SuccessfulTendererMax, notice.ProcurementObject.Defence.Subcontract.SuccessfulTendererMax, typeof(SubcontractingInformation), nameof(SubcontractingInformation.SuccessfulTendererMax));
            changes.Add(parent.ProcurementObject.Defence.Subcontract.SuccessfulTendererToSpecify, notice.ProcurementObject.Defence.Subcontract.SuccessfulTendererToSpecify, typeof(SubcontractingInformation), nameof(SubcontractingInformation.SuccessfulTendererToSpecify));

            // II.1.8) Amount of lots cannot be changed!

            // II.1.9) Information about variants
            changes.Add(parent.ProcurementObject.Defence.OptionsAndVariants.VariantsWillBeAccepted, notice.ProcurementObject.Defence.OptionsAndVariants.VariantsWillBeAccepted, typeof(OptionsAndVariants), nameof(OptionsAndVariants.VariantsWillBeAccepted), section: "II.1.9");

            // II.2) Quantity or scope of the contract (contract notices)
            // II.2.1) Total quantity or scope (contract notices)
            changes.Add(parent.ProcurementObject.Defence.TotalQuantity, notice.ProcurementObject.Defence.TotalQuantity, typeof(ProcurementObjectDefence), nameof(ProcurementObjectDefence.TotalQuantity));
            changes.Add(parent.ProcurementObject.Defence.TotalQuantityOrScope.Value, notice.ProcurementObject.Defence.TotalQuantityOrScope.Value, typeof(ValueRangeContract), nameof(ValueRangeContract.Value), section: "II.2.1");
            changes.Add(parent.ProcurementObject.Defence.TotalQuantityOrScope.MinValue, notice.ProcurementObject.Defence.TotalQuantityOrScope.MinValue, typeof(ValueRangeContract), nameof(ValueRangeContract.MinValue), section: "II.2.1");
            changes.Add(parent.ProcurementObject.Defence.TotalQuantityOrScope.MaxValue, notice.ProcurementObject.Defence.TotalQuantityOrScope.MaxValue, typeof(ValueRangeContract), nameof(ValueRangeContract.MaxValue), section: "II.2.1");
            changes.Add(parent.ProcurementObject.Defence.TotalQuantityOrScope.Currency, notice.ProcurementObject.Defence.TotalQuantityOrScope.Currency, typeof(ValueRangeContract), nameof(ValueRangeContract.Currency), section: "II.2.1");

            // II.2.2) Information about options (contract notices)
            changes.Add(parent.ProcurementObject.Defence.OptionsAndVariants.Options, notice.ProcurementObject.Defence.OptionsAndVariants.Options, typeof(OptionsAndVariants), nameof(OptionsAndVariants.Options), section: "II.2.2");
            changes.Add(parent.ProcurementObject.Defence.OptionsAndVariants.OptionsDescription, notice.ProcurementObject.Defence.OptionsAndVariants.OptionsDescription, typeof(OptionsAndVariants), nameof(OptionsAndVariants.OptionsDescription), section: "II.2.2");
            changes.Add(parent.ProcurementObject.Defence.OptionsAndVariants.OptionsDays, notice.ProcurementObject.Defence.OptionsAndVariants.OptionsDays, typeof(OptionsAndVariants), nameof(OptionsAndVariants.OptionsDays), section: "II.2.2");
            changes.Add(parent.ProcurementObject.Defence.OptionsAndVariants.OptionsMonths, notice.ProcurementObject.Defence.OptionsAndVariants.OptionsMonths, typeof(OptionsAndVariants), nameof(OptionsAndVariants.OptionsMonths), section: "II.2.2");

            // II.2.3) Information about renewals (contract notices)
            changes.Add(parent.ProcurementObject.Defence.Renewals.CanBeRenewed, notice.ProcurementObject.Defence.Renewals.CanBeRenewed, typeof(DefenceRenewals), nameof(DefenceRenewals.CanBeRenewed), section: "II.2.3");
            changes.Add(parent.ProcurementObject.Defence.Renewals.Amount.Value, notice.ProcurementObject.Defence.Renewals.Amount.Value, typeof(DefenceRenewals), nameof(DefenceRenewals.Amount));
            changes.Add(parent.ProcurementObject.Defence.Renewals.Amount.MinValue, notice.ProcurementObject.Defence.Renewals.Amount.MinValue, typeof(DefenceRenewals), nameof(DefenceRenewals.Amount));
            changes.Add(parent.ProcurementObject.Defence.Renewals.Amount.MaxValue, notice.ProcurementObject.Defence.Renewals.Amount.MaxValue, typeof(DefenceRenewals), nameof(DefenceRenewals.Amount));
            changes.Add(parent.ProcurementObject.Defence.Renewals.SubsequentContract.Months, notice.ProcurementObject.Defence.Renewals.SubsequentContract.Months, typeof(TimeFrame), nameof(TimeFrame.Months), section: "II.2.3");
            changes.Add(parent.ProcurementObject.Defence.Renewals.SubsequentContract.Days, notice.ProcurementObject.Defence.Renewals.SubsequentContract.Days, typeof(TimeFrame), nameof(TimeFrame.Days), section: "II.2.3");

            // II.2) Total final value of contract(s) (contract awards)
            changes.Add(parent.ProcurementObject.TotalValue?.Value, notice.ProcurementObject.TotalValue?.Value, typeof(ValueRangeContract), nameof(ValueRangeContract.Value), section: "II.2");
            changes.Add(parent.ProcurementObject.TotalValue?.MinValue, notice.ProcurementObject.TotalValue?.MinValue, typeof(ValueRangeContract), nameof(ValueRangeContract.MinValue), section: "II.2");
            changes.Add(parent.ProcurementObject.TotalValue?.MaxValue, notice.ProcurementObject.TotalValue?.MaxValue, typeof(ValueRangeContract), nameof(ValueRangeContract.MaxValue), section: "II.2");
            changes.Add(parent.ProcurementObject.TotalValue?.Currency, notice.ProcurementObject.TotalValue?.Currency, typeof(ValueRangeContract), nameof(ValueRangeContract.Currency), section: "II.2");

            // II.3) Duration of the contract or time limit for completion
            changes.Add(parent.ProcurementObject.Defence.TimeFrame.Months, notice.ProcurementObject.Defence.TimeFrame.Months, typeof(TimeFrame), nameof(TimeFrame.Months), section: "II.3");
            changes.Add(parent.ProcurementObject.Defence.TimeFrame.Days, notice.ProcurementObject.Defence.TimeFrame.Days, typeof(TimeFrame), nameof(TimeFrame.Days), section: "II.3");
            changes.AddDate(parent.ProcurementObject.Defence.TimeFrame.BeginDate, notice.ProcurementObject.Defence.TimeFrame.BeginDate, typeof(TimeFrame), nameof(TimeFrame.BeginDate), section: "II.3");
            changes.AddDate(parent.ProcurementObject.Defence.TimeFrame.EndDate, notice.ProcurementObject.Defence.TimeFrame.EndDate, typeof(TimeFrame), nameof(TimeFrame.EndDate), section: "II.3");

            // II.2
            // Existing lots (changes and additions)
            foreach (var objectDescription in notice.ObjectDescriptions)
            {
                var parentLot = parent.ObjectDescriptions?.FirstOrDefault(x => x.LotNumber == objectDescription.LotNumber) ?? new ObjectDescription();

                HandleLotChanges(changes, objectDescription.LotNumber, objectDescription, parentLot);
            }

            // Lot removals
            foreach (var parentObjectDescr in parent.ObjectDescriptions)
            {
                if (notice.ObjectDescriptions.FirstOrDefault(x => x.LotNumber == parentObjectDescr.LotNumber) == null)
                {
                    changes.Add(parentObjectDescr.LotNumber, null, typeof(ObjectDescription), nameof(ObjectDescription.LotNumber), parentObjectDescr.LotNumber);
                }
            }
        }

        // Annex B
        // Information about lots
        private static void HandleLotChanges(List<Change> changes, string lotNumber, ObjectDescription lot, ObjectDescription parentLot)
        {
            // Title attributed to the contract by the contracting authority/entity:
            changes.Add(parentLot?.LotNumber, lot?.LotNumber, typeof(ObjectDescription), nameof(ObjectDescription.LotNumber), lotNumber, "Annex B");
            changes.Add(parentLot.Title, lot.Title, typeof(ObjectDescription), nameof(ObjectDescription.Title), lotNumber, "Annex B");

            // 1) Short description:
            changes.Add(parentLot.DescrProcurement, lot.DescrProcurement, typeof(ObjectDescription), nameof(ObjectDescription.DescrProcurement), lotNumber, "Annex B, 1");

            // 2) Common procurement vocabulary (CPV)
            changes.AddCpv(parentLot.MainCpvCode, lot.MainCpvCode, typeof(ProcurementObject), nameof(ProcurementObject.MainCpvCode), lotNumber, "Annex B, 2");
            changes.AddAdditionalCpv(parentLot.AdditionalCpvCodes.ToList(), lot.AdditionalCpvCodes.ToList(), typeof(ObjectDescription), nameof(ObjectDescription.AdditionalCpvCodes), lotNumber, "Annex B, 2");

            // 3) Quantity or scope:
            changes.Add(parentLot.QuantityOrScope, lot.QuantityOrScope, typeof(ObjectDescription), nameof(ObjectDescription.QuantityOrScope), lotNumber, "Annex B, 3");

            changes.Add(parentLot.EstimatedValue.MinValue, lot.EstimatedValue.MinValue, typeof(ValueContract), nameof(ValueContract.Value), lotNumber, "Annex B, 3");
            changes.Add(parentLot.EstimatedValue.MaxValue, lot.EstimatedValue.MaxValue, typeof(ValueContract), nameof(ValueContract.Value), lotNumber, "Annex B, 3");
            changes.Add(parentLot.EstimatedValue.Value, lot.EstimatedValue.Value, typeof(ValueContract), nameof(ValueContract.Value), lotNumber, "Annex B, 3");
            changes.Add(parentLot.EstimatedValue.Currency, lot.EstimatedValue.Currency, typeof(ValueContract), nameof(ValueContract.Currency), lotNumber, "Annex B, 3");

            // 4) Indication about different date for start of award procedures and/or duration of contract
            changes.Add(parentLot.TimeFrame.Months, lot.TimeFrame.Months, typeof(TimeFrame), nameof(TimeFrame.Months), lotNumber, "Annex B, 4");
            changes.Add(parentLot.TimeFrame.Days, lot.TimeFrame.Days, typeof(TimeFrame), nameof(TimeFrame.Days), lotNumber, "Annex B, 4");
            changes.AddDate(parentLot.TimeFrame.BeginDate, lot.TimeFrame.BeginDate, typeof(TimeFrame), nameof(TimeFrame.BeginDate), lotNumber, "Annex B, 4");
            changes.AddDate(parentLot.TimeFrame.EndDate, lot.TimeFrame.EndDate, typeof(TimeFrame), nameof(TimeFrame.EndDate), lotNumber, "Annex B, 4");
            changes.Add(parentLot.TimeFrame.CanBeRenewed, lot.TimeFrame.CanBeRenewed, typeof(TimeFrame), nameof(TimeFrame.CanBeRenewed), lotNumber, "Annex B, 4");
            changes.Add(parentLot.TimeFrame.RenewalDescription, lot.TimeFrame.RenewalDescription, typeof(TimeFrame), nameof(TimeFrame.RenewalDescription), lotNumber, "Annex B, 4");

            // 5) Additional information about lots
            changes.Add(parentLot.AdditionalInformation, lot.AdditionalInformation, typeof(ObjectDescription), nameof(ObjectDescription.AdditionalInformation), lotNumber, "Annex B, 5");
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
            if (parent.ConditionsInformationDefence == null || notice.ConditionsInformationDefence == null)
            {
                return;
            }

            var conditions = notice.ConditionsInformationDefence;
            var parentConditions = parent.ConditionsInformationDefence;

            // III.1
            var financingSection = notice.Type == NoticeType.DefencePriorInformation ? "III.1.1" : "III.1.2";
            changes.Add(parentConditions.FinancingConditions, conditions.FinancingConditions, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.FinancingConditions), section: financingSection);
            changes.Add(parentConditions.DepositsRequired, conditions.DepositsRequired, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.DepositsRequired));
            changes.Add(parentConditions.LegalFormTaken, conditions.LegalFormTaken, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.LegalFormTaken));
            changes.Add(parentConditions.OtherParticularConditions, conditions.OtherParticularConditions, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.OtherParticularConditions));
            changes.AddDate(parentConditions.SecurityClearanceDate, conditions.SecurityClearanceDate, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.SecurityClearanceDate));

            // III.2
            changes.Add(parentConditions.PersonalSituationOfEconomicOperators, conditions.PersonalSituationOfEconomicOperators, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.PersonalSituationOfEconomicOperators));
            changes.Add(parentConditions.PersonalSituationOfSubcontractors, conditions.PersonalSituationOfSubcontractors, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.PersonalSituationOfSubcontractors));
            changes.Add(parentConditions.EconomicCriteriaOfEconomicOperators, conditions.EconomicCriteriaOfEconomicOperators, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.EconomicCriteriaOfEconomicOperators));
            changes.Add(parentConditions.EconomicCriteriaOfEconomicOperatorsMinimum, conditions.EconomicCriteriaOfEconomicOperatorsMinimum, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.EconomicCriteriaOfEconomicOperatorsMinimum));
            changes.Add(parentConditions.EconomicCriteriaOfSubcontractors, conditions.EconomicCriteriaOfSubcontractors, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.EconomicCriteriaOfSubcontractors));
            changes.Add(parentConditions.EconomicCriteriaOfSubcontractorsMinimum, conditions.EconomicCriteriaOfSubcontractorsMinimum, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.EconomicCriteriaOfSubcontractorsMinimum));
            changes.Add(parentConditions.TechnicalCriteriaOfEconomicOperators, conditions.TechnicalCriteriaOfEconomicOperators, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.TechnicalCriteriaOfEconomicOperators));
            changes.Add(parentConditions.TechnicalCriteriaOfEconomicOperatorsMinimum, conditions.TechnicalCriteriaOfEconomicOperatorsMinimum, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.TechnicalCriteriaOfEconomicOperatorsMinimum));
            changes.Add(parentConditions.TechnicalCriteriaOfSubcontractors, conditions.TechnicalCriteriaOfSubcontractors, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.TechnicalCriteriaOfSubcontractors));
            changes.Add(parentConditions.TechnicalCriteriaOfSubcontractorsMinimum, conditions.TechnicalCriteriaOfSubcontractorsMinimum, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.TechnicalCriteriaOfSubcontractorsMinimum));

            var reservedContractsSection = _notice.Type == NoticeType.DefencePriorInformation ? "III.2.1" : "III.2.4";
            changes.Add(parentConditions.RestrictedToShelteredWorkshops, conditions.RestrictedToShelteredWorkshops, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.RestrictedToShelteredWorkshops), section: reservedContractsSection);
            changes.Add(parentConditions.RestrictedToShelteredProgrammes, conditions.RestrictedToShelteredProgrammes, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.RestrictedToShelteredProgrammes), section: reservedContractsSection);

            // III.3
            changes.Add(parentConditions.RestrictedToParticularProfession, conditions.RestrictedToParticularProfession, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.RestrictedToParticularProfession));
            changes.Add(parentConditions.RestrictedToParticularProfessionLaw, conditions.RestrictedToParticularProfessionLaw, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.RestrictedToParticularProfessionLaw));
            changes.Add(parentConditions.StaffResponsibleForExecution, conditions.StaffResponsibleForExecution, typeof(ConditionsInformationDefence), nameof(ConditionsInformationDefence.StaffResponsibleForExecution));
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

            // IV 1.1 Type of procedure cannot be changed!

            // IV.1.2
            changes.Add(parentProcedureInformation.Defence.CandidateNumberRestrictions.EnvisagedNumber, procedureInformation.Defence.CandidateNumberRestrictions.EnvisagedNumber, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedNumber), section: "IV.1.2");
            changes.Add(parentProcedureInformation.Defence.CandidateNumberRestrictions.EnvisagedMinimumNumber, procedureInformation.Defence.CandidateNumberRestrictions.EnvisagedMinimumNumber, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedMinimumNumber), section: "IV.1.2");
            changes.Add(parentProcedureInformation.Defence.CandidateNumberRestrictions.EnvisagedMaximumNumber, procedureInformation.Defence.CandidateNumberRestrictions.EnvisagedMaximumNumber, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.EnvisagedMaximumNumber), section: "IV.1.2");
            changes.Add(parentProcedureInformation.Defence.CandidateNumberRestrictions.ObjectiveCriteriaForChoosing, procedureInformation.Defence.CandidateNumberRestrictions.ObjectiveCriteriaForChoosing, typeof(CandidateNumberRestrictions), nameof(CandidateNumberRestrictions.ObjectiveCriteriaForChoosing), section: "IV.1.2");

            // IV.2
            changes.AddEnum(parentProcedureInformation.Defence.AwardCriteria.CriterionTypes, procedureInformation.Defence.AwardCriteria.CriterionTypes, typeof(AwardCriteriaDefence), nameof(AwardCriteriaDefence.CriterionTypes));
            changes.AddEnum(parentProcedureInformation.Defence.AwardCriteria.EconomicCriteriaTypes, procedureInformation.Defence.AwardCriteria.EconomicCriteriaTypes, typeof(AwardCriteriaDefence), nameof(AwardCriteriaDefence.CriterionTypes));

            // Add & change AwardCriteria
            for (var a = 0; a < procedureInformation.Defence.AwardCriteria.Criteria.Length; a++)
            {
                var currentCriteria = parentProcedureInformation.Defence.AwardCriteria.Criteria[a];
                var parentCriteria = parentProcedureInformation.Defence.AwardCriteria.Criteria.ElementAtOrDefault(a);
                changes.Add(parentCriteria?.Criterion, currentCriteria?.Criterion, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Criterion), section: "IV.2.1");
                changes.Add(parentCriteria?.Weighting, currentCriteria?.Weighting, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Weighting), section: "IV.2.1");
            }

            // Removals AwardCriteria
            for (var i = procedureInformation.Defence.AwardCriteria.Criteria.Length; i < parentProcedureInformation.Defence.AwardCriteria.Criteria.Length; i++)
            {
                changes.Add(parentProcedureInformation.Defence.AwardCriteria.Criteria[i].Criterion, string.Empty, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Criterion), section: "IV.2.1");
                changes.Add(parentProcedureInformation.Defence.AwardCriteria.Criteria[i].Weighting, string.Empty, typeof(AwardCriterionDefinition), nameof(AwardCriterionDefinition.Weighting), section: "IV.2.1");
            }

            changes.Add(parentProcedureInformation.ElectronicAuctionWillBeUsed, procedureInformation.ElectronicAuctionWillBeUsed, typeof(ProcedureInformation), nameof(ProcedureInformation.ElectronicAuctionWillBeUsed), section: "IV.2.2");
            changes.Add(parentProcedureInformation.AdditionalInformationAboutElectronicAuction, procedureInformation.AdditionalInformationAboutElectronicAuction, typeof(ProcedureInformation), nameof(ProcedureInformation.AdditionalInformationAboutElectronicAuction), section: "IV.2.2");

            // IV.3
            // IV.3.1
            changes.Add(parent.Project.ReferenceNumber, notice.Project.ReferenceNumber, typeof(ProcurementProjectContract), nameof(ProcurementProjectContract.ReferenceNumber), section: "IV.3.1");

            // IV.3.2
            if (notice.TenderingInformation.Defence.PreviousPublicationExists)
            {
                if (!string.IsNullOrEmpty(notice.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber.Number) && !string.IsNullOrEmpty(parent.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber.Number))
                {
                    changes.Add(parent.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber.Number, notice.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber.Number, typeof(OjsNumber), nameof(OjsNumber.Number));
                    changes.AddDate(parent.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber.Date, notice.TenderingInformation.Defence.PreviousPriorInformationNoticeOjsNumber.Date, typeof(OjsNumber), nameof(OjsNumber.Date));
                }
                if (!string.IsNullOrEmpty(parent.TenderingInformation.Defence.PreviousContractNoticeOjsNumber.Number) && !string.IsNullOrEmpty(parent.TenderingInformation.Defence.PreviousContractNoticeOjsNumber.Number))
                {
                    changes.Add(parent.TenderingInformation.Defence.HasPreviousContractNoticeOjsNumber, notice.TenderingInformation.Defence.HasPreviousContractNoticeOjsNumber, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.HasPreviousContractNoticeOjsNumber));
                    changes.Add(parent.TenderingInformation.Defence.PreviousContractNoticeOjsNumber.Number, notice.TenderingInformation.Defence.PreviousContractNoticeOjsNumber.Number, typeof(OjsNumber), nameof(OjsNumber.Number));
                    changes.AddDate(parent.TenderingInformation.Defence.PreviousContractNoticeOjsNumber.Date, notice.TenderingInformation.Defence.PreviousContractNoticeOjsNumber.Date, typeof(OjsNumber), nameof(OjsNumber.Date));
                }
                if (!string.IsNullOrEmpty(parent.TenderingInformation.Defence.PreviousExAnteOjsNumber.Number) && !string.IsNullOrEmpty(parent.TenderingInformation.Defence.PreviousExAnteOjsNumber.Number))
                {
                    changes.Add(parent.TenderingInformation.Defence.HasPreviousExAnteOjsNumber, notice.TenderingInformation.Defence.HasPreviousExAnteOjsNumber, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.HasPreviousExAnteOjsNumber));
                    changes.Add(parent.TenderingInformation.Defence.PreviousExAnteOjsNumber.Number, notice.TenderingInformation.Defence.PreviousExAnteOjsNumber.Number, typeof(OjsNumber), nameof(OjsNumber.Number));
                    changes.AddDate(parent.TenderingInformation.Defence.PreviousExAnteOjsNumber.Date, notice.TenderingInformation.Defence.PreviousExAnteOjsNumber.Date, typeof(OjsNumber), nameof(OjsNumber.Date));
                }
            }

            // IV.3.3
            changes.AddDate(parent.TenderingInformation.Defence.TimeLimitForReceipt, notice.TenderingInformation.Defence.TimeLimitForReceipt, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.TimeLimitForReceipt));
            changes.Add(parent.TenderingInformation.Defence.PayableDocuments, notice.TenderingInformation.Defence.PayableDocuments, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.PayableDocuments));
            changes.Add(parent.TenderingInformation.Defence.DocumentPrice.Value, notice.TenderingInformation.Defence.DocumentPrice.Value, typeof(ValueContract), nameof(ValueContract.Value), section: "IV.3.3");
            changes.Add(parent.TenderingInformation.Defence.DocumentPrice.Currency, notice.TenderingInformation.Defence.DocumentPrice.Currency, typeof(ValueContract), nameof(ValueContract.Currency), section: "IV.3.3");
            changes.Add(parent.TenderingInformation.Defence.PaymentTermsAndMethods, notice.TenderingInformation.Defence.PaymentTermsAndMethods, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.PaymentTermsAndMethods));

            // IV.3.4
            changes.AddDate(parent.TenderingInformation.TendersOrRequestsToParticipateDueDateTime, notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime, typeof(TenderingInformation), nameof(TenderingInformation.TendersOrRequestsToParticipateDueDateTime), section: "IV.3.4");

            // IV.3.5
            changes.AddDate(parent.TenderingInformation.EstimatedDateOfInvitations, notice.TenderingInformation.EstimatedDateOfInvitations, typeof(TenderingInformation), nameof(TenderingInformation.EstimatedDateOfInvitations), section: "IV.3.5");

            // IV.3.6
            changes.AddEnum(parent.TenderingInformation.Defence.LanguageType, notice.TenderingInformation.Defence.LanguageType, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.LanguageType));
            changes.Add(parent.TenderingInformation.Defence.Languages, notice.TenderingInformation.Defence.Languages, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.Languages));
            changes.Add(parent.TenderingInformation.Defence.OtherLanguage, notice.TenderingInformation.Defence.OtherLanguage, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.OtherLanguage));
            changes.Add(parent.TenderingInformation.Defence.OtherLanguages, notice.TenderingInformation.Defence.OtherLanguages, typeof(DefenceAdministrativeInformation), nameof(DefenceAdministrativeInformation.OtherLanguages));
        }
        #endregion

        #region Section V: Award of contract
        private static void Award(NoticeContract notice, NoticeContract parent, List<Change> changes)
        {
            // Handle only changes - lots cannot be removed or added.
            if (notice.Type.IsContractAward() || notice.Type == NoticeType.ExAnte)
            {
                for (var i = 0; i < notice.ContractAwardsDefence.Length; i++)
                {
                    HandleAwardChanges(changes, parent.ContractAwardsDefence[i], notice.ContractAwardsDefence[i]);
                }
            }
        }

        private static void HandleAwardChanges(List<Change> changes, ContractAwardDefence parentAward, ContractAwardDefence award)
        {
            // V
            changes.Add(parentAward.LotTitle, award.LotTitle, typeof(ContractAwardDefence), nameof(ContractAwardDefence.LotTitle));

            // V.1
            changes.AddDate(parentAward.ContractAwardDecisionDate, award.ContractAwardDecisionDate, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ContractAwardDecisionDate));

            // V.2
            changes.Add(parentAward.NumberOfTenders.Total, award.NumberOfTenders.Total, typeof(NumberOfTenders), nameof(NumberOfTenders.Total), section: "V.2");
            changes.Add(parentAward.NumberOfTenders.Electronic, award.NumberOfTenders.Electronic, typeof(NumberOfTenders), nameof(NumberOfTenders.Electronic), section: "V.2");

            // V.3
            var parentContractor = parentAward.Contractor;
            var currentConrtactor = award.Contractor;
            changes.Add(parentContractor?.OfficialName, currentConrtactor?.OfficialName, typeof(ContractorContactInformation), nameof(ContractorContactInformation.OfficialName), section: "V.3");
            changes.Add(parentContractor?.NationalRegistrationNumber, currentConrtactor?.NationalRegistrationNumber, typeof(ContractorContactInformation), nameof(ContractorContactInformation.NationalRegistrationNumber), section: "V.3");
            changes.Add(parentContractor?.PostalAddress.StreetAddress, currentConrtactor?.PostalAddress.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), section: "V.3");
            changes.Add(parentContractor?.PostalAddress.Town, currentConrtactor?.PostalAddress.Town, typeof(PostalAddress), nameof(PostalAddress.Town), section: "V.3");
            changes.Add(parentContractor?.PostalAddress.PostalCode, currentConrtactor?.PostalAddress.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), section: "V.3");
            changes.Add(parentContractor?.PostalAddress.Country, currentConrtactor?.PostalAddress.Country, typeof(PostalAddress), nameof(PostalAddress.Country), section: "V.3");
            changes.AddNuts(parentContractor?.NutsCodes, currentConrtactor?.NutsCodes, typeof(ContractorContactInformation), nameof(ContractorContactInformation.NutsCodes), section: "V.3");
            changes.Add(parentContractor?.Email, currentConrtactor?.Email, typeof(ContractorContactInformation), nameof(ContractorContactInformation.Email), section: "V.3");
            changes.Add(parentContractor?.TelephoneNumber, currentConrtactor?.TelephoneNumber, typeof(ContractorContactInformation), nameof(ContractorContactInformation.TelephoneNumber), section: "V.3");
            changes.Add(parentContractor?.MainUrl, currentConrtactor?.MainUrl, typeof(ContractorContactInformation), nameof(ContractorContactInformation.MainUrl), section: "V.3");

            // V.4
            changes.Add(parentAward.EstimatedValue.Value, award.EstimatedValue.Value, typeof(ContractAwardDefence), nameof(ContractAwardDefence.EstimatedValue));
            changes.Add(parentAward.EstimatedValue.Currency, award.EstimatedValue.Currency, typeof(ContractAwardDefence), nameof(ContractAwardDefence.EstimatedValue));

            changes.Add(parentAward.FinalTotalValue.Value, award.FinalTotalValue.Value, typeof(ContractAwardDefence), nameof(ContractAwardDefence.FinalTotalValue));
            changes.Add(parentAward.FinalTotalValue.Currency, award.FinalTotalValue.Currency, typeof(ContractAwardDefence), nameof(ContractAwardDefence.FinalTotalValue));

            changes.Add(parentAward.LowestOffer.Value, award.LowestOffer.Value, typeof(ContractAwardDefence), nameof(ContractAwardDefence.LowestOffer));
            changes.Add(parentAward.LowestOffer.Value, award.LowestOffer.Value, typeof(ContractAwardDefence), nameof(ContractAwardDefence.LowestOffer));

            changes.Add(parentAward.HighestOffer.Value, award.HighestOffer.Value, typeof(ContractAwardDefence), nameof(ContractAwardDefence.HighestOffer));
            changes.Add(parentAward.HighestOffer.Value, award.HighestOffer.Value, typeof(ContractAwardDefence), nameof(ContractAwardDefence.HighestOffer));

            changes.Add(parentAward.AnnualOrMonthlyValue.Years, award.AnnualOrMonthlyValue.Years, typeof(TimeFrame), nameof(TimeFrame.Years), section: "V.4");
            changes.Add(parentAward.AnnualOrMonthlyValue.Months, award.AnnualOrMonthlyValue.Months, typeof(TimeFrame), nameof(TimeFrame.Months), section: "V.4");

            // V.5
            changes.Add(parentAward.LikelyToBeSubcontracted, award.LikelyToBeSubcontracted, typeof(ContractAwardDefence), nameof(ContractAwardDefence.LikelyToBeSubcontracted));
            changes.Add(parentAward.ValueOfSubcontract.Value, award.ValueOfSubcontract.Value, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ValueOfSubcontract));
            changes.Add(parentAward.ValueOfSubcontract.Currency, award.ValueOfSubcontract.Currency, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ValueOfSubcontract));
            changes.Add(parentAward.ValueOfSubcontractNotKnown, award.ValueOfSubcontractNotKnown, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ValueOfSubcontractNotKnown));
            changes.Add(parentAward.ProportionOfValue, award.ProportionOfValue, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ProportionOfValue));

            changes.Add(parentAward.SubcontractingDescription, award.SubcontractingDescription, typeof(ContractAwardDefence), nameof(ContractAwardDefence.SubcontractingDescription));

            changes.Add(parentAward.AllOrCertainSubcontractsWillBeAwarded, award.AllOrCertainSubcontractsWillBeAwarded, typeof(ContractAwardDefence), nameof(ContractAwardDefence.AllOrCertainSubcontractsWillBeAwarded));
            changes.Add(parentAward.ShareOfContractWillBeSubcontracted, award.ShareOfContractWillBeSubcontracted, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ShareOfContractWillBeSubcontracted));
            changes.Add(parentAward.ShareOfContractWillBeSubcontractedMinPercentage, award.ShareOfContractWillBeSubcontractedMinPercentage, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ShareOfContractWillBeSubcontractedMinPercentage));
            changes.Add(parentAward.ShareOfContractWillBeSubcontractedMaxPercentage, award.ShareOfContractWillBeSubcontractedMaxPercentage, typeof(ContractAwardDefence), nameof(ContractAwardDefence.ShareOfContractWillBeSubcontractedMaxPercentage));
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
            var euFundsSection = notice.Type == NoticeType.DefenceContract ? "VI.2" : "VI.1";
            changes.Add(parentComp.Defence.EuFunds.ProcurementRelatedToEuProgram, comp.Defence.EuFunds.ProcurementRelatedToEuProgram, typeof(EuFunds), nameof(EuFunds.ProcurementRelatedToEuProgram), section: euFundsSection);
            changes.Add(parentComp.Defence.EuFunds.ProjectIdentification, comp.Defence.EuFunds.ProjectIdentification, typeof(EuFunds), nameof(EuFunds.ProjectIdentification), section: euFundsSection);

            changes.Add(parentComp.IsRecurringProcurement, comp.IsRecurringProcurement, typeof(ComplementaryInformation), nameof(ComplementaryInformation.IsRecurringProcurement));
            changes.Add(parentComp.EstimatedTimingForFurtherNoticePublish, comp.EstimatedTimingForFurtherNoticePublish, typeof(ComplementaryInformation), nameof(ComplementaryInformation.EstimatedTimingForFurtherNoticePublish));

            // VI.2
            var additionalInfoSection = notice.Type == NoticeType.DefenceContract ? "VI.3" : "VI.2";
            changes.Add(parentComp.AdditionalInformation, comp.AdditionalInformation, typeof(ComplementaryInformation), nameof(ComplementaryInformation.AdditionalInformation), section: additionalInfoSection);

            // VI.3
            changes.Add(parentComp.Defence.TaxLegislationUrl, comp.Defence.TaxLegislationUrl, typeof(ComplementaryInformationDefence), nameof(ComplementaryInformationDefence.TaxLegislationUrl));
            changes.Add(parentComp.Defence.TaxLegislation.OfficialName, comp.Defence.TaxLegislation.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.NationalRegistrationNumber, comp.Defence.TaxLegislation.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.PostalAddress.StreetAddress, comp.Defence.TaxLegislation.PostalAddress.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.PostalAddress.Town, comp.Defence.TaxLegislation.PostalAddress.Town, typeof(PostalAddress), nameof(PostalAddress.Town), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.PostalAddress.PostalCode, comp.Defence.TaxLegislation.PostalAddress.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.PostalAddress.Country, comp.Defence.TaxLegislation.PostalAddress.Country, typeof(PostalAddress), nameof(PostalAddress.Country), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.ContactPerson, comp.Defence.TaxLegislation.ContactPerson, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.ContactPerson), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.TelephoneNumber, comp.Defence.TaxLegislation.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.Email, comp.Defence.TaxLegislation.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email), section: "Annex A, II)");
            changes.Add(parentComp.Defence.TaxLegislation.MainUrl, comp.Defence.TaxLegislation.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl), section: "Annex A, II)");

            changes.Add(parentComp.Defence.EnvironmentalProtectionUrl, comp.Defence.EnvironmentalProtectionUrl, typeof(ComplementaryInformationDefence), nameof(ComplementaryInformationDefence.EnvironmentalProtectionUrl));
            changes.Add(parentComp.Defence.EnvironmentalProtection.OfficialName, comp.Defence.EnvironmentalProtection.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.NationalRegistrationNumber, comp.Defence.EnvironmentalProtection.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.PostalAddress.StreetAddress, comp.Defence.EnvironmentalProtection.PostalAddress.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.PostalAddress.Town, comp.Defence.EnvironmentalProtection.PostalAddress.Town, typeof(PostalAddress), nameof(PostalAddress.Town), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.PostalAddress.PostalCode, comp.Defence.EnvironmentalProtection.PostalAddress.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.PostalAddress.Country, comp.Defence.EnvironmentalProtection.PostalAddress.Country, typeof(PostalAddress), nameof(PostalAddress.Country), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.ContactPerson, comp.Defence.EnvironmentalProtection.ContactPerson, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.ContactPerson), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.TelephoneNumber, comp.Defence.EnvironmentalProtection.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.Email, comp.Defence.EnvironmentalProtection.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email), section: "Annex A, III)");
            changes.Add(parentComp.Defence.EnvironmentalProtection.MainUrl, comp.Defence.EnvironmentalProtection.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl), section: "Annex A, III)");

            changes.Add(parentComp.Defence.EmploymentProtectionUrl, comp.Defence.EmploymentProtectionUrl, typeof(ComplementaryInformationDefence), nameof(ComplementaryInformationDefence.EmploymentProtectionUrl));
            changes.Add(parentComp.Defence.EmploymentProtection.OfficialName, comp.Defence.EmploymentProtection.OfficialName, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.OfficialName), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.NationalRegistrationNumber, comp.Defence.EmploymentProtection.NationalRegistrationNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.NationalRegistrationNumber), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.PostalAddress.StreetAddress, comp.Defence.EmploymentProtection.PostalAddress.StreetAddress, typeof(PostalAddress), nameof(PostalAddress.StreetAddress), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.PostalAddress.Town, comp.Defence.EmploymentProtection.PostalAddress.Town, typeof(PostalAddress), nameof(PostalAddress.Town), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.PostalAddress.PostalCode, comp.Defence.EmploymentProtection.PostalAddress.PostalCode, typeof(PostalAddress), nameof(PostalAddress.PostalCode), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.PostalAddress.Country, comp.Defence.EmploymentProtection.PostalAddress.Country, typeof(PostalAddress), nameof(PostalAddress.Country), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.ContactPerson, comp.Defence.EmploymentProtection.ContactPerson, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.ContactPerson), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.TelephoneNumber, comp.Defence.EmploymentProtection.TelephoneNumber, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.TelephoneNumber), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.Email, comp.Defence.EmploymentProtection.Email, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.Email), section: "Annex A, IV)");
            changes.Add(parentComp.Defence.EmploymentProtection.MainUrl, comp.Defence.EmploymentProtection.MainUrl, typeof(ContractBodyContactInformation), nameof(ContractBodyContactInformation.MainUrl), section: "Annex A, IV)");

            changes.Add(_parent.ProceduresForReview?.ReviewProcedure, _notice.ProceduresForReview?.ReviewProcedure, typeof(ProceduresForReviewInformation), nameof(ProceduresForReviewInformation.ReviewProcedure), section: "VI.4.2");
        }
        #endregion
    }
}
