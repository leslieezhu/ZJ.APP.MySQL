using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZJ.App.Common.Cache;
using ZJ.App.Common;
using ZJ.App.Entity;
using ZJ.App.DAL;

namespace ZJ.App.BLL
{
    public partial class CacheBLL : BllBase
    {
        #region [Cache Method]
        public void Flush()
        {
            CacheHelper.GetManagerInstance().Flush();
        }

        /// <summary>
        /// 字典表[tcfg_DictItem]实体对象缓存
        /// </summary>
        /// <returns></returns>
        public List<tcfg_dictitemEntity> GetDictItem()
        {
            return this.CachedDictItem;
        }

        public List<tcfg_dictitemEntity> CachedDictItem
        {
            get
            {
                List<tcfg_dictitemEntity> list = null;
                if (CacheHelper.GetManagerInstance().ExistsCacheData(CacheKeyConst.CACHE_KEY_DICTIONARY_ALL))
                {
                    list = CacheHelper.GetManagerInstance().GetCacheData(CacheKeyConst.CACHE_KEY_DICTIONARY_ALL) as List<tcfg_dictitemEntity>;
                }
                else
                {
                    tcfg_dictitemDAL dal = new tcfg_dictitemDAL();
                    List<SqlDbParameter> parms = new List<SqlDbParameter>();
                    SqlDbParameter parm = new SqlDbParameter();
                    parm.ColumnName = tcfg_dictitemEntity.FieldName_IsDelete;
                    parm.ParameterValue = 0;

                    parms.Add(parm);
                    list = dal.GetAll(parms).OrderBy(t => t.OrderNumber).ToList();

                    CacheHelper.GetManagerInstance().AddCache(CacheKeyConst.CACHE_KEY_DICTIONARY_ALL, list);
                }

                return list;
            }
        }


        #endregion

    }
}
