using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [EnumContract]
    public enum TedPublishState 
    {
        Undefined = 0,
        /// <summary>
        /// Notice is in queue to be sent to TED.
        /// </summary>
        SendingToTed = 1,
        /// <summary>
        /// TED has given a RECEIVED -response. Now awaits TED publication.
        /// </summary>
        SentToTed = 2,
        /// <summary>
        /// TED status has been polled and still waiting to be published.
        /// </summary>
        AwaitingTedPublish = 3,
        /// <summary>
        /// Success!
        /// </summary>
        PublishedInTed = 4,
        /// <summary>
        /// Rejected by TED due to invalid XML or other reasons
        /// </summary>
        RejectedByTed = 5,
        /// <summary>
        /// Not used anymore.
        /// </summary>
        WaitingForInformation = 6,
        /// <summary>
        /// Occupied with a reason code.
        /// </summary>
        NotPublished = 7,
        /// <summary>
        /// TED has approved notice to be published. Publication will occur in the future.
        /// </summary>
        ApprovedForPublish = 8
    }
}
