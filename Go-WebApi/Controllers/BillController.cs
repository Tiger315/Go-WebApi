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
    [RoutePrefix("api/v1/Bill")]
    public class BillController : ApiController
    {
        /// <summary>
        /// 获取帐单数据，返回List
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
    }
}
