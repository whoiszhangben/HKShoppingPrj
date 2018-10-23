using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HKShoppingManage.Model;
using HKShoppingManage.DBUtility;
using HKShoppingManage.Common;
using Dapper;

namespace HKShoppingManage.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Profile model)
        {
            int result = 0;
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    #region Profiles
                    StringBuilder strSql = new StringBuilder();
                    var param = new DynamicParameters();
                    if (model.Flag == 0)
                    {
                        strSql.Append("insert into Profiles(");
                        strSql.Append("ProfileNo,EmpName,EmpIDNo,EmpTelNo,IsDimissioned,RelationVal,CreateTime,Creator,UpdateTime,Updator)");

                        strSql.Append(" values (");
                        strSql.Append("@ProfileNo,@EmpName,@EmpIDNo,@EmpTelNo,@IsDimissioned,@RelationVal,@CreateTime,@Creator,@UpdateTime,@Updator)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ProfileNo", model.ProfileNo, dbType: DbType.String);
                        param.Add("@EmpName", model.EmpName, dbType: DbType.String);
                        param.Add("@EmpIDNo", model.EmpIDNo, dbType: DbType.String);
                        param.Add("@EmpTelNo", model.EmpTelNo, dbType: DbType.String);
                        param.Add("@IsDimissioned", model.IsDimissioned, dbType: DbType.Boolean);
                        param.Add("@RelationVal", model.RelationVal, dbType: DbType.Int32);
                        param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                        param.Add("@Creator", model.Creator, dbType: DbType.String);
                        param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                        param.Add("@Updator", model.Updator, dbType: DbType.String);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        await conn.ExecuteAsync(strSql.ToString(), param, tran);
                        result = param.Get<int>("@returnid");
                    }
                    else
                    {
                        strSql.Append("Update Profiles set EmpName = @EmpName, EmpIDNo = @EmpIDNo, EmpTelNo = @EmpTelNo, IsDimissioned = @IsDimissioned, RelationVal = @RelationVal where ProfileNo = @ProfileNo ");
                        param.Add("@ProfileNo", model.ProfileNo, dbType: DbType.String);
                        param.Add("@EmpName", model.EmpName, dbType: DbType.String);
                        param.Add("@EmpIDNo", model.EmpIDNo, dbType: DbType.String);
                        param.Add("@EmpTelNo", model.EmpTelNo, dbType: DbType.String);
                        param.Add("@IsDimissioned", model.IsDimissioned, dbType: DbType.Boolean);
                        param.Add("@RelationVal", model.RelationVal, dbType: DbType.Int32);
                        param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                        param.Add("@Updator", model.Updator, dbType: DbType.String);
                        result = await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    }
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增或更新档案出错", ex);
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
        #region
        public async Task<List<Profile>> GetList(string name, string idNo, string telNo, int isDimissioned)
        {
            StringBuilder strFilter = new StringBuilder();
            var param = new DynamicParameters();
            strFilter.Append(" And IsDimissioned = " + isDimissioned.ToString());

            if (!string.IsNullOrEmpty(name))
            {
                strFilter.Append(" and EmpName like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(idNo))
            {
                strFilter.Append(" and EmpIdNo like '%" + idNo + "%' ");
            }
            if (!string.IsNullOrEmpty(telNo))
            {
                strFilter.Append(" and EmpTelNo like '%" + telNo + "%' ");
            }
            List<Profile> list = new List<Profile>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[Profiles]", dbType: DbType.String);
                param.Add("@PK", "[Id]", dbType: DbType.String);
                param.Add("@Sort", "[CreateTime]", dbType: DbType.String);
                param.Add("@PageNumber", 1, dbType: DbType.Int32);
                param.Add("@PageSize", 10000, dbType: DbType.Int32);
                param.Add("@Fields", "ProfileNo,EmpName,EmpIDNo,EmpTelNo,RelationVal ", dbType: DbType.String);
                param.Add("@Filter", strFilter.ToString(), dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var temp = await conn.QueryAsync<Profile>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                list = temp.ToList();
            }
            return list;
        }

        public async Task<PagedList<Profile>> GetListByConditions(int pageIndex, int pageSize, string name, string idNo, string telNo, int isDimissioned)
        {
            int totalCount = 0;
            StringBuilder strFilter = new StringBuilder();
            var param = new DynamicParameters();
            strFilter.Append(" And IsDimissioned = " + isDimissioned.ToString() );

            if (!string.IsNullOrEmpty(name))
            {
                strFilter.Append(" and EmpName like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(idNo))
            {
                strFilter.Append(" and EmpIdNo like '%" + idNo + "%' ");
            }
            if (!string.IsNullOrEmpty(telNo))
            {
                strFilter.Append(" and EmpTelNo like '%" + telNo + "%' ");
            }
            List<Profile> list = new List<Profile>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[Profiles]", dbType: DbType.String);
                param.Add("@PK", "[Id]", dbType: DbType.String);
                param.Add("@Sort", "[CreateTime]", dbType: DbType.String);
                param.Add("@PageNumber", pageIndex, dbType: DbType.Int32);
                param.Add("@PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@Fields", "ProfileNo,EmpName,EmpIDNo,EmpTelNo,RelationVal,IsDimissioned,CreateTime ", dbType: DbType.String);
                param.Add("@Filter", strFilter.ToString(), dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var temp = await conn.QueryAsync<Profile>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                list = temp.ToList();
                totalCount = param.Get<int>("@Total");
            }
            return new PagedList<Profile>(list, pageIndex, pageSize, totalCount);
        }
        #endregion

        public string GenerateBillNo()
        {
            var param = new DynamicParameters();
            string BillNo = string.Empty;
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@BillNo", dbType: DbType.StringFixedLength, direction: ParameterDirection.Output, size: 15);
                var temp = conn.Query("Proc_GenerateBillNo", param, commandType: CommandType.StoredProcedure);
                BillNo = param.Get<string>("@BillNo");
            }
            return BillNo;
        }

        public async Task<bool> Delete(string profileNo)
        {
            bool result = true;
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Delete from Profiles where ProfileNo = @ProfileNo ");
                    param.Add("@ProfileNo", profileNo, dbType: DbType.String);
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    LogHelper.Log.WriteError("[回滚]删除档案出错", ex);
                    tran.Rollback();
                }
            }
            return result;
        }
    }
}
