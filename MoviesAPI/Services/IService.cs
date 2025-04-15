namespace MoviesAPI.Services
{
    public interface IService<TDto, TCreateDto, TKey>
    {
        Task<IEnumerable<TDto>> GetAllAsyncService();
        Task<TDto> GetByIdAsyncService(TKey id);
        Task<TDto> CreateAsyncService(TCreateDto createDto);
        Task UpdateAsyncService(TKey id, TDto dto);
        Task<bool> DeleteAsyncService(TKey id);
        Task<bool> ExistsByIdAsyncService(TKey id);
    }
}
