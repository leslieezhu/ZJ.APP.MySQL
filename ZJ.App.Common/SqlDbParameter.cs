using System;
using System.Collections.Generic; 
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace ZJ.App.Common
{
    /// <summary>
    /// 参数查询类
    /// </summary>
    [Serializable]
    public class SqlDbParameter : WhereClauseBuilder
    {
        protected override char ParameterChar
        {
            get { return '@'; }
        }

        //private SysLoginData _SysLoginData = new SysLoginData();

        //public SysLoginData SysLoginData
        //{
        //    get { return _SysLoginData; }
        //    set { _SysLoginData = value; }
        //}

        private bool _isWhereClause = true;
        public bool IsWhereClause
        {
            get { return _isWhereClause; }
            set { _isWhereClause = value; }
        }

        private string _columnName = string.Empty;
        /// <summary>
        /// 列名
        /// </summary>
        /// <remarks>
        /// 对应SQL中的字段名
        /// </remarks>
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        private DbType _columnType = DbType.String;
        /// <summary>
        /// 列类型-默认DbType.String
        /// </summary>
        public DbType ColumnType 
        {
            get { return _columnType; }
            set { _columnType = value; }
        }

        public string _parameterName = string.Empty;
        /// <summary>
        /// 参数名 - 默认为列名(ColumnName)
        /// </summary>
        public string ParameterName
        {
            get
            {
                string paraName = "{0}{1}";
                if (string.IsNullOrEmpty(_parameterName))
                {
                    _parameterName = ColumnName;
                }

                return string.Format(paraName,ParameterChar,_parameterName);
            }
            set
            {
                _parameterName = value;
            }
        }

        public object ParameterValue { get; set; }         

        private QualificationSymbol _qualificationType = QualificationSymbol.Equal;
        /// <summary>
        /// 默认为Equal
        /// </summary>
        public QualificationSymbol QualificationType 
        {
            get { return _qualificationType; }
            set { _qualificationType = value; }
        }

        public List<SqlDbParameter> SqlDbParameters { get; set; }

        public SqlDbParameter()
        {
            this.SqlDbParameters = new List<SqlDbParameter>();
        }

        public SqlDbParameter(string ColumnName, DbType type, object value)
        {
            this.ColumnName = ColumnName;
            this.ColumnType = type;
            this.ParameterValue = value;
        }

        public static string BuildSqlStringWithTable(Hashtable ht, string sqlFormat, List<SqlDbParameter> parameters)
        {
            if (parameters == null) return string.Format(sqlFormat, "", "");
            StringBuilder str = new StringBuilder();

            string top = string.Empty;

            foreach (SqlDbParameter parameter in parameters)
            {
                if (!parameter.IsWhereClause) continue;

                if(ht[parameter.ColumnName] == null)
                {
                    continue;
                }

                string tranTableName = ht[parameter.ColumnName].ToString() + ".";

                switch (parameter.QualificationType)
                {
                    case QualificationSymbol.Equal:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " = " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Greater:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " > " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.GreaterAndEqual:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " >= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Less:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " < " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.LessAndEqual:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " <= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.NotEqual:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " <> " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Like:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " like  " + parameter.ParameterName + "");
                        break;
                    case QualificationSymbol.In:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " in ( " + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.NotIn:
                        str.Append(" and " + tranTableName + parameter.ColumnName + " not in ( " + parameter.ParameterValue + ")");
                        break;
                }
            }

            string sql = string.Format(sqlFormat,str);

            return sql;

        }

        public static string BuildSqlString(string sqlFormat, List<SqlDbParameter> parameters)
        {
            if (parameters == null || parameters.Count==0) return string.Format(sqlFormat, "", "");
            StringBuilder str = new StringBuilder();

            string top = string.Empty;

            foreach (SqlDbParameter parameter in parameters)
            {
                if (!parameter.IsWhereClause) continue;
                switch (parameter.QualificationType)
                {
                    case QualificationSymbol.Equal:
                        str.Append(" and " + parameter.ColumnName + " = " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Greater:
                        str.Append(" and " + parameter.ColumnName + " > " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.GreaterAndEqual:
                        str.Append(" and " + parameter.ColumnName + " >= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Less:
                        str.Append(" and " + parameter.ColumnName + " < " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.LessAndEqual:
                        str.Append(" and " + parameter.ColumnName + " <= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.NotEqual:
                        str.Append(" and " + parameter.ColumnName + " <> " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Like:
                        str.Append(" and " + parameter.ColumnName + " like  " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.IsNull:
                        str.Append(" and " + parameter.ColumnName + " is Null");
                        break;
                    case QualificationSymbol.In:
                        str.Append(" and " + parameter.ColumnName + " in ( " + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.NotIn:
                        str.Append(" and " + parameter.ColumnName + " not in ( " + parameter.ParameterValue + ")");
                        break;
                    //case QualificationSymbol.Top:
                    //    top = "top " + parameter.ParameterValue.ToString() + " ";
                    //    break;
                    case QualificationSymbol.Or:
                        str.Append(" and (" + BuildSqlOrString(parameter.SqlDbParameters) + ")");
                        break;
                    case QualificationSymbol.IsNotNull:
                        str.Append(" and " + parameter.ColumnName + " is not Null");
                        break;
                    case QualificationSymbol.Exists:
                        str.Append(" and Exists (" + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.Contains:
                        str.Append(" and CONTAINS (" + parameter.ColumnName + ",'" + parameter.ParameterValue + "')");
                        break;

                }
            }

            //string sql = string.Format(sqlFormat, top, str);
            string sql = string.Format(sqlFormat, str, "{0}"); //{0}重写ORDER BY占位符
            return sql;

        }

        public static string BuildSqlString(string sqlFormat, string SqlText, List<SqlDbParameter> parameters)
        {
            if (parameters == null) return string.Format(sqlFormat, "", "", SqlText);
            StringBuilder str = new StringBuilder();

            string top = string.Empty;

            foreach (SqlDbParameter parameter in parameters)
            {
                if (!parameter.IsWhereClause) continue;
                switch (parameter.QualificationType)
                {
                    case QualificationSymbol.Equal:
                        str.Append(" and " + parameter.ColumnName + " = " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Greater:
                        str.Append(" and " + parameter.ColumnName + " > " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.GreaterAndEqual:
                        str.Append(" and " + parameter.ColumnName + " >= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Less:
                        str.Append(" and " + parameter.ColumnName + " < " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.LessAndEqual:
                        str.Append(" and " + parameter.ColumnName + " <= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.NotEqual:
                        str.Append(" and " + parameter.ColumnName + " <> " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Like:
                        str.Append(" and " + parameter.ColumnName + " like  " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.IsNull:
                        str.Append(" and " + parameter.ColumnName + " is Null");
                        break;
                    case QualificationSymbol.In:
                        str.Append(" and " + parameter.ColumnName + " in ( " + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.NotIn:
                        str.Append(" and " + parameter.ColumnName + " not in ( " + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.Top:
                        top = "top " + parameter.ParameterValue.ToString() + " ";
                        break;
                    case QualificationSymbol.Or:
                        str.Append(" and (" + BuildSqlOrString(parameter.SqlDbParameters) + ")");
                        break;
                    case QualificationSymbol.IsNotNull:
                        str.Append(" and " + parameter.ColumnName + " is not Null");
                        break;
                    case QualificationSymbol.Exists:
                        str.Append(" and Exists (" + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.Contains:
                        str.Append(" and CONTAINS (" + parameter.ColumnName + ",'" + parameter.ParameterValue + "')");
                        break;

                }
            }

            string sql = string.Format(sqlFormat, top, str, SqlText);

            return sql;

        }

        public static string BuildSqlOrString(List<SqlDbParameter> parameters)
        {
            StringBuilder str = new StringBuilder();

            foreach (SqlDbParameter parameter in parameters)
            {
                if (str.Length > 0 && parameter.QualificationType != QualificationSymbol.Top)
                {
                    str.Append(" or ");
                }

                switch (parameter.QualificationType)
                {
                    case QualificationSymbol.Equal:
                        str.Append(parameter.ColumnName + " = " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Greater:
                        str.Append(parameter.ColumnName + " > " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.GreaterAndEqual:
                        str.Append(parameter.ColumnName + " >= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Less:
                        str.Append(parameter.ColumnName + " < " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.LessAndEqual:
                        str.Append(parameter.ColumnName + " <= " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.NotEqual:
                        str.Append(parameter.ColumnName + " <> " + parameter.ParameterName);
                        break;
                    case QualificationSymbol.Like:
                        str.Append(parameter.ColumnName + " like " + parameter.ParameterName );
                        break;
                    case QualificationSymbol.IsNull:
                        str.Append(parameter.ColumnName + " is Null");
                        break;
                    case QualificationSymbol.In:
                        str.Append(parameter.ColumnName + " in ( " + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.NotIn:
                        str.Append(parameter.ColumnName + " not in ( " + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.Or:
                        str.Append("(" + BuildSqlOrString(parameter.SqlDbParameters) + ")");
                        break;
                    case QualificationSymbol.IsNotNull:
                        str.Append(parameter.ColumnName + " is not Null");
                        break;
                    case QualificationSymbol.Exists:
                        str.Append(" and Exists (" + parameter.ParameterValue + ")");
                        break;
                    case QualificationSymbol.Contains:
                        str.Append(" and CONTAINS (" + parameter.ColumnName + ",'" + parameter.ParameterValue + "')");
                        break;

                }
            }

            return str.ToString();
        }

        public void SetDbCommond(DbCommand dbCommand)
        {

            switch (this.QualificationType)
            {
                case QualificationSymbol.In:

                case QualificationSymbol.Top:
                case QualificationSymbol.NotIn:
                case QualificationSymbol.IsNull:
                case QualificationSymbol.IsNotNull:
                    break;
                case QualificationSymbol.Or:
                    foreach (SqlDbParameter sub in this.SqlDbParameters)
                    {
                        sub.SetDbCommond(dbCommand);
                    }

                    break;
                default:
                    DbParameter parameter = dbCommand.CreateParameter();

                    parameter.DbType = this.ColumnType;
                    parameter.ParameterName = this.ParameterName;
                    if (QualificationType == QualificationSymbol.Like)
                        parameter.Value = "%" + ParameterValue + "%";
                    else
                        parameter.Value = this.ParameterValue;

                    dbCommand.Parameters.Add(parameter);
                    break;
            }
        }

        public enum QualificationSymbol
        {
            Equal = 0,
            Greater = 1,
            GreaterAndEqual = 2,
            Less = 3,
            LessAndEqual = 4,
            NotEqual = 5,
            Like = 6,
            In = 7,
            Top = 8,
            NotIn = 9,
            IsNull = 10,
            Or = 11,
            IsNotNull =12,
            Exists = 13,
            NotExists = 14,
            Contains = 15,
        }
    }
}
