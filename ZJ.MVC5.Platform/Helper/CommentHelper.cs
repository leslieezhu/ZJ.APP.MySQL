using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ZJ.MVC5.Platform
{
    public class CommentHelper
    {

        /// <summary>
        ///  允许上传文件扩展名
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static bool IsAllowUploadFile(string fileExtension)
        {
            if (fileExtension.StartsWith("."))
            {
                fileExtension = fileExtension.TrimStart('.');
            }
            string allowFileExtensStr = ConfigurationManager.AppSettings["AllowUpLoadFile"];    //"jpg,jpeg,gif,png,bmp,doc,docx,pdf,zip,rar";

            if (allowFileExtensStr.IndexOf(fileExtension) == -1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// DataTable转集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
        {
            List<T> lst = new List<T>();
            foreach (DataRow r in tbl.Rows)
            {
                lst.Add(CreateItemFromRow<T>(r));
            }
            return lst;
        }

        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            Type objType = typeof(T);
            PropertyInfo[] properties = objType.GetProperties();
            T item = new T();
            foreach (PropertyInfo property in properties)
            {
                //object[] colAttr = property.GetCustomAttributes(typeof(Encrypt), false);
                //if (colAttr.Length > 0)
                //{
                //    string ID = row[property.Name.Substring(6)].ToString();
                //    string ecryptID = SecurityHelper.Encrypt(ID);
                //    property.SetValue(item, ecryptID, null);
                //    continue;
                //}
                if (!row.Table.Columns.Contains(property.Name) || row[property.Name] == null || row[property.Name] == DBNull.Value)
                {
                    continue;
                }
                else
                {
                    property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }

    }
}