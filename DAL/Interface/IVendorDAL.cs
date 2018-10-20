using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.DAL
{
    public interface IVendorDAL
    {
        #region 增
        Task<int> Insert(Vendor model);
        #endregion

        #region 查
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<Vendor>> GetList();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="ProductName">产品名称</param>
        /// <returns></returns>
        Task<PagedList<Vendor>> GetPageList(int pageIndex, int pageSize);

        #endregion
    }
}
