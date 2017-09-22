using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKShoppingManage.Model
{
    /// <summary>
    /// 管理端操作日志模型
    /// </summary>
    public class OperationLog
    {
        /// <summary>
        /// 唯一Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public int LogType { get; set; }
        public string LogTypeName { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string ActionType
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationDate { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        public string AssetCode { get; set; }
    }
}
