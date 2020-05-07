using System;

namespace Hilma.Domain.DataContracts
{
    public class EspdAuthorizationRequest
    {
        public Guid ObjectId { get; set; }
        public string EspdRequestedProject { get; set; }
    }
}
