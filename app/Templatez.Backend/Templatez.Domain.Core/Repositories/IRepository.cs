using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Templatez.Domain.Core.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        ValueTask<EntityEntry<TEntity>> Add(TEntity obj);
        ValueTask<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        EntityEntry<TEntity> Update(TEntity entity);
        EntityEntry<TEntity> Remove(TEntity entity);
        Task<int> SaveChanges();
    }
}
