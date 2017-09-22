using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;
using HKShoppingManage.Common;
using HKShoppingManage.DAL;

namespace HKShoppingManage.BLL
{
    public class OperationLogBLL : IOperationLogBLL
    {
        private IOperationLogDAL dal;
        public OperationLogBLL(IOperationLogDAL dal)
        {
            this.dal = dal;
        }
        public async Task<int> Insert(OperationLog model)
        {
            return await this.dal.Insert(model);
        }
        public async Task<PagedList<OperationLog>> GetPageList(int pageIndex, int pageSize, DateTime? startDate, DateTime? endDate, int? logType, string AssetCode)
        {
            var list = await this.dal.GetPageList(pageIndex, pageSize, startDate, endDate, logType, AssetCode);
            return list;
        }
    }
}
