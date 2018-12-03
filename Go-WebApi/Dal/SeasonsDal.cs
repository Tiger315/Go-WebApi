using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using DBA;
using Dapper;
using Go_WebApi.Models;

namespace Go_WebApi.Dal
{
    public class SeasonsDal
    {
        /// <summary>
        /// 通过联赛编号，获取赛季数据，返回List
        /// </summary>
        /// <param name="zLeague_ID">联赛编号</param>
        public List<dynamic> GetSeasons(int zLeague_ID)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@zLeague_ID", zLeague_ID);

            string sql = "select * from Seasons Where zLeague_ID = @zLeague_ID Order by zSeason desc";
            return SqlHelper.Query(sql, pars);
        }
    }
}