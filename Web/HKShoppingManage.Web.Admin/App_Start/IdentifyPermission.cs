using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HKShoppingManage.Common;
using HKShoppingManage.Model;

namespace HKShoppingManage.Web.Admin
{
    public class IdentifyPermission : ActionFilterAttribute, IActionFilter
    {
        private string uploadController = "import";
        private static string[] freeControllers = { "login" };

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            string curControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            if (!freeControllers.Contains(curControllerName))
            {
                var sessionUser = SessionHelper.GetSession<User>(SessionKey.UserInfo);

                #region 判断用户是否在线
                if (sessionUser == null)
                {
                    SetNoLoginResult(filterContext, "10000");//10000 你尚未登录或者登录已过期!
                    return;
                }
                #endregion

                #region 判断用户是否被删除
                //var cacheUser = MvcHelper.User;
                //if (cacheUser.Id <= 0)
                //{
                //    SetNoLoginResult(filterContext, "21000");//账号不存在，请联系管理员!
                //    return;
                //}
                #endregion

            }
        }


        /// <summary>
        /// 处理未登录返回结果
        /// </summary>
        /// <param name="filterContext"></param>
        private void SetNoLoginResult(ActionExecutingContext filterContext, string errCode, string redirectUrl = "/Login/Index")
        {
            //当请求上下文是Ajax请求时
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new NewtonsoftJsonResult
                {
                    Data = new JsonModel(false, errCode, null, redirectUrl)
                };
            }
            else
            {
                //针对上传进行特殊处理
                if (filterContext.ActionDescriptor.ActionName.ToLower().Equals(uploadController))
                {
                    filterContext.Result = new ContentResult
                    {
                        Content = new JsonModel(false, errCode, null, redirectUrl).ToString(),
                        ContentType = "text/html"
                    };
                }
                else
                {
                    SessionHelper.SetSession(SessionKey.ErrorCode, errCode);
                    filterContext.Result = new RedirectResult(redirectUrl);
                }
            }
        }
    }
}