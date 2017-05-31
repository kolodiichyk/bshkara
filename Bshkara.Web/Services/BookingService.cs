using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bashkra.ApiClient.Requests;
using Bashkra.Shared.Enums;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Extentions;
using Bshkara.Web.Helpers;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class BookingService : CRUDService<BookingEntity, BookingViewModel>
    {
        public BookingService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override BookingViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new BookingViewModel
            {
                Title = BshkaraRes.Booking_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<BookingEntity>().Query()
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
                {
                    case "en":
                        query.Filter(x => x.Maid.Name.En.Contains(args.SearchString));
                        break;
                    case "ar":
                        query.Filter(x => x.Maid.Name.Ar.Contains(args.SearchString));
                        break;
                }
            }

            // For agency user we will show only they maid with booking status in "Processing"
            var agency = HttpContext.Current.User.Identity.GetAgency();
            if (agency != null)
            {
                query.Filter(x => x.Maid.AgencyId == agency.Id && x.BookingStatus == BookingStatus.Processing);
            }

            if (args.BookingStatus != null)
            {
                query.Filter(x => x.BookingStatus == args.BookingStatus.Value);
            }

            query.Filter(x => x.IsDeleted == false);

            query.OrderBy(q => q.OrderBy(d => d.Maid.Name.En));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<BookingEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(BookingEntity entity)
        {
            return null;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                    $"select Id from booking where isDeleted = 0 and Id like N'%{key}%' order by Id")
                    .ToList();
        }

        public void Accept(BookingEntity booking)
        {
            if (booking == null)
            {
                return;
            }

            booking.BookingStatus = BookingStatus.Boocked;
            InsertOrUpdate(booking);
        }


        public string BookApi(BookingArgs args)
        {
            if (
                args == null || args.UserId == Guid.Empty || args.MaidId == Guid.Empty)
            {
                return
                    BshkaraRes.Booking_NotEnoughData;
            }
            var booking = UnitOfWork.Context.Set<BookingEntity>().FirstOrDefault(book =>
                book.MaidId == args.MaidId && book.UserId == args.UserId && !book.IsDeleted);

            if (booking == null)
            {
                try
                {
                    InsertOrUpdate(
                        new BookingEntity
                        {
                            BookingStatus = BookingStatus.Boocked,
                            MaidId = args.MaidId,
                            UserId = args.UserId,
                            IsDeleted = false,
                            Notes = args.Notes
                        }
                        );
                }
                catch (
                    Exception ex)
                {
                    return
                        ex.Message;
                }

                return null;
            }

            return BshkaraRes.Booking_AlreadyBooked;
        }

        public string UnBookApi(UnBookingArgs args)
        {
            if (args == null || args.UserId == Guid.Empty || args.MaidId == Guid.Empty)
            {
                return BshkaraRes.Booking_NotEnoughData;
            }

            var booking = UnitOfWork.Context.Set<BookingEntity>().FirstOrDefault(book =>
                book.MaidId == args.MaidId && book.UserId == args.UserId && !book.IsDeleted);
            if (booking != null)
            {
                if (booking.BookingStatus != BookingStatus.Processing)
                {
                    return DeleteEntity(booking);
                }

                return BshkaraRes.Booking_IsInProcessing;
            }

            return BshkaraRes.Booking_NotExists;
        }
    }
}