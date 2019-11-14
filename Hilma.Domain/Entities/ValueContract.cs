using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [Contract]
    public class ValueContract
    {
        [CorrigendumLabel("value_excl_vat", "II.1.5")]
        public decimal? Value { get; set; }

        [Required]
        [CorrigendumLabel("currency", "II.1.5")]
        public string Currency { get; set; } = "EUR";
    }
}
