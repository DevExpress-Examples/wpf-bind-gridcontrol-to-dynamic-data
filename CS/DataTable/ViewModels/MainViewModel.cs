using DevExpress.Mvvm;
using System;
using System.Data;
using System.Windows.Input;

namespace DataTable.ViewModels {
    public class MainViewModel : ViewModelBase {
        public System.Data.DataTable Items { get => GetProperty(() => Items); set => SetProperty(() => Items, value); }

        public MainViewModel() {
            Items = new System.Data.DataTable();

            Items.Columns.Add(new DataColumn("Id", typeof(int)));
            Items.Columns.Add(new DataColumn("Name", typeof(string)));
            Items.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            Items.Columns.Add(new DataColumn("Value", typeof(int), "Id * 10"));

            for (var i = 0; i < 10; i++)
                Items.Rows.Add(i, $"Item {i}", DateTime.Today.AddDays(i));

            AddColumnCommand = new DelegateCommand(AddColumn);
        }

        public void AddColumn() {
            Items.Columns.Add(new DataColumn($"Value {Items.Columns.Count}", typeof(int),
                $"Id * {Items.Columns.Count * 10}"));
        }

        public ICommand AddColumnCommand { get; }
    }
}