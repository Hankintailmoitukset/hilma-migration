using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {

    [EnumContract]
    public enum OrganisationMembershipApplicationType : int
    {
        Unknown = 0,
        Application = 1,
        Invite = 2
    }
}
