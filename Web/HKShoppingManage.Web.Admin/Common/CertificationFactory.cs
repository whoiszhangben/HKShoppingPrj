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
    public class CertificationFactory
    {
        private static string xmlPath = string.Empty;
        public static CertificationXML model = null;

        public static void Init()
        {
            try
            {
                model = model ?? new CertificationXML();
                lock (model)
                {
                    xmlPath = HttpRuntime.AppDomainAppPath + @"Config\certificationLst.xml";
                    model = XmlHelper.Deserializer(typeof(CertificationXML), xmlPath) as CertificationXML;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("初始化单位配置文件失败", ex);
            }
        }
    }

    public class CertificationXML
    {
        public CertificationXML()
        {
            CertificationLst = new List<CertificationModel>();
        }
        public List<CertificationModel> CertificationLst;
    }
    public class CertificationModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string IsRemark { get; set; }
    }
}