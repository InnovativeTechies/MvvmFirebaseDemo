using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmLightFirebaseDemo.Models;

namespace MvvmLightFirebaseDemo.AuthManagers
{
    public interface IFirebaseManager
    {
        bool IsAuthenticated{ get; }

        FirebaseUser User{ get; }

        Task<FirebaseUser> FirebaseLoginWithFacebookAsync(string token);
        Task<FirebaseUser> CreateUserWithEmailPasswordAsync(string email, string password, string displayName);
        Task<FirebaseUser> LoginWithEmailPasswordAsync(string email, string password);
        Task<GeneralResponse> SendComment(string topic, string comment);
        Task<GeneralResponse> LogoutAsync();
        Task<GeneralResponse> InitTopic(string topic);

        void LoadUser();
        void SaveUser();
    }
}