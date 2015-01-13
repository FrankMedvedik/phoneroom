using System;
using System.Windows;

namespace PhoneLogic.Core
{
    public static class ApiWebSite
    {
#if DEBUG
        //    public static string urlRoot = "http://localhost:19938/api/";
        public static string urlRoot = "http://cc-prod.reckner.com/phoneroom/api/";
            //public static string urlRoot = "http://cc-app.reckner.com/inbound/api/";
#else
        public static string urlRoot = "http://cc-prod.reckner.com/phoneroom/api/";
#endif
       public static string applicationUri = System.Windows.Application.Current.Host.ToString();
    }

    public class ApiRefreshFrequency
    {
        public static int Rpts = 30;
        public static int UserDB = 10;
        public static int LyncApi = 1;
    }


  }
