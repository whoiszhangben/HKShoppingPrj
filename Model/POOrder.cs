using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HKShoppingManage.Model
{
    public class POOrder
    {
        public POOrder()
        {
            OrderDetails = new List<POOrderDetail>();
        }
        /// <summary>
        /// 采购订单自增长ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 采购订单编号
        /// </summary>
        [DisplayName("订单编号")]
        public string POOrderNo { get; set; }
        /// <summary>
        /// 当前汇率
        /// </summary>
        [DisplayName("默认汇率")]
        public float ExchangeRate { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        [DisplayName("总金额")]
        public float TotalAmount { get; set; }
        /// <summary>
        /// 订单分录
        /// </summary>
        public List<POOrderDetail> OrderDetails { get; set; }
        /// <summary>
        /// 订单状态 
        /// </summary>
        public int OrderState { get; set; }
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
