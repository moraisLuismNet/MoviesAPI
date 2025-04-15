using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Models;
using MoviesAPI.Repository;

namespace MoviesAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        
        public async Task<IEnumerable<MovieDTO>> GetAllAsyncService()
        {
            var movies = _movieRepository.GetAllRepository();
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<MovieDTO> GetByIdAsyncService(int movieId)
        {
            var movie = _movieRepository.GetByIdRepository(movieId);
            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task<IEnumerable<MovieDTO>> GetByCategoryAsyncService(int categoryId)
        {
            var movies = _movieRepository.GetByCategoryRepository(categoryId);
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<IEnumerable<MovieDTO>> SearchByNameAsyncService(string name)
        {
            var movies = _movieRepository.SearchByNameRepository(name);
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<MovieDTO> CreateAsyncService(MovieCreateDTO movieCreateDTO)
        {
            if (await ExistsByNameAsyncService(movieCreateDTO.Name))
            {
                throw new InvalidOperationException("The film already exists");
            }

            var movie = _mapper.Map<Movie>(movieCreateDTO);
            movie.CreationDate = DateTime.Now;

            if (!_movieRepository.AddRepository(movie))
            {
                throw new Exception($"Something went wrong while saving the record {movie.Name}");
            }

            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task UpdateAsyncService(int movieId, MovieDTO movieDTO)
        {
            if (movieId != movieDTO.IdMovie)
            {
                throw new ArgumentException("ID mismatch");
            }

            var existingMovie = _movieRepository.GetByIdRepository(movieId);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {movieId} not found");
            }

            var movie = _mapper.Map<Movie>(movieDTO);
            movie.CreationDate = DateTime.Now;

            if (!_movieRepository.UpdateRepository(movie))
            {
                throw new Exception($"Something went wrong updating the record {movie.Name}");
            }
        }

        public async Task<bool> DeleteAsyncService(int movieId)
        {
            try
            {
                var movie = _movieRepository.GetByIdRepository(movieId);
                if (movie == null) return false;

                return _movieRepository.DeleteRepository(movie);
            }
            catch (Exception)
            {
                // Log the error if needed
                return false;
            }
        }

        public async Task<bool> ExistsByIdAsyncService(int movieId)
        {
            return _movieRepository.ExistsByIdRepository(movieId);
        }

        public async Task<bool> ExistsByNameAsyncService(string name)
        {
            return _movieRepository.ExistsByNameRepository(name);
        }
    }
}
