using System.Threading.Tasks;

namespace MvvmLightFirebaseDemo.AuthManagers
{
    public interface IFacebookManager
    {
        Task<string> LoginToFacebookAsync();
    }
}