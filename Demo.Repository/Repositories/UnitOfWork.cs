using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Repository.Data.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable Repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            Repositories = new Hashtable();
        }
        public async Task<int> CompletSaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenaricRepo<TEntity, TKey> CreateRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!Repositories.ContainsKey(type))
            {
            var repository= new GenaricRepo<TEntity,TKey>(_context);
                Repositories.Add(type, repository);
            }
            return (IGenaricRepo<TEntity,TKey>) Repositories[type];
        }    }
}
