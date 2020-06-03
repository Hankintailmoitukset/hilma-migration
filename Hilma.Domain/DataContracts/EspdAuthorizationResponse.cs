using System;

namespace Hilma.Domain.DataContracts
{
    public class EspdAuthorizationResponse
    {
        public string EspdProjectId { get; set; }
        public Guid ObjectId { get; set; }
    }
}
