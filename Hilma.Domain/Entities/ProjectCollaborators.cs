using System;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Project collaborators
    /// </summary>
    public class ProjectCollaborators
    {
        /// <summary>
        /// Navigational property for project
        /// </summary>
        public ProcurementProject Project { get; set; }

        /// <summary>
        /// Project fk.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Navigational prop for user
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// User fk.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
