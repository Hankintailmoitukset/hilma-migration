using System;

namespace Hilma.Domain.Entities
{
    public class OrganisationUser
    {
        public Guid OrganisationId { get; set; }
        public Organisation Organisation { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }  
    }
}
