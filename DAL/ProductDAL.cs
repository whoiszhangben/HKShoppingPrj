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
    public class ProductDAL : IProductDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Product model)
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
                    strSql.Append("insert into Product(");
                    strSql.Append("Name,Description,HelpCode,CategoryId,CategoryName,IsDeleted,CreateTime,Creator,UpdateTime,Updator)");

                    strSql.Append(" values (");
                    strSql.Append("@Name,@Description,@HelpCode,@CategoryId,@CategoryName,@IsDeleted,@CreateTime,@Creator,@UpdateTime,@Updator)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@Name", model.Name, dbType: DbType.String);
                    param.Add("@Description", model.Description, dbType: DbType.String);
                    param.Add("@HelpCode", model.HelpCode, dbType: DbType.String);
                    param.Add("@CategoryId", model.CategoryId, dbType: DbType.Int32);
                    param.Add("@CategoryName", model.CategoryName, dbType: DbType.String);
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
                    LogHelper.Log.WriteError("[回滚]新增产品出错", ex);
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
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetList()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from Product where IsDeleted=0 ");

            List<Product> list = new List<Product>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Product>(strSql.ToString());
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
        public async Task<PagedList<Product>> GetPageList(int pageIndex, int pageSize, string ProductName)
        {
            int totalCount = 0;
            List<Product> result = new List<Product>();
            var param = new DynamicParameters();

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[Product]", dbType: DbType.String);
                param.Add("@PK", "[Id]", dbType: DbType.String);
                param.Add("@Sort", "[Id]", dbType: DbType.String);
                param.Add("@PageNumber", pageIndex, dbType: DbType.Int32);
                param.Add("@PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@Fields", "Id,Name,Description,HelpCode,CategoryId,CategoryName,StockQty ", dbType: DbType.String);
                string strFilter = string.Empty;

                if (!string.IsNullOrEmpty(ProductName))
                {
                    strFilter += string.Format(" and Name like '%{0}%' ", ProductName);
                }
                param.Add("@Filter", strFilter, dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var temp = await conn.QueryAsync<Product>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                result = temp.ToList();
                totalCount = param.Get<int>("@Total");
            }
            return new PagedList<Product>(result, pageIndex, pageSize, totalCount);
        }
        #endregion
    }
}
