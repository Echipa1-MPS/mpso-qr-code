using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models.APIModels
{
    public class User
    {
        [PrimaryKey]
        public int User_id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Group { get; set; }
        public int Privilege { get;set; }


        public string Ldap { get; set; }
        public string SecondName { get; set; }

        public string MainTitle => $"{Name} {Surname}";
        public string MainTitleID => $"{Name}{Surname}";

        public string SecondTitle => $"{Email}";
        public string EndTitle => $"{Group}";
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
