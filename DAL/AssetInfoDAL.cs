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
    public class AssetInfoDAL : IAssetInfoDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(AssetInfo model)
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
                    strSql.Append("insert into uf_IT_zcgl(");
                    strSql.Append("zcbh,zclx,zclxmc,zcyjxlh,zcpzmx,gmrq,bfrq,zczt,zcztmc,IsDeleted,bz1,bz2,bz3,bz4,bz5,bz6,bz7)");

                    strSql.Append(" values (");
                    strSql.Append("@AssetCode,@AssetType,@AssetTypeName,@HDSeriesNo,@AssetConfigDetails,@PurchaseDate,@DiscardDate,@AssetStatus,@AssetStatusName,@IsDeleted,@Remark,@RemarkIP,@RemarkAddr,@Ext1,@Ext2,@Ext3,@Ext4)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@AssetCode", model.AssetCode, dbType: DbType.String);
                    param.Add("@AssetType", model.AssetType, dbType: DbType.Int32);
                    param.Add("@AssetTypeName", model.AssetTypeName, dbType: DbType.String);
                    param.Add("@HDSeriesNo", model.HDSeriesNo, dbType: DbType.String);
                    param.Add("@AssetConfigDetails", model.AssetConfigDetails, dbType: DbType.String);
                    param.Add("@PurchaseDate", model.PurchaseDate, dbType: DbType.String);
                    param.Add("@DiscardDate", model.DiscardDate, dbType: DbType.String);
                    param.Add("@AssetStatus", model.AssetStatus, dbType: DbType.Int32);
                    param.Add("@AssetStatusName", model.AssetStatusName, dbType: DbType.String);
                    param.Add("@IsDeleted", model.IsDeleted, dbType: DbType.Boolean);
                    param.Add("@Remark", model.Remark, dbType: DbType.String);
                    param.Add("@RemarkIP", model.RemarkIP, dbType: DbType.String);
                    param.Add("@RemarkAddr", model.RemarkAddr, dbType: DbType.String);
                    param.Add("@Ext1", model.Ext1, dbType: DbType.String);
                    param.Add("@Ext2", model.Ext2, dbType: DbType.String);
                    param.Add("@Ext3", model.Ext3, dbType: DbType.String);
                    param.Add("@Ext4", model.Ext4, dbType: DbType.String);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    result = param.Get<int>("@returnid");
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增资产出错", ex);
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
                    strSql.Append("Update uf_IT_zcgl set IsDeleted=1 where Id=@Id;");
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    LogHelper.Log.WriteError("[回滚]删除资产出错", ex);
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
        public async Task<bool> Update(AssetInfo model)
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
                    strSql.Append("update uf_IT_zcgl set ");
                    strSql.Append("zcbh=@AssetCode,");
                    strSql.Append("zclx=@AssetType,");
                    strSql.Append("zclxmc=@AssetTypeName,");
                    strSql.Append("zcyjxlh=@HDSeriesNo,");
                    strSql.Append("zcpzmx=@AssetConfigDetails,");
                    strSql.Append("gmrq=@PurchaseDate,");
                    strSql.Append("bfrq=@DiscardDate,");
                    strSql.Append("zczt=@AssetStatus,");
                    strSql.Append("zcztmc=@AssetStatusName,");
                    strSql.Append("bz1=@Remark,");
                    strSql.Append("bz2=@RemarkIP,");
                    strSql.Append("bz3=@RemarkAddr,");
                    strSql.Append("bz4=@Ext1,");
                    strSql.Append("bz5=@Ext2,");
                    strSql.Append("bz6=@Ext3,");
                    strSql.Append("bz7=@Ext4 ");
                    strSql.Append(" where id=@Id ");

                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@AssetCode", model.AssetCode, dbType: DbType.String);
                    param.Add("@AssetType", model.AssetType, dbType: DbType.Int32);
                    param.Add("@AssetTypeName", model.AssetTypeName, dbType: DbType.String);
                    param.Add("@HDSeriesNo", model.HDSeriesNo, dbType: DbType.String);
                    param.Add("@AssetConfigDetails", model.AssetConfigDetails, dbType: DbType.String);
                    param.Add("@PurchaseDate", model.PurchaseDate, dbType: DbType.String);
                    param.Add("@DiscardDate", model.DiscardDate, dbType: DbType.String);
                    param.Add("@AssetStatus", model.AssetStatus, dbType: DbType.Int32);
                    param.Add("@AssetStatusName", model.AssetStatusName, dbType: DbType.String);
                    param.Add("@Remark", model.Remark, dbType: DbType.String);
                    param.Add("@RemarkIP", model.RemarkIP, dbType: DbType.String);
                    param.Add("@RemarkAddr", model.RemarkAddr, dbType: DbType.String);
                    param.Add("@Ext1", model.Ext1, dbType: DbType.String);
                    param.Add("@Ext2", model.Ext2, dbType: DbType.String);
                    param.Add("@Ext3", model.Ext3, dbType: DbType.String);
                    param.Add("@Ext4", model.Ext4, dbType: DbType.String);
                    result = await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]修改资产出错", ex);
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
        public async Task<bool> IsExists(string AssetCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from uf_IT_zcgl where zcbh=@AssetCode and IsDeleted=0 ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@AssetCode", AssetCode, dbType: DbType.String);
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
        public async Task<AssetInfo> GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id as Id,zcbh as AssetCode,zclx as AssetType,zclxmc as AssetTypeName,zcyjxlh as HDSeriesNo,zcpzmx as AssetConfigDetails,gmrq as PurchaseDate,bfrq as DiscardDate,u1 as CurrentUser,suobm2 as DeptName,suofb2 as SubDeptName,zczt as AssetStatus,zcztmc as AssetStatusName,bz1 as Remark,bz2 as RemarkIP,bz3 as RemarkAddr,bz4 as Ext1,bz5 as Ext2,bz6 as Ext3,bz7 as Ext4 from uf_IT_zcgl");
            strSql.Append(" where id=@Id");

            AssetInfo model = null;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                var temp = await conn.QueryAsync<AssetInfo>(strSql.ToString(), param);
                model = temp.FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<AssetInfo>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id as Id,zcbh as AssetCode,zclx as AssetType,zclxmc as AssetTypeName,zcyjxlh as HDSeriesNo,zcpzmx as AssetConfigDetails,gmrq as PurchaseDate,bfrq as DiscardDate,u1 as CurrentUser,u2 as CurrentUserId,suobm as Dept,suobm2 as DeptName,suofb as SubDept,suofb2 as SubDeptName,zczt as AssetStatus,zcztmc as AssetStatusName,bz1 as Remark,bz2 as RemarkIP,bz3 as RemarkAddr,bz4 as Ext1,bz5 as Ext2,bz6 as Ext3,bz7 as Ext4 ");
            strSql.Append(" from uf_IT_zcgl where IsDeleted=0 ");
            strSql.Append(" order by AssetCode ");

            List<AssetInfo> list = new List<AssetInfo>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<AssetInfo>(strSql.ToString());
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
        public async Task<PagedList<AssetInfoDTO>> GetPageList(int pageIndex, int pageSize, int AssetType, int AssetStatus,int AssetDelete,string AssetCode)
        {
            int totalCount = 0;
            List<AssetInfo> result = new List<AssetInfo>();
            var param = new DynamicParameters();

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[uf_IT_zcgl]", dbType: DbType.String);
                param.Add("@PK", "[id]", dbType: DbType.String);
                param.Add("@Sort", "[HDSeriesNo]", dbType: DbType.String);
                param.Add("@PageNumber", pageIndex, dbType: DbType.Int32);
                param.Add("@PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@Fields", "id as [Id],zcbh as [AssetCode],zclxmc as [AssetTypeName],zcyjxlh as [HDSeriesNo],zcpzmx as [AssetConfigDetails],zcztmc as [AssetStatusName] ", dbType: DbType.String);
                string strFilter = string.Empty;
                if (AssetType!=0)
                {
                    strFilter = string.Format(" and [zclx] = {0} ", AssetType);
                }
                if (AssetStatus != 0)
                {
                    strFilter += string.Format(" and [zczt] = {0} ", AssetStatus);
                }
                if (AssetDelete != 0)
                {
                    strFilter += string.Format(" and [IsDeleted] = {0} ",AssetDelete);
                }
                else
                {
                    strFilter += string.Format(" and [IsDeleted] = {0} ", AssetDelete);
                }
                if (!string.IsNullOrEmpty(AssetCode))
                {
                    strFilter += string.Format(" and [zcbh] like '%{0}%' ", AssetCode);
                }
                param.Add("@Filter", strFilter, dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var temp = await conn.QueryAsync<AssetInfo>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                result = temp.ToList();
                totalCount = param.Get<int>("@Total");
            }

            return new PagedList<AssetInfoDTO>(EmitMapperHelper.MapList<AssetInfoDTO,AssetInfo>(result), pageIndex, pageSize, totalCount);
        }
        #endregion
    }
}
