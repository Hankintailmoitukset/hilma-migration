using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [EnumContract]
    public enum ProcedureType
    {
        Undefined = 0,

        /// <summary>
        /// Open procedure
        /// OPEN or PT_OPEN
        /// </summary>
        ProctypeOpen = 1,

        /// <summary>
        /// Restricted procedure
        /// RESTRICTED, ACCELERATED_RESTRICTED or PT_ACCELERATED_RESTRICTED_CHOICE
        /// </summary>
        ProctypeRestricted = 2,

        /// <summary>
        /// Competitive procedure with negotiation
        /// PT_COMPETITIVE_NEGOTIATION
        /// </summary>
        ProctypeCompNegotiation = 3,

        /// <summary>
        /// Competitive dialogue
        /// COMPETITIVE_DIALOGUE or PT_COMPETITIVE_DIALOGUE
        /// </summary>
        ProctypeCompDialogue = 4,

        /// <summary>
        /// InnovationPartnership
        /// PT_INNOVATION_PARTNERSHIP
        /// </summary>
        ProctypeInnovation = 5,

        /// <summary>
        /// Not used.
        /// </summary>
        ProctypeConcessionWoPub = 6,

        /// <summary>
        /// PT_NEGOTIATED_WITH_PRIOR_CALL
        /// </summary>
        ProctypeNegotWCall = 7,

        /// <summary>
        /// Not used.
        /// </summary>
        ProctypeAwardWoCall = 8,

        /// <summary>
        /// Annexes AD1.2, AD2.2, AD3.2 and AD4.2
        /// PT_AWARD_CONTRACT_WITHOUT_CALL
        /// </summary>
        ProctypeNegotiatedWoNotice = 9,

        /// <summary>
        /// Not used.
        /// </summary>
        ProctypeNegotiatedWoPub = 10,

        /// <summary>
        /// PT_INVOLVING_NEGOTIATION
        /// </summary>
        ProctypeNegotiationsInvolved = 11,

        /// <summary>
        /// PT_AWARD_CONTRACT_WITH_PRIOR_PUBLICATION
        /// </summary>
        ProctypeWithConcessNotice = 12,

        /// <summary>
        /// Defence notices -> PT_NEGOTIATED_WITH_PUBLICATION_CONTRACT_NOTICE or PT_NEGOTIATED_CHOICE
        /// F14 -> NEGOTIATED or ACCELERATED_NEGOTIATED
        /// </summary>
        ProctypeNegotiation = 13,

        /// <summary>
        /// Not used
        /// </summary>
        ProctypeOther = 14,

        /// <summary>
        /// F15 -> Annexes AD1.1, AD2.1 and AD3.1 -> PT_NEGOTIATED_WITHOUT_PUBLICATION
        /// F21, F03 -> PT_AWARD_CONTRACT_WITHOUT_CALL
        /// </summary>
        AwardWoPriorPubD1 = 15,

        /// <summary>
        /// Annex AD4.1
        /// PT_AWARD_CONTRACT_WITHOUT_PUBLICATION
        /// </summary>
        AwardWoPriorPubD4 = 16,

        /// <summary>
        /// Kansallinen suorahankinta
        /// </summary>
        ProctypeNationalDirect = 17,

        /// <summary>
        /// Annex D1-3 - 2. Other justification...
        /// F21, F03 -> PT_AWARD_CONTRACT_WITHOUT_CALL
        /// </summary>
        AwardWoPriorPubD1Other = 18,

        /// <summary>
        /// TED F25 Annex D4 - 2- Other justification
        /// PT_AWARD_CONTRACT_WITHOUT_PUBLICATION
        /// </summary>
        AwardWoPriorPubD4Other = 19
    }
}
