using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLightFirebaseDemo.ViewModels;
using Xamarin.Forms;

namespace MvvmLightFirebaseDemo.Views
{
    public partial class SignUpPage : BaseView
    {
        public SignUpPage(SignUpPageViewModel signUpPageViewModel) : base(signUpPageViewModel)
        {
            InitializeComponent();
        }
    }
}