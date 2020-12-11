using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Templatez.Domain.Core.Repositories;
using Templatez.Infra.Data.Core.Contexts;

namespace Templatez.Infra.Data.Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual ValueTask<EntityEntry<TEntity>> Add(TEntity obj) => DbSet.AddAsync(obj);

        public virtual ValueTask<TEntity> GetAsync(params object[] keyValues) => DbSet.FindAsync(keyValues);

        public virtual ValueTask<TEntity> GetById(Guid id) => DbSet.FindAsync(id);

        public virtual Task<List<TEntity>> GetAll() => DbSet.ToListAsync();

        public virtual EntityEntry<TEntity> Update(TEntity entity) => DbSet.Update(entity);

        public virtual EntityEntry<TEntity> Remove(TEntity entity) => DbSet.Remove(entity);

        public Task<int> SaveChanges() => Db.SaveChangesAsync();

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
