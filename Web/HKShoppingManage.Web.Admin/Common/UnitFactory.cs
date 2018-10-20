using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using HKShoppingManage.Common;
using HKShoppingManage.Model;
using System.Xml.Serialization;

namespace HKShoppingManage.Web.Admin
{
    public class UnitFactory
    {
        private static string xmlPath = string.Empty;
        public static ProductUnitXML model = null;

        public static void Init()
        {
            try
            {
                model = model ?? new ProductUnitXML();
                lock (model)
                {
                    xmlPath = HttpRuntime.AppDomainAppPath + @"Config\productUnit.xml";
                    model = XmlHelper.Deserializer(typeof(ProductUnitXML), xmlPath) as ProductUnitXML;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("初始化单位配置文件失败", ex);
            }
        }
    }

    public class ProductUnitXML
    {
        public ProductUnitXML()
        {
            ProductUnits = new List<ProductUnitModel>();
        }
        public List<ProductUnitModel> ProductUnits;
    }
    public class ProductUnitModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}