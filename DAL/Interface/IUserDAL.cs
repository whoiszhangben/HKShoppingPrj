using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;

namespace HKShoppingManage.DAL
{
    public interface IUserDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        Task<int> Insert(User model);
        #endregion

        #region 删
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        Task<bool> Delete(int Id);
        #endregion

        #region 改
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        Task<bool> Update(User model);

        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        Task<bool> UpdateLoginInfo(int Id, string loginIP);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        Task<bool> ResetPwd(User model);
        #endregion

        #region 查
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="loginCode"></param>
        /// <returns></returns>
        Task<bool> IsExists(string loginCode);

        /// <summary>
        /// 根据登录账号查询用户信息
        /// </summary>
        /// <param name="loginCode">账号</param>
        /// <param name="loginPwd">密码</param>
        /// <returns></returns>
        Task<User> GetModel(string loginCode, string loginPwd);

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<User> GetModel(int Id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetList();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="userName">用户名称</param>
        /// <returns></returns>
        Task<PagedList<User>> GetPageList(int pageIndex, int pageSize, string userName);
        #endregion
    }
}
