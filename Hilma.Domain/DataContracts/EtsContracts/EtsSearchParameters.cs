using System;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts.EtsContracts
{
    /// <summary>
    ///     Search parameters for ets writers search.
    /// </summary>
    public class EtsSearchParameters
    {
        /// <summary>
        ///     Fetches only notices created at or after given datetime.
        /// </summary>
        public DateTime? After { get; set; }

        /// <summary>
        ///     Fetch only notices created at or before given datetime.
        /// </summary>
        public DateTime? Before { get; set; }

        /// <summary>
        ///     Fetch only notices with given HilmaStatus. See <see cref="EtsNoticeSummary"/> for details.
        /// </summary>
        public PublishState[] HilmaStatus { get; set; }

        /// <summary>
        ///     Fetch only notices with given TedStatus. See <see cref="EtsNoticeSummary"/> for details.
        /// </summary>
        public TedPublishState[] TedStatus { get; set; }

        /// <summary>
        ///     Fetch only notices of given type. See <see cref="EtsNoticeSummary"/> for details.
        /// </summary>
        public NoticeType[] Type { get; set; }
    }
}
