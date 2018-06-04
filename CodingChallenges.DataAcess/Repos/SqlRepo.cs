using CodingChallenges.DataAcess.DbContexts;
using CodingChallenges.DataAcess.Extensions;
using CodingChallenges.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenges.DataAcess.Repos
{
    public class SqlRepo<TEntity> : IRepo<TEntity> where TEntity : AuditableEntity
    {
        private readonly SqlContext _dbContext;

        public IQueryable<TEntity> Table => throw new NotImplementedException();

        public SqlRepo(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual TEntity Select(params object[] ids)
        {
            return _dbContext.Set<TEntity>().Find(ids);
        }

        public TEntity Select(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        public List<TEntity> SelectBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public List<TEntity> SelectBy(Expression<Func<TEntity, bool>> predicate, string sortField, string sortBy
            , int skip, int take, out int total)
        {
            var queryable = _dbContext.Set<TEntity>().Where(predicate);
            total = queryable.Count();
            if (sortBy.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                return queryable
                    .OrderByDescendingFieldName(sortField)
                    .Skip(skip).Take(take).ToList();
            }
            else
            {
                return queryable
                    .OrderByFieldName(sortField)
                    .Skip(skip).Take(take).ToList();
            }
        }

        public virtual void Delete(params TEntity[] entities)
        {
            
                foreach (var entity in entities)
                {
                    var dbEntityEntry = _dbContext.Entry(entity);

                    var entry = dbEntityEntry;
                    var dbSet = _dbContext.Set<TEntity>();

                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                    }

                    dbSet.Remove(entity);
                }

            _dbContext.SaveChanges();
            
        }

        public virtual void Delete(params object[] ids)
        {
            var dbSet = _dbContext.Set<TEntity>();
            var entities = new List<TEntity>();
            foreach (var id in ids)
            {
                var entity = dbSet.Find(id);
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }
            dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {

            var dbSet = _dbContext.Set<TEntity>();
            dbSet.RemoveRange(dbSet.Where(predicate));
            _dbContext.SaveChanges();
        }
        public virtual void Update(params TEntity[] entities)
        {

            foreach (var entity in entities)
            {
                var dbEntityEntry = _dbContext.Entry(entity);

                var entry = dbEntityEntry;

                if (entry.State == EntityState.Detached)
                {
                    var dbSet = _dbContext.Set<TEntity>();
                    dbSet.Attach(entity);
                }

                entry.State = EntityState.Modified;
            }

            _dbContext.SaveChanges();

        }

        public void Update<TChange>(params TEntity[] entities)
        {
            var propsToChange = typeof(TChange).GetProperties();
            var propsEntity = typeof(TEntity).GetProperties();


            foreach (var entity in entities)
            {
                var dbEntityEntry = _dbContext.Entry(entity);

                var entry = dbEntityEntry;

                if (entry.State == EntityState.Detached)
                {
                    var dbSet = _dbContext.Set<TEntity>();
                    dbSet.Attach(entity);
                }

                foreach (var temp in propsToChange)
                {
                    if (!temp.PropertyType.IsPrimitiveOrComplex()) continue;

                    if (propsEntity.Any(e => e.Name.Equals(temp.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        entry.Property(temp.Name).IsModified = true;
                    }
                    else
                    {
                        entry.Property(temp.Name).IsModified = false;
                    }
                }
            }

            _dbContext.SaveChanges();

        }
        public virtual void Insert(params TEntity[] entities)
        {

            var dbSet = _dbContext.Set<TEntity>();
            dbSet.AddRange(entities);
            _dbContext.SaveChanges();

        }

        public virtual void InsertOrUpdate(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.AddOrUpdate(entity);
                _dbContext.SaveChanges();
            }

        }
    }
}

