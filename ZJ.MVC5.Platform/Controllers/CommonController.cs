using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LitJson;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace ZJ.MVC5.Platform
{
    public class CommonController : Controller
    {
        private HttpContext context;

        // GET: Common
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadAttachment()
        {
            if (Request.Files == null || Request.Files.Count == 0)
            {
                return Json(new { result = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
            HttpPostedFileBase fileData = Request.Files[0];
            UploadFileResult resultTemp = new UploadFileResult();

            if (fileData != null)
            {
                try
                {
                    int _limitedFileSize = 10000000;
                    int.TryParse(ConfigurationManager.AppSettings["LimitedFileSize"], out _limitedFileSize);
                    if (fileData.ContentLength > _limitedFileSize)
                    {
                        return Json(new { result = false, Message = "上传的文件过大！" }, JsonRequestBehavior.AllowGet);
                    }
                    // 文件上传后的保存路径
                    string filePath = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UploadFileBasePath"]);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名

                    if (CommentHelper.IsAllowUploadFile(fileExtension))
                    {
                        string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称
                        fileData.SaveAs(filePath + saveName);

                        string baseDirectory = filePath.Substring(filePath.LastIndexOf('\\', filePath.Length - 2)).Trim('\\');
                        resultTemp.url = "/" + baseDirectory + "/" + saveName;
                        resultTemp.saveName = saveName;
                        resultTemp.name = fileName;
                        List<UploadFileResult> array = new List<UploadFileResult>();
                        array.Add(resultTemp);
                        return Json(new { result = true, files = array });
                    }
                    else
                    {
                        return Json(new { result = false, Message = "上传的文件类型不符合！" }, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    return Json(new { result = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { result = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// KindEditor  uploadJson
        /// </summary>
        public void UploadJsonKindEditor()
        {
            HttpContext context = System.Web.HttpContext.Current;
            //string aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1); //反射请求URL,如/Common/UploadJsonKindEditor/

            //文件保存根目录路径,这里为应用根目录
            string savePath = context.Server.MapPath("~");
            //文件保存目录URL
            string saveUrl = ConfigurationManager.AppSettings["QuestionImageDirectory"];
            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;
            this.context = context;

            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }
            string dirPath = savePath + ConfigurationManager.AppSettings["QuestionImageDirectory"];
            if (!Directory.Exists(dirPath))
            {
                showError("上传目录不存在。");
            }

            string dirName = context.Request.QueryString["dir"];
            if (string.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            string fileName = imgFile.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(((string)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((string)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            string fileUrl = saveUrl + newFileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void FileManagerJsonKindEditor()
        {
            HttpContext context = System.Web.HttpContext.Current;
            String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

            //根目录路径，相对路径
            String rootPath = "../attached/";
            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            String rootUrl = aspxUrl + "../attached/";
            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "";
            String moveupDirPath = "";

            String dirPath = context.Server.MapPath(rootPath);
            String dirName = context.Request.QueryString["dir"];
            if (!String.IsNullOrEmpty(dirName))
            {
                if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
                {
                    context.Response.Write("Invalid Directory name.");
                    context.Response.End();
                }
                dirPath += dirName + "/";
                rootUrl += dirName + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }

            //根据path参数，设置各路径和URL
            String path = context.Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = context.Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                context.Response.Write("Access is not allowed.");
                context.Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                context.Response.Write("Parameter is not valid.");
                context.Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                context.Response.Write("Directory does not exist.");
                context.Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(result));
            context.Response.End();
        }

        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }

   


        public JsonResult ImgTest()
        {
            List<Img> fileList = new List<Img>();
            Img img_1 = new Img()
            {
                name = "4ac84229-3755-4d86-975f-05e267e621b3.jpg",
                size = 879394,
                url= "/ImageBase/Movie/Chrysanthemum.jpg",
                thumbnailUrl= "/ImageBase/Movie/thumbnail/Chrysanthemum_small.jpg",
                deleteType= "DELETE",
                deleteUrl= "/Common/ImageHandler?file=Chrysanthemum.jpg" //会影响到
            };
            fileList.Add(img_1);

            img_1 = new Img()
            {
                name = "02e9e56d-fd44-41e4-ae09-7cfcb6b7404c.jpg",
                size = 879394,
                url = "/ImageBase/Movie/Desert.jpg",
                thumbnailUrl = "/ImageBase/Movie/thumbnail/Desert_small.jpg",
                deleteType = "DELETE",
                deleteUrl = "/Common/ImageHandler?file=Chrysanthemum.jpg"
            };
            fileList.Add(img_1);

            img_1 = new Img()
            {
                name = "Hydrangeas.jpg",
                size = 879394,
                url = "/ImageBase/Movie/Hydrangeas.jpg",
                thumbnailUrl = "/ImageBase/Movie/thumbnail/Hydrangeas_small.jpg",
                deleteType = "DELETE",
                deleteUrl = ""
            };
            fileList.Add(img_1);

            return Json(new { files = fileList, }, JsonRequestBehavior.AllowGet);
        }

    }
} 