using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ChurchManager.Core.Persistence;
using NHibernate;
using NHibernate.Criterion;

namespace ChurchManager.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        protected readonly ISession Session;

        public RepositoryBase(ISession session)
        {
            Session = session;
        }

        public T Get(int id, bool readOnly = false)
        {
            T entity = Session.Get<T>(id);
            if (readOnly)
            {
                Session.Evict(entity);
            }

            return entity;
        }

        public void Save(T entity)
        {
            Session.Save(entity);
        }

        public void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }

        public void Delete(int id)
        {
            T entity = Get(id);
            if (entity == null)
            {
                throw new ArgumentException();
            }
            Delete(entity);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            IQueryOver<T, T> query = Session.QueryOver<T>();
            query.Where(predicate);

            return query.RowCount() > 0;
        }

        public IList<T> All(bool readOnly = false)
        {
            return All(null, readOnly);
        }

        public IList<T> All(Expression<Func<T, bool>> predicate, bool readOnly = false)
        {
            IQueryOver<T, T> query = Session.QueryOver<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (readOnly)
            {
                return query.ReadOnly().List();
            }

            return query.List();
        }

        public SearchResult<T> GetPageByCriteria(SearchRequest searchRequest, bool readOnly = true)
        {
            ICriteria countCriteria = Session.CreateCriteria<T>();
            ICriteria listCriteria = Session.CreateCriteria<T>();
            listCriteria.SetReadOnly(readOnly);

            IList<string> listAliases = new List<string>();
            ApplySortings(listCriteria, searchRequest.SortFields, searchRequest.Ascending, listAliases);

            IFutureValue<int> rowCount = countCriteria
                .SetProjection(Projections.RowCount())
                .FutureValue<int>();

            IList<T> list = listCriteria
                .SetFirstResult(searchRequest.Start)
                .SetMaxResults(searchRequest.PageSize)
                .List<T>();

            return new SearchResult<T>
            {
                List = list,
                Total = rowCount.Value
            };
        }

        public void Transaction(Action action)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                action();
                transaction.Commit();
            }
        }

        private void ApplySortings(ICriteria criteria, IList<string> sortings, bool ascending, IList<string> aliases)
        {
            if (sortings != null && sortings.Any())
            {
                foreach (string sortField in sortings)
                {
                    AddAlias(criteria, aliases, sortField);
                    criteria.AddOrder(ascending
                                              ? Order.Asc(sortField)
                                              : Order.Desc(sortField));
                }
            }
        }

        private void AddAlias(ICriteria criteria, IList<string> aliases, string field)
        {
            int idx = field.IndexOf('.');
            if (idx != -1)
            {
                string relation = field.Substring(0, idx);
                if (aliases.All(a => a != relation))
                {
                    criteria.CreateAlias(relation, relation);
                    aliases.Add(relation);
                }
            }
        }
    }
}
