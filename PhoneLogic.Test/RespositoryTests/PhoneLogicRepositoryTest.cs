using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Repository;

namespace PhoneLogic.Test
{
    [TestClass]
    public class PhoneLogicRepositoryTest
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        [TestMethod]
        public void GetMyCallbacksTestMethod()
        {
          //  var m = db.getMyCallbacks(TestConfig.SIP);
          //  Assert.IsTrue(m.Any());
        }

        [TestMethod]
        public void GetPhoneLogicTasksBySipTestMethod( )
        {
            var m = db.GetMyPhoneLogicTasks(TestConfig.SIP);
            Assert.IsTrue(m.Any());
       }

        [TestMethod]
        public void GetPhoneLogicTaskTestMethod()
        {
            var m = db.GetPhoneLogicTask(TestConfig.JobNum, TestConfig.TaskId);
            Assert.IsTrue(m.Any());
        }


        [TestMethod]
        public void InsertPlacedCallTestMethod()
        {
            throw new NotImplementedException();
            /* var m = db.InsertPlacedCall(TestConfig.AgentId, TestConfig.JobNum,  TestConfig.  )
                TestConfig.SIP);
            Assert.IsTrue(m.Any()); */
        }

        [TestMethod]
        public void InsertSuccessfullCallbackTestMethod()
        {
            /*var m = db.InsertPlacedCall(TestConfig.AgentId, TestConfig.JobNum,  TestConfig.  )
                TestConfig.SIP);
            Assert.IsTrue(m.Any());
             * */
            throw new NotImplementedException();
        }
        
        [TestMethod]
        public void CloseCallbackTestMethod()
        {
            /*var m = db.InsertPlacedCall(TestConfig.AgentId, TestConfig.JobNum,  TestConfig.  )
                TestConfig.SIP);
            Assert.IsTrue(m.Any());
             * */
            throw new NotImplementedException();
        }


    }

}
