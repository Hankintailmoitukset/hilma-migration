
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ConditionsInformationDefence for Ted integration
    /// </summary>
    public class ConditionsInformationDefenceConfiguration
    {
        
        
        public bool DepositsRequired {get; set;} = false;
        public bool FinancingConditions {get; set;} = false;
        public bool LegalFormTaken {get; set;} = false;
        public bool OtherParticularConditions {get; set;} = false;
        public bool SecurityClearanceDate {get; set;} = false;
        public bool PersonalSituationOfEconomicOperators {get; set;} = false;
        public bool PersonalSituationOfSubcontractors {get; set;} = false;
        public bool EconomicCriteriaOfEconomicOperators {get; set;} = false;
        public bool EconomicCriteriaOfEconomicOperatorsMinimum {get; set;} = false;
        public bool EconomicCriteriaOfSubcontractors {get; set;} = false;
        public bool EconomicCriteriaOfSubcontractorsMinimum {get; set;} = false;
        public bool TechnicalCriteriaOfEconomicOperators {get; set;} = false;
        public bool TechnicalCriteriaOfEconomicOperatorsMinimum {get; set;} = false;
        public bool TechnicalCriteriaOfSubcontractors {get; set;} = false;
        public bool TechnicalCriteriaOfSubcontractorsMinimum {get; set;} = false;
        public bool RestrictedToShelteredWorkshops {get; set;} = false;
        public bool RestrictedToShelteredProgrammes {get; set;} = false;
        public bool RestrictedToParticularProfession {get; set;} = false;
        public bool RestrictedToParticularProfessionLaw {get; set;} = false;
        public bool StaffResponsibleForExecution {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
