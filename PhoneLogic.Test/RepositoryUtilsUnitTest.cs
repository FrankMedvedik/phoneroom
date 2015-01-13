using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Repository.Utils;

namespace PhoneLogic.Test
{
    [TestClass]
    public class RepositoryUtilsUnitTest
    {
        [TestMethod]
        public void AgentLookupBySipTestMethod()
        {
            var a = AgentUtils.GetAgentId(TestConfig.SIP);
            Assert.AreEqual(a, TestConfig.AgentId);
        }

        [TestMethod]
        public void StripPhoneNumberTestMethod()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void FormatPhoneNumberTestMethod()
        {
            throw new NotImplementedException();
        }
    }
}
