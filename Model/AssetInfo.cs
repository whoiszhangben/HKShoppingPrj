using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKShoppingManage.Model
{
    public class AssetInfo
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 资产编码
        /// </summary>
        public string AssetCode { get; set; }
        /// <summary>
        /// 资产类型
        /// </summary>
        public int AssetType { get; set; }
        /// <summary>
        /// 资产类型名称
        /// </summary>
        public string AssetTypeName { get; set; }
        /// <summary>
        /// 硬件序列号
        /// </summary>
        public string HDSeriesNo { get; set; }
        /// <summary>
        /// 资产配置明细
        /// </summary>
        public string AssetConfigDetails { get; set; }
        /// <summary>
        /// 购买日期
        /// </summary>
        public string PurchaseDate { get; set; }
        /// <summary>
        /// 报废日期
        /// </summary>
        public string DiscardDate { get; set; }
        /// <summary>
        /// 使用者
        /// </summary>
        public string CurrentUser { get; set; }
        /// <summary>
        /// 使用者Id
        /// </summary>
        public int CurrentUserId { get; set; }
        /// <summary>
        /// 所属部门Id
        /// </summary>
        public int Dept { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 所属分部Id
        /// </summary>
        public int SubDept { get; set; }
        /// <summary>
        /// 所属分部名称
        /// </summary>
        public string SubDeptName { get; set; }
        /// <summary>
        /// 资产状态 1、使用 2、闲置 3、借用 4、报废
        /// </summary>
        public int AssetStatus { get; set; }
        /// <summary>
        /// 资产状态名称
        /// </summary>
        public string AssetStatusName { get; set; }
        /// <summary>
        /// 是否删除标志
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 备注IP
        /// </summary>
        public string RemarkIP { get; set; }
        /// <summary>
        /// 备注地址
        /// </summary>
        public string RemarkAddr { get; set; }
        /// <summary>
        /// 扩展字段1
        /// </summary>
        public string Ext1 { get; set; }
        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string Ext2 { get; set; }
        /// <summary>
        /// 扩展字段3
        /// </summary>
        public string Ext3 { get; set; }
        /// <summary>
        /// 扩展字段4
        /// </summary>
        public string Ext4 { get; set; }
    }
}
