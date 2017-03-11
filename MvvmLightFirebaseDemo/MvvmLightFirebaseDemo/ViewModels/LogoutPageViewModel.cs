using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLightFirebaseDemo.Services;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.ViewModels
{
    public class LogoutPageViewModel: BaseViewModel
    {
        public string LogoutText => "Logout";

        public RelayCommand LogoutCommand =>
            new RelayCommand(
                async () =>
                {
                    var response = await AuthService.Instance.LogoutAsync();
                    await Application.Current.MainPage.Navigation.PopAsync(false);
                });
    }
}