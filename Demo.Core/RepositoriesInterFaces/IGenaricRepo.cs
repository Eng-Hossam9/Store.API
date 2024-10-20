﻿using Demo.Core.Models;
using Demo.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.RepositoriesInterFaces
{
    public interface IGenaricRepo<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Tkey Id);
        Task<IEnumerable<TEntity>> GetAllWihSpecAsync(ISpecifications<TEntity,Tkey> spec);
        Task<TEntity> GetByIdWihSpecAsync(ISpecifications<TEntity, Tkey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> CountAsync(ISpecifications<TEntity, Tkey> spec);
    }
}
