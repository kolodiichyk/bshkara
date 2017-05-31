using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Bshkara.Core.Services
{
    public interface IDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        DbEntityEntry Entry(object o);
    }
}