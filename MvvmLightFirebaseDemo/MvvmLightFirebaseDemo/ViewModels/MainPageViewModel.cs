using System.Collections.Generic;
using System.ServiceModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MvvmLightFirebaseDemo.Bootstrap;
using MvvmLightFirebaseDemo.Services;
using MvvmLightFirebaseDemo.Views;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.ViewModels
{

	public class MainPageViewModel : ViewModelBase
	{
		public string Title => "MvvmFirebaseDemo";
		public string SignInWithAccount => "Sign in";
	    public string SignUpWithAccount => "Sign up";
        public string LogoutAccount => "Logout";
        public string SignInWithFacebook => "Sign in with Facebook";

	    public bool LoginButtonsAreVisible
		{
		    get { return _loginButtonsAreVisible; }
		    set { Set(ref _loginButtonsAreVisible, value);}
		}

	    public bool AuthenticatedControlsVisible
	    {
	        get { return _authenticatedControlsVisible; }
	        set { Set(ref _authenticatedControlsVisible, value);}
	    }

	    public List<Message> Messages
	    {
	        get { return _messages; }
	        set { Set(ref _messages, value);}
	    }

	    public string ProfilePhotoUrl
	    {
	        get { return _profilePhotoUrl; }
	        set { Set(ref _profilePhotoUrl, value);}
	    }

	    private readonly FacebookSignInPageViewModel _facebookSignInPageViewModel = new FacebookSignInPageViewModel();
        private readonly SignInPageViewModel _signInPageViewModel = new SignInPageViewModel();
	    private readonly LogoutPageViewModel _logoutPageViewModel = new LogoutPageViewModel();
	    private List<Message> _messages = new List<Message>();

	    private bool _loginButtonsAreVisible;
        private bool _authenticatedControlsVisible;
	    private string _profilePhotoUrl;

        public RelayCommand AppearingCommand =>
            new RelayCommand(
                () =>
                {
					LoginButtonsAreVisible = !AuthService.Instance.Authenticated;
                    AuthenticatedControlsVisible = AuthService.Instance.Authenticated;

					if (AuthService.Instance.Authenticated)
					{
					    ProfilePhotoUrl = AuthService.Instance.User.PhotoUrl;

                        Messages = new List<Message>
                        		{
                        		     new Message{ Text = "123", DisplayName = "aa" },
                        		     new Message{ Text = "456", DisplayName = "aa" }
                        		};
                    }
                });

        public RelayCommand ShowSignInPageCommand =>
            new RelayCommand(
                async () =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new SignInPage(_signInPageViewModel));
                });

	    public RelayCommand ShowCreateUserPageCommand =>
	        new RelayCommand(
	            async () =>
	            {
	                await Application.Current.MainPage.Navigation.PopAsync(false);
	                await Application.Current.MainPage.Navigation.PushAsync(new SignUpPage(new SignUpPageViewModel()));

	            });


        public RelayCommand SignInWithFacebookCommand =>
            new RelayCommand(
                async () =>
                {
                    await
                        Application.Current.MainPage.Navigation.PushAsync(new FacebookSignInPage(_facebookSignInPageViewModel));
                 
                });

	    public RelayCommand LogoutCommand =>
	        new RelayCommand(
	            async () =>
	            {
	                await
	                    Application.Current.MainPage.Navigation.PushAsync(
	                        new LogoutPage(_logoutPageViewModel));

	            });

        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public MainPageViewModel()
        {
            LoginButtonsAreVisible = !AuthService.Instance.Authenticated;
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}