using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace T1098897.ViewModels {
    public class ItemDetailViewModel : BaseViewModel {
        public const string ViewName = "ItemDetailPage";

        string text;
        string description;

        public string Id { get; set; }

        public string Text {
            get => this.text;
            set => SetProperty(ref this.text, value);
        }

        public string Description {
            get => this.description;
            set => SetProperty(ref this.description, value);
        }

        public async Task LoadItemId(string itemId) {
            try {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception) {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public override async Task InitializeAsync(object parameter) {
            await LoadItemId(parameter as string);
        }
    }
}
