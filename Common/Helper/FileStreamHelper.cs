using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HKShoppingManage.Common
{
    public class FileStreamHelper
    {
        /// <summary>
        /// 文件流转换
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static Stream DecompressStream(Stream sourceStream)
        {
            MemoryStream responseStream = new MemoryStream();
            using (System.IO.Compression.GZipStream compressedStream = new System.IO.Compression.GZipStream(responseStream, System.IO.Compression.CompressionMode.Compress, true))
            {
                byte[] buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                if (checkCounter != buffer.Length) throw new ApplicationException();
                compressedStream.Write(buffer, 0, buffer.Length);
            }
            responseStream.Position = 0;
            return responseStream;
        }
    }
}
