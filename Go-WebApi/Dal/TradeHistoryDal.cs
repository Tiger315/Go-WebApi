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
    public class TradeHistoryDal
    {
        /// <summary>
        /// 获取交易历史数据，返回List
        /// </summary>
        public List<dynamic> GetList()
        {
            string sql = "select zID, zLeague_ID, zIsHalf, zIsHedging, zType, zCurrent_Time, zCurrent_Bifen, zPkFirst, zPk, convert(varchar(10), zOdds) as zOdds, zSxp, zIsRp, zIsRedCard, convert(varchar(50), zDate, 23) as zDate, zState, zEnd_Bifen, convert(varchar(10), zPrice) as zPrice, convert(varchar(10), zGain) as zGain, zDescription from TradeHistory where year(zDate) = year(getdate()) order by zID desc";
            return SqlHelper.Query(sql);
        }

        ///// <summary>
        ///// 获取帐单统计结果，返回List
        ///// </summary>
        //public List<dynamic> GetBill()
        //{
        //    string procName = "Get_Bill_P";
        //    return SqlHelper.QueryProc(procName);
        //}

        ///// <summary>
        ///// 获取年帐单统计结果，返回List
        ///// </summary>
        //public List<dynamic> GetYearBill(int year)
        //{
        //    DynamicParameters pars = new DynamicParameters();
        //    pars.Add("@year", year);

        //    string procName = "Get_Year_Bill_P";
        //    return SqlHelper.QueryProc(procName, pars);
        //}

        ///// <summary>
        ///// 获取月帐单统计结果，返回List
        ///// </summary>
        //public List<dynamic> GetMonthBill(int year, int month)
        //{
        //    DynamicParameters pars = new DynamicParameters();
        //    pars.Add("@year", year);
        //    pars.Add("@month", month);

        //    string procName = "Get_Month_Bill_P";
        //    return SqlHelper.QueryProc(procName, pars);
        //}

        ///// <summary>
        ///// 获取玩法帐单统计结果，返回List
        ///// </summary>
        //public List<dynamic> GetTypeBill()
        //{
        //    string procName = "Get_Type_Bill_P";
        //    return SqlHelper.QueryProc(procName);
        //}

        /// <summary>
        /// 新增交易历史数据，返回受影响的行数
        /// </summary>
        /// <param name="tra_model">交易历史对象</param>
        public int Add(TradeHistoryModel tra_model)
        {
            DynamicParameters pars = new DynamicParameters();

            string[] bfs = tra_model.zEnd_Bifen.Split('-');
            if (bfs.Length > 1)
            {
                int z_bf = Convert.ToInt32(bfs[0]);
                int k_bf = Convert.ToInt32(bfs[1]);
                if (z_bf < k_bf)
                {
                    pars.Add("@zResult", "负");
                }
                else if (z_bf > k_bf)
                {
                    pars.Add("@zResult", "胜");
                }
                else
                {
                    pars.Add("@zResult", "平");
                }
            }
            else
            {
                pars.Add("@zResult", "^*^");
            }

            pars.Add("@zLeague_ID", tra_model.zLeague_ID);
            pars.Add("@zIsHalf", tra_model.zIsHalf);
            pars.Add("@zIsHedging", tra_model.zIsHedging);
            pars.Add("@zIsRp", tra_model.zIsRp);
            pars.Add("@zType", tra_model.zType);
            pars.Add("@zCurrent_Time", tra_model.zCurrent_Time);
            pars.Add("@zCurrent_Bifen", tra_model.zCurrent_Bifen);
            pars.Add("@zPkFirst", tra_model.zPkFirst);
            pars.Add("@zPk", tra_model.zPk);
            pars.Add("@zOdds", tra_model.zOdds);
            pars.Add("@zSxp", tra_model.zSxp);
            pars.Add("@zIsRedCard", tra_model.zIsRedCard);
            pars.Add("@zDate", tra_model.zDate);
            pars.Add("@zState", tra_model.zState);
            pars.Add("@zEnd_Bifen", tra_model.zEnd_Bifen);
            pars.Add("@zPrice", tra_model.zPrice);
            pars.Add("@zGain", tra_model.zGain);
            pars.Add("@zDescription", tra_model.zDescription);

            string sql = @"insert into TradeHistory(zLeague_ID, zIsHalf, zIsHedging, zType, zCurrent_Time, zCurrent_Bifen, zPk, zOdds,zSxp, zIsRedCard, zIsRp, zDate, zState, zEnd_Bifen, zPrice, zGain, zDescription, zPkFirst, zResult) values(@zLeague_ID, @zIsHalf, @zIsHedging, @zType, @zCurrent_Time, @zCurrent_Bifen, @zPk, @zOdds, @zSxp ,@zIsRedCard, @zIsRp, @zDate, @zState, @zEnd_Bifen, @zPrice, @zGain, @zDescription, @zPkFirst, @zResult); select @@identity;";
            int maxid = SqlHelper.ExecuteScalar(sql, pars);

            if (maxid > 0)
            {
                if (tra_model.zIsRp)
                {
                    double gain = 0;
                    double price = Convert.ToDouble(tra_model.zPrice);
                    double odds = Convert.ToDouble(tra_model.zOdds);
                    switch (tra_model.zState)
                    {
                        case "赢":
                            gain = tra_model.zOdds >= 1 ? price * 0.006 : price * 0.006 * odds;
                            break;
                        case "赢半":
                            gain = tra_model.zOdds >= 1 ? price * 0.006 * 0.5 : price * 0.006 * odds * 0.5;
                            break;
                        case "输":
                            gain = price * 0.006;
                            break;
                        case "输半":
                            gain = price * 0.006 * 0.5;
                            break;
                        default:
                            break;
                    }

                    sql = @"insert ReturnGain values(" + maxid + ", '" + Math.Round(gain, 2) + "', '" + tra_model.zDate + "');";
                    SqlHelper.ExecuteScalar(sql, null);
                }
            }

            return maxid;
            //List<string> sqls = new List<string>()
            //{
            //    @"insert into TradeHistory(zLeague_ID,zIsHalf,zIsHedging,zType,zCurrent_Time,zCurrent_Bifen,zPk,zOdds,zSxp,zIsRedCard,zIsRp,zDate,zState,zEnd_Bifen,zPrice,zGain,zDescription,zPkFirst,zResult) values(@zLeague_ID,@zIsHalf,@zIsHedging,@zType,@zCurrent_Time,@zCurrent_Bifen,@zPk,@zOdds,@zSxp,@zIsRedCard,@zIsRp,@zDate,@zState,@zEnd_Bifen,@zPrice,@zGain,@zDescription,@zPkFirst,@zResult);select @@identity;"
            //};
        }
    }
}