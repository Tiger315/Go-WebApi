using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Go_WebApi.Models
{
    public class ScoreModel
    {
        public Guid Guid { get; set; }
        public int League_ID { get; set; }
        public string Req_ID { get; set; }
        public int Odds_ID { get; set; }
        public string Season { get; set; }
        public int Rounds { get; set; }
        public DateTime Game_Date { get; set; }
        public int HomeTeam_ID { get; set; }
        public string HomeTeam_Cn { get; set; }
        public string HomeTeam_Fn { get; set; }
        public string HomeTeam_En { get; set; }
        public int AwayTeam_ID { get; set; }
        public string AwayTeam_Cn { get; set; }
        public string AwayTeam_Fn { get; set; }
        public string AwayTeam_En { get; set; }
        public string Fl_Bifen { get; set; }
        public int Fl_HomeTeam_Goal { get; set; }
        public int Fl_AwayTeam_Goal { get; set; }
        public int Fl_Total_Goal { get; set; }
        public int Fl_Spf { get; set; }
        public string H1_Bifen { get; set; }
        public int H1_HomeTeam_Goal { get; set; }
        public int H1_AwayTeam_Goal { get; set; }
        public int H1_Total_Goal { get; set; }
        public int H1_Spf { get; set; }
        public string H2_Bifen { get; set; }
        public int H2_HomeTeam_Goal { get; set; }
        public int H2_AwayTeam_Goal { get; set; }
        public int H2_Total_Goal { get; set; }
        public int H2_Spf { get; set; }
    }
}