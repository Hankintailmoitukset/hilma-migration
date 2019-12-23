using System;
using System.Collections.Generic;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts.EtsContracts
{
    /// <summary>
    /// Summary of a notice posted to hilma for Ets API.
    /// </summary>
    [Contract]
    public class EtsNoticeSummary
    {
        public EtsNoticeSummary() { }

        /// <summary>
        /// Create the summary from a dbo.
        /// </summary>
        /// <param name="dbo">Dbo to create the summary from.</param>
        /// <param name="includeNotice">Include EtsNoticeContract</param>
        public EtsNoticeSummary(Notice dbo, bool includeNotice = false)
        {
            EtsIdentifier = dbo.EtsIdentifier;
            ProjectId = dbo.ProcurementProjectId;
            NoticeId = dbo.Id;

            OrganisationId = dbo.Project.Organisation.Information.NationalRegistrationNumber;

            CreationDate = dbo.DateCreated;
            ModificationDate = dbo.DateModified;
            HilmaStatus = dbo.State;
            HilmaPublicationDate = dbo.DatePublished;
            TedStatus = dbo.TedPublishState;
            TedSubmissionId = dbo.TedSubmissionId;
            NoticeOjsNumber = dbo.NoticeOjsNumber;
            NoticeNumber = dbo.NoticeNumber;
            TedValidationReport = dbo.TedValidationErrors;

            if (dbo.TedPublicationInfo != null)
            {
                TedPublicationInfo = new EtsTedPublicationInfo(dbo.TedPublicationInfo)
                {
                    PublicationRequestedDate = dbo.TedPublishRequestSentDate
                };
            }

            if (includeNotice)
            {
                Notice = new EtsNoticeContract(dbo);
            }
        }

        /// <summary>
        /// Hilma notice number, formatting [year]-[id].
        /// Assigned by Hilma. Used as TED No Doc Ext.
        /// </summary>
        public string NoticeNumber { get; set; }

        /// <summary>
        /// OJS Number for published Ted notices.
        /// </summary>
        /// <example>2019/S 001-999999</example>
        public string NoticeOjsNumber { get; set; }

        /// <summary>
        /// The datetime at which this notice was first sent to EtsApi.
        /// </summary>
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Datetime at which this notice was modified the last time.
        /// </summary>
        public DateTime? ModificationDate { get; set; }
        /// <summary>
        /// Status of publication to Hilma.
        /// </summary>
        public PublishState HilmaStatus { get; set; }
        /// <summary>
        /// The date this notice was published in Hilma, or null if it is not published yet. The most common reason for delay
        /// is legal obligation to wait for TED to publish first.
        /// </summary>
        public DateTime? HilmaPublicationDate { get; set; }
        /// <summary>
        /// Ets API user assigned identifier for the notice. Non-empty string that needs to be unique per subscription.
        /// </summary>
        public string EtsIdentifier { get; set; }
        /// <summary>
        /// Identifier for an organisation this notice is created under. The Organisation can be created by an user in Hilma
        /// or it can be automatically created with the information provided to the Ets API.
        /// </summary>
        public string OrganisationId { get; set; }
        /// <summary>
        /// Project Id assigned to this notice. In Hilma, each notice belongs to a project. Notices regarding same purchase should
        /// be placed in the same project. Project can be set in the Ets API by supplying optional query parameter on notice creation.
        /// If not supplied, hilma will generate a new project based on the information provided.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Internal identifier of created Hilma notice. 
        /// This identifier can be user to generate URL to public notice in Hilma together with project id
        /// </summary>
        public int NoticeId { get; set; }

        /// <summary>
        /// Information regarding publication in TED.
        /// </summary>
        public EtsTedPublicationInfo TedPublicationInfo { get; set; }
        /// <summary>
        /// Status of publication to Ted.
        /// </summary>
        public TedPublishState TedStatus { get; set; }
        /// <summary>
        /// Ted-assigned identifier for this notice, or null if not published to TED or not yet sent to
        /// TED.
        /// </summary>
        public string TedSubmissionId { get; set; }
        /// <summary>
        /// Possible error and warning messages from TED regarding this notice. Warnings are not critical and in most cases can be
        /// ignored. In fact, some times they cannot be avoided. Errors must be corrected and notice publication must be attempted
        /// again. Currently trying again is not supported by the API. Working on it. Probably just PUT the updated resource again
        /// to same location.
        /// </summary>
        public List<TedValidationReport> TedValidationReport { get; set; }
        /// <summary>
        /// Summary can be extended to contain full EtsNoticeContract. By default this is null.
        /// </summary>
        public EtsNoticeContract Notice { get; set; }
    }
}
