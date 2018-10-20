using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;
using HKShoppingManage.DAL;

namespace HKShoppingManage.BLL
{
    public class ProductBLL:IProductBLL
    {
        private IProductDAL dal;
        public ProductBLL(IProductDAL _dal)
        {
            this.dal = _dal;
        }
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Product model)
        {
            return await this.dal.Insert(model);
        }
        #endregion

        #region 查
        public async Task<List<Product>> GetList()
        {
            return await this.dal.GetList();
        }

        public async Task<PagedList<Product>> GetPageList(int pageIndex, int pageSize, string ProductName)
        {
            return await this.dal.GetPageList(pageIndex, pageSize, ProductName);
        }
        #endregion
    }
}
