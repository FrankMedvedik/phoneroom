using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;

namespace PhoneLogic.Test.ControllerTests
{
    [TestClass]
    public class CallsControllerUnitTest
    {
        [TestMethod]
        public void GetCallsBySipTest()
        {
            var controller = new CallsController();

            var result = controller.GetCalls(TestConfig.SIP, new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 08, 03).Ticks);
            var data = result.ToString();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetCallsByCalllerTest()
        {
            var controller = new CallsController();

            var result = controller.GetCalls(TestConfig.PhoneNumber);
            var data = result.ToString();
            Assert.IsNotNull(result);
        }

        public void GetCallsTest()
        {
            var controller = new CallsController();

            var result = controller.GetCalls("20150", TestConfig.SIP, new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 08, 03).Ticks);
            var data = result.ToString();
            Assert.IsNotNull(result);
        }
    }
}
