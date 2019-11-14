namespace Hilma.Domain.DataContracts
{
    public class TedPublishRequest
    {
        public NoticeContract notice { get; set; }

        public NoticeContract parent { get; set; }
    }
}
