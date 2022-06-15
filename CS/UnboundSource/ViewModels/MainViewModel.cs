using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DevExpress.Data;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace UnboundSource.ViewModels {
    public class MainViewModel : ViewModelBase {
        private readonly ObservableCollection<Item> _items;
        private readonly Random _random = new();
        private int _columnCount = 2;
        public DevExpress.Data.UnboundSource UnboundSource { get => GetProperty(() => UnboundSource); set => SetProperty(() => UnboundSource, value); }

        public MainViewModel() {
            _items = new ObservableCollection<Item>(Enumerable.Range(0, 10)
                                                              .Select(i => new Item { Id = i, Name = $"Item {i}" }));

            _items.ForEach(item => item["Value 1"] = Convert.ToDouble(item.Id));

            UnboundSource = new DevExpress.Data.UnboundSource();

            UnboundSource.ValueNeeded += (_, e) => {
                if (EqualityComparer<string>.Default.Equals(e.PropertyName, nameof(Item.Id)))
                    e.Value = _items[e.RowIndex].Id;
                else if (EqualityComparer<string>.Default.Equals(e.PropertyName, nameof(Item.Name)))
                    e.Value = _items[e.RowIndex].Name;
                else if (EqualityComparer<string>.Default.Equals(e.PropertyName, nameof(Item.CreatedAt)))
                    e.Value = _items[e.RowIndex].CreatedAt;
                else
                    e.Value = _items[e.RowIndex][e.PropertyName];
            };

            UnboundSource.ValuePushed += (_, e) => {
                if (EqualityComparer<string>.Default.Equals(e.PropertyName, nameof(Item.Id)))
                    _items[e.RowIndex].Id = (int)e.Value;
                else if (EqualityComparer<string>.Default.Equals(e.PropertyName, nameof(Item.Name)))
                    _items[e.RowIndex].Name = (string)e.Value;
                else if (EqualityComparer<string>.Default.Equals(e.PropertyName, nameof(Item.CreatedAt)))
                    _items[e.RowIndex].CreatedAt = (DateTime)e.Value;
                else _items[e.RowIndex][e.PropertyName] = e.Value;
            };

            UnboundSource.Properties.Add(new UnboundSourceProperty(nameof(Item.Id),
                typeof(Item).GetProperty(nameof(Item.Id))?.PropertyType));
            UnboundSource.Properties.Add(new UnboundSourceProperty(nameof(Item.Name),
                typeof(Item).GetProperty(nameof(Item.Name))?.PropertyType));
            UnboundSource.Properties.Add(new UnboundSourceProperty("Value 1", typeof(double)));

            UnboundSource.SetRowCount(_items.Count);

            AddColumnCommand = new DelegateCommand(AddColumn);
        }

        private void AddColumn() {
            _items.ForEach(item => item[$"Value {_columnCount}"] = _random.NextDouble());
            UnboundSource?.Properties.Add(new UnboundSourceProperty($"Value {_columnCount}", typeof(double)));
            UnboundSource?.SetRowCount(_items.Count);

            _columnCount++;
        }

        public ICommand AddColumnCommand { get; }
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