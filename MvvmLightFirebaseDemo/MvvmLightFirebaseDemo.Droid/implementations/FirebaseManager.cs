using System.Threading.Tasks;
using AutoMapper;
using MvvmLightFirebaseDemo.Models;
using Firebase.Auth;
using MvvmLightFirebaseDemo.AuthManagers;
using Newtonsoft.Json;
using PerpetualEngine.Storage;
using FirebaseUser = MvvmLightFirebaseDemo.Models.FirebaseUser;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.Droid.implementations
{
    public class FirebaseManager: IFirebaseManager
    {
		public bool IsAuthenticated => User != null;
        public FirebaseUser User{ get; private set; }

        private readonly FirebaseAuth _firebaseAuth = FirebaseAuth.Instance;
        private Firebase.Auth.FirebaseUser _user;
        private readonly SimpleStorage _storage = SimpleStorage.EditGroup("data");

        public async Task<FirebaseUser> FirebaseLoginWithFacebookAsync(string token)
        {

            var credential = FacebookAuthProvider.GetCredential(token);

            _user = (await _firebaseAuth.SignInWithCredentialAsync(credential)).User;

            SaveUser();

            return User;

        }

        public async Task<FirebaseUser> CreateUserWithEmailPasswordAsync(string email, string password, string displayName)
        {
            _user = (await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password)).User;

            var request = new UserProfileChangeRequest.Builder();
            request.SetDisplayName(displayName);

            await  _firebaseAuth.CurrentUser.UpdateProfileAsync(request.Build());

            SaveUser();

            return User;
        }

        public async Task<FirebaseUser> LoginWithEmailPasswordAsync(string email, string password)
        {
            _user = (await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password)).User;

            SaveUser();

            return User;

        }

        public async Task<GeneralResponse> LogoutAsync()
        {

            var task = new Task<GeneralResponse>(() =>
			{
				_firebaseAuth.SignOut();

			    User = null;
			    _user = null;

			    SaveUser();

				return new GeneralResponse { IsOK = true };
			});
				
			task.Start();


			return await task;
        }

        public void LoadUser()
        {
            
            if (_storage == null) return;

            if ( _storage.HasKey("FirebaseUser"))
            {
                var user = _storage.Get("FirebaseUser");
                if (user != null)
                {
                    User = JsonConvert.DeserializeObject<FirebaseUser>(user);
                }
            }

        }

        public void SaveUser()
        {

            MapUser();

            _storage.Put("FirebaseUser", JsonConvert.SerializeObject(User));

        }

        private void MapUser()
        {
            if (_user == null) return;

            User = Mapper.Map<FirebaseUser>(_user);

            User.IsOK = _user != null;

        }

    }
}