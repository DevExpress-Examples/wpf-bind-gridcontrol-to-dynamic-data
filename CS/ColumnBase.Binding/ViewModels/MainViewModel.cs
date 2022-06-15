using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ColumnBase.Binding.ViewModels {
    public class MainViewModel : ViewModelBase {
        public ObservableCollection<IBaseItem> Items { get => GetProperty(() => Items); set => SetProperty(() => Items, value); }

        public MainViewModel() {
            Items = new ObservableCollection<IBaseItem>(Enumerable.Range(0, 10).Select(i =>
                new Item { Id = i, Name = $"Item {i}", CreatedAt = DateTime.Today.AddDays(i) }));
        }
    }

    public interface IBaseItem {
        DateTime CreatedAt { get; set; }
    }

    public interface IItem : IBaseItem {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class Item : BindableBase, IItem {
        public DateTime CreatedAt { get => GetProperty(() => CreatedAt); set => SetProperty(() => CreatedAt, value); }

        public int Id { get => GetProperty(() => Id); set => SetProperty(() => Id, value); }

        public string Name { get => GetProperty(() => Name); set => SetProperty(() => Name, value); }
    }
}