using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract]
    public enum ContractingType : int
    {
        Undefined = 0,
        ContractingAuthority = 1,
        ContractingEntity = 2
    }
}
