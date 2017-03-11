using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.Models;

namespace MvvmLightFirebaseDemo.Services
{
    public class AuthService
    {
        public static AuthService Instance = new AuthService();

        public bool Authenticated => SimpleIoc.Default.GetInstance<IFirebaseManager>().IsAuthenticated;

        public FirebaseUser User => SimpleIoc.Default.GetInstance<IFirebaseManager>().User;

        private AuthService()
        {
            SimpleIoc.Default.GetInstance<IFirebaseManager>().LoadUser();
        }

        public async Task<GeneralResponse> LogoutAsync()
        {
            return await SimpleIoc.Default.GetInstance<IFirebaseManager>().LogoutAsync();
        }
    
    }
}