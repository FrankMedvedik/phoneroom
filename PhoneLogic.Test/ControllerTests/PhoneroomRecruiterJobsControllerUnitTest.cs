using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;

namespace PhoneLogic.Server.Tests.ControllerTests
{
    [TestClass]
    public class PhoneroomRecruiterJobsControllerUnitTest
    {
        [TestMethod]
        public void GetPhoneroomRecruiterJobsTest()
        {

            var controller = new PhoneroomRecruiterJobsController();
            var result = controller.GetPhoneRoomRecruiterJobs();
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }
    }
}
