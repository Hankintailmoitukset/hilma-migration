using Hilma.Domain.DataContracts;
using System;
using System.Collections.Generic;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Ted data wrapper
    /// </summary>
    public class TedData : BaseEntity
    {
        /// <summary>
        ///     TED assigned submission id of the notice.
        /// </summary>
        public string SubmissionId { get; set; }
        /// <summary>
        ///    Publication status returned by TED.
        /// </summary>
        public TedPublishState PublishState { get; set; }
        /// <summary>
        ///     Reason for rejection by TED, if rejected.
        /// </summary>
        public string ReasonCode { get; set; }
        /// <summary>
        ///     TED-generated validation report, in case there are any
        ///     problems. Non-critical are just warnings and can and should
        ///     be ignored.
        /// </summary>
        public List<TedValidationReport> ValidationRules { get; set; }
        /// <summary>
        /// Is used to update TED datamodel
        /// </summary>
        /// <param name="update"></param>
        public void Update(TedData update)
        {
            DateModified = DateTime.UtcNow;
            ValidationRules = update.ValidationRules;
            ReasonCode = update.ReasonCode;
            PublishState = update.PublishState;
        }
    }
}
