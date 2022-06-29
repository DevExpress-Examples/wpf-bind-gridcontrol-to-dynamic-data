using System;
using System.Linq;
using T1098897.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T1098897.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupPage : ContentPage {
        public PopupPage() {
            InitializeComponent();
            BindingContext = new PopupViewModel();
        }

        void OnButtonClicked(object sender, EventArgs e) {
            Popup.IsOpen = true;
        }
    }
}