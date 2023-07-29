using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBlazor.Models
{
    public class AuthorizationResponse
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }

    }
}
