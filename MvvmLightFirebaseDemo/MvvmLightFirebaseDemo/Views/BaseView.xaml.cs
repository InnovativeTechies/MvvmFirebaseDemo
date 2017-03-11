using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLightFirebaseDemo.ViewModels;
using MvvmLightFirebaseDemo.ViewModels.Eventargs;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.Views
{
    public partial class BaseView : ContentPage
    {
        public BaseView()
        {
            InitializeComponent();
        }

        public BaseView(BaseViewModel viewModel) : this()
        {
            BindingContext = viewModel;
            viewModel.ShowMessage +=
                delegate(object sender, ShowMessageEventArgs args) { DisplayAlert(args.Title, args.Message, "OK"); };
        }
    }
}