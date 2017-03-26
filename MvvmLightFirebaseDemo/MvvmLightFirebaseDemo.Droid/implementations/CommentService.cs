using System;
using System.Collections.Concurrent;
using Firebase.Database;
using Firebase.Messaging;
using MvvmLightFirebaseDemo.Services;
using Newtonsoft.Json;

namespace MvvmLightFirebaseDemo.Droid.implementations
{
    public class CommentService: FirebaseMessagingService, ICommentService, IChildEventListener
    {

        public event EventHandler<Comment> NewCommentReceived;

        public BlockingCollection<Comment> Comments { get; private set; } = new BlockingCollection<Comment>();

        public void AddComment(Comment comment)
        {
            Comments.TryAdd(comment);

            NewCommentReceived?.Invoke(this, comment);

        }

        public void AddComment(Java.Lang.Object remoteMessage)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<Comment>(remoteMessage.ToString());

                AddComment(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void OnCancelled(DatabaseError error)
        {
        }

        public void OnChildAdded(DataSnapshot snapshot, string previousChildName)
        {
            AddComment(snapshot.Value);
        }

        public void OnChildChanged(DataSnapshot snapshot, string previousChildName)
        {
        }

        public void OnChildMoved(DataSnapshot snapshot, string previousChildName)
        {
        }

        public void OnChildRemoved(DataSnapshot snapshot)
        {
        }
    }
}