using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// https://www.cnblogs.com/linyongqin/articles/9452201.html
/// 1. MVCHelper.cs
/// 2. MemcacheMgr.cs
/// 3. RebuildStaticPageResult.cs 
/// </summary>
namespace ZJ.MVC5.Platform.Controllers
{
    public class HouseController : Controller
    {
        #region 生成静态页
        /// <summary>
        /// 生成静态页
        /// </summary>
        /// <param name="houseId"></param>
        private void CreateStaticPage(long houseId)
        {
            /*
            var house = houseService.GetDTOById(houseId);
            var pics = housePicService.GetByHouseId(houseId);
            var attachments = attachmentService.GetByHouseId(houseId);
            var goodHouses = houseService.GetHousesByCommunityId((long)house.CommunityId, 3);

            HouseIndexViewModel model = new HouseIndexViewModel();
            model.House = house;
            model.Pics = pics;
            model.Attachments = attachments;
            model.GoodHouses = goodHouses;
            string html = MVCHelper.RenderViewToString(this.ControllerContext, @"~/Areas/WebAdmin/Views/House/StaticIndex.cshtml", model);
            string houseDir = HttpContext.Server.MapPath("~/House/");
            if (!Directory.Exists(houseDir))
            {
                Directory.CreateDirectory(houseDir);
            }
            System.IO.File.WriteAllText(houseDir + houseId + ".html", html);

            Response.Write("<p>/House/" + houseId + ".html" + " " + house.CommunityName + house.Address + "</p>");
            Response.Flush();
            */
        }

        [HttpGet]
        public ActionResult RebuildAllStaticPage()
        {
            //缓存key
            ViewBag.Key = Guid.NewGuid().ToString();
            return View();
        }

        /// <summary>
        /// 推测是由RebuildAllStaticPage.cshtml 中iframe src调用
        /// </summary>
        /// <param name="key"></param>
        [HttpPost]
        public void RebuildAllStaticPageShow(string key)
        {
            Response.Write("====================== 生成静态页 begin ======================");
            /*
            Response.Flush();
            var houses = houseService.GetHouses();
            //生成页面总数
            int totleCount = houses.Count();
            //已生成页面数量
            int rebuildCount = 0;
            string percent = "";
            //缓存key
            string cacheKey = "StaticPage_" + key;
            foreach (var house in houses)
            {
                //生成静态页
                CreateStaticPage(house.Id);
                //已生成页面数量
                rebuildCount++;
                //计算百分比
                percent = (((rebuildCount * 1.0) / totleCount) * 100).ToString("0") + "%";
                //写入缓存
                HttpContext.Cache.Insert(cacheKey, new RebuildStaticPageResult { TotleCount = totleCount, RebuildCount = rebuildCount, Percent = percent }, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero);
                // Website.Common.MemcacheMgr.Instance.SetValue(cacheKey, new RebuildStaticPageResult { TotleCount = totleCount, RebuildCount = rebuildCount, Percent = percent } , TimeSpan.FromMinutes(3));

            }
            */
            Response.Write("====================== 生成静态页 end ======================");
            Response.Flush();
        }

        /// <summary>
        /// 生成进度, 由前台页面触发RebuildAllStaticPage.cshtml
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRebuildStaticPageProgress(string key)
        {
            string cacheKey = "StaticPage_" + key;
            //读取缓存
            RebuildStaticPageResult model = (RebuildStaticPageResult)HttpContext.Cache[cacheKey];
            //RebuildStaticPageResult model = Website.Common.MemcacheMgr.Instance.GetValue<RebuildStaticPageResult>(cacheName);
            if (model == null)
            {
                return Json(new RebuildStaticPageResult { TotleCount = 0, RebuildCount = 0, Percent = "0%" });
            }
            return Json(new RebuildStaticPageResult { TotleCount = model.TotleCount, RebuildCount = model.RebuildCount, Percent = model.Percent });
        }
        #endregion
    }
}