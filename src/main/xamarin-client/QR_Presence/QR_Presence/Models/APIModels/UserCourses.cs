using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QR_Presence.Models.APIModels
{
    public class StudentsEnrolled
    {
        public string ldap { get; set; }
        public string name { get; set; }
        public string privilege { get; set; }
        public string secondName { get; set; }
        public string group { get; set; }

        public string FullName => $"{name} {secondName}";
    }

    public class CoursesEnrolled
    {
        public List<Interval> Intervals { get; set; }
        public string grading { get; set; }
        public string name_prof { get; set; }
        public int id_course { get; set; }
        public string name_course { get; set; }
        public List<StudentsEnrolled> Students_Enrolled { get; set; }
        public string desc { get; set; }
    }

    public class Cours
    {
        public string Desc { get; set; }
        public List<Interval> intervals { get; set; }
        public string Professor_Name { get; set; }
        public int Id_Professor { get; set; }
        public List<StudentsEnrolled> Students_Enrolled { get; set; }
        public string Name_C { get; set; }
        public string Grading { get; set; }
        public int Id_Course { get; set; }

        public string MainTitle => $"{Id_Course} {Name_C}";
        public string SecondTitle => $"{Professor_Name}";
        public string Icon1 => "course.png";
        public string Icon2 => "chalkboard-teacher";
        public Color BackColor => (Color)Application.Current.Resources["AccentDarkBlue"];

    }

    public class UserCourses
    {
        public List<CoursesEnrolled> courses_enrolled { get; set; }
        public int count { get; set; }
    }
}
