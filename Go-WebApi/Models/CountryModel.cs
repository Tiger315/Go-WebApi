using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Go_WebApi.Models
{
    public class CountryModel
    {
        public int ID { get; set; }
        public string Name_Cn { get; set; }
        public string Name_Fn { get; set; }
        public string Name_En { get; set; }
        public string Icon { get; set; }
        public char Pinyin_Index { get; set; }
        public int Sort { get; set; }
        public int Hide { get; set; }
        public int Island_ID { get; set; }
    }
}