using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models
{
    public class CourseInfoModel
    {
        public int Id_Course { get; set; }
        public string Name_C { get; set; }
        public string Professor { get; set; }
        public string Desc { get; set; }
        public string Grading { get; set; }
        public List<IntervalModel> Intervals { get; set; }
        public string FullName => $"Name:{Name_C}  Professor:{Professor}";
    }
}
