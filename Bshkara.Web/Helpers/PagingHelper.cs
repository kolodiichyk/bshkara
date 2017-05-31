using System.Collections.Generic;

namespace Bshkara.Web.Helpers
{
    public static class PagingHelper
    {
        public const int DefaultPageSize = _pageSize25;
        public const int DefaultPageNumber = 1;

        private const int _pageSize5 = 5;
        private const int _pageSize10 = 10;
        private const int _pageSize25 = 25;
        private const int _pageSize50 = 50;
        private const int _pageSize100 = 100;

        private static readonly string PAGE_SIZE_SESSION_KEY = "PageSize";

        public static List<int> PageSizes = new List<int>
        {
            _pageSize5,
            _pageSize10,
            _pageSize25,
            _pageSize50,
            _pageSize100
        };

        public static int GetCurrentPageSizeFromSession()
        {
            return SessionHelper.Get(PAGE_SIZE_SESSION_KEY, DefaultPageSize);
        }

        public static void SaveCurrentPageSizeToSession(int pageSize)
        {
            SessionHelper.Set(PAGE_SIZE_SESSION_KEY, pageSize);
        }
    }
}