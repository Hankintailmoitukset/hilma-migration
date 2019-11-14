using System.Collections.Generic;
using AutoMapper.Attributes;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes members of on organisation.
    /// </summary>
    [MapsFrom(typeof(Organisation))]
    [Contract]
    public class MembershipsContract
    {
        /// <summary>
        ///     Approved members of the organisation.
        /// </summary>
        public List<OrganisationContract> Member { get; set; }
        /// <summary>
        ///     Users with a pending membership request for the organisation.
        /// </summary>
        public List<OrganisationContract> Pending { get; set; }
    }
}
