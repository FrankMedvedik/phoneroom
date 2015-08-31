using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;
using PhoneLogic.Model;
namespace PhoneLogic.Test
{
    [TestClass]
    public class LyncCallJobsControllerUnitTest
    {
        [TestMethod]
        public void GetLyncCallJobsController()
        {

            var controller = new LyncCallJobsController();

            var result = controller.GetLyncCallJobs(new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 10, 03).Ticks);
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }
    }
}
