using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json.Serialization;

namespace PhoneLogic.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            log4net.Config.XmlConfigurator.Configure();
            var serializerSettings =
        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            var contractResolver =
              (DefaultContractResolver)serializerSettings.ContractResolver;
            contractResolver.IgnoreSerializableAttribute = true;

        }
    }
}