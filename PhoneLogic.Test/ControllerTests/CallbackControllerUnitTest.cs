using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Model;
using PhoneLogic.Test;
using PhoneLogic.Web.Controllers;

namespace PhoneLogic.Server.Tests.ControllerTests
{
    [TestClass]
    public class CallbackControllerUnitTest
    {
        [TestMethod]
        //public void GetCallbacksBySipTest()
        //{

        //    var controller = new CallbacksController();

        //    var result = controller.GetCallbacks(TestConfig.SIP);
        //    Assert.IsNotNull(result);
        //    Assert.AreNotEqual(0, result.Count);
        //}

        public void GetCallbacksById()
        {

            var controller = new CallbacksController();

            var result = controller.GetCallbacks(TestConfig.callbackId);
            Assert.IsNotNull(result);
            Assert.AreEqual(TestConfig.callbackId, result.callbackID);
        }


        [TestMethod]
        public void UpdateCallbackTest()
        {
            var controller = new CallbacksController();
            var p = new CallbackDto()
            {
                callbackID = TestConfig.callbackId,
                SIP = TestConfig.SIP,
                phoneNum = TestConfig.PhoneNumber,
                jobNum = TestConfig.JobNum,
                taskID = TestConfig.TaskId,
                EventDesc = "Test Call logging",
                status ="test",
                statusDate = DateTime.Now
                
            };

            var result = controller.Putcallback(p.callbackID, p);
            Assert.AreNotEqual(result.Status,HttpStatusCode.InternalServerError);

        }

    }
}
