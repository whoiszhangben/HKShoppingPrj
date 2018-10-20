using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.DAL;
using HKShoppingManage.Model;

namespace HKShoppingManage.BLL
{
    public class CategoryBLL : ICategoryBLL
    {
        private ICategoryDAL dal;
        public CategoryBLL(ICategoryDAL _dal)
        {
            this.dal = _dal;
        }
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Category model)
        {
            return await this.dal.Insert(model);
        }
        #endregion

        #region 删
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        public async Task<bool> Delete(int Id)
        {
            return await this.dal.Delete(Id);
        }
        #endregion

        #region 改
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(Category model)
        {
            return await this.dal.Update(model);
        }
        #endregion

        #region 查
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="AssetCode"></param>
        /// <returns></returns>
        public async Task<bool> IsExists(string CatalogName)
        {
            return await this.dal.IsExists(CatalogName);
        }


        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Category> GetModel(int Id)
        {
            return await this.dal.GetModel(Id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> GetList()
        {
            return await this.dal.GetList();
        }

        public async Task<List<KeyValuePair<int, List<Category>>>> GetAllList()
        {
            return await this.dal.GetAllList();
        }
        #endregion
    }
}
