using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QR_Presence.Models
{
    public class TeamMembersModel
    {
        public string ProfileImage { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string GitUrl { get; set; }

        public string GitHubUser { get;set; }
        public string Role { get; set; }

    }
}
