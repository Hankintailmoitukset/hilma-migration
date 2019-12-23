
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Change for Ted integration
    /// </summary>
    public class ChangeConfiguration
    {
        
        
        public bool Section {get; set;} = false;
        public bool Label {get; set;} = false;
        public bool LotNumber {get; set;} = false;
        public bool OldText {get; set;} = false;
        public bool NewText {get; set;} = false;
        public CpvCodeConfiguration NewMainCpvCode {get; set;} = new CpvCodeConfiguration();
        public bool NewNutsCodes {get; set;} = false;
        public CpvCodeConfiguration NewAdditionalCpvCodes {get; set;} = new CpvCodeConfiguration();
        public CpvCodeConfiguration OldMainCpvCode {get; set;} = new CpvCodeConfiguration();
        public CpvCodeConfiguration OldAdditionalCpvCodes {get; set;} = new CpvCodeConfiguration();
        public bool OldNutsCodes {get; set;} = false;
        public bool NewDate {get; set;} = false;
        public bool OldDate {get; set;} = false;
    }
}
