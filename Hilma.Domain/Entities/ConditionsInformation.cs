using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Validators;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Section III: Legal, economic, financial and technical information
    /// </summary>
    [Contract]
    public class ConditionsInformation
    {
        #region III.1.1 Suitability to pursue the professional activity, including requirements relating to enrolment on professional or trade registers
        /// <summary>
        ///     III.1.1) List and brief description of conditions
        /// </summary>
        [CorrigendumLabel("info_evaluating_requir", "III.1.1")]
        [StringMaxLength(10000)]
        public string[] ProfessionalSuitabilityRequirements { get; set; }
        #endregion

        #region III.1.2 Economic and financial standing
        /// <summary>
        ///     III.1.2) Selection criteria as stated in the procurement documents
        /// </summary>
        [CorrigendumLabel("economic_criteria_doc", "III.1.2")]
        public bool EconomicCriteriaToParticipate { get; set; }

        /// <summary>
        ///     III.1.2) List and brief description of selection criteria
        /// </summary>
        [CorrigendumLabel("info_evaluating_weth_requir", "III.1.2")]
        [StringMaxLength(10000)]
        public string[] EconomicCriteriaDescription { get; set; }

        /// <summary>
        ///     III.1.2) Minimum level(s) of standards possibly required
        /// </summary>
        [CorrigendumLabel("min_standards_required", "III.1.2")]
        [StringMaxLength(10000)]
        public string[] EconomicRequiredStandards { get; set; }
        #endregion

        #region III.1.3 Technical requirements to participate
        /// <summary>
        ///     III.1.3) Selection criteria as stated in the procurement documents
        /// </summary>
        [CorrigendumLabel("criteria_selection_docs", "III.1.3")]
        public bool TechnicalCriteriaToParticipate { get; set; }

        /// <summary>
        ///     III.1.3) List and brief description of selection criteria
        ///     participate.
        /// </summary>
        [CorrigendumLabel("info_evaluating_weth_requir", "III.1.3")]
        [StringMaxLength(10000)]
        public string[] TechnicalCriteriaDescription { get; set; }

        /// <summary>
        ///     III.1.3) Minimum level(s) of standards possibly required
        /// </summary>
        [CorrigendumLabel("min_standards_required", "III.1.3")]
        [StringMaxLength(10000)]
        public string[] TechnicalRequiredStandards { get; set; }
        #endregion

        #region III.1.4 Objective rules and criteria for participation
        /// <summary>
        ///     III.1.4) List and brief description of rules and criteria
        /// </summary>
        [MaxLength(1000)]
        [CorrigendumLabel("descr_brief_rules", "III.1.4")]
        [StringMaxLength(10000)]
        public string[] RulesForParticipation { get; set; }
        #endregion

        #region III.1.5 Reserved contracts information
        /// <summary>
        ///     III.1.5) Contract is reserved for workshops for disadvantaged persons.
        /// </summary>
        [CorrigendumLabel("restricted_sheltered_workshop", "III.1.5")]
        public bool RestrictedToShelteredWorkshop { get; set; }

        /// <summary>
        ///     III.1.5) Contract is restricted to framework of sheltered employment programs.
        /// </summary>
        [CorrigendumLabel("restricted_sheltered_program", "III.1.5")]
        public bool RestrictedToShelteredProgram { get; set; }

        /// <summary>
        ///     III.1.5) Participation in the procedure is reserved to organisations pursuing
        ///     a public service mission and fulfilling the conditions set in Article 94(2)
        ///     of Directive 2014/25/EU
        /// </summary>
        [CorrigendumLabel("reserved_public_mission", "III.1.5")]
        public bool ReservedOrganisationServiceMission { get; set; }
        #endregion

        #region III.1.6 Deposits and guarantees required
        /// <summary>
        ///     III.1.6) Deposits and guarantees required
        /// </summary>
        [CorrigendumLabel("deposits_required", "III.1.6")]
        [StringMaxLength(2000)]
        public string[] DepositsRequired { get; set; }
        #endregion

        #region III.1.7 Main financing conditions and payment arrangements and/or reference to the relevant provisions governing them
        /// <summary>
        ///     III.1.7) Main financing conditions and payment arrangements and/or reference to the relevant provisions governing them
        /// </summary>
        [CorrigendumLabel("financing_conditions", "III.1.7")]
        [StringMaxLength(2000)]
        public string[] FinancingConditions { get; set; }
        #endregion

        #region III.1.8 Legal form to be taken by the group of economic operators to whom the contract is to be awarded
        /// <summary>
        ///     III.1.8) Legal form to be taken by the group of economic operators to whom the contract is to be awarded
        /// </summary>
        [CorrigendumLabel("legal_form_taken", "III.1.8")]
        [StringMaxLength(1500)]
        public string[] LegalFormTaken { get; set; }
        #endregion

        /// <summary>
        /// III.1.9) Qualification for the system (summary of the main conditions and methods)
        /// </summary>
        public QualificationSystemCondition[] QualificationSystemConditions { get; set; }

        #region III.1.10) Criteria for the selection of participants: 2 (in the case of a restricted contest)
        /// <summary>
        /// III.1.10) Criteria for the selection of participants: 2 (in the case of a restricted contest)
        /// </summary>
        [CorrigendumLabel("criteria_participants", "III.1.10")]
        public string[] CiriteriaForTheSelectionOfParticipants { get; set; }
        #endregion

        #region III.2 Conditions for contract
        /// <summary>
        ///     III.2.1) Execution of the service is reserved to a particular profession 
        /// </summary>
        [CorrigendumLabel("particular_profession_reserved", "III.2.1")]
        public bool ExecutionOfServiceIsReservedForProfession { get; set; }

        /// <summary>
        ///     III.2.1) Information about a particular profession
        /// </summary>
        [CorrigendumLabel("ref_law_reg_prov", "III.2.1")]
        [StringMaxLength(2000)]
        public string[] ReferenceToRelevantLawRegulationOrProvision { get; set; }

        /// <summary>
        ///     III.2.1) Participation is reserved to a particular profession
        /// </summary>
        [CorrigendumLabel("particular_prof_reserved_particip", "III.2.1")]
        public bool ParticipationIsReservedForProfession { get; set; }

        /// <summary>
        /// III.2.1) Information about a particular profession
        /// Indicate profession
        /// </summary>
        [CorrigendumLabel("dc_indicate_profession", "III.2.1")]
        public string[] IndicateProfession { get; set; }

        /// <summary>
        ///     III.2.2) Contract performance conditions
        /// </summary>
        [CorrigendumLabel("other_conditions", "III.2.2")]
        [StringMaxLength(4000)]
        public string[] ContractPerformanceConditions { get; set; }

        /// <summary>
        ///     III.2.3) Information about staff responsible for the performance of the contract
        ///     Obligation to indicate the names and professional qualifications of the staff assigned to performing the contract
        /// </summary>
        [CorrigendumLabel("staff_responsible_indicate", "III.2.3")]
        public bool ObligationToIndicateNamesAndProfessionalQualifications { get; set; }
        #endregion

        #region VueJS
        /// <summary>
        ///     Validation state for Vuejs application.
        /// </summary>
        public ValidationState ValidationState { get; set; }
        #endregion
    }
}
