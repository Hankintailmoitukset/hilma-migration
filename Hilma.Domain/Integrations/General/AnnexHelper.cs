using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Entities.Annexes;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Configuration;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hilma.Domain.Integrations.General
{
    public class AnnexHelper
    {

        private readonly NoticeContract _notice;
        private readonly AnnexConfiguration _configuration;

    /// <summary>
    /// Public constructor that sets the notice and configuration.
    /// </summary>
    /// <param name="notice"></param>
    /// <param name="configuration">Annex field configuration</param>
    public AnnexHelper(NoticeContract notice, AnnexConfiguration configuration)
        {
            _notice = notice;
            _configuration = configuration;
        }


        public XElement SelectAnnexD()
        {
            if (_notice.Type == NoticeType.ExAnte)
            {
                if (_notice.Project.ProcurementCategory == ProcurementCategory.Defence)
                {
                    // Annex D3
                    return WrapAnnex(AnnexD3());
                }
                else if (_notice.Project.ProcurementCategory == ProcurementCategory.Lisence)
                {
                    // Annex D4
                    return WrapAnnex(AnnexD4(),
                        ProcedureType.AwardWoPriorPubD4, "PT_AWARD_CONTRACT_WITHOUT_PUBLICATION");
                }
                else if (_notice.Project.ProcurementCategory == ProcurementCategory.Utility)
                {
                    // Annex D2
                    return WrapAnnex(AnnexD2());
                }

                // Annex D1
                return WrapAnnex(AnnexD1());
            }

            if (_notice.Type == NoticeType.SocialContractAward &&
                (_notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1 ||
                _notice.ProcedureInformation.ProcedureType == ProcedureType.AwardWoPriorPubD1Other))
            {
                // Annex D1
                return WrapAnnex(AnnexD1(), ProcedureType.AwardWoPriorPubD1, "PT_AWARD_CONTRACT_WITHOUT_CALL", ProcedureType.AwardWoPriorPubD1Other, "PT_AWARD_CONTRACT_WITHOUT_CALL");
            }

            return null;
        }

        private XElement WrapAnnex(IEnumerable<XElement> annex,
            ProcedureType annexType = ProcedureType.AwardWoPriorPubD1,
            string annexElement = "PT_NEGOTIATED_WITHOUT_PUBLICATION",
            ProcedureType outOfScopeType = ProcedureType.ProctypeNegotiatedWoNotice,
            string outOfScopeElement = "PT_AWARD_CONTRACT_WITHOUT_CALL"
            )
        {
            if (_notice.ProcedureInformation.ProcedureType == annexType)
            {
                return TedHelpers.Element(annexElement,
                    TedHelpers.Element("D_ACCORDANCE_ARTICLE", annex),
                    TedHelpers.PElement("D_JUSTIFICATION", SelectJustification().Justification));
            }
            else if (_notice.ProcedureInformation.ProcedureType == outOfScopeType)
            {
                return TedHelpers.Element(outOfScopeElement,
                    TedHelpers.Element("D_OUTSIDE_SCOPE"),
                    TedHelpers.PElement("D_JUSTIFICATION", SelectJustification().Justification));
            }

            throw new ArgumentOutOfRangeException();
        }

        private IJustifiable SelectJustification()
        {
            if (_notice.Project.ProcurementCategory == ProcurementCategory.Defence)
            {
                return _notice.Annexes.D3;
            }
            else if (_notice.Project.ProcurementCategory == ProcurementCategory.Lisence)
            {
                return _notice.Annexes.D4;
            }
            else if (_notice.Project.ProcurementCategory == ProcurementCategory.Utility)
            {
                return _notice.Annexes.D2;
            }

            // Annex D1
            return _notice.Annexes.D1;
        }

        private IEnumerable<XElement> AnnexD1()
        {
            var annex = _notice?.Annexes?.D1 ?? throw new NullReferenceException("Annex D1 not set!");
            var conf = _configuration.D1;

            // AD1.1.1-2
            if (annex.NoTenders && conf.NoTenders)
            {
                switch (annex.ProcedureType)
                {
                    case AnnexProcedureType.DProcOpen:
                        yield return TedHelpers.Element("D_PROC_OPEN");
                        break;
                    case AnnexProcedureType.DProcRestricted:
                        yield return TedHelpers.Element("D_PROC_RESTRICTED");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // AD1.1.3
            if (_notice.Project.ContractType == ContractType.Supplies &&
                annex.SuppliesManufacturedForResearch &&
                conf.SuppliesManufacturedForResearch)
            {
                yield return TedHelpers.Element("D_MANUF_FOR_RESEARCH", new XAttribute("CTYPE", "SUPPLIES"));
            }

            // AD1.1.4-5
            if (annex.ProvidedByOnlyParticularOperator && conf.ProvidedByOnlyParticularOperator)
            {
                switch (annex.ReasonForNoCompetition)
                {
                    case ReasonForNoCompetition.DTechnical:
                        yield return TedHelpers.Element("D_TECHNICAL");
                        break;
                    case ReasonForNoCompetition.DArtistic:
                        yield return TedHelpers.Element("D_ARTISTIC");
                        break;
                    case ReasonForNoCompetition.DProtectRights:
                        yield return TedHelpers.Element("D_PROTECT_RIGHTS");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // AD1.1.6
            if (annex.ExtremeUrgency && conf.ExtremeUrgency)
            {
                yield return TedHelpers.Element("D_EXTREME_URGENCY");
            }


            // AD1.1.7
            if (annex.AdditionalDeliveries && conf.AdditionalDeliveries)
            {
                yield return TedHelpers.Element("D_ADD_DELIVERIES_ORDERED");
            }

            // AD1.1.8
            yield return RepetitionWorksOrServices(annex.RepetitionExisting && conf.RepetitionExisting);
            // AD1.1.9
            yield return DesignContestServiceElement(annex.DesignContestAward && conf.DesignContestAward);
            // AD1.1.10
            yield return CommodityMarketElement(annex.CommodityMarket && conf.CommodityMarket);
            // AD1.1.11-12
            yield return AdvantageousTermsElement(annex.AdvantageousTerms && conf.AdvantageousTerms, annex.AdvantageousPurchaseReason);
        }


        private IEnumerable<XElement> AnnexD2()
        {
            var annex = _notice?.Annexes?.D2 ?? throw new NullReferenceException("Annex D2 not set!");

            // AD2.1.1
            if (annex.NoTenders)
            {
                yield return TedHelpers.Element("D_NO_TENDERS_REQUESTS");
            }

            // AD2.1.2
            if (annex.PureResearch)
            {
                yield return TedHelpers.Element("D_PURE_RESEARCH");
            }

            // AD2.1.3-4
            if (annex.ProvidedByOnlyParticularOperator)
            {
                switch (annex.ReasonForNoCompetition)
                {
                    case ReasonForNoCompetition.DTechnical:
                        yield return TedHelpers.Element("D_TECHNICAL");
                        break;
                    case ReasonForNoCompetition.DArtistic:
                        yield return TedHelpers.Element("D_ARTISTIC");
                        break;
                    case ReasonForNoCompetition.DProtectRights:
                        yield return TedHelpers.Element("D_PROTECT_RIGHTS");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // AD2.1.5
            if (annex.ExtremeUrgency)
            {
                yield return TedHelpers.Element("D_EXTREME_URGENCY");
            }

            // AD2.1.6
            if (annex.AdditionalDeliveries)
            {
                yield return TedHelpers.Element("D_ADD_DELIVERIES_ORDERED");
            }

            // AD2.1.7
            yield return RepetitionWorksOrServices(annex.RepetitionExisting);
            // AD2.1.8
            yield return DesignContestServiceElement(annex.DesignContestAward);
            // AD2.1.9
            yield return CommodityMarketElement(annex.CommodityMarket);
            // AD2.1.10-11
            yield return AdvantageousTermsElement(annex.AdvantageousTerms, annex.AdvantageousPurchaseReason);

            // AD2.1.12
            if (annex.BargainPurchase)
            {
                yield return TedHelpers.Element("D_BARGAIN_PURCHASE");
            }
        }

        private IEnumerable<XElement> AnnexD3()
        {
            var annex = _notice?.Annexes?.D3 ?? throw new NullReferenceException("Annex D3 not set!");

            // AD3.1.1-2
            if (annex.NoTenders)
            {
                switch (annex.ProcedureType)
                {
                    case AnnexProcedureType.DProcNegotiatedPriorCallCompetition:
                        yield return TedHelpers.Element("D_PROC_NEGOTIATED_PRIOR_CALL_COMPETITION");
                        break;
                    case AnnexProcedureType.DProcRestricted:
                        yield return TedHelpers.Element("D_PROC_RESTRICTED");
                        break;
                    case AnnexProcedureType.DProcCompetitiveDialogue:
                        yield return TedHelpers.Element("D_PROC_COMPETITIVE_DIALOGUE");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (_notice.Project.ContractType == ContractType.Supplies ||
                 _notice.Project.ContractType == ContractType.Services ||
                 _notice.Project.ContractType == ContractType.SocialServices ||
                 _notice.Project.ContractType == ContractType.EducationalServices)
            {
                // AD3.1.3
                if (annex.OtherServices)
                {
                    yield return TedHelpers.Element("D_OTHER_SERVICES",
                        new XAttribute("CTYPE", _notice.Project.ContractType == ContractType.Supplies
                            ? "SUPPLIES"
                            : "SERVICES"));
                }

                // AD3.1.4
                if (annex.ProductsManufacturedForResearch)
                {
                    yield return TedHelpers.Element("D_MANUF_FOR_RESEARCH",
                        new XAttribute("CTYPE", _notice.Project.ContractType == ContractType.Supplies
                            ? "SUPPLIES"
                            : "SERVICES"));
                }
            }

            // AD3.1.5
            if (annex.AllTenders)
            {
                yield return TedHelpers.Element("D_ALL_TENDERS");
            }

            // AD3.1.6-7
            if (annex.ProvidedByOnlyParticularOperator)
            {
                switch (annex.ReasonForNoCompetition)
                {
                    case ReasonForNoCompetition.DTechnical:
                        yield return TedHelpers.Element("D_TECHNICAL");
                        break;
                    case ReasonForNoCompetition.DProtectRights:
                        yield return TedHelpers.Element("D_PROTECT_RIGHTS");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // AD3.1.8
            if (annex.CrisisUrgency)
            {
                yield return TedHelpers.Element("D_PERIODS_INCOMPATIBLE");
            }

            // AD3.1.9
            if (annex.ExtremeUrgency)
            {
                yield return TedHelpers.Element("D_EXTREME_URGENCY");
            }

            // AD3.1.10
            if (annex.AdditionalDeliveries)
            {
                yield return TedHelpers.Element("D_ADD_DELIVERIES_ORDERED");
            }

            // AD3.1.11
            yield return RepetitionWorksOrServices(annex.RepetitionExisting);
            // AD3.1.12
            yield return CommodityMarketElement(annex.CommodityMarket);
            // AD3.1.13-14
            yield return AdvantageousTermsElement(annex.AdvantageousTerms, annex.AdvantageousPurchaseReason);

            // AD3.1.15
            if (annex.MaritimeService &&
                (_notice.Project.ContractType == ContractType.Services ||
                _notice.Project.ContractType == ContractType.SocialServices ||
                _notice.Project.ContractType == ContractType.EducationalServices))
            {
                yield return TedHelpers.Element("D_MARITIME_SERVICES",
                    new XAttribute("CTYPE", "SERVICES"));
            }
        }

        private IEnumerable<XElement> AnnexD4()
        {
            var annex = _notice?.Annexes?.D4 ?? throw new NullReferenceException("Annex D4 not set!");

            // AD4.1.1
            if (annex.NoTenders)
            {
                yield return TedHelpers.Element("D_NO_TENDERS_REQUESTS");
            }

            // AD4.1.2-3
            if (annex.ProvidedByOnlyParticularOperator)
            {
                switch (annex.ReasonForNoCompetition)
                {
                    case ReasonForNoCompetition.DTechnical:
                        yield return TedHelpers.Element("D_TECHNICAL");
                        break;
                    case ReasonForNoCompetition.DProtectRights:
                        yield return TedHelpers.Element("D_PROTECT_RIGHTS");
                        break;
                    case ReasonForNoCompetition.DArtistic:
                        yield return TedHelpers.Element("D_ARTISTIC");
                        break;
                    case ReasonForNoCompetition.DExistenceExclusive:
                        yield return TedHelpers.Element("D_EXCLUSIVE_RIGHT");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }



        private XElement CommodityMarketElement(bool commodityMarket)
        {
            if (commodityMarket && _notice.Project.ContractType == ContractType.Supplies)
            {
                return TedHelpers.Element("D_COMMODITY_MARKET", new XAttribute("CTYPE", "SUPPLIES"));
            }

            return null;
        }

        private XElement DesignContestServiceElement(bool designContestAward)
        {
            if (designContestAward &&
                (_notice.Project.ContractType == ContractType.Services ||
                 _notice.Project.ContractType == ContractType.SocialServices ||
                 _notice.Project.ContractType == ContractType.EducationalServices))
            {
                return TedHelpers.Element("D_CONTRACT_AWARDED_DESIGN_CONTEST", new XAttribute("CTYPE", "SERVICES"));
            }

            return null;
        }

        private XElement RepetitionWorksOrServices(bool repeatExisting)
        {
            if (repeatExisting)
            {
                var showCtype = _notice.Type != NoticeType.SocialContractAward;
                switch (_notice.Project.ContractType)
                {
                    case ContractType.Services:
                    case ContractType.SocialServices:
                    case ContractType.EducationalServices:
                        return showCtype ? TedHelpers.Element("D_REPETITION_EXISTING", new XAttribute("CTYPE", "SERVICES")) : TedHelpers.Element("D_REPETITION_EXISTING");
                    case ContractType.Works:
                        return TedHelpers.Element("D_REPETITION_EXISTING", showCtype ? new XAttribute("CTYPE", "WORKS") : null);
                }
            }

            return null;
        }

        private XElement AdvantageousTermsElement(bool advantageousTerms, AdvantageousPurchaseReason reason)
        {
            if (advantageousTerms &&
                (_notice.Project.ContractType == ContractType.Services ||
                 _notice.Project.ContractType == ContractType.SocialServices ||
                 _notice.Project.ContractType == ContractType.EducationalServices ||
                 _notice.Project.ContractType == ContractType.Supplies))
            {
                var servOrSupp = _notice.Project.ContractType == ContractType.Supplies
                    ? "SUPPLIES"
                    : "SERVICES";
                var showCtype = _notice.Type != NoticeType.SocialContractAward;
                switch (reason)
                {
                    case AdvantageousPurchaseReason.DFromWindingSupplier:
                        return showCtype ? TedHelpers.Element("D_FROM_WINDING_PROVIDER", new XAttribute("CTYPE", servOrSupp)) : TedHelpers.Element("D_FROM_WINDING_PROVIDER");
                    case AdvantageousPurchaseReason.DFromReceivers:
                        return showCtype ? TedHelpers.Element("D_FROM_LIQUIDATOR_CREDITOR", new XAttribute("CTYPE", servOrSupp)) : TedHelpers.Element("D_FROM_LIQUIDATOR_CREDITOR");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return null;
        }

    }
}
