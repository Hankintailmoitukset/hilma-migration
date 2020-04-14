using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hilma.Tests
{
    [TestClass]
    public class Form3JointProcurement
    {
        [TestMethod]
        public void Form3JointProcurementTest()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form3JointProcurement.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("3", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}
