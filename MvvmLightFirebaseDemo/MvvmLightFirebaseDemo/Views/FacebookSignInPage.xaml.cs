using MvvmLightFirebaseDemo.ViewModels;

namespace MvvmLightFirebaseDemo.Views
{
    public partial class FacebookSignInPage : BaseView
    {
        public FacebookSignInPage(FacebookSignInPageViewModel facebookSignInPageViewModel): base(facebookSignInPageViewModel)
        {
            InitializeComponent();

        }
    }
}
