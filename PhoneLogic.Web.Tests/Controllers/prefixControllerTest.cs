using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Phonelogic.Repository;


namespace Phonelogic.API.Tests
{
    [TestClass]
    public class prefixControllerTestControllerTest
    {
        HttpClient client = new HttpClient(); 

        public prefixControllerTestControllerTest()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(TestConfig.baseAddress);
        }
        #region prefixControllerTestController

        [TestMethod]
        public void GetAllprefixControllerTests()
        {
            HttpResponseMessage response = client.GetAsync("prefix").Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificprefixControllerTest()
        {
            HttpResponseMessage response = client.GetAsync("prefix('"+ TestConfig.NamePrefix + "')").Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
