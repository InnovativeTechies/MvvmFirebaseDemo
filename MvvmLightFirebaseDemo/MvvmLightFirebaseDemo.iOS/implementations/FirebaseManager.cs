using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreImage;
using Firebase.Auth;
using Firebase.CloudMessaging;
using Firebase.Database;
using Foundation;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFirebaseDemo.AuthManagers;
using MvvmLightFirebaseDemo.Models;
using MvvmLightFirebaseDemo.Services;
using Newtonsoft.Json;
using PerpetualEngine.Storage;

namespace MvvmLightFirebaseDemo.iOS.implementations
{
    public class FirebaseManager : IFirebaseManager
    {
        public bool IsAuthenticated => User != null;
        public FirebaseUser User{ get; private set; }

        private User _user;
        private readonly SimpleStorage _storage = SimpleStorage.EditGroup("data");
        private readonly DatabaseReference _databaseReference = Database.DefaultInstance.GetRootReference();
        private ICommentService _commentService;
        private string _topic;

        public FirebaseManager()
        {
            LoadUser();
        }

        public async Task<FirebaseUser> FirebaseLoginWithFacebookAsync(string token)
        {
            var taskCompletion = new TaskCompletionSource<FirebaseUser>();

            Auth.DefaultInstance.SignIn(FacebookAuthProvider.GetCredential(token), (user, error) => Callback(taskCompletion, user, error));

            return await taskCompletion.Task;
        }

        public async Task<FirebaseUser> CreateUserWithEmailPasswordAsync(string email, string password, string displayName)
        {
            var taskCompletion = new TaskCompletionSource<FirebaseUser>();

            Auth.DefaultInstance.CreateUser(email, password, (user, error) => Callback(taskCompletion, user, error));

            //set displayname
            await taskCompletion.Task.ContinueWith(
                firebaseUser =>
                {
                    if (firebaseUser != null)
                    {
                        var request = Auth.DefaultInstance.CurrentUser.ProfileChangeRequest();
                        request.DisplayName = displayName;

                        request.CommitChanges(
                            error =>
                            {
                                //todo: maybe the displayname already is exist
                            }
                        );
                    }
                }
            );

            return await taskCompletion.Task;
        }

        public async Task<FirebaseUser> LoginWithEmailPasswordAsync(string email, string password)
        {
            var taskCompletion = new TaskCompletionSource<FirebaseUser>();

            Auth.DefaultInstance.SignIn(email, password, (user, error) => Callback(taskCompletion, user, error));

            return await taskCompletion.Task;
        }

        public async Task<GeneralResponse> SendComment(string topic, string comment)
        {
            var task = new Task<GeneralResponse>(() =>
            {

                var commentItem = new Comment {Text = comment, User = User, Topic = _topic};

                _databaseReference.GetChild(topic).GetChildByAutoId().SetValue(new NSString(JsonConvert.SerializeObject(commentItem)));

                return new GeneralResponse {IsOK = true};
            });

            task.Start();

            return await task;
        }

        public async Task<GeneralResponse> LogoutAsync()
        {
            _user = null;
            User = null;

            var task = new Task<GeneralResponse>(
                () =>
                {
                    NSError error;

                    Auth.DefaultInstance.SignOut(out error);

                    return new GeneralResponse {IsOK = error == null, ErrorMessage = error?.ToString()};
                }
            );

            task.Start();

            return await task;
        }

        public async Task<GeneralResponse> InitTopic(string topic)
        {
            CheckAndLoadMessagingService();

            var task = new Task<GeneralResponse>(
                () =>
                {

                    _databaseReference.GetChild(topic).ObserveEvent(DataEventType.ChildAdded, (snapshot) =>
                    {
                        var data = snapshot.GetValue<NSObject>();

                        try
                        {
                            var comment = JsonConvert.DeserializeObject<Comment>(data.ToString());

                            _commentService.AddComment(comment);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                    });

                return new GeneralResponse { IsOK = true };

                });

			task.Start();

			return await task;
        }

        public void LoadUser()
        {
            if (_storage == null) return;

            if (_storage.HasKey("FirebaseUser"))
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

        private void Callback(TaskCompletionSource<FirebaseUser> taskCompletionSource, User user, NSError error)
        {
            if (error != null)
            {
                FirebaseUser firebaseUser;
                AuthErrorCode errorCode;

                if (IntPtr.Size == 8)
                    // 64 bits devices
                    errorCode = (AuthErrorCode) (long) error.Code;
                else // 32 bits devices
                    errorCode = (AuthErrorCode) (int) error.Code;

                // More information about the errors https://firebase.google.com/docs/auth/ios/errors
                switch (errorCode)
                {
                    case AuthErrorCode.OperationNotAllowed:
                        firebaseUser = new FirebaseUser {IsOK = false, ErrorMessage = "Operation Not Allowed"};
                        break;
                    case AuthErrorCode.InvalidEmail:
                        firebaseUser = new FirebaseUser {IsOK = false, ErrorMessage = "Invalid Email"};
                        break;
                    case AuthErrorCode.UserDisabled:
                        firebaseUser = new FirebaseUser {IsOK = false, ErrorMessage = "User Disabled"};
                        break;
                    case AuthErrorCode.WrongPassword:
                        firebaseUser = new FirebaseUser {IsOK = false, ErrorMessage = "Wrong password"};
                        break;
                    default:
                        firebaseUser = new FirebaseUser {IsOK = false, ErrorMessage = "Errorcode: " + errorCode};
                        break;
                }

                taskCompletionSource.TrySetResult(firebaseUser);
            }
            else
            {
                _user = user;

                SaveUser();

                MapUser();

                taskCompletionSource.TrySetResult(User);
            }
        }

        private void MapUser()
        {
            if (_user == null) return;

            User = Mapper.Map<FirebaseUser>(_user);

            User.IsOK = _user != null;
        }

        private void CheckAndLoadMessagingService()
        {
            if (_commentService == null)
            {
                _commentService = (CommentService)SimpleIoc.Default.GetInstance<ICommentService>();
            }
        }
    }
}