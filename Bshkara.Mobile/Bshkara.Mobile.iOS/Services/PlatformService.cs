using Bshkara.Mobile.Services.PlatformService;
using UIKit;

namespace Bshkara.Mobile.iOS.Services
{
    public class PlatformService : IPlatformService
    {
        public double ConvertPixelsToDp(int pixelValue)
        {
            var dp = (double) (pixelValue/UIScreen.MainScreen.Scale);
            return dp;
        }

        public int ConvertDpToPixels(double dp)
        {
            var pixels = (int) (UIScreen.MainScreen.Scale*dp);
            return pixels;
        }
    }
}