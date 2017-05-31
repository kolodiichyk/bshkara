using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Bshkara.Core.Base;
using Bshkara.Core.Services;

namespace Bshkara.DAL.DB
{
    /// <summary>
    /// <c>Repository</c>
    /// </summary>
    /// <typeparam name="TEntity">Database entity</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IdentityEntity
    {
        /// <summary>
        /// EF database context
        /// </summary>
        internal IDbContext Context;

        /// <summary>
        /// EF database set
        /// </summary>
        internal IDbSet<TEntity> DbSet;

        public Repository(IDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual TEntity FindById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void InsertGraph(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void InsertOrUpdate(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual RepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        public IQueryable<TEntity> Get(
            List<Expression<Func<TEntity, bool>>> filters = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>>
                includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filters != null)
            {
                query = filters.Aggregate(query, (current, filter) => current.Where(filter));
            }

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1)*pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }
    }
}