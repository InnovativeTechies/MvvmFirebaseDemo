using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLightFirebaseDemo.ViewModels;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MainPageViewModel)BindingContext).AppearingCommand.Execute(null);
        }
    }
}
