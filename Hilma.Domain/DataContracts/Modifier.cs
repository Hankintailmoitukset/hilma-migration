using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Modification information. For Hilma use only
    /// </summary>
    [Contract]
    public class Modifier
    {
        public DateTime DateModified { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
