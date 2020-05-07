using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hilma.Domain.DataContracts;
using Hilma.Domain.DataContracts.EtsContracts;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities {
    public class Department : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public ContractBodyContactInformation Information { get; set; }

        public ContractingAuthorityType ContractingAuthorityType { get; set; }
        public string OtherContractingAuthorityType { get; set; }
        public ContractingType ContractingType { get; set; }
        public MainActivity MainActivity { get; set; }
        public string OtherMainActivity { get; set; }
        public MainActivityUtilities MainActivityUtilities { get; set; }

        public string EtsIdentifier { get; set; }
        public EtsUser EtsCreator { get; set; }
        public Guid? EtsCreatorId { get; set; }

        public Guid OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        public List<Notice> Notices { get; set; }

        public void Update(DepartmentContract dto)
        {
            Information.Department = dto.Department;
            Information.ContactPerson = dto.ContactPerson;
            Information.MainUrl = dto.MainUrl;
            Information.NutsCodes = dto.NutsCodes;
            Information.PostalAddress = dto.PostalAddress;
            Information.TelephoneNumber = dto.TelephoneNumber;
            Information.ValidationState = dto.ValidationState;
            Information.Email = dto.Email;

            ContractingAuthorityType = dto.ContractingAuthorityType;
            OtherContractingAuthorityType = dto.OtherContractingAuthorityType;
            ContractingType = dto.ContractingType;
            MainActivity = dto.MainActivity;
            OtherMainActivity = dto.OtherMainActivity;
            MainActivityUtilities = dto.MainActivityUtilities;
        }

        public void Update(EtsOrganisationContract dto)
        {
            Information.OfficialName = dto.Information.OfficialName;
            Information.Department = dto.Information.Department;
            Information.ContactPerson = dto.Information.ContactPerson;
            Information.MainUrl = dto.Information.MainUrl;
            Information.NutsCodes = dto.Information.NutsCodes;
            Information.PostalAddress = dto.Information.PostalAddress;
            Information.TelephoneNumber = dto.Information.TelephoneNumber;
            Information.ValidationState = ValidationState.Pristine;
            Information.Email = dto.Information.Email;

            ContractingAuthorityType = dto.ContractingAuthorityType;
            OtherContractingAuthorityType = dto.OtherContractingAuthorityType;
            ContractingType = dto.ContractingType;
            MainActivity = dto.MainActivity;
            OtherMainActivity = dto.OtherMainActivity;
            MainActivityUtilities = dto.MainActivityUtilities;
        }
    }
}
