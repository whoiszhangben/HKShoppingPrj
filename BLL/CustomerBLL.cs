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
    public class CustomerBLL : ICustomerBLL
    {
        private ICustomerDAL dal;
        public CustomerBLL(ICustomerDAL _dal)
        {
            this.dal = _dal;
        }
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Customer model)
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
        public async Task<bool> Update(Customer model)
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
        public async Task<bool> IsExists(string CustomerName, string Telephone)
        {
            return await this.dal.IsExists(CustomerName, Telephone);
        }


        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Customer> GetModel(int Id)
        {
            return await this.dal.GetModel(Id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetList()
        {
            return await this.dal.GetList();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="Telephone">客户电话</param>
        /// <returns></returns>
        public async Task<PagedList<Customer>> GetPageList(int pageIndex, int pageSize, string CustomerName, string Telephone)
        {
            return await this.dal.GetPageList(pageIndex, pageSize, CustomerName, Telephone);
        }
        #endregion
    }
}
