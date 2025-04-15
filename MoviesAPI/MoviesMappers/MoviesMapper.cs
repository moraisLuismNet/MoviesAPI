using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.MoviesMappers
{
    public class MoviesMapper : Profile
    {
        public MoviesMapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, MovieCreateDTO>().ReverseMap();
        }
    }
}
