using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Data;

namespace VirtualSources.InfiniteAsyncSource {
    public class MainViewModel : ViewModelBase {
        private readonly ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public PropertyDescriptorCollection CustomFields { get; }

        public MainViewModel() {
            _items = new ObservableCollection<Item>(Enumerable.Range(0, 10).Select(i =>
                new Item { Id = i, Name = $"Item {i}", CreatedAt = DateTime.Now.AddDays(i) }));

            CustomFields = new PropertyDescriptorCollection(TypeDescriptor.GetProperties(typeof(Item)).Cast<PropertyDescriptor>()
                                                               .ToArray());

            FetchRowsCommand = new DelegateCommand<FetchRowsAsyncArgs>(FetchRows);
        }

        public void FetchRows(FetchRowsAsyncArgs e) {
            if (_items != null)
                e.Result = Task.FromResult(new FetchRowsResult(_items.Cast<object>().ToArray(), false));
        }

        public ICommand FetchRowsCommand { get; }
    }

    public class Item : BindableBase {
        private readonly Dictionary<string, object> _customFieldValues = new Dictionary<string, object>();
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