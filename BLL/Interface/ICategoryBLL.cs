using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.BLL
{
    public interface ICategoryBLL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        Task<int> Insert(Category model);
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
        Task<bool> Update(Category model);
        #endregion

        #region 查
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="CatalogName"></param>
        /// <returns></returns>
        Task<bool> IsExists(string CatalogName);


        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Category> GetModel(int Id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetList();
        /// <summary>
        /// 获取层级关系列表
        /// </summary>
        /// <returns></returns>
        Task<List<KeyValuePair<int, List<Category>>>> GetAllList();
        #endregion
    }
}
