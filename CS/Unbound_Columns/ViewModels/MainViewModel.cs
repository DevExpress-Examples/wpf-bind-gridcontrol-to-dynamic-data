using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DevExpress.Data;
using DevExpress.Mvvm.CodeGenerators;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.Xpf;

namespace Unbound_Columns.ViewModels {
    [GenerateViewModel]
    public partial class MainViewModel {
        [GenerateProperty] private ObservableCollection<Column> _columns;
        [GenerateProperty] private ObservableCollection<Item> _items;

        public MainViewModel() {
            _items = new ObservableCollection<Item>(Enumerable.Range(0, 10).Select(i => new Item { Id = i }));
            _items.ForEach(x => {
                x["Name"] = $"Item {_items.IndexOf(x)}";
                x["CreatedAt"] = DateTime.Now.AddDays(_items.IndexOf(x));
            });

            _columns = new ObservableCollection<Column> {
                new() { FieldName = "Id" },
                new() { FieldName = "Name", UnboundType = UnboundColumnType.String },
                new() { FieldName = "CreatedAt", UnboundType = UnboundColumnType.DateTime }
            };
        }

        [GenerateCommand]
        public void OnCustomUnboundColumnData(UnboundColumnRowArgs e) {
            if (e.IsGetData) e.Value = _items[e.SourceIndex][e.FieldName];

            if (e.IsSetData) _items[e.SourceIndex][e.FieldName] = e.Value;
        }

        [GenerateCommand]
        public void AddColumn() {
            _columns.Add(new Column { FieldName = $"Value {_columns.Count}", UnboundType = UnboundColumnType.Integer });
        }
    }

    [GenerateViewModel]
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

    [GenerateViewModel]
    public partial class Column {
        [GenerateProperty] private string _fieldName;
        [GenerateProperty] private UnboundColumnType? _unboundType;
    }
}