using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HKShoppingManage.Web.Admin
{
    public class CssRewriteUrlTransformWrapper : IItemTransform
    {
        public string Process(string includeVirtualPath, string input)
        {
            return new CssRewriteUrlTransform().Process("~" + VirtualPathUtility.ToAbsolute(includeVirtualPath), input);
        }
    }
}