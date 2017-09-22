using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.DAL
{
    public interface IOperationLogDAL
    {
        Task<int> Insert(OperationLog model);
        Task<PagedList<OperationLog>> GetPageList(int pageIndex, int pageSize, DateTime? startDate, DateTime? endDate, int? logType, string AssetCode);
    }
}
