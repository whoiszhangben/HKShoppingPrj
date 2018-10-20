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
    public class VendorDAL : IVendorDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Vendor model)
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

                    #region 订单信息
                    strSql.Append("insert into Vendors(");
                    strSql.Append("VendorName,HelpCode,VendorAddress,VendorTel,VendorFax,VendorEmail,Remark,IsDeleted)");

                    strSql.Append(" values (");
                    strSql.Append("@VendorName,@HelpCode,@VendorAddress,@VendorTel,@VendorFax,@VendorEmail,@Remark,@IsDeleted)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@VendorName", model.VendorName, dbType: DbType.String);
                    param.Add("@HelpCode", model.HelpCode, dbType: DbType.String);
                    param.Add("@VendorAddress", model.VendorAddress, dbType: DbType.String);
                    param.Add("@VendorTel", model.VendorTel, dbType: DbType.String);
                    param.Add("@VendorFax", model.VendorFax, dbType: DbType.String);
                    param.Add("@VendorEmail", model.VendorEmail, dbType: DbType.String);
                    param.Add("@Remark", model.Remark, dbType: DbType.String);
                    param.Add("@IsDeleted", model.IsDeleted, dbType: DbType.Boolean);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    result = param.Get<int>("@returnid");
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增供应商出错", ex);
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

        #region 查
        public async Task<List<Vendor>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Vendors");

            List<Vendor> list = new List<Vendor>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Vendor>(strSql.ToString());
                list = tempList.ToList();
            }
            return list;
        }

        public async Task<PagedList<Vendor>> GetPageList(int pageIndex, int pageSize)
        {
            int totalCount = 0;
            List<Vendor> result = new List<Vendor>();
            var param = new DynamicParameters();

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[Vendors]", dbType: DbType.String);
                param.Add("@PK", "[Id]", dbType: DbType.String);
                param.Add("@Sort", "[Id]", dbType: DbType.String);
                param.Add("@PageNumber", pageIndex, dbType: DbType.Int32);
                param.Add("@PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@Fields", "[Id],[VendorName],[HelpCode],[VendorAddress],[VendorTel] ,[VendorFax],[VendorEmail],[Remark],[IsDeleted] ", dbType: DbType.String);
                string strFilter = string.Empty;

                param.Add("@Filter", strFilter, dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var temp = await conn.QueryAsync<Vendor>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                result = temp.ToList();
                totalCount = param.Get<int>("@Total");
            }
            return new PagedList<Vendor>(result, pageIndex, pageSize, totalCount);
        }

        #endregion
    }
}
