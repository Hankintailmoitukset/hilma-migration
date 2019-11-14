using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Hilma.Domain.DataContracts;
using Hilma.Domain.DataContracts.EtsContracts;
using Hilma.Domain.Integrations.HilmaMigration;
using Newtonsoft.Json;

namespace Hilma.MigrationExample
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

    public class Program 

    {
        static void Main(string[] args)
        {

            if (args.Length < 4)
            {
                Console.WriteLine(@"Instructions: give 4 arguments:
[0] Filename, give xml file as first argument. i.e: ./notice.xml
[1] Form number, i.e: 2 
[2] Notice special type (string), if not known:  0, example: 'PRI_REDUCING_TIME_LIMITS'
[3] Notice OJS Number, if not applicable: null");
                Console.WriteLine(JsonConvert.SerializeObject(args));
                return;
            }

            var filename = args[0];
            var formNumber = args[1];
            var noticeType = args[2];
            var ojsNumber = args[3];

            using (var content = File.OpenRead(filename))
            using (var sr = new StreamReader(content, Encoding.UTF8))
            {
                var parser = new NoticeXMLParser();

                var importModel = new NoticeImportContract()
                {
                    FormNumber = formNumber,
                    NoticeNumber = string.Empty, // Assigned by Hilma
                    NoticeOjsNumber = ojsNumber, 
                    NoticeType = noticeType,
                    HilmaSubmissionDate =  DateTime.Now,
                    Notice = sr.ReadToEnd(),
                };

                var notice = parser.ParseNotice(importModel);
                var etsNotice = new EtsNoticeContract(notice);
                Console.Write(JsonConvert.SerializeObject(etsNotice));
            }
        }
    }
}
