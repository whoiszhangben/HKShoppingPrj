using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKShoppingManage.Model
{
    public class AssetInfoDTO
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
        /// 资产状态名称
        /// </summary>
        public string AssetStatusName { get; set; }
    }
}
