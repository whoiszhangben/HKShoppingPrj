using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HKShoppingManage.Common;
using System.Reflection;
using System.ComponentModel;

namespace HKShoppingManage.Web.Admin
{
    public class OperationLogTypeCache
    {
        private static OperationLogTypeCache _instance = null;
        private static object objLock = new object();
        private static Dictionary<string, string> _dic = new Dictionary<string, string>();

        private OperationLogTypeCache() { }
        public static OperationLogTypeCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new OperationLogTypeCache();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        public void Init()
        {
            foreach (var enumObj in Enum.GetValues(typeof(OperationLogType)))
            {
                string key = enumObj.ToString();
                FieldInfo filed = typeof(OperationLogType).GetField(key);
                object[] attrArr = filed.GetCustomAttributes(false);
                DescriptionAttribute attr = (DescriptionAttribute)attrArr[0];
                string strKey = ((int)enumObj).ToString();
                string strValue = attr.Description;
                _dic.Add(strKey, strValue);
            }
        }

        public static Dictionary<string, string> EnumLogTypeCache
        {
            get { return _dic; }
            set { _dic = value; }
        }
    }
}