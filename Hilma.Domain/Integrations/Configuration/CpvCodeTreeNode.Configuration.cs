
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of CpvCodeTreeNode for Ted integration
    /// </summary>
    public class CpvCodeTreeNodeConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public bool Label {get; set;} = false;
        public CpvCodeTreeNodeConfiguration Children {get; set;} = new CpvCodeTreeNodeConfiguration();
    }
}
