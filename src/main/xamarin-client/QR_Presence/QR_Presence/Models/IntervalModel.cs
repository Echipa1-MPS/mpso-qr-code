using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models
{
    public class IntervalModel
    {
        public string Name { get; set; }
        public string Day { get; set; }
        public int StartHour { get; set; }
        public int Step { get; set; }
        public string StartH => $"{StartHour}:00";
        public string StartE => $"{StartHour + Step}:00";
        public string IntervalHE => $"{StartH} - {StartE}";

    }

    public class IntervalPicker
    {
        public List<DayOfWeek> DayOfWeekCourse { get; set; } = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        public List<int> LenghtOfCourse { get; set; } = new List<int> { 1, 2, 3, 4 };

        public List<string> StartHOfCourse { get; set; } = new List<string> {
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00"
        };
        public DayOfWeek Day { get; set; }
        public int Duration { get; set; }
        public string StartH{ get; set; }

        public bool IsVisibleButton { get; set; }
        public string TextButton { get;set; }

    }
}
