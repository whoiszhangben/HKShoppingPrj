using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.Model;
using HKShoppingManage.Common;
using HKShoppingManage.DAL;

namespace HKShoppingManage.BLL
{
    public class AssetInfoBLL : IAssetInfoBLL
    {
        private IAssetInfoDAL dal;
        public AssetInfoBLL(IAssetInfoDAL dal)
        {
            this.dal = dal;
        }
        public async Task<int> Insert(AssetInfo model)
        {
            return await this.dal.Insert(model);
        }
        public async Task<bool> Delete(int Id)
        {
            return await this.dal.Delete(Id);
        }
        public async Task<bool> Update(AssetInfo model)
        {
            return await this.dal.Update(model);
        }
        public async Task<bool> IsExists(string AssetCode)
        {
            return await this.dal.IsExists(AssetCode);
        }
        public async Task<AssetInfo> GetModel(int Id)
        {
            return await this.dal.GetModel(Id);
        }
        public async Task<List<AssetInfo>> GetList()
        {
            return await this.dal.GetList();
        }
        public async Task<PagedList<AssetInfoDTO>> GetPageList(int pageIndex, int pageSize, int AssetType, int AssetStatus,int AssetDelete,string AssetCode)
        {
            return await this.dal.GetPageList(pageIndex, pageSize, AssetType, AssetStatus, AssetDelete, AssetCode);
        }
    }
}
