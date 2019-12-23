using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts.EtsContracts
{
    /// <summary>
    ///  Information about procuring organisation.
    /// </summary>
    [Contract]
    public class EtsOrganisationContract
    {
        /// <summary>
        ///     Main form type information of the organisation.
        /// </summary>
        public ContractBodyContactInformation Information { get; set; }

        /// <summary>
        ///     Eu classification for the organisations legal basis.
        /// </summary>
        public ContractingAuthorityType ContractingAuthorityType { get; set; }
        /// <summary>
        /// Used in F24 and F25 to determine type of main activity:
        ///  (in the case of a notice published by a contracting authority)
        ///  or
        ///  (in the case of a notice published by a contracting entity)
        /// </summary>
        public ContractingType ContractingType { get; set; }
        /// <summary>
        ///     Eu classification for organisations primary domain of operation.
        /// </summary>
        public MainActivity MainActivity { get; set; }
        /// <summary>
        ///     Eu classification for organisations primary domain of operation for utilities.
        /// </summary>
        public MainActivityUtilities MainActivityUtilities { get; set; }
        /// <summary>
        ///     Free text explanation if "Other" is selected for <see cref="ContractingAuthorityType"/>
        /// </summary>
        public string OtherContractingAuthorityType { get; set; }
        /// <summary>
        ///     Free text explanation if "Other" is selected for <see cref="MainActivity"/> or <see cref="MainActivityUtilities"/>
        /// </summary>
        public string OtherMainActivity { get; set; }
    }
}
