using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    ///     State of a tracked file.
    /// </summary>
    [Flags]
    [EnumContract]
    public enum FileStatus : int
    {
        /// <summary>
        ///     Default value, error state
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     User has requested file upload permission
        /// </summary>
        Staged = 1 << 0, 
        /// <summary>
        ///     File is published.
        /// </summary>
        Published = 1 << 1,
        /// <summary>
        ///     File is removed by the user.
        /// </summary>
        Removed = 1 << 2,
        /// <summary>
        ///     File has been virus checked.
        /// </summary>
        Scanned = 1 << 3,
        /// <summary>
        ///     An error has occurred for this file.
        /// </summary>
        Error = 1 << 4
    }
}
