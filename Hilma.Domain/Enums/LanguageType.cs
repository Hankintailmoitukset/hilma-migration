using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.Enums
{
    /// <summary>
    /// Directive 2009/81/EC (Defence contracts)
    /// </summary>
    [EnumContract]
    [Flags]
    public enum LanguageType : int
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     Any official EU language.
        /// </summary>
        AnyOfficialEu = 1 << 0,
        /// <summary>
        ///     Any official EU language.
        /// </summary>
        SelectedEu = 1 << 1
    }
}
