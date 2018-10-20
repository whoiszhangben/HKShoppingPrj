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
    public class POOrderDAL:IPOOrderDAL
    {
        #region 增
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Insert(POOrder model)
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
                    strSql.Append("insert into POOrder(");
                    strSql.Append("POOrderNo,ExchangeRate,TotalAmount,OrderState,CreateTime,Creator,UpdateTime,Updator)");

                    strSql.Append(" values (");
                    strSql.Append("@POOrderNo,@ExchangeRate,@TotalAmount,@OrderState,@CreateTime,@Creator,@UpdateTime,@Updator)");
                    strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                    param.Add("@POOrderNo", model.POOrderNo, dbType: DbType.String);
                    param.Add("@ExchangeRate", model.ExchangeRate, dbType: DbType.Double);
                    param.Add("@TotalAmount", model.TotalAmount, dbType: DbType.Double);
                    param.Add("@OrderState", model.OrderState, dbType: DbType.Int32);
                    param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                    param.Add("@Creator", model.Creator, dbType: DbType.String);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Updator, dbType: DbType.String);
                    param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    result = param.Get<int>("@returnid");

                    if (model.OrderDetails.Count > 0)
                    {
                        var sum = 0.0;
                        foreach(var detail in model.OrderDetails)
                        {
                            string remark = string.Empty;
                            sum += detail.Amount;
                            StringBuilder sb = new StringBuilder();
                            var detailparam = new DynamicParameters();
                            sb.Append("select top 5 b.Price,b.VendorName from POOrder a left join POOrderDetail b on a.Id=b.Id where a.OrderState=1 and b.ProductId = @ProductId and b.State=1 Order by b.CreateTime desc");
                            detailparam.Add("@ProductId", detail.ProductId, dbType: DbType.Int32);
                            IEnumerable<POOrderDetail> history = await conn.QueryAsync<POOrderDetail>(sb.ToString(), detailparam, tran);
                            foreach (var item in history)
                            {
                                remark += (item.VendorName + ":" + item.Price + ";");
                            }
                            if (!string.IsNullOrEmpty(remark))
                            {
                                remark = remark.Substring(0, remark.Length - 1);
                            }

                            sb.Clear();
                            detailparam = new DynamicParameters();

                            sb.Append("insert into POOrderDetail(");
                            sb.Append("Id,EntryId,ProductId,ProductName,VendorId,VendorName,Qty,Price,ExRate,Amount,AmountRMB,State,Remark,CreateTime,Creator,UpdateTime,Updator)");

                            sb.Append(" values (");
                            sb.Append("@Id,@EntryId,@ProductId,@ProductName,@VendorId,@VendorName,@Qty,@Price,@ExRate,@Amount,@AmountRMB,@State,@Remark,@CreateTime,@Creator,@UpdateTime,@Updator)");

                            detailparam.Add("@Id", result, dbType: DbType.Int32);
                            detailparam.Add("@EntryId", detail.EntryId, dbType: DbType.Int32);
                            detailparam.Add("@ProductId", detail.ProductId, dbType: DbType.Int32);
                            detailparam.Add("@ProductName", detail.ProductName, dbType: DbType.String);
                            detailparam.Add("@VendorId", detail.VendorId, dbType: DbType.Int32);
                            detailparam.Add("@VendorName", detail.VendorName, dbType: DbType.String);
                            detailparam.Add("@Qty", detail.Qty, dbType: DbType.Double);
                            detailparam.Add("@Price", detail.Price, dbType: DbType.Double);
                            detailparam.Add("@ExRate", detail.ExRate, dbType: DbType.Double);
                            detailparam.Add("@Amount", detail.Amount, dbType: DbType.Double);
                            detailparam.Add("@AmountRMB", detail.AmountRMB, dbType: DbType.Double);
                            detailparam.Add("@State", detail.State, dbType: DbType.Int32);
                            detailparam.Add("@Remark", remark, dbType: DbType.String);
                            detailparam.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                            detailparam.Add("@Creator", model.Creator, dbType: DbType.String);
                            detailparam.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                            detailparam.Add("@Updator", model.Updator, dbType: DbType.String);


                            await conn.ExecuteAsync(sb.ToString(), detailparam, tran);
                        }
                        StringBuilder sbUpdate = new StringBuilder();
                        var updateparam = new DynamicParameters();
                        sbUpdate.Append("update POOrder set TotalAmount=@TotalAmount where Id=@Id");
                        updateparam.Add("@Id", result, dbType: DbType.Int32);
                        updateparam.Add("@TotalAmount", sum);
                        await conn.ExecuteAsync(sbUpdate.ToString(), updateparam, tran);
                    }

                    #endregion

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]新增采购订单出错", ex);
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
        public async Task<int> Update(POOrder model)
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
                    strSql.Append("Update POOrder set ExchangeRate=@ExchangeRate,TotalAmount=@TotalAmount,UpdateTime=@UpdateTime,Updator=@Updator ");
                    strSql.Append(" where Id=@Id");

                    param.Add("@ExchangeRate", model.ExchangeRate, dbType: DbType.Double);
                    param.Add("@TotalAmount", model.TotalAmount, dbType: DbType.Double);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Updator, dbType: DbType.String);
                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    strSql.Clear();
                    param = new DynamicParameters();
                    strSql.Append("delete from POOrderDetail where Id=@Id");
                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    await conn.ExecuteAsync(strSql.ToString(), param, tran);

                    if (model.OrderDetails!=null && model.OrderDetails.Count > 0)
                    {
                        var sum = 0.0;
                        foreach (var detail in model.OrderDetails)
                        {
                            string remark = string.Empty;
                            sum += detail.Amount;
                            StringBuilder sb = new StringBuilder();
                            var detailparam = new DynamicParameters();
                            sb.Append("select top 5 * from POOrderDetail where ProductId = @ProductId and State=1 Order by CreateTime desc");
                            detailparam.Add("@ProductId", detail.ProductId, dbType: DbType.Int32);
                            IEnumerable<POOrderDetail> history = await conn.QueryAsync<POOrderDetail>(sb.ToString(), detailparam, tran);
                            foreach (var item in history)
                            {
                                remark += (item.VendorName + ":" + item.Price + ";");
                            }
                            if (!string.IsNullOrEmpty(remark))
                            {
                                remark = remark.Substring(0, remark.Length - 1);
                            }

                            sb.Clear();
                            detailparam = new DynamicParameters();
                            sb.Append("insert into POOrderDetail(");
                            sb.Append("Id,EntryId,ProductId,ProductName,VendorId,VendorName,Qty,Price,ExRate,Amount,AmountRMB,State)");

                            sb.Append(" values (");
                            sb.Append("@Id,@EntryId,@ProductId,@ProductName,@VendorId,@VendorName,@Qty,@Price,@ExRate,@Amount,@AmountRMB,@State)");

                            detailparam.Add("@Id", model.Id, dbType: DbType.Int32);
                            detailparam.Add("@EntryId", detail.EntryId, dbType: DbType.Int32);
                            detailparam.Add("@ProductId", detail.ProductId, dbType: DbType.Int32);
                            detailparam.Add("@ProductName", detail.ProductName, dbType: DbType.String);
                            detailparam.Add("@VendorId", detail.VendorId, dbType: DbType.Int32);
                            detailparam.Add("@VendorName", detail.VendorName, dbType: DbType.String);
                            detailparam.Add("@Qty", detail.Qty, dbType: DbType.Double);
                            detailparam.Add("@Price", detail.Price, dbType: DbType.Double);
                            detailparam.Add("@ExRate", detail.ExRate, dbType: DbType.Double);
                            detailparam.Add("@Amount", detail.Amount, dbType: DbType.Double);
                            detailparam.Add("@AmountRMB", detail.AmountRMB, dbType: DbType.Double);
                            detailparam.Add("@State", detail.State, dbType: DbType.Int32);

                            await conn.ExecuteAsync(sb.ToString(), detailparam, tran);
                        }
                        StringBuilder sbUpdate = new StringBuilder();
                        var updateparam = new DynamicParameters();
                        sbUpdate.Append("update POOrder set TotalAmount=@TotalAmount where Id=@Id");
                        updateparam.Add("@Id", model.Id, dbType: DbType.Int32);
                        updateparam.Add("@TotalAmount", sum);
                        await conn.ExecuteAsync(sbUpdate.ToString(), updateparam, tran);
                    }

                    #endregion

                    tran.Commit();
                    result = model.Id;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]修改采购订单出错", ex);
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
        public POOrder GetModel(int Id)
        {
            POOrder ret = new POOrder();
            var param = new DynamicParameters();
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from POOrder where Id =@Id");

            StringBuilder sbsub = new StringBuilder();
            sbsub.Append("select * from POOrderDetail where Id =@Id");
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Id", Id);
                var temp = conn.Query<POOrder>(sb.ToString(), param);
                ret = temp.FirstOrDefault();
                var tempsub = conn.Query<POOrderDetail>(sbsub.ToString(), param);
                ret.OrderDetails = tempsub.ToList();
            }
            return ret;
        }

        public async Task<List<POOrder>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from POOrder");

            List<POOrder> list = new List<POOrder>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<POOrder>(strSql.ToString());
                list = tempList.ToList();
            }
            return list;
        }

        public async Task<PagedList<POOrder>> GetPageList(int pageIndex, int pageSize)
        {
            int totalCount = 0;
            List<POOrder> result = new List<POOrder>();
            var param = new DynamicParameters();

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@Tables", "[POOrder]", dbType: DbType.String);
                param.Add("@PK", "[Id]", dbType: DbType.String);
                param.Add("@Sort", "[Id]", dbType: DbType.String);
                param.Add("@PageNumber", pageIndex, dbType: DbType.Int32);
                param.Add("@PageSize", pageSize, dbType: DbType.Int32);
                param.Add("@Fields", "Id,POOrderNo,ExchangeRate,TotalAmount,CreateTime,Creator,UpdateTime,Updator ", dbType: DbType.String);
                string strFilter = string.Empty;

                param.Add("@Filter", strFilter, dbType: DbType.String);
                param.Add("@isCount", true, dbType: DbType.Boolean);
                param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var temp = await conn.QueryAsync<POOrder>("Proc_CommonPagingStoredProcedure", param, commandType: CommandType.StoredProcedure);
                result = temp.ToList();
                totalCount = param.Get<int>("@Total");
            }
            return new PagedList<POOrder>(result, pageIndex, pageSize, totalCount);
        }

        public string GetBillNo()
        {
            string ret = string.Empty;
            var param = new DynamicParameters();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                param.Add("@TableName", "POOrder", dbType: DbType.String);
                param.Add("@BillNo", "", dbType: DbType.String, direction: ParameterDirection.Output);
                var temp = conn.Query("GetBillNo", param, commandType: CommandType.StoredProcedure);
                ret = param.Get<string>("@BillNo");
            }
            return ret;
        }
        #endregion

        #region 删
        public async Task<bool> Delete(int Id)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Delete from POOrder where Id=@Id");
            StringBuilder substrsql = new StringBuilder();
            substrsql.Append("delete from POOrderDetail where Id=@Id");

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    var main = await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    var sub = await conn.ExecuteAsync(substrsql.ToString(), param, tran);
                    tran.Commit();
                    if (main > 0 && sub > 0)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]删除订单出错", ex);
                    tran.Rollback();
                }
                finally
                {
                    if (tran != null)
                        tran.Dispose();
                    if (conn != null)
                        conn.Close();
                }
                return result;
            }
        }
        #endregion

        #region 确认订单
        public async Task<bool> Confirm(int Id)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update POOrder set OrderState=1 where Id=@Id");
            StringBuilder substrsql = new StringBuilder();
            substrsql.Append("Update Product set StockQty=ISNULL(StockQty,0)+p2.Qty from Product p1 inner join POOrderDetail p2 on p1.Id=p2.ProductId where p2.Id=@Id And p2.State=1");

            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    var main = await conn.ExecuteAsync(strSql.ToString(), param, tran);
                    var sub = await conn.ExecuteAsync(substrsql.ToString(), param, tran);
                    tran.Commit();
                    if (main > 0 && sub > 0)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]确认订单出错", ex);
                    tran.Rollback();
                }
                finally
                {
                    if (tran != null)
                        tran.Dispose();
                    if (conn != null)
                        conn.Close();
                }
                return result;
            }
        }
        #endregion
    }
}
