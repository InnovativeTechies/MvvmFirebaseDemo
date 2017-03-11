using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AutoMapper;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.Droid.implementations;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Firebase;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.Models;
using PCLAppConfig;
using PerpetualEngine.Storage;

namespace MvvmLightFirebaseDemo.Droid
{
    [Activity(Label = "MvvmLightFirebaseDemo.Droid", Icon = "@android:color/transparent", MainLauncher = true,
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        private ICallbackManager _callbackManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			
            SimpleStorage.SetContext(ApplicationContext);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            App.LoadConfig();

            SetupFacebook();
            SetupFirebase();
		
            LoadApplication(new App());

			
        }

        private void SetupFacebook()
        {
			FacebookSdk.SdkInitialize(this);

			FacebookSdk.ApplicationId = ConfigurationManager.AppSettings["FacebookAppId"];

			SimpleIoc.Default.Register<IFacebookManager, FacebookManager>();

			_callbackManager = CallbackManagerFactory.Create();

			LoginManager.Instance.RegisterCallback(_callbackManager,
												   (IFacebookCallback)SimpleIoc.Default.GetInstance<IFacebookManager>());

        }

        private void SetupFirebase()
        {
			var options = new FirebaseOptions.Builder()
			.SetApplicationId(ConfigurationManager.AppSettings["FirebaseApplicationId"])
			.SetApiKey(ConfigurationManager.AppSettings["FirebaseAppiKey"])
			.SetDatabaseUrl(ConfigurationManager.AppSettings["FirebaseDatabaseUrl"])
			.SetGcmSenderId(ConfigurationManager.AppSettings["FirebaseSenderId"])
			.Build();

			var firebaseApp = FirebaseApp.InitializeApp(this, options);

            Mapper.Initialize(cfg => cfg.CreateMap<Firebase.Auth.FirebaseUser, FirebaseUser>());

			SimpleIoc.Default.Register<IFirebaseManager, FirebaseManager>();

		}

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _callbackManager.OnActivityResult(requestCode, (int) resultCode, data);
        }


    }
}