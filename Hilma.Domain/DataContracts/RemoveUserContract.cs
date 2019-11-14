using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     User removal API parameters.
    /// </summary>
    [Contract]
    public class RemoveUserContract
    {
        /// <summary>
        ///     If the user should also be blocked.
        /// </summary>
        public bool BlockUser { get; set; }
    }
}
