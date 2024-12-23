using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IGeneric<TEntity> where TEntity : class
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = false)
        {
            var result =  context.Set<TEntity>().Where(filter);
            if (!tracking)
            {
                result.AsNoTracking();
            }
            return await result.FirstOrDefaultAsync();
        }
        public async Task<int> AddAsync(TEntity entity)
        {
           context.Set<TEntity>().Add(entity);
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return await context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if(entity is null)
               return 0;
            
            context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync();
        }
    }
}
