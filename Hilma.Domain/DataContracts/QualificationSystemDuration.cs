using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// II.2.8) Duration of the Qualification system
    /// </summary>
    [Contract]
    public class QualificationSystemDuration
    {
        /// <summary>
        /// Type of the duration: start - end/indefinite
        /// </summary>
        [CorrigendumLabel("qs_duration", "II.2.8")]
        public QualificationSystemDurationType Type { get; set; }

        /// <summary>
        /// Start date of the qualification system
        /// </summary>
        [CorrigendumLabel("begin", "II.2.8")]
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// End date of the qualification system
        /// </summary>
        [CorrigendumLabel("end", "II.2.8")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Renewal of the qualification system
        /// </summary>
        [CorrigendumLabel("qs_renewal", "II.2.8")]
        public bool Renewal { get; set; }

        /// <summary>
        /// Formalities necessary for evaluating if requirements are met
        /// </summary>
        [CorrigendumLabel("qs_formalities_renewal", "II.2.8")]
        //[StringMaxLength(1000)]
        public string[] NecessaryFormalities { get; set; }
    }
}
