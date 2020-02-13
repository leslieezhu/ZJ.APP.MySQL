using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using ZJ.App.Common;
using ZJ.App.Entity;

namespace ZJ.App.DAL
{
    /// <summary>
    /// Data Access Layer class tdept.
    /// </summary>
    public partial class tdeptDAL : DalBase<tdeptEntity>
    {
        #region 构造函数
        
		public tdeptDAL()
            : base()
        { }

        public tdeptDAL(string DbName)
            : base(DbName)
        { }

        public tdeptDAL(DbTransaction tran)
            : base(tran)
        { }
        
        #endregion
        
        #region public method
        
        public void BulkInsert(List<tdeptEntity> list)
        {
            foreach (tdeptEntity item in list)
            {
                this.Insert(item);
            }
        /*
            SqlBulkCopy bulkCopy;

            if (System.Transactions.Transaction.Current != null)
            {
                using (System.Transactions.TransactionScope t = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Suppress))
                {
                    bulkCopy = new SqlBulkCopy(CurrentDatabase.ConnectionString, SqlBulkCopyOptions.CheckConstraints);
                    bulkCopy.BulkCopyTimeout = 360;
                    bulkCopy.DestinationTableName = "tdept";
                    DataTable dataTable =ConvertToDataTable(list);
                    bulkCopy.ColumnMappings.Add("ID", "ID");
                    bulkCopy.ColumnMappings.Add("CID", "CID");
                    bulkCopy.ColumnMappings.Add("DeptName", "DeptName");
                    bulkCopy.ColumnMappings.Add("Memo", "Memo");
                    bulkCopy.ColumnMappings.Add("FID", "FID");
                    bulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");
                    bulkCopy.ColumnMappings.Add("CreateUser", "CreateUser");
                    
                    bulkCopy.WriteToServer(dataTable);
                    t.Complete();
                    bulkCopy.Close();
                }
            }
            else
            {
                bulkCopy = new SqlBulkCopy(CurrentDatabase.ConnectionString, SqlBulkCopyOptions.CheckConstraints);
                bulkCopy.BulkCopyTimeout = 360;
                bulkCopy.DestinationTableName = "tdept";
                DataTable dataTable =ConvertToDataTable(list);
                bulkCopy.ColumnMappings.Add("ID", "ID");
                bulkCopy.ColumnMappings.Add("CID", "CID");
                bulkCopy.ColumnMappings.Add("DeptName", "DeptName");
                bulkCopy.ColumnMappings.Add("Memo", "Memo");
                bulkCopy.ColumnMappings.Add("FID", "FID");
                bulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");
                bulkCopy.ColumnMappings.Add("CreateUser", "CreateUser");
                
                bulkCopy.WriteToServer(dataTable);
                bulkCopy.Close();
            }
             */
        }
        #endregion
        
        #region help method
        public DataTable ConvertToDataTable(List<tdeptEntity> list)
        {
            DataTable table = new DataTable("tdept");
            table.Columns.Add("ID", typeof(System.Int32));
            table.Columns.Add("CID", typeof(System.Int32));
            table.Columns.Add("DeptName", typeof(System.String));
            table.Columns.Add("Memo", typeof(System.String));
            table.Columns.Add("FID", typeof(System.Int32));
            table.Columns.Add("CreateDate", typeof(System.DateTime));
            table.Columns.Add("CreateUser", typeof(System.String));
            
            if (list != null && list.Count > 0)
            {
                foreach (tdeptEntity entity in list)
                {
                    table.Rows.Add(
                        entity.ID,
                        entity.CID,
                        entity.DeptName,
                        entity.Memo,
                        entity.FID,
                        entity.CreateDate,
                        entity.CreateUser
                        );
                }
            }
            return table;
        }
        #endregion
    }
}
