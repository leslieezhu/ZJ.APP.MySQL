﻿using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;


namespace ZJ.MVC5.Platform
{
    public class NullToEmptyStringValueProvider : IValueProvider
    {
        private readonly PropertyInfo _memberInfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memberInfo"></param>
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _memberInfo = memberInfo;
        }

        /// <inheritdoc />
        /// <summary>
        /// 获取Value
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            var result = _memberInfo.GetValue(target);
            if (_memberInfo.PropertyType == typeof(string) && result == Convert.DBNull)
            {
                result = string.Empty;
            }

            return result;

        }

        /// <inheritdoc />
        /// <summary>
        /// 设置Value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            _memberInfo.SetValue(target, value);
        }
    }
}