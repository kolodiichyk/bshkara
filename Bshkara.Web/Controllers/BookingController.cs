using System;
using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class BookingController :
        BaseCRUDController
            <BookingEntity, BookingViewModel, BookingService>
    {
        public BookingController(IUnitOfWork unitOfWork, BookingService service) : base(unitOfWork, service)
        {
        }

        public ActionResult Accept(Guid id)
        {
            var book = _service.GetEntity(id);
            _service.Accept(book);

            return RedirectToAction("Index");
        }
    }
}