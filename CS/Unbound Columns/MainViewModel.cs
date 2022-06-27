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
using DevExpress.Mvvm.Xpf;

namespace Unbound_Columns {
    public class MainViewModel : ViewModelBase {
        public ObservableCollection<Column> Columns { get => GetProperty(() => Columns); set => SetProperty(() => Columns, value); }
        public ObservableCollection<Item> Items { get; }

        public MainViewModel() {
            Items = new ObservableCollection<Item>(Enumerable.Range(0, 10).Select(i => new Item { Id = i }));
            Items.ForEach(x => {
                x["Name"] = $"Item {Items.IndexOf(x)}";
                x["CreatedAt"] = DateTime.Now.AddDays(Items.IndexOf(x));
            });

            Columns = new ObservableCollection<Column> {
                new Column() { FieldName = "Id" },
                new Column() { FieldName = "Name", UnboundType = UnboundColumnType.String },
                new Column() { FieldName = "CreatedAt", UnboundType = UnboundColumnType.DateTime }
            };

            OnCustomUnboundColumnDataCommand = new DelegateCommand<UnboundColumnRowArgs>(OnCustomUnboundColumnData);
            AddColumnCommand = new DelegateCommand(AddColumn);
        }

        public void OnCustomUnboundColumnData(UnboundColumnRowArgs e) {
            if (e.IsGetData) e.Value = Items[e.SourceIndex][e.FieldName];

            if (e.IsSetData) Items[e.SourceIndex][e.FieldName] = e.Value;
        }

        public void AddColumn() {
            Columns.Add(new Column { FieldName = $"Value {Columns.Count}", UnboundType = UnboundColumnType.Integer });
        }

        public ICommand OnCustomUnboundColumnDataCommand { get; }
        public ICommand AddColumnCommand { get; }
    }

    public class Item : BindableBase {
        private readonly Dictionary<string, object> _customFieldValues = new Dictionary<string, object>();
        public int Id { get => GetProperty(() => Id); set => SetProperty(() => Id, value); }

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

    public class Column : BindableBase {
        public string FieldName { get => GetProperty(() => FieldName); set => SetProperty(() => FieldName, value); }
        public UnboundColumnType? UnboundType { get => GetProperty(() => UnboundType); set => SetProperty(() => UnboundType, value); }
    }
}