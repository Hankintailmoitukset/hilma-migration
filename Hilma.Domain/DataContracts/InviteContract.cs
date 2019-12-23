using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts {
    [Contract]
    public class InviteContract
    {
        public string ApplicantContactEmail { get; set; }
    }
}
