using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChurchManager.Core.Persistence
{
    public interface IRepository<T>
        where T : class, IEntity, new()
    {
        T Get(int id, bool readOnly = false);

        void Save(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        IList<T> All(bool readOnly = false);

        IList<T> All(Expression<Func<T, bool>> predicate, bool readOnly = false);

        SearchResult<T> GetPageByCriteria(SearchRequest searchRequest, bool readOnly = true);
    }
}
