using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.iOS.Renderers;
using MvvmLightFirebaseDemo.Services;
using MvvmLightFirebaseDemo.ViewModels;
using MvvmLightFirebaseDemo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookSignInPage), typeof(FacebookLoginPageRenderer))]
namespace MvvmLightFirebaseDemo.iOS.Renderers
{
    public class FacebookLoginPageRenderer : PageRenderer
    {
        //Avoid duplicated calling from the Facebook authentication.
        private bool _inOperation = false;

        public override async void ViewDidAppear(bool animated)
        {
            if (_inOperation)
            {
                return;
            }

            base.ViewDidAppear(animated);


            _inOperation = true;

            var token = await SimpleIoc.Default.GetInstance<IFacebookManager>().LoginToFacebookAsync();

            await SimpleIoc.Default.GetInstance<IFirebaseManager>().FirebaseLoginWithFacebookAsync(token);

            ((FacebookSignInPageViewModel) ((FacebookSignInPage) Element).BindingContext).FinishLoginAsync();

            _inOperation = false;
        }




    }
}