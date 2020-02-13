using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ZJ.App.BLL;
using ZJ.App.Common;
using ZJ.App.Entity;
using ZJ.App.Common.Extension;
using Webdiyer.WebControls.Mvc;
using System.Threading;
using System.Globalization;

namespace ZJ.MVC5.Platform.Areas.Movie.Controllers
{
    public class TestController : Controller
    {
        // GET: Movie/Test
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 统计哪些图片在 副本体检机构所需提供表格-机构汇总.xlsx 未配置
        /// </summary>
        public void ShopPic()
        {
            string result = "";
            tbiz_movieBLL movieBLL = new tbiz_movieBLL();
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            parm = new SqlDbParameter();
            parms.Add(parm);
            DataTable shopDetailDt = movieBLL.GetshopdetailPicID(parms);
            int maxPicID = 317;
            int j = 0;
            for (int i = 1; i < maxPicID; i++)
            {
                DataRow[] dr = shopDetailDt.Select("PicID=" + i);
                if (dr.Length == 0)
                {
                    result = result + i + ",";
                    j++;
                    if (j % 20 == 0)
                        result += "<br>";
                }
            }
            Response.Write(result);
        }


        public void ShopPic2()
        {
            tbiz_movieBLL movieBLL = new tbiz_movieBLL();
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            parm = new SqlDbParameter();
            parm.ColumnName = "Id";
            parm.ParameterName = "Id";
            parm.ParameterValue = "197,203,204,207,208,209,211,213,214,219,223,225,228,229,231,233,234,235,236,237,239,241,243,244,245,247,248,250,251,252,253,257,258,260,265,266,267,268,269,270,271,274,275,282,284,285,286,290,295,296,298,299,300,306,307,308,309,312,315";
            parm.ColumnType = DbType.String;
            parm.QualificationType = SqlDbParameter.QualificationSymbol.In;
            parms.Add(parm);
            //门店图片库去匹配门店, 2019.10.22 共有门店图片316张
            DataTable shopPicDt = movieBLL.Getshoppic(parms);

            for (int i = 0; i < shopPicDt.Rows.Count; i++)
            {
                string searchFileName = shopPicDt.Rows[i]["FileName"].ToString();
                string tipName = shopPicDt.Rows[i]["TipName"].ToString().Substring(0, 2); //供应商名字
                if (searchFileName.EndsWith(".jpg") || searchFileName.EndsWith(".png"))
                {
                    searchFileName = searchFileName.Substring(0, searchFileName.IndexOf('.'));
                }
                if (searchFileName.StartsWith("瑞慈_"))
                {
                    searchFileName = searchFileName.Split('_')[1];
                    if (searchFileName.IndexOf("机构") > -1)
                    {
                        searchFileName = searchFileName.Replace("机构", "");
                    }
                }
                if (searchFileName.IndexOf("爱康") > -1)
                {
                    if (searchFileName.IndexOf("爱康君安健疗国际") > -1)
                    {
                        searchFileName = shopPicDt.Rows[i]["FileName"].ToString().Substring(8, 4);
                    }
                    else
                    {
                        searchFileName = shopPicDt.Rows[i]["FileName"].ToString().Substring(6, 2);
                    }
                }
                else if (searchFileName.Length > 8)
                {
                    searchFileName = shopPicDt.Rows[i]["FileName"].ToString().Substring(0, 8);
                }
                //if (searchFileName.IndexOf("店") > -1)
                //{
                //    searchFileName = searchFileName.Replace("店", "");
                //}
                parms = new List<SqlDbParameter>();
                if (!string.IsNullOrEmpty(tipName))
                {
                    parm = new SqlDbParameter();
                    parm.ColumnName = "VendorName";
                    parm.ParameterName = "VendorName";
                    parm.ParameterValue = tipName;
                    parm.ColumnType = DbType.String;
                    parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;//SqlDbParameter.QualificationSymbol.Like;
                    parms = new List<SqlDbParameter>();
                    parms.Add(parm);
                }
                parm = new SqlDbParameter();
                parm.ColumnName = "ShopName";
                parm.ParameterName = "ShopName";
                parm.ParameterValue = searchFileName;
                parm.ColumnType = DbType.String;
                parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
                parms.Add(parm);
                DataTable shopDetailDt = movieBLL.Getshopdetail(parms);

                if (shopDetailDt.Rows.Count == 0)
                {
                    if (tipName == "慈铭" || tipName == "爱康")
                    {
                        parms = new List<SqlDbParameter>();
                        parm = new SqlDbParameter();
                        parm.ColumnName = "VendorName";
                        parm.ParameterName = "VendorName";
                        parm.ParameterValue = tipName;
                        parm.ColumnType = DbType.String;
                        parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;
                        parms = new List<SqlDbParameter>();
                        parms.Add(parm);

                        parm = new SqlDbParameter();
                        parm.ColumnName = "ShopAddress";
                        parm.ParameterName = "ShopAddress";
                        parm.ParameterValue = searchFileName;
                        parm.ColumnType = DbType.String;
                        parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
                        parms.Add(parm);
                        shopDetailDt = movieBLL.Getshopdetail(parms);
                        string searchKey1 = searchFileName.Substring(0, 2);

                        if (shopDetailDt.Rows.Count == 0)
                        {
                            parm.ParameterValue = searchKey1;
                            shopDetailDt = movieBLL.Getshopdetail(parms);
                        }
                        if (shopDetailDt.Rows.Count == 0 && searchFileName.Length > 4)
                        {
                            string searchKey2 = searchFileName.Substring(2, 2);
                            parm.ParameterValue = searchKey2;
                            shopDetailDt = movieBLL.Getshopdetail(parms);
                        }
                        if (shopDetailDt.Rows.Count == 0)
                        {
                            parm.ColumnName = "ShopName";
                            parm.ParameterName = "ShopName";
                            parm.ParameterValue = searchKey1;
                            shopDetailDt = movieBLL.Getshopdetail(parms);
                        }
                    }
                    else if (tipName == "美年")
                    {
                        parms = new List<SqlDbParameter>();
                        parm = new SqlDbParameter();
                        parm.ColumnName = "VendorName";
                        parm.ParameterName = "VendorName";
                        parm.ParameterValue = tipName;
                        parm.ColumnType = DbType.String;
                        parm.QualificationType = SqlDbParameter.QualificationSymbol.Equal;//SqlDbParameter.QualificationSymbol.Like;
                        parms = new List<SqlDbParameter>();
                        parms.Add(parm);
                        string searchKey1 = searchFileName.Substring(0, 2);
                        string searchKey2 = searchFileName.Substring(2, 2);
                        parm = new SqlDbParameter();
                        parm.ColumnName = "ShopName";
                        parm.ParameterName = "ShopName";
                        parm.ParameterValue = searchKey1;
                        parm.ColumnType = DbType.String;
                        parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
                        parms.Add(parm);
                        shopDetailDt = movieBLL.Getshopdetail(parms);
                        //searchFileName = searchKey1;

                        if (shopDetailDt.Rows.Count == 0)
                        {
                            parm.ParameterValue = searchKey2;
                            shopDetailDt = movieBLL.Getshopdetail(parms);
                            //searchFileName = searchKey1;
                        }

                        if (shopDetailDt.Rows.Count == 0) //再从地址中搜关键词
                        {
                            parm.ColumnName = "ShopAddress";
                            parm.ParameterName = "ShopAddress";
                            parm.ParameterValue = searchKey1;
                            shopDetailDt = movieBLL.Getshopdetail(parms);
                        }



                        if (shopDetailDt.Rows.Count == 0)
                        {
                            DataRow[] dr = shopDetailDt.Select("ShopName Like '%" + searchKey2 + "%'");

                            if (dr.Length == 0)
                            {
                                dr = shopDetailDt.Select("ShopAddress Like '%" + searchKey2 + "%'");
                            }
                            if (dr.Length > 0)
                            {
                                shopDetailDt = shopDetailDt.Clone();
                                foreach (var item in dr)
                                {
                                    shopDetailDt.ImportRow(item);
                                }
                                //searchFileName = searchKey1 + "&nbsp;" + searchKey2;
                            }
                        }

                    }
                }


                string shopDetailIds = "";
                string picID = "";
                string updateTpl = "UPDATE shopdetail SET PicID={0} WHERE ID IN ({1})";
                if (shopDetailDt.Rows.Count == 0)
                    Response.Write("供应商:[" + tipName + "]&nbsp;PicID:" + shopPicDt.Rows[i]["Id"].ToString() + ";&nbsp;搜索关键词:[" + searchFileName + "];&nbsp <br />");
                for (int j = 0; j < shopDetailDt.Rows.Count; j++)
                {
                    Response.Write("供应商:[" + tipName + "]&nbsp;PicID:" + shopPicDt.Rows[i]["Id"].ToString() + ";&nbsp;搜索关键词:[" + searchFileName + "];&nbsp");
                    Response.Write("=>");
                    Response.Write("shopDetailID:" + shopDetailDt.Rows[j]["ID"].ToString() + "&nbsp;" + shopDetailDt.Rows[j]["ShopName"].ToString() + "&nbsp;[" + shopDetailDt.Rows[j]["VendorName"].ToString() + "]&nbsp;地址:" + shopDetailDt.Rows[j]["ShopAddress"].ToString());
                    Response.Write("<br />");

                    picID = shopPicDt.Rows[i]["Id"].ToString();
                    if (j > 0)
                    {
                        shopDetailIds = shopDetailIds + "," + shopDetailDt.Rows[j]["ID"].ToString();
                    }
                    else
                    {
                        shopDetailIds = shopDetailDt.Rows[j]["ID"].ToString();
                    }
                }
                Response.Write(string.Format(updateTpl, picID, shopDetailIds));
                Response.Write("<br /><br />");
            }



            //parm = new SqlDbParameter();
            //parm.ColumnName = "ShopName";
            //parm.ParameterName = "ShopName";
            //parm.ParameterValue = "爱康国宾重庆龙湖源";
            //parm.ColumnType = DbType.String;
            //parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
            //parms.Add(parm);
            //DataTable dt = movieBLL.Getshopdetail(parms);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Response.Write(dt.Rows[i]["ShopName"].ToString());
            //    //Response.Write(dt.Rows[i]["FileName"].ToString());
            //    Response.Write("<br />");
            //}
            Response.End();
        }

    }
}