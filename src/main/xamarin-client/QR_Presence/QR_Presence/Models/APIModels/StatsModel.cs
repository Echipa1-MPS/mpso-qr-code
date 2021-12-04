using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models.APIModels
{
   
    public class StatsModel
    {
        public int Absent { get; set; }
        public List<List<User>> List_qr_attendance { get; set; }

        [JsonProperty("full-strike")]
        public int FullStrike { get; set; }
    }
}
