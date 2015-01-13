using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Phonelogic.Repository;


namespace Phonelogic.API.Tests
{
    [TestClass]
    public class phone_typeControllerTest
    {
        HttpClient client = new HttpClient();

        public phone_typeControllerTest()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(TestConfig.baseAddress);
        }
        #region phone_typeController

        [TestMethod]
        public void GetAllphone_types()
        {
            HttpResponseMessage response = client.GetAsync("phone_type").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificphone_type()
        {
            HttpResponseMessage response = client.GetAsync("phone_type('" + TestConfig.PhoneType + "')").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
