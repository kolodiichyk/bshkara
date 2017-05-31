using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;
using Bshkara.Core.Base;
using Bshkara.Core.Services;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels.Bases;

namespace Bshkara.Web.Controllers.Bases
{
    public class BaseCRUDController<TEntity, TIndexViewModel, TService> : BaseAuthorizedController
        where TEntity : class, IIdentityEntity
        where TIndexViewModel : BaseListViewModel<TEntity>
        where TService : CRUDService<TEntity, TIndexViewModel>
    {
        protected TService _service;
        protected IUnitOfWork _unitOfWork;
        protected TIndexViewModel _viewModel;

        public BaseCRUDController(IUnitOfWork unitOfWork, TService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public ActionResult Index(FilterArgs args)
        {
            _viewModel = _service.GetViewModelForIndex(args);
            return View(_viewModel);
        }

        public ActionResult Create()
        {
            PrepareForEditOrCreate();
            return View(Activator.CreateInstance<TEntity>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TEntity entity, string returnUrl = null)
        {
            CustomValidation(entity);

            if (ModelState.IsValid)
            {
                _service.InsertOrUpdate(entity);
                AfterEditOrCreate(entity);
                return !string.IsNullOrWhiteSpace(returnUrl)
                    ? RedirectToLocal(returnUrl)
                    : RedirectToAction("Index");
            }

            PrepareForEditOrCreate(entity);

            return View(entity);
        }

        public virtual ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = _service.GetEntity(id);
            if (entity == null || entity.IsDeleted)
            {
                return HttpNotFound();
            }

            PrepareForEditOrCreate(entity);

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEntity entity, string returnUrl = null)
        {
            CustomValidation(entity);

            if (ModelState.IsValid)
            {
                try
                {
                    _service.InsertOrUpdate(entity);
                    AfterEditOrCreate(entity);
                    return !string.IsNullOrWhiteSpace(returnUrl)
                        ? RedirectToLocal(returnUrl)
                        : RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty,
                        BshkaraRes.Warning_DataWasChenged);
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("",
                        BshkaraRes.Warning_UnableToSave);
                }
            }

            PrepareForEditOrCreate(entity);

            return View(entity);
        }

        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new AjaxResponse("BadRequest"), JsonRequestBehavior.AllowGet);
            }

            var entity = _service.GetEntity(id);
            if (entity == null || entity.IsDeleted)
            {
                return Json(new AjaxResponse("NotFound"), JsonRequestBehavior.AllowGet);
            }

            var error = _service.DeleteEntity(entity);
            if (!string.IsNullOrWhiteSpace(error))
            {
                return Json(new AjaxResponse(error), JsonRequestBehavior.AllowGet);
            }

            AfterEditOrCreate(entity);

            return Json(new AjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = _service.GetEntity(id);

            if (entity == null || entity.IsDeleted)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        public ActionResult AutocompleteSearch(string term)
        {
            return Json(_service.AutocompleteSearch(term), JsonRequestBehavior.AllowGet);
        }

        public virtual void PrepareForEditOrCreate(TEntity entity = null)
        {
        }

        public virtual void AfterEditOrCreate(TEntity entity)
        {
        }

        public virtual void CustomValidation(TEntity entity)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}