using Hilma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describe status polled from TED for Hilma.
    /// </summary>
    public class TedStatusUpdate
    {
        /// <summary>
        ///     Hilma assigned primary key for the notice's project.
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        ///     Hilma assigned primary key for the notice.
        /// </summary>
        public int NoticeId { get; set; }
        /// <summary>
        ///    Publication status returned by TED.
        /// </summary>
        public TedPublishState PublishState { get; set; }
        /// <summary>
        ///     TED assigned submission id of the notice.
        /// </summary>
        public string SubmissionId { get; set; }
        /// <summary>
        ///     TED-generated validation report, in case there are any
        ///     problems. Non-critical are just warnings and can and should
        ///     be ignored.
        /// </summary>
        public List<TedValidationReport> ValidationRules { get; set; }
        /// <summary>
        ///     If the notice is published in TED, the publication information
        ///     about the notice.
        /// </summary>
        public TedPublicationInfo PublicationInfo { get; set; }
        /// <summary>
        ///     Time of publication to TED, if published.
        /// </summary>
        public DateTime? TedPublicationDate { get; set; }
        /// <summary>
        ///     Reason for rejection by TED, if rejected.
        /// </summary>
        public string ReasonCode { get; set; }
        /// <summary>
        ///     Document identifier that is an Open Journal System
        ///     enforced useless surrogate key in addition to SubmissionId.
        /// </summary>
        public string NoDocExt { get; set; }
    }
}
