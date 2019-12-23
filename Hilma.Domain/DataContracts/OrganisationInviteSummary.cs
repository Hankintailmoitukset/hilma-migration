using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;

namespace Hilma.Domain.DataContracts {
    [Contract]
    public class OrganisationInviteSummary
    {
        public Guid Id { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string InviterName { get; set; }
        public string InviterEmail { get; set; }

        public OrganisationInviteSummary()
        {
        }

        public OrganisationInviteSummary(OrganisationMembershipApplication dbo)
        {
            Id = dbo.Id;
            OrganisationName = dbo.Organisation.Information.OfficialName;
            OrganisationIdentifier = dbo.Organisation.Information.NationalRegistrationNumber;
            InviterName = dbo.Handler.Name;
            InviterEmail = dbo.Handler.ContactEmail;
        }
    }
}
