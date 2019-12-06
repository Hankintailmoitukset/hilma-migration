using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// A number or currency value represented as a single value or range
    /// </summary>
    //[Owned]
    [Contract]
    public class ValueRangeContract
    {
        /// <summary>
        /// Type of value (exact or range)
        /// </summary>
        public ContractValueType Type { get; set; }

        /// <summary>
        /// Exact value
        /// </summary>
        [CorrigendumLabel("value_excl_vat", "II.2.1")]
        public decimal? Value { get; set; }

        /// <summary>
        /// Min value
        /// </summary>
        [CorrigendumLabel("value_excl_vat", "II.2.1")]
        public decimal? MinValue { get; set; }

        /// <summary>
        /// Max value
        /// </summary>
        [CorrigendumLabel("value_excl_vat", "II.2.1")]
        public decimal? MaxValue { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        //[Required]
        [CorrigendumLabel("currency", "II.2.1")]
        public string Currency { get; set; } = "EUR";

        /// <summary>
        /// Whether the value can be published or not
        /// </summary>
        public bool? DisagreeToBePublished { get; set; }

        /// <summary>
        /// Required for national contracts by law :(
        /// </summary>
        [CorrigendumLabel("doesNotExceedNationalThreshold", "II.2.1")]
        public bool? DoesNotExceedNationalThreshold { get; set; }
    }
}
