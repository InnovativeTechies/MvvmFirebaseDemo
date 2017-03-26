using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.Services;
using MvvmLightFirebaseDemo.Views;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.ViewModels
{

	public class MainPageViewModel : ViewModelBase
	{
		public string Title => "MvvmFirebaseDemo";
		public string SignInWithAccountText => "Sign in";
	    public string SignUpWithAccountText => "Sign up";
        public string LogoutAccountText => "Logout";
        public string SignInWithFacebookText => "Sign in with Facebook";
	    public string NewCommentSendText => "Send";
	
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

	    public List<Comment> Comments
	    {
	        get { return _comments; }
	        set { Set(ref _comments, value);}
	    }

	    public string ProfilePhotoUrl
	    {
	        get { return _profilePhotoUrl; }
	        set { Set(ref _profilePhotoUrl, value);}
	    }

	    public string NewComment
	    {
	        get { return _newComment; }
	        set { Set(ref _newComment, value);}
	    }

	    public Comment LastComment
	    {
	        get { return _lastComment; }
	        set { Set(ref _lastComment, value);}
	    }

	    private readonly FacebookSignInPageViewModel _facebookSignInPageViewModel = new FacebookSignInPageViewModel();
        private readonly SignInPageViewModel _signInPageViewModel = new SignInPageViewModel();
	    private readonly LogoutPageViewModel _logoutPageViewModel = new LogoutPageViewModel();
	    private readonly IFirebaseManager _firebaseManager = SimpleIoc.Default.GetInstance<IFirebaseManager>();
	    private readonly ICommentService _messagingService = SimpleIoc.Default.GetInstance<ICommentService>();
		private const string _topic = "/topics/topic1";

	    private List<Comment> _comments = new List<Comment>();

	    private bool _loginButtonsAreVisible;
        private bool _authenticatedControlsVisible;
	    private string _profilePhotoUrl;
	    private string _newComment;
	    private Comment _lastComment;

	    public RelayCommand AppearingCommand =>
            new RelayCommand(
                () =>
                {
					LoginButtonsAreVisible = !AuthService.Instance.Authenticated;
                    AuthenticatedControlsVisible = AuthService.Instance.Authenticated;

					if (AuthService.Instance.Authenticated)
					{
					    ProfilePhotoUrl = AuthService.Instance.User.PhotoUrl;

                        _firebaseManager.InitTopic(_topic);
					   
					    Comments = _messagingService.Comments.ToList();
					}
                });


	    public RelayCommand SendNewCommentCommand =>
	        new RelayCommand(
	            async () =>
	            {
                    await _firebaseManager.SendComment(_topic, NewComment);
	                NewComment = "";
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

            _messagingService.NewCommentReceived += (sender, comment) =>
            {
                Comments = _messagingService.Comments.ToList();

                LastComment = Comments.Last();
            };


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