﻿using MoviesAPI.Validators;

namespace MoviesAPI.DTOs
{
    public class MovieCreateDTO
    {
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int Duration { get; set; }

        [WeightFileValidator(MaximumWeightInMegaBytes: 4)]
        [FileTypeValidator(GroupFileType.Image)]
        public IFormFile? ImageFile { get; set; }

        public bool AllPublic { get; set; } = true;
        public DateTime CreationDate { get; set; }
        public int categoryId { get; set; }
    }
}
