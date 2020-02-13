using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary;

namespace ZJ.App.Common
{
    public partial class DalBase<T> where T : EntityBase
    {
        /// <summary>
        /// 改写原来Get()方法,根据主键获得实体对象
        /// </summary>
        /// <param name="primaryID">实体主键</param>
        /// <returns></returns>
        public T GetEntityById(object primaryID)
        {
            Type objType = typeof(T);
            string tbName = objType.Name;
            object[] tbNameAtts = objType.GetCustomAttributes(typeof(TableName), false);

            if (tbNameAtts != null && tbNameAtts.Length > 0)
            {
                TableName tableName = (TableName)tbNameAtts[0];
                tbName = tableName.Name;
            }
            string sqlStr = "SELECT {1} FROM {0} WHERE {2}";
            string fieldWheres = "";
            string selectFields = "";

            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object[] colAttr = property.GetCustomAttributes(typeof(Column), false);
                if (colAttr == null || colAttr.Length == 0)
                {
                    continue;
                }
                //获取真正的表字段名
                string fieldName = property.Name;
                foreach (Column item in colAttr)
                {
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        continue;
                    }
                    fieldName = item.Name;
                }
                selectFields += fieldName + ",";
                object[] pkAtts = property.GetCustomAttributes(typeof(PrimaryKey), false);
                if (pkAtts != null && pkAtts.Length > 0)
                {
                    fieldWheres += property.Name + "=@" + property.Name + " And ";
                }
            }

            if (selectFields != "") selectFields = selectFields.Trim(',');
            if (fieldWheres != "") fieldWheres = fieldWheres.Substring(0, fieldWheres.Length - 4);

            sqlStr = string.Format(sqlStr, tbName, selectFields, fieldWheres);
            DbCommand dbCommand = CurrentDatabase.GetSqlStringCommand(sqlStr);
            foreach (PropertyInfo property in properties)
            {
                object[] colAttr = property.GetCustomAttributes(typeof(Column), false);
                if (colAttr == null || colAttr.Length == 0)
                {
                    continue;
                }
                object[] pkAtts = property.GetCustomAttributes(typeof(PrimaryKey), false);

                if (pkAtts != null && pkAtts.Length > 0)
                {
                    CurrentDatabase.AddInParameter(dbCommand, "@" + property.Name, CovertToDBType(property.PropertyType), primaryID);
                }
            }

            if (_tran == null)
            {
                using (IDataReader theReader = CurrentDatabase.ExecuteReader(dbCommand))
                {
                    return GetFromReader<T>(theReader);
                }
            }
            else
            {
                using (IDataReader theReader = CurrentDatabase.ExecuteReader(dbCommand, _tran))
                {
                    return GetFromReader<T>(theReader);
                }
            }
        }


        /// <summary>
        /// 根据参数获得查询类别，带返回记录个数, 带排序
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <param name="OrderBy">排序字段名称</param>
        /// <returns></returns>
        public List<T> GetAll(List<SqlDbParameter> parameters, string Top, string OrderBy)
        {
            List<T> objList = new List<T>();

            Type objType = typeof(T);
            string tbName = objType.Name;
            object[] tbNameAtts = objType.GetCustomAttributes(typeof(TableName), false);

            if (tbNameAtts != null && tbNameAtts.Length > 0)
            {
                TableName tableName = (TableName)tbNameAtts[0];
                tbName = tableName.Name;
            }
            /**
            StringBuilder sbAllField = new StringBuilder();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object[] colAttr = property.GetCustomAttributes(typeof(Column), false);
                if (colAttr == null || colAttr.Length == 0)
                {
                    continue;
                }

                sbAllField.Append("[").Append(property.Name).Append("]").Append(",");
            }*/
            string allFields = GenrateSelectFields(objType.GetProperties());
            StringBuilder sbSql = new StringBuilder();
            if (!string.IsNullOrEmpty(Top))
            {
                sbSql.Append("SELECT Top " + Top + " {0} ").Append(allFields).Append(" FROM ").Append(tbName).Append(" WHERE 1=1 {1} ");
            }
            else
            {
                sbSql.Append("SELECT {0} ").Append(allFields).Append(" FROM ").Append(tbName).Append(" WHERE 1=1 {1} ");
            }
            if (!string.IsNullOrEmpty(OrderBy))
            {
                sbSql.Append(" ORDER BY ").Append(OrderBy);
            }
            string sql = SqlDbParameter.BuildSqlString(sbSql.ToString(), parameters);
            DbCommand dbCommand = this.CurrentDatabase.GetSqlStringCommand(sql);
            foreach (SqlDbParameter parameter in parameters)
            {
                parameter.SetDbCommond(dbCommand);
            }
            if (_tran == null)
            {
                using (IDataReader theReader = this.CurrentDatabase.ExecuteReader(dbCommand))
                {
                    return GetListFromReader<T>(theReader);
                }
            }
            else
            {
                using (IDataReader theReader = this.CurrentDatabase.ExecuteReader(dbCommand, _tran))
                {
                    return GetListFromReader<T>(theReader);
                }
            }
        }

        /// <summary>根据参数获得分页实体集合,长字段List优化版本,取出指定名称字段的定长值
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <param name="OrderBy">排序字段</param>
        /// <param name="PageSize">每页包含记录数量</param>
        /// <param name="PageIndex">获取指定的页</param>
        /// <param name="RecordCount">总数</param>
        /// <param name="Fields">需要截取的字段名列表</param>
        /// <param name="Length">截取的长度,默认值300</param>
        /// <returns></returns>
        public List<T> GetAllSubstring(List<SqlDbParameter> parameters, string OrderBy, int PageSize, int PageIndex, out int RecordCount,string[] Fields,int Length=300)
        {
            List<T> objList = new List<T>();

            Type objType = typeof(T);
            string tbName = objType.Name;
            object[] tbNameAtts = objType.GetCustomAttributes(typeof(TableName), false);

            if (tbNameAtts != null && tbNameAtts.Length > 0)
            {
                TableName tableName = (TableName)tbNameAtts[0];
                tbName = tableName.Name;
            }

            string KeyName = "";
            StringBuilder sbAllColumn = new StringBuilder();
            PropertyInfo[] Properties = objType.GetProperties();
            foreach (PropertyInfo property in Properties)
            {
                object[] colAttr = property.GetCustomAttributes(typeof(Column), false);
                if (colAttr == null || colAttr.Length == 0)
                {
                    continue;
                }

                object[] pkAttr = property.GetCustomAttributes(typeof(PrimaryKey), false);
                if (pkAttr != null && pkAttr.Length > 0)
                {
                    KeyName = property.Name;
                }

                if (sbAllColumn.Length != 0)
                {
                    sbAllColumn.Append(",");
                }
                sbAllColumn.Append("[").Append(property.Name).Append("]");
            }

            if (string.IsNullOrEmpty(OrderBy))
            {
                OrderBy = KeyName;
            }

            StringBuilder sqlStr = new StringBuilder("SELECT {0} ").Append(sbAllColumn.ToString()).Append(" FROM ");
            sqlStr.Append(tbName);
            sqlStr.Append(" WHERE 1=1 {1}");
            string sql = SqlDbParameter.BuildSqlString(sqlStr.ToString(), parameters);
            //重新拼接SUBSTRING(FieldName,0,Length)
            if (Fields.Length > 0)
            {
                foreach (string item in Fields)
                {
                    sql = sql.Replace("["+item+"]","SUBSTRING(["+item+"],0,"+Length+") "+item);
                }
            }
            string CmdText = @"WITH T AS
(
SELECT ROW_NUMBER() OVER(ORDER BY {2} ) AS row_number, * 
  FROM ({0}) as A
)
SELECT * FROM T WHERE row_number > @StartRowNum AND  row_number <= @EndRowNum
SELECT COUNT(1) FROM ({1}) AS B 
";

            CmdText = string.Format(CmdText, sql.ToString(), sql.ToString(), OrderBy);

            DbCommand dbCommand = this.CurrentDatabase.GetSqlStringCommand(CmdText);
            CurrentDatabase.AddInParameter(dbCommand, "@StartRowNum", System.Data.DbType.Int32, (PageIndex) * PageSize);
            CurrentDatabase.AddInParameter(dbCommand, "@EndRowNum", System.Data.DbType.Int32, (PageIndex + 1) * PageSize);

            if (parameters != null)
            {
                foreach (SqlDbParameter parameter in parameters)
                {
                    parameter.SetDbCommond(dbCommand);
                }
            }

            IDataReader reader = null;
            if (_tran == null)
            {
                reader = CurrentDatabase.ExecuteReader(dbCommand);
            }
            else
            {
                reader = CurrentDatabase.ExecuteReader(dbCommand, _tran);
            }

            using (reader)
            {
                objList = GetListFromReader<T>(reader);
                if (reader.NextResult() && reader.Read())
                    RecordCount = reader.GetInt32(0);
                else
                    RecordCount = 0;
            }
            return objList;
        }

        /// <summary>
        /// 不返回自增主键
        /// </summary>
        /// <param name="entity"></param>
        public void InsertQuick(T entity)
        {
            Type type = typeof(T);
            string tbName = type.Name;
            object[] tbNameAtts = type.GetCustomAttributes(typeof(TableName), false);

            if (tbNameAtts != null && tbNameAtts.Length > 0)
            {
                TableName tableName = (TableName)tbNameAtts[0];
                tbName = tableName.Name;
            }

            string sqlStr = "INSERT INTO {0} ({1}) VALUES ({2}){3}";
            StringBuilder sbFieldNames = new StringBuilder();
            StringBuilder sbFieldValues = new StringBuilder();

            PropertyInfo[] properties = type.GetProperties();
            /**
            foreach (PropertyInfo property in properties)
            {
                object[] colAttr = property.GetCustomAttributes(typeof(Column), false);
                if (colAttr == null || colAttr.Length == 0)
                {
                    continue;
                }
                //Set SQL when the Primary Key is identity
                object[] pkAtts = property.GetCustomAttributes(typeof(PrimaryKey), false);
                if (pkAtts != null && pkAtts.Length > 0)
                {
                    if (property.PropertyType == typeof(int))
                    {
                        sqlStr += "; set @" + property.Name + " = scope_identity();";
                        continue;
                    }
                }
                sbFieldNames.Append("[").Append(property.Name).Append("]").Append(",");
                sbFieldValues.Append("@").Append(property.Name).Append(",");
            }

            string fieldNames = sbFieldNames.ToString();
            string fieldValues = sbFieldValues.ToString();
            if (fieldNames != "") fieldNames = fieldNames.Trim(',');
            if (fieldValues != "") fieldValues = fieldValues.Trim(','); */

            string fieldNames = "";
            string fieldValues = "";
            string genericIDSql = ""; //set @Xxx = scope_identity()
            string genericPrimaryName = ""; //自增主键名
            GernateFieldNamesFieldValues(properties, ref fieldNames, ref fieldValues, ref genericIDSql);
            sqlStr = string.Format(sqlStr, tbName, fieldNames, fieldValues, genericIDSql);

            DbCommand dbCommand = CurrentDatabase.GetSqlStringCommand(sqlStr);

            foreach (PropertyInfo property in properties)
            {
                object[] colAttr = property.GetCustomAttributes(typeof(Column), false);
                if (colAttr == null || colAttr.Length == 0)
                {
                    continue;
                }

                //Set OutPut parameter when the Primary Key is identity
                object[] pkAtts = property.GetCustomAttributes(typeof(PrimaryKey), false);

                if (pkAtts != null && pkAtts.Length > 0)
                {
                    object[] identity = property.GetCustomAttributes(typeof(PrimaryKey), false);
                    //判断是否是自增主键
                    if (identity != null && identity.Length > 0 && property.PropertyType == typeof(int))
                    {
                        genericPrimaryName = property.Name;
                        CurrentDatabase.AddOutParameter(dbCommand, "@" + property.Name, DbType.Int32, 4);
                        continue;
                    }
                }

                Type columnType = property.PropertyType;
                // We need to check whether the property is NULLABLE
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    columnType = property.PropertyType.GetGenericArguments()[0];
                }
                CurrentDatabase.AddInParameter(dbCommand, "@" + property.Name, CovertToDBType(columnType), property.GetValue(entity, null));
            }

            if (_tran == null)
                CurrentDatabase.ExecuteNonQuery(dbCommand);
            else
                CurrentDatabase.ExecuteNonQuery(dbCommand, _tran);

            //当主键为整型自增时，将自增字段回写对象
            /*
            foreach (PropertyInfo property in properties)
            {
                object[] pkAtts = property.GetCustomAttributes(typeof(PrimaryKey), false);
                if (pkAtts != null && pkAtts.Length > 0)
                {
                    object[] identity = property.GetCustomAttributes(typeof(PrimaryKey), false);
                    if (identity != null && identity.Length > 0 && property.PropertyType == typeof(int))
                    {
                        object keyid = CurrentDatabase.GetParameterValue(dbCommand, "@" + property.Name);
                        property.SetValue(entity, keyid, null);
                        break;
                    }
                }
            }
            */
        }

    }
}
