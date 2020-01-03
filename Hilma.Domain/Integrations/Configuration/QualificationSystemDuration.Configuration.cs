
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of QualificationSystemDuration for Ted integration
    /// </summary>
    public class QualificationSystemDurationConfiguration
    {
        
        
        public bool Type {get; set;} = false;
        public bool BeginDate {get; set;} = false;
        public bool EndDate {get; set;} = false;
        public bool Renewal {get; set;} = false;
        public bool NecessaryFormalities {get; set;} = false;
    }
}
