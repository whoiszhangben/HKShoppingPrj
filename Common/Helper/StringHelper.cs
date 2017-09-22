using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HKShoppingManage.Common
{
    public class StringHelper
    {
        /// <summary>
        /// script标签转义
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        public static string ReplaceScript(string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString))
            {
                return "";
            }

            htmlString = Regex.Replace(htmlString, @"<script", "&lt;script", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"</script", "&lt;/script", RegexOptions.IgnoreCase);

            return htmlString;
        }

        public static string Txt2Html(string htmlString)
        {
            htmlString = htmlString.Replace("\n", "<br />");
            return htmlString;
        }

        public static string Txt3Html(string htmlString)
        {
            htmlString = htmlString.Replace("\n\n", "\n&nbsp;\n");
            htmlString = htmlString.Replace("\n\n", "\n&nbsp;\n");
            htmlString = "<p>" + htmlString.Replace("\n", "</p><p>") + "</p>";
            return htmlString;
        }
    }
}
