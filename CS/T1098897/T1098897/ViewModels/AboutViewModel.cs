using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace T1098897.ViewModels {
    public class AboutViewModel : BaseViewModel {
        public const string ViewName = "AboutPage";
        public AboutViewModel() {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.devexpress.com/xamarin/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}