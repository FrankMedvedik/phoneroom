using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Phonelogic.Repository;
using System.Linq;

namespace Phonelogic.API.Tests
{
    [TestClass]
    public class dbUnitTest1
    {
        respEntities db = new respEntities();

        //[TestMethod]
        //public void address_typeDBTest()
        //{
        //    address_type a = db.address_types.Find(TestConfig.AddressType);
        //    Assert.AreEqual(a.address_type_code, TestConfig.AddressType);
        //}

        [TestMethod]
        public void FormatPhoneDBTest()
        {
            Assert.AreEqual(FormatUtil.formatPhoneNumber(TestConfig.unformattedPhoneNumber), TestConfig.formattedPhoneNumber);

        }

        [TestMethod]
        public void phoneDBTest()
        {
            phone a = db.phones.Find(new Guid(TestConfig.phoneGuid));
            Assert.AreEqual(a.phone_id, new Guid(TestConfig.phoneGuid));
        }

        //[TestMethod]
        //public void addressDBTest()
        //{
        //    address a = db.addresses.Find( new Guid(TestConfig.addressGuid));
        //    Assert.AreEqual(a.address_id,new Guid(TestConfig.addressGuid));
        //}

        [TestMethod]
        public void personDBTest()
        {
            person a = db.people.Find(new Guid(TestConfig.personGuid));
            Assert.AreEqual(a.person_id, new Guid(TestConfig.personGuid));
        }

        //[TestMethod]
        //public void prefixDBTest()
        //{
        //    prefix a = db.prefixes.Find(TestConfig.NamePrefix);
        //    Assert.AreEqual(a.prefix_code, TestConfig.NamePrefix);
        //}

        //[TestMethod]
        //public void countryDBTest()
        //{
        //    country a = db.countries.Find(TestConfig.CountryCode);
        //    Assert.AreEqual(a.country_code, TestConfig.CountryCode);
        //}

        //public void data_sourceDBTest()
        //{
        //    data_source a = db.data_sources.Find(TestConfig.DataSource);
        //    Assert.AreEqual(a.source_id, TestConfig.DataSource);
        //}

    }
}
