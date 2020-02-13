using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Drawing;
using System.Net;
using System.Configuration;

namespace ZJ.App.Common.Upload
{
    public partial class UpLoad
    {
        /// <summary>上传附件的工具方法
        /// </summary>
        /// <param name="postedFile">HttpPostedFile对象</param>
        /// <param name="isKeepFileName">可选参数,默认值false表示使用新文件名</param>
        /// <returns>上传成功 msg=1; 失败 msg=0;</returns>
        public ReturnMsg fileSaveAs(HttpPostedFile postedFile, bool isKeepFileName = false)
        {
            ReturnMsg remsg = new ReturnMsg();
            remsg.Msg = 0;
            try
            {
                string fileExt = Utils.GetFileExt(postedFile.FileName); //文件扩展名，不含“.”
                string originalFileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1); //取得文件原名
                string fileName = isKeepFileName ? originalFileName : Utils.GetRamCode() + "." + fileExt; //随机文件名
                //生成上传文件保存目录
                string savePath = "";
                switch (UploadConfig.attachsave)
                {
                    case 1: //按年月日每天一个文件夹
                        savePath += DateTime.Now.ToString("yyyyMMdd");
                        break;
                    default: //按年月/日存入不同的文件夹
                        savePath += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                        break;
                }
                savePath += "/";
                string dirPath = GetUpLoadPath(); //上传目录相对路径

                //检查文件扩展名是否合法
                if (!CheckFileExt(fileExt))
                {
                    remsg.Msgbox = "不允许上传" + fileExt + "类型的文件！";
                    return remsg;
                }
                //检查文件大小是否合法
                if (!CheckFileSize(fileExt, postedFile.ContentLength))
                {
                    remsg.Msgbox = "文件超过限制的大小！";
                    return remsg;
                }
                //上传文件保存的完整路径,不包括文件名
                string toFullPath = Utils.GetMapPath(dirPath);
                if (!Directory.Exists(toFullPath))
                {
                    Directory.CreateDirectory(toFullPath);
                }
                //保存文件全路径包括文件名
                string toFileFullPath = toFullPath + fileName;
                postedFile.SaveAs(toFileFullPath);
                //保存在数据库中的文件路径,有保存路径(即按规则生成的目录+文件名)
                string returnFileName = savePath + fileName; ;
                remsg.Msg = 1;
                remsg.Msgbox = returnFileName;
                return remsg;
            }
            catch
            {
                remsg.Msgbox = "上传过程中发生意外错误！";
                return remsg;
            }
        }
    }

    public class ReturnMsg {
        private int _msg;
        /// <summary>返回结果标志,0表示上传失败,1表示上传成功
        /// 
        /// </summary>
        public int Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        private string _msgbox;
        /// <summary>保存上传文件处理信息, 如上传失败的原因
        /// 当上传成功,将返回"保存路径+文件名的字符串"
        /// </summary>
        public string Msgbox
        {
            get { return _msgbox; }
            set { _msgbox = value; }
        }
    
    }
}
