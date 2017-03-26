using System;
using System.Collections.Concurrent;
using Firebase.CloudMessaging;
using Foundation;
using MvvmLightFirebaseDemo.Services;

namespace MvvmLightFirebaseDemo.iOS.implementations
{
    public class CommentService:  ICommentService
    {

        public BlockingCollection<Comment> Comments { get; private set; } = new BlockingCollection<Comment>();

        public event EventHandler<Comment> NewCommentReceived;


        public void AddComment(Comment comment)
        {
			Comments.TryAdd(comment);

			NewCommentReceived?.Invoke(this, comment);
            
        }

        

    }
}