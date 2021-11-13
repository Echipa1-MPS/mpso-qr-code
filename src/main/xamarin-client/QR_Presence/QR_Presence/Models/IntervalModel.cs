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
}
