using System;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities {
    /// <summary>
    ///     Stores meta-data related to a file uploaded to hilma.
    /// </summary>
    public class File : BaseEntity
    {
        /// <summary>
        ///     Hilma assigned identifier for this file.
        /// </summary>
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        /// <summary>
        ///     Blob container of this file.
        /// </summary>
        public string Container { get; set; }
        /// <summary>
        ///     Stored filename of this file.
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        ///     Status of a file.
        /// </summary>
        public FileStatus Status { get; set; }
        /// <summary>
        ///     Public url with permanent SAS token, if published.
        /// </summary>
        public string PublicUrl { get; set; }
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public File() { }
        /// <summary>
        ///     Constructor with settings the container and optionally desired filename.
        /// </summary>
        /// <param name="container">Container into which to create the file.</param>
        /// <param name="filename">
        ///     Displayed file name. Actual file name in the blob storage is based on
        ///     the database assigned Id. Optional.
        /// </param>
        public File(string container, string filename = null)
        {
            Container = container;
            Filename = filename;
            Status = FileStatus.Staged;
        }
    }
}
