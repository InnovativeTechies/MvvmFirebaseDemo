using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.Services;
using MvvmLightFirebaseDemo.ViewModels.Eventargs;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        public string DisplayName{ get; set; }
        public string Email{ get; set; }
        public string Password{ get; set; }
        public string Password2{ get; set; }


        public RelayCommand CreateUserCommand =>
            new RelayCommand(
                async () =>
                {
                    if (!CheckValidation()) return;

                    await SimpleIoc.Default.GetInstance<IFirebaseManager>().CreateUserWithEmailPasswordAsync(Email, Password, DisplayName);

                    await GotoNextPageIfAuthenticated();
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

            if (Password != Password2)
            {
                OnShowMessage(new ShowMessageEventArgs {Message = "Passwords must match."});
                return false;
            }

            return true;
        }

        private async Task GotoNextPageIfAuthenticated()
        {
            if (AuthService.Instance.Authenticated)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}