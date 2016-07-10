using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Repository;
using PhoneLogic.Test;

namespace PhoneLogic.Server.Tests.RespositoryTests
{
    [TestClass]
    public class PhoneLogicCallRptRepositoryTest
    {
        private DateTime _dtStart = DateTime.Parse("01/01/2016");
        private DateTime _dtEnd = DateTime.Parse("01/01/2017");

        private PhoneLogicEntities db = new PhoneLogicEntities();
        [TestMethod]
        public void rpt_GetClosedCallbackRptTest()
        {
            var m = db.rpt_GetClosedCallbackRpt(_dtStart, _dtEnd);

            Assert.IsTrue(m.Any());
        }
        [TestMethod]
        public void rpt_GetOpenCallbackRptTest()
        {
            var m = db.rpt_GetOpenCallbackRpt();

            Assert.IsTrue(m.Any());
        }

        [TestMethod]
        public void rpt_GetLyncCallLogTestMethod()
        {
            var m = db.rpt_GetLyncCallLog(_dtStart, _dtEnd);
            Assert.IsTrue(m.Any());
       }

        //[TestMethod]
        //public void GetPhoneLogicTaskTestMethod()
        //{
        //    var m = db.rpt_GetLyncCallJobRecruiterLog(String.Format(TestConfig.JobNum , TestConfig.TaskId);
        //    Assert.IsTrue(m.Any());
        //}



        [TestMethod]
        public void rpt_GetLyncCallLogLogTestMethod()
        {
            var m = db.rpt_GetLyncCallLog(_dtStart, _dtEnd);
            Assert.IsTrue(m.Any());
        }


        [TestMethod]
        public void rpt_GetLyncCallRecruiterTestMethod()
        {
            var m = db.rpt_GetLyncCallRecruiters(_dtStart, _dtEnd);
            Assert.IsTrue(m.Any());
        }
        public void rpt_GetRecruiterLyncCallLogTestMethod()
        {
            var m = db.rpt_GetRecruiterLyncCallLog(TestConfig.SIP, _dtStart, _dtEnd);
            Assert.IsTrue(m.Any());
        }

    }

}
