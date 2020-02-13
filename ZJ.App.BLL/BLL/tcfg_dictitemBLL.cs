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
    public partial class tcfg_dictitemBLL
    {

        public List<tcfg_dictitemEntity> GetDictitemEntity(string tableName)
        {
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            parm = new SqlDbParameter();
            parm.ColumnName = "TableName";
            parm.ParameterName = "TableName";
            parm.ParameterValue = tableName;
            parm.ColumnType = DbType.String;
            parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;
            parms.Add(parm);

            //TODO Cache
            return this.GetAlltcfg_dictitem(parms, "OrderNumber");
        }

        public List<tcfg_dictitemEntity> GetDictitemEntity(string tableName, string fieldName)
        {
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            parm = new SqlDbParameter();
            parm.ColumnName = "TableName";
            parm.ParameterName = "TableName";
            parm.ParameterValue = tableName;
            parm.ColumnType = DbType.String;
            parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;
            parms.Add(parm);

            parm = new SqlDbParameter();
            parm.ColumnName = "FieldName";
            parm.ParameterName = "FieldName";
            parm.ParameterValue = fieldName;
            parm.ColumnType = DbType.String;
            parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;
            parms.Add(parm);

            return this.GetAlltcfg_dictitem(parms, "OrderNumber");
        }
   
    }
}
