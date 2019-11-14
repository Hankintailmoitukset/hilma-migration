using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.Enums
{
    /// <summary>
    ///     Defines where additional information about a notice can be gotten from.
    /// </summary>
    [EnumContract]
    [Flags]
    public enum AdditionalInformationAvailability
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     "Internet Address" given in "Contracting authority" section.
        /// </summary>
        [CorrigendumLabel("address_to_above", "I.3")]
        AddressToAbove = 1 << 0,

        /// <summary>
        ///     New address in a separate field specific to this information.
        /// </summary>
        [CorrigendumLabel("address_another", "I.3")]
        AddressAnother = 1 << 1
    }
}
