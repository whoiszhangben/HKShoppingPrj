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
        public async Task<List<Profile>> GetList(string name, string idNo, string telNo, int isDimissioned)
        {
            return await this.dal.GetList(name, idNo, telNo, isDimissioned);
        }

        public async Task<PagedList<Profile>> GetListByConditions(int pageIndex, int pageSize, string name, string idNo, string telNo, int isDimissioned)
        {
            return await this.dal.GetListByConditions(pageIndex, pageSize, name, idNo, telNo, isDimissioned);
        }
        public string GenerateBillNo()
        {
            return this.dal.GenerateBillNo();
        }

        public async Task<bool> Delete(string profileNo)
        {
            return await this.dal.Delete(profileNo);
        }
    }
}
