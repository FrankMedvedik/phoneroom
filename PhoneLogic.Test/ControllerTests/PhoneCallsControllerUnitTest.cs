using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Model;
using PhoneLogic.Test;
using PhoneLogic.Web.Controllers;

namespace PhoneLogic.Server.Tests.ControllerTests
{
    [TestClass]
    public class PhoneCallsControllerUnitTest
    {

        [TestMethod]
        public void PostPhoneCall()
        {
            var controller = new PhoneCallsController();
            var p = new PhoneCall()
            {

                callbackID = TestConfig.callbackId,
                SIP = TestConfig.SIP,
                phoneNum = TestConfig.PhoneNumber,
                jobNum = TestConfig.JobNum,
                taskID = TestConfig.TaskId,
                EventDesc = "Test Call logging"
            };

            var result = controller.PostPhoneCall(p);
            Assert.IsNotNull(result.Status);
        }

    }
}
