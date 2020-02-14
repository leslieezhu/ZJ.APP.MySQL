using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ZJ.App.BLL;
using ZJ.App.Common;
using ZJ.App.Entity;
using ZJ.App.Common.Extension;
using Webdiyer.WebControls.Mvc;
using System.Threading;
using System.Globalization;

namespace ZJ.MVC5.Platform.Areas.Movie
{
    public class DefaultController : Controller
    {
        // GET: Movie/Default
        public ActionResult Index()
        {
            //movie type dropdownList
            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> categoryByLocalSelect = dictitemBLL.GetDictitemEntity("movie", "categoryByLocal");
            ViewBag.CategoryByLocal = new SelectList(categoryByLocalSelect, "PropertyValue", "PropertyName");

            return View();
        }

        /// <summary>
        /// 电影列表数据
        /// </summary>
        /// <returns></returns>
        public string GetList()
        {
            tbiz_movieBLL movieBLL = new tbiz_movieBLL();
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            if (!string.IsNullOrEmpty(Request.Form["MovieFileName"]))
            {
                parm = new SqlDbParameter();
                parm.ColumnName = "MovieFileName";
                parm.ParameterName = "MovieFileName";
                parm.ParameterValue = Request.Form["MovieFileName"].UrlDecode().Trim();
                parm.ColumnType = DbType.String;
                parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
                parms.Add(parm);
            }

            int recordCount;
            int draw = Convert.ToInt32(Request["draw"]);
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int page = start / length; //start 初始值0
            DataTable dataTable = movieBLL.GetMovieDataTablePage(parms, "Id DESC", length, page, out recordCount);
            IDictionary info = new Hashtable();
            info.Add("draw", draw);
            info.Add("recordsTotal", recordCount);
            info.Add("recordsFiltered", recordCount);
            info.Add("data", dataTable);
            return JsonConvert.SerializeObject(info);
        }

        public ActionResult Create()
        {
            //movie type dropdownList
            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> categoryByLocalSelect = dictitemBLL.GetDictitemEntity("movie", "categoryByLocal");
            ViewBag.CategoryByLocal = new SelectList(categoryByLocalSelect, "PropertyValue", "PropertyName",0);
            List<tcfg_dictitemEntity> saveLocalSelect = dictitemBLL.GetDictitemEntity("movie", "saveLocal");
            ViewBag.SaveLocal = new SelectList(saveLocalSelect, "PropertyValue", "PropertyName", 0);
            return View();
        }

        [HttpPost]
        public string Create(tbiz_movieEntity movie)
        {
            if (ModelState.IsValid)
            {
                tbiz_movieBLL movieBLL = new tbiz_movieBLL();
                movie.MovieFileName = movie.MovieFileName.UrlDecode(); //HttpUtility.HtmlDecode(movie.MovieFileName);
                movie.MovieName = movie.MovieName.UrlDecode();
                movie.CreateTime = DateTime.Now;
                movieBLL.Addtbiz_movieEntity(movie);

                //电影图片文件
                if (!string.IsNullOrEmpty(movie.ImgFileName))
                {
                    tbiz_pictureBLL pictureBLL = new tbiz_pictureBLL();

                    //Step1:将图片存档
                    string FromPath = ConfigurationManager.AppSettings["UploadFileBasePath"] + movie.ImgFileName;
                    string ToPath = ConfigurationManager.AppSettings["MovieImageDirectory"];
                    //TO DO 放到App启动时初始化一次
                    if (!Directory.Exists(ToPath))
                    {
                        Directory.CreateDirectory(ToPath);
                    }
                    ToPath = ConfigurationManager.AppSettings["MovieImageDirectory"] + movie.ImgFileName;
                    System.IO.File.Copy(FromPath, ToPath);

                    //Insert or Update tbiz_picture
                    tbiz_pictureEntity pictureEntity = new tbiz_pictureEntity();
                    pictureEntity.FileName = movie.ImgFileName;
                    pictureEntity.FileDirectory = ConfigurationManager.AppSettings["MovieImageDirectory"].SubStringLastIndexByChar('\\', 3);
                    pictureEntity.ReferenceID = (int)movie.Id;
                    pictureEntity.DataType = (int)Enumerator.DataType.Movie;
                    pictureEntity.CreateTime = DateTime.Now;
                    pictureBLL.Addtbiz_pictureEntity(pictureEntity);
                }
                return JsonConvert.SerializeObject(new { result = true, message = "", returnUrl="Index"});
            }
            return JsonConvert.SerializeObject(new { result = false, message = "" });
        }

        public ActionResult Edit(int id)
        {
            tbiz_movieBLL movieBLL = new tbiz_movieBLL();
            tbiz_movieEntity movie = movieBLL.Gettbiz_movieEntityById(id);
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            //Step2: 取出图片
            tbiz_pictureBLL pictureBLL = new tbiz_pictureBLL();
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            parm = new SqlDbParameter();
            parm.ColumnName = "ReferenceID";
            parm.ParameterName = "ReferenceID";
            parm.ParameterValue = movie.Id;
            parm.ColumnType = DbType.Int32;
            parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;
            parms.Add(parm);
            List<tbiz_pictureEntity> list = pictureBLL.GetAlltbiz_picture(parms, "Id ASC");
            if (list.Count > 0)
            {
                tbiz_pictureEntity pictureEntity = list[0];
                movie.ImgPathFileName = pictureEntity.FileDirectory + pictureEntity.FileName;
            }
            //movie type dropdownList
            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> categoryByLocalSelect = dictitemBLL.GetDictitemEntity("movie", "categoryByLocal");
            ViewBag.CategoryByLocal = new SelectList(categoryByLocalSelect, "PropertyValue", "PropertyName",  movie.CategoryByLocal);
            List<tcfg_dictitemEntity> saveLocalSelect = dictitemBLL.GetDictitemEntity("movie", "saveLocal");
            ViewBag.SaveLocal = new SelectList(saveLocalSelect, "PropertyValue", "PropertyName", movie.SaveLocal);
            return View(movie);
        }

        [HttpPost]
        public string Edit(tbiz_movieEntity movie)
        {
            if (ModelState.IsValid)
            {
                tbiz_movieBLL movieBLL = new tbiz_movieBLL();
                tbiz_movieEntity iniMovie = movieBLL.Gettbiz_movieEntityById(movie.Id);
                iniMovie.MovieFileName = movie.MovieFileName.UrlDecode(); //HttpUtility.HtmlDecode(movie.MovieFileName);
                iniMovie.MovieName = movie.MovieName.UrlDecode();
                iniMovie.CategoryByLocal = movie.CategoryByLocal;
                iniMovie.SaveLocal = movie.SaveLocal;
                iniMovie.PublicDate = movie.PublicDate;
                movieBLL.Updatetbiz_movieEntity(iniMovie);

                //电影图片文件
                if (!string.IsNullOrEmpty(movie.ImgFileName))
                {
                    tbiz_pictureBLL pictureBLL = new tbiz_pictureBLL();
                    List<SqlDbParameter> parms = new List<SqlDbParameter>();
                    SqlDbParameter parm = null;
                    parm = new SqlDbParameter();
                    parm.ColumnName = "ReferenceID";
                    parm.ParameterName = "ReferenceID";
                    parm.ParameterValue = movie.Id;
                    parm.ColumnType = DbType.Int32;
                    parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;
                    parms.Add(parm);
                    List<tbiz_pictureEntity>  list = pictureBLL.GetAlltbiz_picture(parms, "Id ASC");

                    //Step2:将图片存档
                    string FromPath = ConfigurationManager.AppSettings["UploadFileBasePath"] + movie.ImgFileName;
                    string ToPath = ConfigurationManager.AppSettings["MovieImageDirectory"];
                    //TO DO 放到App启动时初始化一次
                    if (!Directory.Exists(ToPath))
                    {
                        Directory.CreateDirectory(ToPath);
                    }
                    ToPath = ConfigurationManager.AppSettings["MovieImageDirectory"] + movie.ImgFileName;
                    System.IO.File.Copy(FromPath, ToPath);

                    if (list.Count == 0)
                    {
                        //Insert or Update tbiz_picture
                        tbiz_pictureEntity pictureEntity = new tbiz_pictureEntity();
                        pictureEntity.FileName = movie.ImgFileName; //电影主图文件
                        pictureEntity.FileDirectory = ConfigurationManager.AppSettings["MovieImageDirectory"].SubStringLastIndexByChar('\\', 3);
                        pictureEntity.ReferenceID = (int)movie.Id;
                        pictureEntity.DataType = (int)Enumerator.DataType.Movie;
                        pictureEntity.CreateTime = DateTime.Now;
                        pictureBLL.Addtbiz_pictureEntity(pictureEntity);
                    }
                    else
                    {
                        tbiz_pictureEntity pictureEntity = list[0];
                        string oldImgFile = pictureEntity.FileName;
                        pictureEntity.FileName = movie.ImgFileName;
                        pictureBLL.Updatetbiz_pictureEntity(pictureEntity);
                        //TO DO 旧图片清理逻辑
                        tbiz_pictureEntity  oldPic = new tbiz_pictureEntity();
                        oldPic.FileName = oldImgFile;
                        oldPic.FileDirectory = pictureEntity.FileDirectory;
                        oldPic.DataType = (int)Enumerator.DataType.Movie;
                        oldPic.CreateTime = pictureEntity.CreateTime;
                        pictureBLL.Addtbiz_pictureEntity(pictureEntity);
                    }
                }
                return JsonConvert.SerializeObject(new { result = true, message = "" });
            }
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return JsonConvert.SerializeObject(new { result = false, message = "" });
        }


        public ViewResult Detail(int id)
        {
            tbiz_movieBLL movieBLL = new tbiz_movieBLL();
            tbiz_movieEntity movie = movieBLL.Gettbiz_movieEntityById(id);

            string html = MVCHelper.RenderViewToString(this.ControllerContext, @"~/Areas/Movie/Views/Default/Detail.cshtml", movie);
            string movieDir = HttpContext.Server.MapPath("~/Movie/");
            if (!Directory.Exists(movieDir))
            {
                Directory.CreateDirectory(movieDir);
            }
            System.IO.File.WriteAllText(movieDir + id + ".html", html);

            return View(movie);
        }

        /// <summary>
        /// 全图片列表视图
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Graphic(int pageIndex = 1)
        {
            tbiz_movieBLL movieBLL = new tbiz_movieBLL();
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            if (!string.IsNullOrEmpty(Request.Form["MovieFileName"]))
            {
                parm = new SqlDbParameter();
                parm.ColumnName = "MovieFileName";
                parm.ParameterName = "MovieFileName";
                parm.ParameterValue = Request.Form["MovieFileName"].UrlDecode().Trim();
                parm.ColumnType = DbType.String;
                parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
                parms.Add(parm);
            }
            int recordCount;
            const int pageSize = 20;
            int limitOffSet = pageIndex - 1;  //MySQL Limit 第一个参数起始值0
            DataTable dataTable = movieBLL.GetMoviePicTablePage(parms, "Id DESC", pageSize, limitOffSet, out recordCount);
            List<tbiz_movieEntity> list = CommentHelper.CreateListFromTable<tbiz_movieEntity>(dataTable);
            //pageIndex:当前第几页,默认; pageSize:每页记录数,当list.count=34,pageSize=20 MvcPager生成2个page按钮
            var model = list.ToPagedList<tbiz_movieEntity>(pageIndex, pageSize);//Webdiyer.MvcPager 控件在模板中要求数据类型是: Webdiyer.WebControls.Mvc.PagedList<T> 
            ViewBag.totalItemCount = recordCount;
            ViewBag.pageSize = pageSize;
            ViewBag.currentPageIndex = pageIndex;

            if (Request.IsAjaxRequest())
                return PartialView("_MovieGraphicList", model);

            var lang = Request.QueryString["Languages"];
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang ?? "zh-CN"); //需要在bin中移植双语资源文件zh-CHS
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang ?? "zh-CN");
            return View(model);
        }

        /// <summary>
        /// 实践Mvc.Pager一个例子,单表查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Graphic_Bak(int pageIndex = 1)
        {
            tbiz_movieBLL movieBLL = new tbiz_movieBLL();
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            if (!string.IsNullOrEmpty(Request.Form["MovieFileName"]))
            {
                parm = new SqlDbParameter();
                parm.ColumnName = "MovieFileName";
                parm.ParameterName = "MovieFileName";
                parm.ParameterValue = Request.Form["MovieFileName"].UrlDecode().Trim();
                parm.ColumnType = DbType.String;
                parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
                parms.Add(parm);
            }
            int recordCount;
            const int pageSize = 20;
            int limitOffSet = pageIndex - 1;  //MySQL Limit 第一个参数起始值0
            List<tbiz_movieEntity> list = movieBLL.Gettbiz_moviePaged(parms, "Id DESC", pageSize, limitOffSet, out recordCount);
            //pageIndex:当前第几页,默认; pageSize:每页记录数,当list.count=34,pageSize=20 MvcPager生成2个page按钮
            var model = list.ToPagedList<tbiz_movieEntity>(pageIndex, pageSize);//Webdiyer.MvcPager 控件在模板中要求数据类型是: Webdiyer.WebControls.Mvc.PagedList<T> 
            ViewBag.totalItemCount = recordCount;
            ViewBag.pageSize = pageSize;
            ViewBag.currentPageIndex = pageIndex;

            if (Request.IsAjaxRequest())
                return PartialView("_MovieGraphicList", model);

            var lang = Request.QueryString["Languages"];
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang ?? "zh-CN"); //需要在bin中移植双语资源文件zh-CHS
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang ?? "zh-CN");
            return View(model);
        }

        public ViewResult Edit2()
        {
            return new ViewResult();
        }

        
    }
}