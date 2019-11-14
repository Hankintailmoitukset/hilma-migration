using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.Enums
{
    /// <summary>
    ///     Where the documents relating the procurement can be gotten from.
    /// </summary>
    [EnumContract]
    [Flags]
    public enum ProcurementDocumentAvailability : int
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     The procurement documents are available for unrestricted
        ///     and full direct access, free of charge
        /// </summary>
        [CorrigendumLabel("address_obtain_docs", "I.3")]
        AddressObtainDocs = 1 << 0,

        /// <summary>
        ///     Access to the procurement documents is restricted. 
        /// </summary>
        [CorrigendumLabel("docs_restricted", "I.3")]
        DocsRestricted = 1 << 1,

        /// <summary>
        ///     For national notices
        /// </summary>
        OrganisationAddress = 1 << 2,

        /// <summary>
        ///     For national notices
        /// </summary>
        OtherAddress = 1 << 3
    }
}
