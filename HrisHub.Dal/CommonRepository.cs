using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrisHub.Dal
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly HrisHubDbContext _dbContext;

        private DbSet<T> table;

        public CommonRepository(HrisHubDbContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetDetails(int id)
        {
            return table.Find(id);
        }

        public void Insert(T item)
        {
            table.Add(item);
        }

        public void Update(T item)
        {
            table.Update(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            table.Remove(item);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
