using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZJ.App.Common;

namespace ZJ.App.DAL
{
    public partial class tbiz_movieDAL
    {
        public DataTable GetMovieDataTablePage(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        {
            RecordCount = 0;
            string sql = @"
                    SELECT * from
                    (
                        SELECT
                        A.ID,
                        A.MovieFileName,
                        A.MovieName,
                        A.CategoryByLocal,
                        '' CategoryByLocalName,
                        A.SaveLocal,
                        '' SaveLocalName,
                        A.PublicDate,
                        A.CreateTime
                        from tbiz_movie A
                    ) T WHERE 1=1 {0}";
            sql = SqlDbParameter.BuildSqlString(sql, parms);
            string sqlString = SqlDbParameter.BuildSqlString(sql, parms);
            DataTable dt = this.GetAll(sqlString, parms, OrderBy, PageSize, PageIndex, out RecordCount);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataTable GetMoviePicTablePage(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        {
            RecordCount = 0;
            string sql = @"
                    SELECT * from
                    (
                        SELECT
                        A.ID,
                        A.MovieName,
                        A.PublicDate,
                        B.FileName,
                        B.FileDirectory
                        from tbiz_movie A LEFT JOIN (SELECT FileName,FileDirectory,ReferenceID FROM tbiz_picture WHERE DataType=" + (int)Enumerator.DataType.Movie + @") B
                        ON A.ID = B.ReferenceID 
                    ) T WHERE 1=1 {0}";
            sql = SqlDbParameter.BuildSqlString(sql, parms);
            string sqlString = SqlDbParameter.BuildSqlString(sql, parms);
            DataTable dt = this.GetAll(sqlString, parms, OrderBy, PageSize, PageIndex, out RecordCount);
            return dt;
        }

        /// <summary>
        /// 临时方法,用于体检图片guid批量工具
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable Getshoppic(List<SqlDbParameter> parms)
        {
            string sql = @"
    SELECT * FROM shoppic
	WHERE 1=1 {1}";
            sql = SqlDbParameter.BuildSqlString(sql, parms);
            string sqlString = SqlDbParameter.BuildSqlString(sql, parms);
            DataTable dt = this.GetSqlData(sqlString, parms);
            return dt;
        }

        public DataTable Getshopdetail(List<SqlDbParameter> parms)
        {
            string sql = @"
        SELECT * FROM shopdetail
        WHERE 1=1 {1}";
            sql = SqlDbParameter.BuildSqlString(sql, parms);
            string sqlString = SqlDbParameter.BuildSqlString(sql, parms);
            DataTable dt = this.GetSqlData(sqlString, parms);
            return dt;
        }

        public DataTable GetshopdetailPicID(List<SqlDbParameter> parms)
        {
            string sql = @"
        SELECT PicID FROM shopdetail WHERE PicID>0 ORDER BY PicID ";
            sql = SqlDbParameter.BuildSqlString(sql, parms);
            string sqlString = SqlDbParameter.BuildSqlString(sql, parms);
            DataTable dt = this.GetSqlData(sqlString, parms);
            return dt;
        }

    }
}
