using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZJ.MVC5.Platform
{
    public class MVCHelper
    {
        #region 生成静态内容
        /// <summary>
        /// 生成静态内容
        /// </summary>
        /// <param name="context">当前Controller的ControllerContext对象</param>
        /// <param name="viewPath">视图模板路径</param>
        /// <param name="model"></param>
        /// <returns>返回已绚烂好的string类型的视图结果</returns>
        public static string RenderViewToString(ControllerContext context, string viewPath,  object model = null)
        {
            ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);
            if (viewEngineResult == null)
                throw new FileNotFoundException("View" + viewPath + "cannot be found.");
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                context.Controller.ViewData,
                                context.Controller.TempData,
                                sw);
                view.Render(ctx, sw);
                return sw.ToString();
            }
        }
        #endregion
    }
}