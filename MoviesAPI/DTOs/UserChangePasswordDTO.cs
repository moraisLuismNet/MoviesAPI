﻿namespace MoviesAPI.DTOs
{
    public class UserChangePasswordDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
