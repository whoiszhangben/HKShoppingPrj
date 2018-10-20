using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.DAL;
using HKShoppingManage.Model;

namespace HKShoppingManage.BLL
{
    public class ProfileBLL : IProfileBLL
    {
        private IProfileDAL dal;

        public ProfileBLL(IProfileDAL _dal)
        {
            this.dal = _dal;
        }
        public async Task<int> Insert(Profile model)
        {
            return await this.dal.Insert(model);
        }
        public async Task<List<Profile>> GetList()
        {
            return await this.dal.GetList();
        }
    }
}
