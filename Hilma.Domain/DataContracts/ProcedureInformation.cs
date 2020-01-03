using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Section IV: Procedure
    /// </summary>
    [Contract]
    public class ProcedureInformation
    {
        /// <summary>
        /// IV.1.1) Type of procedure
        /// Type of procedure 
        /// </summary>
        [CorrigendumLabel("type_procedure", "IV.1.1")]
        public ProcedureType ProcedureType { get; set; }

        /// <summary>
        /// IV.1.1) Type of procedure
        ///  Accelerated procedure 
        /// </summary>
        [CorrigendumLabel("proctype_accelerated", "IV.1.1")]
        public bool AcceleratedProcedure { get; set; }

        /// <summary>
        /// IV.1.1) Type of procedure
        /// Justification for accelerated procedure
        /// </summary>
        [CorrigendumLabel("justification", "IV.1.1")]
        //[StringMaxLength(1000)]
        public string[] JustificationForAcceleratedProcedure { get; set; }

        /// <summary>
        /// IV.1.2) Type of contest
        /// </summary>
        [CorrigendumLabel("type_contest", "IV.1.2")]
        public ContestType ContestType { get; set; }

        /// <summary>
        /// IV.1.2) Type of contest
        /// Number of participants to be contemplated: [ ]
        /// or Minimum number: [ ] / Maximum number: [ ]
        /// </summary>
        [CorrigendumLabel("number_participants", "IV.1.2")]
        public ValueRangeContract ContestParticipants { get; set; }

        /// <summary>
        /// IV.1.3) Information about a framework agreement or a dynamic purchasing system
        /// Information about a framework agreement or a dynamic purchasing systemInformation about 
        /// </summary>
        public FrameworkAgreementInformation FrameworkAgreement { get; set; }

        /// <summary>
        /// IV.1.4) Information about reduction of the number of solutions or tenders during negotiation or dialogue
        /// Recourse to staged procedure to gradually reduce the number of solutions to be discussed or tenders to be negotiated
        /// </summary>
        [CorrigendumLabel("reduction_during_dialogue", "IV.1.4")]
        public bool ReductionRecourseToReduceNumberOfSolutions { get; set; }

        /// <summary>
        /// IV.1.5) Information about negotiation
        /// The contracting authority reserves the right to award the contract on the basis of the initial tenders without conducting negotiations
        /// </summary>
        [CorrigendumLabel("negotiation_without", "IV.1.5")]
        public bool ReserveRightToAwardWithoutNegotiations { get; set; }

        /// <summary>
        /// IV.1.6) Information about electronic auction
        /// An electronic auction will be used
        /// </summary>
        [CorrigendumLabel("eauction_will_used", "IV.1.6")]
        public bool ElectronicAuctionWillBeUsed { get; set; }

        /// <summary>
        /// IV.1.6) Information about electronic auction
        /// Additional information about electronic auction
        /// </summary>
        [CorrigendumLabel("eauction_info_addit", "IV.1.6")]
        //[StringMaxLength(1000)]
        public string[] AdditionalInformationAboutElectronicAuction { get; set; }

        /// <summary>
        /// IV.1.7) Names of participants already selected: 1 (in the case of a restricted contest)
        /// </summary>
        [CorrigendumLabel("dc_names_selected", "IV.1.7")]
        public string[] NamesOfParticipantsAlreadySelected { get; set; }

        /// <summary>
        /// IV.1.8) Information about the Government Procurement Agreement (GPA)
        /// </summary>
        [CorrigendumLabel("gpa_covered", "IV.1.8")]
        public bool ProcurementGovernedByGPA { get; set; }

        /// <summary>
        /// IV.1.9) Criteria for the evaluation of projects
        /// </summary>
        [CorrigendumLabel("dc_criteria_evaluation", "IV.1.9")]
        public string[] CriteriaForEvaluationOfProjects { get; set; }

        /// <summary>
        /// Disagree to publish whatever is in CriteriaForEvaluationOfProjects
        /// </summary>
        [CorrigendumLabel("H_agree_to_publish", "IV.1.9")]
        public bool DisagreeCriteriaForEvaluationOfProjectsPublish { get; set; }

        /// <summary>
        /// IV.1.10 Identification of the national rules applicable to the procedure
        /// </summary>
        [CorrigendumLabel("national_rules", "IV.1.10")]
        public string UrlNationalProcedure { get; set; }

        /// <summary>
        /// IV.1.11 Main features of the award procedure
        /// </summary>
        [CorrigendumLabel("award_main_features", "IV.1.11")]
        public string[] MainFeaturesAward { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence contracts)
        /// Additional fields related to defence notices
        /// </summary>
        public ProcedureInformationDefence Defence { get; set; }

        /// <summary>
        /// Additional information for national notices.
        /// </summary>
        public ProcedureInformationNational National { get; set; }

        public ValidationState ValidationState { get; set; }
    }
}
