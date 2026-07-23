using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            if (entity is ISoftDelete softDeleteEntity)
            {
                softDeleteEntity.IsDeleted = false;
            }

            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity is ISoftDelete softDeleteEntity)
            {
                softDeleteEntity.IsDeleted = true;
                var updatedEntity = _context.Entry(softDeleteEntity);
                updatedEntity.State = EntityState.Modified;
            }
            else
            {
                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
            }

            _context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(e => !EF.Property<bool>(e, "IsDeleted"));
            }

            return query.SingleOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(e => !EF.Property<bool>(e, "IsDeleted"));
            }

            return filter == null ? query.ToList() : query.Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
