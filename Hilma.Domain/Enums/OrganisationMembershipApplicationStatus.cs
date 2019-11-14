using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract]
    [Flags]
    public enum OrganisationMembershipApplicationStatus : int
    {
        Unknown = 0,
        Pending = 1,
        Rejected = 2,
        Blocked = 4,
        Approved = 8
    }
}
