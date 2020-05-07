using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    [Contract]
    public partial class CpvCodeMetadata
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsSocialService { get; set; }
        public bool HasSocialServiceChild { get; set; }
        public bool IsNatSocial { get; set; }
        public bool IsNatOther { get; set; }
    }
}
