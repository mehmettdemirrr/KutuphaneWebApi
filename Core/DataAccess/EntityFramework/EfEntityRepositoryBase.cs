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
            DetachIfTracked(entity);

            if (entity is ISoftDelete softDeleteEntity)
            {
                softDeleteEntity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }

            _context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return filter == null
                ? query.ToList()
                : query.Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            DetachIfTracked(entity);

            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void DetachIfTracked(TEntity entity)
        {
            var pkProperty = _context.Model
                .FindEntityType(typeof(TEntity))
                .FindPrimaryKey()
                .Properties[0];

            var entityKey = _context.Entry(entity)
                .Property(pkProperty.Name).CurrentValue;

            var tracked = _context.ChangeTracker
                .Entries<TEntity>()
                .FirstOrDefault(e =>
                    e.Property(pkProperty.Name).CurrentValue?.Equals(entityKey) == true);

            if (tracked != null)
            {
                tracked.State = EntityState.Detached;
            }
        }
    }
}
