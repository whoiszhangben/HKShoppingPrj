using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKShoppingManage.Web.Admin
{
    /// <summary>
    /// 保存session中的验证码信息
    /// </summary>
    public class SessionCode
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}