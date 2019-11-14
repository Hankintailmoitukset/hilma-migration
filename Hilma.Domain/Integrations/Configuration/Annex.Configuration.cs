
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Annex for Ted integration
    /// </summary>
    public class AnnexConfiguration
    {
        
        
        public AnnexD1Configuration D1 {get; set;} = new AnnexD1Configuration();
        public AnnexD2Configuration D2 {get; set;} = new AnnexD2Configuration();
        public AnnexD3Configuration D3 {get; set;} = new AnnexD3Configuration();
        public AnnexD4Configuration D4 {get; set;} = new AnnexD4Configuration();
        public AnnexNationalConfiguration DirectNational {get; set;} = new AnnexNationalConfiguration();
    }
}
