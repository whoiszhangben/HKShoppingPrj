﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HKShoppingManage.Model
{
    public class Product
    {
        /// <summary>
        /// 自动增长Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DisplayName("产品名称")]
        public string Name { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        [DisplayName("产品描述")]
        public string Description { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [DisplayName("产品助记码")]
        public string HelpCode { get; set; }
        /// <summary>
        /// 产品类别ID
        /// </summary>
        [DisplayName("产品类别")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 产品类别
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        [DisplayName("库存数量")]
        public decimal StockQty { get; set; }
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
