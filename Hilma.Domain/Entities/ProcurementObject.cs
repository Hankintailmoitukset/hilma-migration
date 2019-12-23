using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Validators;
using Innofactor.EfCoreJsonValueConverter;
using Microsoft.EntityFrameworkCore;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Section II: Object
    /// </summary>
    [Contract]
    public class ProcurementObject
    {
        /// <summary>
        /// II.1.4) Short description
        /// </summary>
        [Required]
        [CorrigendumLabel("descr_short", "II.1.4")]
        [StringMaxLength(4000)]
        public string[] ShortDescription { get; set; }

        /// <summary>
        /// II.1.5) Estimated total value
        /// </summary>
        [CorrigendumLabel("value_magnitude_estimated_total", "II.1.5")]
        public ValueRangeContract EstimatedValue { get; set; } = new ValueRangeContract();

        /// <summary>
        /// II.1.5.3 Method used for calculating the estimated value of the concession
        /// </summary>
        [CorrigendumLabel("concession_method_calculate", "II.1.5")]
        public string[] EstimatedValueCalculationMethod { get; set; }

        /// <summary>
        /// II.1.2) Main CPV code
        /// </summary>
        [Required]
        [JsonField]
        [CorrigendumLabel("cpv_main", "II.1.2")]
        public CpvCode MainCpvCode { get; set; }

        /// <summary>
        ///     Total value of the procurement
        /// </summary>
        [CorrigendumLabel("value_total", "II.1.7")]
        public ValueRangeContract TotalValue { get; set; }

        /// <summary>
        /// In case of defence contract (Directive 2009/81/EC), additional fields will be set here.
        /// </summary>
        public ProcurementObjectDefence Defence { get; set; }

        public ValidationState ValidationState { get; set; }
    }
}
