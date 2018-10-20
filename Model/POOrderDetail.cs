using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKShoppingManage.Model
{
    public class POOrderDetail
    {
        /// <summary>
        /// 采购订单内码
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 采购订单分录ID
        /// </summary>
        public int EntryId { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// 产品单价$
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public float ExRate { get; set; }
        /// <summary>
        /// 总金额$
        /// </summary>
        public float Amount { get; set; }
        /// <summary>
        /// 总金额￥
        /// </summary>
        public float AmountRMB { get; set; }
        /// <summary>
        /// 单个采购项的状态 待采购0、已采购1
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
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
