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
    public class LeaguesDal
    {
        /// <summary>
        /// 获取联赛数据，返回List
        /// </summary>
        public List<dynamic> GetLeagues()
        {
            string sql = "Select L.zID, L.zReqID, L.zName_Cn, L.zName_Sh, L.zName_Fn, L.zName_En, L.zPinyin_Index, L.zSort, L.zDescription, L.zHide, L.zCountry_ID, C.zName_Cn as zCountry_Name From Leagues L Join Country C On L.zCountry_ID = C.zID where L.zHide = 0 Order by zPinyin_Index, zCountry_ID, zSort";
            return SqlHelper.Query(sql);
        }

        /// <summary>
        /// 通过国家编号，获取联赛数据，返回List
        /// </summary>
        /// <param name="zCountry_ID">国家编号</param>
        public List<dynamic> GetLeagues(int zCountry_ID)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@zCountry_ID", zCountry_ID);

            string sql = "Select L.*, C.zName_Cn As zCountry_Name From Leagues L Join Country C On L.zCountry_ID = C.zID Where L.zCountry_ID = @zCountry_ID Order by zSort, zPinyin_Index, zCountry_ID";
            return SqlHelper.Query(sql, pars);
        }

        /// <summary>
        /// 修改联赛数据，返回受影响的行数
        /// </summary>
        /// <param name="model">联赛对象</param>
        public int Update(LeaguesModel model)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@zID", model.ID);
            pars.Add("@zName_Cn", model.Name_Cn);
            pars.Add("@zName_Fn", model.Name_Fn);
            pars.Add("@zName_En", model.Name_En);
            pars.Add("@zName_Sh", model.Name_Sh);
            pars.Add("@zSort", model.Sort);
            pars.Add("@zHide", model.Hide);
            pars.Add("@zDescription", model.Description);

            List<string> sqls = new List<string>()
            {
                @"Update Leagues Set zName_Cn = @zName_Cn, zName_Fn = @zName_Fn, zName_En = @zName_En, zName_Sh = @zName_Sh, zSort = @zSort, zHide = @zHide, zDescription = @zDescription Where zID = @zID"
            };

            return SqlHelper.ExecuteTran(sqls, pars);
        }

        /// <summary>
        /// 通过联赛编号，获取平局率数据，返回List
        /// </summary>
        /// <param name="zLeague_ID">联赛编号</param>
        public List<dynamic> GetDraws(int zLeague_ID)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@zLeague_ID", zLeague_ID);

            string sql = "Select Left(S.zSeason, 4) As zSeason, L.zName_Cn, Convert(Varchar, Cast(Round(Sum(Case When zFl_Spf = 1 Then 1 Else 0 End) * 1.00 / Count(zFl_Spf) * 100, 2) As Numeric(5, 2))) + '%' As zFl_Draw, Convert(Varchar, Cast(Round(Sum(Case When zH1_Spf = 1 Then 1 Else 0 End) * 1.00 / Count(zH1_Spf) * 100, 2) As Numeric(5, 2))) + '%' As zH1_Draw, Convert(Varchar, Cast(Round(Sum(Case When zH2_Spf = 1 Then 1 Else 0 End) * 1.00 / Count(zH2_Spf) * 100, 2) As Numeric(5, 2))) + '%' As zH2_Draw From Score S Join Leagues L On S.zLeague_ID = L.zID Where zLeague_ID = @zLeague_ID Group By zLeague_ID, zSeason, L.zName_Cn Order by zSeason Desc";
            return SqlHelper.Query(sql, pars);
        }

        /// <summary>
        /// 通过联赛编号，获取绝杀球率数据，返回List
        /// </summary>
        /// <param name="zLeague_ID">联赛编号</param>
        public List<dynamic> GetFinalHitByLeagueID(int zLeague_ID)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@zLeague_ID", zLeague_ID);

            string sql = "GetFinalHitByLeagueID";
            return SqlHelper.QueryProc(sql, pars);
        }
    }
}