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
    public partial class tbiz_questionBLL
    {
        public DataTable GetQuestionDataTablePage(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        {
            tbiz_questionDAL dal = new tbiz_questionDAL();
            DataTable dt = dal.GetQuestionDataTablePage(parms, OrderBy, PageSize, PageIndex, out RecordCount);

            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> list = dictitemBLL.GetDictitemEntity("movie");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dt.Rows[i]["CategoryByLocalName"] = list.GetPropertyName("categoryByLocal", dt.Rows[i]["CategoryByLocal"]);//绑定外键参考
            //    dt.Rows[i]["SaveLocalName"] = list.GetPropertyName("saveLocal", dt.Rows[i]["SaveLocal"]);
            //}
            return dt;
        }
    }
}
