using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Attributes;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Validators;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes a procurement project.
    /// </summary>
    [MapsFrom(typeof(ProcurementProject))]
    [Contract]
    public class ProcurementProjectContract : BaseEntity
    {
        /// <summary>
        ///     Hilma assigned primary key.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///     Title displayed for this notice in various UIs
        /// </summary>
        [Required]
        [CorrigendumLabel("title_official", "II.1.1")]
        [StringMaxLength(400)]
        public string Title { get; set; }
        /// <summary>
        ///     Type of procurement according to TED taxonomy. This limits
        ///     what CPV codes are acceptable, for example. Is not always
        ///     intuitive at all.
        /// </summary>
        [Required]
        [CorrigendumLabel("type_contract", "II.1.3")]
        public ContractType ContractType { get; set; }

        /// <summary>
        ///     Selection to to help select correct directive and form when later
        ///     creating a notice.
        /// </summary>
        [Required]
        public ProcurementCategory ProcurementCategory { get; set; }

        /// <summary>
        ///     User assigned reference number
        /// </summary>
        [CorrigendumLabel("fileref", "II.1.1")]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// I.1.2.1) The contract involves join purchase.
        /// </summary>
        [CorrigendumLabel("joint_procurement_involved", "I.1.2")]
        public bool JointProcurement { get; set; }
        /// <summary>
        /// I.1.2.2) Reference to applicable law related to JointProcurement.
        /// </summary>
        [CorrigendumLabel("procurement_law", "I.1.2")]
        [StringMaxLength(200)]
        public string[] ProcurementLaw { get; set; }
        /// <summary>
        /// I.1.2.3) Contract is awarded by a central purchasing body.
        /// </summary>
        [CorrigendumLabel("central_purchasing", "I.1.2")]
        public bool CentralPurchasing { get; set; }
        /// <summary>
        ///     List of co-purchasers in a joint purchase.
        /// </summary>
        public List<ContractBodyContactInformation> CoPurchasers { get; set; } = new List<ContractBodyContactInformation>();

        /// <summary>
        ///     Creator Id for user created procurement projects. Ets API
        ///     created projects do not have a creator. The entities instead
        ///     have EtsCreator, but those are not to be mapped into
        ///     this type of contract for now.
        /// </summary>
        public Guid? CreatorId { get; set; }
        /// <summary>
        ///     Vuejs application validation state for organisation form section.
        /// </summary>
        public ValidationState ValidationState { get; set; }
        /// <summary>
        ///     Organisation contract at the time of updating this project. It is
        ///     important that the organisation information is not updated to
        ///     project and notices without the knowledge of the user.
        /// </summary>
        public OrganisationContract Organisation { get; set; }
        /// <summary>
        ///     Publication status to Hilma. Goes to public when atleast once
        ///     notice in the project goes public.
        /// </summary>
        public PublishState State { get; set; }
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
        /// If defence contract award and DefenceCategory > 20, this should to be set.
        /// </summary>
        public bool? DisagreeToPublishNoticeBasedOnDefenceServiceCategory4 { get; set; }
        /// <summary>
        /// If notices under this project should be published to TED as well.
        /// </summary>
        public PublishType Publish { get; set; }
        /// <summary>
        /// If NoticeType == NationalAgricultureContract and ContractType == Works
        /// </summary>
        public AgricultureWorks AgricultureWorks { get; set; }

        /// <summary>
        /// Used by Hilma App.
        /// If project is private (salainen)
        /// Normally projects that have not been published can be viewed by all employees.
        /// Not published notices is still limited to collabs only.
        /// Private projects are visible only to collaborators, until a notice is published.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Is procurement concluded
        /// Affects how the procurement is shown to the user
        /// </summary>
        public bool IsConcluded { get; set; }
    }
}
