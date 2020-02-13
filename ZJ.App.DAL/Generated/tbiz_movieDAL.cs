﻿/**************************************************************
 * This file is part of SMP Project
 * Copyright (C)2019 Microsoft
 * 
 * Author      : Generated by CodeSmith(DAL_v3.cst)
 * Mail        : 
 * Create Date : 2019/8/29 10:00:06
 * Summary     : this file was auto generated by tool . do not modify
 * 
 * 
 * Modified By : 
 * Date        : 
 * Mail        : 
 * Comment     :   
 * *************************************************************/
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
    /// Data Access Layer class tbiz_movie.
    /// </summary>
    public partial class tbiz_movieDAL : DalBase<tbiz_movieEntity>
    {
        #region 构造函数
        
		public tbiz_movieDAL(): base()
        { }

        public tbiz_movieDAL(string DbName): base(DbName)
        { }

        public tbiz_movieDAL(DbTransaction tran): base(tran)
        { }
        
        #endregion
        
        #region public method
        
        public void BulkInsert(List<tbiz_movieEntity> list)
        {
            foreach (tbiz_movieEntity item in list)
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
                    bulkCopy.DestinationTableName = "tbiz_movie";
                    DataTable dataTable =ConvertToDataTable(list);
                    bulkCopy.ColumnMappings.Add("Id", "Id");
                    bulkCopy.ColumnMappings.Add("MovieFileName", "MovieFileName");
                    bulkCopy.ColumnMappings.Add("MovieName", "MovieName");
                    bulkCopy.ColumnMappings.Add("CategoryByLocal", "CategoryByLocal");
                    bulkCopy.ColumnMappings.Add("SaveLocal", "SaveLocal");
                    bulkCopy.ColumnMappings.Add("PublicDate", "PublicDate");
                    bulkCopy.ColumnMappings.Add("CreateTime", "CreateTime");
                    
                    bulkCopy.WriteToServer(dataTable);
                    t.Complete();
                    bulkCopy.Close();
                }
            }
            else
            {
                bulkCopy = new SqlBulkCopy(CurrentDatabase.ConnectionString, SqlBulkCopyOptions.CheckConstraints);
                bulkCopy.BulkCopyTimeout = 360;
                bulkCopy.DestinationTableName = "tbiz_movie";
                DataTable dataTable =ConvertToDataTable(list);
                bulkCopy.ColumnMappings.Add("Id", "Id");
                bulkCopy.ColumnMappings.Add("MovieFileName", "MovieFileName");
                bulkCopy.ColumnMappings.Add("MovieName", "MovieName");
                bulkCopy.ColumnMappings.Add("CategoryByLocal", "CategoryByLocal");
                bulkCopy.ColumnMappings.Add("SaveLocal", "SaveLocal");
                bulkCopy.ColumnMappings.Add("PublicDate", "PublicDate");
                bulkCopy.ColumnMappings.Add("CreateTime", "CreateTime");
                
                bulkCopy.WriteToServer(dataTable);
                bulkCopy.Close();
            }
             */
        }
        #endregion
        
        #region help method
        public DataTable ConvertToDataTable(List<tbiz_movieEntity> list)
        {
            DataTable table = new DataTable("tbiz_movie");
            table.Columns.Add("Id", typeof(System.UInt32));
            table.Columns.Add("MovieFileName", typeof(System.String));
            table.Columns.Add("MovieName", typeof(System.String));
            table.Columns.Add("CategoryByLocal", typeof(System.Int32));
            table.Columns.Add("SaveLocal", typeof(System.SByte));
            table.Columns.Add("PublicDate", typeof(System.Int32));
            table.Columns.Add("CreateTime", typeof(System.DateTime));
            
            if (list != null && list.Count > 0)
            {
                foreach (tbiz_movieEntity entity in list)
                {
                    table.Rows.Add(
                        entity.Id,
                        entity.MovieFileName,
                        entity.MovieName,
                        entity.CategoryByLocal,
                        entity.SaveLocal,
                        entity.PublicDate,
                        entity.CreateTime
                        );
                }
            }
            return table;
        }
        #endregion
    }
}