namespace MoviesAPI.Repository
{
    public interface IRepository<TEntity, TKey>
    {
        IEnumerable<TEntity> GetAllRepository();
        TEntity GetByIdRepository(TKey id);
        bool AddRepository(TEntity entity);
        bool UpdateRepository(TEntity entity);
        bool DeleteRepository(TEntity entity);
        bool ExistsByIdRepository(TKey id);
        bool SaveRepository();
    }
}
