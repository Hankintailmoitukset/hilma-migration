using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Hilma related statistical information
    /// </summary>
    [Contract]
    public class HilmaStatistics
    {
        /// <summary>
        /// Tässä hankintamenettelyssä otetaan huomioon energiatehokkuusnäkökohtia
        /// </summary>
        public bool EnergyEfficiencyConsidered { get; set; }

        /// <summary>
        /// Tässä hankintamenettelyssä otetaan huomioon innovaationäkökohtia
        /// </summary>
        public bool InnovationConsidered { get; set; }

        /// <summary>
        /// Tässä hankintamenettelyssä otetaan huomioon pienten ja keskisuurten yritysten osallisumismahdollisuudet
        /// </summary>
        public bool SMEParticipationConsidered { get; set; }

        public ValidationState ValidationState { get; set; }
    }
}
