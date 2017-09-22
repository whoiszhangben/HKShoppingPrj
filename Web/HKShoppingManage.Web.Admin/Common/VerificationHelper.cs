using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HKShoppingManage.Model;

namespace HKShoppingManage.Web.Admin
{
    public class VerificationHelper
    {
        #region 验证用户新增
        /// <summary>
        /// 验证用户新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static bool VerifyUserAdd(User model, out string errCode)
        {
            errCode = string.Empty;

            if (string.IsNullOrEmpty(model.UserName))
            {
                errCode = "11002";//11002 姓名不能为空!
                return false;
            }
            if (model.UserName.Length > 20)
            {
                errCode = "11003";//11003 姓名请限制在30字内!
                return false;
            }
            if (string.IsNullOrEmpty(model.LoginCode))
            {
                errCode = "11006";//11006 登录账号不能为空!
                return false;
            }
            if (model.LoginCode.Length > 20 || model.LoginCode.Length < 6)
            {
                errCode = "11007";//11007 登录账号只能为6-20位的数字、字母或下划线!
                return false;
            }
            if (string.IsNullOrEmpty(model.LoginPwd))
            {
                errCode = "11009";//11009 登录密码不能为空!
                return false;
            }
            if (model.LoginPwd.Length > 20 || model.LoginPwd.Length < 6)
            {
                errCode = "11010";//11010 登录密码只能为6-20位的字母、数字、符号!
                return false;
            }

            return true;
        } 
        #endregion

        public static bool VerifyUserEdit(User model, out string errCode)
        {
            errCode = string.Empty;
            return true;
        } 
    }
}