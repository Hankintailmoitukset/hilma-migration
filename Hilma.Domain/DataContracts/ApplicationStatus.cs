using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// For setting application status to organisation
    /// </summary>
    [Contract]
    public class ApplicationStatus
    {
        /// <summary>
        /// The status
        /// </summary>
        public OrganisationMembershipApplicationStatus status { get; set; }
    }
}
