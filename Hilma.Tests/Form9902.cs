// Responsible developer:
// Responsible team:

using Hilma.Domain.DataContracts;
using Hilma.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form9902
    {
        [TestMethod]
        public void METHOD()
        {
            var json = TestHelpers.GetEmbeddedResourceAsString("TestSchema9902_3.json");
            Newtonsoft.Json.JsonConvert.DeserializeObject<NoticeContract>(json);
        }
    }
}