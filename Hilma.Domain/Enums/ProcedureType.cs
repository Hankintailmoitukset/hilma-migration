using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [EnumContract]
    public enum ProcedureType
    {
        Undefined = 0,
        /// <summary>
        /// Open procedure
        /// </summary>
        ProctypeOpen = 1,
        /// <summary>
        /// Restricted procedure
        /// </summary>
        ProctypeRestricted = 2,
        /// <summary>
        /// Competitive procedure with negotiation
        /// </summary>
        ProctypeCompNegotiation = 3,
        /// <summary>
        /// Competitive dialogue
        /// </summary>
        ProctypeCompDialogue = 4,
        /// <summary>
        /// InnovationPartnership
        /// </summary>
        ProctypeInnovation = 5,
        ProctypeConcessionWoPub = 6,
        ProctypeNegotWCall = 7,
        ProctypeAwardWoCall = 8,
        ProctypeNegotiatedWoNotice = 9,
        ProctypeNegotiatedWoPub = 10,
        ProctypeNegotiationsInvolved = 11,
        ProctypeWithConcessNotice = 12,
        ProctypeNegotiation = 13,
        ProctypeOther = 14,
        AwardWoPriorPubD1 = 15,
        AwardWoPriorPubD4 = 16,
        ProctypeNationalDirect = 17,
        /// <summary>
        /// TEF F21 Annex D1 - 2. Other justification...
        /// </summary>
        AwardWoPriorPubD1Other = 18
    }
}
