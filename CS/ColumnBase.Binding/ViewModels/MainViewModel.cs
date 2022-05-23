using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm.CodeGenerators;

namespace ColumnBase.Binding.ViewModels {
    [GenerateViewModel]
    public partial class MainViewModel {
        [GenerateProperty] private ObservableCollection<IBaseItem> _items;

        public MainViewModel() {
            _items = new ObservableCollection<IBaseItem>(Enumerable.Range(0, 10).Select(i =>
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

    [GenerateViewModel]
    public partial class Item : IItem {
        [GenerateProperty] private DateTime _createdAt;

        [GenerateProperty] private int _id;

        [GenerateProperty] private string _name;
    }
}