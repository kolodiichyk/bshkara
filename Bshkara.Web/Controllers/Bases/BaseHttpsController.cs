using System;
using System.Web.Mvc;
using Bshkara.Web.Helpers;

namespace Bshkara.Web.Controllers.Bases
{
    public class BaseHttpsController : Controller
    {
        public TimeSpan TimeZoneOffset { get; set; }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            CultureHelper.SetCultureFromCookiesOrBrowser();
            SetTimeZoneOffsetFromCookies();

            return base.BeginExecuteCore(callback, state);
        }

        private void SetTimeZoneOffsetFromCookies()
        {
            TimeZoneOffset = TimeZoneOffsetHelper.GetTimeZoneOffsetFromCookies();
        }
    }
}