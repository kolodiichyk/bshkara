using Android.Content;
using Bshkara.Mobile.Services.PlatformService;

namespace Bshkara.Mobile.Droid.Services
{
    public class PlatformService : IPlatformService
    {
        private static Context _context;

        public double ConvertPixelsToDp(int pixelValue)
        {
            var dp = (double) (pixelValue/_context.Resources.DisplayMetrics.Density);
            return dp;
        }

        public int ConvertDpToPixels(double dp)
        {
            var pixels = (int) (_context.Resources.DisplayMetrics.Density*dp);
            return pixels;
        }

        public static void Init(Context context)
        {
            _context = context;
        }
    }
}