using System;
using System.Collections;
using System.Data.Entity;
using Bshkara.Core.Base;
using Bshkara.Core.Services;

namespace Bshkara.DAL.DB
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;

        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public IDbContext Context => _context as EFDbContext;

        public Database Database => ((EFDbContext) _context).Database;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public IRepository<T> Repository<T>() where T : IIdentityEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof (T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof (Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof (T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>) _repositories[type];
        }
    }
}