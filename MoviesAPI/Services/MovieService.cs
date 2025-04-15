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

        public async Task<List<MovieDTO>> GetAllAsyncService()
        {
            var movies = await _movieRepository.GetAllRepository();
            return _mapper.Map<List<MovieDTO>>(movies);
        }

        public async Task<MovieDTO> GetByIdAsyncService(int movieId)
        {
            var movie = await _movieRepository.GetByIdRepository(movieId);
            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task<IEnumerable<MovieDTO>> GetByCategoryAsyncService(int categoryId)
        {
            var movies = await _movieRepository.GetByCategoryRepository(categoryId);
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<IEnumerable<MovieDTO>> SearchByNameAsyncService(string name)
        {
            var movies = await _movieRepository.SearchByNameRepository(name);
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

            await _movieRepository.AddRepository(movie);
            await _movieRepository.SaveRepository();

            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task UpdateAsyncService(int movieId, MovieDTO movieDTO)
        {
            if (movieId != movieDTO.IdMovie)
            {
                throw new ArgumentException("ID mismatch");
            }

            var existingMovie = await _movieRepository.GetByIdRepository(movieId);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {movieId} not found");
            }

            var movie = _mapper.Map<Movie>(movieDTO);
            movie.CreationDate = DateTime.Now;

            _movieRepository.UpdateRepository(movie);
            await _movieRepository.SaveRepository();
        }

        public async Task<bool> DeleteAsyncService(int movieId)
        {
            var movie = await _movieRepository.GetByIdRepository(movieId);
            if (movie == null) return false;

            _movieRepository.DeleteRepository(movie);
            await _movieRepository.SaveRepository();
            return true;
        }

        public async Task<bool> ExistsByIdAsyncService(int movieId)
        {
            return await Task.FromResult(_movieRepository.ExistsByIdRepository(movieId));
        }

        public async Task<bool> ExistsByNameAsyncService(string name)
        {
            return await Task.FromResult(_movieRepository.ExistsByNameRepository(name));
        }
    }
}