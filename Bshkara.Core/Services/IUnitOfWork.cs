using System;
using System.Data.Entity;
using Bshkara.Core.Base;

namespace Bshkara.Core.Services
{
    public interface IUnitOfWork : IDisposable
    {
        Database Database { get; }
        IDbContext Context { get; }
        void Save();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : IIdentityEntity;
    }
}