using System;
using System.Text;

namespace ZJ.App.Common.Extension
{
    public static class StringExtension
    {     
        

        /// <summary>
        /// 金额类型字符串处理
        /// </summary>
        /// <param name="strOrg">待处理的字符串</param>
        /// <param name="strFormatType">金额格式</param>
        /// <returns></returns>
        public static string SetString2MoneyType(string strOrg, string strFormatType)
        {
            string strReturn = strOrg;

            if (!string.IsNullOrEmpty(strOrg))
            {
                decimal dcTryParse = 0;
                if (decimal.TryParse(strOrg, out dcTryParse))
                {
                    strReturn = decimal.Parse(strReturn).ToString(strFormatType) + " RMB";
                }
            }
            else
            {
                strReturn = "0.00 RMB";
            }

            return strReturn;
        }
        public static string ConvertToPercent(object percent)
        {
            string val = null;
            decimal result;
            if (percent != null && decimal.TryParse(percent.ToString(), out result))
            {
                val = String.Format("{0:N2}%", Convert.ToDecimal(percent));
            }
            return val;
        }
        public static string ConvertToMoney(object money)
        {
            string val = null;
            decimal result;
            if (money != null && decimal.TryParse(money.ToString(), out result))
            {
                val=String.Format("￥{0:N2}", Convert.ToDecimal(money));
            }
            return val;
        }
        public static string ConvertToDec(object num)
        {
            string val = null;
            decimal result;
            if (num != null && decimal.TryParse(num.ToString(),out result))
            {
                val = String.Format("{0:F2}", Convert.ToDecimal(result));
            }
            return val;
         
        }
        public static string ConvertToDateTime(object dateTime)
        {
            string val = null;
            DateTime result;
            if (dateTime!=null && DateTime.TryParse(dateTime.ToString(),out result))
            {
                val =String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(result));
            }
            return val;
             
        }

        /// <summary>在HTML显示时转换成br标签
        /// </summary>
        public static string RNtoBr(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            else
                return str.Replace("\r\n", "<br />");
        }

        #region 过滤HTML及JAVASCRIPT
        public static string checkStr(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                html = regex1.Replace(html, ""); //过滤<script></script>标记 
                html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            }
            return html;
        }
        #endregion

        /// <summary>
        ///  从右起截取第几个指定字符串之后的字符串,例:\ZJ.APP.MySQL\ZJ.MVC5.Platform\uploadImage\ 截取从右数第3个\之后字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findChar">查找的目标字符</param>
        /// <param name="lastIndex">倒数第几个</param>
        /// <returns></returns>
        public static string SubStringLastIndexByChar(this string value, char findChar, int lastIndex)
        {
            int findIndex = -1;
            int startIndex = value.Length - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                startIndex = value.LastIndexOf(findChar, startIndex) - 1;
                findIndex = startIndex + 1;
            }
            return value.Substring(findIndex);
        }

    }
}
