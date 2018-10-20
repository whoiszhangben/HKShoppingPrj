using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.DAL
{
    public interface IPOOrderDAL
    {
        #region 增
        Task<int> Insert(POOrder model);
        #endregion

        #region 改
        Task<int> Update(POOrder model);
        #endregion

        #region 查
        /// <summary>
        /// 根据订单ID查询订单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        POOrder GetModel(int Id);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<POOrder>> GetList();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="ProductName">产品名称</param>
        /// <returns></returns>
        Task<PagedList<POOrder>> GetPageList(int pageIndex, int pageSize);

        string GetBillNo();
        #endregion

        #region 删
        Task<bool> Delete(int Id);
        #endregion

        #region 确认订单
        Task<bool> Confirm(int Id);
        #endregion
    }
}
