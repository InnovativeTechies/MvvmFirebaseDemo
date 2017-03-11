//using System;
//using System.Threading.Tasks;
//using GalaSoft.MvvmLight.Ioc;
//using MvvmLightFirebaseDemo.AuthManagers;
//using Xamarin.Facebook;
//using Xamarin.Facebook.Login;
//
//namespace MvvmLightFirebaseDemo.Droid.implementations
//{
//    public class FacebookCallBack: Java.Lang.Object, IFacebookCallback, IDisposable
//    {
//
//        public Action FinishAction{ get; set; }
//
//        public void OnCancel()
//        {
//        }
//
//        public void OnError(FacebookException p0)
//        {
//        }
//
//        public async void OnSuccess(Java.Lang.Object p0)
//        {
//            LoginResult loginResult = (LoginResult) p0;
//
//            await SimpleIoc.Default.GetInstance<IFirebaseManager>().FirebaseLoginWithFacebookAsync(loginResult.AccessToken.Token);
//
//            FinishAction?.Invoke();
//        }
//
//    }
//}