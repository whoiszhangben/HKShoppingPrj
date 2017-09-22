using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;

namespace HKShoppingManage.Web.Admin
{
    public static class HtmlHelperExtend
    {
        public static string GetCssJsUrl(this HtmlHelper helper, string url)
        {
            string version = ConfigurationManager.AppSettings["CssJsVersion"];
            version = version == null ? "1.0" : version;
            return url += "?v=" + version;
        }
    }
}