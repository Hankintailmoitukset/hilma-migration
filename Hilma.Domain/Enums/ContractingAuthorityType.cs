using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.Enums
{
    [EnumContract][Flags]
    public enum ContractingAuthorityType : int
    {
        Undefined = 0,
        MaintypeMinistry = 1 << 0,
        MaintypeNatagency = 1 << 1,
        MaintypeLocalauth = 1 << 2,
        MaintypeLocalagency = 1 << 3,
        MaintypePublicbody = 1 << 4,
        MaintypeEu = 1 << 5,
        OtherType = 1 << 6,
        MaintypeChurch = 1 << 7,
        MaintypeFarmer = 1 << 8
    }
}
