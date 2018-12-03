using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Go_WebApi.Models
{
    public class TradeHistoryModel
    {
        public int zID { get; set; }
        public int zLeague_ID { get; set; }
        public bool zIsHalf { get; set; }
        public bool zIsHedging { get; set; }
        public bool zIsRp { get; set; }
        public string zType { get; set; }
        public int zCurrent_Time { get; set; }
        public string zCurrent_Bifen { get; set; }
        public string zPkFirst { get; set; }
        public string zPk { get; set; }
        public decimal zOdds { get; set; }
        public string zSxp { get; set; }
        public bool zIsRedCard { get; set; }
        public DateTime zDate { get; set; }
        public string zState { get; set; }
        public string zEnd_Bifen { get; set; }
        public decimal zPrice { get; set; }
        public decimal zGain { get; set; }
        public string zDescription { get; set; }
    }
}