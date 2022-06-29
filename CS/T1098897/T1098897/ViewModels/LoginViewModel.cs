using System;
using Xamarin.Forms;

namespace T1098897.ViewModels {
    public class LoginViewModel : BaseViewModel {
        string userName;
        string password;

        public LoginViewModel() {
            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }

        public string UserName {
            get => this.userName;
            set => SetProperty(ref this.userName, value);
        }

        public string Password {
            get => this.password;
            set => SetProperty(ref this.password, value);
        }

        public Command LoginCommand { get; }

        async void OnLoginClicked() {
            await Navigation.NavigateToAsync<MainViewModel>();
        }

        bool ValidateLogin() {
            return !String.IsNullOrWhiteSpace(UserName)
                && !String.IsNullOrWhiteSpace(Password);
        }
    }
}
