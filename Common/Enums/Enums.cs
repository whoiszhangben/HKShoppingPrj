using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKShoppingManage.Common
{
    public enum OperationLogType
    {
        /// <summary>
        /// 1 新增账号
        /// </summary>
        [Description("新增账号")]
        Account_Add = 1,
        /// <summary>
        /// 2 修改账号
        /// </summary>
        [Description("修改账号")]
        Account_Edit = 2,
        /// <summary>
        /// 3 删除账号
        /// </summary>
        [Description("删除账号")]
        Account_Del = 3,
        /// <summary>
        /// 4 新增汇率
        /// </summary>
        [Description("新增汇率")]
        ExRate_Add = 4,
        /// <summary>
        /// 5 修改汇率
        /// </summary>
        [Description("修改汇率")]
        ExRate_Edit = 5,
        /// <summary>
        /// 6 删除汇率
        /// </summary>
        [Description("删除汇率")]
        ExRate_Del = 6,
        /// <summary>
        /// 7 新增周计划
        /// </summary>
        [Description("新增周计划")]
        WeekPlan_Add = 7,
        /// <summary>
        /// 8 修改周计划
        /// </summary>
        [Description("修改周计划")]
        WeekPlan_Edit = 8,
        /// <summary>
        /// 9 删除周计划
        /// </summary>
        [Description("删除周计划")]
        WeekPlan_Del = 9,
        /// <summary>
        /// 10 新增品种
        /// </summary>
        [Description("新增品种")]
        OriginalVariety_Add = 10,
        /// <summary>
        /// 11 修改品种
        /// </summary>
        [Description("修改品种")]
        OriginalVariety_Edit = 11,
        /// <summary>
        /// 12 删除品种
        /// </summary>
        [Description("删除品种")]
        OriginalVariety_Del = 12,
        /// <summary>
        /// 13 新增公式
        /// </summary>
        [Description("新增公式")]
        Formula_Add = 13,
        /// <summary>
        /// 14 修改公式
        /// </summary>
        [Description("修改公式")]
        Formula_Edit = 14,
        /// <summary>
        /// 15 删除公式
        /// </summary>
        [Description("删除公式")]
        Formula_Del = 15,
        /// <summary>
        /// 16 启用公式
        /// </summary>
        [Description("启用公式")]
        Formula_Enable = 16,
        /// <summary>
        /// 17 停用公式
        /// </summary>
        [Description("停用公式")]
        Formula_Disable = 17,
        /// <summary>
        /// 18 生成数据
        /// </summary>
        [Description("生成数据")]
        DataWarehouse_Create = 18,
        /// <summary>
        /// 19 数据生效
        /// </summary>
        [Description("数据生效")]
        DataWarehouse_Enable = 19,
        /// <summary>
        /// 20 删除数据
        /// </summary>
        [Description("删除数据")]
        DataWarehouse_Delete = 20,
        /// <summary>
        /// 21 新增通道
        /// </summary>
        [Description("新增通道")]
        Channel_Add = 21,
        /// <summary>
        /// 22 修改通道
        /// </summary>
        [Description("修改通道")]
        Channel_Edit = 22,
        /// <summary>
        /// 23 删除通道
        /// </summary>
        [Description("删除通道")]
        Channel_Del = 23,
        /// <summary>
        /// 24 新增DST
        /// </summary>
        [Description("新增DST")]
        DST_Add = 24,
        /// <summary>
        /// 25 修改DST
        /// </summary>
        [Description("修改DST")]
        DST_Edit = 25,
        /// <summary>
        /// 26 删除DST
        /// </summary>
        [Description("删除DST")]
        DST_Del = 26,
        /// <summary>
        /// 27 新增商品组
        /// </summary>
        [Description("新增商品组")]
        GoodsGroup_Add = 27,
        /// <summary>
        /// 28 修改商品组
        /// </summary>
        [Description("修改商品组")]
        GoodsGroup_Edit = 28,
        /// <summary>
        /// 29 删除商品组
        /// </summary>
        [Description("删除商品组")]
        GoodsGroup_Del = 29,
        /// <summary>
        /// 30 新增商品
        /// </summary>
        [Description("新增商品")]
        Goods_Add = 30,
        /// <summary>
        /// 31 修改商品
        /// </summary>
        [Description("修改商品")]
        Goods_Edit = 31,
        /// <summary>
        /// 32 删除商品
        /// </summary>
        [Description("删除商品")]
        Goods_Del = 32,
        /// <summary>
        /// 33 新增汇率组
        /// </summary>
        [Description("新增汇率组")]
        ExRateGroup_Add = 33,
        /// <summary>
        /// 34 修改汇率组
        /// </summary>
        [Description("修改汇率组")]
        ExRateGroup_Edit = 34,
        /// <summary>
        /// 35 删除汇率组
        /// </summary>
        [Description("删除汇率组")]
        ExRateGroup_Del = 35
    }
}
