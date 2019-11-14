using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [EnumContract]
    public enum FrameworkAgreementType
    {
        Undefined = 0,
        FrameworkSingle = 1,
        FrameworkSeveral = 2
    }
}
