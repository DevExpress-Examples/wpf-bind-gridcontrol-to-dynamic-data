using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.CodeGenerators;

namespace ExpandoObject.ViewModels {
    [GenerateViewModel]
    public partial class MainViewModel {
        [GenerateProperty] private ObservableCollection<System.Dynamic.ExpandoObject> _items;

        public MainViewModel() {
            _items = new ObservableCollection<System.Dynamic.ExpandoObject>();

            for (var i = 0; i < 10; i++) {
                dynamic item = new System.Dynamic.ExpandoObject();
                item.Id = i;
                item.Name = $"Item {i}";
                item.CreatedAt = DateTime.Now.AddDays(i);

                _items.Add(item);
            }
        }

        [GenerateCommand]
        public void AddColumn() {
            foreach (var item in _items) {
                var dict = (IDictionary<string, object>)item;
                dict.Add($"Value {dict.Keys.Count}", _items.IndexOf(item) * dict.Keys.Count);
            }
        }
    }
}