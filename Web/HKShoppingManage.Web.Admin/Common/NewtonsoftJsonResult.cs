using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace HKShoppingManage.Web.Admin
{
    public class NewtonsoftJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                response.Write(JsonConvert.SerializeObject(this.Data));
            }
        }
    }
}