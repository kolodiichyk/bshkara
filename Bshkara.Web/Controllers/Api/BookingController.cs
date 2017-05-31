using System.Web.Mvc;
using Bashkra.ApiClient.Requests;
using Bashkra.ApiClient.Responses;
using Bshkara.Web.Services;

namespace Bshkara.Web.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class BookingController : BaseApiController
    {
        private BookingService _bookingService => DependencyResolver.Current.GetService<BookingService>();


        /// <summary>
        /// Get all countries
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("book")]
        public ApiResponse Book(BookingArgs args)
        {
            return TryInvoce(() => new ApiResponse
            {
                Lang = Language,
                Error = _bookingService.BookApi(args)
            });
        }

        /// <summary>
        /// Get all countries
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("unbook")]
        public ApiResponse UnBook(UnBookingArgs args)
        {
            return TryInvoce(() => new ApiResponse
            {
                Lang = Language,
                Error = _bookingService.UnBookApi(args)
            });
        }
    }
}