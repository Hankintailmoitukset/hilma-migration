using System;
using System.Text;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Exceptions;

namespace Hilma.Domain.Integrations.Extensions
{
    public static class EnumExtensions
    {
        public static string ToTEDFormat(this ContractingAuthorityType type)
        {
            switch (type)
            {
                case ContractingAuthorityType.MaintypeMinistry:
                    return "MINISTRY";
                case ContractingAuthorityType.MaintypeNatagency:
                    return "NATIONAL_AGENCY";
                case ContractingAuthorityType.MaintypeLocalauth:
                    return "REGIONAL_AUTHORITY";
                case ContractingAuthorityType.MaintypeLocalagency:
                    return "REGIONAL_AGENCY";
                case ContractingAuthorityType.MaintypePublicbody:
                    return "BODY_PUBLIC";
                case ContractingAuthorityType.MaintypeEu:
                    return "EU_INSTITUTION";
                default:
                    return "";
            }
        }

        public static string ToTEDFormat(this MainActivity activity)
        {
            switch (activity)
            {
                case MainActivity.MainactivGeneral:
                    return "GENERAL_PUBLIC_SERVICES";
                case MainActivity.MainactivDefence:
                    return "DEFENCE";
                case MainActivity.MainactivEconomic:
                    return "ECONOMIC_AND_FINANCIAL_AFFAIRS";
                case MainActivity.MainactivEducation:
                    return "EDUCATION";
                case MainActivity.MainactivEnvironment:
                    return "ENVIRONMENT";
                case MainActivity.MainactivHealth:
                    return "HEALTH";
                case MainActivity.MainactivHousing:
                    return "HOUSING_AND_COMMUNITY_AMENITIES";
                case MainActivity.MainactivSafety:
                    return "PUBLIC_ORDER_AND_SAFETY";
                case MainActivity.MainactivCulture:
                    return "RECREATION_CULTURE_AND_RELIGION";
                case MainActivity.MainactivSocial:
                    return "SOCIAL_PROTECTION";
                default:
                    return "";
            }
        }

        public static string ToTEDFormat(this MainActivityUtilities activity)
        {
            switch (activity)
            {
                case MainActivityUtilities.MainactivGasProduct:
                    return "PRODUCTION_TRANSPORT_DISTRIBUTION_GAS_HEAT";
                case MainActivityUtilities.MainactivElectricity:
                    return "ELECTRICITY";
                case MainActivityUtilities.MainactivGasExplor:
                    return "EXPLORATION_EXTRACTION_GAS_OIL";
                case MainActivityUtilities.MainactivCoal:
                    return "EXPLORATION_EXTRACTION_COAL_OTHER_SOLID_FUEL";
                case MainActivityUtilities.MainactivWater:
                    return "WATER";
                case MainActivityUtilities.MainactivPostal:
                    return "POSTAL_SERVICES";
                case MainActivityUtilities.MainactivRailway:
                    return "RAILWAY_SERVICES";
                case MainActivityUtilities.MainactivBus:
                    return "URBAN_RAILWAY_TRAMWAY_TROLLEYBUS_BUS_SERVICES";
                case MainActivityUtilities.MainactivPort:
                    return "PORT_RELATED_ACTIVITIES";
                case MainActivityUtilities.MainactivAirportrelated:
                    return "AIRPORT_RELATED_ACTIVITIES";
                default:
                    return "";
            }
        }

        public static string ToTEDFormat(this ContractType type, ProcurementCategory category = ProcurementCategory.Undefined)
        {
            switch (type)
            {
                case ContractType.Supplies:
                    if (category == ProcurementCategory.Lisence)
                    {
                        throw new HilmaException("License legal basis does not support supplies contract type!");
                    }
                    return "SUPPLIES";
                case ContractType.SocialServices:
                case ContractType.Services:
                    return "SERVICES";
                case ContractType.Works:
                    return "WORKS";
                default:
                    return "";
            }
        }
        public static string ToTEDFormat(this Supplies type)
        {
            switch (type)
            {
                case Supplies.Combination:
                    return "COMBINATION_THESE";
                case Supplies.HirePurchase:
                    return "HIRE_PURCHASE";
                case Supplies.Lease:
                    return "LEASE";
                case Supplies.Purchase:
                    return "PURCHASE";
                case Supplies.Rental:
                    return "RENTAL";
                default:
                    return "";
            }
        }

        public static string ToTEDFormat(this ProcedureType type)
        {
            switch (type)
            {
                case ProcedureType.ProctypeOpen:
                    return "PT_OPEN";
                case ProcedureType.ProctypeRestricted:
                    return "PT_RESTRICTED";
                case ProcedureType.ProctypeCompNegotiation:
                    return "PT_COMPETITIVE_NEGOTIATION";
                case ProcedureType.ProctypeCompDialogue:
                    return "PT_COMPETITIVE_DIALOGUE";
                case ProcedureType.ProctypeInnovation:
                    return "PT_INNOVATION_PARTNERSHIP";
                case ProcedureType.ProctypeNegotiationsInvolved:
                    return "PT_INVOLVING_NEGOTIATION";
                case ProcedureType.ProctypeNegotiation:
                    return "PT_NEGOTIATED_CHOICE";
                case ProcedureType.ProctypeNegotiatedWoPub:
                case ProcedureType.ProctypeConcessionWoPub:
                case ProcedureType.ProctypeAwardWoCall:
                case ProcedureType.ProctypeNegotiatedWoNotice:
                    return "PT_NEGOTIATED_WITHOUT_PUBLICATION";
                case ProcedureType.AwardWoPriorPubD1:
                case ProcedureType.AwardWoPriorPubD4:
                case ProcedureType.AwardWoPriorPubD1Other:
                    return "PT_AWARD_CONTRACT_WITHOUT_CALL";
                default:
                    return "";
            }
        }


        public static string ToTEDChangeFormat(this TenderSendOptions type)
        {
            switch (type)
            {
                case TenderSendOptions.AddressSendTenders:
                    return "address_send_tenders";
                case TenderSendOptions.AddressOrganisation:
                    return "address_to_above";
                case TenderSendOptions.AddressFollowing:
                    return "address_following";
                case TenderSendOptions.EmailSendTenders:
                    return "email_send_tenders";
                default:
                    return "";
            }
        }

        public static string ToTEDChangeFormat(this ContractAwarded type)
        {
            switch (type)
            {
                case ContractAwarded.AwardedContract:
                    return "yes";
                case ContractAwarded.NoAwardedContract:
                    return "no";
                default:
                    return "";
            }
        }

        /// <summary>
        ///     Generic version of all the crap above, only use if the enum names reflect the exact TED names
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTedChangeFormatGeneric<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var sb = new StringBuilder();
            var charArray = value.ToString().ToCharArray();

            for (var i = 0; i < charArray.Length; i++)
            {
                var c = charArray[i];
                if (char.IsUpper(c) && i > 0)
                {
                    sb.Append("_");
                }

                sb.Append(c);
            }

            return sb.ToString().ToLowerInvariant();
        }
    }
}
