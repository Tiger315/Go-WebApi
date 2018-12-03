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
    [RoutePrefix("api/v1/Country")]
    public class CountryController : ApiController
    {
        /// <summary>
        /// 获取国家数据，返回List
        /// </summary>
        public IHttpActionResult Get()
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            CountryDal cou_dal = new CountryDal();

            try
            {
                var result = cou_dal.GetCountryList();

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
