using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Test;
using PhoneLogic.Web.Controllers;

namespace PhoneLogic.Server.Tests.ControllerTests
{
    [TestClass]
    public class PhoneLogicTasksControllerUnitTest
    {
        [TestMethod]
        public void GetPhoneLogicTasksBySip()
        {

            var controller = new PhoneLogicTasksController();

            var tr = controller.GetPhoneLogicTask(TestConfig.JobNum, TestConfig.TaskId);
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
