using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using HKShoppingManage.Common;
using HKShoppingManage.Model;

namespace HKShoppingManage.Web.Admin
{
    public class WatchHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            try
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = HttpContext.Current.Server.MapPath("/Config");
                watcher.Filter = "*.xml";
                watcher.Changed += new FileSystemEventHandler(OnProcess);
                watcher.EnableRaisingEvents = true;
                watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
            }
            catch (Exception ex)
            {
                
                LogHelper.Log.WriteError("初始化配置监控出错", ex);
            }
        }

        private static void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                switch (e.Name)
                {
                    case "productUnit.xml":
                        {
                            UnitFactory.Init();
                        }
                        break;
                    case "certificationLst.xml":
                        {
                            CertificationFactory.Init();
                        }
                        break;
                    default:
                        break;
                }
                LogHelper.Log.WriteInfo(e.Name + "文件发生改变，重新读取配置文件");
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                LogHelper.Log.WriteInfo(e.Name + "被删除了");
            }
        }

        private static void OnRename(object source, RenamedEventArgs e)
        {
            LogHelper.Log.WriteInfo(e.Name + "改名了");
        }
    }
}