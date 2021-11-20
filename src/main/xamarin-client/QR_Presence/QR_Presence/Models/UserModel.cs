using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models
{
    public class UserModel
    {
        [PrimaryKey]
        public int user_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string group { get; set; }
        public int Privilege { get; set; }
        public string FullName => $"Name:{name} {surname} Group:{group}";
    }
}
