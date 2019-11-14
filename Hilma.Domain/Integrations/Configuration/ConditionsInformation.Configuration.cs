
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ConditionsInformation for Ted integration
    /// </summary>
    public class ConditionsInformationConfiguration
    {
        
        
        public bool ProfessionalSuitabilityRequirements {get; set;} = false;
        public bool EconomicCriteriaToParticipate {get; set;} = false;
        public bool EconomicCriteriaDescription {get; set;} = false;
        public bool EconomicRequiredStandards {get; set;} = false;
        public bool TechnicalCriteriaToParticipate {get; set;} = false;
        public bool TechnicalCriteriaDescription {get; set;} = false;
        public bool TechnicalRequiredStandards {get; set;} = false;
        public bool RulesForParticipation {get; set;} = false;
        public bool RestrictedToShelteredWorkshop {get; set;} = false;
        public bool RestrictedToShelteredProgram {get; set;} = false;
        public bool ReservedOrganisationServiceMission {get; set;} = false;
        public bool DepositsRequired {get; set;} = false;
        public bool FinancingConditions {get; set;} = false;
        public bool LegalFormTaken {get; set;} = false;
        public bool CiriteriaForTheSelectionOfParticipants {get; set;} = false;
        public bool ExecutionOfServiceIsReservedForProfession {get; set;} = false;
        public bool ReferenceToRelevantLawRegulationOrProvision {get; set;} = false;
        public bool ParticipationIsReservedForProfession {get; set;} = false;
        public bool IndicateProfession {get; set;} = false;
        public bool ContractPerformanceConditions {get; set;} = false;
        public bool ObligationToIndicateNamesAndProfessionalQualifications {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
