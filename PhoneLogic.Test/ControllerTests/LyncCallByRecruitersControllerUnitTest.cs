using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;

namespace PhoneLogic.Server.Tests.ControllerTests
{
    [TestClass]
    public class LyncCallByRecuitersControllerUnitTest
    {
        [TestMethod]
        public void GetLyncCallByRecruitersRecruiter()
        {

            var controller = new LyncCallByRecruitersController();

            var result = controller.GetLyncCallByRecruiters(new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 08, 03).Ticks, "Recruiter");
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }
        public void GetLyncCallByRecruitersLog()
        {

            var controller = new LyncCallByRecruitersController();

            var result = controller.GetLyncCallByRecruiters(new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 08, 03).Ticks, "Log");
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }


        public void GetLyncCallByRecruitersForRecruiter()
        {

            var controller = new LyncCallByRecruitersController();
            var result = controller.GetLyncCallByRecruiters("sip:jdiehl@reckner.com",new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 08, 03).Ticks);
            var data = result.ToString();
            Assert.IsNotNull(result);
        }

   }
}
