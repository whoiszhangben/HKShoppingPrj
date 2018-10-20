using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using HKShoppingManage.Common;
using HKShoppingManage.Model;
using HKShoppingManage.BLL;

namespace HKShoppingManage.Web.Admin
{
    public static class CacheManager
    {
        private static ICategoryBLL categoryBLL = MvcHelper.Resolve<ICategoryBLL>();
        private static IProductBLL productBLL = MvcHelper.Resolve<IProductBLL>();
        private static ICustomerBLL customerBLL = MvcHelper.Resolve<ICustomerBLL>();
        private static IVendorBLL vendorBLL = MvcHelper.Resolve<IVendorBLL>();
        #region 缓存容器
        public static SynchronisedDictionary<int, Category> CategoryCache;
        public static SynchronisedDictionary<int, Product> ProductCache;
        public static SynchronisedDictionary<int, Customer> CustomerCache;
        public static ConcurrentDictionary<int, List<Category>> CategoryLstCache;
        public static SynchronisedDictionary<int, Vendor> VendorCache;

        #endregion

        #region 初始化缓存
        public static async void Init()
        {
            try
            {
                CategoryCache = new SynchronisedDictionary<int, Category>(x => x.Id, await categoryBLL.GetList());
                ProductCache = new SynchronisedDictionary<int, Product>(x => x.Id, await productBLL.GetList());
                CustomerCache = new SynchronisedDictionary<int, Customer>(x => x.Id, await customerBLL.GetList());
                VendorCache = new SynchronisedDictionary<int, Vendor>(x => x.Id, await vendorBLL.GetList());
                CategoryLstCache = new ConcurrentDictionary<int, List<Category>>();
                var list = await categoryBLL.GetAllList();
                for (var i = 0; i < list.Count; i++)
                {
                    CategoryLstCache.TryAdd(list[i].Key, list[i].Value);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Log.WriteError("缓存初始化失败,"+ex.Message);
            }
        }
        #endregion
    }
}