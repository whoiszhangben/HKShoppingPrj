using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HKShoppingManage.Model
{
    /// <summary>
    /// 客户实体类
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 主键，自增长Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        public string CusName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [DisplayName("客户助记码")]
        public string HelpCode { get; set; }
        /// <summary>
        /// 客户性别
        /// </summary>
        [DisplayName("客户性别")]
        public bool CusGender { get; set; }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        [DisplayName("客户电话")]
        public string CusTelephone { get; set; }
        /// <summary>
        /// 客户联系地址
        /// </summary>
        [DisplayName("客户地址")]
        public string CusAddress { get; set; }
        /// <summary>
        /// 客户备注
        /// </summary>
        [DisplayName("客户备注")]
        public string CusRemark { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("是否有效")]
        public bool IsValid { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Updator { get; set; }
    }
}
