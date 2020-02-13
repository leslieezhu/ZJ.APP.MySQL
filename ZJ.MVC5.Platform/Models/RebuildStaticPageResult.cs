using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///  ASPNET MVC 生成静态页并显示生成进度之代码
/// </summary>
namespace ZJ.MVC5.Platform
{
    [Serializable]
    public class RebuildStaticPageResult
    {
        /// <summary>
        /// 生成页面总数
        /// </summary>
        public int TotleCount { get; set; }
        /// <summary>
        /// 已生成页面数量
        /// </summary>
        public int RebuildCount { get; set; }
        /// <summary>
        /// 生成页面数量百分比
        /// </summary>
        public string Percent { get; set; }
    }
}