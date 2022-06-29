using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using T1098897.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T1098897.ViewModels {
    public class ItemsViewModel : BaseViewModel {
        Item _selectedItem;

        public ItemsViewModel() {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        public ObservableCollection<Item> Items { get; }

        public Command LoadItemsCommand { get; }

        public Command AddItemCommand { get; }

        public Command<Item> ItemTapped { get; }

        public Item SelectedItem {
            get => this._selectedItem;
            set {
                SetProperty(ref this._selectedItem, value);
                OnItemSelected(value);
            }
        }

        public async void OnAppearing() {
            SelectedItem = null;
            await ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand() {
            IsBusy = true;

            try {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items) {
                    Items.Add(item);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }
            finally {
                IsBusy = false;
            }
        }

        async void OnAddItem(object obj) {
            await Navigation.NavigateToAsync<NewItemViewModel>(null);
        }

        async void OnItemSelected(Item item) {
            if (item == null)
                return;
            await Navigation.NavigateToAsync<ItemDetailViewModel>(item.Id);
        }
    }
}