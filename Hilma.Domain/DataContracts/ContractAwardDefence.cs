using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Directive 2009/81/EY (Defence contract award)
    /// </summary>
    [Contract]
    public class ContractAwardDefence
    {
        /// <summary>
        /// Lot no (not required, if lotinfo.divisionlots = false)
        /// </summary>
        public int LotNumber { get; set; }

        /// <summary>
        /// Lot title (not required, if lotinfo.divisionlots = false)
        /// </summary>
        [CorrigendumLabel("title", "V")]
        public string LotTitle { get; set; }
        /// <summary>
        /// Contract number
        /// </summary>
        public string ContractNumber { get; set; }
        /// <summary>
        ///     V.1) Date of contract award decision
        /// </summary>
        [CorrigendumLabel("date_award_exante", "V.1")]
        public DateTime? ContractAwardDecisionDate { get; set; }

        /// <summary>
        ///     V.2)
        ///     Numbers for different types of tenders received.
        ///     Only fill Total and Electronic
        /// </summary>
        public NumberOfTenders NumberOfTenders { get; set; } = new NumberOfTenders();

        /// <summary>
        ///     V.3) Name and address of economic operator in favour of whom the contract award decision has been taken
        ///     Contractor
        /// </summary>
        public ContractorContactInformation Contractor { get; set; } = new ContractorContactInformation();

        #region V.4) Information on value of contract
        /// <summary>
        /// V.4) Information on value of contract
        /// </summary>
        [CorrigendumLabel("value_estim_total_contract", "V.4")]
        public ValueContract EstimatedValue { get; set; } = new ValueContract();

        /// <summary>
        /// Determines whether FinalTotalValue or Highest/Lowest offer value should be given.
        /// </summary>
        public ContractValueType ContractValueType { get; set; }

        /// <summary>
        ///     Total final value of the contract or lot.
        /// </summary>
        [CorrigendumLabel("value_total_final", "V.4")]
        public ValueContract FinalTotalValue { get; set; } = new ValueContract();

        /// <summary>
        ///     Lowest offer received excluding VAT.
        /// </summary>
        [CorrigendumLabel("lowest_offer", "V.4")]
        public ValueContract LowestOffer { get; set; } = new ValueContract();

        /// <summary>
        ///     Highest offer received excluding VAT.
        /// </summary>
        [CorrigendumLabel("highest_offer", "V.4")]
        public ValueContract HighestOffer { get; set; } = new ValueContract();

        /// <summary>
        /// If annual or monthly value: (please give)
        /// </summary>
        public TimeFrame AnnualOrMonthlyValue { get; set; } = new TimeFrame();
        #endregion

        #region V.5) Information about subcontracting
        /// <summary>
        ///     The contract is likely to be subcontracted to third parties.
        /// </summary>
        [CorrigendumLabel("subcontr_likely", "V.5")]
        public bool LikelyToBeSubcontracted { get; set; }

        /// <summary>
        ///     The value of likely subcontracted part.
        /// </summary>
        [CorrigendumLabel("subcontr_proportion_likely", "V.5")]
        public ValueContract ValueOfSubcontract { get; set; } = new ValueContract();

        /// <summary>
        ///     Proportion of the contract likely to be subcontracted, 0-100 (%)
        /// </summary>
        [CorrigendumLabel("proportion", "V.5")]
        public decimal? ProportionOfValue { get; set; }

        /// <summary>
        /// Not known
        /// </summary>
        [CorrigendumLabel("not_known", "V.5")]
        public bool ValueOfSubcontractNotKnown { get; set; }

        /// <summary>
        ///     Short description of the part of the contract to be subcontracted.
        /// </summary>
        [CorrigendumLabel("subcontr_descr_short", "V.5")]
        public string[] SubcontractingDescription { get; set; }

        /// <summary>
        /// All or certain subcontracts will be awarded through a competitive procedure (see Title III of Directive 2009/81/EC)
        /// </summary>
        [CorrigendumLabel("subcontr_all_competitive", "V.5")]
        public bool AllOrCertainSubcontractsWillBeAwarded { get; set; }

        /// <summary>
        /// A share of the contract will be subcontracted through a competitive procedure (see Title III of Directive 2009/81/EC)
        /// </summary>
        [CorrigendumLabel("subcontr_share_competitive", "V.5")]
        public bool ShareOfContractWillBeSubcontracted { get; set; }

        /// <summary>
        /// minimum percentage
        /// </summary>
        [CorrigendumLabel("min_percentage", "V.5")]
        public decimal? ShareOfContractWillBeSubcontractedMinPercentage { get; set; }

        /// <summary>
        /// maximum percentage
        /// </summary>
        [CorrigendumLabel("max_percentage", "V.5")]
        public decimal? ShareOfContractWillBeSubcontractedMaxPercentage { get; set; }
        #endregion

        /// <summary>
        ///     Directive 2009/81/EY (Defence contract award)
        ///     Validation state for Vuejs application.
        /// </summary>
        public ValidationState ValidationState { get; set; }
    }
}
