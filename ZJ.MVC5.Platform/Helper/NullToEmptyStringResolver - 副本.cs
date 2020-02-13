using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ZJ.MVC5.Platform
{
    public class NullToEmptyStringResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberSerialization">序列化成员</param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                .Select(x =>
                {
                    var property = CreateProperty(x, memberSerialization);
                    property.ValueProvider = new NullToEmptyStringValueProvider(x);
                    return property;
                }).ToList();
        }

        /// <summary>
        /// 小写
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        //protected override string ResolvePropertyName(string propertyName)
        //{
        //    return propertyName.ToLower();
        //}
    }



    [JsonObject(MemberSerialization.OptOut)]
    public class Person
    {
        public int Age { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        [JsonIgnore]
        public bool IsMarry { get; set; }

        public DateTime Birthday { get; set; }
    }
}