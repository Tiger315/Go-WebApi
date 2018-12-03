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
    public class ScoreDal
    {
        /// <summary>
        /// 通过联赛编号、赛季，获取赛果数据，返回List
        /// </summary>
        /// <param name="zLeague_ID">联赛编号</param>
        /// <param name="zSeason">赛季</param>
        public List<dynamic> GetScore(int zLeague_ID, string zSeason)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@zLeague_ID", zLeague_ID);
            pars.Add("@zSeason", zSeason);

            string sql = "select * from Score Where zLeague_ID = @zLeague_ID and zSeason = @zSeason Order by zGame_Date desc";
            return SqlHelper.Query(sql, pars);
        }
    }
}