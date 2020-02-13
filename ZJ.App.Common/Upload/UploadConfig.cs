using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZJ.App.Common.Upload
{
    public class UploadConfig
    {
        public static string webpath = "/";

        public static string attachpath = "";
        //设置上传文件保存目录的方式,参考 UpLoad.cs  private string GetUpLoadPath 方法
        public static int attachsave = 2;
        //控制上传附件的类型
        public static string attachextension = "gif,jpg,png,bmp,rar,zip,doc,xls,txt";

        public static int attachimgsize = 1024;

        public static int attachfilesize = 51200;
        
        public static int attachimgmaxheight = 600;

        public static int attachimgmaxwidth = 800;

        public static int thumbnailwidth = 400;

        public static int thumbnailheight = 300;

        public static int watermarktype = 2;

        public static string watermarktext = "myJob500";

        public static int watermarkposition = 9;

        public static int watermarkimgquality = 80;

        public static string watermarkfont = "Tahoma";

        public static int watermarkfontsize = 12;

        public static string watermarkpic = "watermark.png";

        public static int watermarktransparency = 5;
    }
}
