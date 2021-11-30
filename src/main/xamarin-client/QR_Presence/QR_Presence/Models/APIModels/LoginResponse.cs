using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Presence.Models.APIModels
{
    public class LoginResponse
    {
        public string role { get;set; }
        public string jwt_token { get;set; }
    }
}
