using Hilma.Domain.Attributes;
using Hilma.Domain.Entities.Annexes;

namespace Hilma.Domain.Entities {
    /// <summary>
    ///     Container for annexes of a notice.
    /// </summary>
    [Contract]
    public class Annex
    {
        public AnnexD1 D1 { get; set; }
        public AnnexD2 D2 { get; set; }
        public AnnexD3 D3 { get; set; }
        public AnnexD4 D4 { get; set; }
        public AnnexNational DirectNational { get; set; }
    }
}
