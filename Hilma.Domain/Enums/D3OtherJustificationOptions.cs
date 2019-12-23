using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract]
    public enum D3OtherJustificationOptions
    {
        /// <summary>
        /// The contract has as its object services listed in Annex II B of the Directive
        /// D.13
        /// </summary>
        ContractServicesListedInDirective = 0,
        /// <summary>
        /// The contract falls outside the scope of application of the Directive
        /// D.14
        /// </summary>
        ContractServicesOutsideDirective = 1
    }
}
