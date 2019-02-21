using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library_EF_App.Core.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        // Förstår inte nedan riktigt. Find tar in en delagate (predicate?!?) (Expression<Func<TEntity), så att jag kan använda Lambda här. 
        // Sen skickar den ut en bool till en IEnumerable<TEntity>. Hur fungerar det här?
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
