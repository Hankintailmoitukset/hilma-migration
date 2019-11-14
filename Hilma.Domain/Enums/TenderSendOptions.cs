using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.Enums
{
    /// <summary>
    ///     Defines options for where to send tenders to.
    /// </summary>
    [EnumContract]
    [Flags]
    public enum TenderSendOptions : int
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     Send tenders to a electronic address defined separately.
        /// </summary>
        [CorrigendumLabel("address_send_tenders", "I.3")]
        AddressSendTenders = 1 << 0,

        /// <summary>
        ///     Send tenders to a physical address given for the organisation earlier.
        /// </summary>
        [CorrigendumLabel("address_to_above", "I.3")]
        AddressOrganisation = 1 << 1,

        /// <summary>
        ///     Send tenders to physical address defined separately.
        /// </summary>
        [CorrigendumLabel("address_following", "I.3")]
        AddressFollowing = 1 << 2,

        /// <summary>
        /// Send tenders to given email address. Only for national notices
        /// </summary>
        EmailSendTenders = 1 << 3,
    }
}
