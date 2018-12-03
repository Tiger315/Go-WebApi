using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Go_WebApi.Models
{
    public enum Code
    {
        [Description("OK")]
        OK = 1000,
        [Description("非法请求")]
        BadRequest = 1100,
        [Description("传入参数格式不正确")]
        ParError = 1800,
        [Description("Web Api系统内部错误")]
        InternalError = 1900
    };

    public class ReturnMessageModel
    {
        public Code Code { get; set; }
        public string Description { get; set; }
        public ResultDataModel Result { get; set; }

        /// <summary>  
        /// 获取枚举的描述  
        /// </summary>  
        /// <param name="en">枚举</param>  
        /// <returns>返回枚举的描述</returns>  
        public string GetDescription(Enum en)
        {
            Type type = en.GetType();   //获取类型  
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   //获取成员

            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性  

                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述  
                }
            }
            return en.ToString();
        }
    }

    public class ResultDataModel
    {
        public int Total { get; set; } = 0;
        public dynamic Data { get; set; } = 0;
    }

}