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
    public class UserBLL : IUserBLL
    {
        private IUserDAL dal;
        public UserBLL(IUserDAL dal)
        {
            this.dal = dal;
        }
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(User model)
        {
            return await this.dal.Insert(model);
        }
        #endregion

        #region 删
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="Id">用户Id</param>
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
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(User model)
        {
            return await this.dal.Update(model);
        }

        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public async Task<bool> UpdateLoginInfo(int Id, string loginIP)
        {
            return await this.dal.UpdateLoginInfo(Id, loginIP);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        public async Task<bool> ResetPwd(User model)
        {
            return await this.dal.ResetPwd(model);
        }
        #endregion

        #region 查
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="loginCode"></param>
        /// <returns></returns>
        public async Task<bool> IsExists(string loginCode)
        {
            return await this.dal.IsExists(loginCode);
        }

        /// <summary>
        /// 根据登录账号查询用户信息
        /// </summary>
        /// <param name="loginCode">登录账号</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        public async Task<User> GetModel(string loginCode, string loginPwd)
        {
            var model = await this.dal.GetModel(loginCode, loginPwd);
            return model;
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<User> GetModel(int Id)
        {
            var model = await this.dal.GetModel(Id);
            return model;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetList()
        {
            var list = await this.dal.GetList();
            return list;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="userName">用户名称</param>
        /// <returns></returns>
        public async Task<PagedList<User>> GetPageList(int pageIndex, int pageSize, string userName)
        {
            var list = await this.dal.GetPageList(pageIndex, pageSize, userName);
            return list;
        }
        #endregion

    }
}
