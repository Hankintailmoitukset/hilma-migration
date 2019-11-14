using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Procedure information for national contracts
    /// </summary>
    [Contract]
    public class ProcedureInformationNational
    {
        /// <summary>
        /// If national notice and procedure type = other
        /// Eligibility requirements for candidates or tenderers
        /// Ehdokkaiden tai tarjoajien soveltuvuutta koskevat vaatimukset
        /// </summary>
        public string[] OtherProcedure { get; set; }

        public string[] AdditionalProcedureInformation { get; set; }
    }
}
