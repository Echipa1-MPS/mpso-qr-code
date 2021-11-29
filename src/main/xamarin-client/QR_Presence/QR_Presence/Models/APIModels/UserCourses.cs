using System;
using System.Collections.Generic;
using System.Text;

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

    public class UserCourses
    {
        public List<CoursesEnrolled> courses_enrolled { get; set; }
        public int count { get; set; }
    }
}
