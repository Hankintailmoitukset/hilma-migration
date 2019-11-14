using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [EnumContract]
    public enum TedPublishState 
    {
        Undefined = 0,
        SendingToTed = 1,
        SentToTed = 2,
        AwaitingTedPublish = 3,
        PublishedInTed = 4,
        RejectedByTed = 5,
        WaitingForInformation = 6,
        NotPublished = 7
    }
}
