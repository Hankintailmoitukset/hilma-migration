using System;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Base class for EF entities. When inheriting form this base class,
    ///     entity will automatically be timestamped when created or updated.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        ///     Creation time of the entity.
        /// </summary>
        public DateTime? DateCreated { get; set; }
        /// <summary>
        ///     Latest update time of the entity.
        /// </summary>
        public DateTime? DateModified { get; set; }
    }
}
