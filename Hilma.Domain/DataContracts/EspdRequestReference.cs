using System;

namespace Hilma.Domain.DataContracts
{
    public interface IEspdRequestIdentifier
    {
        int Id { get; set; }
        int ProjectId { get; set; }
        int NoticeId { get; set; }
    }

    public class EspdRequestReference : IEspdRequestIdentifier
    {
        public int Id { get; set; }
        public Guid? UUID { get; set; }
        public int ProjectId { get; set; }
        public int NoticeId { get; set; }
        public string Title { get; set; }
        public string[] Lots { get; set; }
    }
}
