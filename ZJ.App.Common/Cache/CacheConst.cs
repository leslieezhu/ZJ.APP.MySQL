using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace ZJ.App.Common.Cache
{
    /// <summary>
    /// 缓存常量定义
    /// </summary>
    /// <remarks>
    /// 与配置文件对应
    /// </remarks>
    public class CacheConst
    {
        public static readonly string CACHEMANAGER_DEFAULT = "CacheManager_Default";
    }

    public class CacheKeyConst
    {
        /// <summary>
        /// 字典表[tcfg_DictItem]缓存KEY
        /// </summary>
        public static readonly string CACHE_KEY_DICTIONARY_ALL = "CACHE_KEY_DICTIONARY_ALL";

        public static readonly string CACHE_KEY_MODELQUESTIONHIDE = "CACHE_KEY_MODELQUESTIONHIDE";

        public static readonly string CACHE_KEY_ROLESCOPE = "CACHE_KEY_ROLESCOPE";

        public static readonly string CACHE_KEY_PAGEROLERELATION = "CACHE_KEY_PAGEROLERELATION";

        public static readonly string CACHE_KEY_ALLMENU = "CACHE_KEY_ALLMENU";

        public static readonly string CACHE_KEY_USERSCOPE = "CACHE_KEY_USERSCOPE";

        public static readonly string CACHE_KEY_SLCT_QUESTION = "CACHE_KEY_SLCT_QUESTION";

        public static readonly string CACHE_KEY_SLCT_ANSWER = "CACHE_KEY_SLCT_ANSWER";

        public static readonly string CACHE_KEY_TEAM_QUESTION = "CACHE_KEY_TEAM_QUESTION";

        public static readonly string CACHE_KEY_TEAM_PROJ_QUESTION = "CACHE_KEY_TEAM_PROJ_QUESTION";

        public static readonly string CACHE_KEY_ASS_QUESTION = "CACHE_KEY_ASS_QUESTION";

        public static readonly string CACHE_KEY_SLCTQUESTION = "CACHE_KEY_SLCTQUESTION";

        public static readonly string CACHE_KEY_SLCTANSWER = "CACHE_KEY_SLCTANSWER";

        public static readonly string CACHE_KEY_ORG = "CACHE_KEY_ORG";

        public static readonly string CACHE_KEY_POS = "CACHE_KEY_POS";

        public static readonly string CACHE_KEY_EMP = "CACHE_KEY_EMP";

        public static readonly string CACHE_KEY_FIRSTPAGE_TOTAL = "CACHE_KEY_FIRSTPAGE_TOTAL";

        public static readonly string CACHE_KEY_FIRSTPAGE_TIME = "CACHE_KEY_FIRSTPAGE_TIME";

        public static readonly string CACHE_KEY_RECENT = "CACHE_KEY_RECENT";

        public static readonly string CACHE_KEY_RECENT_TIME = "CACHE_KEY_RECENT_TIME";

    }
}
