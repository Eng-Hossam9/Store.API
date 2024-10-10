using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.Specifications;
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

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> spec)
        {
            return await ApplyEvaluate(spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplyEvaluate(spec).FirstOrDefaultAsync();
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


        private IQueryable<TEntity> ApplyEvaluate(ISpecifications<TEntity, TKey> spec)
        {
          return  SpacificationEvaluate<TEntity, TKey>.GenerateQuery(_context.Set<TEntity>(), spec);
        }


    }
}
