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
    public class CustomerDAL : ICustomerDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Customer model)
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
                    strSql.Append("insert into Customer(");
                    strSql.Append("CusName,HelpCode,CusGender,CusTelephone,CusAddress,CusRemark,IsValid,IsDeleted,CreateTime,Creator,UpdateTime,Updator)");

                    strSql.Append(" values (");
                    strSql.Append("@CusName,@HelpCode,@CusGender,@CusTelephone,@CusAddress,@CusRemark,@IsValid,@IsDeleted,@CreateTime,@Creator,@UpdateTime,@Updator)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@CusName", model.CusName, dbType: DbType.String);
                    param.Add("@HelpCode", model.HelpCode, dbType: DbType.String);
                    param.Add("@CusGender", model.CusGender, dbType: DbType.Boolean);
                    param.Add("@CusTelephone", model.CusTelephone, dbType: DbType.String);
                    param.Add("@CusAddress", model.CusAddress, dbType: DbType.String);
                    param.Add("@CusRemark", model.CusRemark, dbType: DbType.String);
                    param.Add("@IsValid", model.IsValid, dbType: DbType.Boolean);
                    param.Add("@IsDeleted", model.IsDeleted, dbType: DbType.Boolean);
                    param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                    param.Add("@Creator", model.Creator, dbType: DbType.String);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Updator, dbType: DbType.String);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    result = param.Get<int>("@returnid");
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增客户出错", ex);
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
        /// <param name="Id">Id</param>
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
                    strSql.Append("Update Customer set IsDeleted=1 where Id=@Id;");
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    LogHelper.Log.WriteError("[回滚]删除客户出错", ex);
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
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(Customer model)
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
                    strSql.Append("update Customer set ");
                    strSql.Append("CusName=@CusName,");
                    strSql.Append("HelpCode=@HelpCode,");
                    strSql.Append("CusGender=@CusGender,");
                    strSql.Append("CusTelephone=@CusTelephone,");
                    strSql.Append("CusAddress=@CusAddress,");
                    strSql.Append("CusRemark=@CusRemark,");
                    strSql.Append("IsValid=@IsValid,");
                    strSql.Append("IsDeleted=@IsDeleted,");
                    strSql.Append("UpdateTime=@UpdateTime,");
                    strSql.Append("Updator=@Updator ");
                    strSql.Append(" where id=@Id ");

                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@CusName", model.CusName, dbType: DbType.String);
                    param.Add("@HelpCode", model.HelpCode, dbType: DbType.String);
                    param.Add("@CusGender", model.CusGender, dbType: DbType.Boolean);
                    param.Add("@CusTelephone", model.CusTelephone, dbType: DbType.String);
                    param.Add("@CusAddress", model.CusAddress, dbType: DbType.String);
                    param.Add("@CusRemark", model.CusRemark, dbType: DbType.String);
                    param.Add("@IsValid", model.IsValid, dbType: DbType.Boolean);
                    param.Add("@IsDeleted", model.IsDeleted, dbType: DbType.Boolean);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Updator, dbType: DbType.String);
                    result = await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]修改客户出错", ex);
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
        #endregion

        #region 查
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="AssetCode"></param>
        /// <returns></returns>
        public async Task<bool> IsExists(string CusName,string CusTelephone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Customer where CusName=@CusName and CusTelephone=@CusTelephone and IsDeleted=0 ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@CusName", CusName, dbType: DbType.String);
                param.Add("@CusTelephone", CusTelephone, dbType: DbType.String);
                var temp = await conn.QueryAsync<int>(strSql.ToString(), param);
                result = temp.FirstOrDefault();
            }
            return result > 0;
        }


        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Customer> GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Customer where id=@Id");

            Customer model = null;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                var temp = await conn.QueryAsync<Customer>(strSql.ToString(), param);
                model = temp.FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Customer where IsDeleted=0 ");

            List<Customer> list = new List<Customer>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Customer>(strSql.ToString());
                list = tempList.ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="AssetType">资产类型</param>
        /// /// <param name="AssetStatus">资产状态</param>
        /// <returns></returns>
        public async Task<PagedList<Customer>> GetPageList(int pageIndex, int pageSize, string CustomerName, string Telephone)
        {
            int totalCount = 0;
            List<Customer> result = new List<Customer>();
            var param = new DynamicParameters();

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[Customer]", dbType: DbType.String);
                param.Add("@PK", "[Id]", dbType: DbType.String);
                param.Add("@Sort", "[Id]", dbType: DbType.String);
                param.Add("@PageNumber", pageIndex, dbType: DbType.Int32);
                param.Add("@PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@Fields", "Id,CusName,HelpCode,CusGender,CusTelephone,CusAddress,CusRemark,IsValid,CreateTime,Creator,UpdateTime,Updator ", dbType: DbType.String);
                string strFilter = string.Empty;
                
                if (!string.IsNullOrEmpty(CustomerName))
                {
                    strFilter += string.Format(" and CusName like '%{0}%' ", CustomerName);
                }
                if (!string.IsNullOrEmpty(Telephone))
                {
                    strFilter += string.Format(" and CusTelephone like '%{0}%' ", Telephone);
                }
                param.Add("@Filter", strFilter, dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var temp = await conn.QueryAsync<Customer>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                result = temp.ToList();
                totalCount = param.Get<int>("@Total");
            }

            return new PagedList<Customer>(result, pageIndex, pageSize, totalCount);
        }
        #endregion
    }
}
