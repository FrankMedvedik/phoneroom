using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Repository;

namespace PhoneLogic.Test
{
    [TestClass]
    public class PhoneLogicCallRptRepositoryTest
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        [TestMethod]
        public void rpt_GetCallbackRptText()
        {
            var m = db.rpt_GetCallbackRpt(DateTime.Today, DateTime.Today.AddDays(1));

            Assert.IsTrue(m.Any());
        }

        [TestMethod]
        public void rpt_GetLyncCallLogTestMethod()
        {
            var m = db.rpt_GetLyncCallLog((DateTime.Today.AddDays(-5)), DateTime.Today.AddDays(1));
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
            var m = db.rpt_GetLyncCallLog((DateTime.Today.AddDays(-5)), DateTime.Today.AddDays(1));
            Assert.IsTrue(m.Any());
        }


        [TestMethod]
        public void rpt_GetLyncCallRecruiterTestMethod()
        {
            var m = db.rpt_GetLyncCallRecruiters((DateTime.Today.AddDays(-5)), DateTime.Today.AddDays(1));
            Assert.IsTrue(m.Any());
        }
        public void rpt_GetRecruiterLyncCallLogTestMethod()
        {
            var m = db.rpt_GetRecruiterLyncCallLog(TestConfig.SIP,(DateTime.Today.AddDays(-5)), DateTime.Today.AddDays(1));
            Assert.IsTrue(m.Any());
        }

    }

}
