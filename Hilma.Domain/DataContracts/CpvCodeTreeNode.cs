using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts {
    [Contract]
    public class CpvCodeTreeNode
    {
        public CpvCodeTreeNode() { }

        public string Id { get; set; }
        public string Label { get; set; }
        public CpvCodeTreeNode[] Children { get; set; }
    }
}
