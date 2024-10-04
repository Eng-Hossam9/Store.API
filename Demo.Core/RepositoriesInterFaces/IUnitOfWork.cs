using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.RepositoriesInterFaces
{
    public interface IUnitOfWork
    {
        Task<int> CompletSaveChangesAsync();

        IGenaricRepo<TEntity,TKey> CreateRepository<TEntity, TKey>() where TEntity:BaseEntity<TKey>;
    }
}
