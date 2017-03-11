using AutoMapper;
using Facebook.CoreKit;
using Firebase.Auth;
using Foundation;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.iOS.implementations;
using MvvmLightFirebaseDemo.Models;
using UIKit;

namespace MvvmLightFirebaseDemo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            App.LoadConfig();

            //Settings.AppID = ConfigurationManager.AppSettings["FacebookAppId"];
            //Settings.DisplayName = ConfigurationManager.AppSettings["FacebookDisplayName"];

            Firebase.Analytics.App.Configure();

			SimpleIoc.Default.Register<IFirebaseManager, FirebaseManager>();
			SimpleIoc.Default.Register<IFacebookManager, FacebookManager>();


            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

			Mapper.Initialize(cfg => cfg.CreateMap<User, FirebaseUser>());

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

       
    }
}