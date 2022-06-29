using System;
using System.Linq;
using T1098897.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T1098897.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage {
        public ItemsPage() {
            InitializeComponent();
            BindingContext = ViewModel = new ItemsViewModel();
        }

        ItemsViewModel ViewModel { get; }

        protected override void OnAppearing() {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}