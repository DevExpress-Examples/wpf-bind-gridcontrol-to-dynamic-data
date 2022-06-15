using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Xpf.Data;

namespace VirtualSources.InfiniteAsyncSource.ViewModels {
    public class MainViewModel : ViewModelBase {
        private readonly ObservableCollection<Item> _items = new();
        public DevExpress.Xpf.Data.InfiniteAsyncSource Source { get => GetProperty(() => Source); set => SetProperty(() => Source, value); }

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

    public class Item : BindableBase {
        private readonly Dictionary<string, object> _customFieldValues = new();
        public DateTime CreatedAt { get => GetProperty(() => CreatedAt); set => SetProperty(() => CreatedAt, value); }
        public int Id { get => GetProperty(() => Id); set => SetProperty(() => Id, value); }
        public string Name { get => GetProperty(() => Name); set => SetProperty(() => Name, value); }

        public object this[string fieldName] {
            get {
                _customFieldValues.TryGetValue(fieldName, out var value);
                return value;
            }

            set {
                _customFieldValues[fieldName] = value;
                RaisePropertyChanged(fieldName);
            }
        }
    }
}