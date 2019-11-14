using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    public class OrganisationMembershipApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public OrganisationMembershipApplicationStatus ApplicationStatus { get; set; }

        public Guid OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        public Guid ApplicantId { get; set; }
        public User Applicant { get; set; }

        public Guid? HandlerId { get; set; }
        public User Handler { get; set; }

        public string ApproveReply { get; set; }
        public string RejectReply { get; set; }
        public string BlockReply { get; set; }
    }
}
