using AutoMapper.Attributes;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;

namespace Hilma.Domain.DataContracts
{
    [MapsFrom(typeof(User))]
    [Contract]
    public class CollaboratorContract
    {
        /// <summary>
        ///     User inputted name from the AADB2C policy.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     User inputted email (login) from AADB2C policy
        /// </summary>
        public string ContactEmail { get; set; }
    }
}
