using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library_EF_App.Core.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Snyggt! 
        bool Any(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        // Förstår inte nedan riktigt. Find tar in en delagate (predicate?!?) (Expression<Func<TEntity), så att jag kan använda Lambda här. 
        // Sen skickar den ut en bool till en IEnumerable<TEntity>. Hur fungerar det här?

        /*
        Jo, tänk såhär,
        
            Vi har x-antal TEntity i listan.
            För varje iteration i listan körs vårt expression.
            då skickas iterationens nuvarande TEntity in till vår function.
            I vår function har vi då tillgång till TEntity och behöver avgöra ifall vi är intresserade av den.
            är vi intresserade returnerar vi true annars false.
            Alla iterationer där vår expression returnerar true kommer finnas i listan.

            ex, om vi enbart vill ha alla böcker som innehåller bokstaven E

            unitOfWork.Books.Find(b=> b.Name.Contains("T));
             
             
        */
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
