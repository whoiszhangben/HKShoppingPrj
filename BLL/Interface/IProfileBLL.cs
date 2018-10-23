using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.BLL
{
    public interface IProfileBLL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        Task<int> Insert(Profile model);
        #endregion

        #region
        Task<List<Profile>> GetList(string name, string idNo, string telNo, int isDimissioned);

        Task<PagedList<Profile>> GetListByConditions(int pageIndex, int pageSize, string name, string idNo, string telNo, int isDimissioned);
        #endregion
        String GenerateBillNo();

        Task<bool> Delete(string profileNo);
    }
}
