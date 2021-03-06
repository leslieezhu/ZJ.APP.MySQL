﻿/**************************************************************
 * This file is part of  Project  
 * Copyright (C)2019 Microsoft
 * 
 * Author      : Generated by CodeSmith(Entity_v3.cst)
 * Mail        : 
 * Create Date : 2019/8/29 10:00:05
 * Summary     : this file was auto generated by tool . do not modify
 * 
 * 
 * Modified By : 
 * Date        : 
 * Mail        :
 * Comment     :增加新属性Cloumn,Dispaly 需要和相应的DalBase对应
 * *************************************************************/

using System; 
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZJ.App.Common;

namespace ZJ.App.Entity
{
	/// <summary>
	/// 表tbiz_questionanswer数据实体
	/// </summary>
	[Serializable]
    [DataContract]
	[TableName("tbiz_questionanswer")]
	public partial class tbiz_questionanswerEntity : EntityBase
	{ 
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public tbiz_questionanswerEntity():base()
		{
			
		}

        public tbiz_questionanswerEntity(bool IsQueryTemplate) : base(IsQueryTemplate)
        {

        }
		 
		#endregion 
		
		#region 公共属性
        #region Id
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_Id = "Id";
        private uint _Id;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column(Name="Id")]
        [PrimaryKey]
		[Identity]
        public uint Id
		{
			get{return _Id;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_Id, value);
                }
                
                _Id = value;
            }   
		}
        
	    #endregion

        #region AnswerTitle
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_AnswerTitle = "AnswerTitle";
        private string _AnswerTitle;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column(Name="AnswerTitle")]
		[MaxLength(1500)]
        public string AnswerTitle
		{
			get{return _AnswerTitle;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_AnswerTitle, value);
                }
                
                _AnswerTitle = value;
            }   
		}
        
	    #endregion

        #region QuestionId
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_QuestionId = "QuestionId";
        private int? _QuestionId;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column(Name="QuestionId")]
        public int? QuestionId
		{
			get{return _QuestionId;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_QuestionId, value);
                }
                
                _QuestionId = value;
            }   
		}
        
	    #endregion

        #region OptionIds
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_OptionIds = "OptionIds";
        private string _OptionIds;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column(Name="OptionIds")]
		[MaxLength(765)]
        public string OptionIds
		{
			get{return _OptionIds;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_OptionIds, value);
                }
                
                _OptionIds = value;
            }   
		}
        
	    #endregion

        #region PublicDate
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_PublicDate = "PublicDate";
        private int? _PublicDate;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column(Name="PublicDate")]
        public int? PublicDate
		{
			get{return _PublicDate;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_PublicDate, value);
                }
                
                _PublicDate = value;
            }   
		}
        
	    #endregion

        #region CreateTime
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_CreateTime = "CreateTime";
        private System.DateTime? _CreateTime;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column(Name="CreateTime")]
        public System.DateTime? CreateTime
		{
			get{return _CreateTime;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_CreateTime, value);
                }
                
                _CreateTime = value;
            }   
		}
        
	    #endregion
		#endregion
	}
}

