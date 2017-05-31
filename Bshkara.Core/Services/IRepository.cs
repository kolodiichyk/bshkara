using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bshkara.Core.Base;

namespace Bshkara.Core.Services
{
    public interface IRepository<TEntity> where TEntity : IIdentityEntity
    {
        TEntity FindById(object id);
        void InsertGraph(TEntity entity);
        void InsertOrUpdate(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Insert(TEntity entity);

        RepositoryQuery<TEntity> Query();

        IQueryable<TEntity> Get(
            List<Expression<Func<TEntity, bool>>> filters = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>>
                includeProperties = null,
            int? page = null,
            int? pageSize = null);
    }
}