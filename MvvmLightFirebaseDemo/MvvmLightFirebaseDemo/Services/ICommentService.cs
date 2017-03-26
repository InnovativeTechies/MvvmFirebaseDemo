using System;
using System.Collections.Concurrent;

namespace MvvmLightFirebaseDemo.Services
{
    public interface ICommentService
    {
        event EventHandler<Comment> NewCommentReceived;

        BlockingCollection<Comment> Comments { get; }

        void AddComment(Comment comment);
    }
}