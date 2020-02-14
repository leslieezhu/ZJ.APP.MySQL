using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ZJ.App.Common;

namespace ZJ.App.Entity
{
    public partial class tbiz_movieEntity
    {
        #region ImgPathFileName
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_ImgPathFileName = "ImgPathFileName";
        private string _ImgPathFileName;
        ///<summary>
        ///
        ///</summary>
        [DataMember]
        [NoColumn]
        public string ImgPathFileName
        {
            get { return _ImgPathFileName; }
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_ImgPathFileName, value);
                }

                _ImgPathFileName = value;
            }
        }

        #endregion

        #region ImgFileName
        ///<summary>
        /// 电影海报主图文件名
        ///</summary>
        public const string FieldName_ImgFileName = "ImgFileName";
        private string _ImgFileName;
        ///<summary>
        /// 电影海报主图文件名
        ///</summary>
        [DataMember]
        [NoColumn]
        public string ImgFileName
        {
            get { return _ImgFileName; }
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_ImgFileName, value);
                }

                _ImgFileName = value;
            }
        }

        #endregion

        #region FileDirectory
        ///<summary>
        ///
        ///</summary>
        public const string FieldName_FileDirectory = "FileDirectory";
        private string _FileDirectory;
        ///<summary>
        ///
        ///</summary>
        [DataMember]
        [NoColumn]
        public string FileDirectory
        {
            get { return _FileDirectory; }
            set
            {
                if (_IsQueryTemplate)
                {
                    this.RegisterQueryCondition(FieldName_FileDirectory, value);
                }

                _FileDirectory = value;
            }
        }

        #endregion
    }
}
