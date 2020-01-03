using System;
using System.Collections.Generic;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    /// 
    /// </summary>
    [Contract]
    public class ContractAward
    {
        /// <summary>
        ///     Date of conclusion of the contract.
        /// </summary>
        [CorrigendumLabel("date_award", "V.2.1")]
        public DateTime? ConclusionDate { get; set; }

        /// <summary>
        ///     Contract No
        /// </summary>
        [CorrigendumLabel("contract_number", "V.0.0")]
        public string ContractNumber { get; set; }

        /// <summary>
        ///     Contract title
        /// </summary>
        [CorrigendumLabel("contract_title", "V.0.0")]
        public string ContractTitle { get; set; }

        #region V.2.2 Information about tenders
        /// <summary>
        ///     Numbers for different types of tenders received.
        /// </summary>
        public NumberOfTenders NumberOfTenders { get; set; } = new NumberOfTenders();
        #endregion

        #region V.2.3 Name and address of contractor
        /// <summary>
        ///     If the information in this section is confidential and should not be published on TED, it must be indicated by selecting false.
        /// </summary>
        [CorrigendumLabel("disagreeContractorInformationToBePublished", "V.2.3")]
        public bool DisagreeContractorInformationToBePublished { get; set; }
        /// <summary>
        ///     
        /// </summary>
        public List<ContractorContactInformation> Contractors { get; set; } = new List<ContractorContactInformation>();
        #endregion

        #region V.2.4 Contract Value
        /// <summary>
        ///     Initial estimated total value of the contract / lot
        /// </summary>
        [CorrigendumLabel("value_estim_total_contract", "V.2.4.2")]
        public ValueContract InitialEstimatedValueOfContract { get; set; } = new ValueContract();
        /// <summary>
        ///     Total final value of the contract or lot.
        /// </summary>
        [CorrigendumLabel("lowest_offer", "V.2.4")]
        public ValueRangeContract FinalTotalValue { get; set; } = new ValueRangeContract();
        #endregion

        #region V.2.5 Information about subcontracting
        /// <summary>
        ///     The contract is likely to be subcontracted to third parties.
        /// </summary>
        [CorrigendumLabel("subcontr_likely", "V.2.5")]
        public bool LikelyToBeSubcontracted { get; set; }

        /// <summary>
        ///     The value of likely subcontracted part.
        /// </summary>
        public ValueContract ValueOfSubcontract { get; set; } = new ValueContract();

        /// <summary>
        ///     Proportion of the contract likely to be subcontracted, 0-100 (%)
        /// </summary>
        [CorrigendumLabel("proportion", "V.2.5")]
        public decimal? ProportionOfValue { get; set; }

        /// <summary>
        ///     Short description of the part of the contract to be subcontracted.
        /// </summary>
        [CorrigendumLabel("subcontr_descr_short", "V.2.5")]
        //[StringMaxLength(2000)]
        public string[] SubcontractingDescription { get; set; }

        /// <summary>
        ///     Contains F15 specific subcontracting fields.
        /// </summary>
        public ExAnteSubcontracting ExAnteSubcontracting { get; set; }
        #endregion
        
        // 2.6 contract award utilities
        /// <summary>
        ///     Price paid for bargain purchases 
        /// </summary>
        [CorrigendumLabel("price_paid_for_bargain_purchases", "V.2.6")]
        public ValueContract PricePaidForBargainPurchases { get; set; }

        /// <summary>
        /// Required but not public fields
        /// </summary>
        public ContractAwardNotPublicFields NotPublicFields { get; set; } = new ContractAwardNotPublicFields();

        /// <summary>
        /// Revenue from the payment of fees and fines by the users - concession award notices
        /// </summary>
        [CorrigendumLabel("concess_fees_prices", "V.2.4")]
        public ValueContract ConcessionRevenue { get; set; }

        /// <summary>
        /// Prices, payments or other financial advantages provided by the contracting authority - concession award notices
        /// </summary>
        [CorrigendumLabel("concess_fees_prices", "V.2.4")]
        public ValueContract PricesAndPayments { get; set; }

        /// <summary>
        /// Any other details relevant to the value of the concession according to Art. 8(3) of the Directive - concession award notices
        /// </summary>
        [CorrigendumLabel("concess_other_details", "V.2.4")]
        public string[] ConcessionValueAdditionalInformation { get; set; }
    }
}
