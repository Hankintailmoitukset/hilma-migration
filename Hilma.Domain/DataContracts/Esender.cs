using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    /// 
    /// </summary>
    [Contract]
    public class Esender
    {
        /// <summary>
        /// 
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerLogin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [CorrigendumLabel("no_doc_ext", "V")]
        public string TedNoDocExt { get; set; }
    }
}
