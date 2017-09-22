using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HKShoppingManage.Web.Admin
{
    public class PostFile
    {
         public string Name { get; private set; }

        public string Type { get; private set; }

        public byte[] Buffer { get; private set; }

        public PostFile(HttpPostedFileBase file)
        {
            Name = file.FileName;
            Type = file.ContentType;
            byte[] temp = new byte[file.InputStream.Length];
            file.InputStream.Read(temp, 0, (int)file.InputStream.Length);
            Buffer = temp;
        }

        public PostFile(string name, string type, byte[] buffer)
        {
            Name = name;
            Type = type;
            Buffer = buffer;
        }

        public void SaveAs(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fs.Write(Buffer, 0, Buffer.Length);
            }
        }
    }

}