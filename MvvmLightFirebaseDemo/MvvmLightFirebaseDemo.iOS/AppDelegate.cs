using System;
using AutoMapper;
using Facebook.CoreKit;
using Firebase.Auth;
using Firebase.CloudMessaging;
using Foundation;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.iOS.implementations;
using MvvmLightFirebaseDemo.Models;
using MvvmLightFirebaseDemo.Services;
using UIKit;
using UserNotifications;

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

            Firebase.Analytics.App.Configure();

			SimpleIoc.Default.Register<IFirebaseManager, FirebaseManager>();
			SimpleIoc.Default.Register<IFacebookManager, FacebookManager>();
            SimpleIoc.Default.Register<ICommentService, CommentService>();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

			Mapper.Initialize(cfg => cfg.CreateMap<User, FirebaseUser>());

            return base.FinishedLaunching(app, options);
        }



        public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}


        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }


    }
}