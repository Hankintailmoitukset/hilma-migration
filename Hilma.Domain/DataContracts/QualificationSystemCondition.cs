using Hilma.Domain.Attributes;
using Hilma.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hilma.Domain.DataContracts
{
    [Contract]
    public class QualificationSystemCondition
    {
        /// <summary>
        /// Conditions to be fulfilled by economic operators in view of their qualification
        /// </summary>
        [CorrigendumLabel("qs_conditions_qualify", "III.1.9")]
        [StringMaxLength(10000)]
        public string[] Conditions { get; set; }

        /// <summary>
        /// Methods according to which each of those conditions will be verified
        /// </summary>
        [CorrigendumLabel("qs_methods_verified", "III.1.9")]
        [StringMaxLength(10000)]
        public string[] Methods { get; set; }
    }
}
