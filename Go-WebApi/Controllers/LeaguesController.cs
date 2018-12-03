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
    [RoutePrefix("api/v1/Leagues")]
    public class LeaguesController : ApiController
    {
        /// <summary>
        /// 获取联赛数据，返回List
        /// </summary>
        public IHttpActionResult Get()
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            LeaguesDal lea_dal = new LeaguesDal();

            try
            {
                var result = lea_dal.GetLeagues();

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
        /// 通过国家编号，获取联赛数据，返回List
        /// </summary>
        /// <param name="zCountry_ID">国家编号</param>
        [Route("{zCountry_ID:int}")]
        public IHttpActionResult Get(int zCountry_ID)
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            LeaguesDal dal = new LeaguesDal();

            try
            {
                var result = dal.GetLeagues(zCountry_ID);

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
        /// 通过联赛编号，获取平局率数据，返回List
        /// </summary>
        /// <param name="zLeague_ID">联赛编号</param>
        [Route("GetDraws/{zLeague_ID:int}")]
        public IHttpActionResult GetDraws(int zLeague_ID)
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            LeaguesDal lea_dal = new LeaguesDal();

            try
            {
                var result = lea_dal.GetDraws(zLeague_ID);

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
        /// 通过联赛编号，获取绝杀球率数据，返回List
        /// </summary>
        /// <param name="zLeague_ID">联赛编号</param>
        [Route("GetFinalHitByLeagueID/{zLeague_ID:int}")]
        public IHttpActionResult GetFinalHitByLeagueID(int zLeague_ID)
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            LeaguesDal dal = new LeaguesDal();
        
            try
            {
                var result = dal.GetFinalHitByLeagueID(zLeague_ID);

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
        /// 修改联赛数据，返回受影响的行数
        /// </summary>
        /// <param name="model">联赛对象</param>
        public IHttpActionResult Put([FromBody]LeaguesModel model)
        {
            ReturnMessageModel rm = new ReturnMessageModel();
            ResultDataModel rd = new ResultDataModel();
            LeaguesDal dal = new LeaguesDal();

            try
            {
                var result = dal.Update(model);
                rd.Total = result;

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
