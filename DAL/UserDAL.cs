using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using HKShoppingManage.Model;
using HKShoppingManage.DBUtility;
using HKShoppingManage.Common;
using Dapper;

namespace HKShoppingManage.DAL
{
    public class UserDAL : IUserDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(User model)
        {
            int result = 0;
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    StringBuilder strSql = new StringBuilder();
                    var param = new DynamicParameters();

                    #region 用户信息
                    strSql.Append("insert into Users(");
                    strSql.Append("UserName,LoginCode,LoginPwd,IsValid,LastLoginIP,LastLoginTime,CreateTime,Creator,UpdateTime,Updator)");

                    strSql.Append(" values (");
                    strSql.Append("@UserName,@LoginCode,@LoginPwd,@IsValid,@LastLoginIP,@LastLoginTime,@CreateTime,@Creator,@UpdateTime,@Updator)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@UserName", model.UserName, dbType: DbType.String);
                    param.Add("@LoginCode", model.LoginCode, dbType: DbType.String);
                    param.Add("@LoginPwd", model.LoginPwd, dbType: DbType.String);
                    param.Add("@IsValid", model.IsValid, dbType: DbType.Boolean);
                    param.Add("@LastLoginIP", model.LastLoginIP, dbType: DbType.String);
                    param.Add("@LastLoginTime", model.LastLoginTime, dbType: DbType.DateTime);
                    param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                    param.Add("@Creator", model.Creator, dbType: DbType.String);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Creator, dbType: DbType.String);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    result = param.Get<int>("@returnid");
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增用户出错", ex);
                    tran.Rollback();
                }
                finally
                {
                    if (tran != null)
                        tran.Dispose();
                    if (conn != null)
                        conn.Close();
                }
            }
            
            return result;
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
            bool result = true;
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    StringBuilder strSql = new StringBuilder();
                    var param = new DynamicParameters();
                    param.Add("@Id", Id, dbType: DbType.Int32);

                    strSql.Clear();
                    strSql.Append("delete from Users where Id=@Id;");
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    LogHelper.Log.WriteError("[回滚]删除用户出错", ex);
                    tran.Rollback();
                }
                finally
                {
                    if (tran != null)
                        tran.Dispose();
                    if (conn != null)
                        conn.Close();
                }
            }
            
            return result;
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
            int result = 0;
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    StringBuilder strSql = new StringBuilder();
                    var param = new DynamicParameters();

                    #region 用户信息
                    strSql.Append("update Users set ");
                    strSql.Append("UserName=@UserName,");
                    strSql.Append("IsValid=@IsValid,");
                    strSql.Append("LastLoginIP=@LastLoginIP,");
                    strSql.Append("LastLoginTime=@LastLoginTime,");
                    strSql.Append("UpdateTime=@UpdateTime,");
                    strSql.Append("Updator=@Updator");
                    strSql.Append(" where Id=@Id ");

                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@UserName", model.UserName, dbType: DbType.String);
                    param.Add("@IsValid", model.IsValid, dbType: DbType.Boolean);
                    param.Add("@LastLoginIP", model.LastLoginIP, dbType: DbType.String);
                    param.Add("@LastLoginTime", model.LastLoginTime, dbType: DbType.DateTime);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Updator, dbType: DbType.String);
                    result = await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]修改用户出错", ex);
                    tran.Rollback();
                }
                finally
                {
                    if (tran != null)
                        tran.Dispose();
                    if (conn != null)
                        conn.Close();
                }
            }
            
            return result > 0;
        }

        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public async Task<bool> UpdateLoginInfo(int Id, string loginIP)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users set ");
            strSql.Append("LastLoginTime=now(),LastLoginIP=@LastLoginIP");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                param.Add("@LastLoginIP", loginIP, dbType: DbType.String);
                result = await conn.ExecuteAsync(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns></returns>
        public async Task<bool> ResetPwd(User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users set ");
            strSql.Append("LoginPwd=@LoginPwd,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Updator=@Updator");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@LoginPwd", model.LoginPwd, dbType: DbType.String);
                param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                param.Add("@Updator", model.Updator, dbType: DbType.String);
                result = await conn.ExecuteAsync(strSql.ToString(), param);
            }
            return result > 0;
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Users where LoginCode=@LoginCode ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@LoginCode", loginCode, dbType: DbType.String);
                var temp = await conn.QueryAsync<int>(strSql.ToString(), param);
                result = temp.FirstOrDefault();
            }
            return result > 0;
        }

        /// <summary>
        /// 根据登录账号查询用户信息
        /// </summary>
        /// <param name="loginCode">登录账号</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        public async Task<User> GetModel(string loginCode, string loginPwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserName,LoginCode,LoginPwd,IsValid,LastLoginIP,LastLoginTime from Users");
            strSql.Append(" where LoginCode=@LoginCode and LoginPwd=@LoginPwd limit 1");

            User model = null;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@LoginCode", loginCode, dbType: DbType.String);
                param.Add("@LoginPwd", loginPwd, dbType: DbType.String);
                var temp = await conn.QueryAsync<User>(strSql.ToString(), param);
                model = temp.FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<User> GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserName,LoginCode,LoginPwd,IsValid,LastLoginIP,LastLoginTime from Users ");
            strSql.Append(" where Id=@Id ");

            User model = null;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                var temp = await conn.QueryAsync<User>(strSql.ToString(), param);
                model = temp.FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserName,LoginCode,LoginPwd,IsValid,LastLoginIP,LastLoginTime ");
            strSql.Append(" from Users ");
            strSql.Append(" order by Creator desc ");

            List<User> list = new List<User>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<User>(strSql.ToString());
                list = tempList.ToList();
            }
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
            int totalCount = 0;
            List<User> result = new List<User>();
            var param = new DynamicParameters();

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@$TableName", "Users", dbType: DbType.String);
                param.Add("@$PrimaryKey", "Id", dbType: DbType.String);
                param.Add("@$OrderStr", "CreateDate", dbType: DbType.String);
                param.Add("@$SortType", 2, dbType: DbType.Int32);
                param.Add("@$RecorderCount", 0, dbType: DbType.Int32);
                param.Add("@$PageIndex", pageIndex, dbType: DbType.Int32);
                param.Add("@$PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@$FieldList", "Id,UserName,LoginCode,LoginPwd,IsValid,LastLoginIP,LastLoginTime", dbType: DbType.String);
                if (string.IsNullOrEmpty(userName))
                {
                    param.Add("@$WhereStr", "", dbType: DbType.String);
                }
                else
                {
                    //过滤危险字段，如单引号等
                    string key = userName.Replace("'", "''");
                    param.Add("@$WhereStr", string.Format(" and UserName like '%{0}%'", key), dbType: DbType.String);
                }
                param.Add("@$TotalPageCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@$TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var temp = await conn.QueryAsync<User>("P_viewPage", param, commandType: CommandType.StoredProcedure);
                result = temp.ToList();
                totalCount = param.Get<int>("@$TotalCount");
            }

            return new PagedList<User>(result, pageIndex, pageSize, totalCount);
        }
        #endregion
    }
}
