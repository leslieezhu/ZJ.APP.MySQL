
using System; 
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using ZJ.App.Common;

namespace ZJ.App.Entity
{
	/// <summary>
	/// 表tdept数据实体
	/// </summary>
	[Serializable]
    [DataContract]
	[TableName("tdept")]
	public partial class tdeptEntity : EntityBase
	{ 
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public tdeptEntity():base()
		{
			
		}

        public tdeptEntity(bool IsQueryTemplate)
            : base(IsQueryTemplate)
        {

        }
		 
		#endregion 
		
		#region 公共属性
        #region ID
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_ID = "ID";
        private int _ID;
		///<summary>
		///
		///</summary>
        [DataMember]
		[PrimaryKey]
		public int ID
		{
			get{return _ID;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_ID, value);
                }
                
                _ID = value;
            }
		}
        
	    #endregion

        #region CID
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_CID = "CID";
        private int? _CID;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column]
		public int? CID
		{
			get{return _CID;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_CID, value);
                }
                
                _CID = value;
            }
		}
        
	    #endregion

        #region DeptName
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_DeptName = "DeptName";
        private string _DeptName;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column]
		public string DeptName
		{
			get{return _DeptName;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_DeptName, value);
                }
                
                _DeptName = value;
            }
		}
        
	    #endregion

        #region Memo
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_Memo = "Memo";
        private string _Memo;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column]
		public string Memo
		{
			get{return _Memo;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_Memo, value);
                }
                
                _Memo = value;
            }
		}
        
	    #endregion

        #region FID
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_FID = "FID";
        private int? _FID;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column]
		public int? FID
		{
			get{return _FID;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_FID, value);
                }
                
                _FID = value;
            }
		}
        
	    #endregion

        #region CreateDate
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_CreateDate = "CreateDate";
        private System.DateTime? _CreateDate;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column]
		public System.DateTime? CreateDate
		{
			get{return _CreateDate;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_CreateDate, value);
                }
                
                _CreateDate = value;
            }
		}
        
	    #endregion

        #region CreateUser
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_CreateUser = "CreateUser";
        private string _CreateUser;
		///<summary>
		///
		///</summary>
        [DataMember]
        [Column]
		public string CreateUser
		{
			get{return _CreateUser;}
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_CreateUser, value);
                }
                
                _CreateUser = value;
            }
		}
        
	    #endregion
		#endregion
	}
}
