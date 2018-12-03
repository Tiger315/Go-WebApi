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
    [RoutePrefix("api/v1/Seasons")]
    public class SeasonsController : ApiController
    {
        /// <summary>
        /// 通过联赛编号，获取赛季数据，返回List
        /// </summary>
        /// <param name="zLeague_ID">联赛编号</param>
        [Route("{zLeague_ID:int}")]
        public IHttpActionResult Get(int zLeague_ID)
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            SeasonsDal sea_dal = new SeasonsDal();

            try
            {
                var result = sea_dal.GetSeasons(zLeague_ID);

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
