using System;
using System.Windows;

namespace PhoneLogic.Core
{
    public static class ApiWebSite
    {
        // services root ;

       
       //public static string urlRoot = "http://cc-app.reckner.com/inbound/api/";

       public static string urlRoot = "http://cc-prod.reckner.com/phoneroom/api/";
       //public static string urlRoot = "http://localhost:19938/api/";

        //public static string urlRoot = "System.Windows.Application.Current.Host" + "/api/";

       public static string applicationUri = System.Windows.Application.Current.Host.ToString();
    }

    public class ApiRefreshFrequency
    {
        public static int Rpts = 30;
        public static int UserDB = 10;
        public static int LyncApi = 1;
    }


  }
