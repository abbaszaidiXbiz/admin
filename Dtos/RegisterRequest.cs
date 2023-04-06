using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Dtos
{
    public class RegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;

        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not Match.")]
        public string ConfirmPassword { get; set; } = String.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

    }
}