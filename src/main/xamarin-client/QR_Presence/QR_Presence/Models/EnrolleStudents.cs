using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models
{
    public class EnrolleStudents
    {

        public int id_course { get; set; }
        public List<Student> students_to_enroll { get; set; }
    }


    public class Student
    {
        public int id_user { get; set; }
    }
}
