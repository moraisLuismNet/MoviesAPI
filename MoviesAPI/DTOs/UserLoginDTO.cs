﻿using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string? Role { get; set; }
    }
}
