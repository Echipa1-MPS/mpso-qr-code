using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models.APIModels
{
    public class ListOfDate
    {
        public string Date { get; set; }

        public string Format => DateTime.Parse(Date).ToString("MMMM, dd yyyy");
    }

    public class DatesModel
    {
        public int Id_interval { get; set; }
        public List<ListOfDate> List_of_dates { get; set; }
    }
}
