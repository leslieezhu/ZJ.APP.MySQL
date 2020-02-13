using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZJ.App.Common;
using ZJ.App.DAL;
using ZJ.App.Entity;

namespace ZJ.App.BLL
{
    public partial class tbiz_movieBLL
    {
        public DataTable GetMovieDataTablePage(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        {
            tbiz_movieDAL dal = new tbiz_movieDAL();
            DataTable dt = dal.GetMovieDataTablePage(parms, OrderBy, PageSize, PageIndex, out RecordCount);

            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> list = dictitemBLL.GetDictitemEntity("movie");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["CategoryByLocalName"] = list.GetPropertyName("categoryByLocal", dt.Rows[i]["CategoryByLocal"]);//绑定外键参考
                dt.Rows[i]["SaveLocalName"] = list.GetPropertyName("saveLocal", dt.Rows[i]["SaveLocal"]);
            }
            return dt;
        }

        /// <summary>
        /// 电影列表数据 left join tbiz_picture
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataTable GetMoviePicTablePage(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        {
            tbiz_movieDAL dal = new tbiz_movieDAL();
            DataTable dt = dal.GetMoviePicTablePage(parms, OrderBy, PageSize, PageIndex, out RecordCount);
            return dt;
        }

        public DataTable Getshoppic(List<SqlDbParameter> parms)
        {
            tbiz_movieDAL dal = new tbiz_movieDAL();
            return dal.Getshoppic(parms);
        }


        public DataTable Getshopdetail(List<SqlDbParameter> parms)
        {
            tbiz_movieDAL dal = new tbiz_movieDAL();
            return dal.Getshopdetail(parms);
        }

        /// <summary>
        /// 哪些门店的图片有匹配的门店
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetshopdetailPicID(List<SqlDbParameter> parms)
        {
            tbiz_movieDAL dal = new tbiz_movieDAL();
            return dal.GetshopdetailPicID(parms);
        }
    }
}
