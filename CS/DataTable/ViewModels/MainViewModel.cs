using System;
using System.Data;
using DevExpress.Mvvm.CodeGenerators;

namespace DataTable.ViewModels {
    [GenerateViewModel]
    public partial class MainViewModel {
        [GenerateProperty] private System.Data.DataTable _items;

        public MainViewModel() {
            _items = new System.Data.DataTable();

            _items.Columns.Add(new DataColumn("Id", typeof(int)));
            _items.Columns.Add(new DataColumn("Name", typeof(string)));
            _items.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            _items.Columns.Add(new DataColumn("Value", typeof(int), "Id * 10"));

            for (var i = 0; i < 10; i++)
                _items.Rows.Add(i, $"Item {i}", DateTime.Today.AddDays(i));
        }

        [GenerateCommand]
        public void AddColumn() {
            _items.Columns.Add(new DataColumn($"Value {_items.Columns.Count}", typeof(int),
                $"Id * {_items.Columns.Count * 10}"));
        }
    }
}