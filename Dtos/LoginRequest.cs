using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Dtos
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required,DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        
    }
}