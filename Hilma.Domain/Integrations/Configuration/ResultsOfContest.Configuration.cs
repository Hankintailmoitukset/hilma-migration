
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ResultsOfContest for Ted integration
    /// </summary>
    public class ResultsOfContestConfiguration
    {
        
        
        public bool ContestWasTerminated {get; set;} = false;
        public bool NoPrizeType {get; set;} = false;
        public bool OriginalNoticeSentVia {get; set;} = false;
        public EsenderConfiguration OriginalEsender {get; set;} = new EsenderConfiguration();
        public bool OriginalNoticeSentViaOther {get; set;} = false;
        public bool OriginalNoticeSentDate {get; set;} = false;
        public bool DateOfJuryDecision {get; set;} = false;
        public bool ParticipantsContemplated {get; set;} = false;
        public bool ParticipantsSme {get; set;} = false;
        public bool ParticipantsForeign {get; set;} = false;
        public bool DisagreeParticipantCountPublish {get; set;} = false;
        public ContractorContactInformationConfiguration Winners {get; set;} = new ContractorContactInformationConfiguration();
        public bool DisagreeWinnersPublish {get; set;} = false;
        public ValueContractConfiguration ValueOfPrize {get; set;} = new ValueContractConfiguration();
        public bool DisagreeValuePublish {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
