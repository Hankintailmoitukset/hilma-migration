using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Entities.Annexes;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Configuration;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hilma.Domain.Integrations.Defence
{
    public class AnnexHelper
    {

        private readonly NoticeContract _notice;
        private readonly AnnexConfiguration _configuration;

        /// <summary>
        /// Public constructor that sets the notice and configuration.
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="configuration"></param>
        public AnnexHelper(NoticeContract notice, AnnexConfiguration configuration)
        {
            _notice = notice;
            _configuration = configuration;
        }


        public IEnumerable<XElement> SelectAnnexD()
        {
            if (_notice.Type == NoticeType.DefenceContractAward &&
                _notice.Project.ProcurementCategory == ProcurementCategory.Defence)
            {
                // Annex D3
                return AnnexD3Defence();
            }

            return null;
        }

        private IEnumerable<XElement> AnnexD3Defence()
        {
            var annex = _notice?.Annexes?.D3 ?? throw new NullReferenceException("Annex D3 not set!");

            // AD3.1.1-2
            if (annex.NoTenders)
            {
                string value;
                switch (annex.ProcedureType)
                {
                    case AnnexProcedureType.DProcNegotiatedPriorCallCompetition:
                        value = "NEGOTIATED_PROCEDURE_COMPETITION";
                        break;
                    case AnnexProcedureType.DProcRestricted:
                        value = "RESTRICTED_PROCEDURE";
                        break;
                    case AnnexProcedureType.DProcCompetitiveDialogue:
                        value = "COMPETITIVE_DIALOGUE";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                yield return TedHelpers.Element("NO_OPEN_RESTRICTED",
                    new XAttribute("VALUE", value));
            }

            if (annex.AllTenders)
            {
                yield return TedHelpers.Element("ONLY_IRREGULAR_INACCEPTABLE_TENDERERS");
            }
            else
            {
                yield return TedHelpers.Element("NO_ONLY_IRREGULAR_INACCEPTABLE_TENDERERS");
            }

            if (_notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1)
            {
                yield return TedHelpers.Element("JUSTIFICATION_CHOICE_NEGOCIATED_PROCEDURE",
                    JUSTIFICATION_C(annex));
            }
            else if (_notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1Other)
            {
                yield return TedHelpers.Element("OTHER_JUSTIFICATION",
                    OTHER_JUSTIFICATION(annex));
            }

            yield return TedHelpers.PElement("REASON_CONTRACT_LAWFUL", annex.Justification);

        }

        private IEnumerable<XElement> JUSTIFICATION_C(AnnexD3 annex)
        {
            if (annex.CrisisUrgency)
            {
                yield return TedHelpers.Element("PERIOD_FOR_PROCEDURE_INCOMPATIBLE_WITH_CRISIS");
            }
            else
            {
                yield return TedHelpers.Element("NO_PERIOD_FOR_PROCEDURE_INCOMPATIBLE_WITH_CRISIS");
            }

            if (annex.ExtremeUrgency)
            {
                yield return TedHelpers.Element("EXTREME_URGENCY_EVENTS_UNFORESEEABLE");
            }
            else
            {
                yield return TedHelpers.Element("NO_EXTREME_URGENCY_EVENTS_UNFORESEEABLE");
            }

            if (annex.ProvidedByOnlyParticularOperator)
            {
                switch (annex.ReasonForNoCompetition)
                {
                    case ReasonForNoCompetition.DTechnical:
                        yield return TedHelpers.Element("REASONS_PROVIDED_PARTICULAR_TENDERER",
                            TedHelpers.Element("REASONS_PROVIDED_PARTICULAR_TENDERER_TECHNICAL"));
                        break;
                    case ReasonForNoCompetition.DExistenceExclusive:
                        yield return TedHelpers.Element("REASONS_PROVIDED_PARTICULAR_TENDERER",
                            TedHelpers.Element("REASONS_PROVIDED_PARTICULAR_TENDERER_EXCLUSIVE_RIGHTS"));
                        break;
                }
            }

            if (_notice.Project.ContractType == ContractType.Supplies ||
                _notice.Project.ContractType == ContractType.Services)
            {
                if (annex.OtherServices)
                {
                    yield return TedHelpers.Element("CONTRACT_RESEARCH_DIRECTIVE");
                }
                else
                {
                    yield return TedHelpers.Element("NO_CONTRACT_RESEARCH_DIRECTIVE");
                }

                if (annex.ProductsManufacturedForResearch)
                {
                    yield return TedHelpers.Element("MANUFACTURED_BY_DIRECTIVE");
                }
                else
                {
                    yield return TedHelpers.Element("NO_MANUFACTURED_BY_DIRECTIVE");
                }
            }

            if (annex.AdditionalDeliveries)
            {
                yield return TedHelpers.Element("ADDITIONAL_WORKS");
            }
            else
            {
                yield return TedHelpers.Element("NO_ADDITIONAL_WORKS");
            }

            if (annex.CommodityMarket)
            {
                yield return TedHelpers.Element("SUPPLIES_QUOTED_PURCHASED_COMMODITY_MARKET");
            }
            else
            {
                yield return TedHelpers.Element("NO_SUPPLIES_QUOTED_PURCHASED_COMMODITY_MARKET");
            }

            if (annex.AdvantageousTerms)
            {
                var reasonElement = annex.AdvantageousPurchaseReason == AdvantageousPurchaseReason.DFromReceivers
                    ? TedHelpers.Element("RECEIVERS_ARRANGEMENT_CREDITORS")
                    : TedHelpers.Element("SUPPLIER_WINDING_UP_BUSINESS");
                yield return TedHelpers.Element("PURCHASE_SUPPLIES_ADVANTAGEOUS_TERMS", reasonElement);
            }

            if (_notice.Project.ContractType == ContractType.Works ||
                _notice.Project.ContractType == ContractType.Services)
            {
                if (annex.RepetitionExisting)
                {
                    yield return TedHelpers.Element("WORKS_REPETITION_EXISTING_WORKS");
                }
                else
                {
                    yield return TedHelpers.Element("NO_WORKS_REPETITION_EXISTING_WORKS");
                }
            }

            if (annex.MaritimeService)
            {
                yield return TedHelpers.Element("AIR_MARITIME_TRANSPORT_FOR_ARMED_FORCES_DEPLOYMENT");
            }
            else
            {
                yield return TedHelpers.Element("NO_AIR_MARITIME_TRANSPORT_FOR_ARMED_FORCES_DEPLOYMENT");
            }
        }

        private IEnumerable<XElement> OTHER_JUSTIFICATION(AnnexD3 annex)
        {
            switch (annex.OtherJustification)
            {
                case D3OtherJustificationOptions.ContractServicesListedInDirective:
                    yield return TedHelpers.Element("CONTRACT_SERVICES_LISTED_IN_DIRECTIVE");
                    break;
                case D3OtherJustificationOptions.ContractServicesOutsideDirective:
                    yield return TedHelpers.Element("CONTRACT_SERVICES_OUTSIDE_DIRECTIVE");
                    break;
                default:
                    yield break;
            }
        }
    }
}
