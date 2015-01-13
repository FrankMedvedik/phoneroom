using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Phonelogic.Repository;


namespace Phonelogic.API.Tests
{
    [TestClass]
    public class PeopleControllerTest
    {
        HttpClient client = new HttpClient();

        public PeopleControllerTest()
        {
            client.BaseAddress = new Uri(TestConfig.baseAddress);
        }
        #region PeopleController

        [TestMethod]
        public void GetAllPersons()
        {
            HttpResponseMessage response = client.GetAsync("person?$top=4").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificPerson()
        {
            HttpResponseMessage response = client.GetAsync("People/"+ TestConfig.personGuid).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificPersonAddresses()
        {
            HttpResponseMessage response = client.GetAsync("person(guid'" + TestConfig.personGuid + "')/addresses").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void GetSpecificPersonPhones()
        {
            HttpResponseMessage response = client.GetAsync("person(guid'" + TestConfig.personGuid + "')/phones").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public void GetSpecificPersonEmails()
        {
            HttpResponseMessage response = client.GetAsync("person(guid'" + TestConfig.personGuid + "')/emails").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificPersonSampleItems()
        {
            HttpResponseMessage response = client.GetAsync("person(guid'" + TestConfig.personGuid + "')/sample_items").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion

    }
}
