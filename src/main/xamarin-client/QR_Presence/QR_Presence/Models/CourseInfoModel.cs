﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QR_Presence.Models
{
    public class CourseInfoModel
    {
        public int Id_Course { get; set; }
        public string Name_C { get; set; }
        public string Id_Professor { get; set; }
        public string Desc { get; set; }
        public string Grading { get; set; }
        public List<IntervalModel> Intervals { get; set; }
        public string MainTitle => $"{Id_Course} {Name_C}";
        public string SecondTitle => $"{Id_Professor}";
        public string Icon1 => "course.png";
        public string Icon2 => "chalkboard-teacher";
        public Color BackColor => (Color)Application.Current.Resources["AccentDarkBlue"]; 


    }
}
