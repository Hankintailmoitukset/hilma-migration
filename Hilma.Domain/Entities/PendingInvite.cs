using System;

namespace Hilma.Domain.Entities {
    public class PendingInvite : BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Email { get; set; }

        public Guid OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        public Guid HandlerId { get; set; }
        public User Handler { get; set; }

        public Guid? ApplicationId { get; set; }
        public OrganisationMembershipApplication Application { get; set; }
    }
}
