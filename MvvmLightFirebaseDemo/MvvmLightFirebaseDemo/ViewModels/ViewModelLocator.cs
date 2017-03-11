/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MvvmLightFirebaseDemo"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Dynamic;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MvvmLightFirebaseDemo.ViewModels;
using MvvmLightFirebaseDemo.Views;

namespace MvvmLightFirebaseDemo.Bootstrap
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public const string FacebookLoginPage = "FacebookLoginPage";
        public const string UserPagePage = "UserPage";

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainPageViewModel>();
       
        
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
             "CA1822:MarkMembersAsStatic",
             Justification = "This non-static member is needed for data binding purposes.")]
        public MainPageViewModel Main => ServiceLocator.Current.GetInstance<MainPageViewModel>();
    }
}