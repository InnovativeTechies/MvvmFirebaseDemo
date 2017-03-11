using System.Collections.Generic;

namespace MvvmLightFirebaseDemo.Models
{
    public class FirebaseUser : GeneralResponse
    {
        public string DisplayName{ get; set; }

        public string Email{ get; set; }

        public bool IsAnonymous{ get; set; }

        public string PhotoUrl{ get; set; }

        public string ProviderId{ get; set; }

        public IList<string> Providers{ get; set; }

        public string Uid{ get; set; }

        public bool IsEmailVerified{ get; set; }
    }
}