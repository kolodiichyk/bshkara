using System;
using Android.Runtime;
using Xamarin.Facebook;
using Object = Java.Lang.Object;

namespace Bshkara.Mobile.Droid.Helpers
{
    public class FacebookCallback<TResult> : Object, IFacebookCallback where TResult : Object
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<TResult> HandleSuccess { get; set; }

        public void OnCancel()
        {
            HandleCancel?.Invoke();
        }

        public void OnError(FacebookException error)
        {
            HandleError?.Invoke(error);
        }

        public void OnSuccess(Object result)
        {
            HandleSuccess?.Invoke(result.JavaCast<TResult>());
        }
    }
}