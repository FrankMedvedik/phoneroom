using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Phonelogic.Repository;

namespace Phonelogic.API.Tests
{
    [TestClass]
    public class contact_prefControllerTest
    {
        HttpClient client = new HttpClient();

        public contact_prefControllerTest()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(TestConfig.baseAddress);
        }
        #region contact_prefController

        [TestMethod]
        public void GetAllcontact_prefs()
        {
            HttpResponseMessage response = client.GetAsync("contact_pref").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificcontact_pref()
        {
            HttpResponseMessage response = client.GetAsync("contact_pref(" + TestConfig.ContactPrefId +" )").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
