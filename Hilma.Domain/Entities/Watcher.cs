using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     A search saved as a watcher by an user, to get personalized email digest of
    ///     new notices.
    /// </summary>
    [Contract]
    public class Watcher
    {
        /// <summary>
        ///     User (front-end on behalf of user) assigned surrogate key.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     User assigned name for this watcher.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The search string for this watcher.
        /// </summary>
        public HilmaSearchParameters SearchParameters { get; set; }

        /// <summary>
        ///     Query parameters, saved to go back in edit mode.
        /// </summary>
        public string FrontEndParameters { get; set; }
    }
}
