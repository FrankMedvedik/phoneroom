﻿using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneLogic.Web.Controllers;
using PhoneLogic.Model;
namespace PhoneLogic.Test
{
    [TestClass]
    public class LyncCallJobPhoneroomRecruitersControllerUnitTest
    {
        [TestMethod]
        public void GetLyncCallJobPhoneroomRecruitersController()
        {

            var controller = new LyncCallJobPhoneroomRecruitersController();

            var result = controller.GetLyncCallJobPhoneRoomRecruiters("20151191:01", new DateTime(2015, 08, 01).Ticks, new DateTime(2015, 10, 03).Ticks);
            var data = result.ToString();
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(0, reCount);
        }
   }
}