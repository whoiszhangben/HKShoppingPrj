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
    public class OperationLogDAL : IOperationLogDAL
    {
        public async Task<int> Insert(OperationLog model)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into operationlog(");
            strSql.Append("LogType,LogTypeName,ActionType,Context,OperationDate,Operator,AssetCode)");

            strSql.Append(" values (");
            strSql.Append("@LogType,@LogTypeName,@ActionType,@Context,@OperationDate,@Operator,@AssetCode)");

            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@LogType", model.LogType, dbType: DbType.Int32);
                param.Add("@LogTypeName", model.LogTypeName, dbType: DbType.String);
                param.Add("@ActionType", model.ActionType, dbType: DbType.Int32);
                param.Add("@Context", model.Context, dbType: DbType.String);
                param.Add("@OperationDate", model.OperationDate, dbType: DbType.DateTime);
                param.Add("@Operator", model.Operator, dbType: DbType.String);
                param.Add("@AssetCode", model.AssetCode, dbType: DbType.String);
                int temp = await conn.ExecuteAsync(strSql.ToString(), param);

                result = temp > 0 ? temp : 0;
            }
            return result;
        }
        public async Task<PagedList<OperationLog>> GetPageList(int pageIndex, int pageSize, DateTime? startDate, DateTime? endDate, int? logType, string AssetCode)
        {
            int totalCount = 0;
            List<OperationLog> result = new List<OperationLog>();
            var param = new DynamicParameters();

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[AssetOperationLog]", dbType: DbType.String);
                param.Add("@PK", "[Id]", dbType: DbType.String);
                param.Add("@Sort", "[OperationDate]", dbType: DbType.String);
                param.Add("@PageNumber", pageIndex, dbType: DbType.Int32);
                param.Add("@PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@Fields", "[Id],[LogType],[LogTypeName],[ActionType],[Context],[OperationDate],[Operator],[AssetCode]", dbType: DbType.String);
                string strFilter = string.Empty;
                if (startDate.HasValue)
                {
                    strFilter = string.Format(" and OperationDate >= '{0}'", startDate);
                }
                if (endDate.HasValue)
                {
                    strFilter += string.Format(" and OperationDate <= '{0}'", endDate);
                }
                if (logType.HasValue)
                {
                    strFilter += string.Format(" and LogType = {0}", logType);
                }
                if (!string.IsNullOrEmpty(AssetCode))
                {
                    strFilter += string.Format(" and AssetCode like '%{0}%'", AssetCode);
                }
                param.Add("@Filter", strFilter, dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var temp = await conn.QueryAsync<OperationLog>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                result = temp.ToList();
                totalCount = param.Get<int>("@Total");
            }

            return new PagedList<OperationLog>(result, pageIndex, pageSize, totalCount);
        }
    }
}
