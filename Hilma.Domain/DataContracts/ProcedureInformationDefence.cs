using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Directive 2009/81/EC
    /// Section IV: Procedure
    /// </summary>
    [Contract]
    public class ProcedureInformationDefence
    {
        /// <summary>
        /// IV.1.2) Limitations on the number of operators who will be invited to tender or to participate
        /// </summary>
        public CandidateNumberRestrictions CandidateNumberRestrictions { get; set; } = new CandidateNumberRestrictions();

        /// <summary>
        /// IV.2.1) Award criteria
        /// </summary>
        public AwardCriteriaDefence AwardCriteria { get; set; } = new AwardCriteriaDefence();
    }
}
