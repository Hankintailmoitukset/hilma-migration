using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    /// 
    /// </summary>
    [Contract]
    public class FileEditorContract
    {
        /// <summary>
        ///     Service url.
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        ///     Container uri.
        /// </summary>
        public string Container { get; set; }
        /// <summary>
        ///     Shared access signature generated for this write operation.
        /// </summary>
        public string SasToken { get; set; }
        /// <summary>
        ///     Blob name assigned by Hilma
        /// </summary>
        public string BlobName { get; set; }
        /// <summary>
        ///     The filename given when the file write permission was requested, so front end can
        ///     decide which file to upload where.
        /// </summary>
        public string FileName { get; set; }
        public string PreviewUrl { get; set; }
    }
}
