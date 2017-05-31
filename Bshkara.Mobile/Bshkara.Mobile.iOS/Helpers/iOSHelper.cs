using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace Bshkara.Mobile.iOS.Helpers
{
    public static class iOSHelper
    {
        public static Task<UIViewController> GetCurrentViewControllerAsync()
        {
            var tcs = new TaskCompletionSource<UIViewController>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var window = UIApplication.SharedApplication.KeyWindow;
                    var vc = window.RootViewController;
                    while (vc.PresentedViewController != null)
                    {
                        vc = vc.PresentedViewController;
                    }
                    tcs.SetResult(vc);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });

            return tcs.Task;
        }
    }
}