using System;
using AutoMapper;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;

namespace Hilma.Domain.Profiles
{
    /// <summary>
    /// Mapper profiles, automatically discovered by AutoMapper from this assembly, hopefully.
    /// </summary>
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Notice, NoticeContract>()
                .AfterMap((s, d) =>
                {
                    d.CreatorSystem = s.EtsCreator?.Name;
                });

            CreateMap<Department, DepartmentContract>()
                .BeforeMap((s, d) =>
                {
                    if (s.Information.PostalAddress != null)
                    {
                        d.PostalAddress = new PostalAddress
                        {
                            Country = s.Information.PostalAddress.Country,
                            PostalCode = s.Information.PostalAddress.PostalCode,
                            StreetAddress = s.Information.PostalAddress.StreetAddress,
                            Town = s.Information.PostalAddress.Town,
                        };
                    }

                    if (s.Information.NutsCodes != null)
                    {
                        d.NutsCodes = new string[s.Information.NutsCodes.Length];
                        for (var i = 0; i < s.Information.NutsCodes.Length; i++)
                        {
                            d.NutsCodes[i] = s.Information.NutsCodes[i];
                        }
                    }
                })
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Department, o => o.MapFrom(s => s.Information.Department))
                .ForMember(d => d.TelephoneNumber, o => o.MapFrom(s => s.Information.TelephoneNumber))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Information.Email))
                .ForMember(d => d.ContactPerson, o => o.MapFrom(s => s.Information.ContactPerson))
                .ForMember(d => d.MainUrl, o => o.MapFrom(s => s.Information.MainUrl))
                .ForMember(d => d.ValidationState, o => o.MapFrom(s => s.Information.ValidationState))
                .ForMember(d => d.ContractingAuthorityType, o => o.MapFrom(s => s.ContractingAuthorityType))
                .ForMember(d => d.OtherContractingAuthorityType, o => o.MapFrom(s => s.OtherContractingAuthorityType))
                .ForMember(d => d.ContractingType, o => o.MapFrom(s => s.ContractingType))
                .ForMember(d => d.MainActivity, o => o.MapFrom(s => s.MainActivity))
                .ForMember(d => d.OtherMainActivity, o => o.MapFrom(s => s.OtherMainActivity))
                .ForMember(d => d.MainActivityUtilities, o => o.MapFrom(s => s.MainActivityUtilities));

            CreateMap<DepartmentContract, Department>()
                .BeforeMap((s, d) =>
                {
                    d.Id = s.Id ?? Guid.Empty;
                    d.Information = new ContractBodyContactInformation
                    {
                        OfficialName = string.Empty,
                        Department = s.Department,
                        PostalAddress = s.PostalAddress != null
                            ? new PostalAddress
                            {
                                PostalCode = s.PostalAddress.PostalCode,
                                Country = s.PostalAddress.Country,
                                StreetAddress = s.PostalAddress.StreetAddress,
                                Town = s.PostalAddress.Town,
                            }
                            : null,
                        TelephoneNumber = s.TelephoneNumber,
                        Email = s.Email,
                        ContactPerson = s.ContactPerson,
                        MainUrl = s.MainUrl,
                        ValidationState = s.ValidationState
                    };

                    if (s.NutsCodes != null)
                    {
                        d.Information.NutsCodes = new string[s.NutsCodes.Length];
                        for (var i = 0; i < s.NutsCodes.Length; i++)
                        {
                            d.Information.NutsCodes[i] = s.NutsCodes[i];
                        }
                    }
                })
                .ForMember(d => d.ContractingAuthorityType, o => o.MapFrom(s => s.ContractingAuthorityType))
                .ForMember(d => d.OtherContractingAuthorityType, o => o.MapFrom(s => s.OtherContractingAuthorityType))
                .ForMember(d => d.ContractingType, o => o.MapFrom(s => s.ContractingType))
                .ForMember(d => d.MainActivity, o => o.MapFrom(s => s.MainActivity))
                .ForMember(d => d.OtherMainActivity, o => o.MapFrom(s => s.OtherMainActivity))
                .ForMember(d => d.MainActivityUtilities, o => o.MapFrom(s => s.MainActivityUtilities));
        }
    }
}
