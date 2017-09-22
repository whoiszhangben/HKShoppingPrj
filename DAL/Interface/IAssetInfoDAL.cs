using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.DAL
{
    public interface IAssetInfoDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        Task<int> Insert(AssetInfo model);
        #endregion

        #region 删
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        Task<bool> Delete(int Id);
        #endregion

        #region 改
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        Task<bool> Update(AssetInfo model);
        #endregion


        #region 查
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="AssetCode"></param>
        /// <returns></returns>
        Task<bool> IsExists(string AssetCode);


        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<AssetInfo> GetModel(int Id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<AssetInfo>> GetList();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="AssetType">资产类型</param>
        /// /// <param name="AssetStatus">资产状态</param>
        /// <returns></returns>
        Task<PagedList<AssetInfoDTO>> GetPageList(int pageIndex, int pageSize, int AssetType,int AssetStatus,int AssetDelete,string AssetCode);
        #endregion
    }
}
