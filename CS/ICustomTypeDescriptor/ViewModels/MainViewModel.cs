using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DevExpress.Mvvm.CodeGenerators;
using DevExpress.Mvvm.Native;

namespace ICustomTypeDescriptor.ViewModels {
    [GenerateViewModel]
    public partial class MainViewModel {
        [GenerateProperty] private ObservableCollection<Item> _items;

        static MainViewModel() {
            ItemTypeDescriptor.CustomFields.Add(new ItemPropertyDescriptor(new CustomField("Name", typeof(string))));
            ItemTypeDescriptor.CustomFields.Add(
                new ItemPropertyDescriptor(new CustomField("CreatedAt", typeof(DateTime))));
        }

        public MainViewModel() {
            _items = new ObservableCollection<Item>(Enumerable.Range(0, 10).Select(i => new Item { Id = i }));
            _items.ForEach(x => x["Name"] = $"Item {_items.IndexOf(x)}");
            _items.ForEach(x => x["CreatedAt"] = DateTime.Now.AddDays(_items.IndexOf(x)));
        }

        [GenerateCommand]
        public void AddColumn() {
            var fieldCount = ItemTypeDescriptor.CustomFields.Count;
            ItemTypeDescriptor.CustomFields.Add(
                new ItemPropertyDescriptor(new CustomField($"Value {fieldCount}", typeof(int))));
            _items.ForEach(x => x[$"Value {fieldCount}"] = _items.IndexOf(x) * fieldCount);
        }
    }

    [GenerateViewModel]
    [TypeDescriptionProvider(typeof(ItemDescriptionProvider))]
    public partial class Item : INotifyPropertyChanged {
        private readonly Dictionary<string, object> _customFieldValues = new();

        [GenerateProperty] private int _id;

        public object this[string fieldName] {
            get {
                _customFieldValues.TryGetValue(fieldName, out var value);
                return value;
            }

            set {
                _customFieldValues[fieldName] = value;
                OnPropertyChanged(fieldName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}