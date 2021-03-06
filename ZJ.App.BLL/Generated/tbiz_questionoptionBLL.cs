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
using ZJ.App.DAL;

namespace ZJ.App.BLL
{
    /// <summary>
    /// Data Access Layer class tbiz_questionoption.
    /// </summary>
    public partial class tbiz_questionoptionBLL : BllBase
    {
        
		public void Addtbiz_questionoptionEntity(tbiz_questionoptionEntity entity)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            dal.InsertMySQL(entity);
        }

        public void Updatetbiz_questionoptionEntity(tbiz_questionoptionEntity entity)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            dal.Update(entity);
        }

        public void Disabletbiz_questionoptionEntityById(object Id)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            dal.Disabled(Id);
        }
        
        public void Deletetbiz_questionoptionEntityById(tbiz_questionoptionEntity entity)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            dal.Delete(entity);
        }
           
        public tbiz_questionoptionEntity Gettbiz_questionoptionEntityById(object Id)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            return dal.GetEntityById(Id);
        }
        
        //返回全部
        public List<tbiz_questionoptionEntity> GetAlltbiz_questionoption(List<SqlDbParameter> parms)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            return dal.GetAll(parms);
        }
        
        //返回单实体对象
        public tbiz_questionoptionEntity Gettbiz_questionoptionEntity(List<SqlDbParameter> parms)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            List<tbiz_questionoptionEntity> list = dal.GetAll(parms);
            if(list.Count > 0)
    	    {
    		    return list[0];
    	    }
    	    else
    	    {
    		    return null;
    	    }
        }
        
        public List<tbiz_questionoptionEntity> GetAlltbiz_questionoption(List<SqlDbParameter> parms, string orderBy)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            return dal.GetAll(parms, orderBy);
        }
        
        public List<tbiz_questionoptionEntity> Gettbiz_questionoptionPaged(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        { 
            tbiz_questionoptionDAL dal = new tbiz_questionoptionDAL();
            return dal.GetAllMySql(parms, OrderBy, PageSize, PageIndex, out RecordCount);
        }
        
        
    }
}
