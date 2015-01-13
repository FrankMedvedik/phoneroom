using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Phonelogic.Repository;


namespace Phonelogic.API.Tests
{
    [TestClass]
    public class comm_typeControllerTest
    {
        HttpClient client = new HttpClient();

        public comm_typeControllerTest()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(TestConfig.baseAddress);
        }
        #region comm_typeController

        [TestMethod]
        public void GetAllcomm_types()
        {
            HttpResponseMessage response = client.GetAsync("comm_type").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificcomm_type()
        {
            HttpResponseMessage response = client.GetAsync("comm_type('"+ TestConfig.CommType + "')").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

    }
}
