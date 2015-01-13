using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Core;
using PhoneLogic.Model;
using PhoneLogic.Core.Services;
using PhoneLogic.Test;

namespace Silverlight.Tests
{
    [TestClass]
    public class CallbackSvcTests
    {


        //CallbackDto c = CallbackSvc.GetMyCallback(TestConfig.callbackId);
            
        [TestMethod]
        public void StartCallTest()
        {
            var cb = new CallbackDto()
            {
                callbackID = TestConfig.callbackId,
                SIP = TestConfig.SIP,
            };

            CallbackSvc.StartCall(cb);
            var t = CallbackSvc.GetMyCallback(TestConfig.callbackId);
            CallbackDto c = t.Result;
            Assert.AreEqual(cb.status, c.status);
        }

        [TestMethod]
        public void EndCallTest()
        {
            var cb = new CallbackDto()
            {
                callbackID = TestConfig.callbackId,
                SIP = TestConfig.SIP,
            };
            CallbackSvc.EndCall(cb);
            var t = CallbackSvc.GetMyCallback(TestConfig.callbackId);
            CallbackDto c = t.Result;
            Assert.AreEqual(cb.status, c.status);
        }

        [TestMethod]
        public void CloseCallBacksTest()
        {
            var cb = new CallbackDto()
            {
                callbackID = TestConfig.callbackId,
                SIP = TestConfig.SIP,
            };
            CallbackSvc.Close(cb);
            var t = CallbackSvc.GetMyCallback(TestConfig.callbackId);
            CallbackDto c = t.Result;
        }

        [TestMethod]
        public void GetCallBacksTest()
        {
            var t = CallbackSvc.GetMyCallback(TestConfig.callbackId);
            CallbackDto c = t.Result;
            Assert.AreNotEqual(TestConfig.callbackId, c.callbackID);

        }


    }
}
