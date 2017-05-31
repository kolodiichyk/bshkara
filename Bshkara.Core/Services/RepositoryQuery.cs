using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bshkara.Core.Base;

namespace Bshkara.Core.Services
{
    public sealed class RepositoryQuery<TEntity> where TEntity : IIdentityEntity
    {
        private readonly List<Expression<Func<TEntity, object>>>
            _includeProperties;

        private readonly IRepository<TEntity> _repository;
        private List<Expression<Func<TEntity, bool>>> _filters;

        private Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> _orderByQuerable;

        private int? _page;
        private int? _pageSize;

        public RepositoryQuery(IRepository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties =
                new List<Expression<Func<TEntity, object>>>();
        }

        public RepositoryQuery<TEntity> Filter(params
            Expression<Func<TEntity, bool>>[] filters)
        {
            if (_filters == null)
            {
                _filters = filters.ToList();
            }
            else
            {
                _filters.AddRange(filters);
            }

            return this;
        }

        public RepositoryQuery<TEntity> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        public RepositoryQuery<TEntity> Include(
            Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> GetPage(
            int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filters).Count();

            return _repository.Get(
                _filters,
                _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        public int Count()
        {
            var query = _repository.Get(_filters);
            var totalCount = query.Count();
            return totalCount;
        }

        public IEnumerable<TEntity> Get()
        {
            return _repository.Get(
                _filters,
                _orderByQuerable, _includeProperties, _page, _pageSize);
        }
    }
}