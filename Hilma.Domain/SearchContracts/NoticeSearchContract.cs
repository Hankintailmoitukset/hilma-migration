using Hilma.Domain.Enums;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using System;

namespace Hilma.Domain.SearchContracts
{
    [SerializePropertyNamesAsCamelCase]
    public class NoticeSearchContract
    {
        [System.ComponentModel.DataAnnotations.Key, IsSearchable, IsFilterable, IsSortable]
        public string Id { get; set; }

        [IsSearchable, IsFilterable, Analyzer(AnalyzerName.AsString.Keyword)]
        public string NoticeOjsNumber { get; set; }

        [IsSortable, IsFilterable]
        public DateTime? DatePublished { get; set; }
        [IsSortable, IsFilterable]
        public DateTime? DateModified { get; set; }

        [IsFilterable]
        public int? ProcurementProjectId { get; set; }

        /// <summary>
        /// Specific notice type, numeric format of <see cref="NoticeType"/> enum
        /// </summary>
        [IsSortable, IsFilterable]
        public int? Type { get; set; }

        /// <summary>
        /// Used for combining speficic notice types to more generic "main type" e.g. prior information notices, contract notices etc.
        /// This is more relevant for search than <see cref="NoticeType"/>
        /// </summary>
        [IsSortable, IsFilterable]
        public string MainType { get; set; }

        public string ProjectTitle { get; set; }

        [IsSortable, IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        public string ProjectTitleNormalized { get; set; }

        [IsFilterable, IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        [JsonProperty("cpvCodes")]
        public string CpvCodes { get; set; }

        [IsSortable, IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        [JsonProperty("organisationName")]
        public string ProjectOrganisationOrganisationInformationOfficialName { get; set; }

        [IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        [JsonProperty("organisationNationalRegistrationNumber")]
        public string ProjectOrganisationOrganisationInformationNationalRegistrationNumber { get; set; }

        [IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        [JsonProperty("organisationAddress")]
        public string OrganisationAddress { get; set; }

        [IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        [JsonProperty("nutsCodes")]
        public string NutsCodes { get; set; }

        [IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        [JsonProperty("organisationDepartment")]
        public string ProjectOrganisationOrganisationInformationDepartment { get; set; }

        [IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        public string ProjectShortDescription { get; set; }
        
        [IsSearchable, Analyzer(AnalyzerName.AsString.FiLucene)]
        public string ObjectDescriptions { get; set; }

        [IsSortable, IsFilterable]
        public DateTime? TendersOrRequestsToParticipateDueDateTime { get; set; }

        [IsSortable, IsFilterable]
        public int? ParentId { get; set; }

        [IsFilterable]
        public bool IsLatest { get; set; }

        [IsFilterable]
        public bool IsCorrigendum { get; set; }

        [IsFilterable]
        public bool IsCancelled { get; set; }

        [IsFilterable]
        public bool IncludesDynamicPurcharingSystem { get; set; }

        [IsFilterable]
        public bool IncludesFrameworkAgreement { get; set; }

        [IsFilterable]
        public bool IsNationalProcurement { get; set; }
        
        [IsFilterable, IsSearchable, Analyzer(AnalyzerName.AsString.Keyword)]
        public string NoticeNumber { get; internal set; }

        [IsFilterable]
        public double EstimatedValue { get; set; }

        [IsFilterable]
        public string Currency { get; set; }
    }
}
