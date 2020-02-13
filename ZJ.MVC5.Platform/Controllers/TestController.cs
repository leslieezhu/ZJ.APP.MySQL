using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZJ.MVC5.Platform.Controllers
{
    public class TestController : Controller
    {

        /// <summary>
        /// jQuery File Upload Demo - Basic version基本例子
        /// </summary>
        /// <returns></returns>
        public ActionResult Basic()
        {
            //return View("~/Views/Test/Index_3.cshtml");//实践图片平铺
            return View();
        }

        // GET: Test
        /// <summary>
        ///  利用JS第三方,在浏览器预览图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        
    }
}