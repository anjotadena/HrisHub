using Microsoft.EntityFrameworkCore;

namespace HrisHub.Dal
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly HrisHubDbContext _dbContext;

        public CommonRepository(HrisHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetDetails(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Update(int id, T entity)
        {
            var currentEntity = await _dbContext.Set<T>().FindAsync(id);

            if (currentEntity == null)
            {
                return currentEntity;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return entity;
            }

            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
