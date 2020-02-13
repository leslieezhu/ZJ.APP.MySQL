using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZJ.App.BLL;
using ZJ.App.Common;
using ZJ.App.Entity;

namespace ZJ.MVC5.Platform
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            //tbiz_movieEntity entity = movieBLL.Gettbiz_movieEntityById(1);
            

            //parm = new SqlDbParameter();
            //parm.ColumnName = "RoleCode";
            //parm.ParameterName = "RoleCode";
            //parm.ParameterValue = Request.Form["roleCode"];
            //parm.ColumnType = DbType.String;
            //parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;
            //parms.Add(parm);
            return View();
        }

        

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}