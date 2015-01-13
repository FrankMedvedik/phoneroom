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
        public void GetJobActivityReport()
        {

            var controller = new JobActivityRptController();

            var result = controller.GetJobActivityRpt(new DateTime(2014,11,01), new DateTime(2014,11,03));
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }

        [TestMethod]
        public void GetTodayCallLogSummary()
        {

            var controller = new TodayRptController();

            var result = controller.GetTodayCallLogSummary();
            var data = result.ToString();
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetInBoundCallsByHour( )
        {
            
            var controller = new InboundCallRptController();

            var result = controller.GetInboundCallsByHour(DateTime.Now, DateTime.Now);
            var data = result.ToString();
            Assert.IsNotNull(result);
            
        }


    }
}
