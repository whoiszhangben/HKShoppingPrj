using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using HKShoppingManage.Common;
using HKShoppingManage.BLL;
using HKShoppingManage.Model;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class LoginController : BaseController
    {
        private IUserBLL bll;
        public LoginController(IUserBLL bll)
        {
            this.bll = bll;
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginCode">登录账号</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string loginCode, string loginPwd, string validateCode)
        {
            SessionCode code = SessionHelper.GetSession<SessionCode>("ValidateCode");

            #region 验证
            if (string.IsNullOrEmpty(loginCode))
            {
                return Json(new JsonModel(false, "11012"));//11012 用户名不能为空!
            }
            if (string.IsNullOrEmpty(loginPwd))
            {
                return Json(new JsonModel(false, "11013"));//11013 密码不能为空!
            }
            if (string.IsNullOrEmpty(validateCode))
            {
                return Json(new JsonModel(false, "11018"));//11016 验证码不能为空!
            }
            if (code == null || (DateTime.Now - code.Time).TotalMinutes > 2)
            {
                return Json(new JsonModel(false, "11020")); //验证码失效!
            }
            if (!validateCode.ToUpper().Equals(code.ValidateCode.ToUpper()))
            {
                return Json(new JsonModel(false, "11017"));//11017 验证码不匹配!
            }
            #endregion

            SessionHelper.RemoveSession(SessionKey.ErrorCode);

            var setloginCode = AppSettingsHelper.GetStringByKey("loginCode", "admin");
            var setloginPwd = AppSettingsHelper.GetStringByKey("loginPwd", "muchinfo");
            if (loginCode == setloginCode && loginPwd == setloginPwd)
            {
                var user = new User { LoginCode = loginCode, LoginPwd = loginPwd };
                SessionHelper.SetSession(SessionKey.UserInfo, user);

                string url = "/Home/Index";

                return Json(new JsonModel(true, "", null, url));
            }
            else
            {
                return Json(new JsonModel(false, "11014"));//11014 用户名或密码错误,请重新输入!
            }
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            SessionHelper.RemoveSession(SessionKey.UserInfo);
            SessionHelper.RemoveSession(SessionKey.ErrorCode);
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 显示验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode()
        {
            ValidCode code = new ValidCode(4, ValidCode.CodeType.Words);
            byte[] bytes = code.CreateCheckCodeImage();
            ViewBag.ValidateCode = code.CheckCode;
            SessionHelper.SetSession(SessionKey.ValidateCode, new SessionCode() { ValidateCode = code.CheckCode, Time = DateTime.Now });
            return File(bytes, @"image/jpeg");
        }

        #region 获取客户端IP
        private string GetClientIP()
        {
            string userHostAddress = string.Empty;
            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                userHostAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            //前两者均失败,则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式(检查其格式非常重要)
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        private bool IsIP(string ip)
        {
            bool isIp = false;
            isIp = System.Text.RegularExpressions.Regex.IsMatch(ip, @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            return isIp;
        }
        #endregion
    }
}