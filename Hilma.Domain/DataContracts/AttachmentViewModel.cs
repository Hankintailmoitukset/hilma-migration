using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    [Contract]
    public class AttachmentViewModel
    {
        public string Url { get; set; }
        public FileStatus Status { get; set; }
    }
}
