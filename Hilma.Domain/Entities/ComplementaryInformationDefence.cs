using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;

namespace Hilma.Domain.Entities
{

    /// <summary>
    ///     Directive 2009/81/EC (Defence notices) 
    ///     Section VI: Complementary information
    /// </summary>
    [Contract]
    public class ComplementaryInformationDefence
    {

        /// <summary>
        ///     Conrtacts and awards
        ///     If this objects purchase is funded by EU project.
        /// </summary>
        [CorrigendumLabel("eu_progr_info", "VI.2")]
        public EuFunds EuFunds { get; set; } = new EuFunds();

        /// <summary>
        ///     Prior information
        ///     VI.3) Information on general regulatory framework
        ///     Tax legislation
        /// </summary>
        [CorrigendumLabel("tax_legislation", "VI.3")]
        public string TaxLegislationUrl { get; set; }

        /// <summary>
        /// True, if TaxLegislation should be filled.
        /// </summary>
        public bool TaxLegislationInfoProvided { get; set; }

        /// <summary>
        ///     Prior information
        ///     VI.3) Information on general regulatory framework
        ///     Annex A II)
        /// </summary>
        [CorrigendumLabel("tax_legislation_additional_info_provided", "VI.3")]
        public ContractBodyContactInformation TaxLegislation { get; set; }

        /// <summary>
        ///     Prior information
        ///     VI.3) Information on general regulatory framework
        ///     Environmental protection legislation
        /// </summary>
        [CorrigendumLabel("environmental_protection", "VI.3")]
        public string EnvironmentalProtectionUrl { get; set; }

        /// <summary>
        /// True, if EnvironmentalProtection should be filled.
        /// </summary>
        public bool EnvironmentalProtectionInfoProvided { get; set; }

        /// <summary>
        ///     Prior information
        ///     VI.3) Information on general regulatory framework
        ///     Environmental protection legislation
        ///     Annex A III)
        /// </summary>
        [CorrigendumLabel("environmental_protection_additional_info_provided", "VI.3")]
        public ContractBodyContactInformation EnvironmentalProtection { get; set; }

        /// <summary>
        ///     Prior information
        ///     VI.3) Information on general regulatory framework
        ///     Employment protection and working conditions
        /// </summary>
        [CorrigendumLabel("employment_protection", "VI.3")]
        public string EmploymentProtectionUrl { get; set; }

        /// <summary>
        /// True, if EmploymentProtection should be filled.
        /// </summary>
        public bool EmploymentProtectionInfoProvided { get; set; }

        /// <summary>
        ///     Prior information
        ///     VI.3) Information on general regulatory framework
        ///     Employment protection and working conditions
        ///     Annex A IV)
        /// </summary>
        [CorrigendumLabel("employment_protection_additional_info_provided", "VI.3")]
        public ContractBodyContactInformation EmploymentProtection { get; set; }
    }
}
