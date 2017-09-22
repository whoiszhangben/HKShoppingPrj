using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace HKShoppingManage.Common
{
    public static class AppSettingsHelper
    {
        /// <summary>
        /// 根据Key从配置文件获取Int值--取配置值通用方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntByKey(string key, int defaultValue)
        {
            int tempInt = 0;

            string tempStr = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(tempStr) || !int.TryParse(tempStr, out tempInt))
            {
                tempInt = defaultValue;//默认 
            }

            return tempInt;
        }

        /// <summary>
        /// 获取字符串配置值--取配置值通用方法
        /// </summary>
        /// <param name="key">配置Key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetStringByKey(string key, string defaultValue)
        {
            string setValue = string.Empty;

            setValue = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(setValue))
            {
                setValue = setValue.Trim();
            }
            if (string.IsNullOrEmpty(setValue))
            {
                setValue = defaultValue;
            }
            return setValue;
        }

        public static List<string> GetList(string key,string splitKey)
        {
            List<string> list = new List<string>();

            string value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(value))
            {
                list = value.Split(new string[] { splitKey }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            return list;
        }
    }
}
