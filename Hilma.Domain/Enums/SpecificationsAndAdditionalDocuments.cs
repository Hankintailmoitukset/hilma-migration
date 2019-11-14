using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.Enums
{
    /// <summary>
    ///     Directive 2009/81/EC
    ///     I.1) Name, addresses and contact point(s)
    ///     Specifications and additional documents can be obtained from 
    /// </summary>
    [EnumContract]
    [Flags]
    public enum SpecificationsAndAdditionalDocuments
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     The above mentioned contact point(s)
        /// </summary>
        [CorrigendumLabel("address_to_above", "I.3")]
        AddressToAbove = 1 << 0,

        /// <summary>
        ///     Other (please complete Annex A.II)
        /// </summary>
        [CorrigendumLabel("address_another", "I.3")]
        AddressAnother = 1 << 1
    }
}
