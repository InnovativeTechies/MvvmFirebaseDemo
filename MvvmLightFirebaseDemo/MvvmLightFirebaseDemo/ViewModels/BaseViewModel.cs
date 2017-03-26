using System;
using GalaSoft.MvvmLight;
using MvvmLightFirebaseDemo.ViewModels.Eventargs;
using Xamarin.Forms;

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
