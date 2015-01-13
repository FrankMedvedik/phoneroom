using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Phonelogic.Repository;


namespace Phonelogic.API.Tests
{
    [TestClass]
    public class phoneControllerTest
    {
        HttpClient client = new HttpClient();

        public phoneControllerTest()
        {
            client.BaseAddress = new Uri(TestConfig.baseAddress);
        }
        #region phoneController

        [TestMethod]
        public void GetAllphones()
        {
            HttpResponseMessage response = client.GetAsync("phone?$top=4").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificphone()
        {
            HttpResponseMessage response = client.GetAsync("phone(guid'"+ TestConfig.phoneGuid +"')").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
