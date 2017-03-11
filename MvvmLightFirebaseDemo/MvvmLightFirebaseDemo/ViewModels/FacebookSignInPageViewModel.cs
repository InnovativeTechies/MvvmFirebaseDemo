using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.ViewModels
{
    public class FacebookSignInPageViewModel : BaseViewModel
    {
        public event EventHandler AuthenticationFinished;

        //This method is called from the FacebookPageRenderer implementations
        public async Task FinishLoginAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}