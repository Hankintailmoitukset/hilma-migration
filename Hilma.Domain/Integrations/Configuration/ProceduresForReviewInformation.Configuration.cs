
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ProceduresForReviewInformation for Ted integration
    /// </summary>
    public class ProceduresForReviewInformationConfiguration
    {
        
        
        public ContractBodyContactInformationConfiguration ReviewBody {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool ReviewProcedure {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
