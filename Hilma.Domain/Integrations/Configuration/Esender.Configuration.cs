
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Esender for Ted integration
    /// </summary>
    public class EsenderConfiguration
    {
        
        
        public bool Login {get; set;} = false;
        public bool CustomerLogin {get; set;} = false;
        public bool TedNoDocExt {get; set;} = false;
    }
}
