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
                    StringBuilder strSql = new StringBuilder();
                    var param = new DynamicParameters();

                    #region Album
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

                    //根据RelationVal 向关系中插入关联数据 
                    for (int i = 0; i < 10; i++)
                    {
                        if ((model.RelationVal & i) != 0)
                        {
                            strSql.Clear();
                            param = new DynamicParameters();
                            strSql.Append("insert into EmpProfileRelation(");
                            strSql.Append("ProfileId,InfoId,Remark)");

                            strSql.Append(" values (");
                            strSql.Append("@ProfileId,@InfoId,@Remark);");
                            param.Add("@ProfileId", result, dbType: DbType.Int32);
                            param.Add("@InfoId", i, dbType: DbType.Int32);
                            param.Add("@Remark", "", dbType: DbType.String);
                            await conn.ExecuteAsync(strSql.ToString(), param, tran);
                        }
                    }
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增档案出错", ex);
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
        public async Task<List<Profile>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProfileNo,EmpName,EmpIDNo,EmpTelNo,IsDimissioned,RelationVal from Profiles");

            List<Profile> list = new List<Profile>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Profile>(strSql.ToString());
                list = tempList.ToList();
            }
            return list;
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
    }
}
