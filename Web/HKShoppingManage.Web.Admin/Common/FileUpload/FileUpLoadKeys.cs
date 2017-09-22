using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKShoppingManage.Web.Admin
{
    public class FileUpLoadKeys
    {
        private static readonly string _uploadPath = "/UploadFiles/";

        public static string UploadPath { get { return _uploadPath; } }

        public const string TypeImg = "Img";

        public const string TypeFile = "File";
    }
}