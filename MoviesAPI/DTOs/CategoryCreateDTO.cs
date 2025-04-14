﻿using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class CategoryCreateDTO
    {
        // To create a category
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "The maximum number of characters is 100!")]
        public string? Name { get; set; }
    }
}
