using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts.EtsContracts
{
    /// <summary>
    ///     Project information.
    /// </summary>
    public class EtsProjectContract
    {
        public int Id { get; set; }
        /// <summary>
        ///     II.1.3) Type of contract
        ///     Type of contract described by this project.
        /// </summary>
        [Required] public ContractType ContractType { get; set; }

        /// <summary>
        ///     II.1.1) Reference number
        ///     Optional reference number for use of the procuring organisation.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        ///     II.1.1) Title
        ///     Given name for the procurement described by this project.
        /// </summary>
        [Required] public string Title { get; set; }


        /// <summary>
        ///     Selection to to help select correct directive and form when later
        ///     creating a notice.
        /// </summary>
        [Required]
        public ProcurementCategory ProcurementCategory { get; set; }

        /// <summary>
        /// I.1.2.1) The contract involves join purchase.
        /// </summary>
        public bool JointProcurement { get; set; }

        /// <summary>
        /// I.1.2.2) Reference to applicable law related to JointProcurement.
        /// </summary>
        public string[] ProcurementLaw { get; set; }

        /// <summary>
        /// I.1.2.3) Contract is awarded by a central purchasing body.
        /// </summary>
        public bool CentralPurchasing { get; set; }

        /// <summary>
        ///     List of involved organisations in case of joint procurement
        /// </summary>
        public List<ContractBodyContactInformation> CoPurchasers { get; set; }

        /// <summary>
        ///     If defence contract and ContractType = Works: use this enum
        /// </summary>
        public Works DefenceWorks { get; set; }
        /// <summary>
        ///     If defence contract and ContractType = Supplies: use this enum
        /// </summary>
        public Supplies DefenceSupplies { get; set; }
        /// <summary>
        ///     If defence contract and ContractType = Services: use this entity
        /// </summary>
        public DefenceCategory DefenceCategory { get; set; }
        /// <summary>
        /// If NoticeType == NationalAgricultureContract and ContractType == Works
        /// </summary>
        public AgricultureWorks AgricultureWorks { get; set; }
    }
}
