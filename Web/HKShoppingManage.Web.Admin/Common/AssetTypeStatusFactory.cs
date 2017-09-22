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
    public class AssetTypeStatusFactory
    {
        private static string xmlPath = string.Empty;
        public static AssetConfigXML model = null;

        public static void Init()
        {
            try
            {
                model = model ?? new AssetConfigXML();
                lock (model)
                {
                    xmlPath = HttpRuntime.AppDomainAppPath + @"Config\assetConfig.xml";
                    model = XmlHelper.Deserializer(typeof(AssetConfigXML), xmlPath) as AssetConfigXML;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("初始化quotepattern配置文件失败", ex);
            }
        }
    }
    public class AssetConfigXML
    {
        public AssetConfigXML()
        {
            AssetType = new List<AssetModel>();
            AssetStatus = new List<AssetModel>();
        }
        public List<AssetModel> AssetType { get; set; }
        public List<AssetModel> AssetStatus { get; set; }
    }
    
    public class AssetModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}