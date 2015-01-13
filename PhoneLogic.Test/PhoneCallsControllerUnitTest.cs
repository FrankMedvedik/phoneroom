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
        public void GetPhoneCallsBySip()
        {

            var controller = new PhoneCallsController();

            var tr = controller.GetPhoneCalls(TestConfig.SIP);
            Assert.IsNotNull(tr);

        }

        [TestMethod]
        public void GetPhoneCallsByJobAndTask()
        {
            // arrange
            var controller = new PhoneCallsController();
            var result = controller.GetPhoneCalls(TestConfig.JobNum, TestConfig.TaskId) as List<PhoneCall>;
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
        }

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
