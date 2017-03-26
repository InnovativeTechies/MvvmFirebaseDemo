using System.Threading.Tasks;
using Facebook.LoginKit;
using MvvmLightFirebaseDemo.AuthManagers;
using UIKit;

namespace MvvmLightFirebaseDemo.iOS.implementations
{
    public class FacebookManager: IFacebookManager
    {
        public async Task<string> LoginToFacebookAsync()
        {
            var loginManager = new LoginManager();

            loginManager.Init();
            loginManager.LoginBehavior = LoginBehavior.Native;


            var loginResult = await loginManager.LogInWithReadPermissionsAsync(new[] {"public_profile", "user_birthday"},
                                                                               UIApplication
                                                                                   .SharedApplication.KeyWindow.RootViewController);

            return loginResult.Token.TokenString;
        }

    }
}