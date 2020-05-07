using System;
using System.ComponentModel.Design;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Integrations.General
{
    public static class DirectiveMapper
    {
        /// <summary>
        /// Directive 2014/24/EU on public procurement
        /// </summary>
        public const string EuEuratom2018Directive = "32018R1046";

        /// <summary>
        /// Directive (EU, Euratom) N:o 2018/1046
        /// </summary>
        public const string EuPublicProcurements2014Directive = "32014L0024";

        /// <summary>
        /// Directive 2014/25/EU on procurement by entities operating in the water, energy, transport and postal services sectors.
        /// </summary>
        public const string EuUtilitiesProcurements2014Directive = "32014L0025";

        /// <summary>
        /// Directive 2014/23/EU Concession notices
        /// </summary>
        public const string EuConcessionProcurement2014Directive = "32014L0023";

        /// <summary>
        /// Directive 2009/81/EC Defence contracts
        /// </summary>
        public const string EuDefenceProcurements2009Directive = "32009L0081";

        public static string GetDirective(NoticeContract notice, NoticeContract parent)
        {
            switch (notice.Type)
            {
                case NoticeType.PriorInformation:
                case NoticeType.PriorInformationReduceTimeLimits:
                case NoticeType.Contract:
                case NoticeType.ContractAward:
                    return notice.Project.Organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeEu ? EuEuratom2018Directive : EuPublicProcurements2014Directive;
                case NoticeType.PeriodicIndicativeUtilities:
                case NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits:
                case NoticeType.ContractUtilities:
                case NoticeType.ContractAwardUtilities:
                case NoticeType.QualificationSystemUtilities:
                case NoticeType.SocialUtilities:
                case NoticeType.SocialUtilitiesPriorInformation:
                case NoticeType.SocialUtilitiesContractAward:
                case NoticeType.SocialUtilitiesQualificationSystem:
                    return EuUtilitiesProcurements2014Directive;
                case NoticeType.DesignContest:
                case NoticeType.DesignContestResults:
                    return notice.Project.ProcurementCategory == ProcurementCategory.Public ? EuPublicProcurements2014Directive : EuUtilitiesProcurements2014Directive;
                case NoticeType.SocialPriorInformation:
                case NoticeType.SocialContract:
                case NoticeType.SocialContractAward:
                    return EuPublicProcurements2014Directive;
                case NoticeType.SocialConcessionPriorInformation:
                case NoticeType.SocialConcessionAward:
                case NoticeType.Concession:
                case NoticeType.ConcessionAward:
                    return EuConcessionProcurement2014Directive;
                case NoticeType.DefenceSimplifiedContract:
                case NoticeType.DefenceConcession:
                case NoticeType.DefenceContractConcessionnaire:
                case NoticeType.DefencePriorInformation:
                case NoticeType.DefenceContract:
                case NoticeType.DefenceContractAward:
                case NoticeType.DefenceContractSub:
                    return EuDefenceProcurements2009Directive;
                case NoticeType.ExAnte:
                    if (notice.Project.ProcurementCategory == ProcurementCategory.Defence)
                    {
                        return EuDefenceProcurements2009Directive;
                    } else if (notice.Project.ProcurementCategory == ProcurementCategory.Utility)
                    {
                        return EuUtilitiesProcurements2014Directive;
                    }
                    else if (notice.Project.ProcurementCategory == ProcurementCategory.Lisence)
                    {
                        return EuConcessionProcurement2014Directive;
                    }
                    else if (notice.Project.ProcurementCategory == ProcurementCategory.Public)
                    {
                        return EuPublicProcurements2014Directive;
                    }
                    return null;
                case NoticeType.Modification:
                    if (parent != null && notice.ParentId != null)
                    {
                        if( !string.IsNullOrEmpty(parent.LegalBasis))
                        {
                            return parent.LegalBasis;
                        }
                        return GetDirectiveByProcurementCategory(parent);
                    }
                    else
                    {
                        return GetDirectiveByProcurementCategory(notice);
                    }
                case NoticeType.BuyerProfile:   // Killed with holy fire
                case NoticeType.DpsAward:
                    // Copied from Contract award and contract award utilities based on procurement category
                    if (notice.Project.ProcurementCategory == ProcurementCategory.Public)
                    {
                        return notice.Project.Organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeEu
                            ? EuEuratom2018Directive
                            : EuPublicProcurements2014Directive;
                    }
                    else { 
                        return EuUtilitiesProcurements2014Directive;
                    }
                default:
                    return null;
            }
        }

        public static string GetDirectiveByProcurementCategory(NoticeContract parent)
        {
            switch (parent.Project.ProcurementCategory)
            {
                case ProcurementCategory.Defence:
                    return EuDefenceProcurements2009Directive;
                case ProcurementCategory.Lisence:
                    return EuConcessionProcurement2014Directive;
                case ProcurementCategory.Public:
                    return EuPublicProcurements2014Directive;
                case ProcurementCategory.Utility:
                    return EuUtilitiesProcurements2014Directive;
                default:
                    return EuPublicProcurements2014Directive;
            }
        }
    }
}
