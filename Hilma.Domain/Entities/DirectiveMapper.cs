using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Integrations.General
{
    public static class DirectiveMapper
    {
        public static string GetDirective(NoticeContract notice, NoticeContract parent)
        {
            switch (notice.Type)
            {
                case NoticeType.PriorInformation:
                case NoticeType.PriorInformationReduceTimeLimits:
                case NoticeType.Contract:
                case NoticeType.ContractAward:
                    return notice.Project.Organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeEu
                        || notice.Project.Organisation.ContractingAuthorityType == ContractingAuthorityType.OtherType ? "32018R1046" : "32014L0024";
                case NoticeType.PeriodicIndicativeUtilities:
                case NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits:
                case NoticeType.ContractUtilities:
                case NoticeType.ContractAwardUtilities:
                case NoticeType.QualificationSystemUtilities:
                case NoticeType.SocialUtilities:
                case NoticeType.SocialUtilitiesPriorInformation:
                case NoticeType.SocialUtilitiesContractAward:
                case NoticeType.SocialUtilitiesQualificationSystem:
                    return "32014L0025";
                case NoticeType.DesignContest:
                case NoticeType.DesignContestResults:
                    return notice.Project.ProcurementCategory == ProcurementCategory.Public ? "32014L0024" : "32014L0025";
                case NoticeType.SocialPriorInformation:
                case NoticeType.SocialContract:
                case NoticeType.SocialContractAward:
                    return "32014L0024";
                case NoticeType.SocialConcessions:
                case NoticeType.Concession:
                case NoticeType.ConcessionAward:
                    return "32014L0023";
                case NoticeType.DefenceSimplifiedContract:
                case NoticeType.DefenceConcession:
                case NoticeType.DefenceContractConcessionnaire:
                case NoticeType.DefencePriorInformation:
                case NoticeType.DefenceContract:
                case NoticeType.DefenceContractAward:
                case NoticeType.DefenceContractSub:
                    return "32009L0081";
                case NoticeType.ExAnte:
                    if (notice.Project.ProcurementCategory == ProcurementCategory.Defence)
                    {
                        return "32009L0081";
                    } else if (notice.Project.ProcurementCategory == ProcurementCategory.Utility)
                    {
                        return "32014L0025";
                    }
                    else if (notice.Project.ProcurementCategory == ProcurementCategory.Lisence)
                    {
                        return "32014L0023";
                    }
                    else if (notice.Project.ProcurementCategory == ProcurementCategory.Public)
                    {
                        return "32014L0024";
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
                    return notice.Project.ProcurementCategory == ProcurementCategory.Public ?
                    notice.Project.Organisation.ContractingAuthorityType == ContractingAuthorityType.MaintypeEu
                        || notice.Project.Organisation.ContractingAuthorityType == ContractingAuthorityType.OtherType ? "32018R1046" : "32014L0024"
                    :
                    "32014L0025";
                default:
                    return null;
            }
        }

        public static string GetDirectiveByProcurementCategory(NoticeContract parent)
        {
            switch (parent.Project.ProcurementCategory)
            {
                case ProcurementCategory.Defence:
                    return "32009L0081";
                case ProcurementCategory.Lisence:
                    return "32014L0023";
                case ProcurementCategory.Public:
                    return "32014L0024";
                case ProcurementCategory.Utility:
                    return "32014L0025";
                default:
                    return "32014L0024";
            }
        }
    }
}
