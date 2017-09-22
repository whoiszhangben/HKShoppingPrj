using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.BLL
{
    public interface IOperationLogBLL
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        Task<int> Insert(OperationLog model);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="userName">用户名称</param>
        /// <returns></returns>
        Task<PagedList<OperationLog>> GetPageList(int pageIndex, int pageSize, DateTime? startDate, DateTime? endDate, int? logType, string AssetCode);
    }
}
