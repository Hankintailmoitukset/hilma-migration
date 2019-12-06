using AutoMapper;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;

namespace Hilma.Domain.SearchContracts
{
    /// <summary>
    /// AutoMapper configuration factory for search contracts
    /// </summary>
    public static class SearchContractMapper
    {
        /// <summary>
        /// Create mapper for NoticeContract and NoticeSearchContract
        /// </summary>
        /// <returns>IMapper</returns>
        public static IMapper Create()
        {
            return null;
            ////var config = new MapperConfiguration(cfg => cfg.CreateMap<NoticeContract, NoticeSearchContract>()
            ////                    .ForMember(d => d.DatePublished, opt => opt.MapFrom(x => x.DatePublished))
            ////                    .ForMember(d => d.NoticeOjsNumber, opt => opt.MapFrom(x => x.NoticeOjsNumber))
            ////                    .ForMember(d => d.IsCorrigendum, opt => opt.MapFrom(x => x.IsCorrigendum))
            ////                    .ForMember(d => d.IsCancelled, opt => opt.MapFrom(x => x.IsCancelled))
            ////                    .ForMember(d => d.NoticeNumber, opt => opt.MapFrom( x => x.NoticeNumber ))
            ////                    .ForMember(d => d.ProjectTitleNormalized, opt => opt.MapFrom((x,s) => x.Project?.Title?.ToUpperInvariant()))
            ////                    .ForMember(d => d.Type, opt => opt.MapFrom(source => (int)source.Type))
            ////                    .ForMember(d => d.MainType, opt => opt.MapFrom(source => GetMainNoticeType(source.Type)))
            ////                    .ForMember(d => d.ProjectOrganisationOrganisationInformationOfficialName, d => d.MapFrom((x, s) => x.Project?.Organisation?.Information?.OfficialName))
            ////                    .ForMember(d => d.ProjectOrganisationOrganisationInformationDepartment, d => d.MapFrom((x, s) => x.Project?.Organisation?.Information?.Department))
            ////                    .ForMember(d => d.ProjectOrganisationOrganisationInformationNationalRegistrationNumber, d => d.MapFrom((x, s) => x.Project?.Organisation?.Information?.NationalRegistrationNumber))
            ////                    .ForMember(d => d.OrganisationAddress, d => d.MapFrom((x, s) => string.Join(" ",
            ////                        x.Project?.Organisation?.Information?.PostalAddress?.StreetAddress,
            ////                        x.Project?.Organisation?.Information?.PostalAddress?.PostalCode,
            ////                        x.Project?.Organisation?.Information?.PostalAddress?.Town,
            ////                        x.Project?.Organisation?.Information?.PostalAddress?.Country)))
            ////                    .ForMember(d => d.CpvCodes, d => d.MapFrom((x,s) =>
            ////                                                          string.Join(" ", x.ObjectDescriptions?.SelectMany(l => l.AdditionalCpvCodes).Union(new List<CpvCode> { x.ProcurementObject.MainCpvCode }).Select(o => $"{o?.Code} {o?.Name}"))))
            ////                    .ForMember(d => d.NutsCodes, d => d.MapFrom((x,s) => string.Join(" ",
            ////                        x.Project?.Organisation?.Information?.NutsCodes.Aggregate("", (current, next) => current + " " + next),
            ////                        string.Join(" ", x.ObjectDescriptions.Select(l => $"{l.NutsCodes.Aggregate("", (current, next) => current + " " + next)}")))))
            ////                    .ForMember(d => d.TendersOrRequestsToParticipateDueDateTime, d => d.MapFrom((x,s) => x.TenderingInformation?.TendersOrRequestsToParticipateDueDateTime))
            ////                    .ForMember(d => d.IncludesDynamicPurcharingSystem, d => d.MapFrom((n, s) => n.ProcedureInformation?.FrameworkAgreement?.IncludesDynamicPurchasingSystem ?? false))
            ////                    .ForMember(d => d.IncludesFrameworkAgreement, d => d.MapFrom((n, s) => n.ProcedureInformation?.FrameworkAgreement?.IncludesFrameworkAgreement ?? false))
            ////                    .ForMember(d => d.IsNationalProcurement, d => d.MapFrom( (n,s) => n.Project.Publish == PublishType.ToHilma ))
            ////                    .ForMember(d => d.ObjectDescriptions,
            ////                               d => d.MapFrom((x,s) => string.Join(" ", x.ObjectDescriptions.Select(l => $"{l.Title ?? ""} {string.Join(" ", l.DescrProcurement??new[] { "" })}"))))
            ////                    );
            ////return config.CreateMapper();
        }

        private static string GetMainNoticeType(NoticeType type)
        {
            if(type.IsPriorInformation()) {
                return nameof(NoticeTypes.PriorInformationNotices);
            }
            if (type.IsContract()) {
                return nameof( NoticeTypes.ContractNotices);
            }
            if (type.IsContractAward()) {
                return nameof(NoticeTypes.ContractAwardNotices);
            }
            if (type == NoticeType.Modification)
            {
                return nameof(NoticeType.Modification);
            }
            if (type.IsNational())
            {
                return nameof(NoticeTypes.NationalNotices);
            }
            return null;
        }
    }
}
