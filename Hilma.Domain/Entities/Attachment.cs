using System;

namespace Hilma.Domain.Entities {
    /// <summary>
    ///     Stores relation of file to notice.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        ///     File identifier FK for the attachment.
        /// </summary>
        public Guid FileId { get; set; }
        /// <summary>
        ///     File navigational property to the attached file.
        /// </summary>
        public File File { get; set; }
        /// <summary>
        ///     Notice navigational property to the attaching notice.
        /// </summary>
        public Notice Notice { get; set; }
        /// <summary>
        ///     Notice FK for the attaching notice.
        /// </summary>
        public int NoticeId { get; set; }
        /// <summary>
        ///     This attachment is a copy, the indicated notice does not
        ///     control delete/edit permissions to the indicated file.
        /// </summary>
        public bool IsCopy { get; set; }
        /// <summary>
        ///     Priority of the notice. Lowest is displayed first. 
        /// </summary>
        public int Order { get; set; } // TODO(JanneF): Not actually implemented. Added here while was adding other fields to avoid migration in future.
    }
}
