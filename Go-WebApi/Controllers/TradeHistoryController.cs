using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Go_WebApi.Models;
using Go_WebApi.Dal;
using Go_WebApi.Filter;

namespace Go_WebApi.Controllers
{
    [CompressionFilter]
    [RoutePrefix("api/v1/TradeHistory")]
    public class TradeHistoryController : ApiController
    {
        /// <summary>
        /// 获取交易历史数据，返回List
        /// </summary>
        public IHttpActionResult Get()
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            TradeHistoryDal tra_dal = new TradeHistoryDal();

            try
            {
                var result = tra_dal.GetList();

                rd.Data = result;
                rd.Total = result.Count();

                rm.Code = Code.OK;
                rm.Description = rm.GetDescription(Code.OK);
                rm.Result = rd;
            }
            catch (Exception ex)
            {
                rm.Code = Code.InternalError;
                rm.Description = ex.Message;
            }

            return Json(rm);
        }

        /// <summary>
        /// 获取年帐单统计结果，返回List
        /// </summary>
        [Route("Bill/{year}")]
        public IHttpActionResult Get(int year)
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            TradeHistoryDal tra_dal = new TradeHistoryDal();

            //try
            //{
            //    var result = year == 0 ? tra_dal.GetBill() : tra_dal.GetYearBill(year);
            //    rd.Data = result;
            //    rd.Total = result.Count();

            //    rm.Code = Code.OK;
            //    rm.Description = rm.GetDescription(Code.OK);
            //    rm.Result = rd;
            //}
            //catch (Exception ex)
            //{
            //    rm.Code = Code.InternalError;
            //    rm.Description = ex.Message;
            //}

            return Json(rm);
        }

        /// <summary>
        /// 获取年帐单统计结果，返回List
        /// </summary>
        //[Route("Bill/Type/{year}")]
        //public IHttpActionResult Get(int year)
        //{
        //    ReturnMessageModel rm = new ReturnMessageModel();
        //    ResultDataModel rd = new ResultDataModel();
        //    TradeHistoryDal tra_dal = new TradeHistoryDal();

        //    try
        //    {
        //        var result = year == 0 ? tra_dal.GetBill() : tra_dal.GetYearBill(year);
        //        rd.Data = result;
        //        rd.Total = result.Count();

        //        rm.Code = Code.OK;
        //        rm.Description = rm.GetDescription(Code.OK);
        //        rm.Result = rd;
        //    }
        //    catch (Exception ex)
        //    {
        //        rm.Code = Code.InternalError;
        //        rm.Description = ex.Message;
        //    }

        //    return Json(rm);
        //}

        ///// <summary>
        ///// 获取月帐单统计结果，返回List
        ///// </summary>
        //[Route("Bill/{year}/{month}")]
        //public IHttpActionResult Get(int year, int month)
        //{
        //    ReturnMessageModel rm = new ReturnMessageModel();
        //    ResultDataModel rd = new ResultDataModel();
        //    TradeHistoryDal tra_dal = new TradeHistoryDal();

        //    try
        //    {
        //        var result = tra_dal.GetMonthBill(year, month);

        //        rd.Data = result;
        //        rd.Total = result.Count();

        //        rm.Code = Code.OK;
        //        rm.Description = rm.GetDescription(Code.OK);
        //        rm.Result = rd;
        //    }
        //    catch (Exception ex)
        //    {
        //        rm.Code = Code.InternalError;
        //        rm.Description = ex.Message;
        //    }

        //    return Json(rm);
        //}

        /// <summary>
        /// 新增交易历史数据，返回受影响的行数
        /// </summary>
        /// <param name="tra_model">交易历史对象</param>
        public IHttpActionResult Post([FromBody]TradeHistoryModel tra_model)
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            TradeHistoryDal tra_dal = new TradeHistoryDal();

            try
            {
                var result = tra_dal.Add(tra_model);
                rd.Data = result;
                rd.Total = 1;

                rm.Code = rd.Total > 0 ? Code.OK : Code.InternalError;
                rm.Description = rm.GetDescription(Code.OK);
                rm.Result = rd;
            }
            catch (Exception ex)
            {
                rm.Code = Code.InternalError;
                rm.Description = ex.Message;
            }

            return Json(rm);
        }
    }
}
