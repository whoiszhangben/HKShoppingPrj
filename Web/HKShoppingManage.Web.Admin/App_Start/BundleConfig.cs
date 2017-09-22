using System.Web;
using System.Web.Optimization;

namespace HKShoppingManage.Web.Admin
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region 通用
            BundleTable.Bundles.Add(new StyleBundle("~/resource/common_css")
                .Include("~/Content/css/style.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/css/skin.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/css/layer.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/css/custom.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/css/cus-icons.css", new CssRewriteUrlTransformWrapper())
                );
            BundleTable.Bundles.Add(new ScriptBundle("~/resource/common_js").Include(
                "~/Content/js/common/common.js",
                "~/Content/js/common/layer.js",
                "~/Content/js/common/ajaxHelper.js",
                "~/Content/js/common/arrayHelper.js",
                "~/Content/js/common/jquery.cookie.js",
                "~/Content/js/common/pageHelper.js",
                "~/Content/js/common/selectHelper.js",
                "~/Content/js/common/StringHelper.js",
                "~/Content/js/common/VerificationHelper.js",
                "~/Content/js/common/dialogHelper.js",
                "~/Content/js/common/custom.js",
                "~/Content/js/data/msgList.js"
                ));
            BundleTable.Bundles.Add(new StyleBundle("~/resource/ztree_css")
                .Include("~/Content/css/zTreeStyle.css", new CssRewriteUrlTransformWrapper())
                );
            BundleTable.Bundles.Add(new StyleBundle("~/resource/date_css")
                .Include("~/Content/js/laydate/need/laydate.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/js/laydate/skins/default/laydate.css", new CssRewriteUrlTransformWrapper())
                );
            BundleTable.Bundles.Add(new ScriptBundle("~/resource/ztree_js").Include(
                "~/Content/js/common/jquery.ztree.core.js",
                "~/Content/js/common/jquery.ztree.excheck.js"
                ));

            BundleTable.Bundles.Add(new ScriptBundle("~/resource/upload_js").Include(
                "~/Content/js/common/jquery.ui.widget.js",
                "~/Content/js/common/jquery.iframe-transport.js",
                "~/Content/js/common/jquery.fileupload.js"
                ));

            BundleTable.Bundles.Add(new ScriptBundle("~/resource/date_js").Include(
                "~/Content/js/laydate/laydate.dev.js"
                ));
            #endregion

            #region 登录页 Login
            BundleTable.Bundles.Add(new StyleBundle("~/resource/login_css")
                .Include("~/Content/css/login.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/css/skin.css", new CssRewriteUrlTransformWrapper())
                );
            BundleTable.Bundles.Add(new ScriptBundle("~/resource/login_js").Include(
                "~/Content/js/data/msgList.js",
                "~/Content/js/manage/login.js"
                ));
            #endregion

            #region 资产管理
            BundleTable.Bundles.Add(new ScriptBundle("~/resource/asset_list_js").Include(
                "~/Content/js/common/tooltip.js",
                "~/Content/js/common/popover.js",
                "~/Content/js/data/asset.data.js",
                "~/Content/js/manage/asset.js"
                ));
            #endregion

            #region 操作日志
            BundleTable.Bundles.Add(new ScriptBundle("~/resource/log_list_js").Include(
                "~/Content/js/manage/log.js"
                ));
            #endregion

            BundleTable.Bundles.Add(new ScriptBundle("~/resource/asset_edit_js").Include(
                "~/Content/js/common/tooltip.js",
                "~/Content/js/common/popover.js",
                "~/Content/js/manage/asset.edit.js"
                ));
            BundleTable.Bundles.Add(new ScriptBundle("~/resource/asset_detail_js").Include(
                "~/Content/js/manage/asset.detail.js"
                ));
        }
    }
}
