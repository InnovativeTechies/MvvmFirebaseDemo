using System;
using MvvmLightFirebaseDemo.Models;

namespace MvvmLightFirebaseDemo
{
    public class Comment
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedDate { get; set; } = DateTime.Now;

	    public string Text{ get; set; } = "";

        public FirebaseUser User{ get; set; } 

	    public string Topic{ get; set; } = "";

	}
}
