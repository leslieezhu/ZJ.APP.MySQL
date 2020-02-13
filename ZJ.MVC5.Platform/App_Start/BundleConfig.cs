using System.Web;
using System.Web.Optimization;

namespace ZJ.MVC5.Platform
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/plugins/jqueryValidate/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //主CSS _Layout.cshtml
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Font Awesome icons 对此项目是必备的,列表上的按钮需要此样式,需要添加添加font-awesome下很多文件
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            //datetimepicker  Styles
            bundles.Add(new StyleBundle("~/Content/datetimepickerStyles").Include(
                      "~/Content/plugins/datetimepicker/bootstrap-datetimepicker.min.css"));

            //datetimepicker
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                      "~/Scripts/plugins/datetimepicker/bootstrap-datetimepicker.min.js",
                      "~/Scripts/plugins/datetimepicker/bootstrap-datetimepicker.zh-CN.js",
                      "~/Scripts/plugins/datetimepicker/moment-with-locales.js"));

            // Sweet alert Styless
            bundles.Add(new StyleBundle("~/Content/sweetAlertStyles").Include(
                      "~/Content/plugins/sweetalert/sweetalert.css"));

            // Sweet alert
            bundles.Add(new ScriptBundle("~/bundles/sweetAlert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));


            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/datatables.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js",
                      "~/Scripts/plugins/dataTables/localization/messages_cn.js"));

            // jQuery-File-Upload Css
            bundles.Add(new StyleBundle("~/Content/jQueryFileUploadCss").Include(
                      "~/Content/plugins/jQuery-File-Upload/jquery.fileupload.css"));

            //jQuery-File-Upload
            bundles.Add(new ScriptBundle("~/bundles/jQueryFileUpload").Include(
                      "~/Scripts/plugins/jQuery-File-Upload/jquery.ui.widget.js",
                      "~/Scripts/plugins/jQuery-File-Upload/jquery.iframe-transport.js",
                      "~/Scripts/plugins/jQuery-File-Upload/jquery.fileupload.js",
                      "~/Scripts/plugins/jQuery-File-Upload/jquery.fileupload-process.js",
                      "~/Scripts/plugins/jQuery-File-Upload/jquery.fileupload-validate.js"));

            //jquery-Dm-Uploader
            bundles.Add(new ScriptBundle("~/bundles/jQueryDm").Include(
                      "~/Scripts/plugins/jquery-dm-uploader/jquery.dm-uploader.js",
                      "~/Scripts/ControlJS/Talent/ResumeBatchUpload/upload-ui.js",
                      "~/Scripts/ControlJS/Talent/ResumeBatchUpload/upload-config.js"));

            // jquery-Dm-Uploader Css
            bundles.Add(new StyleBundle("~/Content/jQueryDmCss").Include(
                      "~/Content/plugins/jqueryDmUploader/jquery.dm-uploader.min.css",
                      "~/Content/plugins/jqueryDmUploader/styles.css"));
        }
    }
}
