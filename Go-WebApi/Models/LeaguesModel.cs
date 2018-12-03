using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Go_WebApi.Models
{
    public class LeaguesModel
    {
        public int ID { get; set; }
        public string ReqID { get; set; }
        public string Name_Cn { get; set; }
        public string Name_Fn { get; set; }
        public string Name_En { get; set; }
        public string Name_Sh { get; set; }
        public char Pinyin_Index { get; set; }
        public int Sort { get; set; }
        public int Hide { get; set; }
        public string Description { get; set; }
        public int Country_ID { get; set; }
        public int ReqType { get; set; }
    }
}