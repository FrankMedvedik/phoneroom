using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Repository;

namespace PhoneLogic.Test
{
    [TestClass]
    public class RespondentRepositoryTest
    {
        private respEntities db = new respEntities();


        [TestMethod]
        public void FormatPhoneDbTest()
        {
            Assert.AreEqual(FormatUtil.formatPhoneNumber(TestConfig.UnformattedPhoneNumber), TestConfig.FormattedPhoneNumber);
        }

        [TestMethod]
        public void PhoneDbTest()
        {
            phone a = db.phones.Find(new Guid(TestConfig.PhoneGuid));
            Assert.AreEqual(a.phone_id, new Guid(TestConfig.PhoneGuid));
        }

        [TestMethod]
        public void PersonDbTest()
        {
            person a = db.people.Find(new Guid(TestConfig.PersonGuid));
            Assert.AreEqual(a.person_id, new Guid(TestConfig.PersonGuid));
        }

    }
}

