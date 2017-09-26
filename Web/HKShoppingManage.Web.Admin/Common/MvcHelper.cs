using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using HKShoppingManage.Common;
using HKShoppingManage.Model;
using Autofac;
using Autofac.Integration.Mvc;

namespace HKShoppingManage.Web.Admin
{
    public class MvcHelper
    {
        /// <summary>
        /// 登录页常量
        /// </summary>
        public const string LoginUrl = "/Login/Index";

        /// <summary>
        /// 系统名称
        /// </summary>
        public const string SiteName = "HKShoppingManage";

        /// <summary>
        /// 获取当前用户(为空时会创建新实体)
        /// </summary>
        public static User User
        {
            get
            {
                var user = SessionHelper.GetSession<User>(SessionKey.UserInfo);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return new User();
                }
            }
        }

        /// <summary>
        /// 用户是否在线
        /// </summary>
        public static bool IsUserOnline
        {
            get
            {
                if (SessionHelper.GetSession(SessionKey.UserInfo) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static Autofac.IContainer _container = null;

        /// <summary>
        /// 初始化IoC注入
        /// </summary>
        public static void InitAutofacConfig()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            var dal = System.Reflection.Assembly.Load("HKShoppingManage.DAL");
            builder.RegisterAssemblyTypes(dal, dal).AsImplementedInterfaces();
            var bll = System.Reflection.Assembly.Load("HKShoppingManage.BLL");
            builder.RegisterAssemblyTypes(bll, bll).AsImplementedInterfaces();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //容器
            _container = builder.Build();
            //注入改为Autofac注入
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        /// <summary>
        /// 动态注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

    }
}