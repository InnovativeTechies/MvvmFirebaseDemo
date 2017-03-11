
using MvvmLightFirebaseDemo.ViewModels;


namespace MvvmLightFirebaseDemo.Views
{
	public partial class SignInPage : BaseView
	{

        public SignInPage(SignInPageViewModel loginPageViewModel):base(loginPageViewModel)
        {
			InitializeComponent();
		}
	}
}
