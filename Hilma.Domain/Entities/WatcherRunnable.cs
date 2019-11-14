namespace Hilma.Domain.Entities {
    /// <summary>
    /// Describes a watchable search.
    /// </summary>
    public class WatcherRunnable
    {
        /// <summary>
        /// Saved name of the watcher given by the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Azure search uri fragment to search for new notices with.
        /// </summary>
        public HilmaSearchParameters SearchParameters { get; set; }
    }
}
