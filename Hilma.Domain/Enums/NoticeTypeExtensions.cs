using System.Linq;
using Hilma.Domain.DataContracts;

namespace Hilma.Domain.Enums
{
    public static class NoticeTypeExtensions {
        private static readonly NoticeTypes _types;

        static NoticeTypeExtensions()
        {
            _types = new NoticeTypes();
        }

        public static bool IsContract(this NoticeType type)
        {
            return _types.ContractNotices.Contains(type);
        }
        public static bool IsPriorInformation(this NoticeType type)
        {
            return _types.PriorInformationNotices.Contains(type);
        }

        public static bool IsContractAward(this NoticeType type)
        {
            return _types.ContractAwardNotices.Contains(type);
        }

        public static bool IsDefence(this NoticeType type)
        {
            return _types.DefenceNotices.Contains(type);
        }

        public static bool IsSocial(this NoticeType type)
        {
            return _types.SocialNotices.Contains(type);
        }

        public static bool IsUtilities(this NoticeType type)
        {
            return _types.UtilitiesNotices.Contains(type);
        }

        public static bool IsNational(this NoticeType type)
        {
            return _types.NationalNotices.Contains(type);
        }
    }


}
