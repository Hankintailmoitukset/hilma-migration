// Responsible developer:
// Responsible team:

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Hilma.Domain.Configuration;
using Hilma.Domain.DataContracts;
using Hilma.Domain.DataContracts.EtsContracts;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.HilmaMigration;
using Hilma.Domain.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Hilma.Tests
{
    public static class TestHelpers
    {
        public static string GetEmbeddedResourceAsString(string endsWith, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            var name = assembly.GetManifestResourceNames().First(s => s.EndsWith(endsWith));
            string result;

            using (Stream stream = assembly.GetManifestResourceStream(name))
            using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public static EtsNoticeContract ConvertContract(string formNumber, string noticeType, string formOriginalXml)
        {
            var parser = new NoticeXMLParser();

            var importModel = new NoticeImportContract()
            {
                FormNumber = formNumber,
                NoticeNumber = string.Empty, // Assigned by Hilma
                NoticeOjsNumber = null,
                NoticeType = noticeType,
                HilmaSubmissionDate = DateTime.Now,
                Notice = formOriginalXml
            };

            var noticeContract = parser.ParseNotice(importModel);
            var etsNotice = new EtsNoticeContract(noticeContract);
            return etsNotice;
        }

        public static string ValidateFormReturnTedXml(string formNumber, string noticeType, string formOriginalXml, bool publishToTED = true)
        {
            var etsNotice = ConvertContract(formNumber, noticeType, formOriginalXml);

            var jsonstring = JsonConvert.SerializeObject(etsNotice);

            var noticeDto = etsNotice.CreateNotice("123");
            noticeDto.CreatorId = Guid.NewGuid();
            noticeDto.Project.Id = 1;
            noticeDto.Project.Organisation.Id = Guid.NewGuid();
            noticeDto.Project.Publish = publishToTED ? PublishType.ToTed : PublishType.ToHilma;
            noticeDto.NoticeNumber = "2019-123456";
            
            var noticeJson = JsonConvert.SerializeObject(noticeDto);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Notice, NoticeContract>());
            var translate = new Mock<ITranslationProvider>();

            var noticeValidator = new NoticeValidator(noticeDto, config.CreateMapper(), translate.Object);

            var isValid = noticeValidator.Validate(out string tedXml);

            foreach (string error in noticeValidator.ValidationErrors)
            {
                Trace.WriteLine(error);
            }

            Assert.IsTrue(isValid,"Notice is not valid, please se output!");

            return tedXml;
        }
    }
}