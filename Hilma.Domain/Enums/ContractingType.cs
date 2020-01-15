using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract]
    public enum ContractingType : int
    {
        Undefined = 0,
        /// <summary>
        /// In case of contracting authority
        /// </summary>
        ContractingAuthority = 1,

        /// <summary>
        /// In case of contracting entity
        /// </summary>
        ContractingEntity = 2
    }
}
