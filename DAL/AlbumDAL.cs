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
    public class AlbumDAL:IAlbumDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Album model)
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
                    strSql.Append("insert into Albums(");
                    strSql.Append("CoverImg,AlbumName,PhotoNum,AlbumDesc,CreateTime,Creator,UpdateTime,Updator)");

                    strSql.Append(" values (");
                    strSql.Append("@CoverImg,@AlbumName,@PhotoNum,@AlbumDesc,@CreateTime,@Creator,@UpdateTime,@Updator)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@CoverImg", model.CoverImg, dbType: DbType.String);
                    param.Add("@AlbumName", model.AlbumName, dbType: DbType.String);
                    param.Add("@PhotoNum", model.PhotoNum, dbType: DbType.Int32);
                    param.Add("@AlbumDesc", model.AlbumDesc, dbType: DbType.String);
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
                    LogHelper.Log.WriteError("[回滚]新增相册出错", ex);
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
        public async Task<List<Album>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CoverImg,AlbumName,PhotoNum,AlbumDesc from Albums where IsDeleted = 0 ");

            List<Album> list = new List<Album>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Album>(strSql.ToString());
                list = tempList.ToList();
            }
            return list;
        }
        #endregion
    }
}
