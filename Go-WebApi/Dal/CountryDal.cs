using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using DBA;
using Dapper;

namespace Go_WebApi.Dal
{
    public class CountryDal
    {
        /// <summary>
        /// 获取国家数据，返回List
        /// </summary>
        public List<dynamic> GetCountryList()
        {
            string sql = "select * from Country order by zPinyin_Index";
            return SqlHelper.Query(sql);
        }
    }
}