using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Repository.Utils;
using PhoneLogic.Test;

namespace PhoneLogic.Server.Tests.RespositoryTests
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
