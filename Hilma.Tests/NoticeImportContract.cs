// Responsible developer:
// Responsible team:

using System;
using Hilma.Domain.Integrations.HilmaMigration;

namespace Hilma.Tests
{
    public class NoticeImportContract : INoticeImportModel
    {
        public string NoticeNumber { get; set; }
        public string FormNumber { get; set; }
        public string Notice { get; set; }
        public string TedSubmissionId { get; set; }
        public string NoticeOjsNumber { get; set; }
        public string NoticeType { get; set; }
        public DateTime? HilmaSubmissionDate { get; set; }
        public DateTime? HilmaPublishedDate { get; set; }
        public bool IsPublishedInTed { get; set; }
        public DateTime? TedPublishedDate { get; set; }
        public string PreviousNoticeNumber { get; set; }
    }
}