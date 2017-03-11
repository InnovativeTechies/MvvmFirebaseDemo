using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MvvmLightFirebaseDemo.ViewModels.Eventargs;

namespace MvvmLightFirebaseDemo.ViewModels
{
    public class BaseViewModel: ViewModelBase
    {
        public virtual event EventHandler<ShowMessageEventArgs> ShowMessage;

        public void OnShowMessage(ShowMessageEventArgs eventArgs)
        {
            ShowMessage?.Invoke(this, eventArgs);
        }
    }
}
