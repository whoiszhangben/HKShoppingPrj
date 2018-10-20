using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;
using HKShoppingManage.DAL;

namespace HKShoppingManage.BLL
{
    public class VendorBLL:IVendorBLL
    {
        private IVendorDAL dal;
        public VendorBLL(IVendorDAL _dal)
        {
            this.dal = _dal;
        }
        public async Task<int> Insert(Vendor model)
        {
            return await this.dal.Insert(model);
        }
        public async Task<List<Vendor>> GetList()
        {
            return await this.dal.GetList();
        }
        public async Task<PagedList<Vendor>> GetPageList(int pageIndex, int pageSize)
        {
            return await this.dal.GetPageList(pageIndex, pageSize);
        }
    }
}
