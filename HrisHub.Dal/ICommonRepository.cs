namespace HrisHub.Dal
{
    public interface ICommonRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetDetails(int id);

        Task<T> Insert(T entity);

        Task<T> Update(int id, T entity);

        Task<T> Delete(int id);
    }
}
