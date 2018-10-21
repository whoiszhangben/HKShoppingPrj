using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.DAL
{
    public interface IProfileDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        Task<int> Insert(Profile model);
        #endregion

        #region 查
        Task<List<Profile>> GetList();
        #endregion

        string GenerateBillNo();
    }
}
