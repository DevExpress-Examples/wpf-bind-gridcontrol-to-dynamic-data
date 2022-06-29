using System;
using System.Linq;
using T1098897.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T1098897.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage {
        public AboutPage() {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }
    }
}