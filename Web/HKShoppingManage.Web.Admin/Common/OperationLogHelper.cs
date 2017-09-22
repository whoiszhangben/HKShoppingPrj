using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using HKShoppingManage.Common;
using HKShoppingManage.Model;
using HKShoppingManage.BLL;
using HKShoppingManage.DBUtility;
using Dapper;

namespace HKShoppingManage.Web.Admin
{
    public class OperationLogHelper
    {
        public static void Add(int LogType,string LogTypeName, string actionType, string name, string operatorName, string AssetCode = "")
        {
            Task.Run(() =>
            {
                string context = string.Empty;
                context = string.Format("{0}", name);
                if (!string.IsNullOrEmpty(context))
                {
                    OperationLog model = new OperationLog();
                    model.LogType = LogType;
                    model.LogTypeName = LogTypeName;
                    model.ActionType = actionType;
                    model.Context = context;
                    model.OperationDate = DateTime.Now;
                    model.Operator = operatorName;
                    model.AssetCode = AssetCode;
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into AssetOperationLog(");
                    strSql.Append("LogType,LogTypeName,ActionType,Context,OperationDate,Operator,AssetCode)");

                    strSql.Append(" values (");
                    strSql.Append("@LogType,@LogTypeName,@ActionType,@Context,@OperationDate,@Operator,@AssetCode)");

                    var param = new DynamicParameters();
                    using (var conn = DBConnFactory.CreateSqlConnection())
                    {
                        conn.Open();
                        param.Add("@LogType", model.LogType, dbType: DbType.Int32);
                        param.Add("@LogTypeName", model.LogTypeName, dbType: DbType.String);
                        param.Add("@ActionType", model.ActionType, dbType: DbType.String);
                        param.Add("@Context", model.Context, dbType: DbType.String);
                        param.Add("@OperationDate", model.OperationDate, dbType: DbType.DateTime);
                        param.Add("@Operator", model.Operator, dbType: DbType.String);
                        param.Add("@AssetCode", model.AssetCode, dbType: DbType.String);
                        conn.Execute(strSql.ToString(), param);
                    }
                }

            })
            .ConfigureAwait(false);
        }
    }
}