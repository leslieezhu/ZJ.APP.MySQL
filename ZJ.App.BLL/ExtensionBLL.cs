using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZJ.App.Entity;

namespace ZJ.App.BLL
{
    public static class CommontExtension
    {
        /// <summary>
        ///  根据属性类别和属性值返回属性名
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fieldName">属性类别名</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns></returns>
        public static string GetPropertyName(this List<tcfg_dictitemEntity> list, string fieldName ,object propertyValue)
        {
            int _propertyValue = 0;
            if (int.TryParse(propertyValue.ToString(), out _propertyValue))
            {
                tcfg_dictitemEntity dictitemEntity = list.Find(t => t.FieldName == fieldName & t.PropertyValue == _propertyValue);
                if (dictitemEntity == null)
                {
                    return string.Empty;
                }
                return dictitemEntity.PropertyName;
            }
            return string.Empty;

        }
    }
}