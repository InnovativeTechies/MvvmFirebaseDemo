using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.Services;
using MvvmLightFirebaseDemo.ViewModels.Eventargs;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.ViewModels
{
    public class SignInPageViewModel : BaseViewModel
    {
        public string SignIn => "Sign in";
        public string EmailPlaceholder => "Email";
        public string PasswordPlaceholder => "Password";
        public string Email { get; set; }
        public string Password { get; set; }

        public RelayCommand LoginCommand =>
            new RelayCommand(
                async () =>
                {
                    if (!CheckValidation()) return;

                    await SimpleIoc.Default.GetInstance<IFirebaseManager>().LoginWithEmailPasswordAsync(Email, Password);
                    await GoBackIfAuthenticated();
                });


        private bool CheckValidation()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                OnShowMessage(new ShowMessageEventArgs {Message = "The Email must not be empty."});
                return false;
            }

            if (Password.Length < 6)
            {
                OnShowMessage(new ShowMessageEventArgs {Message = "Passwords must be at least 6 characters in length."});
                return false;
            }

            return true;
        }

        private async Task GoBackIfAuthenticated()
        {
            if (AuthService.Instance.Authenticated)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}