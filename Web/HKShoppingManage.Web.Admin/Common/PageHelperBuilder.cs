using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKShoppingManage.Web.Admin
{
    public class PageHelperBuilder
    {
        private static PageHelperBuilder _instance;
        private static object objLock = new object();
        private PageHelperBuilder() { }
        public static PageHelperBuilder Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PageHelperBuilder();
                        }
                    }
                }
                return _instance;
            }
        }
        private string _activeAdminMenuSystemName = "";
        public void setActiveMenuSystemName(string systemName)
        {
            _activeAdminMenuSystemName = systemName;
        }
        public string getActiveMenuSystemName()
        {
            return _activeAdminMenuSystemName;
        }
    }
}
