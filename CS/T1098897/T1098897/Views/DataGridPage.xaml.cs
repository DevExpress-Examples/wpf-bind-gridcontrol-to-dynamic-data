using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1098897.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T1098897.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataGridPage : ContentPage {
        public DataGridPage() {
            InitializeComponent();
            BindingContext = ViewModel = new DataGridViewModel();
        }

        DataGridViewModel ViewModel { get; }

        protected override void OnAppearing() {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}