using System;
using System.Collections.Generic;
using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Exceptions;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Procurement project
    /// </summary>
    public class ProcurementProject : BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Is the project deleted?
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// II.1.1) Title
        /// </summary>
        //[Required]
        //[StringMaxLength(400)]
        public string Title { get; set; }

        /// <summary>
        /// II.1.3) Type of contract
        /// </summary>
        //[Required]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Required]
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
        /// If defence contract award and DefenceCategory > 20, this should to be set.
        /// </summary>
        public bool? DisagreeToPublishNoticeBasedOnDefenceServiceCategory4 {get; set;}

        /// <summary>
        /// I.2.1) The contract involves join purchase.
        /// </summary>
        public bool JointProcurement { get; set; }

        /// <summary>
        /// I.2.2) Reference to applicable law related to JointProcurement.
        /// </summary>
        //[StringMaxLength(200)]
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
        [CorrigendumLabel("agriculture_works", "I.4")]
        public AgricultureWorks AgricultureWorks { get; set; }

        /// <summary>
        /// If project is private (salainen)
        /// Normally projects that have not been published can be viewed by all employees.
        /// Not published notices still cannot be viewed by all employees (collabs only).
        /// Private projects are visible only to collaborators, until a notice is published.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Is procurement concluded
        /// Affects how the procurement is shown to the user
        /// </summary>
        public bool IsConcluded { get; set; }

        #region Navigation
        public User Creator { get; set; }
        public Guid? CreatorId { get; set; }

        public EtsUser EtsCreator { get; set; }
        public Guid? EtsCreatorId { get; set; }

        public Organisation OwningOrganisation { get; set; }
        public Guid? OwningOrganisationId { get; set; }
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

        public ProcurementProject Update(ProcurementProjectContract dto, bool ignoreErrors = false )
        {
            if (State == PublishState.Published)
            {
                if (!ignoreErrors && ( ProcurementCategory != dto.ProcurementCategory ||
                    ContractType != dto.ContractType))
                {
                    throw new HilmaException("Updating procurement category, contract type is not allowed for published projects");
                }
            }
            // Creator and  Id cannot be updated with this method.
            ValidationState = dto.ValidationState;
            Title = dto.Title; 
            ReferenceNumber = dto.ReferenceNumber;
            Organisation = dto.Organisation;

            if (dto.Organisation?.Id != Guid.Empty) {
                OwningOrganisationId = dto.Organisation?.Id;
            }
            CoPurchasers = dto.CoPurchasers;
            CentralPurchasing = dto.CentralPurchasing;
            ProcurementLaw = dto.ProcurementLaw;
            JointProcurement = dto.JointProcurement;
            ContractType = dto.ContractType;
            ProcurementCategory = dto.ProcurementCategory;
            DefenceCategory = dto.DefenceCategory;
            DisagreeToPublishNoticeBasedOnDefenceServiceCategory4 = dto.DisagreeToPublishNoticeBasedOnDefenceServiceCategory4;
            DefenceSupplies = dto.DefenceSupplies;
            DefenceWorks = dto.DefenceWorks;
            Publish = dto.Publish;
            AgricultureWorks = dto.AgricultureWorks;
            IsPrivate = dto.IsPrivate;
            IsConcluded = dto.IsConcluded;

            return this;
        }

        public void PublishProject()
        {
            State = PublishState.Published;
        }
        #endregion
    }
}
