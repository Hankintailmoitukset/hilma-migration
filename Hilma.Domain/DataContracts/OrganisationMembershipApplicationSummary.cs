using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts {
    [Contract]
    public class OrganisationMembershipApplicationSummary
    {
        public Guid Id { get; set; }
        public OrganisationMembershipApplicationType ApplicationType { get; set; }
        public OrganisationMembershipApplicationStatus ApplicationStatus { get; set; }
        public string ApplicantContactEmail { get; set; }
        public string ApplicantName { get; set; }

        public OrganisationMembershipApplicationSummary(OrganisationMembershipApplication dbo)
        {
            Id = dbo.Id;
            ApplicationType = dbo.ApplicationType;
            ApplicationStatus = dbo.ApplicationStatus;
            ApplicantContactEmail = dbo.Applicant.ContactEmail;
            ApplicantName = dbo.Applicant.Name;
        }
    }
}
