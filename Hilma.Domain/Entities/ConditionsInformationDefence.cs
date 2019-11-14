using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Directive 2009/81/EC
    /// Section III: Legal, economic, financial and technical information
    /// </summary>
    [Contract]
    public class ConditionsInformationDefence
    {
        #region III.1) Conditions relating to the contract
        /// <summary>
        ///     III.1.1) List and brief description of conditions
        /// </summary>
        [CorrigendumLabel("deposits_required", "III.1.1")]
        public string[] DepositsRequired { get; set; }

        /// <summary>
        ///     III.1.2) Main financing conditions and payment arrangements and/or reference to the relevant provisions governing them
        /// </summary>
        [CorrigendumLabel("financing_conditions", "III.1.2")]
        public string[] FinancingConditions { get; set; }

        /// <summary>
        /// III.1.3) Legal form to be taken by the group of economic operators to whom the contract is to be awarded
        /// </summary>
        [CorrigendumLabel("legal_form_taken", "III.1.3")]
        public string[] LegalFormTaken { get; set; }

        /// <summary>
        /// III.1.4) Other particular conditions to which the performance of the contract is subject, in particular with regard to security of supply
        /// and security of information
        /// </summary>
        [CorrigendumLabel("other_particular_conditions", "III.1.4")]
        public string[] OtherParticularConditions { get; set; }

        /// <summary>
        /// III.1.5) Information about security clearance
        /// Candidates which do not yet hold security clearance may obtain such clearance until
        /// </summary>
        [CorrigendumLabel("security_clearance_until", "III.1.5")]
        public DateTime? SecurityClearanceDate { get; set; }
        #endregion

        #region III.2) Conditions for participation
        /// <summary>
        /// Criteria regarding the personal situation of economic operators
        /// (that may lead to their exclusion) including requirements relating to
        /// enrolment on professional or trade registers
        /// </summary>
        [CorrigendumLabel("personal_situation_criteria_of_economic", "III.2.1")]
        public string[] PersonalSituationOfEconomicOperators { get; set; }

        /// <summary>
        /// Criteria regarding the personal situation of subcontractors (that may
        /// lead to their rejection) including requirements relating to enrolment
        /// on professional or trade registers(if applicable)
        /// </summary>
        [CorrigendumLabel("personal_situation_criteria_of_sub", "III.2.1")]
        public string[] PersonalSituationOfSubcontractors { get; set; }

        /// <summary>
        /// Criteria regarding the economic and financial standing of economic
        /// operators(that may lead to their exclusion)
        /// </summary>
        [CorrigendumLabel("economic_criteria_of_economic_operators", "III.2.2")]
        public string[] EconomicCriteriaOfEconomicOperators { get; set; }

        /// <summary>
        /// Minimum level(s) of standards possibly required: (if applicable)
        /// </summary>
        [CorrigendumLabel("min_standards_required", "III.2.2")]
        public string[] EconomicCriteriaOfEconomicOperatorsMinimum { get; set; }

        /// <summary>
        /// Criteria regarding the economic and financial standing of
        /// subcontractors(that may lead to their rejection) (if applicable)
        /// </summary>
        [CorrigendumLabel("economic_criteria_of_subcontractors", "III.2.2")]
        public string[] EconomicCriteriaOfSubcontractors { get; set; }

        /// <summary>
        /// Minimum level(s) of standards possibly required: (if applicable)
        /// </summary>
        [CorrigendumLabel("min_standards_required", "III.2.2")]
        public string[] EconomicCriteriaOfSubcontractorsMinimum { get; set; }


        /// <summary>
        /// Criteria regarding the economic and financial standing of economic
        /// operators(that may lead to their exclusion)
        /// </summary>
        [CorrigendumLabel("technical_criteria_of_economic_operators", "III.2.3")]
        public string[] TechnicalCriteriaOfEconomicOperators { get; set; }

        /// <summary>
        /// Minimum level(s) of standards possibly required: (if applicable)
        /// </summary>
       [CorrigendumLabel("min_standards_required", "III.2.3")]
        public string[] TechnicalCriteriaOfEconomicOperatorsMinimum { get; set; }

        /// <summary>
        /// Criteria regarding the economic and financial standing of
        /// subcontractors(that may lead to their rejection) (if applicable)
        /// </summary>
        [CorrigendumLabel("technical_criteria_of_subcontractors", "III.2.3")]
        public string[] TechnicalCriteriaOfSubcontractors { get; set; }

        /// <summary>
        /// Minimum level(s) of standards possibly required: (if applicable)
        /// </summary>
        [CorrigendumLabel("min_standards_required", "III.2.3")]
        public string[] TechnicalCriteriaOfSubcontractorsMinimum { get; set; }

        /// <summary>
        /// III.2.4) Information about reserved contracts
        /// The contract is restricted to sheltered workshops
        /// </summary>
        public bool RestrictedToShelteredWorkshops { get; set; }

        /// <summary>
        /// III.2.4) Information about reserved contracts
        /// The execution of the contract is restricted to the framework of sheltered employment programmes
        /// </summary>
        public bool RestrictedToShelteredProgrammes { get; set; }
        #endregion

        #region III.3) Conditions specific to services contracts
        /// <summary>
        /// III.3.1) Information about a particular profession
        /// Execution of the service is reserved to a particular profession
        /// </summary>
        [CorrigendumLabel("particular_profession_reserved", "III.3.1")]
        public bool RestrictedToParticularProfession { get; set; }

        /// <summary>
        /// III.3.1) Information about a particular profession
        /// Reference to the relevant law, regulation or administrative provision
        /// </summary>
        [CorrigendumLabel("ref_law_reg_prov", "III.3.1")]
        public string[] RestrictedToParticularProfessionLaw { get; set; }

        /// <summary>
        /// III.3.2) Staff responsible for the execution of the service
        /// Legal persons should indicate the names and professional qualifications of the staff responsible for the execution of the service
        /// </summary>
        [CorrigendumLabel("legal_persons_to_indicate_qualifications", "III.3.2")]
        public bool StaffResponsibleForExecution { get; set; }

        #endregion

        #region VueJS
        /// <summary>
        ///     Validation state for Vuejs application.
        /// </summary>
        public ValidationState ValidationState { get; set; }
        #endregion
    }
}
