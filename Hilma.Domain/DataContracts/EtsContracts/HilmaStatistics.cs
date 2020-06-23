using System;
using System.Collections.Generic;
using System.Text;

namespace Hilma.Domain.DataContracts.EtsContracts
{
    public class HilmaStatistics
    {
        /// <summary>
        /// The procurement takes energy efficiency into consideration
        /// </summary>
        public bool EnergyEfficiencyConsidered { get; set; }

        /// <summary>
        /// The procurement takes innovations into consideration
        /// </summary>
        public bool InnovationConsidered { get; set; }

        /// <summary>
        /// The procurement is inclusive to SMEs.
        /// </summary>
        public bool SMEParticipationConsidered { get; set; }
       
    }
}
