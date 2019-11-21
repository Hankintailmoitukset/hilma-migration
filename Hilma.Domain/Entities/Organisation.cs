using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Exceptions;
using Hilma.Domain.Extensions;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Entity representing a contracting authority or contracting entity.
    /// </summary>
    public class Organisation : BaseEntity
    {
        /// <summary>
        ///     Primary key for the entity.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Main bunch of form information for the organisation.
        /// </summary>
        public ContractBodyContactInformation Information { get; set; }


        /// <summary>
        /// Type of the contracting authority
        /// </summary>
        [Required]
        public ContractingAuthorityType ContractingAuthorityType { get; set; }

        /// <summary>
        /// Asked if ContractingAuthorityType is "Other"
        /// </summary>
        public string OtherContractingAuthorityType { get; set; }

        /// <summary>
        ///     Vuejs application validation state
        /// </summary>
        public ValidationState ValidationState { get; set; }

        public MainActivity MainActivity { get; set; }

        public MainActivityUtilities MainActivityUtilities { get; set; }

        /// <summary>
        /// Asked if MainActivity is "Other"
        /// </summary>
        public string OtherMainActivity { get; set; }

        #region Navigation
        /// <summary>
        ///     Navigational property for users belonging to organisation.
        /// </summary>
        public List<OrganisationUser> OrganisationUsers { get; set; }

        /// <summary>
        ///     Applications to the organisation.
        /// </summary>
        public List<OrganisationMembershipApplication> MembershipApplications { get; set; }
        #endregion

        #region Methods
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public Organisation() { }

        /// <summary>
        ///     Constructor from dto.
        /// </summary>
        /// <param name="dto">The dto to build the entity from.</param>
        public Organisation(OrganisationContract dto)
        {
            Update(dto);
        }

        /// <summary>
        ///     Update method for the entity.
        /// </summary>
        /// <param name="dto">Dto to update the entity from.</param>
        public void Update(OrganisationContract dto)
        {
            if (dto.ContractingAuthorityType == ContractingAuthorityType.OtherType
                && string.IsNullOrEmpty(dto.OtherContractingAuthorityType))
            {
                throw new HilmaMalformedRequestException($"if ContractingAuthorityType is {ContractingAuthorityType.OtherType} then OtherContractingAuthorityType is required, but null or empty string was provided!");
            }

            if ((dto.MainActivity == MainActivity.OtherActivity || dto.MainActivityUtilities == MainActivityUtilities.OtherActivity)
                && string.IsNullOrEmpty(dto.OtherMainActivity))
            {
                throw new HilmaMalformedRequestException($"if MainActivity is {MainActivity.OtherActivity} then OtherMainActivity is required, but null or empty string was provided!");
            }

            ValidationState = dto.ValidationState;
            Information = dto.Information;
            ContractingAuthorityType = dto.ContractingAuthorityType;
            OtherContractingAuthorityType = dto.ContractingAuthorityType == ContractingAuthorityType.OtherType ? dto.OtherContractingAuthorityType : null;
            MainActivity = dto.MainActivity;
            MainActivityUtilities = dto.MainActivityUtilities;
            OtherMainActivity = (dto.MainActivity == MainActivity.OtherActivity || dto.MainActivityUtilities == MainActivityUtilities.OtherActivity) ? dto.OtherMainActivity : null;
        }
        #endregion
    }
}
