namespace Bshkara.Mobile.Services.PlatformService
{
    public interface IPlatformService
    {
        double ConvertPixelsToDp(int pixelValue);
        int ConvertDpToPixels(double dp);
    }
}