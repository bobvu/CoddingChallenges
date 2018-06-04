using CodingChallenges.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodingChallenges.DataAcess.Repos
{
    public interface IRepo<TEntity> where TEntity : AuditableEntity
    {
        void InsertOrUpdate(params TEntity[] entities);
        void Insert(params TEntity[] entities);
        void Update(params TEntity[] entities);
        void Update<TChange>(params TEntity[] entities);
        void Delete(params TEntity[] entities);
        TEntity Select(params object[] ids);
        TEntity Select(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> SelectBy(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> SelectBy(Expression<Func<TEntity, bool>> predicate, string sortField, string sortBy, int skip, int take, out int total);
        void Delete(params object[] ids);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Table { get; }
    }
}
