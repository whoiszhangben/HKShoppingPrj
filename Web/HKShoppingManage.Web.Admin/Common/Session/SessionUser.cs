namespace HKShoppingManage.Web.Admin
{
    /// <summary>
    /// 保存在Session中的简易用户信息
    /// </summary>
    public class SessionUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 唯一识别码
        /// </summary>
        public string GUID { get; set; }
    }
}