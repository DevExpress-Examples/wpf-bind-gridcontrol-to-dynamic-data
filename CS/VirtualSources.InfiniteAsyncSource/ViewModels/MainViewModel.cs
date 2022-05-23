using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DevExpress.Mvvm.CodeGenerators;
using DevExpress.Xpf.Data;

namespace VirtualSources.InfiniteAsyncSource.ViewModels {
    [GenerateViewModel]
    public partial class MainViewModel {
        private readonly ObservableCollection<Item> _items = new();
        [GenerateProperty] private DevExpress.Xpf.Data.InfiniteAsyncSource _source;

        public MainViewModel() {
            _items = new ObservableCollection<Item>(Enumerable.Range(0, 10).Select(i =>
                new Item { Id = i, Name = $"Item {i}", CreatedAt = DateTime.Now.AddDays(i) }));

            Source = new DevExpress.Xpf.Data.InfiniteAsyncSource();
            Source.CustomProperties =
                new PropertyDescriptorCollection(TypeDescriptor.GetProperties(typeof(Item)).Cast<PropertyDescriptor>()
                                                               .ToArray());

            Source.FetchRows += (_, e) => {
                if (_items != null)
                    e.Result = Task.FromResult(new FetchRowsResult(_items.Cast<object>().ToArray(), false));
            };
        }
    }

    [GenerateViewModel]
    public partial class Item : INotifyPropertyChanged {
        private readonly Dictionary<string, object> _customFieldValues = new();
        [GenerateProperty] private DateTime _createdAt;
        [GenerateProperty] private int _id;
        [GenerateProperty] private string _name;

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