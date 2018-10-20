using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;
using HKShoppingManage.DAL;

namespace HKShoppingManage.BLL
{
    public class POOrderBLL:IPOOrderBLL
    {
        private IPOOrderDAL dal;
        public POOrderBLL(IPOOrderDAL _dal)
        {
            this.dal = _dal;
        }
        public async Task<int> Insert(POOrder model)
        {
            return await this.dal.Insert(model);
        }
        public async Task<int> Update(POOrder model)
        {
            return await this.dal.Update(model);
        }
        public POOrder GetModel(int Id)
        {
            return this.dal.GetModel(Id);
        }
        public async Task<List<POOrder>> GetList()
        {
            return await this.dal.GetList();
        }
        public async Task<PagedList<POOrder>> GetPageList(int pageIndex, int pageSize)
        {
            return await this.dal.GetPageList(pageIndex, pageSize);
        }
        public string GetBillNo()
        {
            return this.dal.GetBillNo();
        }

        public async Task<bool> Delete(int Id)
        {
            return await this.dal.Delete(Id);
        }

        public async Task<bool> Confirm(int Id)
        {
            return await this.dal.Confirm(Id);
        }
    }
}
