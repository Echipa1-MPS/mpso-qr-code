﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models
{
    public class UserModel
    {
        [PrimaryKey]
        public int Id_User { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LDAP { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
        public int Privilege { get; set; }
        public string FullName => $"Name:{Name} {SecondName} Group:{Group}";
    }
}
