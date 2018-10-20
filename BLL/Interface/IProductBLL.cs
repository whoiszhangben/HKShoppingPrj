using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.BLL
{
    public interface IProductBLL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        Task<int> Insert(Product model);
        #endregion

        #region 查
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetList();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="ProductName">产品名称</param>
        /// <returns></returns>
        Task<PagedList<Product>> GetPageList(int pageIndex, int pageSize, string ProductName);
        #endregion
    }
}
