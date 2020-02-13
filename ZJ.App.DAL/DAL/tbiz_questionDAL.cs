using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZJ.App.Common;

namespace ZJ.App.DAL
{
    public partial class tbiz_questionDAL
    {

        public DataTable GetQuestionDataTablePage(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        {
            RecordCount = 0;
            string sql = @"
SELECT
	ID,
	CASE
	WHEN LENGTH(QuestionTitle) < 40 THEN
		QuestionTitle
	ELSE
		SUBSTR(QuestionTitle,1,40)
	END QuestionTitle,
	SubjectType,
	'' SubjectTypeName,
	QuestionType,
	'' QuestionTypeName,
	CreateTime 
FROM
	tbiz_question
WHERE 1=1 {0}";
            sql = SqlDbParameter.BuildSqlString(sql, parms);
            string sqlString = SqlDbParameter.BuildSqlString(sql, parms);
            DataTable dt = this.GetAll(sqlString, parms, OrderBy, PageSize, PageIndex, out RecordCount);
            return dt;
        }

    }
}
