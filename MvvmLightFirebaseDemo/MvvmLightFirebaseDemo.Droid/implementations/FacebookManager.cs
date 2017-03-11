using System;
using System.Threading.Tasks;
using Android.App;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;


namespace MvvmLightFirebaseDemo.Droid.implementations
{
    public class FacebookManager: Java.Lang.Object, IFacebookCallback,IFacebookManager, IDisposable
    {
        public Activity Activity{ get; set; }

        private  TaskCompletionSource<string> _taskCompletion;

        public void OnCancel()
        {
        }

        public void OnError(FacebookException p0)
        {
        }

        public void OnSuccess(Java.Lang.Object p0)
        {
            var loginResult = (LoginResult) p0;

            _taskCompletion.TrySetResult(loginResult.AccessToken.Token);
        }

        public async Task<string> LoginToFacebookAsync()
        {
            _taskCompletion = new TaskCompletionSource<string>();

            var loginManager = LoginManager.Instance;

            loginManager.SetLoginBehavior( LoginBehavior.NativeWithFallback);

            loginManager.LogInWithReadPermissions(Activity , new[] { "public_profile", "user_birthday" });

            return await _taskCompletion.Task;
        }
    }
}