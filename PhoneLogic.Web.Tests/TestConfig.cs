using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Phonelogic.Repository;

namespace Phonelogic.API.Tests
{
    public static class TestConfig
    {
        public static string baseAddress = "http://localhost:19938/api/";
        public static string phoneGuid = "0B26532F-0DE6-4FC1-A148-000036014EE8";
        public static string personGuid = "8070A40C-10A4-4B99-A80A-000D8E5C0C2F";
        public static string addressGuid = "DAF03158-6BE7-462A-9534-00000AC94033";
        public static string AddressType = "HOME";
        public static string NamePrefix = "DR";
        public static string CommType = "Post";
        public static string unformattedPhoneNumber = "2152578927";
        public static string formattedPhoneNumber = "+1 215 2578927";
        public static int    ContactPrefId = 1;
        public static string CountryCode = "AD";
        public static string DataSource = "0";
        public static string PhoneType = "HOME";
        public static int SampleMaster = 32;
        public static int SampleItem = 2;
 
    }

}
