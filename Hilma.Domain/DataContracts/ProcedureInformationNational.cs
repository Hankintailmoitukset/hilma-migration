using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

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

        /// <summary>
        /// Used in national transparency notices
        /// Ilmoituksen tyyppi
        /// </summary>
        public TransparencyType TransparencyType { get; set; }

        /// <summary>
        /// Used in national transparency notices
        /// Menettelyyn valitaan rajoitettu määrä osallistujia
        /// </summary>
        public bool LimitedNumberOfParticipants { get; set; }

        /// <summary>
        /// Used in national transparency notices
        /// jos kyllä: kuinka monta / valittavien osallistujien enimmäismäärä
        /// </summary>
        public int NumberOfParticipants { get; set; }

        /// <summary>
        /// Used in national transparency notices
        /// Valintaperuste
        /// (jos menettelyyn sisältyy valintaa)
        /// </summary>
        public string[] SelectionCriteria { get; set; }
    }
}
