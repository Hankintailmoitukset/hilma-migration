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
        public static string ToTEDFormat(this PreviousContractType type)
        {
            switch (type)
            {
                case PreviousContractType.BuyerProfile:
                    return "NOTICE_BUYER_PROFILE";
                case PreviousContractType.PriorInformation:
                    return "PRIOR_INFORMATION_NOTICE";
                default:
                    return "";
            }
        }

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

        public static string ToTEDChangeFormat(this LotsSubmittedFor type)
        {
            switch (type)
            {
                case LotsSubmittedFor.LotsAll:
                    return "lots_all";
                case LotsSubmittedFor.LotsMax:
                    return "lots_max";
                case LotsSubmittedFor.LotOneOnly:
                    return "lot_one_only";
                default:
                    return "";
            }
        }
        public static string ToTEDChangeFormat(this AwardCriterionType type)
        {
            switch (type)
            {
                case AwardCriterionType.QualityCriterion:
                    return "quality_criterion";
                case AwardCriterionType.CostCriterion:
                    return "cost_criterion";
                case AwardCriterionType.CostAndQualityCriteria:
                    return "cost_and_quality_criteria";
                case AwardCriterionType.PriceCriterion:
                    return "price_criterion";
                case AwardCriterionType.PriceAndQualityCriteria:
                    return "price_and_quality_criteria";
                case AwardCriterionType.DescriptiveCriteria:
                    return "descriptive_criteria";
                case AwardCriterionType.AwardCriteriaDescrBelow:
                    return "award_criteria_descr_below";
                case AwardCriterionType.AwardCriteriaInDocs:
                    return "award_criteria_in_docs";
                default:
                    return "";
            }
        }
        public static string ToTEDChangeFormat(this ProcurementDocumentAvailability type)
        {
            switch (type)
            {
                case ProcurementDocumentAvailability.AddressObtainDocs:
                    return "address_obtain_docs";
                case ProcurementDocumentAvailability.DocsRestricted:
                    return "docs_restricted";
                case ProcurementDocumentAvailability.OrganisationAddress:
                    return "organisation_address";
                case ProcurementDocumentAvailability.OtherAddress:
                    return "other_address";
                default:
                    return "";
            }
        }
        public static string ToTEDChangeFormat(this AdditionalInformationAvailability type)
        {
            switch (type)
            {
                case AdditionalInformationAvailability.AddressToAbove:
                    return "address_to_above";
                case AdditionalInformationAvailability.AddressAnother:
                    return "address_another";
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
        public static string ToTEDChangeFormat(this ContractType type)
        {
            switch (type)
            {
                case ContractType.Supplies:
                    return "supplies";
                case ContractType.SocialServices:
                case ContractType.Services:
                    return "services";
                case ContractType.Works:
                    return "works";
                default:
                    return "";
            }
        }
        public static string ToTEDChangeFormat(this ContractingAuthorityType type)
        {
            switch (type)
            {
                case ContractingAuthorityType.MaintypeMinistry:
                    return "maintype_ministry";
                case ContractingAuthorityType.MaintypeNatagency:
                    return "maintype_natagency";
                case ContractingAuthorityType.MaintypeLocalauth:
                    return "maintype_localauth";
                case ContractingAuthorityType.MaintypeLocalagency:
                    return "maintype_localagency";
                case ContractingAuthorityType.MaintypePublicbody:
                    return "maintype_publicbody";
                case ContractingAuthorityType.MaintypeEu:
                    return "maintype_eu";
                case ContractingAuthorityType.OtherType:
                    return "other_type";
                default:
                    return "";
            }
        }

        public static string ToTEDChangeFormat(this MainActivity activity)
        {
            switch (activity)
            {
                case MainActivity.MainactivGeneral:
                    return "mainactiv_general";
                case MainActivity.MainactivDefence:
                    return "mainactiv_defence";
                case MainActivity.MainactivEconomic:
                    return "mainactiv_economic";
                case MainActivity.MainactivEducation:
                    return "mainactiv_education";
                case MainActivity.MainactivEnvironment:
                    return "mainactiv_environment";
                case MainActivity.MainactivHealth:
                    return "mainactiv_health";
                case MainActivity.MainactivHousing:
                    return "mainactiv_housing";
                case MainActivity.MainactivSafety:
                    return "mainactiv_safety";
                case MainActivity.MainactivCulture:
                    return "mainactiv_culture";
                case MainActivity.MainactivSocial:
                    return "mainactiv_social";
                case MainActivity.OtherActivity:
                    return "other_activity";
                default:
                    return "";
            }
        }
        public static string ToTEDChangeFormat(this MainActivityUtilities activity)
        {
            switch (activity)
            {
                case MainActivityUtilities.MainactivGasProduct:
                    return "mainactiv_gas_product";
                case MainActivityUtilities.MainactivElectricity:
                    return "mainactiv_electricity";
                case MainActivityUtilities.MainactivGasExplor:
                    return "mainactiv_gas_explor";
                case MainActivityUtilities.MainactivCoal:
                    return "mainactiv_coal";
                case MainActivityUtilities.MainactivWater:
                    return "mainactiv_water";
                case MainActivityUtilities.MainactivPostal:
                    return "mainactiv_postal";
                case MainActivityUtilities.MainactivRailway:
                    return "mainactiv_railway";
                case MainActivityUtilities.MainactivBus:
                    return "mainactiv_bus";
                case MainActivityUtilities.MainactivPort:
                    return "mainactiv_port";
                case MainActivityUtilities.MainactivAirportrelated:
                    return "mainactiv_airportrelated";
                case MainActivityUtilities.OtherActivity:
                    return "other_activity";
                default:
                    return "";
            }
        }

        public static string ToTEDChangeFormat(this ModificationReason reason)
        {
            switch (reason)
            {
                case ModificationReason.ModNeedByCircums:
                    return "mod_need_by_circums";
                case ModificationReason.ModNeedForAdditional:
                    return "mod_need_for_additional";
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
        public static string ToTEDChangeFormat(this ProcurementFailureReason type)
        {
            switch (type)
            {
                case ProcurementFailureReason.AwardDiscontinued:
                    return "award_discontinued";
                case ProcurementFailureReason.AwardNoTenders:
                    return "award_no_tenders";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Reason/Justification for advantageous purchase on F15.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToTedChangeFormat(this AdvantageousPurchaseReason type)
        {
            switch (type)
            {
                case AdvantageousPurchaseReason.DFromWindingSupplier:
                    return "d_from_winding_provider";
                case AdvantageousPurchaseReason.DFromReceivers:
                    return "d_from_liquidator_creditor";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Enumeration for annex reasons for no competition.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToTEDChangeFormat(this ReasonForNoCompetition type)
        {
            switch (type)
            {
                case ReasonForNoCompetition.DTechnical:
                    return "d_technical";
                case ReasonForNoCompetition.DArtistic:
                    return "d_artistic";
                case ReasonForNoCompetition.DExistenceExclusive:
                    return "d_exclusive_rights";
                case ReasonForNoCompetition.DProtectRights:
                    return "d_protect_rights";
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
