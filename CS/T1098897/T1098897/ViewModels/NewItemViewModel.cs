using DevExpress.XamarinForms.DataForm;
using System;
using System.Windows.Input;
using T1098897.Models;
using Xamarin.Forms;

namespace T1098897.ViewModels {
    public class NewItemViewModel : BaseViewModel {
        public const string ViewName = "NewItemPage";

        string text;
        string description;

        public NewItemViewModel() {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Text {
            get => this.text;
            set => SetProperty(ref this.text, value);
        }

        public string Description {
            get => this.description;
            set => SetProperty(ref this.description, value);
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SaveCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command CancelCommand { get; }

        bool ValidateSave() {
            return !String.IsNullOrWhiteSpace(this.text)
                && !String.IsNullOrWhiteSpace(this.description);
        }

        async void OnCancel() {
            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }

        async void OnSave() {
            Item newItem = new Item() {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }
    }
}
