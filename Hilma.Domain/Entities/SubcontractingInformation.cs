using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Directive 2009/81/EC (Defence contracts)
    /// II.1.7) Information about subcontracting
    /// </summary>
    [Contract]
    public class SubcontractingInformation
    {
        /// <summary>
        /// The tenderer has to indicate in the tender any share of the contract it may intend to subcontract to third parties and any proposed
        /// subcontractor, as well as the subject-matter of the subcontracts for which they are proposed. (if applicable)
        /// </summary>
        public bool TendererHasToIndicateShare { get; set; }

        /// <summary>
        /// The tenderer has to indicate any change occurring at the level of subcontractors during the execution of the contract. (if applicable)
        /// </summary>
        public bool TendererHasToIndicateChange { get; set; }

        /// <summary>
        /// The contracting authority/entity may oblige the successful tenderer to award all or certain subcontracts through the procedure set out in
        /// Title III of Directive 2009/81/EC.
        /// </summary>
        public bool CaMayOblige { get; set; }

        /// <summary>
        /// The successful tenderer is obliged to subcontract the following share of the contract through the procedure set out in Title III of Directive
        /// 2009/81/EC: minimum percentage: [ ][ ],[ ] (%), maximum percentage: [ ][ ],[ ] (%) of the value of the contract. 
        /// </summary>
        public bool SuccessfulTenderer { get; set; }

        /// <summary>
        /// minimum percentage
        /// </summary>
        public decimal SuccessfulTendererMin { get; set; }

        /// <summary>
        /// maximum percentage
        /// </summary>
        public decimal SuccessfulTendererMax { get; set; }

        /// <summary>
        /// The successful tenderer is obliged to specify which part or parts of the contract it intends to subcontract beyond the required percentage
        /// and to indicate the subcontractors already identified. (if applicable)
        /// </summary>
        public bool SuccessfulTendererToSpecify { get; set; }
    }
}
