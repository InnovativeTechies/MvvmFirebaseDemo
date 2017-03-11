using Android.App;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.Droid.implementations;
using MvvmLightFirebaseDemo.Droid.Renderers;
using MvvmLightFirebaseDemo.Services;
using MvvmLightFirebaseDemo.ViewModels;
using MvvmLightFirebaseDemo.Views;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookSignInPage), typeof(FacebookLoginPageRenderer))]

namespace MvvmLightFirebaseDemo.Droid.Renderers
{
    public class FacebookLoginPageRenderer : PageRenderer
    {
        protected override async void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var activity = Context as Activity;

            var facebookMnager = (FacebookManager)SimpleIoc.Default.GetInstance<IFacebookManager>();
			var viewModel = (FacebookSignInPageViewModel) ((FacebookSignInPage) Element).BindingContext;

            facebookMnager.Activity = activity;

            var token = await facebookMnager.LoginToFacebookAsync();

            await SimpleIoc.Default.GetInstance<IFirebaseManager>().FirebaseLoginWithFacebookAsync(token);

            await viewModel.FinishLoginAsync();
        }


	}

}