using Hilma.Domain.Enums;

namespace Hilma.Domain.Integrations.HilmaMigration
{
    public class NoticeTypeParser
    {
        public static NoticeType ParseNoticeType(INoticeImportModel editaNotice, out bool isCorrigendum, out bool isCancelled)
        {
            isCorrigendum = false;
            isCancelled = false;
            NoticeType noticeType;
            switch (editaNotice.FormNumber)
            {
                case "1":
                    if (editaNotice.NoticeType == "PRI_REDUCING_TIME_LIMITS".ToLower())
                    {
                        noticeType = NoticeType.PriorInformationReduceTimeLimits;
                    }
                    else
                    {
                        noticeType = NoticeType.PriorInformation;
                    }
                    break;
                case "2":
                    noticeType = NoticeType.Contract;
                    break;
                case "3":
                    noticeType = NoticeType.ContractAward;
                    break;
                case "5":
                    noticeType = NoticeType.ContractUtilities;
                    break;
                case "6":
                    noticeType = NoticeType.ContractAwardUtilities;
                    break;
                case "14":
                    noticeType = NoticeType.Undefined;
                    isCorrigendum = true;
                    break;
                case "15":
                    noticeType = NoticeType.ExAnte;
                    break;
                case "17":
                    noticeType = NoticeType.DefenceContract;
                    break;
                case "18":
                    noticeType = NoticeType.DefenceContractAward;
                    break;
                case "20":
                    noticeType = NoticeType.Modification;
                    break;
                case "24":
                    noticeType = NoticeType.Concession;
                    break;
                case "25":
                    noticeType = NoticeType.ConcessionAward;
                    break;
                case "99":
                    switch (editaNotice.NoticeType)
                    {
                        case "domestic_contract":
                            noticeType = NoticeType.NationalContract;
                            break;
                        case "request_for_information":
                            noticeType = NoticeType.NationalPriorInformation;
                            break;
                        case "procurement_discontinued":
                            noticeType = NoticeType.NationalContract;
                            isCancelled = true;
                            break;
                        case "corrigendum_notice":
                            noticeType = NoticeType.NationalContract;
                            isCorrigendum = true;
                            break;
                        default:
                            noticeType = NoticeType.NationalContract;
                            break;
                    }
                    break;
                case "92":
                    noticeType = NoticeType.NationalTransparency;
                    break;
                case "93":
                    noticeType = NoticeType.NationalDirectAward;
                    break;
                case "21":
                    switch (editaNotice.NoticeType)
                    {
                        case "contract":
                            noticeType = NoticeType.SocialContract;
                            break;
                        case "award_contract":
                            noticeType = NoticeType.SocialContractAward;
                            break;
                        case "pri_only":
                            noticeType = NoticeType.SocialPriorInformation;
                            break;
                        default:
                            noticeType = NoticeType.Undefined;
                            break;
                    }
                    break;
                default:
                    noticeType = NoticeType.Undefined;
                    break;
            }
            return noticeType;
        }
    }
}
