using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract]
    public enum ContestType
    {
        Undefined = 0,
        /// <summary>
        /// Open
        /// </summary>
        Open = 1,
        /// <summary>
        /// Restricted procedure
        /// </summary>
        TypeRestricted = 2
    }
}
