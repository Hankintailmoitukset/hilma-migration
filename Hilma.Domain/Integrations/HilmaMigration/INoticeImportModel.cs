using System;

namespace Hilma.Domain.Integrations.HilmaMigration
{
    public interface INoticeImportModel
    {
        string NoticeNumber { get; set; }
        string FormNumber { get; set; }
        string Notice { get; set; }
        string TedSubmissionId { get; set; }
        string NoticeOjsNumber { get; set; }
        string NoticeType { get; set; }
        DateTime? HilmaSubmissionDate { get; set; }
        DateTime? HilmaPublishedDate { get; set; }
        bool IsPublishedInTed { get; set; }
        DateTime? TedPublishedDate { get; set; }
        string PreviousNoticeNumber { get; set; }
    }
}
