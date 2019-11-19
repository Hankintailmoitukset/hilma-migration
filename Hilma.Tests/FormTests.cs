using System;
using System.Diagnostics;
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

namespace Hilma.Tests
{
    [TestClass]
    public class FormTests
    {
        private void TestForm(string formNumber)
        {
            var parser = new NoticeXMLParser();
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form{formNumber}.xml");

            var importModel = new NoticeImportContract()
            {
                FormNumber = formNumber,
                NoticeNumber = string.Empty, // Assigned by Hilma
                NoticeOjsNumber = null,
                NoticeType = null,
                HilmaSubmissionDate = DateTime.Now,
                Notice = formOriginalXml
            };

            var notice = parser.ParseNotice(importModel);
            var etsNotice = new EtsNoticeContract(notice);

            var noticeDto = etsNotice.CreateNotice("123");
            noticeDto.CreatorId = Guid.NewGuid();
            noticeDto.Project.Id = 1;
            noticeDto.Project.Organisation.Id = Guid.NewGuid();
            noticeDto.Project.Publish = PublishType.ToTed;
            noticeDto.NoticeNumber = "2019-123456";

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Notice, NoticeContract>());
            var translate = new Mock<ITranslationProvider>();

            var noticeValidator = new NoticeValidator(noticeDto, config.CreateMapper(), translate.Object);

            var isValid = noticeValidator.Validate(true, out string tedXml);

            foreach (string error in noticeValidator.ValidationErrors)
            {
                Trace.WriteLine(error);
            }

            Assert.IsTrue(isValid);

            Trace.WriteLine(tedXml);
        }

        [TestMethod]
        public void TestForm1()
        {
            TestForm("1");
        }

        [TestMethod]
        public void TestForm2()
        {
            TestForm("2");
        }

        [Ignore]
        [TestMethod]
        public void TestForm3()
        {
            TestForm("3");
        }

        [Ignore]
        [TestMethod]
        public void TestForm4()
        {
            TestForm("4");
        }

        [Ignore]
        [TestMethod]
        public void TestForm5()
        {
            TestForm("5");
        }

        [Ignore]
        [TestMethod]
        public void TestForm6()
        {
            TestForm("6");
        }

        [Ignore]
        [TestMethod]
        public void TestForm21()
        {
            TestForm("21");
        }
    }
}
