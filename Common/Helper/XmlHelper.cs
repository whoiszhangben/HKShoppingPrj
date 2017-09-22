using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HKShoppingManage.Common
{
    /// <summary>
    /// Xml帮助类
    /// </summary>
    public class XmlHelper
    {
        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            string result = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    serializer.Serialize(stream, obj);
                    stream.Position = 0;
                    using (StreamReader read = new StreamReader(stream))
                    {
                        result = read.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }
        #endregion

        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="path">xml地址</param>
        /// <returns></returns>
        public static object Deserializer(Type type, string path)
        {
            object obj = null;
            using (StreamReader read = new StreamReader(path, Encoding.UTF8))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    obj = serializer.Deserialize(read);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return obj;
        }
        #endregion
    }
}
