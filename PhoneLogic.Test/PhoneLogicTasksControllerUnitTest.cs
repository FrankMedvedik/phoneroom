using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;
using PhoneLogic.Model;
namespace PhoneLogic.Test
{
    [TestClass]
    public class PhoneLogicTasksControllerUnitTest
    {
        [TestMethod]
        public void GetPhoneLogicTasksBySip()
        {

            var controller = new PhoneLogicTasksController();

            var tr = controller.GetPhoneLogicTask(TestConfig.SIP);
            Assert.AreNotEqual(tr.Count(), 0); 
            Assert.IsNotNull(tr);

        }

        [TestMethod]
        public void GetPhoneLogicTaskJobAndTask()
        {
            // arrange
            var controller = new PhoneLogicTasksController();
            var result = controller.GetPhoneLogicTask(TestConfig.JobNum, TestConfig.TaskId);
            Assert.AreNotEqual(result.Count(), 0);
            Assert.IsNotNull(result);
        }


    }
}
