using DevExpress.XamarinForms.Navigation;
using System;
using System.Linq;
using T1098897.ViewModels;

namespace T1098897.Views {
    public partial class MainPage : TabPage {
        public MainPage() {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
