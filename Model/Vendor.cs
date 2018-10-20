using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HKShoppingManage.Model
{
    /// <summary>
    /// 采购供应商
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DisplayName("供应商名称")]
        public string VendorName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [DisplayName("助记码")]
        public string HelpCode { get; set; }
        /// <summary>
        /// 供应商地址
        /// </summary>
        [DisplayName("供应商地址")]
        public string VendorAddress { get; set; }
        /// <summary>
        /// 供应商电话
        /// </summary>
        [DisplayName("供应商电话")]
        public string VendorTel { get; set; }
        /// <summary>
        /// 供应商传真
        /// </summary>
        [DisplayName("供应商传真")]
        public string VendorFax { get; set; }
        /// <summary>
        /// 供应商Email
        /// </summary>
        [DisplayName("供应商Email")]
        public string VendorEmail { get; set; }
        /// <summary>
        /// 供应商备注
        /// </summary>
        [DisplayName("供应商备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
