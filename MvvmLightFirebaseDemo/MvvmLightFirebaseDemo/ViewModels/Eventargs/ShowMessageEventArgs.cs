using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightFirebaseDemo.ViewModels.Eventargs
{
    public class ShowMessageEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
