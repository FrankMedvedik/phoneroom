using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;
using PhoneLogic.Model;
namespace PhoneLogic.Test
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
