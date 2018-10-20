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
    public class CategoryDAL : ICategoryDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(Category model)
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

                    #region 产品类别
                    strSql.Append("insert into Category(");
                    strSql.Append("Name,Description,ParentCategoryId,IsDeleted,CreateTime,Creator,UpdateTime,Updator)");

                    strSql.Append(" values (");
                    strSql.Append("@Name,@Description,@ParentCategoryId,@IsDeleted,@CreateTime,@Creator,@UpdateTime,@Updator)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@Name", model.Name, dbType: DbType.String);
                    param.Add("@Description", model.Description, dbType: DbType.String);
                    param.Add("@ParentCategoryId", model.ParentCategoryId, dbType: DbType.Int32);
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
                    LogHelper.Log.WriteError("[回滚]新增产品类别出错", ex);
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
                    strSql.Append("Update Category set IsDeleted=1 where Id=@Id;");
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    LogHelper.Log.WriteError("[回滚]删除产品类别出错", ex);
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
        public async Task<bool> Update(Category model)
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

                    #region 产品类别
                    strSql.Append("update Category set ");
                    strSql.Append("Name=@Name,");
                    strSql.Append("Description=@Description,");
                    strSql.Append("ParentCategoryId=@ParentCategoryId,");
                    strSql.Append("IsDeleted=@IsDeleted,");
                    strSql.Append("UpdateTime=@UpdateTime,");
                    strSql.Append("Updator=@Updator ");
                    strSql.Append(" where id=@Id ");

                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@Name", model.Name, dbType: DbType.String);
                    param.Add("@Description", model.Description, dbType: DbType.String);
                    param.Add("@ParentCategoryId", model.ParentCategoryId, dbType: DbType.Int32);
                    param.Add("@IsDeleted", model.IsDeleted, dbType: DbType.Boolean);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Updator, dbType: DbType.String);
                    result = await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]修改产品类别出错", ex);
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
        public async Task<bool> IsExists(string CategoryName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Category where Name=@CategoryName and IsDeleted=0 ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@CategoryName", CategoryName, dbType: DbType.String);
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
        public async Task<Category> GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Category where id=@Id");

            Category model = null;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                var temp = await conn.QueryAsync<Category>(strSql.ToString(), param);
                model = temp.FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Category where IsDeleted=0 ");

            List<Category> list = new List<Category>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Category>(strSql.ToString());
                list = tempList.ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取层级关系列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<KeyValuePair<int,List<Category>>>> GetAllList()
        {
            List<KeyValuePair<int, List<Category>>> retLst = new List<KeyValuePair<int, List<Category>>>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Name,Description,ParentCategoryId from Category where IsDeleted=0 and ParentCategoryId is Null ");

            StringBuilder querySql = new StringBuilder();
            querySql.Append("with queryTable as (select Id,Name,Description,ParentCategoryId,0 as Level from Category where IsDeleted=0 and ParentCategoryId is Null and Id=@Id ");
            querySql.Append(" union all ");
            querySql.Append(" select C.Id,C.Name,C.Description,C.ParentCategoryId,Level+1 from Category C inner join queryTable D on C.ParentCategoryId=D.Id where C.IsDeleted=0) ");
            querySql.Append(" select * from queryTable");
            List<Category> list = new List<Category>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Category>(strSql.ToString());
                list = tempList.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    var Id = list[i].Id;
                    var param = new DynamicParameters();
                    param.Add("@Id", Id, dbType: DbType.Int32);
                    var queryTempList = await conn.QueryAsync<Category>(querySql.ToString(),param);
                    var queryList = queryTempList.ToList();
                    retLst.Add(new KeyValuePair<int, List<Category>>(Id, queryList));
                }
            }
            return retLst;
        }
        #endregion
    }
}
