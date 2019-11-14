using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities.Annexes {
    [Contract]
    public class AnnexNational : IJustifiable
    {
        /// <summary>
        ///     Type of excuse for direct purchase
        /// </summary>
        public NationalDirectPurchaseType PurchaseType { get; set; }

        /// <summary>
        ///     Justification for direct purchase
        /// </summary>
        public string[] Justification { get; set; }
    }
}
