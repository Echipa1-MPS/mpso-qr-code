using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models.APIModels
{
    public class User
    {
        [PrimaryKey]
        public int user_id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string group { get; set; }
        public int Privilege { get;set; }

        public string MainTitle => $"{user_id} {name} {surname} ";
        public string SecondTitle => $"{email}";
        public string EndTitle => $"{group}";
        public string Icon1 => "profilem.png";
        public string Icon2 => "envelope-open-text";
    }

    public class StudentsAdmin
    {
        public List<User> students { get; set; }
    }

    public class TeachersAdmin
    {
        public List<User> teachers { get; set; }
    }

}
