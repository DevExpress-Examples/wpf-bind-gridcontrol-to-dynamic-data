using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using T1098897.Models;

namespace T1098897.ViewModels {
    public class ChartsViewModel : BaseViewModel {
        public ChartsViewModel() {
            Title = "ChartsView";
            Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items { get; private set; }

        async public void OnAppearing() {
            IEnumerable<Item> items = await DataStore.GetItemsAsync(true);
            Items.Clear();
            foreach (Item item in items) {
                Items.Add(item);
            }
        }
    }
}
