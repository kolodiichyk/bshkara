using System;
using System.Collections.Generic;
using Bshkara.Core.Base;
using Bshkara.Core.Services;
using Bshkara.Web.Extentions;
using Bshkara.Web.Helpers;
using Bshkara.Web.Models;

namespace Bshkara.Web.Services.Bases
{
    public abstract class CRUDService<TEntity, TViewModel> where TEntity : IIdentityEntity
    {
        public CRUDService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        protected string Lang => CultureHelper.GetCurrentNeutralCulture();

        public abstract TViewModel GetViewModelForIndex(FilterArgs args);

        public virtual TEntity GetEntity(Guid id)
        {
            return UnitOfWork.Repository<TEntity>().FindById(id);
        }

        public void InsertOrUpdate(TEntity entity)
        {
            var e = entity as IdentityEntity;

            if (e.Id == Guid.Empty)
            {
                UnitOfWork.Repository<TEntity>().Insert(entity);
            }
            else
            {
                UnitOfWork.Repository<TEntity>().Update(entity);
            }

            UnitOfWork.Save();
        }

        // return empty string if success or details error message if not success
        public abstract string CanDeleteEntity(TEntity entity);

        public string DeleteEntity(TEntity entity)
        {
            var errorMsg = CanDeleteEntity(entity);
            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                return errorMsg;
            }

            try
            {
                if (entity != null)
                {
                    entity.IsDeleted = true;
                    UnitOfWork.Repository<TEntity>().Update(entity);
                    UnitOfWork.Save();
                }
                else
                {
                    UnitOfWork.Repository<TEntity>().Delete(entity);
                    UnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                return ex.GetExceptionDetailsAsString();
            }

            // success
            return "";
        }

        public abstract List<string> AutocompleteSearch(string key);

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}