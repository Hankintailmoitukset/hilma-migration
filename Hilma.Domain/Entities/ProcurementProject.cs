using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Exceptions;
using Hilma.Domain.Validators;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Procurement project
    /// </summary>
    public class ProcurementProject : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// II.1.1) Title
        /// </summary>
        [Required]
        [StringMaxLength(400)]
        public string Title { get; set; }

        /// <summary>
        /// II.1.3) Type of contract
        /// </summary>
        [Required]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public ProcurementCategory ProcurementCategory { get; set; }

        /// <summary>
        /// II.1.1) Reference number
        /// </summary>
        public string ReferenceNumber { get; set; }

        public ValidationState ValidationState { get; set; }


        public OrganisationContract Organisation { get; set; }

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
        /// I.2.1) The contract involves join purchase.
        /// </summary>
        public bool JointProcurement { get; set; }

        /// <summary>
        /// I.2.2) Reference to applicable law related to JointProcurement.
        /// </summary>
        [StringMaxLength(200)]
        public string[] ProcurementLaw { get; set; }

        /// <summary>
        /// I.2.3) Contract is awarded by a central purchasing body.
        /// </summary>
        public bool CentralPurchasing { get; set; }

        /// <summary>
        /// If I.2) joint procurement, the list of Co-purchasers are listed here.
        /// </summary>
        public List<ContractBodyContactInformation> CoPurchasers { get; set; }

        /// <summary>
        /// If notices under this project should be published to TED as well
        /// </summary>
        public PublishType Publish { get; set; }

        /// <summary>
        /// Project collaborators
        /// </summary>
        public IList<ProjectCollaborators> Collaborators { get; set; }

        /// <summary>
        /// If NoticeType == NationalAgricultureContract and ContractType == Works
        /// </summary>
        [CorrigendumLabel("agriculture_works", "")]
        public AgricultureWorks AgricultureWorks { get; set; }

        #region Navigation
        public User Creator { get; set; }
        public Guid? CreatorId { get; set; }

        public EtsUser EtsCreator { get; set; }
        public Guid? EtsCreatorId { get; set; }
        #endregion

        #region Methods
        public ProcurementProject() { }

        public ProcurementProject(ProcurementProjectContract dto, User creator)
        {
            Update(dto);
            Creator = creator;
            Collaborators = new List<ProjectCollaborators> { new ProjectCollaborators { User = creator, Project = this } };
        }

        public ProcurementProject(ProcurementProjectContract dto, EtsUser creator)
        {
            Update(dto);
            EtsCreator = creator;
        }

        public ProcurementProject Update(ProcurementProjectContract dto)
        {
            if (State == PublishState.Published)
            {
                if (ProcurementCategory != dto.ProcurementCategory ||
                    ContractType != dto.ContractType)
                {
                    throw new HilmaException("Updating procurement category, contract type is not allowed for published projects");
                }
            }
            // Creator and  Id cannot be updated with this method.
            ValidationState = dto.ValidationState;
            Title = dto.Title; 
            ReferenceNumber = dto.ReferenceNumber;
            Organisation = dto.Organisation;
            CoPurchasers = dto.CoPurchasers;
            CentralPurchasing = dto.CentralPurchasing;
            ProcurementLaw = dto.ProcurementLaw;
            JointProcurement = dto.JointProcurement;
            ContractType = dto.ContractType;
            ProcurementCategory = dto.ProcurementCategory;
            DefenceCategory = dto.DefenceCategory;
            DefenceSupplies = dto.DefenceSupplies;
            DefenceWorks = dto.DefenceWorks;
            Publish = dto.Publish;
            AgricultureWorks = dto.AgricultureWorks;

            return this;
        }

        public void PublishProject()
        {
            State = PublishState.Published;
        }
        #endregion
    }
}
