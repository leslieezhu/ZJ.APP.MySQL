using System;
using System.Collections;
using System.Web;
using System.IO;


namespace ZJ.App.Common.Upload
{
    public partial class UpLoad
    {

        public bool cropSaveAs(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
        {
            string fileExt = Utils.GetFileExt(fileName); 
            if (!IsImage(fileExt))
            {
                return false;
            }
            string newFileDir = Utils.GetMapPath(newFileName.Substring(0, newFileName.LastIndexOf(@"/") + 1));
            if (!Directory.Exists(newFileDir))
            {
                Directory.CreateDirectory(newFileDir);
            }
            try
            {
                string fileFullPath = Utils.GetMapPath(fileName);
                string toFileFullPath = Utils.GetMapPath(newFileName);
                return Thumbnail.MakeThumbnailImage(fileFullPath, toFileFullPath, 180, 180, cropWidth, cropHeight, X, Y);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>本次上传图片调用的处理方法
        /// </summary>
        public string fileSaveAs(HttpPostedFile postedFile, bool isThumbnail, bool isWater)
        {
            return fileSaveAs(postedFile, isThumbnail, isWater, false, false);
        }

        public string fileSaveAs(HttpPostedFile postedFile, bool isThumbnail, bool isWater, bool _isImage)
        {
            return fileSaveAs(postedFile, isThumbnail, isWater, _isImage, false);
        }

        public string fileSaveAs(HttpPostedFile postedFile, bool isThumbnail, bool isWater, bool _isImage, bool _isReOriginal)
        {
            try
            {
                string fileExt = Utils.GetFileExt(postedFile.FileName); //文件扩展名，不含“.”
                string originalFileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1); //取得文件原名
                string fileName = Utils.GetRamCode() + "." + fileExt; //随机文件名
                string dirPath = GetUpLoadPath(); //上传目录相对路径

                //检查文件扩展名是否合法
                if (!CheckFileExt(fileExt))
                {
                    return "{\"msg\": 0, \"msgbox\": \"不允许上传" + fileExt + "类型的文件！\"}";
                }
                //检查是否必须上传图片
                if (_isImage && !IsImage(fileExt))
                {
                    return "{\"msg\": 0, \"msgbox\": \"对不起，仅允许上传图片文件！\"}";
                }
                //检查文件大小是否合法
                if (!CheckFileSize(fileExt, postedFile.ContentLength))
                {
                    return "{\"msg\": 0, \"msgbox\": \"文件超过限制的大小啦！\"}";
                }
                //获得要保存的文件路径
                string serverFileName = dirPath + fileName;
                string serverThumbnailFileName = dirPath + "small_" + fileName;
                string returnFileName = serverFileName;
                //物理完整路径                    
                string toFileFullPath = Utils.GetMapPath(dirPath);
                //检查有该路径是否就创建
                if (!Directory.Exists(toFileFullPath))
                {
                    Directory.CreateDirectory(toFileFullPath);
                }
                //保存文件
                postedFile.SaveAs(toFileFullPath + fileName);
                //如果是图片，检查图片尺寸是否超出限制
                if (IsImage(fileExt) && (UploadConfig.attachimgmaxheight > 0 || UploadConfig.attachimgmaxwidth > 0))
                {
                    Thumbnail.MakeThumbnailImage(toFileFullPath + fileName, toFileFullPath + fileName, UploadConfig.attachimgmaxwidth, UploadConfig.attachimgmaxheight);
                }
                //是否生成缩略图
                if (IsImage(fileExt) && isThumbnail && UploadConfig.thumbnailwidth > 0 && UploadConfig.thumbnailheight > 0)
                {
                    Thumbnail.MakeThumbnailImage(toFileFullPath + fileName, toFileFullPath + "small_" + fileName, UploadConfig.thumbnailwidth, UploadConfig.thumbnailheight, "Cut");
                    returnFileName += "," + serverThumbnailFileName; //返回缩略图，以逗号分隔开
                }
                //是否打图片水印
                if (IsWaterMark(fileExt) && isWater)
                {
                    switch (UploadConfig.watermarktype)
                    {
                        case 1:
                            WaterMark.AddImageSignText(serverFileName, serverFileName,
                                UploadConfig.watermarktext, UploadConfig.watermarkposition,
                                UploadConfig.watermarkimgquality, UploadConfig.watermarkfont, UploadConfig.watermarkfontsize);
                            break;
                        case 2:
                            WaterMark.AddImageSignPic(serverFileName, serverFileName,
                                UploadConfig.watermarkpic, UploadConfig.watermarkposition,
                                UploadConfig.watermarkimgquality, UploadConfig.watermarktransparency);
                            break;
                    }
                }
                //如果需要返回原文件名
                if (_isReOriginal)
                {
                    return "{\"msg\": 1, \"msgbox\": \"" + serverFileName + "\", \"mstitle\": \"" + originalFileName + "\"}";
                }
                return "{\"msg\": 1, \"msgbox\": \"" + returnFileName + "\"}";
            }
            catch
            {
                return "{\"msg\": 0, \"msgbox\": \"上传过程中发生意外错误！\"}";
            }
        }


        #region 私有方法

        /// <summary>
        /// 返回上传目录相对路径,此目录有三部分组成 1).站点目录; 2).上传目录; 3).保存目录,即由UploadConfig.attachsave控制生成
        /// </summary>
        /// <param name="fileName">上传文件名</param>
        private string GetUpLoadPath()
        {
            string path = UploadConfig.webpath + UploadConfig.attachpath + "/"; //站点目录+上传目录
            switch (UploadConfig.attachsave)
            {
                case 1: //按年月日每天一个文件夹
                    path += DateTime.Now.ToString("yyyyMMdd");
                    break;
                default: //按年月/日存入不同的文件夹
                    path += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                    break;
            }
            return path + "/";
        }

        /// <summary>
        /// 是否需要打水印
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsWaterMark(string _fileExt)
        {
            //判断是否开启水印
            if (UploadConfig.watermarktype > 0)
            {
                //判断是否可以打水印的图片类型
                ArrayList al = new ArrayList();
                al.Add("bmp");
                al.Add("jpeg");
                al.Add("jpg");
                al.Add("png");
                if (al.Contains(_fileExt.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsImage(string _fileExt)
        {
            ArrayList al = new ArrayList();
            al.Add("bmp");
            al.Add("jpeg");
            al.Add("jpg");
            al.Add("gif");
            al.Add("png");
            if (al.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        private bool CheckFileExt(string _fileExt)
        {
            //检查危险文件
            string[] excExt = { "asp", "aspx", "php", "jsp", "htm", "html" };
            for (int i = 0; i < excExt.Length; i++)
            {
                if (excExt[i].ToLower() == _fileExt.ToLower())
                {
                    return false;
                }
            }
            //检查合法文件
            string[] allowExt = UploadConfig.attachextension.Split(',');
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == _fileExt.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查文件大小是否合法
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        /// <param name="_fileSize">文件大小(KB)</param>
        private bool CheckFileSize(string _fileExt, int _fileSize)
        {
            //判断是否为图片文件
            if (IsImage(_fileExt))
            {
                if (UploadConfig.attachimgsize > 0 && _fileSize > UploadConfig.attachimgsize * 1024)
                {
                    return false;
                }
            }
            else
            {
                if (UploadConfig.attachfilesize > 0 && _fileSize > UploadConfig.attachfilesize * 1024)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

    }
}
