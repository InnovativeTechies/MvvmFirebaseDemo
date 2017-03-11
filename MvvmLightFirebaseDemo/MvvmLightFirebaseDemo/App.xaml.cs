using System.Reflection;
using MvvmLightFirebaseDemo.Views;
using PCLAppConfig;
using Xamarin.Forms;


namespace MvvmLightFirebaseDemo
{
    public partial class App : Application
    {

        public static void LoadConfig()
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            ConfigurationManager.Initialise(assembly.GetManifestResourceStream("MvvmLightFirebaseDemo.app.config"));
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            
            // Handle when your app resumes
        }
    }
}