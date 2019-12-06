using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Annex C3 - Defence and security
    ///     Service categories referred to in Section II: Object of the contract
    /// </summary>
    //[Owned]
    [Contract]
    public class DefenceCategory
    {
        /// <summary>
        ///     The code itself.
        /// </summary>
        //[RegularExpression("[3-9]|1[0-9]?|2[0-6]?")]
        //[Required]
        public string Code { get; set; }

        /// <summary>
        ///     Clear text explanation for the code.
        /// </summary>
        public string Name { get; set; }
    }
}
