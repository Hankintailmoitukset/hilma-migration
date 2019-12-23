using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    /// Describes different options user has regarding an invite
    /// </summary>
    [EnumContract]
    public enum InviteReply : int
    {
        Undefined = 0, // default, not used
        Approve = 1,
        Reject = 2,
        Blocked = 3 // not used currently, mby in future
    }
}
