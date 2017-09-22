using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HKShoppingManage.DBUtility
{
    public sealed class DBConnFactory
    {
        public static IDbConnection CreateSqlConnection()
        {
            string strKey = "HKShoppingManageConnString";
            string strConn = ConfigurationManager.ConnectionStrings[strKey].ConnectionString;
            IDbConnection connection = new SqlConnection(strConn);
            return connection;
        }

        private DBConnFactory()
        { 
        }
    }
}
