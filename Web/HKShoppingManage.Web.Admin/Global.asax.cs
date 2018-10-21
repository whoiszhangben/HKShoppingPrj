using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using Newtonsoft.Json;
using System.Web.Http;
using WebApiContrib.Formatting.Jsonp;

namespace HKShoppingManage.Web.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            GlobalConfiguration.Configuration.AddJsonpFormatter(config.Formatters.JsonFormatter, "callback");
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new IdentifyPermission());
            GlobalFilters.Filters.Add(new ExceptionAttribute());
            MvcHelper.InitAutofacConfig();
            CacheManager.Init();
            UnitFactory.Init();
            CertificationFactory.Init();
            WatchHelper.Init();

            //全局Json设置
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                ////空值处理
                //setting.NullValueHandling = NullValueHandling.Ignore;

                return setting;
            });
        }
    }
}
