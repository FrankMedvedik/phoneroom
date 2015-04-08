
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PhoneLogic.Core
{
    public static class ApiRefreshFrequency
    {
        public static int Rpts = 30;
        public static int UserDB = 10;
        public static int LyncApi = 1;
    }

    //public static class EndPointConfig
    //{
    //    public  static void UpdateAddress(ServiceEndpoint endpoint)
    //    {
    //        endpoint.Address = new EndpointAddress(ConditionalConfiguration.LyncServiceRefUrl);
    //    }
    //}

    public static class ConditionalConfiguration
    {

    
#if DEBUGLOCAL
     public static string BuildType = "DEBUG-LOCAL";
     public const string apiUrl = "http://localhost:19938/api/";
     public const string rootUrl = "http://localhost:19938/";
     public static string LyncServiceRefUrl = "http://cc-prod.reckner.com:1255/PhoneLogic/basic";
     public const string RecknerCallAppGuid = "{49475055-F87F-4C67-96B4-154B05B19484}";
#elif DEBUGTEST
     public static string BuildType = "DEBUG-TEST";
     public static string apiUrl = "http://cc-app.reckner.com/inbound/api/";
     public static string rootUrl = "http://cc-app.reckner.com/inbound/";
     public static string LyncServiceRefUrl = "http://cc-app.reckner.com:1255/PhoneLogic/basic";
     public const string RecknerCallAppGuid = "{EF327138-E9C6-4E5D-8BB3-F505DAB0F567}";
        
#elif DEBUGPROD
        public static string BuildType = "DEBUG-PROD";
        public static string apiUrl = "http://cc-prod.reckner.com/phoneroom/api/";
        public static string rootUrl = "http://cc-prod.reckner.com/phoneroom/";
        public static string LyncServiceRefUrl = "http://cc-prod.reckner.com:1255/PhoneLogic";
     public const string RecknerCallAppGuid = "{E7D2695C-96F8-4C49-858A-28F6106B2B39}";   
#elif RELEASEPROD
        public static string BuildType = "PROD-RELEASE";
        public static string apiUrl = "http://cc-prod.reckner.com/phoneroom/api/";
        public static string rootUrl = "http://cc-prod.reckner.com/phoneroom/";
        public static string LyncServiceRefUrl = "http://cc-prod.reckner.com:1255/PhoneLogic";
     public const string RecknerCallAppGuid = "{EF327138-E9C6-4E5D-8BB3-F505DAB0F567}";
#elif RELEASETEST
      public static string BuildType = "TEST-RELEASE";
     public static string apiUrl = "http://cc-app.reckner.com/inbound/api/";
     public static string LyncServiceRefUrl = "http://cc-app.reckner.com:1255/PhoneLogic";
     public const string RecknerCallAppGuid = "{EF327138-E9C6-4E5D-8BB3-F505DAB0F567}";
#endif
        public static readonly String BuildDate = DateTime.Now.ToString();
    }


    }
