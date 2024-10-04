using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Repositories
{
    public class GenaricRepo<TEntity, TKey> : IGenaricRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenaricRepo(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {

                return (IEnumerable<TEntity>)await _context.Products.Include(p => p.Brand).Include(p => p.Type).ToListAsync();
            }

            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {

                var result = await (_context.Products.Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == id as int?)) as TEntity;
                return result;
            }


            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);

        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }




    }
}
