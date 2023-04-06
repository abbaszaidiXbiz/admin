using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Dtos
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = String.Empty;

        public string Message { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public bool Success { get; set; }

        public string UserId { get; set; } = String.Empty;


    }
}