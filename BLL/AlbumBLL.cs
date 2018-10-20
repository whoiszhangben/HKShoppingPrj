using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKShoppingManage.DAL;
using HKShoppingManage.Model;

namespace HKShoppingManage.BLL
{
    public class AlbumBLL : IAlbumBLL
    {
        private IAlbumDAL dal;

        public AlbumBLL(IAlbumDAL _dal)
        {
            this.dal = _dal;
        }
        public async Task<int> Insert(Album model)
        {
            return await this.dal.Insert(model);
        }
        public async Task<List<Album>> GetList()
        {
            return await this.dal.GetList();
        }
    }
}
