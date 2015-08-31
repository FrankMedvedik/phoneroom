using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;
using PhoneLogic.Model;
namespace PhoneLogic.Test
{
    [TestClass]
    public class RptControllerUnitTest
    {
        [TestMethod]
        public void GetLyncCallByRecruiters()
        {

            var controller = new LyncCallByRecruitersController();

            var result = controller.GetLyncCallByRecruiters(new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 08, 03).Ticks, "Recruiter");
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }
        public void GetLyncCallByJob()
        {

            var controller = new LyncCallByRecruitersController();

            var result = controller.GetLyncCallByRecruiters(new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 08, 03).Ticks, "Log");
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }
        public void GetJobActivityReport()
        {

            var controller = new JobActivityRptController();

            var result = controller.GetJobActivityRpt(new DateTime(2014, 11, 01).Ticks, new DateTime(2014, 11, 03).Ticks);
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }
     
    }
}
