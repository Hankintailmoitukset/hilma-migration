namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Import data contract for notice import for migration purposes.
    /// Not intended for regular use. 
    /// </summary>
    public class NoticeImportContract
    {
        /// <summary>
        /// External identifier for the notice from sender system.
        /// </summary>
        public string NoticeIdentifier { get; set; }

        /// <summary>
        /// Notice to be imported
        /// </summary>
        public NoticeContract Notice { get; set; }

        /// <summary>
        /// Migration api subscription id
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Migration api subscription name
        /// </summary>
        public string SubscriptionName { get; set; }
    }
}
