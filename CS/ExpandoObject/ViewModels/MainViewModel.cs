using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ExpandoObject.ViewModels {
    public class MainViewModel : ViewModelBase {
        public ObservableCollection<System.Dynamic.ExpandoObject> Items { get => GetProperty(() => Items); set => SetProperty(() => Items, value); }

        public MainViewModel() {
            Items = new ObservableCollection<System.Dynamic.ExpandoObject>();

            for (var i = 0; i < 10; i++) {
                dynamic item = new System.Dynamic.ExpandoObject();
                item.Id = i;
                item.Name = $"Item {i}";
                item.CreatedAt = DateTime.Now.AddDays(i);

                Items.Add(item);
            }

            AddColumnCommand = new DelegateCommand(AddColumn);
        }

        public void AddColumn() {
            foreach (var item in Items) {
                var dict = (IDictionary<string, object>)item;
                dict.Add($"Value {dict.Keys.Count}", Items.IndexOf(item) * dict.Keys.Count);
            }
        }

        public ICommand AddColumnCommand { get; }
    }
}